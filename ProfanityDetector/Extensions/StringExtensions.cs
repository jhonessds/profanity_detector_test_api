using System.Collections.Generic;

namespace ProfanityDetector.Extensions
{
    public static class StringExtensions
    {
        public static string LeetDecode(this string word)
        {
            word = word.ToLower();
            Dictionary<string, string> leetRules = new Dictionary<string, string>();

            leetRules.Add("4", "a");
            leetRules.Add(@"/\", "a");
            leetRules.Add(@"/-\", "a");
            leetRules.Add("@", "a");
            leetRules.Add("^", "a");

            leetRules.Add("13", "b");
            leetRules.Add("/3", "b");
            leetRules.Add("|3", "b");
            leetRules.Add("8", "b");
            leetRules.Add("ß", "b");
            leetRules.Add("|:", "b");
            leetRules.Add("P>", "b");

            leetRules.Add("<", "c");
            leetRules.Add("(", "c");
            leetRules.Add("[", "c");

            leetRules.Add("|)", "d");
            leetRules.Add("|))", "d");
            leetRules.Add("[)", "d");
            leetRules.Add("|>", "d");
            leetRules.Add("?", "d");

            leetRules.Add("3", "e");
            leetRules.Add("£", "e");
            leetRules.Add("€", "e");
            leetRules.Add("[-", "e");
            leetRules.Add("|=-", "e");
            leetRules.Add("&", "e");

            leetRules.Add("]]=", "f");
            leetRules.Add("|=", "f");
            leetRules.Add("|#", "f");

            leetRules.Add("6", "g");

            leetRules.Add("/-/", "h");
            leetRules.Add("[-]", "h");
            leetRules.Add("]-[", "h");
            leetRules.Add("#", "h");
            leetRules.Add("{=}", "h");
            leetRules.Add("<~>", "h");

            leetRules.Add("!", "i");


            leetRules.Add("_/", "j");
            leetRules.Add("_|", "j");
            leetRules.Add(",|", "j");

            leetRules.Add("]{", "k");

            leetRules.Add("1", "l");
            leetRules.Add("|_", "l");
            leetRules.Add("||_", "l");

            leetRules.Add("(v)", "m");
            leetRules.Add(@"|\/|", "m");

            leetRules.Add(@"(\)", "n");
            leetRules.Add(@"|\|", "n");

            leetRules.Add("0", "o");
            leetRules.Add("()", "o");

            leetRules.Add("|]D", "p");
            leetRules.Add("]D", "p");
            leetRules.Add("|D", "p");

            leetRules.Add("(,)", "q");

            leetRules.Add("1²", "r");

            leetRules.Add("5", "s");
            leetRules.Add("$", "s");

            leetRules.Add("7", "t");
            leetRules.Add("']'", "t");
            leetRules.Add("'|'", "t");

            leetRules.Add("|_|", "u");

            leetRules.Add(@"\/", "v");

            leetRules.Add(@"\/\/", "w");
            leetRules.Add(@"'//", "w");

            leetRules.Add("><", "x");
            leetRules.Add("%", "x");

            leetRules.Add("'/", "y");

            leetRules.Add("2", "z");
            leetRules.Add("\"/_", "z");
            leetRules.Add(@"¯\_", "z");

            foreach (KeyValuePair<string, string> x in leetRules)
            {
                word = word.Replace(x.Key, x.Value);
            }
            return word;
        }
    }
}