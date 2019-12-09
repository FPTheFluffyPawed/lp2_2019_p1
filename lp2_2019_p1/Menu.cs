using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    class Menu
    {
        FileManager file = new FileManager();
        Searcher searcher = new Searcher();

        string hello = "hi";

        public Menu()
        {
            Execute();
            Console.WriteLine(hello);
        }

        private void Execute()
        {
            Console.Write("Test");
            hello = "hello";
        }
    }
}
