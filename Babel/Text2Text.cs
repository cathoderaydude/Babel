using Babel.Google;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Babel
{
    public partial class Text2Text : Form
    {
        public Text2Text()
        {
            InitializeComponent();
        }

        bool dirty = false;

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            dirty = true;
        }

        // This has no mechanism for ensuring that the callbacks return in order.
        // I hope to never care about it that much.
        Queue<string> ts = new Queue<string>();

        private void Translation_callback(AsyncTranslation tr)
        {
            ts.Enqueue(tr.translatedText);
            Invalidate();
        }

        private void tRefresh_Tick(object sender, EventArgs e)
        {
            if (dirty)
            {
                new AsyncTranslation(txtInput.Text, Translation_callback);
                dirty = false;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tRefresh.Enabled = !tsbPause.Checked;
            dirty = true;
        }

        private void Text2Text_Paint(object sender, PaintEventArgs e)
        {
            if (ts.Count > 0)
                txtOutput.Text = ts.Dequeue();

            if (ts.Count > 0)
                Invalidate();
        }
    }
}
