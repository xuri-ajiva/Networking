using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace ProxyForm {
    public partial class PaketEditor : Form {
        private Button     button1;
        private Button     button2;
        private ByteViewer byteviewer;

        public PaketEditor(Proxy.Query.Paket p) {
            InitializeComponent();

            this.byteviewer          = new ByteViewer();
            this.byteviewer.Location = new Point( 8, 46 );
            this.byteviewer.Size     = new Size( 600, 338 );
            this.byteviewer.Anchor   = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.Controls.Add( this.byteviewer );

            this.textBox1.Text = p.sender.ToString();
            this.textBox2.Text = p.port.ToString();
            this.byteviewer.SetBytes( p._buffer );
        }

        private void loadBytesFromFile(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if ( ofd.ShowDialog() != DialogResult.OK ) return;

            this.byteviewer.SetFile( ofd.FileName );
        }

        private void clearBytes(object sender, EventArgs e) { this.byteviewer.SetBytes( new byte[] { } ); }

        private void changeByteMode(object sender, EventArgs e) {
            RadioButton rbutton = (RadioButton) sender;

            DisplayMode mode;
            switch (rbutton.Text) {
                case "ANSI":
                    mode = DisplayMode.Ansi;
                    break;
                case "Hex":
                    mode = DisplayMode.Hexdump;
                    break;
                case "Unicode":
                    mode = DisplayMode.Unicode;
                    break;
                default:
                    mode = DisplayMode.Auto;
                    break;
            }

            // Sets the display mode.
            this.byteviewer.SetDisplayMode( mode );
        }
    }
}