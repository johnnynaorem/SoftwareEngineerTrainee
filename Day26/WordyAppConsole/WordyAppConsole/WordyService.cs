namespace WordyAppConsole
{
    public class WordyService : IWordy
    {
        static List<string> lists = new List<string>()
        {
            "Wordy", "LIGHT", "ROUGE", "READY", "PEARS", "CHIEF", "TOUCH",
            "Apple", "Brick", "Cloud", "Dance", "Flame", "Grasp", "Quest", "Quick",
            "Share"
        };

        private void ControlConsoleColor(string msg, ConsoleColor bgC, ConsoleColor fgC)
        {
            Console.BackgroundColor = bgC;
            Console.ForegroundColor = fgC;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        public bool CheckingWord(string word, string randomWord)
        {
            List<char> correctPositionLetter = new List<char>();
            List<char> inCorrectPositionLetter = new List<char>();


            if (word.Length != randomWord.Length)
            {
                Console.WriteLine("Wrong length! Try again.");
                return false;
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (word == randomWord)
                {
                    ControlConsoleColor("Wow.... Correct", ConsoleColor.Green, ConsoleColor.Black);
                    return true;
                }

                if (word[i] == randomWord[i])
                {
                    correctPositionLetter.Add(word[i]);
                }
                else
                {
                    for (int j = 0; j < randomWord.Length; j++)
                    {
                        if (word[i] == randomWord[j] && !correctPositionLetter.Contains(word[i]))
                        {
                            inCorrectPositionLetter.Add(word[i]);
                        }
                    }
                }
            }
            if (correctPositionLetter.Count > 0)
            {
                string result = String.Concat(correctPositionLetter);
                ControlConsoleColor($"Correct Position Letters: {result}", ConsoleColor.Black, ConsoleColor.Green);
            }

            if (inCorrectPositionLetter.Count > 0)
            {
                string inCorrectWord = String.Concat(inCorrectPositionLetter);
                ControlConsoleColor($"Incorrect Position Letters: {inCorrectWord}", ConsoleColor.Black, ConsoleColor.Blue);
            }

            if (correctPositionLetter.Count == 0 && inCorrectPositionLetter.Count == 0)
            {
                ControlConsoleColor("NO WORD FOUND", ConsoleColor.Black, ConsoleColor.Red);
            }

            return false;
        }
        public void GiveUp(string randomWord)
        {
            ControlConsoleColor($"The correct word was: {randomWord.ToUpper()}", ConsoleColor.Red, ConsoleColor.Black);
        }

        public string GuestWord()
        {
            Random random = new Random();
            int index = random.Next(lists.Count);
            string randomWord = lists[index];
            Console.WriteLine($"Guess the word: {randomWord.Length} letters.");
            return randomWord;
        }
    }
}
