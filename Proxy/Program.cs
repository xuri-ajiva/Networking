using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Proxy.Query;

namespace Proxy {
    public static class Query {
        public static readonly Queue<Paket> ReservedPaketQueue = new Queue<Paket>();
        public static readonly Queue<Paket> SendingPaketQueue  = new Queue<Paket>();

        public static readonly Queue<Paket> BlockedReservedPaketQueue = new Queue<Paket>();
        public static readonly Queue<Paket> BlockedSendingPaketQueue  = new Queue<Paket>();

        public static readonly List<ClassifiedSocket> SendingClassifiedSockets   = new List<ClassifiedSocket>();
        public static readonly List<ClassifiedSocket> ReservingClassifiedSockets = new List<ClassifiedSocket>();

        public struct ClassifiedSocket {
            public static readonly ClassifiedSocket Empty = new ClassifiedSocket( null, int.MinValue, IPAddress.None );

            public TcpClient client;
            public int       port;
            public IPAddress sender;

            public ClassifiedSocket(TcpClient client, int port, IPAddress sender) {
                this.client = client;
                this.port   = port;
                this.sender = sender;
            }

            #region Overrides of ValueType

            /// <inheritdoc />
            public override string ToString() => $"[{this.sender}:{this.port}]";

            #endregion
        }

        public struct Paket {
            public static readonly Paket Empty = new Paket( null, 0, null );

            public byte[]    _buffer;
            public int       port;
            public IPAddress sender;

            public Paket(byte[] buffer, int port, IPAddress sender) {
                this._buffer = buffer;
                this.port    = port;
                this.sender  = sender;
            }

            public static bool operator *(Paket p1, Paket p2) { return  p1.port == p2.port && Equals( p1.sender, p2.sender ) && (( p1._buffer == null || p2._buffer == null ) || p1._buffer.Length == p2._buffer.Length); }
        }
    }

    public class Receiver {
        private TcpListener _socket;
        private int         port;

        List<Thread>           _runtime = new List<Thread>();
        List<ClassifiedSocket> _clients = new List<ClassifiedSocket>();

        public Receiver(int port) {
            this.port    = port;
            this._socket = new TcpListener( new IPEndPoint( IPAddress.Any, port ) );
        }

        public bool Start(int clientsAllowed = 1) {
            try {
                this._socket.Start( clientsAllowed );
            } catch (Exception e) {
                Console.WriteLine( e.Message );
                return false;
            }

            var t = new Thread( () => {
                while ( clientsAllowed-- > 0 ) {
                    handleClient( this._socket.AcceptTcpClient(), this.port );
                }
            } );
            t.Start();
            this._runtime.Add( t );
            return true;
        }

        void handleClient(TcpClient tc, int port) {
            var addr = ( (IPEndPoint) ( tc.Client.RemoteEndPoint ) ).Address;
            var cs   = new ClassifiedSocket( tc, port, addr );
            this._clients.Add( cs );
            ReservingClassifiedSockets.Add( cs );

            var t = new Thread( () => {
                while ( tc.Connected ) {
                    if ( tc.Available > 0 ) {
                        var size = tc.Available < 4096 ? tc.Available : 4096;
                        if ( size == 0 ) continue;
                        var c = new Paket( new byte[size], port, addr );
                        if ( !( c * Paket.Empty ) ) {
                            tc.Client.Receive( c._buffer );
                            ReservedPaketQueue.Enqueue( c );
                        }
                    }else{Thread.Sleep( 10 );}
                }
            } );
            t.Start();
            this._runtime.Add( t );
        }

        public void Shutdown() {
            this._socket.Stop();

            for ( int i = 0; i < this._clients.Count; i++ ) {
                this._clients[i].client?.Close();
                ReservingClassifiedSockets.Remove( this._clients[i] );
                this._clients[i] = ClassifiedSocket.Empty;
            }

            for ( int i = 0; i < this._runtime.Count; i++ ) {
                this._runtime[i].Abort();
            }
        }
    }

    public class Client {
        private ClassifiedSocket cl;
        private Thread           clientThread;
        private IPEndPoint       ipEndPoint;
        private int              port;
        IPAddress                ipAddress;

        public Client(int port, IPAddress ipAddress) {
            this.port       = port;
            this.ipAddress  = ipAddress;
            this.ipEndPoint = new IPEndPoint( ipAddress, port );
            this.cl         = new ClassifiedSocket( new TcpClient(), port, ipAddress );
        }

        public bool Start() {
            try {
                this.cl.client.Connect( this.ipEndPoint );
            } catch (Exception e) {
                Console.WriteLine( e.Message );
                return false;
            }

            SendingClassifiedSockets.Add( this.cl );
            Console.WriteLine( "addS" );

            var t = new Thread( () => {
                while ( this.cl.client != null ) {
                    if ( this.cl.client.Available > 0 ) {
                        var size = this.cl.client.Available < 4096 ? this.cl.client.Available : 4096;
                        if ( size == 0 ) continue;
                        var c = new Paket( new byte[size], this.port, this.ipAddress );
                        if ( !( c * Paket.Empty ) ) {
                            this.cl.client.Client.Receive( c._buffer );
                            SendingPaketQueue.Enqueue( c );
                        }
                    }else{Thread.Sleep( 10 );}
                }
            } );
            t.Start();
            this.clientThread = t;
            return true;
        }

        public void Shutdown() {
            SendingClassifiedSockets.Remove( this.cl );
            this.cl.client.Close();
            this.cl = ClassifiedSocket.Empty;
            this.clientThread.Abort();
        }
    }
}