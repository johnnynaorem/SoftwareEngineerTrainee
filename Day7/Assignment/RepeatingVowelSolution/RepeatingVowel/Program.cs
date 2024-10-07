namespace RepeatingVowel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            try
            {
                input = WordInput.GetInputFromConsole();
                IWordAnalyzer wordAnalyzer = new WordHandler(input);


                var result = wordAnalyzer.FindWordsWithLeastRepeatingVowels();
                int count = result.Count;
                List<string> words = result.Words;


                Console.WriteLine($"Number of repeating vowels: {count}");
                Console.WriteLine("Words with the least number of repeating vowels:");
                foreach (var word in words)
                {
                    Console.WriteLine(word);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
