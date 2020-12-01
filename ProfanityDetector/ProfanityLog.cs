using System;

namespace ProfanityDetector
{
    public class ProfanityLog
    {
        public ProfanityLog(string term, bool isProfanity, string method, string termWithoutLeet)
        {
            Term = term;
            IsProfanity = isProfanity;
            Date = DateTime.UtcNow;
            Method = method;
            TermWithoutLeet = termWithoutLeet;
        }

        public string Term { get; set; }
        public string TermWithoutLeet { get; set; }
        public bool IsProfanity { get; set; }
        public string Method { get; set; }
        public DateTime Date { get; set; }
    }
}