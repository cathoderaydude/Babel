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
    public partial class Viewfinder : Form
    {
        public Viewfinder()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
        }

        int bWidth = 5;

        private void Viewfinder_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int bWidthH = (bWidth / 2) - 1;

            g.DrawLine(new Pen(Brushes.Magenta, 2), bWidthH, 20, bWidthH, this.Height-1);
            g.DrawLine(new Pen(Brushes.Magenta, 2), this.Width- bWidthH, 20, this.Width - bWidthH, this.Height-1);
            g.DrawLine(new Pen(Brushes.Magenta, 2), 0, this.Height- bWidthH, this.Width, this.Height-1);

            g.DrawLine(new Pen(Brushes.Magenta, 2), 0, 20, this.Width, 20);

            Point[] WTab = new Point[]
            {
                new Point(20,20),
                new Point(30,0),
                new Point(100,0),
                new Point(110,20),
                new Point(this.Width,20)
            };

            g.FillPolygon(Brushes.Purple, WTab);
            g.DrawPolygon(Pens.Magenta, WTab);
            int resSize = RESIZE_HANDLE_SIZE + 2;
            g.FillRectangle(Brushes.Purple, this.Width - resSize, this.Height - resSize, resSize, resSize);
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
                Console.WriteLine(screenPoint.ToString());
                Rectangle hitBox = new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE);
                if (hitBox.Contains(clientPoint))
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
        }
    }
}
