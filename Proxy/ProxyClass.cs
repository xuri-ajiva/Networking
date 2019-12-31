using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace Proxy {
    public class ProxyClass {
        public static bool ForwardingStarted = false;

        List<Thread> _runtime = new List<Thread>();

        private Client   c;
        private Receiver r;

        public IPAddress realServer;
        public int       portC;
        public int       portR;

        public ProxyClass(int portToConnect, int portToListen, IPAddress realServer) {
            this.realServer = realServer;
            this.portC       = portToConnect;
            this.portR = portToListen;
        }

        public void StartProxy() {
            this.c = new Client( this.portC, this.realServer );
            this.r = new Receiver( this.portR );
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
        public void StartForwarding(Interrupt clientInterrupt, Interrupt serverInterrupt) {
            if(ForwardingStarted) return;

            var r = new Thread( () => tStart( Query.ReservedPaketQueue, Query.BlockedReservedPaketQueue, Query.SendingClassifiedSockets,   clientInterrupt ) );
            var s = new Thread( () => tStart( Query.SendingPaketQueue,  Query.BlockedSendingPaketQueue,  Query.ReservingClassifiedSockets, serverInterrupt ) );
            this._runtime.Add( r );
            this._runtime.Add( s );
            r.Start();
            s.Start();
            ForwardingStarted = true;
        }

        void tStart(Queue<Query.Paket> l, Queue<Query.Paket> bq, List<Query.ClassifiedSocket> s, Interrupt it) {
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
                }else{Thread.Sleep( 10 );}
            }
        }

        public List<Query.ClassifiedSocket> SendPaket(Query.Paket p, List<Query.ClassifiedSocket> sockets, SendMode sm = SendMode.ToFirst) {
            if ( sockets.Count == 0 ) return new List<Query.ClassifiedSocket> { Query.ClassifiedSocket.Empty };
            var cc = new List<Query.ClassifiedSocket>(); //s.First( cs => cs.port == i.port );
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
        public struct Interrupt {
            public static readonly Interrupt EMPTY_INTERRUPT = new Interrupt();


            // ReSharper disable once ArrangeThisQualifier
            public Interrupt(Func<Query.Paket, bool> subscribe) { interrupted = subscribe; }

            /// <summary>
            /// Interrupt takes a paket and returns if it shout be Forwarded
            /// <value>Paket p</value>
            /// <returns>bool</returns>
            /// </summary>
            public event Func<Query.Paket, bool> interrupted;

            public bool TriggerInterrupt(Query.Paket p) { return this.interrupted != null && this.interrupted.Invoke( p ); }
        }
    }
}