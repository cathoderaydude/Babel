using System;
using System.Drawing;
using System.Windows.Forms;

namespace Babel
{
    public partial class Viewfinder : Form
    {
        public Viewfinder()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
        }

        int bWidth = 5;

        Brush BrightColor = Brushes.Red;
        Brush DarkColor = Brushes.DarkRed;
        Brush HighlightColor = Brushes.LightPink;

        public frmBabel MainForm;

        private bool FlashState;
        public void Flicker()
        {
            // Make a temporary timer that flashes the viewfinder for attention
            Timer FlashTimer = new Timer();
            FlashTimer.Interval = 60;
            FlashTimer.Tag = 6;
            FlashTimer.Tick += delegate (object ssender, EventArgs ee)
            {
                Timer t = ((Timer)ssender);
                t.Tag = ((int)t.Tag) - 1;
                FlashState = !FlashState;
                this.Invalidate();
                if ((int)t.Tag < 1)
                {
                    t.Dispose();
                }
            };
            FlashTimer.Enabled = true;
        }

        private void Viewfinder_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (FlashState) return;

            int bWidthH = (bWidth / 2) - 1;
            Pen Outline = new Pen(BrightColor, 2);
            Pen DarkLine = new Pen(Brushes.Black, 2);

            // Draw resize handle
            if (SizeGripStyle == SizeGripStyle.Show)
            {
                int resSize = RESIZE_HANDLE_SIZE + 2;
                Point[] ResizeHandle = new Point[]
                {
                new Point(this.Width - resSize, this.Height),
                new Point(this.Width, this.Height),
                new Point(this.Width, this.Height - resSize)
                };
                g.FillPolygon(DarkColor, ResizeHandle);
            }

            // Draw outline
            g.DrawRectangle(Outline, this.ClientRectangle.InflateO(-1, -1));
            
            // These lines were appropriate to the old tab-on-top approach
            /*g.DrawLine(Outline, bWidthH, 20, bWidthH, this.Height-1);
            g.DrawLine(Outline, this.Width- bWidthH, 20, this.Width - bWidthH, this.Height-1);
            g.DrawLine(Outline, 0, this.Height- bWidthH, this.Width, this.Height-1);
            g.DrawLine(Outline, 0, 20, this.Width, 20);*/

            // Draw window title tab
            Point[] WTab = new Point[]
            {
                new Point(100,20),
                new Point(110,0),
                new Point(20,0),
                new Point(30,20)
            };

            g.FillPolygon(DarkColor, WTab);
            g.DrawPolygon(new Pen(BrightColor), WTab);

            // Draw close box
            Rectangle CloseBox = new Rectangle(85, 5, 10, 10);
            g.DrawRectangle(DarkLine, CloseBox);
            CloseBox.Inflate(-2, -2);
            g.FillEllipse(Brushes.Black, CloseBox);

            // Draw title
            g.DrawString("Babel", new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, new Rectangle(33, 2, 80, 20));
            g.DrawString("Babel", new Font(FontFamily.GenericSansSerif, 10), HighlightColor, new Rectangle(32, 1, 80, 20));
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        int RESIZE_HANDLE_SIZE = 16;
        const UInt32 HTBOTTOMRIGHT = 17;
        const UInt32 WM_MOUSEMOVE = 0x0200;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE)
            {
                Size formSize = this.Size;
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);
                Rectangle hitBox = new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE);
                if (hitBox.Contains(clientPoint) && SizeGripStyle == SizeGripStyle.Show)
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                }
                else
                {
                    m.Result = (IntPtr)(HT_CAPTION);
                }
            }
        }

        private void Viewfinder_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
            
            MainForm.SnapRegion = this.RectangleToScreen(this.ClientRectangle);
        }
        
        private void Viewfinder_MouseClick(object sender, MouseEventArgs e)
        {
            Rectangle closeBox = new Rectangle(85, 5, 10, 10);
            if (closeBox.Contains(e.Location)) this.Visible = false;
        }

        private void Viewfinder_Move(object sender, EventArgs e)
        {
            MainForm.SnapRegion = this.RectangleToScreen(this.ClientRectangle);
        }
    }
}
