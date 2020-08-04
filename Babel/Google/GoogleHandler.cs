using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;
using System.Net;
using Google.Cloud.Vision.V1;
using Google.Cloud.Translate.V3;
using Google.Api.Gax.ResourceNames;

// Solving a name collision
using SImage = System.Drawing.Image;
using GImage = Google.Cloud.Vision.V1.Image;
using System.Threading;
using System.Diagnostics;

namespace Babel.Google
{
    static class Extensions
    {
        public static Point ToPoint(this Vertex v) => new Point(v.X, v.Y);
        public static IEnumerable<Point> ToPoints(this IEnumerable<Vertex> vs) => vs.Select(ToPoint);
    }

    public class OCRResult
    {
        public string text;
        public string locale;
        public string translatedText;
        public Point[] poly;
        public bool selected;
        public string translationTime;
        public OCRResult() { }
        public OCRResult(string text, string locale, Point[] poly)
        {
            this.text = text; this.locale = locale; this.poly = poly;
        }
    }

    public class TranslationResult
    {
        public string text;
        public string locale;
        public TranslationResult() { }
        public TranslationResult(string text, string locale)
        {
            this.text = text;
            this.locale = locale; 
        }
    }

    public class GoogleHandler
    {
        private static OCRResult InsertTranslation(OCRResult o, TranslationResult t)
        {
            return new OCRResult
            {
                text = o.text,
                poly = o.poly,
                translatedText = t.text,
                locale = t.locale,
            };
        }

        public static IEnumerable<OCRResult> RecognizeImage(SImage target)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var ocrs = OCRImage(target).ToArray();
            stopwatch.Stop();
            string ocrTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                stopwatch.Elapsed.Hours,
                stopwatch.Elapsed.Minutes,
                stopwatch.Elapsed.Seconds,
                stopwatch.Elapsed.Milliseconds);
            stopwatch.Reset();

            return ocrs.Skip(1);
        }

        public static IEnumerable<OCRResult> TranslateImage(SImage target)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var ocrs = OCRImage(target).ToArray();
            stopwatch.Stop();
            string ocrTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                stopwatch.Elapsed.Hours,
                stopwatch.Elapsed.Minutes,
                stopwatch.Elapsed.Seconds,
                stopwatch.Elapsed.Milliseconds);
            stopwatch.Reset();

            if (ocrs.Count() > 0)
            {
                var sourceStrings = ocrs.Select(x => x.text);

                stopwatch.Start();
                var translations = Translate(sourceStrings, ocrs.First().locale).ToArray();
                stopwatch.Stop();
                string translateTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    stopwatch.Elapsed.Hours,
                    stopwatch.Elapsed.Minutes,
                    stopwatch.Elapsed.Seconds,
                    stopwatch.Elapsed.Milliseconds);
                stopwatch.Reset();

                stopwatch.Start();
                ocrs = ocrs.Zip(translations, InsertTranslation).ToArray();
                stopwatch.Stop();
                string zipTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    stopwatch.Elapsed.Hours,
                    stopwatch.Elapsed.Minutes,
                    stopwatch.Elapsed.Seconds,
                    stopwatch.Elapsed.Milliseconds);
                stopwatch.Reset();
            }

            return ocrs;
        }

        public static IEnumerable<OCRResult> TranslateImageAsync(SImage target)
        {
            var ocrs = OCRImage(target);

            if (ocrs.Count() > 0)
            {
                var sourceStrings = ocrs.Select(x => x.text);

                var translations = Translate(sourceStrings, ocrs.First().locale);

                ocrs = ocrs.Zip(translations, InsertTranslation);
            }

            return ocrs;
        }

        public static IEnumerable<OCRResult> OCRImage(SImage target)
        {
            // Dump the provided image to a memory stream
            var stream = new MemoryStream();
            target.Save(stream, ImageFormat.Png);
            stream.Position = 0;

            // Load the stream as a gimage
            GImage gimage = GImage.FromStream(stream);

            // Make our connection client
            ImageAnnotatorClient client = new ImageAnnotatorClientBuilder
            {
                CredentialsPath = Properties.Settings.Default.apiKeyPath,
            }.Build();

            // Ask for OCR
            var response = client.DetectText(gimage);

            // Could do this with really arcane linq, but foreach yield is easier to read
            foreach (EntityAnnotation ann in response)
            {
                yield return new OCRResult
                {
                    text = ann.Description,
                    locale = ann.Locale,
                    poly = ann.BoundingPoly.Vertices.ToPoints().ToArray(),
                };
            }
        }

        public static IEnumerable<TranslationResult> Translate(IEnumerable<string> inputs, string sourceLocale = "")
        {
            if (sourceLocale != Properties.Settings.Default.targetLocale)
            {
                // Make our connection client
                TranslationServiceClient translationServiceClient = new TranslationServiceClientBuilder
                {
                    CredentialsPath = Properties.Settings.Default.apiKeyPath,
                }.Build();

                // Request translation to english
                TranslateTextRequest request = new TranslateTextRequest
                {
                    SourceLanguageCode = sourceLocale,
                    TargetLanguageCode = Properties.Settings.Default.targetLocale,
                    ParentAsLocationName = new LocationName(Properties.Settings.Default.projectName, "global"),
                };

                // Insert inputs - doesn't work well with constructor above
                request.Contents.Add(inputs);

                // Request translation
                TranslateTextResponse response = translationServiceClient.TranslateText(request);

                // Could do this with really arcane linq, but foreach yield is easier to read
                foreach (Translation tr in response.Translations)
                {
                    yield return new TranslationResult
                    {
                        text = WebUtility.HtmlDecode(tr.TranslatedText),
                        locale = tr.DetectedLanguageCode,
                    };
                }
            }
        }
    }
}
