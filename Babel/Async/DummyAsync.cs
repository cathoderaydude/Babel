namespace Babel.Async.DummyImpl
{
    public class AsyncOCR : IAsyncOCR
    {
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
        public AsyncGSL(GSLCallback callback)
        {
            callback?.Invoke(this);
        }

        public bool isDone => true;

        public LanguageItem[] languages => LanguageItem.DummyLanguages();
        public string timeStamp => "[dummy]";
    }
}
