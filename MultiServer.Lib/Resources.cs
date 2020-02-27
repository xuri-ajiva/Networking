using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace MultiServer.Lib {
    public static class Resources {
        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj) {
            var bf = new BinaryFormatter();

            using ( var ms = new MemoryStream() ) {
                bf.Serialize( ms, obj );
                return ms.ToArray();
            }
        }

        // Convert a byte array to an Object
        public static object ByteArrayToObject(byte[] arrBytes) {
            if ( arrBytes.Length == 0 ) return null;

            using ( var memStream = new MemoryStream() ) {
                var binForm = new BinaryFormatter();
                memStream.Write( arrBytes, 0, arrBytes.Length );
                memStream.Seek( 0, SeekOrigin.Begin );
                var obj = binForm.Deserialize( memStream );
                return obj;
            }
        }
    }
    public static class Extenstion {
        public static void Send(this Stream stream, Request req) { }


    }

    public static class RequestHandler {
        private static readonly object ObjectsLock = new object();

        public static int Succeeded = 0;
        public static int Failed    = 0;

        public static readonly Dictionary<int, object> Objects = new Dictionary<int, object>();

        static BackgroundWorker bw = new BackgroundWorker();

        public static Request WorkOnRequest(Request req) {
            var id = req.Id;

            lock (ObjectsLock) {
                switch (req.Action) {
                    case RequestAction.GET:
                        if ( Objects.ContainsKey( id ) )
                            return Request.BuildRequest( RequestAction.OK, id, Objects[id] );

                        break;
                    case RequestAction.SET:
                        if ( Objects.ContainsKey( id ) ) {
                            Objects[id] = req.Data;
                            return Request.BuildRequest( RequestAction.OK, id, 0 );
                        }

                        break;
                    case RequestAction.OK:
                        Succeeded++;
                        return Request.NoRequest();
                    case RequestAction.FAIL:
                        Failed++;
                        return Request.NoRequest();
                    case RequestAction.CREATE:
                        while ( Objects.ContainsKey( id ) ) {
                            id = new Random().Next( 1000, int.MaxValue - 100 );
                        }

                        Objects.Add( id, req.Data );
                        req.ChangeId( id );
                        req.ChangeAction( RequestAction.OK );

                        return req;
                    case RequestAction.DELETE:
                        if ( Objects.ContainsKey( id ) ) {
                            Objects.Remove( id );

                            return Request.BuildRequest( RequestAction.OK, id, 0 );
                        }

                        break;
                    default: throw new ArgumentOutOfRangeException( req.Action.ToString() );
                }

                return Request.BuildFAILRequest( id );
            }
        }


        public static void ClientLoop(TcpClient cl, Action<Request> onRequest, int idleSleepTime = 10) {
            var binForm = new BinaryFormatter();
            var stream  = cl.GetStream();

            while ( cl.Connected ) {
                try {
                    if ( cl.Available > 0 ) {
                        var obj = binForm.Deserialize( stream );

                        if ( obj is Request req ) {
                            onRequest?.Invoke( req );
                        }
                        else {
                            Console.WriteLine( "NoT RequestType: " + obj.GetType() + ": " + obj );
                        }
                    }
                    else {
                        Thread.Sleep( idleSleepTime );
                    }
                } catch (Exception e) {
                    Console.WriteLine( e.Message );
                }
            }
        }
    }
}
