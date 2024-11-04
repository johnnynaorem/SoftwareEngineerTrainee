using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordyAppConsole
{
    public class UserInteraction 
    {
        IWordy wordy;
        HashSet<string> dictionary = LoadDictionary("dictionary.txt");

        static HashSet<string> LoadDictionary(string filePath)
        {
            HashSet<string> words = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                foreach (var line in File.ReadLines(filePath))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        words.Add(line.Trim());
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred while loading the dictionary: {e.Message}");
            }

            return words;
        }

        static bool IsValidWord(string word, HashSet<string> dictionary)
        {
            return dictionary.Contains(word.ToLower());
        }
        public UserInteraction() { 
           wordy = new WordyService();
        }

        private void ControlConsoleColor(string msg,ConsoleColor bgC, ConsoleColor fgC)
        {
            Console.BackgroundColor = bgC;
            Console.ForegroundColor = fgC;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        public void MainInteraction()
        {
            string randomWord = wordy.GuestWord();
            int count = 0;
            while (count < 6)
            {
                ControlConsoleColor($"Chance Left: {6 - count}", ConsoleColor.Green, ConsoleColor.White);
                Console.Write("Enter the word (or type 'GIVE UP' to reveal the answer): ");
                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "GIVE UP":
                        wordy.GiveUp(randomWord);
                        return;
                    default:
                        if (IsValidWord(input, dictionary))
                        {
                            if (wordy.CheckingWord(input.ToLower(), randomWord.ToLower()))
                            {
                                return;
                            }
                            count++;
                        }
                        else
                        {
                            Console.WriteLine($"{input}' is not a valid English word.");
                        }
    
                        break;
                }
            }
            ControlConsoleColor($"Oops, your chances are finished. Try Again.... {randomWord} is the correct word", ConsoleColor.Magenta, ConsoleColor.Black);
        }
    }
}
