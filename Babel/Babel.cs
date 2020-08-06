using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Babel.Windows;
using Babel.Google;

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
        public AsyncOCR OCRResult;
        
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
        public Rectangle SnapRegion; // Position of capture
        public bool AutoScaleVFW; // Whether viewfinder size should always follow main form

        BoundingState BoundingBoxState;
        enum BoundingState
        {
            Normal,
            RectsFound,
            TooSmall
        }

        private frmWindowPicker Picker;

        public frmBabel()
        {
            InitializeComponent();
        }

        // Contains a rectangle the user has drawn around one or more words to be translated as a single phrase
        public class PhraseRect
        {
            public Rectangle Location;
            public AsyncTranslation atrans;
            public bool Hovered;
            public bool Clicked;
            public bool Selected;

            public PhraseRect(Rectangle Location, AsyncOCR OCRResult, Action<AsyncTranslation> callback = null)
            {
                this.Location = Location;

                this.UpdateText(OCRResult, callback);
            }

            public void UpdateText(AsyncOCR OCRResult, Action<AsyncTranslation> callback = null)
            {
                string text = GetTextInRect(Location, OCRResult);
                atrans = new AsyncTranslation(text, callback);
            }

            IEnumerable<OCRBox> GetBoxes(IEnumerable<OCRBox> boxes)
            {
                return boxes.Where(box => box.rect.IntersectsWith(Location));
            }

            IEnumerable<OCRBox> GetBoxes(AsyncOCR ocrResult)
            {
                return GetBoxes(ocrResult.smallBoxes);
            }
        }

        private void Viewfinder_Load(object sender, EventArgs e)
        {
            OCRResult = new AsyncOCR(new Bitmap(1, 1));
            PhraseRects = new List<PhraseRect>();

            SnapRegion = new Rectangle(0, 0, 640, 480);

            Text = "Viewfinder - Ready";
            ChangeState(State.ready);

            vfw = new Viewfinder();
            vfw.MainForm = this;
            vfw.StartPosition = FormStartPosition.Manual;
            vfw.Location = new Point(this.Left + 50, this.Top + 50);

            Picker = new frmWindowPicker();

            #if DEBUG
            ToggleVFW(); // Show viewfinder immediately
            Picker.Show();
            #endif
        }

        // Takes a screenshot of what's behind the window and returns it
        private Image Snap()
        {
            bool VfwWasVisible = vfw.Visible;
            if (VfwWasVisible) vfw.Visible = false; // Hide viewfinder if appropriate

            Image result = GDI32.Grab(SnapRegion);
            
            if (VfwWasVisible) vfw.Visible = true; // Reshow viewfinder if appropriate

            return result;
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
            pbxDisplay.Visible = true;
            txtPlaceholder.Visible = false;
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
                OCRResult = new AsyncOCR(snap, AsyncOCR_callback);
                return true;
            } else
            {
                return false;
            }
        }

        // Ingest image from viewfinder
        private void btnSnap_Click(object sender, EventArgs e)
        {
            GetSnap(Snap());
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

        private void tsbVFW_Click(object sender, EventArgs e)
        {
            ToggleVFW();
        }

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

        // Clear all identified phrases
        private void btnRevert_Click(object sender, EventArgs e)
        {
            ClearPhrases();
            ChangeState(State.OCRed);
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Text2Text text2Text = new Text2Text();
            text2Text.Show();
        }

        private void AsyncTranslation_callback(AsyncTranslation result)
        {
            //pbxDisplay.Image = edit;
            //ChangeState(State.translated);
            pbxDisplay.Invalidate();
        }

        private void AsyncOCR_callback(AsyncOCR result)
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

        // Finish drawing/dragging
        private void pbxDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (OCRResult != null && OCRResult.isDone)
            {
                if (Marking == true) // We were drawing a bounding box
                {
                    // Find out if any words were selected and, if so, create a phrase box around them
                    Rectangle TestRect = MouseStart.RectTo(MouseEnd);
                    if (TestRect.Width > 25 && TestRect.Height > 15)
                    {
                        rect = OCRResult.smallBoxes
                            .Where(box => box.rect.IntersectsWith(rect))
                            .SelectMany(box => box.points)
                            .FitRect();
                        ChangeState(State.translated);
                        PhraseRects.Add(new PhraseRect(TestRect, OCRResult, AsyncTranslation_callback));
                    }
                    Marking = false;
                }
                else
                { // We were selecting/dragging
                    PhraseRect PRect = GetPhraseAtPoint(e.Location);
                    if (PRect != null)
                    {
                        if (PRect.Clicked == true)
                        {
                            foreach (PhraseRect TPRect in PhraseRects) { TPRect.Clicked = false; TPRect.Selected = false; } // Clear other phrase states
                            PRect.Clicked = false;
                            PRect.Selected = true;
                            PRect.UpdateText(OCRResult);
                        }
                    }
                    else
                    {
                        foreach (PhraseRect TPRect in PhraseRects) { TPRect.Clicked = false; TPRect.Selected = false; } // Clear other phrase states
                    }
                }
                StartingDrag = false;
                Dragging = false;
                pbxDisplay.Invalidate();
            }

            StartingDrag = false;
            Dragging = false;
            pbxDisplay.Invalidate();
        }

        
        private void pbxDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (Marking) // We're drawing a bounding box, set the second endpoint
            {
                MouseEnd = e.Location;

                // Draw the phrase box
                Rectangle TestRec = MouseStart.RectTo(MouseEnd);
                if (TestRec.Width < 25 || TestRec.Height < 15) // If it's too small, draw it red
                {
                    BoundingBoxState = BoundingState.TooSmall;
                } else if (CheckForText(TestRec))
                {
                    BoundingBoxState = BoundingState.RectsFound; // If it's over any text, draw it green
                } else
                {
                    BoundingBoxState = BoundingState.Normal; // Otherwise draw it white
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
        // Find a phrase at a given point, for mouse collision etc.
        PhraseRect GetPhraseAtPoint(Point Location)
        {
            foreach(PhraseRect PRect in PhraseRects)
            {
                if (PRect.Location.Contains(Location)) return PRect;
            }
            return null;
        }

        // Check whether there are any text boxes underneath this rect
        private bool CheckForText(Rectangle rect)
        {
            return OCRResult.smallBoxes
                .Where(ocr => ocr.rect.IntersectsWith(rect)).Count() > 0;
        }
        // Get the combined text content of all boxes under this rect
        private static string GetTextInRect(Rectangle rect, AsyncOCR OCRResult)
        {
            if (OCRResult.smallBoxes
                .Where(ocr => ocr.rect.IntersectsWith(rect)).Count() < 1) return null;

            return OCRResult.smallBoxes
                .Where(ocr => ocr.rect.IntersectsWith(rect))
                .Select(ocr => ocr.text)
                .Aggregate((l, r) => l + " " + r);
        }
        private string GetTextInRect(Rectangle rect)
        {
            return GetTextInRect(rect, OCRResult);
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

        // Called whenever settings should be reevaluated
        void LoadSettings()
        {
            Properties.Settings.Default.Reload();
            tsbOCR.Enabled = !Properties.Settings.Default.autoOCR;
            tsbAutoOCR.Checked = Properties.Settings.Default.autoOCR;

        }

        void ClearPhrases()
        {
            PhraseRects.Clear();
            pbxDisplay.Invalidate();
        }

        // Clear everything to prep for another snap
        void ClearAll()
        {
            OCRResult = new AsyncOCR(new Bitmap(1, 1));
            PhraseRects.Clear();
            pbxDisplay.Invalidate();
        }

        private void tsbMaxVFW_Click(object sender, EventArgs e)
        {
            Screen screen = Screen.FromControl(vfw);
            vfw.Size = new Size(screen.WorkingArea.Width, screen.WorkingArea.Height);
            vfw.Location = new Point(screen.Bounds.X, screen.Bounds.Y);

            vfw.Flicker();
        }

        private void scaleViewfinderToWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoScaleVFW = !AutoScaleVFW;
            vfw.SizeGripStyle = AutoScaleVFW ? SizeGripStyle.Hide : SizeGripStyle.Show;
            vfw.Size = panel1.Size;
            vfw.Invalidate();
        }

        private void frmBabel_Resize(object sender, EventArgs e)
        {
            vfw.Size = panel1.Size;
        }

        private void tsbCrosshair_MouseDown(object sender, MouseEventArgs e)
        {
            Picker.Show();
            // Make a temporary timer that flashes the viewfinder for attention
            Timer FlashTimer = new Timer();
            FlashTimer.Interval = 60;
            FlashTimer.Tag = 6;
            MouseStart = MousePosition;
            FlashTimer.Tick += delegate (object ssender, EventArgs ee)
            {
                Timer t = ((Timer)ssender);

                if (MouseStart != MousePosition)
                {
                    Picker.GoPoint(MousePosition);
                    MouseStart = MousePosition;
                }
                if (MouseButtons != MouseButtons.Left)
                {
                    // TODO: Resize the viewfinder
                    vfw.Size = Picker.Size;
                    vfw.Location = Picker.Location;
                    Picker.Hide();
                    t.Dispose();
                }
            };
            FlashTimer.Enabled = true;
        }
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
