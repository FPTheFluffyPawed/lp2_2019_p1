using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

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
            string input = Console.ReadLine().ToLower();
            searchPrimaryTitle = input;
            return searchPrimaryTitle;
        }

        public string AdultsInputFilter()
        {
            // NOT DONE.
            Console.WriteLine("For adults? [0] for False, [1] for True.");
            string input = Console.ReadLine().ToLower();
            searchForAdults = input;
            return searchForAdults;
        }

        public string StartYearInputFilter()
        {
            Console.WriteLine("Start year of the title: ");
            string input = Console.ReadLine().ToLower();
            searchStartYear = input;
            return searchStartYear;
        }

        public string EndYearInputFilter()
        {
            Console.WriteLine("End year of the title: ");
            string input = Console.ReadLine().ToLower();
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

            queryResults =
                (from title in titles.Titles
                 where ContainString(title.TitleType, searchType)
                 where ContainString(title.PrimaryTitle, searchPrimaryTitle)
                 where ContainString(title.ForAdults.ToString(), searchForAdults)
                 where ContainString(title.StartYear.ToString(), searchStartYear)
                 where ContainString(title.EndYear.ToString(), searchEndYear)
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
                    Console.Write("{0} - ",i+1);
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


                Console.WriteLine("Choose your option:");
                Console.WriteLine("1 - Choose your title \n2 - Exit search \nAny onther key to continue search");
                switch (Console.ReadLine())
                {
                    case "1":

                        break;
                    case "2":
                        return;
                    default:
                            break;
                }
                // Próximos 10
                numTitlesShown += numTitlesToShowOnScreen;
            }
        }
        // the array onto our Contains.

        private bool ContainString(string property, string? varstring)
        {
            bool b;
            if (varstring == null) { b = true; } else { b = property.ToLower().Contains(varstring); }
            return b;

        }

        private void ChooseTitle()
        {
            int choice;
            Console.Write("Type the number of you choosen title:");
            choice = Convert.ToInt32(Console.ReadLine());

            //ver agora os detalhos do titulo selecionado//

            //Detail(queryResults[choice]);
            
        }
    }
        
}
