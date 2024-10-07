namespace LongestShortestProject
{
    internal class Program
    {
        Word word = new Word();
        IWordInterface wordManger;

        public Program() {
            word.TakeInputFromConsole();
            wordManger = new WordManager(word.input);
        }


        static void Main(string[] args)
        {
            var program = new Program();
            program.wordManger.ShortestWord();
            program.wordManger.LongestWord();
        }
    }
}
