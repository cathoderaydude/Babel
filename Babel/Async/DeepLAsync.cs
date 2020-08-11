using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Babel.Async.DeepLImpl
{
    public class AsyncOCR : IAsyncOCR
    {
        // pre-OCR
        public Image image { get; private set; }
        private OCRCallback callback;

        public AsyncOCR(Image image, OCRCallback callback = null)
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

                // No keyfile to check - maybe somehow validate the API key?

                // Wait for rate limiter before starting the clock
                AsyncStatic.rate.Check();
                Stopwatch sw = new Stopwatch();

                // Dump the provided image to a memory stream
                var ms = new MemoryStream();
                image.Save(ms, ImageFormat.Png);

                // Dump the stream out to a byte array
                byte[] byteData = ms.ToArray();

                // Make our connection client
                HttpClient client = new HttpClient();

                // Build the OCR request
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(Properties.Settings.Default.microsoftOcrEndpoint + "vision/v2.1/ocr");
                request.Method = HttpMethod.Post;
                request.Headers.Add(
                    "Ocp-Apim-Subscription-Key",
                    Properties.Settings.Default.microsoftOcrApiKey);
                request.Content = new ByteArrayContent(byteData);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Ask for OCR
                sw.Start();
                HttpResponseMessage response = await client.SendAsync(request);
                var json = JToken.Parse(await response.Content.ReadAsStringAsync());
                sw.Stop();

                // MS data does not contain a meaningful bigbox, just fill something in here
                _bigBox = new OCRBox(
                    (string)json["regions"][0]["boundingBox"],
                    "DeepL doesn't provide this data.");

                // MS smallbox data, meanwhile, is organized in interesting ways
                _smallBoxes = json["regions"]
                    .SelectMany(region => region["lines"])
                    .SelectMany(line => line["words"])
                    .Select(word => new OCRBox((string)word["boundingBox"], (string)word["text"]))
                    .ToArray();

                _timeStamp = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    sw.Elapsed.Hours,
                    sw.Elapsed.Minutes,
                    sw.Elapsed.Seconds,
                    sw.Elapsed.Milliseconds);

                isDone = true;
                callback?.Invoke(this);

                DebugLog.Log("Finished OCR request [" + Identifer + "]");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
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

        // boy I hate this a lot
        private object WrapText(string text) => new { Text = text };

        private async Task DoTranslation()
        {
            try
            {
                string Identifer = Utility.RandomHex();
                DebugLog.Log("Making DeepL translation request [" + Identifer + "]: " + this.rawText);

                // No keyfile to check - maybe somehow validate the API key?

                // Wait for rate limiter before starting the clock
                AsyncStatic.rate.Check();
                Stopwatch sw = new Stopwatch();

                // Make our connection client
                HttpClient client = new HttpClient();

                // Laboriously hand-package our translation request strings into JSON
                string[] splitters = new string[] { Environment.NewLine };
                string[] body = new string[]
                {
                    "auth_key=" + Properties.Settings.Default.DeepLKey,
                    "text=" + rawText,
                    "target_lang=EN"
                };

                // Build the translation request
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://api.deepl.com/v2/translate");
                request.Method = HttpMethod.Post;
                request.Content = new StringContent(String.Join("&", body), Encoding.UTF8, "application/json");
                DebugLog.Log(String.Join("&", body));

                // Send request
                sw.Start();
                HttpResponseMessage response = await client.SendAsync(request);
                var json = JToken.Parse(await response.Content.ReadAsStringAsync());
                sw.Stop();

                // Anticipating one result per submitted line, in same order
                _translatedText = json
                    .Select(x => (string)x["translations"][0]["text"]).ToString();

                // Close enough
                _detectedLocale = (string)json.First()["translations"][0]["detected_source_language"];

                _timeStamp = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    sw.Elapsed.Hours,
                    sw.Elapsed.Minutes,
                    sw.Elapsed.Seconds,
                    sw.Elapsed.Milliseconds);

                isDone = true;
                callback?.Invoke(this);

                DebugLog.Log("Finishing DeepL translation [" + Identifer + "]: " + this._translatedText);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
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

        private LanguageItem ConvertLanguage(string name, JToken lang) => new LanguageItem(name, lang);

        private async Task DoGSL()
        {
            try
            {
                string Identifer = Utility.RandomHex();
                DebugLog.Log("Making get supported languages request [" + Identifer + "]");

                // No keyfile to check - maybe somehow validate the API key?

                // Wait for rate limiter before starting the clock
                AsyncStatic.rate.Check();
                Stopwatch sw = new Stopwatch();

                // Make our connection client
                HttpClient client = new HttpClient();

                // Build the GSL request
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://api.cognitive.microsofttranslator.com/languages?api-version=3.0");
                request.Method = HttpMethod.Get;

                // Send request
                sw.Start();
                HttpResponseMessage response = await client.SendAsync(request);
                var json = JToken.Parse(await response.Content.ReadAsStringAsync());
                sw.Stop();

                // Convert these to a format the combo box will like
                _languages = json["translation"]
                    .Children<JProperty>()
                    .Select(child => child.Name)
                    .Select(name => ConvertLanguage(name, json["translation"][name]))
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
