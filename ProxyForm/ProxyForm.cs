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
        Start                        start = new Start();
        ProxyClass                   proxy = new ProxyClass();
        private ProxyClass.interrupt cInterrupt;
        private ProxyClass.interrupt sInterrupt;
        List<Query.Paket>            cPakets = new List<Query.Paket>();
        List<Query.Paket>            sPakets = new List<Query.Paket>();

        public bool interuptS = false, interuptC = false;

        public bool CSubscribe(Query.Paket p) {
            this.cPakets.Add( p );

            var res = "[⏩] " + p.sender + ": " + string.Concat( p._buffer.Select( b => b.ToString( "X2" ) ).ToArray() );

            this.Invoke( new Action( () => ( this.interuptC ? this.cliB : this.clie ).Items.Add( create( this.cPakets.Count - 1, res ) ) ) );
            //this.Invoke( new Action( () => this.clie.AppendText( res.Substring( 0, res.Length < 30 ? res.Length : 30 ) + "\n" ) ) );

            return !this.interuptC;
        }

        public bool SSubscribe(Query.Paket p) {
            this.sPakets.Add( p );

            var res = "[⏪] " + p.sender + ": " + string.Concat( p._buffer.Select( b => b.ToString( "X2" ) ).ToArray() );

            this.Invoke( new Action( () => ( this.interuptS ? this.serB : this.serv ).Items.Add( create( this.sPakets.Count - 1, res ) ) ) );

            //this.Invoke( new Action( () => this.serv.AppendText( res.Substring( 0, res.Length < 30 ? res.Length : 30 ) + "\n" ) ) );

            return !this.interuptS;
        }

        ListViewItem create(int id, string data) { return new ListViewItem( new string[] { id.ToString(), data.Substring( 0, data.Length < 30 ? data.Length : 30 ) } ); }

        public ProxyForm() {
            cInterrupt = new ProxyClass.interrupt( CSubscribe );
            sInterrupt = new ProxyClass.interrupt( SSubscribe );

            InitializeComponent();

            this.proxy.StartForwarding( this.cInterrupt, this.sInterrupt );
        }

        private void startBtw_Click(object sender, EventArgs e) {
            if ( this.start.ShowDialog( this ) == DialogResult.OK ) this.proxy.StartProxy( this.start.getPort(), IPAddress.Parse( this.start.getIp() ) );
        }

        private void ProxyForm_FormClosed(object sender, FormClosedEventArgs e) { Environment.Exit( 0 ); }

        private void ProxyForm_Load(object sender, EventArgs e) {
            this.columnHeader3.Width = -2;
            this.columnHeader4.Width = -2;
            this.columnHeader6.Width = -2;
            this.columnHeader8.Width = -2;

            this.proxy.StartProxy( 9900, IPAddress.Parse( "127.0.0.1" ) );
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

        private void InterruptPaketS_CheckedChanged(object sender, EventArgs e) { this.interuptS = this.InterruptPaketS.Checked; }

        private void InterruptPaketC_CheckedChanged(object sender, EventArgs e) { this.interuptC = this.InterruptPaketC.Checked; }

        private void sendInteruptC_Click(object sender, EventArgs e) {
            while ( Query.BlockedReservedPaketQueue.Count > 0 ) {
                try {
                    var p = Query.BlockedReservedPaketQueue.Dequeue();
                    this.proxy.SendPaket( p, Query.SendingClassifiedSockets );
                    this.cliB.Items.RemoveAt( 0 );
                } catch (Exception ex) { Console.WriteLine( ex.Message ); }
            }
            this.cliB.Items.Clear();
        }


        private void sendInteruptS_Click(object sender, EventArgs e) {
            while ( Query.BlockedSendingPaketQueue.Count > 0 ) {
                try {
                    var p = Query.BlockedSendingPaketQueue.Dequeue();
                    this.proxy.SendPaket( p, Query.ReservingClassifiedSockets );

                    this.serB.Items.RemoveAt( 0 );
                } catch (Exception ex) { Console.WriteLine( ex.Message ); }
            }
            this.serB.Items.Clear();
        }
    }
}