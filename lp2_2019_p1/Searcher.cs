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

        string searchName, searchStartYear;

        private FileManager titles = new FileManager();

        public string NameInputFilter()
        {
            Console.WriteLine("'Name' to search for: ");
            string input = Console.ReadLine();
            searchName = input;
            return searchName;
        }

        public string StartYearInputFilter()
        {
            Console.WriteLine("Start year of the title: ");
            string input = Console.ReadLine();
            searchStartYear = input;
            return searchStartYear;
        }

        public void Filter()
        {
            queryResults =
                (from title in titles.Titles
                 where title.PrimaryTitle.ToLower().Contains(searchName)
                 where title.StartYear.ToString().Contains(searchStartYear)
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
