using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepeatingVowel
{
    internal class WordHandler : IWordAnalyzer
    {
        string[] words;

        public WordHandler(string input)
        {
            words = input.Split(',').Select(word => word.Trim()).ToArray();
        }

        private int CountRepeatingVowels(string word)
        {
            int count = 0;
            string vowels = "aeiou";
            var vowelCounts = new int[5];
            foreach (char c in word)
            {
                switch (c)
                {
                    case 'a': vowelCounts[0]++; break;
                    case 'e': vowelCounts[1]++; break;
                    case 'i': vowelCounts[2]++; break;
                    case 'o': vowelCounts[3]++; break;
                    case 'u': vowelCounts[4]++; break;
                }
            }
            count = vowelCounts.Where(v => v > 1).Sum(v => v - 1);
            return count;
        }
        public (int Count, List<string> Words) FindWordsWithLeastRepeatingVowels()
        {
            var wordRepeats = new List<Tuple<string, int>>();

            foreach (var word in words)
            {
                int count = CountRepeatingVowels(word);
                wordRepeats.Add(Tuple.Create(word, count));
            }

            if (wordRepeats.Count == 0)
            {
                return (0, new List<string>());
            }

            int minRepeatingVowels = wordRepeats.Min(wr => wr.Item2);
            var leastRepeatingWords = wordRepeats
                .Where(wr => wr.Item2 == minRepeatingVowels)
                .Select(wr => wr.Item1)
                .ToList();

            return (minRepeatingVowels, leastRepeatingWords);
        }
    }
}
