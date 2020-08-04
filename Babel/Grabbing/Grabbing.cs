using System;
using System.Drawing;

namespace Babel.Grabbing
{
    public static class Grabbing
    {
        public static Image Grab(Rectangle target)
        {
            IntPtr hdcSrc = GDI32.User32.GetWindowDC(GDI32.User32.GetDesktopWindow());
            IntPtr hdcDst = GDI32.CreateCompatibleDC(hdcSrc);
            IntPtr bitmap = GDI32.CreateCompatibleBitmap(hdcSrc, target.Width, target.Height);
            GDI32.SelectObject(hdcDst, bitmap);

            GDI32.BitBlt(hdcDst, 0, 0, target.Width, target.Height, hdcSrc, target.X, target.Y, GDI32.TernaryRasterOperations.SRCCOPY);

            return Image.FromHbitmap(bitmap);
        }
    }
}
