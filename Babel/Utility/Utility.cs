using Babel.Google;
using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static Babel.frmBabel;

namespace Babel
{
    public static class Utility
    {
        public static Rectangle RectTo(this Point p1, Point p2)
        {
            return new Rectangle(
              Math.Min(p1.X, p2.X),
              Math.Min(p1.Y, p2.Y),
              Math.Abs(p1.X - p2.X),
              Math.Abs(p1.Y - p2.Y));
        }

        public static Point ToPoint(Vertex v) => new Point(v.X, v.Y);

        public static Vertex ToVertex(Point p) => new Vertex { X = p.X, Y = p.Y };

        public static Rectangle FitRect(this IEnumerable<Point> ps)
        {
            int x = ps.Min(p => p.X);
            int maxX = ps.Max(p => p.X);
            int y = ps.Min(p => p.Y);
            int maxY = ps.Max(p => p.Y);
            int w = maxX - x;
            int h = maxY - y;

            return new Rectangle(x, y, w, h);
        }

        public static Rectangle InflateO(this Rectangle rect, int width, int height)
        {
            Rectangle tempRect = rect;
            tempRect.Inflate(width, height);
            return tempRect;
        }
        
        public static Rectangle FitRect(this IEnumerable<Vertex> vs)
        {
            int x = vs.Min(v => v.X);
            int maxX = vs.Max(v => v.X);
            int y = vs.Min(v => v.Y);
            int maxY = vs.Max(v => v.Y);
            int w = maxX - x;
            int h = maxY - y;

            return new Rectangle(x, y, w, h);
        }

        public static IEnumerable<Point> Corners(this Rectangle rect)
        {
            yield return new Point(rect.Left, rect.Top);
            yield return new Point(rect.Right, rect.Top);
            yield return new Point(rect.Right, rect.Bottom);
            yield return new Point(rect.Left, rect.Bottom);
        }

        public static IEnumerable<Vertex> VCorners(this Rectangle rect)
        {
            yield return new Vertex { X = rect.Left, Y = rect.Top };
            yield return new Vertex { X = rect.Right, Y = rect.Top };
            yield return new Vertex { X = rect.Right, Y = rect.Bottom };
            yield return new Vertex { X = rect.Left, Y = rect.Bottom };
        }

        public static Rectangle FitRect(this IEnumerable<Rectangle> children)
        {
            return FitRect(children.SelectMany(rect => rect.Corners()));
        }

        #region Autophrasing

        // Guess the width of the characters in the box
        public static int CharWidth(this OCRBox box) => box.rect.Width / box.text.Length;

        // Establish how far away the next box is allowed to be
        private const int spacesAllowed = 2;
        public static int AllowedSpace(this OCRBox box) => box.CharWidth() * spacesAllowed;

        public static Point Center(this Rectangle rect) => new Point(rect.Left + rect.Height / 2, rect.Top + rect.Height / 2);
        private static float Slope(this Rectangle rect) => (float)rect.Height / (float)rect.Width;

        public static bool IsAlignedWith(this Rectangle l, Rectangle r)
        {
            if (r.Bottom < l.Top || r.Top > l.Bottom) return false;
            //else return true;// (float)Math.Abs(l.Height - r.Height) < ((float)l.Height * 0.1);
            
            //int h = r.Center().X - l.Center().X;
            int v = Math.Abs(l.Center().Y - r.Center().Y);
            return v * 2 < l.Height;

            //if (h < 0) return false;
            //else return h * l.Slope() > v;
        }

        public static bool IsHorizontallyNear(this Rectangle l, Rectangle r, int spacing) => (l.Right < r.Left) && (l.Right + spacing >= r.Left);
        public static bool IsHorizontallyNear(this Rectangle l, Rectangle r) => l.IsHorizontallyNear(r, l.Height);

        #endregion
    }
}
