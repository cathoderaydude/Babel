using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

        public static Point ToPoint(Vertex v) => new Point(v.X, v.Y);

        public static Vertex ToVertex(Point p) => new Vertex { X = p.X, Y = p.Y };
        
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
    }
}
