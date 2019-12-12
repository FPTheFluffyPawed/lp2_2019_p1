using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    class Menu
    {
        Searcher searcher = new Searcher();

        public Menu()
        {
            Console.WriteLine("Loading");
            Execute();
        }

        private void Execute()
        {
            searcher.NameInputFilter();
            searcher.StartYearInputFilter();
            searcher.Filter();
            searcher.ShowResults();
        }
    }
}
