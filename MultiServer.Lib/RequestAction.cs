using System;
using System.Diagnostics;
using System.IO;

namespace MultiServer.Lib {
    public enum RequestAction {
        GET,
        SET,
        OK,
        FAIL,
        CREATE,
        DELETE
    }

    [Serializable]
    public class Request {
        public RequestAction Action {
            [DebuggerStepThrough]
            get => this._action;
        }

        public object Data {
            [DebuggerStepThrough]
            get => this._object;
        }

        public int Id {
            [DebuggerStepThrough]
            get => this.id;

        }

        private int           id;
        private RequestAction _action;
        private object        _object;

        private Request(RequestAction action, object objectInfo, int id) {
            this._action = action;
            this._object = objectInfo;
            this.id      = id;
        }

        private Request() {
            this._action = default;
            this._object = default;
            this.id      = default;
        }

        public static Request BuildRequest(RequestAction action, int id = -1, object objectInfo = default) {
            if ( id == -1 )
                id = new Random().Next( 0, 100 );
            return new Request( action, objectInfo, id );
        }

        public static Request BuildOKRequest(int id = -1) {
            if ( id == -1 )
                id = new Random().Next( 0, 100 );
            return new Request( RequestAction.OK, 0, id );
        }

        public static Request BuildFAILRequest(int id = -1) {
            if ( id == -1 )
                id = new Random().Next( 0, 100 );
            return new Request( RequestAction.FAIL, 0, id );
        }

        public static Request BuildResponseRequest(RequestAction resp, int id = -1) {
            if ( id == -1 )
                id = new Random().Next( 0, 100 );
            return new Request( resp, 0, id );
        }

        public static void SendS(Stream stream, Request request) {
            var buff = ToBytesS( request );
            stream.Write( buff, 0, buff.Length );
        }

        public static byte[] ToBytesS(Request obj) { return Resources.ObjectToByteArray( obj ); }

        public static Request FromBytesS(byte[] bytes) { return (Request) Resources.ByteArrayToObject( bytes ); }

        public byte[] ToBytes() { return ToBytesS( this ); }

        public void FromBytes(byte[] bytes) {
            var req = FromBytesS( bytes );
            this._action = req._action;
            this._object = req._object;
        }

        public void ChangeId(int i) { this.id = i; }

        public                  void    ChangeAction(RequestAction newAction) { this._action = newAction; }
        public static           Request NoRequest()                           { return noRequest; }
        private static readonly Request noRequest = new Request();
    }
}
