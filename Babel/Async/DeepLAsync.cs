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
using System.Web;
using System.Collections.Generic;

namespace Babel.Async.DeepLImpl
{
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

        string IAsyncTranslation.name => "DeepL";

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

                // Laboriously hand-package our translation request strings into a POST body
                Dictionary<string, string> fields = new Dictionary<string, string>();
                fields["auth_key"] = Properties.Settings.Default.DeepLKey;
                fields["text"] = rawText;
                fields["target_lang"] = "EN";

                // Build the translation request
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://api.deepl.com/v2/translate");
                request.Method = HttpMethod.Post;
                request.Content = new FormUrlEncodedContent(fields);

                // Send request
                sw.Start();
                HttpResponseMessage response = await client.SendAsync(request);
                var json = JToken.Parse(await response.Content.ReadAsStringAsync());
                sw.Stop();

                // Anticipating one result per submitted line, in same order
                _translatedText = json["translations"][0]["text"].ToString();

                // Close enough
                _detectedLocale = json["translations"][0]["detected_source_language"].ToString();

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
        
        // Obviously this will be an async method when it has code in it
        private async Task DoGSL()
        {
            try
            {
                throw new NotImplementedException("DeepL doesn't have a working GSL request yet.");
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
