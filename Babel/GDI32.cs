/*
    This file is part of GoonCam.

    GoonCam is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    GoonCam is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with GoonCam.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Runtime.InteropServices;

namespace Babel
{
    class GDI32
    {
        [DllImport("GDI32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);
        [DllImport("GDI32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("GDI32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("GDI32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);
        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("GDI32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);


        public enum TernaryRasterOperations
        {
            SRCCOPY = 0x00CC0020,
            SRCINVERT = 0x00660046
        }

        public class User32
        {
            [DllImport("User32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("User32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("User32.dll")]
            public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [DllImport("user32.dll")]
            public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
            [DllImport("user32.dll")]
            public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool DestroyIcon(IntPtr hIcon);

            public const Int32 CURSOR_SHOWING = 0x00000001;

            [DllImport("user32.dll", EntryPoint = "GetCursorInfo")]
            public static extern bool GetCursorInfo(out CURSORINFO pci);

            [DllImport("user32.dll", EntryPoint = "GetIconInfo")]
            public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

            [DllImport("user32.dll", EntryPoint = "CopyIcon")]
            public static extern IntPtr CopyIcon(IntPtr hIcon);

            [StructLayout(LayoutKind.Sequential)]
            public struct POINT { public Int32 x; public Int32 y; }

            [StructLayout(LayoutKind.Sequential)]
            public struct CURSORINFO
            {
                public Int32 cbSize; public Int32 flags; public IntPtr hCursor; public POINT ptScreenPos;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct ICONINFO { public bool fIcon; public Int32 xHotspot; public Int32 yHotspot; public IntPtr hbmMask; public IntPtr hbmColor; }

            public static CURSORINFO? GetCursorInfo()
            {
                var info = new CURSORINFO();
                info.cbSize = Marshal.SizeOf(info);
                if (GetCursorInfo(out info))
                {
                    return info;
                }
                return null;
            }

            public static ICONINFO? GetIconInfo(IntPtr hIcon)
            {
                var info = new ICONINFO();
                if (GetIconInfo(hIcon, out info))
                    return info;
                return null;
            }
        }

    }
}
