using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Babel.Windows;
using Babel.Google;
using System.Windows.Input;

namespace Babel
{
    public partial class frmBabel : Form
    {
        #region Header
        public List<PhraseRect> PhraseRects; // Track user-selected phrases
        public AsyncOCR OCRResult;

        // For bounding box code
        public bool Marking;
        Point MouseStart;
        Point MouseEnd;
        public bool StartingDrag;
        public bool Dragging;
        public PhraseRect DrugPhrase;
        public bool CtrlDown;
        PhraseRect SelectedRect;

        // Image buffers
        private Image snap = null; // Exact image captured from screenshot
        private Image edit = null; // Modified image

        Viewfinder vfw; // Persistent viewfinder window
        public Rectangle SnapRegion; // Position of capture
        public bool AutoScaleVFW; // Whether viewfinder size should always follow main form

        public bool AutoOCR;

        BoundingState BoundingBoxState;
        enum BoundingState
        {
            Normal,
            RectsFound,
            TooSmall
        }

        public enum State
        {
            ready,
            snapped,
            OCRing,
            OCRed,
            translating,
            translated,
        }
        State AppState;

        private frmWindowPicker Picker;
        #endregion

        public enum PhraseRectMode
        {
            intersects,
            contains,
        }

        // Contains a rectangle the user has drawn around one or more words to be translated as a single phrase
        public class PhraseRect
        {
            public Rectangle Location;
            public AsyncTranslation atrans;
            public bool Hovered;
            public bool Clicked;
            public bool Selected;

            // This should be controlled via the context menu
            public PhraseRectMode mode = PhraseRectMode.contains;

            // This should be global for the entire form, if not a persistent setting
            public static bool autoFit = true;

            public PhraseRect(Rectangle Location, AsyncOCR OCRResult, Action<AsyncTranslation> callback = null)
            {
                this.Location = Location;

                UpdateText(OCRResult, callback);
                if (autoFit) AutoFit(OCRResult);
            }

            public void AutoFit(AsyncOCR OCRResult)
            {
                Location = GetBoxes(OCRResult).Select(box => box.rect).FitRect();
            }

            public void UpdateText(AsyncOCR OCRResult, Action<AsyncTranslation> callback = null)
            {
                atrans = new AsyncTranslation(GetText(OCRResult), callback);
            }

            // Get the combined text content of all boxes under this rect
            private string GetText(AsyncOCR OCRResult)
            {
                var myBoxes = GetBoxes(OCRResult);
                if (myBoxes.Any())
                {
                    return myBoxes
                        .Select(ocr => ocr.text)
                        .Aggregate((l, r) => l + " " + r);
                }
                else
                {
                    return "";
                }
            }

            IEnumerable<OCRBox> GetBoxes(IEnumerable<OCRBox> boxes)
            {
                switch (mode)
                {
                    default:
                    case PhraseRectMode.intersects:
                        return boxes.Where(box => Location.IntersectsWith(box.rect));
                    case PhraseRectMode.contains:
                        return boxes.Where(box => Location.Contains(box.rect));
                }
            }

            IEnumerable<OCRBox> GetBoxes(AsyncOCR ocrResult)
            {
                return GetBoxes(ocrResult.smallBoxes);
            }
        }

        // Keeps all our buttons enabled/disabled as needed
        private void ChangeState(State newState)
        {
            AppState = newState;
            switch (newState)
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

        // Called whenever settings should be reevaluated
        void LoadSettings()
        {
            Properties.Settings.Default.Reload();
            tsbOCR.Enabled = !Properties.Settings.Default.autoOCR;
            tsbAutoOCR.Checked = Properties.Settings.Default.autoOCR;

        }

        // Clear all phrases
        void ClearPhrases()
        {
            PhraseRects.Clear();
            pbxDisplay.Invalidate();
        }

        // Clear everything to prep for another snap
        void ClearAll()
        {
            OCRResult = null; //new AsyncOCR(new Bitmap(1, 1));
            PhraseRects.Clear();
            pbxDisplay.Invalidate();
        }

        #region Image capture routines
        // Takes a screenshot of the SnapRegion and returns it
        private Image Snap()
        {
            bool VfwWasVisible = vfw.Visible;
            if (VfwWasVisible) vfw.Visible = false; // Hide viewfinder if appropriate
            this.Visible = false; // Hide self (nobody wants to translate Babel)

            Image result = GDI32.Grab(SnapRegion);

            if (VfwWasVisible) vfw.Visible = true; // Reshow viewfinder if appropriate
            this.Visible = true; // Show self again
            this.Focus(); // Return focus to the main form

            return result;
        }

        // All ingest methods call this with an image to take a new snap
        private void GetSnap(Image image)
        {
            ClearAll();

            pbxDisplay.Image = edit = snap = image.Copy();
            pbxDisplay.Visible = true;
            txtPlaceholder.Visible = false;
            ChangeState(State.snapped);
            DoOCR(true); // Trigger AutoOCR, if enabled.
        }

        // Display or hide the viewfinder
        private void ToggleVFW()
        {
            if (!vfw.Visible)
            {
                vfw.Show();
            }
            else
            {
                vfw.Hide();
            }
        }
        #endregion

        #region Image recognition routines
        // Trigger OCR recognition if appropriate
        bool DoOCR(bool Auto = false)
        {
            if (AppState != State.snapped) return false; // Prevent double OCR

            // Proceed if it's a manual request, or if it's an auto request and autoOCR is on
            if (!Auto || AutoOCR) //Properties.Settings.Default.autoOCR)
            {
                ChangeState(State.OCRing);
                OCRResult = new AsyncOCR(snap, AsyncOCR_callback);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AsyncTranslation_callback(AsyncTranslation result)
        {
            //pbxDisplay.Image = edit;
            //ChangeState(State.translated);
            pbxDisplay.Invalidate();
        }


        private delegate void SafeAsyncOCR_Callback(AsyncOCR result);
        private void AsyncOCR_callback(AsyncOCR result)
        {
            //ChangeState(State.OCRed);
            AutoPhrases();
            pbxDisplay.Invalidate();
       

        
            if (InvokeRequired)
            {
                var d = new SafeAsyncOCR_Callback(AsyncOCR_callback);
                Invoke(d, new object[] { result });
            }
            else
            {
                ChangeState(State.OCRed);
                statusBarLeft.Text = "Recognition complete [" + result.timeStamp + " elapsed]";
                pbxDisplay.Invalidate();
            }
        }

        public void AutoPhrases()
        {
            // Put all smallboxes into a queue, left to right
            Queue<OCRBox> boxes = new Queue<OCRBox>(OCRResult.smallBoxes.OrderBy(box => box.rect.Left));

            // We're planning to cut items out of this queue as we go,
            // so I don't think we can safely foreach over it
            while (boxes.Count > 0)
            {
                // Pick the current leftmost box, and start with its rect exactly.
                OCRBox firstBox = boxes.Dequeue();
                Rectangle growingRect = firstBox.rect;
                int charWidth = firstBox.CharWidth();

                List<OCRBox> phraseCandidates = new List<OCRBox> { firstBox };

                // While there are any boxes in the queue that are aligned with my current rect:
                while (boxes.Any(other => growingRect.CouldBeNextRect(other.rect, charWidth)))
                {
                    // Pick out the leftmost aligned box
                    OCRBox next = boxes.First(other => growingRect.CouldBeNextRect(other.rect, charWidth));

                    // Add it to our phrase
                    phraseCandidates.Add(next);

                    // Remake the queue without any elements from the phrase
                    // (this is the only way to delete from the middle of a queue)
                    boxes = new Queue<OCRBox>(boxes.Except(phraseCandidates));

                    // Expand the rect to include the new smallbox
                    growingRect = growingRect.Include(next.rect);
                }

                PhraseRects.Add(new PhraseRect(growingRect, OCRResult, AsyncTranslation_callback));
            }
        }
        #endregion

        #region Graphics routines
        // Draw all the graphics on top of the image
        // This is generalized so it can be used either by onpaint or by the image save routine
        private void DrawImage(Graphics g)
        {
            // Draw identified words
            if (OCRResult != null && OCRResult.isDone)
            {
                foreach (OCRBox ocr in OCRResult.smallBoxes)
                {
                    g.FillPolygon(new SolidBrush(Color.FromArgb(100, 128, 50, 128)), ocr.points);
                    g.DrawPolygon(new Pen(Color.Purple, 1.0f), ocr.points);
                }
            }

            // Draw user bounding box
            if (Marking)
            {
                Rectangle Rect = MouseStart.RectTo(MouseEnd);
                switch (BoundingBoxState)
                {
                    case BoundingState.Normal:
                        g.DrawRectangle(Pens.White, Rect);
                        break;
                    case BoundingState.RectsFound:
                        g.DrawRectangle(Pens.Green, Rect);
                        break;
                    case BoundingState.TooSmall:
                        g.DrawRectangle(Pens.Red, Rect);
                        break;
                }
                // Draw a black rectangle around the others to help with contrast
                Rect.Inflate(1, 1);
                g.DrawRectangle(Pens.Black, Rect);
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

                if (!PRect.atrans.isDone)
                {
                    // Draw untranslated text
                    g.DrawString(
                            PRect.atrans.rawText,
                            DefaultFont,
                            Brushes.Gray,
                            PRect.Location);
                }
                else
                {
                    // Draw translated text

                    // Fit font to bounding box
                    Font LargeFont = GetAdjustedFont(g, PRect.atrans.translatedText, DefaultFont, PRect.Location, 32, 6, true);

                    // Center-justify text
                    // TODO: Currently disabled to enable wordwrap, fix this
                    int JustifySpace = (int)(PRect.Location.Width - g.MeasureString(PRect.atrans.translatedText, LargeFont).Width) / 2;
                    Rectangle AdjustedPosition = new Rectangle(
                        PRect.Location.Left + JustifySpace, PRect.Location.Top,
                        PRect.Location.Width, PRect.Location.Height);

                    // Draw translated text
                    g.DrawString(
                            PRect.atrans.translatedText,
                            LargeFont,
                            Brushes.White,
                            PRect.Location);
                }


                if (Properties.Settings.Default.displayTimes)
                {
                    // Draw the translation time
                    Font BoldFont = new Font(DefaultFont, FontStyle.Bold);
                    SizeF TimeLength = g.MeasureString(PRect.atrans.timeStamp, BoldFont);

                    g.DrawString(PRect.atrans.timeStamp,
                        BoldFont,
                        Brushes.Gray,
                        new Point(PRect.Location.Right - (int)TimeLength.Width, PRect.Location.Bottom - (int)TimeLength.Height));
                }
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
        #endregion

        #region Helper functions

        // Find a phrase at a given point, for mouse collision etc.
        PhraseRect GetPhraseAtPoint(Point Location)
        {
            // It's important to select the last occurrance in the list, since that'll be the one most recently
            // added or clicked, thus it's on the top of the Z stack.
            if (PhraseRects.Count() < 1) return null;
            List<PhraseRect> FoundRects = PhraseRects.FindAll(x => x.Location.Contains(Location));
            if (FoundRects.Count() < 1) return null;
            return FoundRects.Last();
        }

        // Check whether there are any text boxes underneath this rect
        private bool CheckForText(Rectangle rect)
        {
            if (OCRResult == null) return false;
            return OCRResult.smallBoxes
                .Where(ocr => ocr.rect.IntersectsWith(rect)).Count() > 0;
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
        #endregion
    }

    // Image extension methods to make various things better.
    static class BitmapExt
    {
        public static Image Copy(this Image original)
        {
            Image copy = new Bitmap(original.Width, original.Height);
            Graphics.FromImage(copy).DrawImage(original, 0, 0);
            return copy;
        }
    }
}