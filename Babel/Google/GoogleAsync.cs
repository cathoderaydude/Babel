using Google.Api.Gax.ResourceNames;
using Google.Cloud.Translate.V3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using SImage = System.Drawing.Image;
using GImage = Google.Cloud.Vision.V1.Image;
using Google.Cloud.Vision.V1;

namespace Babel.Google
{
    public class OCRBox
    {
        public Rectangle rect;
        public string text;

        private Rectangle FitRect(IEnumerable<Vertex> ps)
        {
            int x = ps.Min(p => p.X);
            int maxX = ps.Max(p => p.X);
            int y = ps.Min(p => p.Y);
            int maxY = ps.Max(p => p.Y);
            int w = maxX - x;
            int h = maxY - y;

            return new Rectangle(x, y, w, h);
        }

        public OCRBox(EntityAnnotation ann)
        {
            text = ann.Description;
            rect = FitRect(ann.BoundingPoly.Vertices);
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
                rect = new Rectangle(gutterSize, gutterSize, dummyBigBoxSize, dummyBigBoxSize),
                text = "BIG BOX",
            };

        internal static OCRBox DummySmallBox(int idx)
        {
            if ((idx < 0)|| (idx >= gridSize * gridSize)) throw new ArgumentOutOfRangeException();

            int gx = idx % gridSize;
            int gy = idx / gridSize;

            int bx = (gutterSize + dummyBoxSize) * gx;
            int by = (gutterSize + dummyBoxSize) * gy;

            return new OCRBox
            {
                rect = new Rectangle(bx, by, dummyBoxSize, dummyBoxSize),
                text = "SMALL BOX " + (idx + 1),
            };
        }

        internal static OCRBox[] DummySmallBoxes() => 
            Enumerable.Range(0, gridSize * gridSize)
                .Select(DummySmallBox)
                .ToArray();

        #endregion
    }

    public class AsyncOCR
    {
        // pre-OCR
        public SImage image { get; private set; }
        private Action<AsyncOCR> callback;

        public AsyncOCR(SImage image, Action<AsyncOCR> callback = null)
        {
            this.image = image;

            if (Properties.Settings.Default.dummyData)
            {
                bigBox = OCRBox.DummyBigBox();
                smallBoxes = OCRBox.DummySmallBoxes();
                timeStamp = "[dummy]";
                callback?.Invoke(this);
            }
            else
            {
                task = Task.Run(DoOCR);
            }
        }

        // do the OCR
        Task task;
        public bool isDone
        {
            get
            {
                if (task == null)
                    return false;
                else
                    return task.IsCompleted;
            }
        }

        // post-OCR
        public OCRBox bigBox { get; private set; }
        public OCRBox[] smallBoxes { get; private set; }
        public string timeStamp { get; private set; }

        private async Task DoOCR()
        {
            Stopwatch sw = new Stopwatch();

            // Dump the provided image to a memory stream
            var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Png);
            stream.Position = 0;

            // Load the stream as a gimage
            GImage gimage = GImage.FromStream(stream);

            // Make our connection client
            ImageAnnotatorClient client = new ImageAnnotatorClientBuilder
            {
                CredentialsPath = Properties.Settings.Default.apiKeyPath,
            }.Build();

            // Ask for OCR
            sw.Start();
            var response = await client.DetectTextAsync(gimage);
            sw.Stop();

            // First result is the big box
            bigBox = new OCRBox(response.First());

            // Following results are the small boxes
            smallBoxes = response.Skip(1)
                .Select(ann => new OCRBox(ann))
                .ToArray();

            timeStamp = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                sw.Elapsed.Hours,
                sw.Elapsed.Minutes,
                sw.Elapsed.Seconds,
                sw.Elapsed.Milliseconds);

            callback?.Invoke(this);
        }
    }

    public class AsyncTranslation
    {
        // pre-translation
        public string rawText { get; private set; }
        private event Action<AsyncTranslation> callback;
        
        public AsyncTranslation(string text, Action<AsyncTranslation> callback = null)
        {
            rawText = text;

            if (Properties.Settings.Default.dummyData)
            {
                translatedText = rawText;
                detectedLocale = Properties.Settings.Default.targetLocale;
                timeStamp = "[dummy]";
                callback?.Invoke(this);
            }
            else
            {
                this.callback += callback;
                task = Task.Run(DoTranslation);
            }
        }

        // do the translation
        Task task;
        public bool isDone
        {
            get
            {
                if (task == null)
                    return false;
                else
                    return task.IsCompleted;
            }
        }

        // post-translation
        public string translatedText { get; private set; }
        public string detectedLocale { get; private set; }
        public string timeStamp { get; private set; }

        private async Task DoTranslation()
        {
            Stopwatch sw = new Stopwatch();

            // Make our connection client
            TranslationServiceClient translationServiceClient = new TranslationServiceClientBuilder
            {
                CredentialsPath = Properties.Settings.Default.apiKeyPath,
            }.Build();

            // Request translation
            TranslateTextRequest request = new TranslateTextRequest
            {
                Contents = {rawText },
                TargetLanguageCode = Properties.Settings.Default.targetLocale,
                ParentAsLocationName = new LocationName(Properties.Settings.Default.projectName, "global"),
            };

            // Send request
            sw.Start();
            TranslateTextResponse response = await translationServiceClient.TranslateTextAsync(request);
            sw.Stop();

            // Really only anticipating a single result here
            Translation tr = response.Translations.First();
            translatedText = WebUtility.HtmlDecode(tr.TranslatedText);
            detectedLocale = tr.DetectedLanguageCode;

            timeStamp = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                sw.Elapsed.Hours,
                sw.Elapsed.Minutes,
                sw.Elapsed.Seconds,
                sw.Elapsed.Milliseconds);

            callback?.Invoke(this);
        }
    }
}
