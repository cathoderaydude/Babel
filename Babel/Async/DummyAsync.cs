namespace Babel.Async.DummyImpl
{
    public class AsyncOCR : IAsyncOCR
    {
        string IAsync.name => "Dummy";

        public AsyncOCR(OCRCallback callback)
        {
            callback?.Invoke(this);
        }

        public bool isDone => true;

        public OCRBox bigBox => OCRBox.DummyBigBox();
        public OCRBox[] smallBoxes => OCRBox.DummySmallBoxes();
        public string timeStamp => "[dummy]";
    }

    public class AsyncTranslation : IAsyncTranslation
    {
        string IAsync.name => "Dummy";

        public AsyncTranslation(string text, TranslationCallback callback)
        {
            rawText = text;
            callback?.Invoke(this);
        }

        public bool isDone => true;

        public string rawText { get; private set; }
        public string translatedText => rawText + " (dummy)";
        public string timeStamp => "[dummy]";
    }

    public class AsyncGSL : IAsyncGSL
    {
        string IAsync.name => "Dummy";

        public AsyncGSL(GSLCallback callback)
        {
            callback?.Invoke(this);
        }

        public bool isDone => true;

        public LanguageItem[] languages => LanguageItem.DummyLanguages();
        public string timeStamp => "[dummy]";
    }

    public class AsyncTTS : IAsyncTTS
    {
        string IAsync.name => "Dummy";

        public AsyncTTS(TTSCallback callback)
        {
            callback?.Invoke(this);
        }

        public bool isDone => true;
        public string text { get; private set; }
        public byte[] audioData => new byte[0];
        public string timeStamp => "[dummy]";

    }
}
