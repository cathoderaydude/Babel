using Babel.Async;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
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

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            AsyncStatic.MakeTTS(txtOutput.Text, AsyncTTS_PlaySound);
        }

        private void AsyncTTS_PlaySound(IAsyncTTS tts)
        {
            using (MemoryStream ms = new MemoryStream(tts.audioData))
            using (SoundPlayer sp = new SoundPlayer(ms))
            {
                sp.Play();
            }
        }
    }
}
