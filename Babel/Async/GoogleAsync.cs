using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Google.Cloud.Vision.V1;
using Google.Api.Gax.ResourceNames;
using Google.Cloud.Translate.V3;

using SImage = System.Drawing.Image;
using GImage = Google.Cloud.Vision.V1.Image;

using System.Text.RegularExpressions;

namespace Babel.Async.GoogleImpl
{
    public class AsyncOCR : IAsyncOCR
    {
        // pre-OCR
        public SImage image { get; private set; }
        private OCRCallback callback;

        public AsyncOCR(SImage image, OCRCallback callback = null)
        {
            this.image = image.Copy();
            this.callback = callback;

            if (image == null)
            {
                _bigBox = null;
                _smallBoxes = new OCRBox[0];
                _timeStamp = "[empty]";
                isDone = true;
                this.callback?.Invoke(this);
            }
            else
            {
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
            try
            {
                string Identifer = Utility.RandomHex();
                DebugLog.Log("Making OCR request [" + Identifer + "]");

                if (!File.Exists(Properties.Settings.Default.googleApiKeyPath))
                    throw new FileNotFoundException("Keyfile not present at " + Properties.Settings.Default.googleApiKeyPath);

                // Wait for rate limiter before starting the clock
                AsyncStatic.rate.Check();
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
                    CredentialsPath = Properties.Settings.Default.googleApiKeyPath,
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
                callback?.Invoke(this);

                DebugLog.Log("Finished OCR request [" + Identifer + "]");
            }
            catch (Grpc.Core.RpcException e)
            {
                string url = "";

                // Define a regular expression for repeated words.
                Regex rx = new Regex(@"(http\S*)",
                  RegexOptions.Compiled | RegexOptions.IgnoreCase);

                // Find matches.
                MatchCollection matches = rx.Matches(e.Message);

                if(matches.Count > 0)
                {
                    url = matches[0].Groups[0].Value;
                }

                frmBabel.LogWorkerError(e.Message, url);
            } catch (Exception e)
            {
                frmBabel.LogWorkerError(e.Message, "");
            }
        }
    }

    public class AsyncTranslation : IAsyncTranslation
    {
        // pre-translation
        public string rawText { get; private set; }
        private TranslationCallback callback;

        public AsyncTranslation(string text, TranslationCallback callback = null)
        {
            rawText = text;
            this.callback = callback;

            if (text == null || text == "")
            {
                _translatedText = "";
                _detectedLocale = Properties.Settings.Default.targetLocale;
                _timeStamp = "[empty]";
                isDone = true;
                this.callback?.Invoke(this);
            }
            else
            {
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
            try
            {
                string Identifer = Utility.RandomHex();
                DebugLog.Log("Making translation request ["+Identifer+"]: " + this.rawText);

                if (!File.Exists(Properties.Settings.Default.googleApiKeyPath))
                    throw new FileNotFoundException("Keyfile not present at " + Properties.Settings.Default.googleApiKeyPath);

                // Wait for rate limiter before starting the clock
                AsyncStatic.rate.Check();
                Stopwatch sw = new Stopwatch();

                // Make our connection client
                TranslationServiceClient translationServiceClient = new TranslationServiceClientBuilder
                {
                    CredentialsPath = Properties.Settings.Default.googleApiKeyPath,
                }.Build();

                // Build the translation request
                TranslateTextRequest request = new TranslateTextRequest
                {
                    //Contents = { rawText },
                    TargetLanguageCode = Properties.Settings.Default.targetLocale,
                    ParentAsLocationName = new LocationName(Properties.Settings.Default.googleProjectName, "global"),
                };

                // It does not appear that there's any way initialize the Contents above directly with a split of strings
                string[] splitters = new string[] { Environment.NewLine };
                request.Contents.AddRange(rawText.Split(splitters, StringSplitOptions.RemoveEmptyEntries));

                // Send request
                sw.Start();
                TranslateTextResponse response = await translationServiceClient.TranslateTextAsync(request);
                sw.Stop();

                // Anticipating one result per submitted line, in same order
                _translatedText = response.Translations
                    .Select(tr => WebUtility.HtmlDecode(tr.TranslatedText))
                    .Aggregate((l, r) => l + Environment.NewLine + r);

                // Close enough
                _detectedLocale = response.Translations.First().DetectedLanguageCode;

                _timeStamp = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    sw.Elapsed.Hours,
                    sw.Elapsed.Minutes,
                    sw.Elapsed.Seconds,
                    sw.Elapsed.Milliseconds);

                isDone = true;
                callback?.Invoke(this);

                DebugLog.Log("Finishing translation ["+Identifer+"]: " + this._translatedText);
            }
            catch(Grpc.Core.RpcException e)
            {
                frmBabel.LogWorkerError(e.Message, "");
            }
            catch (Exception e)
            {
                frmBabel.LogWorkerError(e.Message, "");
            }
        }
    }

    public class AsyncGSL : IAsyncGSL
    {
        // pre-GSL
        private GSLCallback callback;

        public AsyncGSL(GSLCallback callback)
        {
            this.callback = callback;
            task = Task.Run(DoGSL);
        }

        // do the request
        private Task task;
        public bool isDone { get; private set; }

        // post-GSL
        private LanguageItem[] _languages;
        public LanguageItem[] languages => isDone ? _languages : new LanguageItem[0];
        private string _timeStamp;
        public string timeStamp => isDone ? _timeStamp : "";

        private LanguageItem ConvertLanguage(SupportedLanguage lang) => new LanguageItem(lang);

        private async Task DoGSL()
        {
            try
            {
                string Identifer = Utility.RandomHex();
                DebugLog.Log("Making get supported languages request [" + Identifer + "]");

                if (!File.Exists(Properties.Settings.Default.googleApiKeyPath))
                    throw new FileNotFoundException("Keyfile not present at " + Properties.Settings.Default.googleApiKeyPath);

                // Wait for rate limiter before starting the clock
                AsyncStatic.rate.Check();
                Stopwatch sw = new Stopwatch();

                // Make our connection client
                TranslationServiceClient translationServiceClient = new TranslationServiceClientBuilder
                {
                    CredentialsPath = Properties.Settings.Default.googleApiKeyPath,
                }.Build();

                // Request supported languages
                GetSupportedLanguagesRequest request = new GetSupportedLanguagesRequest
                {
                    DisplayLanguageCode = "en",
                    ParentAsLocationName = new LocationName(Properties.Settings.Default.googleProjectName, "global"),
                };

                // Send request
                sw.Start();
                SupportedLanguages response = await translationServiceClient.GetSupportedLanguagesAsync(request);
                sw.Stop();

                // Convert these to a format the combo box will like
                _languages = response.Languages
                    .Where(lang => lang.SupportTarget)
                    .Select(ConvertLanguage)
                    .ToArray();

                _timeStamp = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    sw.Elapsed.Hours,
                    sw.Elapsed.Minutes,
                    sw.Elapsed.Seconds,
                    sw.Elapsed.Milliseconds);

                isDone = true;
                callback?.Invoke(this);

                DebugLog.Log("Finishing getting supported languages [" + Identifer + "]");
            }
            catch (Grpc.Core.RpcException e)
            {
                frmBabel.LogWorkerError(e.Message, "");
            }
            catch (Exception e)
            {
                frmBabel.LogWorkerError(e.Message, "");
            }
        }
    }
}
