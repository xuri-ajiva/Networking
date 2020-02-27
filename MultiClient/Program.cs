using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MultiServer.Lib;

namespace MultiClient {
    class Program {
        private const int BUFF_LEN              = 10000;
        private const int SLEEP_IN_TEST_I       = 100;
        private const int SLEEP_BETWEEN_TESTS_I = 1000;

        private const string LONG_NONSENSE1    = "ROJ3MMR20900XGGZYK417ROP9P0DXMKRH6RK7GPFXT7SARYKKA6DBGKY3IYZ8TIE37M2HITR6SA2MN4A99ZDV2CJFO5V12S93ZQQ7AK9ETYOJ5C75E26FCQUFMC07G4X98C1ROXM4JG9GT4MKWWR5VB8U9GAST3N7ZIMQQN8AEXK5HUQ47D20P77ZVEJBHTBCWRNSIICHZI09SAFWTOQS7P4XGKK8XBD9LAZN8B5A0RERC1Y36CZ11OR57XQOPRQ4TFM4SRBK11EUDPOYMNQO0OD3Q5FU4L3RI2MB0UO4WS80J4IEML6M9J252UNSLJL7ALIT7CPSO1T5I0G2AH8Q7EZGCFV3RUHHDKISYETQ5TSW4LQW8EQ3C24ECE9M91M951GLX6V3AONOW8OKBBZ4DA26PYR7QM5I0O9ZN2G6NO2Q9MQV9FN7LIJ7OVU0Y5RQ5W17YMM8OLCW0NYG14KD6HP903TZ53J1SDT1HGAYGNUQOPFYCE97WF83735BO1YXGFLB8G7FW0UXY9PGWB1KMQWA8PS1YCDW66A6X3MSEU2PC3ENQBADSPHTCBCFQ0G666FVRDO05O8AWAY893B2SWNIC1ETYJ4P6B3DH52Y1ZXTWTE03Y5JRFDRNU80U7C3SNLP51GLBF63CBBVROL7ZQB3G9OUP3584C9NNIOQCGW0K5S8BBPAOFIQN4TWGD4OV0DK4600TFPQFOWN3WWTYYG9N0S2WJSRNW40XII2MUI8WBCH61163FEC72B9HVLCU84H4VY5JX5LBA7ORCII42RUKCU8F1DM7OH84Q7J08O3KTT3J5L7AFHV3XJF5J0YE7CBR0QMQTTB41YR329OI7BLNFMJZAMRIN12L9OPRWX1CUPZW59T8ULOBJRRBJ64LUDY2YTBBLSH3YDJJUV4BBPS46ASOCUGCC5HD6ZV5EGRL8RSDC632FLJCF9MI0VNXIONQ80N8KLP1B8LRDH4S1MAN8JNUADJU2KXMMOOIC2P5WQC6JNUANAYGNG8BO8SRHWX6GR2Z4V2KNK434SOVQ9ZFYRH1HWGGTDY0F2AUICWTB3KSYXEC2PJXV9EBOJ93XRBMX2P8OCGRPM1HDYFQC4KFM9NZXLRCFJCNOXYSS1E1XBQQF075QPPG55PWXR83YAAYV5Y5VVR4MTKF8U7H616FL0GP4M2ALLGB2I9X0Y4AC8PXAZSEOY37HOHO6YJZJC8631K5ATLCVI43NJ4EKMJNC5XIPXBIKLR59J6JEXKJ5ZU8DZJ89EGHQY6GPSYUJRMCRKHAQM09RR10T8DJU302TP46HQWRX9ZAP3V2MEY159VS2J8HFA1KADE7GUQG20BSUC63WAHSFJGDMIFDXUZOPQTGA7RG71E8N8WT0AMTOJCUL59Z3YRK02VSZ5GX68NA6KWFKV36BFF13MR2KPSWHPEK0MWI5MRT9A3JZLPTSESW2B2Z2HTSUMS72R4UMC7U095AESXJ6S1IRM69HANZDH2XT5IVU1LMC63C717CUTKPXEM2UVG6UIHJ40CT337P0KYNHTI2QI2M6DQ05CJN8YF0KTLXTDHUHWDDWFZNJPUI0HLQ4NHYQ3KR7A3RBB24JORM81SJTR6ALEYY1Q4ZNPX145M72Q72VUIGB9BE27JTDN8DWIU76HCCZX4BLEJMZUXRP6YJJQ9TE12MABJMUXGS3X0WSA0KZTC4XNO85GO7FTLWHUBTDH889V2KXGA0P28FZNK0GGHTPFDIFOTOOYX40TALVKMIT2PQKKTE2VQ5VO3A6GN3K65JCIOFLJ8OTR1YGZ0ZBX5E0GBTI5HZULUUX4O6BGMWK5AZQUZJZ51V5T7GUO4FMT9G52LHSHGDUJ575PFJRJBJWBXRJCUCP6BF97V0V9QCMIPA1LJ55P4RP8YSCOXNZ64B5FW8B7Y01AL9H2V4QBQ8MT9QJ6QIYBO80KQSCM89WTRTKHS3IDLZKRO4CE1BVOV5O9G8QXAQO429AQENR6X7HB0HHV4EY0QZN2CLO34G6N8NVAJFDUIC1C4V6NJ1JK1Q9C";
        private const string LONG_NONSENSE10   = LONG_NONSENSE1 + LONG_NONSENSE1 + LONG_NONSENSE1 + LONG_NONSENSE1 + LONG_NONSENSE1 + LONG_NONSENSE1 + LONG_NONSENSE1 + LONG_NONSENSE1 + LONG_NONSENSE1                   + LONG_NONSENSE1;
        private const string LONG_NONSENSE100  = LONG_NONSENSE10 + LONG_NONSENSE10 + LONG_NONSENSE10 + LONG_NONSENSE10 + LONG_NONSENSE10 + LONG_NONSENSE10 + LONG_NONSENSE10 + LONG_NONSENSE10 + LONG_NONSENSE10          + LONG_NONSENSE10;
        private const string LONG_NONSENSE1000 = LONG_NONSENSE100 + LONG_NONSENSE100 + LONG_NONSENSE100 + LONG_NONSENSE100 + LONG_NONSENSE100 + LONG_NONSENSE100 + LONG_NONSENSE100 + LONG_NONSENSE100 + LONG_NONSENSE100 + LONG_NONSENSE100;
        private const string LONG_NONSENSE     = LONG_NONSENSE1000;


        static void Main(string[] args) {
            IPEndPoint ipe = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 1111 );

            Thread.Sleep( SLEEP_BETWEEN_TESTS_I );

            for ( int i = 0; i < 10; i++ ) {
                var cl = new TcpClient();
                cl.Connect( ipe );
                new Thread( () => {
                    Tests( cl );
                    cl.Close();
                } ).Start();
            }

            Console.ReadLine();
        }

        private static void OnRequest(Request obj) {
            Console.WriteLine( obj.Action );
            Console.WriteLine( obj.Id );
            Console.WriteLine( obj.Data );
        }

        public static void Tests(TcpClient cl) {
            try {
                List<int> ids = new List<int>();
                var       bf  = new BinaryFormatter();
                var       str = cl.GetStream();
                Request   resp;
                //while ( s.Connected ) {

                Thread.Sleep( SLEEP_BETWEEN_TESTS_I );
                Console.WriteLine( "-------------------CREATE-------------------" );

                for ( int i = 0; i < 50; i++ ) {
                    Thread.Sleep( SLEEP_IN_TEST_I );
                    bf.Serialize( str, Request.BuildRequest( RequestAction.CREATE, -1, i + " test: " + LONG_NONSENSE ) );
                    resp = (Request) bf.Deserialize( str );
                    PrintRequest( resp );
                    ids.Add( resp.Id );
                    bf.Serialize( str, Request.BuildResponseRequest( resp.Action, resp.Id ) );
                }

                Console.WriteLine( "--------------------GET------------------" );
                Thread.Sleep( SLEEP_BETWEEN_TESTS_I );

                foreach ( var id in ids ) {
                    Thread.Sleep( SLEEP_IN_TEST_I );
                    bf.Serialize( str, Request.BuildRequest( RequestAction.GET, id, 0 ) );
                    resp = (Request) bf.Deserialize( str );
                    PrintRequest( resp );
                    bf.Serialize( str, Request.BuildResponseRequest( resp.Action, id ) );
                }

                Console.WriteLine( "-------------------SET-------------------" );
                Thread.Sleep( SLEEP_BETWEEN_TESTS_I );

                var c = 0;

                foreach ( var id in ids ) {
                    Thread.Sleep( SLEEP_IN_TEST_I );
                    bf.Serialize( str, Request.BuildRequest( RequestAction.SET, id, ( "id is now: " + ++c + "   " + LONG_NONSENSE ) ) );
                    resp = (Request) bf.Deserialize( str );
                    PrintRequest( resp );
                    bf.Serialize( str, Request.BuildResponseRequest( resp.Action, id ) );
                }

                Console.WriteLine( "-------------------GET-------------------" );
                Thread.Sleep( SLEEP_BETWEEN_TESTS_I );

                foreach ( var id in ids ) {
                    Thread.Sleep( SLEEP_IN_TEST_I );
                    bf.Serialize( str, Request.BuildRequest( RequestAction.GET, id, 0 ) );
                    resp = (Request) bf.Deserialize( str );
                    PrintRequest( resp );
                    bf.Serialize( str, Request.BuildResponseRequest( resp.Action, id ) );
                }

                Console.WriteLine( "-------------------RND-------------------" );
                Thread.Sleep( SLEEP_BETWEEN_TESTS_I );

                Random r = new Random();

                for ( int i = 0; i < 10; i++ ) {
                    Thread.Sleep( SLEEP_IN_TEST_I );
                    bf.Serialize( str, Request.BuildRequest( RequestAction.GET, r.Next(), 0 ) );
                    resp = (Request) bf.Deserialize( str );
                    PrintRequest( resp );
                    bf.Serialize( str, Request.BuildResponseRequest( resp.Action, i ) );
                }

                Console.WriteLine( "-------------------DEL-------------------" );
                Thread.Sleep( SLEEP_BETWEEN_TESTS_I );

                foreach ( var id in ids ) {
                    Thread.Sleep( SLEEP_IN_TEST_I );
                    bf.Serialize( str, Request.BuildRequest( RequestAction.DELETE, id, 0 ) );
                    resp = (Request) bf.Deserialize( str );
                    PrintRequest( resp );
                    bf.Serialize( str, Request.BuildResponseRequest( resp.Action, id ) );
                }

                Console.WriteLine( "-------------------END-------------------" );
                Thread.Sleep( SLEEP_BETWEEN_TESTS_I );
                //}
            } catch (Exception e) {
                Console.WriteLine( e.Message );
            }
        }

        static void PrintRequest(Request req) {
            string translation = " ";
            object Data        = default;

            switch (req.Action) {
                case RequestAction.OK:
                case RequestAction.FAIL:
                    translation = "&"                         + req.Id;
                    Data        = req.Data + "(" + req.Action + ")";
                    break;

                case RequestAction.GET:
                case RequestAction.DELETE:
                    translation = " $" + req.Id;
                    Data        = req.Data;
                    break;
                case RequestAction.SET:
                case RequestAction.CREATE:
                    translation = "-> " + req.Id;
                    break;
            }

            if ( Data is string str && str.Length > 100 ) {
                Data = str.Substring( 0, 100 );
            }
            else Data = req.Data;

            Console.WriteLine( req.Action + ": " + translation + " Data:" + Data );
        }
    }
}
