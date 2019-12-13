using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace lp2_2019_p1
{
    class Searcher
    {

        int numTitlesShown = 0;
        int numTitlesToShowOnScreen = 10;

        StructTitle[] queryResults;

        string searchType, searchPrimaryTitle, searchForAdults,
            searchStartYear, searchEndYear;
        string[] searchGenres;

        private FileManager titles = new FileManager();

        public string TypeInputFilter()
        {
            Console.WriteLine("From the list of types, choose one.");
            Console.WriteLine($"There is {titles.AllTypes.Count()} types.");
            string input = Console.ReadLine();
            searchType = input;
            return searchType;
        }

        public string NameInputFilter()
        {
            Console.WriteLine("'Name' to search for: ");
            string input = Console.ReadLine();
            searchPrimaryTitle = input;
            return searchPrimaryTitle;
        }

        public string AdultsInputFilter()
        {
            // NOT DONE.
            Console.WriteLine("For adults? [0] for False, [1] for True.");
            string input = Console.ReadLine();
            searchForAdults = input;
            return searchForAdults;
        }

        public string StartYearInputFilter()
        {
            Console.WriteLine("Start year of the title: ");
            string input = Console.ReadLine();
            searchStartYear = input;
            return searchStartYear;
        }

        public string EndYearInputFilter()
        {
            Console.WriteLine("End year of the title: ");
            string input = Console.ReadLine();
            searchEndYear = input;
            return searchEndYear;
        }

        public string[] GenresInputFilter()
        {
            // NOT DONE.
            Console.WriteLine("From the list of genres, choose a maximum of" +
                "three, separating each one with a ','.\nIf you rather leave" +
                "it blank, simply press ENTER.\nExample: 'documentary,horror'");
            Console.Write($"There is {titles.AllGenres.Count} genres.");
            string[] input = new string[2];
            for (int i = 0; i < input.Length; i++)
                input[i] = Console.ReadLine();
            searchGenres = input;
            return searchGenres;
        }

        public void Filter()
        {
            // Missing genres filter, need a way to make it work so we can get
            // it from our array of searchGenres, and apply the elements from
            // the array onto our Contains.

            queryResults =
                (from title in titles.Titles
                 where title.TitleType.ToLower().Contains(searchType)
                 where title.PrimaryTitle.ToLower().Contains(searchPrimaryTitle)
                 where title.ForAdults.ToString().ToLower().Contains(searchForAdults)
                 where title.StartYear.ToString().ToLower().Contains(searchStartYear)
                 where title.EndYear.ToString().ToLower().Contains(searchEndYear)
                 //where title.searchGenres, etc etc
                 select title)
                .ToArray();
        }

        public void OrderBy(int orderBy)
        {
            // do a switch case 'order by' of the query, all goes here etc
        }

        public void ShowResults()
        {
            //queryResults =
            //    (from title in titles.Titles
            // where title.PrimaryTitle.ToLower().Contains(name)
            //  select title)
            //       .OrderBy(title => title.StartYear)
            //       .ThenBy(title => title.PrimaryTitle)
            //       .ToArray();

            // Mostrar os títulos, 10 de cada vez
            while (numTitlesShown < queryResults.Length)
            {
                Console.WriteLine(
                    $"\t=> Press key to see next {numTitlesToShowOnScreen} titles...");
                Console.ReadKey(true);

                // Mostrar próximos 10
                for (int i = numTitlesShown;
                    i < numTitlesShown + numTitlesToShowOnScreen
                        && i < queryResults.Length;
                    i++)
                {
                    // Usar para melhorar a forma como mostramos os géneros
                    bool firstGenre = true;

                    // Obter titulo atual
                    StructTitle title = queryResults[i];

                    // Mostrar informação sobre o título
                    Console.Write("\t\t* ");
                    Console.Write($"\"{title.PrimaryTitle}\" ");
                    Console.Write($"({title.StartYear?.ToString() ?? "unknown year"}): ");
                    foreach (string genre in title.Genres)
                    {
                        if (!firstGenre) Console.Write("/ ");
                        Console.Write($"{genre} ");
                        firstGenre = false;
                    }
                    Console.WriteLine();
                }

                // Próximos 10
                numTitlesShown += numTitlesToShowOnScreen;
            }
        }
    }
        
}
