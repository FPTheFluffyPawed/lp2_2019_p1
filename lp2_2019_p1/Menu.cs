﻿using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    class Menu
    {
        // The Dictionary is used to swap between menus.
        Dictionary<string, string> menus = new Dictionary<string, string>();

        // The List is used to save the current menu the user is in so we can
        // go back between the menus.
        List<string> historic = new List<string>();

        // Call our Searcher.
        Searcher searcher = new Searcher();

        // Variables.
        string currentMenu;
        bool createdFilter = false;

        /// <summary>
        /// Constructor to call our menu.
        /// </summary>
        public Menu()
        {
            Console.WriteLine("Welcome to the IMDB searcher!\n");
            Execute();
        }
        /// <summary>
        /// This method executes the  menu.
        /// </summary>
        private void Execute()
        {
            menus.Add("menu1", "1 - Choose by title \nb - Exit program");

            // Path for the first decision (Titles).
            menus.Add("menu2", "1 - Filter type \n2 - Show results \nb - Back");
            menus.Add("menu3", "1 -  \nb - Back");
            menus.Add("menu4", "b - Back");

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
                    
                    case "2":
                        if (currentMenu == "menu2")
                        {
                            if (createdFilter)
                            {
                                searcher.Filter();
                                searcher.ShowResults();
                            }
                            else
                                Console.WriteLine("No filter applied yet!");

                        }
                        break;
                    
                    case "b":
                        if (currentMenu == "menu1")
                        {
                            System.Environment.Exit(1);
                        }
                        currentMenu = historic[historic.Count - 1];
                        historic.RemoveAt(historic.Count - 1);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// This method asks to the user 
        /// to put the information asked
        /// and saves it in the Searcher class.
        /// </summary>
        private void SearchPerTitle()
        {
            searcher.TypeInputFilter();
            searcher.NameInputFilter();
            searcher.AdultsInputFilter();
            searcher.StartYearInputFilter();
            searcher.EndYearInputFilter();
            searcher.RatingsInputFilter();
            searcher.GenresInputFilter();

            createdFilter = true;
        }
    }
}
