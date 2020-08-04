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
    }

    public class AsyncOCR
    {
        // pre-OCR
        public SImage image { get; private set; }

        public AsyncOCR(SImage image)
        {
            this.image = image;
            task = Task.Run(DoOCR);
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
        }
    }

    public class AsyncTranslation
    {
        // pre-translation
        public string rawText { get; private set; }

        public AsyncTranslation(string text)
        {
            rawText = text;
            task = Task.Run(DoTranslation);
        }

        public event Action<string> callback;

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

        private static int counter = 0;

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
            translatedText = WebUtility.HtmlDecode(tr.TranslatedText) + "[" + counter + "]";
            counter += 1;
            detectedLocale = tr.DetectedLanguageCode;

            timeStamp = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                sw.Elapsed.Hours,
                sw.Elapsed.Minutes,
                sw.Elapsed.Seconds,
                sw.Elapsed.Milliseconds);

            callback?.Invoke(translatedText);
        }
    }

    public static class GoogleAsync
    {
    }
}
