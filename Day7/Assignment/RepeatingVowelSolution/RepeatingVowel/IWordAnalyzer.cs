using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepeatingVowel
{
    internal interface IWordAnalyzer
    {
        (int Count, List<string> Words) FindWordsWithLeastRepeatingVowels();
    }
}
