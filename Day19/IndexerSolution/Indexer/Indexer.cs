using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexer
{
    internal class Indexer
    {
        private List<string> Skills;
        public string Name;
        public Indexer() {
            Console.WriteLine("Object Created");
            Name = "Johnny";
            Skills = new List<string>
            {
                "C#", "Java", "React", "JS"
            };
        }


        public string this[int index]
        {
            get { return Skills[index]; }
            set { Skills[index] = value; }
        }

        public int this[string name] {
            get
            {
                int index = -1;
                for (int i = 0; i < Skills.Count; i++)
                {
                    if (Skills[i] == name)
                    {
                        return i;
                    }
                }
                return index;
            }
        }
    }
}
