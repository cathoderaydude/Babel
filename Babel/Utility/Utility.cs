using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using Babel.Async;

namespace Babel
{
    public class DebugLog
    {
        static object DebugLock;

        public static void Log(string message)
        {
            if (DebugLock == null) DebugLock = new object();
            lock(DebugLock)
            {
                string LogMessage = "[" + DateTime.Now.ToString() + "] " + message;
                StreamWriter log = new StreamWriter(Application.StartupPath + "\\babel.log", true);
                Console.WriteLine("D:" + LogMessage);
                log.WriteLine(LogMessage);
                log.Close();
            }
            
        }
    }

    public class RateLimiter
    {
        #region Circular queue

        // If you reset the size while threads are waiting, weird behavior may result, but the
        // situation should stabilize on its own
        public int size
        {
            get => timestamps.Length;
            set
            {
                timestamps = new DateTime[value];
                cursor = 0;
            }
        }

        // Our buffer of timestamps.
        // We manage this as a perfectly-full circular queue.
        private DateTime[] timestamps = new DateTime[0];
        private int cursor = 0;

        // If we want to insert a time into the queue, we write it at the cursor and advance one.
        private void WriteTime(DateTime time)
        {
            if (size > 0)
            {
                timestamps[cursor] = time;
                cursor = (cursor + 1) % size;
            }
        }

        // If we want to know the oldest time in the queue, we need read from the cursor.
        private DateTime ReadTime()
        {
            return timestamps[cursor];
        }

        #endregion


        public void Check()
        {
            // If the rate limit is zero, the rate limiter does not check anything
            if (size != 0)
            {
                lock (this)
                {
                    // Oldest time in queue, corresponding to the Nth previous request, plus one second
                    DateTime waitUntil = ReadTime().AddSeconds(1);

                    // Current time in milliseconds
                    DateTime now = DateTime.Now;

                    // If wait time is positive, we need to wait that many ms before continuing
                    int waitTime = (int)(waitUntil - now).TotalMilliseconds;
                    if (waitTime > 0)
                        Thread.Sleep(waitTime);

                    // Before we go, record what time we were allowed to continue
                    WriteTime(DateTime.Now);
                }
            }
        }
    }

    // Interface for a class that has data of a specific type
    public interface IHasData<T>
    {
        T ToData();
    }

    // Objects of this class will display nicely on combo boxes,
    // and work well with the helper method SelectedData<T>
    public class ComboBoxItem<T> : IHasData<T>
    {
        public string name;
        public T data;

        public override string ToString() => name;
        public T ToData() => data;
    }

    public static class Utility
    {
        #region Randomization stuff

        public static Random HexRNG = new Random();
        public static string RandomHex()
        {
            // Generates a random 6 digit hex string
            lock (HexRNG)
            {
                var bytes = new Byte[6];
                HexRNG.NextBytes(bytes);

                var hexArray = Array.ConvertAll(bytes, x => x.ToString("X2"));
                var hexStr = String.Concat(hexArray);
                return (hexStr.ToLower());
            }
        }

        #endregion

        // Extension on ComboBox
        public static T SelectedData<T>(this ComboBox c)
        {
            // Try to turn the SelectedItem object into an IHasData<T> and then retrieve its data
            if (c.SelectedItem == null)
                throw new ArgumentException("c.SelectedItem was null");
            else if (!(c.SelectedItem is IHasData<T>))
                throw new ArgumentException("c.SelectedItem was not a ComboBoxItem<T>");
            else
                return (c.SelectedItem as IHasData<T>).ToData();
        }

        #region Geometry stuff

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
            int l = ps.Min(p => p.X);
            int t = ps.Min(p => p.Y);
            int r = ps.Max(p => p.X);
            int b = ps.Max(p => p.Y);

            return Rectangle.FromLTRB(l, t, r, b);
        }

        public static Rectangle FitRect(this IEnumerable<Rectangle> rs)
        {
            int l = rs.Min(x => x.Left);
            int t = rs.Min(x => x.Top);
            int r = rs.Max(x => x.Right);
            int b = rs.Max(x => x.Bottom);

            return Rectangle.FromLTRB(l, t, r, b);
        }

        public static Rectangle Include(this Rectangle r1, Rectangle r2)
        {
            int l = Math.Min(r1.Left, r2.Left);
            int t = Math.Min(r1.Top, r2.Top);
            int r = Math.Max(r1.Right, r2.Right);
            int b = Math.Max(r1.Bottom, r2.Bottom);

            return Rectangle.FromLTRB(l, t, r, b);
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

        #endregion

        #region Autophrasing helpers

        // Guess the width of the characters in the box, based on total width and character count
        public static int CharWidth(this OCRBox box) => box.rect.Width / box.text.Length;

        // Identify the vertical center of the rect
        public static int VCenter(this Rectangle rect) => rect.Top + (rect.Height / 2);

        // After much experimentation, "your vertical center is between my upper and lower bounds" is Pretty Good
        public static bool IsOnSameLine(this Rectangle l, Rectangle r)
        {
            int rCenter = r.VCenter();
            return (rCenter >= l.Top) && (rCenter <= l.Bottom);
        }

        // This could be a user setting
        public const int characterGapAllowed = 2;

        // Determine if the space between the boxes is acceptable
        public static bool IsRightNeighbor(this Rectangle l, Rectangle r, int charWidth)
        {
            int gapWidth = r.Left - l.Right;

            // Negative gap means the right rectangle overlaps the left rectangle; overlaps of less than a character are fine
            // Positive gap means that there's space between; allow this if it's less than our allowed character gap
            return (gapWidth > -charWidth) && (gapWidth < charWidth * characterGapAllowed);
        }

        // Combine the vertical alignment check and the horizontal spacing check
        public static bool CouldBeNextRect(this Rectangle l, Rectangle r, int charWidth)
        {
            return l.IsOnSameLine(r) && l.IsRightNeighbor(r, charWidth);
        }

        #endregion
    }
}
