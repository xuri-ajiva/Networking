using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MultiServer.Lib;

namespace MultiServer {
    class Program {
        public static int             clients = 0;
        public static List<TcpClient> cls     = new List<TcpClient>();

        static void Main(string[] args) {
            new Thread( () => {
                while ( true ) {
                    SetTitle();
                    Thread.Sleep( 100 );
                }
            } ).Start();

            IPEndPoint ipe = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 1111 );

            var s = new TcpListener( ipe );
            s.Start();
            Console.WriteLine( "waiting for clients" );

            while ( true ) {
                var c = s.AcceptTcpClient();
                cls.Add( c );
                new ClientHandler( c ).StartHandel();
                Console.WriteLine( "client connected" );
                clients++;
            }
        }

        public static void SetTitle() { Console.Title = "objects: " + RequestHandler.Objects.Count + "    Succeeded:" + RequestHandler.Succeeded + "    Failed:" + RequestHandler.Failed + "    Clients:" + Program.clients; }
    }

    class ClientHandler {
        private const int BUFF_LEN = 10000;

        private Thread    _clinetThread;
        private TcpClient _connection;


        public ClientHandler(TcpClient connection) => this._connection = connection;

        public void StartHandel() {
            this._clinetThread = new Thread( Handle );
            this._clinetThread.Start();
        }

        private void Handle() {
            RequestHandler.ClientLoop( this._connection, OnRequest );
            Console.WriteLine( "client disconnected" );
            Program.clients--;
            Program.cls.Remove( this._connection );
        }

        private readonly BinaryFormatter bf = new BinaryFormatter();

        private readonly Request ignore = Request.NoRequest();

        private void OnRequest(Request obj) {
            Console.WriteLine( obj.Action + ": " + obj.Id );
            var send = RequestHandler.WorkOnRequest( obj );

            if ( send != this.ignore ) {
                this.bf.Serialize( this._connection.GetStream(), send );
            }

            Program.SetTitle();
        }

        void Shutdown() {
            this._connection?.Close();
            this._connection?.Dispose();
            this._clinetThread?.Abort();
        }
    }
}
