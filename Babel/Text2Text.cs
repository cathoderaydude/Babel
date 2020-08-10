using Babel.Async;
//using Babel.Async.GoogleImpl;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Babel
{
    public partial class Text2Text : Form
    {
        public frmBabel BabelForm;

        public Text2Text()
        {
            InitializeComponent();
        }

        bool dirty = false;

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            dirty = true;
        }

        Queue<IAsyncTranslation> ts = new Queue<IAsyncTranslation>();

        private void Translation_callback(IAsyncTranslation tr)
        {
            Invalidate();
        }

        private void tRefresh_Tick(object sender, EventArgs e)
        {
            if (dirty)
            {
                ts.Enqueue(AsyncStatic.MakeTranslation(txtInput.Text, Translation_callback));
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
            while ((ts.Count > 0) && ts.Peek().isDone)
                txtOutput.Text = ts.Dequeue().translatedText;

            if (ts.Count > 0)
                Invalidate();
        }
    }
}
