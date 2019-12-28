using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace replay_connection {
    class Program {
        static void Main(string[] args) {
            var s = new Socket( SocketType.Stream, ProtocolType.Tcp );
            s.Bind( new IPEndPoint( IPAddress.Any, 2222 ) );
            s.Listen( 1000 );
            Console.WriteLine( "Listen.." );
            while ( true ) {
                var c = s.Accept();
                Console.WriteLine( "got one..." );
                new Thread( () => {
                    while ( c.Connected ) {
                        if ( c.Available > 0 ) {
                            var size = c.Available < 1024 ? c.Available : 1024;
                            var bu   = new byte[size];
                            c.Receive( bu );
                            c.Send( bu );
                            Console.WriteLine( "[+] Received/Send " + size );
                        }
                    }
                } ).Start();
            }
        }
    }
}