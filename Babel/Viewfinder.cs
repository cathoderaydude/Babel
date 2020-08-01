using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Babel
{
    public enum State
    {
        ready,
        snapped,
        translating,
        translated,
    }

    public partial class frmViewFinder : Form
    {
        public frmViewFinder()
        {
            InitializeComponent();
        }

        private void Viewfinder_Load(object sender, EventArgs e)
        {
            Text = "Viewfinder - Ready";
        }

        private Image Snap()
        {
            int x = PointToScreen(tscMain.ContentPanel.Location).X;
            int y = PointToScreen(tscMain.ContentPanel.Location).Y;
            int width = pbxDisplay.Size.Width;
            int height = pbxDisplay.Size.Height;

            IntPtr hdcSrc = GDI32.User32.GetWindowDC(GDI32.User32.GetDesktopWindow());
            IntPtr hdcDst = GDI32.CreateCompatibleDC(hdcSrc);
            IntPtr bitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            GDI32.SelectObject(hdcDst, bitmap);

            GDI32.BitBlt(hdcDst, 0, 0, width, height, hdcSrc, x, y, GDI32.TernaryRasterOperations.SRCCOPY);

            return Image.FromHbitmap(bitmap);
        }

        private Image snap = null;
        private Image edit = null;

        private void ChangeState(State newState)
        {
            switch(newState)
            {
                case State.ready:
                    Text = "Viewfinder - Ready";
                    btnSnap.Enabled = true;
                    btnTranslate.Enabled = false;
                    btnRevert.Enabled = false;
                    btnSave.Enabled = false;
                    btnClear.Enabled = false;
                    break;

                case State.snapped:
                    Text = "Viewfinder - Snapped";
                    btnSnap.Enabled = false;
                    btnTranslate.Enabled = true;
                    btnRevert.Enabled = false;
                    btnSave.Enabled = true;
                    btnClear.Enabled = true;
                    break;

                case State.translating:
                    Text = "Viewfinder - Translating...";
                    btnSnap.Enabled = false;
                    btnTranslate.Enabled = false;
                    btnRevert.Enabled = false;
                    btnSave.Enabled = false;
                    btnClear.Enabled = false;
                    break;

                case State.translated:
                    Text = "Viewfinder - Translated";
                    btnSnap.Enabled = false;
                    btnTranslate.Enabled = false;
                    btnRevert.Enabled = true;
                    btnSave.Enabled = true;
                    btnClear.Enabled = true;
                    break;
            }
        }

        private void btnSnap_Click(object sender, EventArgs e)
        {
            pbxDisplay.Image = snap = Snap();

            ChangeState(State.snapped);
        }

        /*private void btnTranslate_Click(object sender, EventArgs e)
        {
            edit = snap.Copy();

            var ocrs = GoogleHandler.TranslateImage(edit);

            if (ocrs.Count() > 0)
            {
                var first = ocrs.First();
                edit.FillRect(first.poly.FitRect(), Color.Black);
                edit.DrawString(
                    first.translatedText,
                    Color.White,
                    first.poly.FitRect());

                foreach (var ocr in ocrs.Skip(1))
                {
                    FillPoly(ocr.poly, Color.Black);
                    graphics.DrawString(
                        ocr.translatedText,
                        SystemFonts.DefaultFont,
                        new SolidBrush(Color.White), ocr.poly.First());
                }
            }

            pbxDisplay.Image = edit;

            ChangeState(State.translated);
        }*/

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            edit = snap.Copy();

            ChangeState(State.translating);

            bgwGoogle.RunWorkerAsync();
        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            edit = null;

            pbxDisplay.Image = snap;

            ChangeState(State.snapped);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (sfdDisplay.ShowDialog() == DialogResult.OK)
                pbxDisplay.Image.Save(sfdDisplay.FileName);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            edit = snap = null;

            pbxDisplay.Image = null;

            ChangeState(State.ready);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void bgwGoogle_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var ocrs = GoogleHandler.TranslateImage(edit);
            stopwatch.Stop();
            string translateTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                stopwatch.Elapsed.Hours,
                stopwatch.Elapsed.Minutes,
                stopwatch.Elapsed.Seconds,
                stopwatch.Elapsed.Milliseconds);
            stopwatch.Reset();

            if (ocrs.Count() > 0)
            {
                var first = ocrs.First();
                edit.FillRect(first.poly.FitRect(), Color.Black);
                edit.DrawString(
                    first.translatedText,
                    Color.White,
                    first.poly.FitRect());

                /*foreach (var ocr in ocrs.Skip(1))
                {
                    FillPoly(ocr.poly, Color.Black);
                    graphics.DrawString(
                        ocr.translatedText,
                        SystemFonts.DefaultFont,
                        new SolidBrush(Color.White), ocr.poly.First());
                }*/
            }
        }

        private void bgwGoogle_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            pbxDisplay.Image = edit;

            ChangeState(State.translated);
        }
    }

    static class BitmapExt
    {
        static Graphics GetGraphics(this Image image) => Graphics.FromImage(image);

        public static Rectangle FitRect(this Point[] poly)
        {
            int minX = poly.Min(p => p.X);
            int maxX = poly.Max(p => p.X);
            int minY = poly.Min(p => p.Y);
            int maxY = poly.Max(p => p.Y);
            int w = maxX - minX;
            int h = maxY - minY;

            return new Rectangle(minX, minY, w, h);
        }

        public static Image LoadFromFile(string filePath)
        {
            // In order to make sure the image is drawable, we copy its pixels to a bitmap
            Image original = Image.FromFile(filePath);
            Image bitmap = new Bitmap(original.Width, original.Height);
            bitmap.GetGraphics().DrawImage(original, 0, 0);

            return Image.FromFile(filePath).Copy();
        }

        public static Image Copy(this Image original)
        {
            Image copy = new Bitmap(original.Width, original.Height);
            copy.GetGraphics().DrawImage(original, 0, 0);
            return copy;
        }

        public static void DrawPoly(this Image image, Point[] poly, Color color)
        {
            image.GetGraphics().DrawPolygon(new Pen(color), poly);
        }

        public static void FillPoly(this Image image, Point[] poly, Color color)
        {
            image.GetGraphics().FillPolygon(new SolidBrush(color), poly);
        }

        public static void DrawRect(this Image image, Rectangle rect, Color color)
        {
            image.GetGraphics().DrawRectangle(new Pen(color), rect);
        }

        public static void FillRect(this Image image, Rectangle rect, Color color)
        {
            image.GetGraphics().FillRectangle(new SolidBrush(color), rect);
        }

        public static void DrawString(this Image image, string text, Color color, Rectangle rect)
        {
            image.GetGraphics().DrawString(text, SystemFonts.DefaultFont, new SolidBrush(color), rect);
        }
    }


}
