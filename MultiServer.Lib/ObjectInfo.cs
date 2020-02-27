using System;
using System.Diagnostics;


namespace MultiServer.Lib {
    [Serializable]
    public class ObjectInfo {

        public void ChangeId(int Id) { this.id = Id; }


        public ObjectInfo() {
            this.objectType = default;
            this.content    = default;
            this.id         = default;
        }

        public ObjectInfo(Type objectType, object content, int id) {
            this.objectType = objectType;
            this.content    = content;
            this.id         = id;
        }

        public ObjectInfo(Type objectType, object content) {
            this.objectType = objectType;
            this.content    = content;
            this.id         = new Random().Next( 0, 10 );
        }


        private Type objectType;

        private object content;

        private int id;


        public Type ObjectType {
            [DebuggerStepThrough]
            get => this.objectType;
        }

        public object Content {
            [DebuggerStepThrough]
            get => this.content;
        }

        public int Id {
            [DebuggerStepThrough]
            get => this.id;
        }

        public static explicit operator ObjectInfo(byte[] bytes) => RequestConverter.Convert( bytes );
        public static explicit operator byte[](ObjectInfo objI)  => RequestConverter.Convert( objI );

        public byte[] ToBytes() { return ToBytesS( this ); }

        public void FromBytes(byte[] bytes) {
            var oj = FromBytesS( bytes );
            this.objectType = oj.objectType;
            this.content    = oj.content;
        }

        public static byte[] ToBytesS(ObjectInfo obj) { return Resources.ObjectToByteArray( obj ); }

        public static ObjectInfo FromBytesS(byte[] bytes) { return (ObjectInfo) Resources.ByteArrayToObject( bytes ); }

    }
}
