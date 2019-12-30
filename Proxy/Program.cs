using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Proxy.Query;

namespace Proxy {
    public class ProxyClass {
        List<Thread> _runtime = new List<Thread>();

        private Client   c;
        private Receiver r;

        public IPAddress realServer;
        public int       port;

        public ProxyClass(int port, IPAddress realServer) {
            this.realServer = realServer;
            this.port       = port;
        }


        public void StartProxy() {
            this.c = new Client( this.port - 1, this.realServer );
            this.r = new Receiver( this.port );
            this.c.Start();
            this.r.Start();
        }

        public void Shutdown() {
            this.r.Shutdown();
            this.c.Shutdown();

            for ( int i = 0; i < this._runtime.Count; i++ ) {
                this._runtime[i].Abort();
            }
        }

        /// <summary>
        /// Start The FrowardThreads
        /// </summary>
        /// <param name="clientInterrupt">Interrupt for paketes Reserved from the real Server</param>
        /// <param name="serverInterrupt">Interrupt for paketes Reserved from the Client</param>
        public void StartForwarding(interrupt clientInterrupt, interrupt serverInterrupt) {
            var r = new Thread( () => tStart( ReservedPaketQueue, BlockedReservedPaketQueue, SendingClassifiedSockets,   clientInterrupt ) );
            var s = new Thread( () => tStart( SendingPaketQueue,  BlockedSendingPaketQueue,  ReservingClassifiedSockets, serverInterrupt ) );
            this._runtime.Add( r );
            this._runtime.Add( s );
            r.Start();
            s.Start();
        }

        void tStart(Queue<Paket> l, Queue<Paket> bq, List<ClassifiedSocket> s, interrupt it) {
            while ( true ) {
                if ( l.Count > 0 ) {
                    var i = l.Dequeue();
                    if ( it.TriggerInterrupt( i ) ) {
                        var cc = SendPaket( i, s );
                        Console.WriteLine( "[⏩] " + i.sender + " -> " + String.Join( ", ", cc ) + ": " + string.Concat( i._buffer.Select( b => b.ToString( "X2" ) ).ToArray() ) );
                    }
                    else {
                        bq.Enqueue( i );
                        var res = "[❌]" + i.sender + ": " + string.Concat( i._buffer.Select( b => b.ToString( "X2" ) ).ToArray() );
                        Console.WriteLine( res.Substring( 0, ( res.Length < 30 ) ? res.Length : 30 ) );
                    }
                }
            }
        }

        public List<ClassifiedSocket> SendPaket(Paket p, List<ClassifiedSocket> sockets, SendMode sm = SendMode.ToFirst) {
            if ( sockets.Count == 0 ) return new List<ClassifiedSocket> { ClassifiedSocket.Empty };
            var cc = new List<ClassifiedSocket>(); //s.First( cs => cs.port == i.port );
            try {
                switch (sm) {
                    case SendMode.ToAllByPort:
                        cc.AddRange( sockets );
                        break;
                    case SendMode.ToEny:
                        cc.Add( sockets[new Random().Next( 0, sockets.Count - 1 )] );
                        break;
                    case SendMode.ToFirst:
                        foreach ( var s in sockets ) {
                            if ( s.client == null ) continue;
                            cc.Add( s );
                            break;
                        }

                        break;
                    case SendMode.ToFirstByPort:
                        cc.Add( sockets.First( cs => cs.port == p.port ) );
                        break;
                    default: throw new NotImplementedException( nameof(sm) + sm, null );
                }
            } catch (Exception e) { Console.WriteLine( e.Message ); }

            foreach ( var se in cc ) {
                try { se.client.Client.Send( p._buffer ); } catch (Exception e) { }
            }
            return cc;
        }

        public enum SendMode {
            ToAllByPort, ToEny, ToFirst, ToFirstByPort
        }

        /// <summary>
        /// interrupt struct
        /// </summary>
        public struct interrupt {
            public static readonly interrupt EMPTY_INTERRUPT = new interrupt();


            // ReSharper disable once ArrangeThisQualifier
            public interrupt(Func<Paket, bool> subscribe) { interrupted = subscribe; }

            /// <summary>
            /// Interrupt takes a paket and returns if it shout be Forwarded
            /// <value>Paket p</value>
            /// <returns>bool</returns>
            /// </summary>
            public event Func<Paket, bool> interrupted;

            public bool TriggerInterrupt(Paket p) { return this.interrupted != null && this.interrupted.Invoke( p ); }
        }
    }

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
            public byte[]    _buffer;
            public int       port;
            public IPAddress sender;

            public Paket(byte[] buffer, int port, IPAddress sender) {
                this._buffer = buffer;
                this.port    = port;
                this.sender  = sender;
            }
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
            ReservingClassifiedSockets.Add( cs );

            var t = new Thread( () => {
                while ( tc.Connected ) {
                    if ( tc.Available > 0 ) {
                        var size = tc.Available < 4096 ? tc.Available : 4096;
                        var c    = new Paket( new byte[size], port, addr );
                        tc.Client.Receive( c._buffer );
                        ReservedPaketQueue.Enqueue( c );
                    }
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
        private ClassifiedSocket  cl;
        private Thread     clientThread;
        private IPEndPoint ipEndPoint;
        private int        port;
        IPAddress          ipAddress;

        public Client(int port, IPAddress ipAddress) {
            this.port       = port;
            this.ipAddress  = ipAddress;
            this.ipEndPoint = new IPEndPoint( ipAddress, port );
            this.cl         = new ClassifiedSocket(new TcpClient(),port,ipAddress);
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
                while ( true ) {
                    if ( this.cl.client.Available > 0 ) {
                        var size = this.cl.client.Available < 4096 ? this.cl.client.Available : 4096;
                        var c    = new Paket( new byte[size], this.port, this.ipAddress );
                        this.cl.client.Client.Receive( c._buffer );
                        SendingPaketQueue.Enqueue( c );
                    }
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