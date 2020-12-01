namespace ProfanityDetector
{
    public class BadWord
    {
        public BadWord(string word, string language)
        {
            Word = word;
            Language = language;
        }

        public string Word { get; set; }
        public string Language { get; set; }
    }
}