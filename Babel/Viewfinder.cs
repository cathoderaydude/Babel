using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Babel
{
    public enum State
    {
        ready,
        snapped,
        OCRing,
        OCRed,
        translating,
        translated,
    }

    public partial class frmViewFinder : Form
    {       
        public List<OCRResult> Rects;
        public bool Marking;
        
        public frmViewFinder()
        {
            InitializeComponent();
        }

        private void Viewfinder_Load(object sender, EventArgs e)
        {
            Text = "Viewfinder - Ready";
            Rects = new List<OCRResult>();
            PhraseRects = new List<PhraseRect>();

            bgwTranslate.RunWorkerAsync();
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

                case State.OCRing:
                    Text = "Viewfinder - Recognizing...";
                    btnSnap.Enabled = false;
                    btnTranslate.Enabled = false;
                    btnRevert.Enabled = false;
                    btnSave.Enabled = false;
                    btnClear.Enabled = false;
                    break;

                case State.OCRed:
                    Text = "Viewfinder - Select text";
                    btnSnap.Enabled = false;
                    btnTranslate.Enabled = false;
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

            edit = snap.Copy();

            ChangeState(State.OCRing);

            bgwOCR.RunWorkerAsync();
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            // This will do translation later
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

        private void bgwTranslate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                bool DidTranslate = false;
                foreach(PhraseRect PRect in PhraseRects)
                {
                    if (PRect.Translated == false)
                    {
                        List<string> st = new List<string>();
                        st.Add(PRect.ToBeTranslated);
                        var result = GoogleHandler.Translate(st);
                        if (result.Count() > 0)
                        {
                            Console.WriteLine("Translated " + PRect.ToBeTranslated + " as " + result.First().text);
                            PRect.TranslatedText = result.First().text;
                        } else
                        {
                            Console.WriteLine("No translation found for: " + PRect.ToBeTranslated);
                        }
                        PRect.Translated = true;
                        //var translations = Translate(sourceStrings, ocrs.First().locale).ToArray();
                        //ocrs = ocrs.Zip(translations, InsertTranslation).ToArray();
                        DidTranslate = true;
                    }
                }

                if (DidTranslate)
                {
                    Console.WriteLine("Nothing to translate, sleeping...");
                } else {
                    Console.WriteLine("Translations done, sleeping...");
                }
                /*Stopwatch stopwatch = new Stopwatch();

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
                //}

                Thread.Sleep(500);
            }
        }

        private void bgwTranslate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            pbxDisplay.Image = edit;

            ChangeState(State.translated);
        }

        private void bgwOCR_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var ocrs = GoogleHandler.RecognizeImage(edit);
            foreach(OCRResult ocr in ocrs)
            {
                Rects.Add(ocr);
            }
        }

        private void bgwOCR_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ChangeState(State.OCRed);
            pbxDisplay.Invalidate();
        }

        private void pbxDisplay_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (OCRResult ocr in Rects)
            {

                g.FillPolygon(new SolidBrush(Color.FromArgb(100, 128, 50, 128)), ocr.poly);
                if (ocr.selected)
                {
                    g.DrawPolygon(new Pen(Color.Pink, 1.0f), ocr.poly);
                }
                else
                {
                    g.DrawPolygon(new Pen(Color.Purple, 1.0f), ocr.poly);
                }
            }

            //g.DrawRectangle(Pens.White, new Rectangle(MouseStart, new Size(100, 100)));
            if (Marking)
            {
                g.DrawLine(Pens.White, MouseStart.X, MouseStart.Y, MouseStart.X, MouseEnd.Y);
                g.DrawLine(Pens.White, MouseStart.X, MouseStart.Y, MouseEnd.X, MouseStart.Y);
                g.DrawLine(Pens.White, MouseEnd.X, MouseEnd.Y, MouseEnd.X, MouseStart.Y);
                g.DrawLine(Pens.White, MouseEnd.X, MouseEnd.Y, MouseStart.X, MouseEnd.Y);
            }

            foreach (PhraseRect PRect in PhraseRects)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(128, 0, 0, 0)), PRect.Location);
                g.DrawRectangle(Pens.Green, PRect.Location);
                g.DrawString(
                        PRect.ToBeTranslated,
                        DefaultFont,
                        Brushes.Gray,
                        PRect.Location);
                g.DrawString(
                        PRect.TranslatedText,
                        DefaultFont,
                        Brushes.White,
                        PRect.Location);
            }
        }

        Point MouseStart;
        Point MouseEnd;

        List<PhraseRect> PhraseRects;

        public class PhraseRect
        {
            public Rectangle Location;
            public string ToBeTranslated;
            public string TranslatedText;
            public bool Translated;
            public PhraseRect(Rectangle Location)
            {
                Translated = false;
                this.Location = Location;
            }
            public PhraseRect(Point p1, Point p2)
            {
                Translated = false;
                this.Location = new Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            }
        }

        List<OCRResult> GetRectsInPhrase(PhraseRect PRect)
        {
            List<OCRResult> Results = new List<OCRResult>();

            foreach (OCRResult ocr in Rects)
            {
                Rectangle rect = ocr.poly.FitRect();
                if (PRect.Location.Contains(rect))
                {
                    Results.Add(ocr);
                }
            }
            return (Results);
        }

        void MarkItems()
        {

            foreach (PhraseRect PRect in PhraseRects)
            {
                List<OCRResult> Results = GetRectsInPhrase(PRect);
                string CombinedPhrase = "";
                foreach (OCRResult Result in Results)
                {
                    CombinedPhrase += Result.text + " ";
                }
                PRect.ToBeTranslated = CombinedPhrase;
            }
        }

        private void pbxDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            MouseStart = e.Location;
            MouseEnd = e.Location;
            //btnActuallyTranslate.Enabled = false;
            pbxDisplay.Invalidate();
            Marking = true;
        }

        private void pbxDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            PhraseRect testrect = new PhraseRect(MouseStart, MouseEnd);
            if (GetRectsInPhrase(testrect).Count > 0)
                PhraseRects.Add(new PhraseRect(MouseStart, MouseEnd));
            MarkItems();
            Marking = false;
            pbxDisplay.Invalidate();
        }

        private void pbxDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (Marking)
            {
                MouseEnd = e.Location;
                pbxDisplay.Invalidate();
            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tmrTranslate_Tick(object sender, EventArgs e)
        {
            foreach(PhraseRect PRect in PhraseRects)
            {
                if (PRect.Translated == false)
                {

                }
            }
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
