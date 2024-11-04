using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordyAppConsole
{
    public interface IWordy
    {
        public string GuestWord();
        public void GiveUp(string randomWord);
        public bool CheckingWord(string word, string randomWord);
    }
}
