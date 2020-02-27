using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Proxy;

namespace ProxyForm {
    public partial class ProxyForm : Form {
        Start                       _start  = new Start();
        readonly List<ProxyImplementation>   _proxys = new List<ProxyImplementation>();

        List<Query.Paket> cPakets = new List<Query.Paket>();
        List<Query.Paket> sPakets = new List<Query.Paket>();


        public void CSubscribe(Query.Paket p, ProxyImplementation pi) {
            if ( p * Query.Paket.Empty ) return;
            this.cPakets.Add( p );

            var res = "[⏩] " + p.sender + ": " + string.Concat( p._buffer.Select( b => b.ToString( "X2" ) ).ToArray() );

            Invoke( new Action( () => ( pi.interuptC ? this.cliB : this.clie ).Items.Add( Create( this.cPakets.Count - 1, res ) ) ) );
            //this.Invoke( new Action( () => this.clie.AppendText( res.Substring( 0, res.Length < 30 ? res.Length : 30 ) + "\n" ) ) );
        }

        public void SSubscribe(Query.Paket p, ProxyImplementation pi) {
            if ( p * Query.Paket.Empty ) return;

            this.sPakets.Add( p );

            var res = "[⏪] " + p.sender + ": " + string.Concat( p._buffer.Select( b => b.ToString( "X2" ) ).ToArray() );

            Invoke( new Action( () => ( pi.interuptS ? this.serB : this.serv ).Items.Add( Create( this.sPakets.Count - 1, res ) ) ) );

            //this.Invoke( new Action( () => this.serv.AppendText( res.Substring( 0, res.Length < 30 ? res.Length : 30 ) + "\n" ) ) );
        }

        private static ListViewItem Create(int id, string data) { return new ListViewItem( new string[] { id.ToString(), data.Substring( 0, data.Length < 30 ? data.Length : 30 ) } ); }

        public ProxyForm() {
            InitializeComponent();
        }

        private void ProxyForm_Load(object sender, EventArgs e) {
            Sm( this, null );

            //var p  = new ProxyClass( 9899, 9900, IPAddress.Parse( "127.0.0.1" ) );
            //var pv = new ProxyImplementation( p );
            //pv.triggerCSubscribe += this.CSubscribe;
            //pv.triggerSSubscribe += this.SSubscribe;
            //pv.StartForwarding();
            //
            //pv.Proxy.StartProxy();
            //add( pv );
        }

        private void ProxyForm_FormClosed(object sender, FormClosedEventArgs e) { Environment.Exit( 0 ); }

        void BindAll(ProxyImplementation pi) {
            pi.triggerCSubscribe -= CSubscribe;
            pi.triggerSSubscribe -= SSubscribe;
            pi.triggerCSubscribe += CSubscribe;
            pi.triggerSSubscribe += SSubscribe;
        }

        void add(ProxyImplementation pv) {
            this.proxyList.Items.Add( new ListViewItem( new string[] { this._proxys.Count.ToString(), pv.Proxy.realServer.ToString(), pv.Proxy.portC.ToString(), pv.Proxy.portR.ToString() } ) );
            BindAll( pv );
            this._proxys.Add( pv );
        }

        void StartNew(Start s) {
            var p  = new ProxyClass( s.getPortC(), s.getPortL(), IPAddress.Parse( s.getIp() ) );
            var pv = new ProxyImplementation( p );
            pv.StartForwarding();

            p.StartProxy();
            add( pv );
        }

        private ProxyImplementation GetFromList() {
            try {
                if ( this.proxyList.Items.Count == 0 || this.proxyList.SelectedItems.Count == 0 ) return null;
                var n = int.Parse( this.proxyList.SelectedItems[0].SubItems[0].Text );
                return this._proxys[n];
            } catch (Exception e) {
                return null;
            }
        }

        private ProxyImplementation current;

        private void startBtw_Click(object sender, EventArgs e) {
            var s = new Start();
            if ( s.ShowDialog( this ) == DialogResult.OK ) {
                StartNew( s );
            }
        }

        private void stopBt_Click(object sender, EventArgs e) {
            for ( var i = 0; i < this._proxys.Count; i++ ) {
                this._proxys[i].Shutdown();
                this._proxys[i] = null;
            }

            this._proxys.RemoveAll( x => true );
            for ( int i = 0; i < this.proxyList.Items.Count; i++ ) {
                this.proxyList.Items.RemoveAt( 0 );
            }
        }

        private bool CheckboxChangeAllowed() {
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
            if ( CheckboxChangeAllowed() && this.InterruptPaketS.Checked ) {
                this.current.interuptS = true;
                return;
            }

            if ( this.InterruptPaketS.Checked ) {
                this.InterruptPaketS.Checked = false;
            }

            if ( this.current != null ) this.current.interuptS = false;
        }

        private void InterruptPaketC_CheckedChanged(object sender, EventArgs e) {
            if ( CheckboxChangeAllowed() && this.InterruptPaketC.Checked ) {
                this.current.interuptC = true;
                return;
            }

            if ( this.InterruptPaketC.Checked ) {
                this.InterruptPaketC.Checked = false;
            }

            if ( this.current != null ) this.current.interuptC = false;
        }

        private void sendInterruptS_Click(object sender, EventArgs e) {
            while ( Query.BlockedSendingPaketQueue.Count > 0 ) {
                try {
                    var p = Query.BlockedSendingPaketQueue.Dequeue();
                    this.current.Proxy.SendPaket( p, Query.ReservingClassifiedSockets );

                    this.serB.Items.RemoveAt( 0 );
                } catch (Exception ex) { Console.WriteLine( ex.Message ); }
            }

            this.serB.Items.Clear();
        }

        private void sendInterruptC_Click(object sender, EventArgs e) {
            while ( Query.BlockedReservedPaketQueue.Count > 0 ) {
                try {
                    var p = Query.BlockedReservedPaketQueue.Dequeue();
                    this.current.Proxy.SendPaket( p, Query.SendingClassifiedSockets );
                    this.cliB.Items.RemoveAt( 0 );
                } catch (Exception ex) { Console.WriteLine( ex.Message ); }
            }

            this.cliB.Items.Clear();
        }

        private void Sm(object sender, SplitterEventArgs e) {
            this.columnHeader3.Width  = -2;
            this.columnHeader4.Width  = -2;
            this.columnHeader6.Width  = -2;
            this.columnHeader8.Width  = -2;
            this.columnHeader10.Width = IPAddress.Broadcast.ToString().Length * 7;
            this.columnHeader11.Width = short.MaxValue.ToString().Length      * 7;
            this.columnHeader12.Width = -2;
        }

        public class ProxyImplementation : IDisposable {
            private ProxyClass.Interrupt cInterrupt;
            private ProxyClass.Interrupt sInterrupt;

            public event Action<Query.Paket, ProxyImplementation> triggerCSubscribe;
            public event Action<Query.Paket, ProxyImplementation> triggerSSubscribe;
            public ProxyClass                                     Proxy;

            public bool interuptS = false, interuptC = false;

            public ProxyImplementation(ProxyClass pc) {
                this.Proxy = pc;

                this.cInterrupt = new ProxyClass.Interrupt( CSubscribe );
                this.sInterrupt = new ProxyClass.Interrupt( SSubscribe );
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
                this.cInterrupt   = ProxyClass.Interrupt.EMPTY_INTERRUPT;
                this.sInterrupt   = ProxyClass.Interrupt.EMPTY_INTERRUPT;
                this.Proxy        = null;
            }

            #endregion
        }

        private void proxyList_SelectedIndexChanged(object sender, EventArgs e) {
            var p = GetFromList();
            if ( p != null ) {
                this.current = p;

                this.stopSelectedBtw.Enabled = true;

                this.InterruptPaketC.Enabled = true;
                this.InterruptPaketC.Enabled = true;
            }
            else {
                this.stopSelectedBtw.Enabled = false;

                this.InterruptPaketC.Enabled = false;
                this.InterruptPaketC.Enabled = false;
            }
        }

        private void DoubleClickP(object sender, EventArgs e) {
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

        private void stopSelectedBtw_Click(object sender, EventArgs e) {
            var p = GetFromList();
            if ( p == null ) return;
            this.current = p;
            var n = int.Parse( this.proxyList.SelectedItems[0].SubItems[0].Text );
            this._proxys[n].Shutdown();
            this._proxys[n] = null;
            this.proxyList.Items.RemoveAt( n );
            this._proxys.RemoveAt( n );
        }
    }
}