using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Babel.Windows;

namespace Babel
{
    public partial class frmWindowPicker : Form
    {
        public frmWindowPicker()
        {
            InitializeComponent();
        }

        private void frmWindowPicker_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle WindowRect = ClientRectangle;
            WindowRect.Inflate(-2, -2);
            g.DrawRectangle(new Pen(Brushes.White, 5), WindowRect);
            WindowRect.Inflate(-2, -2);
            g.DrawRectangle(Pens.Black, WindowRect);
        }

        public void GoPoint(Point newpoint)
        {
            Control cWnd;
            IntPtr hWnd = WindowFunctions.GetWindowAtPoint(newpoint, out cWnd);
            Rectangle WindowLoc = WindowFunctions.GetRectFromHwnd(hWnd);
            this.Location = new Point(WindowLoc.Left, WindowLoc.Top);
            this.Size = new Size(WindowLoc.Width, WindowLoc.Height);
            //Console.WriteLine(this.Location.ToString() + " " + WindowFunctions.GetWindowText(hWnd));
            this.Invalidate();
        }

        private void frmWindowPicker_Move(object sender, EventArgs e)
        {
            
        }

        protected override void WndProc(ref Message m)
        {
            // Return transparent to absolutely ensure this window is never seen
            int HTTRANSPARENT = -1;
            m.Result = (IntPtr) HTTRANSPARENT;
        }
    }
}
