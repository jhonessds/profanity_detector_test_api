using System;

namespace ProfanityDetector
{
    public class ProfanityLog
    {
        public ProfanityLog(string term, bool isProfanity, string method)
        {
            Term = term;
            IsProfanity = isProfanity;
            Date = DateTime.UtcNow;
            Method = method;
        }

        public string Term { get; set; }
        public bool IsProfanity { get; set; }
        public string Method { get; set; }
        public DateTime Date { get; set; }
    }
}