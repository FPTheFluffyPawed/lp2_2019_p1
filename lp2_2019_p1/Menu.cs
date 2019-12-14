using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    class Menu
    {
        Dictionary<string, string> menus = new Dictionary<string, string>();
        List<string> historic = new List<string>();
        string currentMenu;

        Searcher searcher = new Searcher();
      
        public Menu()
        {
            Console.WriteLine("Loading");
            Execute();
        }

        private void Execute()
        {
            menus.Add("menu1", "1 - Choose by title \n2 - Choose by people");

            // titles path (decision 1)
            menus.Add("menu2", "1 - Filter type \n2 - Show results \nb - Back");
            menus.Add("menu3", "1 -  \nb - Back");
            menus.Add("menu4", "b - Back");

            // people path (decision 2)
            menus.Add("menu5", "1 - Type the name of the person \nb - Back");
            menus.Add("menu6", "1 - Show more details \nb - Back");
            menus.Add("menu7", "b - Back");

            currentMenu = "menu1";
            while (true)
            {
                Console.WriteLine("Choose the search type:");
                Console.WriteLine(menus[currentMenu]);
                switch (Console.ReadLine())
                {
                    case "1":
                        if (currentMenu == "menu1")
                        {
                            historic.Add(currentMenu);
                            currentMenu = "menu2";
                        }
                        else if (currentMenu == "menu2") SearchPerTitle();
                        break;
                    /*
                    case "2":
                        if (currentMenu == "menu1")
                        {

                        }
                        if (currentMenu == "menu2")
                        {
                            searcher.Filter();
                            searcher.ShowResults();
                        }
                        break;
                    */
                    case "b":
                        currentMenu = historic[historic.Count - 1];
                        historic.RemoveAt(historic.Count - 1);
                        break;

                    default:
                        break;
                }
            }
        }
        private void SearchPerTitle()
        {
            searcher.TypeInputFilter();
            searcher.NameInputFilter();
            searcher.AdultsInputFilter();
            searcher.StartYearInputFilter();
            searcher.EndYearInputFilter();
            searcher.RatingsInputFilter();
            searcher.GenresInputFilter();
        }
        private void OptionFilter()
        {

        }

        private void SearchPerPerson()
        {
            Console.WriteLine("Please type the name of the person you're " +
                "looking for");
            string person = Console.ReadLine();
            // show list
        }
    }
}
