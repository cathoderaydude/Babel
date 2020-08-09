﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Threading;

using Google.Cloud.Vision.V1;
using Google.Api.Gax.ResourceNames;
using Google.Cloud.Translate.V3;

using SImage = System.Drawing.Image;
using GImage = Google.Cloud.Vision.V1.Image;

namespace Babel.Google
{
    public class OCRBox
    {
        public Point[] points;
        public Rectangle rect => Utility.FitRect(points);
        public string text;

        public OCRBox(EntityAnnotation ann)
        {
            text = ann.Description;
            points = ann.BoundingPoly.Vertices.Select(Utility.ToPoint).ToArray();
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
            if ((idx < 0)|| (idx >= gridSize * gridSize)) throw new ArgumentOutOfRangeException();

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

    public class AsyncOCR
    {
        // pre-OCR
        public SImage image { get; private set; }
        private event Action<AsyncOCR> callback;
        public frmBabel Form;

        public AsyncOCR(SImage image, frmBabel Form, Action<AsyncOCR> callback = null)
        {
            this.image = image.Copy();
            this.Form = Form;
            //Form.Invoke(Form.SafeLogWorkerError, new object[] { "Unable to horble dorble blorb", "http://www.yahoo" });

            if (Properties.Settings.Default.dummyData)
            {
                _bigBox = OCRBox.DummyBigBox();
                _smallBoxes = OCRBox.DummySmallBoxes();
                _timeStamp = "[dummy]";
                isDone = true;
                callback?.Invoke(this);
            }
            else
            {
                this.callback += callback;
                task = Task.Run(DoOCR);
            }
        }

        // do the OCR
        Task task;
        public bool isDone { get; private set; }

        // post-OCR
        private OCRBox _bigBox;
        public OCRBox bigBox => isDone ? _bigBox : null;
        private OCRBox[] _smallBoxes;
        public OCRBox[] smallBoxes => isDone ? _smallBoxes : new OCRBox[] { };
        private string _timeStamp;
        public string timeStamp => isDone ? _timeStamp : "";

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

            // If we didn't get anything back
            if (response.Count == 0)
            {
                _bigBox = OCRBox.ErrorBigBox();
                _smallBoxes = new OCRBox[] { };
            }
            else
            {
                // First result is the big box
                _bigBox = new OCRBox(response.First());

                // Following results are the small boxes
                _smallBoxes = response.Skip(1)
                    .Select(ann => new OCRBox(ann))
                    .ToArray();
            }

            _timeStamp = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                sw.Elapsed.Hours,
                sw.Elapsed.Minutes,
                sw.Elapsed.Seconds,
                sw.Elapsed.Milliseconds);

            isDone = true;
            //Form.Invoke(Form.SafeAsyncOCR_Callback, this);
            callback?.Invoke(this);
        }
    }

    public class AsyncTranslation
    {
        // pre-translation
        public string rawText { get; private set; }
        private event Action<AsyncTranslation> callback;
        public frmBabel Form;

        public AsyncTranslation(string text, frmBabel Form, Action<AsyncTranslation> callback = null)
        {
            rawText = text;
            this.Form = Form;

            if (Properties.Settings.Default.dummyData)
            {
                _translatedText = rawText;
                _detectedLocale = Properties.Settings.Default.targetLocale;
                _timeStamp = "[dummy]";
                isDone = true;
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
        public bool isDone { get; private set; }

        // post-translation
        private string _translatedText;
        public string translatedText => isDone ? _translatedText : "";
        private string _detectedLocale;
        public string detectedLocale => isDone ? _detectedLocale : "";
        private string _timeStamp;
        public string timeStamp => isDone ? _timeStamp : "";

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
            _translatedText = WebUtility.HtmlDecode(tr.TranslatedText);
            _detectedLocale = tr.DetectedLanguageCode;

            _timeStamp = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                sw.Elapsed.Hours,
                sw.Elapsed.Minutes,
                sw.Elapsed.Seconds,
                sw.Elapsed.Milliseconds);

            isDone = true;
            callback?.Invoke(this);
        }
    }
}
