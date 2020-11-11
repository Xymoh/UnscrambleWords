using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnscrambleWords
{
    class Constants
    {
        public const string ErrorScrambledWordsCannotBeLoaded = "Scrambled words were not loaded because there was an error: ";
        public const string ErrorProgramWillBeTerminated = "The program will be terminated: ";

        public const string MatchFound = "MATCH FOUND FOR: ";
        public const string MatchNotFound = "NO MATCHES HAVE BEEN FOUND";

        public const string WordListFileName = "wordlist.txt";
        public const string TxtFilter = "Text Document (.txt)|*.txt";
        public const string FileExt = ".txt";

        public const string FileDestination = "File destination: ";
    }
}

