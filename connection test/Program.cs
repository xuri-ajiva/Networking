using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace connection_test {
    class Program {
        static void Main(string[] args) {
            var s = new Socket( SocketType.Stream, ProtocolType.Tcp );
            s.Connect( IPAddress.Parse( "127.0.0.1" ), 9900 );
            new Thread( () => {
                int i = 10;
                while ( s.Connected ) {
                    s.Send( Enumerable.Repeat( (byte) 1, 1024 ).ToArray() );
                    Console.WriteLine( "[-] Send 1024 " );
                    Thread.Sleep( 1000 );
                }
            } ).Start();
            while ( s.Connected ) {
                if ( s.Available <= 0 ) continue;
                var size = s.Available < 1024 ? s.Available : 1024;
                s.Receive( new byte[size] );
                Console.WriteLine( "[+] Received " + size );
            }
        }
    }
}