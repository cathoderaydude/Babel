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

        Queue<string> ts = new Queue<string>();

        private void Translation_callback(string s)
        {
            ts.Enqueue(s);
        }

        private void tRefresh_Tick(object sender, EventArgs e)
        {
            // Manage inputs
            if (dirty)
            {
                var translation = new AsyncTranslation(txtInput.Text);
                translation.callback += Translation_callback;
                dirty = false;
            }

            // Manage outputs
            if (ts.Count > 0)
                txtOutput.Text = ts.Dequeue();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tRefresh.Enabled = !tsbPause.Checked;
            dirty = true;
        }
    }
}
