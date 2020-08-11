using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Linq;

namespace Babel.Async
{
    public static class AsyncStatic
    {
        public static RateLimiter rate = new RateLimiter { size = Properties.Settings.Default.reqsPerSecond };

        public static IAsyncOCR MakeOCR(Image input, OCRCallback callback)
        {
            switch (Properties.Settings.Default.OCRDataSource)
            {
                case DataSource.Google:
                    return new GoogleImpl.AsyncOCR(input, callback);

                case DataSource.Microsoft:
                    return new MicrosoftImpl.AsyncOCR(input, callback);

                default:
                    return new DummyImpl.AsyncOCR(callback);
            }
        }

        public static IAsyncTranslation MakeTranslation(string input, TranslationCallback callback)
        {
            switch (Properties.Settings.Default.TranslationDataSource)
            {
                case DataSource.Google:
                    return new GoogleImpl.AsyncTranslation(input, callback);

                case DataSource.Microsoft:
                    return new MicrosoftImpl.AsyncTranslation(input, callback);

                case DataSource.DeepL:
                    return new DeepLImpl.AsyncTranslation(input, callback);

                default:
                    return new DummyImpl.AsyncTranslation(input, callback);
            }
        }

        public static IAsyncGSL MakeGSL(GSLCallback callback)
        {
            switch (Properties.Settings.Default.OCRDataSource)
            {
                case DataSource.Google:
                    return new GoogleImpl.AsyncGSL(callback);

                case DataSource.Microsoft:
                    return new MicrosoftImpl.AsyncGSL(callback);

                // TODO: Replace this with working code
                case DataSource.DeepL:
                    return new GoogleImpl.AsyncGSL(callback);

                default:
                    return new DummyImpl.AsyncGSL(callback);
            }
        }
    }

    public class OCRBox
    {
        public Point[] points;
        public Rectangle rect => Utility.FitRect(points);
        public string text;

        // Google data
        public OCRBox(Google.Cloud.Vision.V1.EntityAnnotation ann)
        {
            text = ann.Description;
            points = ann.BoundingPoly.Vertices.Select(Utility.ToPoint).ToArray();
        }

        // Microsoft data
        public OCRBox(string boundingBox, string text)
        {
            int[] boxCoords = boundingBox.Split(',').Select(x => Convert.ToInt32(x))
                .ToArray();
            points = new Rectangle(boxCoords[0], boxCoords[1], boxCoords[2], boxCoords[3])
                .Corners()
                .ToArray();

            this.text = text;
        }

        #region Dummy Data

        private OCRBox() { }

        private static readonly int dummyBoxSize = 100;
        private static readonly int gutterSize = 10;
        private static readonly int gridSize = 5;
        private static int dummyBigBoxSize => (dummyBoxSize * gridSize) + (gutterSize * (gridSize - 1));

        internal static OCRBox DummyBigBox() =>
            new OCRBox
            {
                points = new Rectangle(gutterSize, gutterSize, dummyBigBoxSize, dummyBigBoxSize).Corners().ToArray(),
                text = "BIG BOX",
            };

        internal static OCRBox DummySmallBox(int idx)
        {
            if ((idx < 0) || (idx >= gridSize * gridSize)) throw new ArgumentOutOfRangeException();

            int gx = idx % gridSize;
            int gy = idx / gridSize;

            int bx = gutterSize + (dummyBoxSize + gutterSize) * gx;
            int by = gutterSize + (dummyBoxSize + gutterSize) * gy;

            return new OCRBox
            {
                points = new Rectangle(bx, by, dummyBoxSize, dummyBoxSize).Corners().ToArray(),
                text = "SMALL BOX " + (idx + 1),
            };
        }

        internal static OCRBox[] DummySmallBoxes() =>
            Enumerable.Range(0, gridSize * gridSize)
                .Select(DummySmallBox)
                .ToArray();

        internal static OCRBox ErrorBigBox() =>
            new OCRBox
            {
                points = new Rectangle(gutterSize, gutterSize, dummyBoxSize, dummyBoxSize).Corners().ToArray(),
                text = "ERROR",
            };

        #endregion
    }

    public class LanguageItem
    {
        public string name;
        public string code;

        public override string ToString() => name + " (" + code + ")";


        // Google data
        public LanguageItem(Google.Cloud.Translate.V3.SupportedLanguage lang)
        {
            name = lang.DisplayName;
            code = lang.LanguageCode;
        }

        // Microsoft data
        public LanguageItem(string code, JToken lang)
        {
            name = (string)lang["name"];
            this.code = code;
        }

        #region Dummy data

        private LanguageItem(string name, string code)
        {
            this.name = name;
            this.code = code;
        }

        // Dummy data
        public static LanguageItem[] DummyLanguages()
        {
            return new LanguageItem[] { new LanguageItem("English (dummy)", "en") };
        }

        #endregion
    }

    public interface IAsync
    {
        bool isDone { get; }
        string timeStamp { get; }
    }

    public delegate void OCRCallback(IAsyncOCR result);

    public interface IAsyncOCR : IAsync
    {
        OCRBox bigBox { get; }
        OCRBox[] smallBoxes { get; }
        string name { get; }
    }

    public delegate void TranslationCallback(IAsyncTranslation result);

    public interface IAsyncTranslation : IAsync
    {
        string rawText { get; }
        string translatedText { get; }
        string name { get; }
    }

    public delegate void GSLCallback(IAsyncGSL result);

    public interface IAsyncGSL : IAsync
    {
        LanguageItem[] languages { get; }
    }

    public enum DataSource
    {
        Dummy,
        Google,
        Microsoft,
        DeepL
    }
}
