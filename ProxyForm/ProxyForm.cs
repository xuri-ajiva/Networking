using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Proxy;

namespace ProxyForm {
    public partial class ProxyForm : Form {
        Start                     start  = new Start();
        List<proxyImplementation> proxys = new List<proxyImplementation>();

        List<Query.Paket> cPakets = new List<Query.Paket>();
        List<Query.Paket> sPakets = new List<Query.Paket>();


        public void CSubscribe(Query.Paket p, proxyImplementation pi) {
            this.cPakets.Add( p );

            var res = "[⏩] " + p.sender + ": " + string.Concat( p._buffer.Select( b => b.ToString( "X2" ) ).ToArray() );

            Invoke( new Action( () => ( pi.interuptC ? this.cliB : this.clie ).Items.Add( create( this.cPakets.Count - 1, res ) ) ) );
            //this.Invoke( new Action( () => this.clie.AppendText( res.Substring( 0, res.Length < 30 ? res.Length : 30 ) + "\n" ) ) );
        }

        public void SSubscribe(Query.Paket p, proxyImplementation pi) {
            this.sPakets.Add( p );

            var res = "[⏪] " + p.sender + ": " + string.Concat( p._buffer.Select( b => b.ToString( "X2" ) ).ToArray() );

            Invoke( new Action( () => ( pi.interuptS ? this.serB : this.serv ).Items.Add( create( this.sPakets.Count - 1, res ) ) ) );

            //this.Invoke( new Action( () => this.serv.AppendText( res.Substring( 0, res.Length < 30 ? res.Length : 30 ) + "\n" ) ) );
        }

        ListViewItem create(int id, string data) { return new ListViewItem( new string[] { id.ToString(), data.Substring( 0, data.Length < 30 ? data.Length : 30 ) } ); }

        public ProxyForm() { InitializeComponent(); }

        void BindAll(proxyImplementation pi) {
            pi.triggerCSubscribe += CSubscribe;
            pi.triggerSSubscribe += SSubscribe;
        }

        void add(proxyImplementation pv) {
            this.proxyList.Items.Add( new ListViewItem( new string[] { this.proxys.Count.ToString(), pv.Proxy.realServer.ToString(), pv.Proxy.port.ToString() } ) );
            this.proxys.Add( pv );
        }

        void StartNew(Start s) {
            var p  = new ProxyClass( s.getPort(), IPAddress.Parse( s.getIp() ) );
            var pv = new proxyImplementation( p );
            pv.StartForwarding();

            p.StartProxy();
            add( pv );
        }

        proxyImplementation GetFromList() {
            try {
                if ( this.proxyList.Items.Count == 0 || this.proxyList.SelectedItems.Count == 0 ) return null;
                var n = int.Parse( this.proxyList.SelectedItems[0].SubItems[0].Text );
                return this.proxys[n];
            } catch (Exception e) {
                return null;
            }
        }

        private proxyImplementation current;

        private void startBtw_Click(object sender, EventArgs e) {
            var s = new Start();
            if ( s.ShowDialog( this ) == DialogResult.OK ) {
                StartNew( s );
            }
        }

        private void ProxyForm_FormClosed(object sender, FormClosedEventArgs e) { Environment.Exit( 0 ); }

        private void ProxyForm_Load(object sender, EventArgs e) {
            this.columnHeader3.Width = -2;
            this.columnHeader4.Width = -2;
            this.columnHeader6.Width = -2;
            this.columnHeader8.Width = -2;

            var p  = new ProxyClass( 9900, IPAddress.Parse( "127.0.0.1" ) );
            var pv = new proxyImplementation( p );
            pv.triggerCSubscribe += this.CSubscribe;
            pv.triggerSSubscribe += this.SSubscribe;
            pv.StartForwarding();

            pv.Proxy.StartProxy();
            add( pv );
        }

        private void DoubleClick(object sender, EventArgs e) {
            PaketEditor edit = null;
            if ( ( sender as Control ).Name == nameof(this.serv) ) {
                edit = new PaketEditor( this.sPakets[int.Parse( this.serv.SelectedItems[0].SubItems[0].Text )] );
            }
            else if ( ( sender as Control ).Name == nameof(this.clie) ) {
                edit = new PaketEditor( this.cPakets[int.Parse( this.clie.SelectedItems[0].SubItems[0].Text )] );
            }
            else if ( ( sender as Control ).Name == nameof(this.cliB) ) {
                edit = new PaketEditor( this.cPakets[int.Parse( this.cliB.SelectedItems[0].SubItems[0].Text )] );
            }
            else if ( ( sender as Control ).Name == nameof(this.serB) ) {
                edit = new PaketEditor( this.cPakets[int.Parse( this.serB.SelectedItems[0].SubItems[0].Text )] );
            }
            else { MessageBox.Show( "Error: " + ( sender as Control ).Name ); }

            if ( edit != null ) edit.Show();
        }

        bool chechboxChangeAllowed() {
            if ( this.InterruptPaketS.Checked || this.InterruptPaketC.Checked ) {
                var p = GetFromList();
                if ( p != null ) {
                    this.sendInteruptC.Enabled = true;
                    this.sendInteruptS.Enabled = true;
                    this.proxyList.Enabled     = false;
                    this.current               = p;
                    return true;
                }
                else {
                    this.sendInteruptC.Enabled = false;
                    this.sendInteruptS.Enabled = false;
                    this.proxyList.Enabled     = true;
                    return false;
                }
            }

            this.sendInteruptC.Enabled = false;
            this.sendInteruptS.Enabled = false;
            this.proxyList.Enabled     = true;
            return false;
        }

        private void InterruptPaketS_CheckedChanged(object sender, EventArgs e) {
            if ( chechboxChangeAllowed() && this.InterruptPaketS.Checked ) {
                this.current.interuptS = true;
                return;
            }

            if ( this.InterruptPaketS.Checked ) {
                this.InterruptPaketS.Checked = false;
            }

            if ( this.current != null ) this.current.interuptS = false;
        }

        private void InterruptPaketC_CheckedChanged(object sender, EventArgs e) {
            if ( chechboxChangeAllowed() && this.InterruptPaketC.Checked ) {
                this.current.interuptC = true;
                return;
            }

            if ( this.InterruptPaketC.Checked ) {
                this.InterruptPaketC.Checked = false;
            }

            if ( this.current != null ) this.current.interuptC = false;
        }

        private void sendInteruptS_Click(object sender, EventArgs e) {
            while ( Query.BlockedSendingPaketQueue.Count > 0 ) {
                try {
                    var p = Query.BlockedSendingPaketQueue.Dequeue();
                    this.current.Proxy.SendPaket( p, Query.ReservingClassifiedSockets );

                    this.serB.Items.RemoveAt( 0 );
                } catch (Exception ex) { Console.WriteLine( ex.Message ); }
            }

            this.serB.Items.Clear();
        }

        private void sendInteruptC_Click(object sender, EventArgs e) {
            while ( Query.BlockedReservedPaketQueue.Count > 0 ) {
                try {
                    var p = Query.BlockedReservedPaketQueue.Dequeue();
                    this.current.Proxy.SendPaket( p, Query.SendingClassifiedSockets );
                    this.cliB.Items.RemoveAt( 0 );
                } catch (Exception ex) { Console.WriteLine( ex.Message ); }
            }

            this.cliB.Items.Clear();
        }

        private void sm(object sender, SplitterEventArgs e) {
            this.columnHeader3.Width = -2;
            this.columnHeader4.Width = -2;
            this.columnHeader6.Width = -2;
            this.columnHeader8.Width = -2;
        }

        public class proxyImplementation : IDisposable {
            private ProxyClass.interrupt cInterrupt;
            private ProxyClass.interrupt sInterrupt;

            public event Action<Query.Paket, proxyImplementation> triggerCSubscribe;
            public event Action<Query.Paket, proxyImplementation> triggerSSubscribe;
            public ProxyClass                                     Proxy;

            public bool interuptS = false, interuptC = false;

            public proxyImplementation(ProxyClass pc) {
                this.Proxy = pc;

                this.cInterrupt = new ProxyClass.interrupt( CSubscribe );
                this.sInterrupt = new ProxyClass.interrupt( SSubscribe );
            }


            public void StartForwarding() { this.Proxy.StartForwarding( this.cInterrupt, this.sInterrupt ); }

            public bool CSubscribe(Query.Paket p) {
                this.triggerCSubscribe?.Invoke( p, this );
                return !this.interuptC;
            }

            public bool SSubscribe(Query.Paket p) {
                this.triggerSSubscribe?.Invoke( p, this );
                return !this.interuptS;
            }

            public void Shutdown() { this.Dispose(); }

            #region IDisposable

            /// <inheritdoc />
            public void Dispose() {
                this.Proxy.Shutdown();
                triggerCSubscribe = null;
                triggerSSubscribe = null;
                this.cInterrupt   = ProxyClass.interrupt.EMPTY_INTERRUPT;
                this.sInterrupt   = ProxyClass.interrupt.EMPTY_INTERRUPT;
                this.Proxy        = null;
            }

            #endregion
        }

        private void stopBt_Click(object sender, EventArgs e) {
            for ( var i = 0; i < this.proxys.Count; i++ ) {
                this.proxys[i].Shutdown();
                this.proxys[i] = null;
            }

            this.proxys.RemoveAll( x => true );
            for ( int i = 0; i < this.proxyList.Items.Count; i++ ) {
                this.proxyList.Items.RemoveAt( 0 );
            }
        }
    }
}