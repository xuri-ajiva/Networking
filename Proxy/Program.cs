using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Proxy.Query;

namespace Proxy {
    public class ProxyClass {
        public void StartProxy(int port, IPAddress realServer) {
            new Client( port - 1, realServer ).Start();
            new Receiver( port ).Start();
        }

        /// <summary>
        /// Start The FrowardThreads
        /// </summary>
        /// <param name="clientInterrupt">Interrupt for paketes Reserved from the real Server</param>
        /// <param name="serverInterrupt">Interrupt for paketes Reserved from the Client</param>
        public void StartForwarding(interrupt clientInterrupt, interrupt serverInterrupt) {
            new Thread( () => tStart( ReservedPaketQueue, BlockedReservedPaketQueue, SendingClassifiedSockets, clientInterrupt ) ).Start();
            new Thread( () => tStart( SendingPaketQueue, BlockedSendingPaketQueue, ReservingClassifiedSockets, serverInterrupt ) ).Start();
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
                        cc.Add( sockets[0] );
                        break;
                    case SendMode.ToFirstByPort:
                        cc.Add( sockets.First( cs => cs.port == p.port ) );
                        break;
                    default: throw new NotImplementedException( nameof(sm) + sm, null );
                }
            } catch (Exception e) { Console.WriteLine( e.Message ); }

            cc.ForEach( c => c.client.Client.Send( p._buffer ) );
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

            new Thread( () => {
                while ( clientsAllowed-- > 0 ) {
                    handleClient( this._socket.AcceptTcpClient(), this.port );
                }
            } ).Start();
            return true;
        }

        void handleClient(TcpClient tc, int port) {
            var addr = ( (IPEndPoint) ( tc.Client.RemoteEndPoint ) ).Address;
            ReservingClassifiedSockets.Add( new ClassifiedSocket( tc, port, addr ) );
            Console.WriteLine( "addR" );
            while ( true ) {
                if ( tc.Available > 0 ) {
                    var size = tc.Available < 4096 ? tc.Available : 4096;
                    var c    = new Paket( new byte[size], port, addr );
                    tc.Client.Receive( c._buffer );
                    ReservedPaketQueue.Enqueue( c );
                }
            }
        }
    }

    public class Client {
        private TcpClient  cl;
        private IPEndPoint ipEndPoint;
        private int        port;
        IPAddress          ipAddress;

        public Client(int port, IPAddress ipAddress) {
            this.port       = port;
            this.ipAddress  = ipAddress;
            this.ipEndPoint = new IPEndPoint( ipAddress, port );
            this.cl         = new TcpClient();
        }

        public bool Start() {
            try {
                this.cl.Connect( this.ipEndPoint );
            } catch (Exception e) {
                Console.WriteLine( e.Message );
                return false;
            }

            SendingClassifiedSockets.Add( new ClassifiedSocket( this.cl, this.port, this.ipAddress ) );
            Console.WriteLine( "addS" );

            new Thread( () => {
                while ( true ) {
                    if ( this.cl.Available > 0 ) {
                        var size = this.cl.Available < 4096 ? this.cl.Available : 4096;
                        var c    = new Paket( new byte[size], this.port, this.ipAddress );
                        this.cl.Client.Receive( c._buffer );
                        SendingPaketQueue.Enqueue( c );
                    }
                }
            } ).Start();
            return true;
        }
    }
}