using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestShortestProject
{
    internal class WordManager : IWordInterface
    {
        string[] words;
        public WordManager( string input) {
            words = input.Split(',').Select(word => word.Trim()).ToArray();
        }
        public void LongestWord()
        {
            string result = words.OrderByDescending(word => word.Length).FirstOrDefault();
            Console.WriteLine($"Longest Word: {result}");
        }

        public void ShortestWord()
        {
            string result = words.OrderBy(word => word.Length).FirstOrDefault();
            Console.WriteLine($"Shortest Word: {result}");
        }
    }
}
