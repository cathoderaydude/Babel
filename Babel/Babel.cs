﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Babel.Grabbing;
using Babel.Google;
using System.Runtime.InteropServices;
using System.Text;

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


    public partial class frmBabel : Form
    {
        public List<PhraseRect> PhraseRects; // Track user-selected phrases
        public List<OCRResult> OCRResults; // Track identified words
        
        // For bounding box code
        public bool Marking;
        Point MouseStart;
        Point MouseEnd;
        public bool StartingDrag;
        public bool Dragging;
        public PhraseRect DrugPhrase;
        public bool CtrlDown;

        // Image buffers
        private Image snap = null; // Exact image captured from screenshot
        private Image edit = null; // Modified image

        Viewfinder vfw; // Persistent viewfinder window

        public frmBabel()
        {
            InitializeComponent();
        }

        // Contains a rectangle the user has drawn around one or more words to be translated as a single phrase
        public class PhraseRect
        {
            public Rectangle Location;
            public string RawText;
            public string TranslatedText;
            public bool Translated;
            public string translationTime;
            public bool Hovered;
            public bool Clicked;
            public bool Selected;

            public PhraseRect(Rectangle Location)
            {
                Translated = false;
                this.Location = Location;
            }

            private Rectangle Points2Rect(Point p1, Point p2) => new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y));

            public PhraseRect(Point p1, Point p2)
            {
                Translated = false;
                this.Location = Points2Rect(p1, p2);
            }
        }

        private void Viewfinder_Load(object sender, EventArgs e)
        {
            OCRResults = new List<OCRResult>();
            PhraseRects = new List<PhraseRect>();

            bgwTranslate.RunWorkerAsync();

            Text = "Viewfinder - Ready";
            ChangeState(State.ready);

            vfw = new Viewfinder();
            vfw.Show(); // For debugging
        }

        // Takes a screenshot of what's behind the window and returns it
        private Image Snap()
        {
            int x = vfw.RectangleToScreen(vfw.ClientRectangle).X;
            int y = vfw.RectangleToScreen(vfw.ClientRectangle).Y;
            int width = vfw.Size.Width;
            int height = vfw.Size.Height;

            WindowFunctions.HideAllTooltips(); // Hide all open tooltips since the user might be hovering over a toolbar button

            IntPtr hdcSrc = GDI32.User32.GetWindowDC(GDI32.User32.GetDesktopWindow());
            IntPtr hdcDst = GDI32.CreateCompatibleDC(hdcSrc);
            IntPtr bitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            GDI32.SelectObject(hdcDst, bitmap);

            vfw.Visible = false;
            GDI32.BitBlt(hdcDst, 0, 0, width, height, hdcSrc, x, y, GDI32.TernaryRasterOperations.SRCCOPY);
            vfw.Visible = true;

            return Image.FromHbitmap(bitmap);
        }

        State AppState;
        // Keeps all our buttons enabled/disabled as needed
        private void ChangeState(State newState)
        {
            AppState = newState;
            switch(newState)
            {
                case State.ready:
                    Text = "Babel - Ready";
                    tsbRevert.Enabled = false;
                    tsbSave.Enabled = false;
                    tsbOCR.Enabled = false;
                    break;

                case State.snapped:
                    Text = "Babel - Captured";
                    if (!tsbAutoOCR.Checked) tsbOCR.Enabled = true;
                    break;

                case State.OCRing:
                    Text = "Babel - Recognizing...";
                    tsbRevert.Enabled = false;
                    tsbSave.Enabled = false;
                    tsbOCR.Enabled = false;
                    break;

                case State.OCRed:
                    Text = "Babel - Select text";
                    tsbRevert.Enabled = false;
                    tsbSave.Enabled = true;
                    tsbOCR.Enabled = false;
                    break;

                case State.translating:
                    Text = "Babel - Translating...";
                    tsbRevert.Enabled = false;
                    tsbSave.Enabled = false;
                    break;

                case State.translated:
                    Text = "Babel - Translated";
                    tsbRevert.Enabled = true;
                    tsbSave.Enabled = true;
                    break;
            }
        }

        //====== Button events

        // All ingest methods call this with an image to take a new snap
        private void GetSnap(Image image)
        {
            ClearAll();

            pbxDisplay.Image = edit = snap = image.Copy();
            ChangeState(State.snapped);
            DoOCR(true);
        }

        // Trigger OCR recognition if appropriate
        bool DoOCR(bool Auto = false)
        {
            if (AppState != State.snapped) return false; // Prevent double OCR

            // Proceed if it's a manual request, or if it's an auto request and autoOCR is on
            if (!Auto || Properties.Settings.Default.autoOCR)
            {
                ChangeState(State.OCRing);
                bgwOCR.RunWorkerAsync(); // OCR the image to find out where words are
                return true;
            } else
            {
                return false;
            }
        }

        // Ingest image from viewfinder
        private void btnSnap_Click(object sender, EventArgs e)
        {
            if (!vfw.Visible) // If the viewfinder isn't visible, show it and stop
            {
                vfw.Show();
            }
            else
            {
                GetSnap(Snap());
            }
        }

        // Ingest image from clipboard
        private void tsbClipboard_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                GetSnap(Clipboard.GetImage());
            }
        }

        private void tsbOCR_Click(object sender, EventArgs e)
        {
            DoOCR();
        }

        private void tsbAutoOCR_CheckedChanged(object sender, EventArgs e)
        {
            if(tsbAutoOCR.Checked)
            {
                tsbOCR.Enabled = false;
                Properties.Settings.Default.autoOCR = false;
                if (AppState == State.snapped) DoOCR(true);
            } else
            {
                Properties.Settings.Default.autoOCR = true;
                tsbOCR.Enabled = true;
            }
            Properties.Settings.Default.Save();
        }

        // Called whenever settings should be reevaluated
        void LoadSettings()
        {
            Properties.Settings.Default.Reload();
            tsbOCR.Enabled = !Properties.Settings.Default.autoOCR;
            tsbAutoOCR.Checked = Properties.Settings.Default.autoOCR;

        }

        // Clear all identified phrases
        private void btnRevert_Click(object sender, EventArgs e)
        {
            ClearPhrases();
            ChangeState(State.OCRed);
        }

        void ClearPhrases()
        {
            PhraseRects.Clear();
            pbxDisplay.Invalidate();
        }

        // Clear everything to prep for another snap
        void ClearAll()
        {
            OCRResults.Clear();
            PhraseRects.Clear();
            pbxDisplay.Invalidate();
        }

        // Save image to disk
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (sfdDisplay.ShowDialog() == DialogResult.OK)
            {
                // Copy the current snap, run the graphics draw routine on it and save it
                Image tempImage = snap.Copy();
                Graphics g = Graphics.FromImage(tempImage);
                DrawImage(g);
                tempImage.Save(sfdDisplay.FileName);
            }
        }
        // Save unmodified screenshot to disk
        private void tsbSaveRaw_Click(object sender, EventArgs e)
        {
            if (sfdDisplay.ShowDialog() == DialogResult.OK)
            {
                // Copy the current snap and save it as-is
                snap.Save(sfdDisplay.FileName);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog(ActiveForm); // Open as modal so CenterParent will work
            LoadSettings();
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //===== Functions to perform translation of any phrases that have been selected but not yet translated
        private void bgwTranslate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true) // Loop forever
            {
                bool DidTranslate = false;
                try
                {
                    // Walk over all user-selected phrases that aren't yet translated
                    foreach (PhraseRect PRect in PhraseRects.Where(x => x.Translated == false))
                    {
                        // This is necessary to fit the input that the function requires
                        List<string> st = new List<string>();
                        st.Add(PRect.RawText);

                        // Translate the phrase
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        IEnumerable<TranslationResult> result;
                        if (!Properties.Settings.Default.dummyData) // Dummy out Google calls during testing
                        {
                            result = GoogleHandler.Translate(st);
                        } else
                        {
                            List<TranslationResult> DummyData = new List<TranslationResult>();
                            for(int x=0;x<10;x++)
                            {
                                DummyData.Add(new TranslationResult(String.Join(" ", st), "Nambia"));
                            }
                            result = DummyData;
                        }
                        stopwatch.Stop();


                        if (result.Count() > 0)
                        {
                            Console.WriteLine("Translated " + PRect.RawText + " as " + result.First().text);
                            PRect.TranslatedText = result.First().text;
                            PRect.translationTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                                stopwatch.Elapsed.Hours,
                                stopwatch.Elapsed.Minutes,
                                stopwatch.Elapsed.Seconds,
                                stopwatch.Elapsed.Milliseconds);
                        }
                        else
                        {
                            Console.WriteLine("No translation found for: " + PRect.RawText);
                        }
                        PRect.Translated = true; // Mark the phrase as translated (even if it failed, so we don't retry)
                        DidTranslate = true;

                    }

                    if (DidTranslate)
                    {
                        pbxDisplay.Invalidate(); // Redraw the output
                        Console.WriteLine("Translations done, sleeping...");
                    }
                } catch (InvalidOperationException eIOE)
                {
                    eIOE.ToString();
                    // This should resolve all cases where the user deletes or adds a phrase while translation is in progress
                    Console.WriteLine("Phrase set changed, sleeping...");
                }

                Thread.Sleep(500); // Wait for the user to make further selections
            }
        }

        private void bgwTranslate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            pbxDisplay.Image = edit;

            ChangeState(State.translated);
        }


        //====== Functions to identify which words are in the image

        private void bgwOCR_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            IEnumerable<OCRResult> ocrs;
            if (!Properties.Settings.Default.dummyData) // Dummy out data for testing
            {
                ocrs = GoogleHandler.RecognizeImage(edit.Copy()); // Actually do the API call to Google
            } else
            {
                // Make some junk data to return
                List<OCRResult> DummyOCRs = new List<OCRResult>();
                Random r = new Random();
                for (int x=0;x<5;x++)
                {
                    int xoff = r.Next(10, 40);
                    int yoff = r.Next(10, 40);
                    Point[] PointSet = new Point[] {
                        new Point(160 + xoff, 100 + yoff + (60 * x)),
                        new Point(100 + xoff, 100 + yoff + (60 * x)),
                        new Point(100 + xoff, 140 + yoff + (60 * x)),
                        new Point(160 + xoff, 140 + yoff + (60 * x))
                    };
                    DummyOCRs.Add(new OCRResult("Test Text", "BS", PointSet));
                }
                ocrs = DummyOCRs;
            }

            foreach(OCRResult ocr in ocrs)
            {
                OCRResults.Add(ocr);
            }
        }

        private void bgwOCR_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ChangeState(State.OCRed);
            pbxDisplay.Invalidate();
        }

        //======= Keyboard events

        // Handle keyboard input
        private void frmViewFinder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Delete any selected phrase
                PhraseRects.RemoveAll(t => t.Selected == true);
            } else if (e.KeyCode == Keys.ControlKey)
            {
                CtrlDown = true; // For drawing overlapping bounding boxes
            } else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                // Paste in an image
                if (Clipboard.ContainsImage())
                {
                    GetSnap(Clipboard.GetImage());
                }
            }
        }

        private void frmViewFinder_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                CtrlDown = false; // For drawing overlapping bounding boxes
            }
        }



        //======= Graphics events

        // Draw all the graphics on top of the image
        // This is generalized so it can be used either by onpaint or by the image save routine
        private void DrawImage(Graphics g)
        {
            // Draw identified words
            foreach (OCRResult ocr in OCRResults)
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

            // Draw user bounding box
            if (Marking)
            {
                switch (BoundingBoxState)
                {
                    case BoundingState.Normal:
                        g.DrawRectangle(Pens.White, BitmapExt.FitRect(MouseStart, MouseEnd));
                        break;
                    case BoundingState.RectsFound:
                        g.DrawRectangle(Pens.Green, BitmapExt.FitRect(MouseStart, MouseEnd));
                        break;
                    case BoundingState.TooSmall:
                        g.DrawRectangle(Pens.Red, BitmapExt.FitRect(MouseStart, MouseEnd));
                        break;
                }
            }


            // Draw phrases
            foreach (PhraseRect PRect in PhraseRects)
            {
                //Rectangle DisplayRect = QuantizeRect(PRect.Location, 8, 8); // This was quantizing the display location of the box, but this is a bad idea as it turns out and may not ever be practical

                g.FillRectangle(new SolidBrush(Color.FromArgb(230, 0, 0, 0)), PRect.Location); // Background

                // Pick color for outline
                Pen BoxColor = Pens.Green;
                // The order of these statements is critical, leave them as-is
                if (PRect.Hovered) BoxColor = Pens.LightGreen;
                if (PRect.Selected) BoxColor = Pens.LightBlue;
                if (PRect.Clicked) BoxColor = Pens.DarkBlue;
                g.DrawRectangle(BoxColor, PRect.Location); // Draw outline

                if (!PRect.Translated)
                {
                    // Draw untranslated text
                    g.DrawString(
                            PRect.RawText,
                            DefaultFont,
                            Brushes.Gray,
                            PRect.Location);
                }
                else
                {
                    // Draw translated text

                    // Fit font to bounding box
                    Font LargeFont = GetAdjustedFont(g, PRect.TranslatedText, DefaultFont, PRect.Location, 32, 6, true);

                    // Center-justify text
                    // TODO: Currently disabled to enable wordwrap, fix this
                    int JustifySpace = (int)(PRect.Location.Width - g.MeasureString(PRect.TranslatedText, LargeFont).Width) / 2;
                    Rectangle AdjustedPosition = new Rectangle(
                        PRect.Location.Left + JustifySpace, PRect.Location.Top, 
                        PRect.Location.Width, PRect.Location.Height);

                    // Draw translated text
                    g.DrawString(
                            PRect.TranslatedText,
                            LargeFont,
                            Brushes.White,
                            PRect.Location);
                }


                if (Properties.Settings.Default.displayTimes)
                {
                    // Draw the translation time
                    Font BoldFont = new Font(DefaultFont, FontStyle.Bold);
                    SizeF TimeLength = g.MeasureString(PRect.translationTime, BoldFont);

                    g.DrawString(PRect.translationTime,
                        BoldFont,
                        Brushes.Gray,
                        new Point(PRect.Location.Right - (int)TimeLength.Width, PRect.Location.Bottom - (int)TimeLength.Height));
                }
            }
        }

        private void pbxDisplay_Paint(object sender, PaintEventArgs e)
        {
            // Call the image drawing routine against the canvas
            Graphics g = e.Graphics;
            DrawImage(g);
        }

        // Begin drawing or dragging bounding box
        private void pbxDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            PhraseRect PRect = GetPhraseAtPoint(e.Location);
            if (PRect != null && !CtrlDown)
            {
                // There was a phrase under the mouse, so start a drag/select rather than a bounding box
                foreach (PhraseRect TPRect in PhraseRects) { TPRect.Clicked = false; TPRect.Selected = false; } // Clear phrase states
                PRect.Clicked = true;
                PRect.Selected = false;
                MouseStart = e.Location;
                StartingDrag = true;
                DrugPhrase = PRect;
            }
            else
            {
                // There was no phrase under the mouse, start a bounding box
                MouseStart = e.Location;
                MouseEnd = e.Location;
                Marking = true;
                BoundingBoxState = BoundingState.TooSmall;
            }
            pbxDisplay.Invalidate();
        }

        Rectangle GetOCRSetExtents(List<OCRResult> Rects)
        {
            Rectangle Result = new Rectangle();

            Result.X = Rects.Min(x => x.poly.Min(y => y.X));
            Result.Y = Rects.Min(x => x.poly.Min(y => y.Y));
            Result.Width = Result.X - Rects.Max(x => x.poly.Max(y => y.X));
            Result.Height = Result.Y - Rects.Max(x => x.poly.Max(y => y.Y));

            return Result;
        }

        // Finish drawing/dragging
        private void pbxDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (Marking == true) // We were drawing a bounding box
            {
                PhraseRect testrect = new PhraseRect(MouseStart, MouseEnd);
                if (testrect.Location.Width > 25 && testrect.Location.Height > 15)
                {
                    ChangeState(State.translated);
                    PhraseRects.Add(new PhraseRect(MouseStart, MouseEnd));
                }
               
                MarkItems();
                Marking = false;
            } else { // We were selecting/dragging
                PhraseRect PRect = GetPhraseAtPoint(e.Location);
                if (PRect != null )
                {
                    if (PRect.Clicked == true)
                    {
                        foreach (PhraseRect TPRect in PhraseRects) { TPRect.Clicked = false; TPRect.Selected = false; } // Clear other phrase states
                        PRect.Clicked = false;
                        PRect.Selected = true;
                    }
                } else
                {
                    foreach (PhraseRect TPRect in PhraseRects) { TPRect.Clicked = false; TPRect.Selected = false; } // Clear other phrase states
                }
                MarkItems();
            }
            StartingDrag = false;
            Dragging = false;
            pbxDisplay.Invalidate();
        }

        BoundingState BoundingBoxState;
        enum BoundingState { 
            Normal,
            RectsFound,
            TooSmall
        }

        private void pbxDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (Marking) // We're drawing a bounding box, set the second endpoint
            {
                MouseEnd = e.Location;

                // Find out if any words were selected and, if so, create a phrase box around them
                PhraseRect testrect = new PhraseRect(MouseStart, MouseEnd);
                List<OCRResult> FoundRects = GetWordsInRect(testrect);
                if (testrect.Location.Width < 25 || testrect.Location.Height < 15)
                {
                    BoundingBoxState = BoundingState.TooSmall;
                } else if (FoundRects.Count > 0)
                {
                    BoundingBoxState = BoundingState.RectsFound;
                } else
                {
                    BoundingBoxState = BoundingState.Normal;
                }

                pbxDisplay.Invalidate();
            } else if (StartingDrag) { // Delay a few pixels to make sure a drag is really happening
                if (GetPointDiff(MouseStart, e.Location) > 5) { StartingDrag = false; Dragging = true; }
            } else if (Dragging) { // We're dragging, move the selected bounding box
                int diffX = MouseStart.X - e.X;
                int diffY = MouseStart.Y - e.Y;
                DrugPhrase.Location.X -= diffX;
                DrugPhrase.Location.Y -= diffY;
                MouseStart = e.Location;
                pbxDisplay.Invalidate();
            } else { // Just highlight whatever box the mouse is over
                foreach(PhraseRect TPRect in PhraseRects) { TPRect.Hovered = false; } // Clear all phrase hover states
                PhraseRect PRect = GetPhraseAtPoint(e.Location); // Check if we're over a phrase
                if (PRect != null)
                {
                    PRect.Hovered = true; // Mark it as hovered
                }
                pbxDisplay.Invalidate();
            }
        }

        //========== Helper functions
        // Find all words in a given rectangle
        List<OCRResult> GetWordsInRect(Rectangle PRect)
        {
            List<OCRResult> Results = new List<OCRResult>();

            foreach (OCRResult ocr in OCRResults)
            {
                Rectangle rect = ocr.poly.FitRect();
                if (PRect.IntersectsWith(rect))
                {
                    Results.Add(ocr);
                }
            }
            return (Results);
        }
        List<OCRResult> GetWordsInRect(PhraseRect PRect)
        {
            return GetWordsInRect(PRect.Location);
        }

        // Find a phrase at a given point, for mouse collision etc.
        PhraseRect GetPhraseAtPoint(Point Location)
        {
            foreach(PhraseRect PRect in PhraseRects)
            {
                if (PRect.Location.Contains(Location)) return PRect;
            }
            return null;
        }

        // Find the words that are under each selected phrase, concatenate them, and stuff them in the phrase.
        void MarkItems()
        {

            foreach (PhraseRect PRect in PhraseRects)
            {
                List<OCRResult> Results = GetWordsInRect(PRect);
                string CombinedPhrase = "";
                foreach (OCRResult Result in Results)
                {
                    CombinedPhrase += Result.text + " ";
                }
                PRect.RawText = CombinedPhrase;
                PRect.TranslatedText = "";
                PRect.Translated = false;
            }
        }

        // Find the biggest font to fit a given rect
        public Font GetAdjustedFont(Graphics g, string graphicString, Font originalFont, Rectangle Container, int maxFontSize, int minFontSize, bool smallestOnFail)
        {
            Font testFont = null;
            // We utilize MeasureString which we get via a control instance           
            for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
            {
                testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);

                // Test the string with the new size
                SizeF adjustedSizeNew = g.MeasureString(graphicString, testFont, Container.Width);

                if (Container.Width > Convert.ToInt32(adjustedSizeNew.Width) &&
                    Container.Height > Convert.ToInt32(adjustedSizeNew.Height)
                    )
                {
                    // Good font, return it
                    return testFont;
                }
            }

            // If you get here there was no fontsize that worked
            // return minimumSize or original?
            if (smallestOnFail)
            {
                return testFont;
            }
            else
            {
                return originalFont;
            }
        }

        public Font WrapText(Graphics g, string graphicString, Font originalFont, Rectangle Container, int maxFontSize, int minFontSize, bool smallestOnFail)
        {
            Font testFont = null;
            // We utilize MeasureString which we get via a control instance           
            for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
            {
                testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);

                // Test the string with the new size
                SizeF adjustedSizeNew = g.MeasureString(graphicString, testFont, Container.Width);

                if (Container.Width > Convert.ToInt32(adjustedSizeNew.Width) &&
                    Container.Height > Convert.ToInt32(adjustedSizeNew.Height)
                    )
                {
                    // Good font, return it
                    return testFont;
                }
            }

            // If you get here there was no fontsize that worked
            // return minimumSize or original?
            if (smallestOnFail)
            {
                return testFont;
            }
            else
            {
                return originalFont;
            }
        }

        // Get the largest difference between coords in a pair of points
        int GetPointDiff(Point p1, Point p2)
        {
            int xdiff = Math.Abs(p1.X - p2.X);
            int ydiff = Math.Abs(p1.Y - p2.Y);
            if (xdiff > ydiff) { return xdiff; } else { return ydiff; }
        }

        // Get the largest dimension of a rectangle
        int GetRectMax(Rectangle Rect)
        {
            if (Rect.Width > Rect.Height) { return Rect.Width; } else { return Rect.Height; }
        }

        // Round a number to its closest multiple
        int roundToMultiple(int d, int multiple)
        {
            return (int)Math.Round((double)(d / multiple)) * multiple;
        }
        Rectangle QuantizeRect(Rectangle Rect, int quantX, int quantY)
        {
            return new Rectangle(roundToMultiple(Rect.X, quantX),
                                roundToMultiple(Rect.Y, quantY),
                                roundToMultiple(Rect.Width, quantX),
                                roundToMultiple(Rect.Height, quantY));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Text2Text text2Text = new Text2Text();
            text2Text.Show();
        }
    }

    // Image extension methods to make various things better.
    static class BitmapExt
    {
        public static Graphics GetGraphics(this Image image) => Graphics.FromImage(image);

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

        public static Rectangle FitRect(Point a, Point b)
        {
            int x = Math.Min(a.X, b.X);
            int w = Math.Abs(a.X - b.X);
            int y = Math.Min(a.Y, b.Y);
            int h = Math.Abs(a.Y - b.Y);

            return new Rectangle(x, y, w, h);
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
        public static void DrawString(this Image image, string text, Font font, Color color, Rectangle rect)
        {
            image.GetGraphics().DrawString(text, font, new SolidBrush(color), rect);
        }
        public static void DrawString(this Image image, string text, Font font, Color color, Point location)
        {
            image.GetGraphics().DrawString(text, font, new SolidBrush(color), location);
        }
    }

    class WindowFunctions
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public const int SW_HIDE = 0;

        // Delegate to filter which windows to include 
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        /// <summary> Get the text for the window pointed to by hWnd </summary>
        public static string GetWindowText(IntPtr hWnd)
        {
            int size = GetWindowTextLength(hWnd);
            if (size > 0)
            {
                var builder = new StringBuilder(size + 1);
                GetWindowText(hWnd, builder, builder.Capacity);
                return builder.ToString();
            }

            return String.Empty;
        }

        /// <summary> Find all windows that match the given filter </summary>
        /// <param name="filter"> A delegate that returns true for windows
        ///    that should be returned and false for windows that should
        ///    not be returned </param>
        public static IEnumerable<IntPtr> FindWindows(EnumWindowsProc filter)
        {
            IntPtr found = IntPtr.Zero;
            List<IntPtr> windows = new List<IntPtr>();

            EnumWindows(delegate (IntPtr wnd, IntPtr param)
            {
                if (filter(wnd, param))
                {
                    // only add the windows that pass the filter
                    windows.Add(wnd);
                }

                // but return true here so that we iterate all windows
                return true;
            }, IntPtr.Zero);

            return windows;
        }

        /// <summary> Find all windows that contain the given title text </summary>
        /// <param name="titleText"> The text that the window title must contain. </param>
        public static IEnumerable<IntPtr> FindWindowsWithText(string titleText)
        {
            return FindWindows(delegate (IntPtr wnd, IntPtr param)
            {
                return GetWindowText(wnd).Contains(titleText);
            });
        }

        public static bool DummyEnumWindowsProc(IntPtr hWnd, IntPtr param)
        {
            return true;
        }

        // This method will find and hide all open tooltips systemwide
        public static void HideAllTooltips()
        {
            var allToolTips = FindWindows(DummyEnumWindowsProc);
            foreach (IntPtr handle in allToolTips)
            {
                StringBuilder classbuilder = new StringBuilder(100);
                GetClassName(handle, classbuilder, 100);
                if (classbuilder.ToString().Contains("tooltips"))
                {
                    if (IsWindowVisible(handle))
                    {
                        ShowWindow(handle, SW_HIDE); // Hide tooltip
                    }
                }
            }
        }
    }
}