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

        private StructTitleTotal[] queryResults;

        string searchType, searchPrimaryTitle, searchForAdults,
            searchStartYear, searchEndYear, searchRatings;
        string[] searchGenres;

        private FileManager database = new FileManager();

        public string TypeInputFilter()
        {
            Console.WriteLine("From the list of types, choose one.");
            foreach (string type in database.AllTypes)
                Console.Write($"{type} ");
            Console.WriteLine("\nWrite the type you want to search (or not).");
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
            Console.WriteLine("Write up to 3 genres, according to the list.");
            foreach (string genre in database.AllGenres)
                Console.Write($"{genre} ");
            Console.WriteLine("\nThis is case-sensitive, so please type your" +
                " three genres exactly like the list!");
            string input = Console.ReadLine();
            string[] inputArray = new string[3];

            if(input == "")
            {
                inputArray = null;
            }
            else
            {
                inputArray = input.Split(",");
            }

            searchGenres = inputArray;
            return searchGenres;
        }

        public string RatingsInputFilter()
        {
            Console.WriteLine("Between 0 to 10, choose a number.");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
                searchRatings = input;
            else
                searchRatings = "0";

            return searchRatings;
        }

        public void Filter()
        {
            queryResults =
                 (from title in database.Titles
                  join ratings in database.Ratings on title.TitleIdentifier equals ratings.RatingsIdentifier into titleWithRatings
                  from tR in titleWithRatings
                 where ContainString(title.TitleType, searchType)
                 where ContainString(title.PrimaryTitle, searchPrimaryTitle)
                 where ContainString(title.ForAdults.ToString(), searchForAdults)
                 where ContainString(title.StartYear.ToString(), searchStartYear)
                 where ContainString(title.EndYear.ToString(), searchEndYear)
                 where tR.RatingsAverage >= float.Parse(searchRatings)
                 where (searchGenres == null ||
                 !searchGenres.Except(title.Genres).Any())
                  select new StructTitleTotal
                  {
                      RTitleIdentifier = title.TitleIdentifier,
                      RTitleType = title.TitleType,
                      RPrimaryTitle = title.PrimaryTitle,
                      RForAdults = title.ForAdults,
                      RStartYear = title.StartYear,
                      REndYear = title.EndYear,
                      RRatingsAverage = tR.RatingsAverage,
                      RGenres = title.Genres
                  })
                 .ToArray();

            // Order all our results and assign it to our queryResult.
            queryResults = OrderByResultsBy(queryResults);
        }

        private StructTitleTotal[] OrderByResultsBy(StructTitleTotal[] titles)
        {
            Console.WriteLine("Order your list by..." +
                "\n1 - Type" +
                "\n2 - Name" +
                "\n3 - For Adults" +
                "\n4 - Start Year" +
                "\n5 - End Year" +
                "\n6 - Rating");
            string input = Console.ReadLine();
            short orderBy = Convert.ToInt16(input);

            // Order by based on the option picked!
            switch (orderBy)
            {
                case 1:
                    return titles.OrderBy(t => t.RTitleType).ToArray();
                case 2:
                    return titles.OrderBy(t => t.RPrimaryTitle).ToArray();
                case 3:
                    return titles.OrderBy(t => t.RForAdults).ToArray();
                case 4:
                    return titles.OrderBy(t => t.RStartYear).ToArray();
                case 5:
                    return titles.OrderBy(t => t.REndYear).ToArray();
                default:
                    return titles.OrderBy(t => t.RRatingsAverage).ToArray();
            }
        }

        public void ShowResults()
        {
            // Reset back to 0.
            numTitlesShown = 0;

            // Mostrar os títulos, 10 de cada vez
            while (numTitlesShown < queryResults.Length)
            {
                Console.WriteLine(" --- ");
                Console.WriteLine($"Found {queryResults.Length} titles.");
                //Console.WriteLine(
                //    $"\t=> Press key to see next {numTitlesToShowOnScreen} titles...");
                //Console.ReadKey(true);

                // Mostrar próximos 10
                for (int i = numTitlesShown;
                    i < numTitlesShown + numTitlesToShowOnScreen
                        && i < queryResults.Length;
                    i++)
                {
                    // Usar para melhorar a forma como mostramos os géneros
                    bool firstGenre = true;

                    // Get the current title.
                    // StructTitleTotal title = queryResults[i];

                    // Show information about each title.
                    Console.Write("\t* ");
                    Console.Write("{0} - ",i+1);

                    Console.Write("R: {0} - ", queryResults[i].RRatingsAverage);
                    Console.Write($"\"{queryResults[i].RPrimaryTitle}\" ");
                    Console.Write($"({queryResults[i].RStartYear?.ToString() ?? "N/A"}): ");
                    Console.Write($"A: {queryResults[i].RForAdults.ToString()} ");
                    foreach (string genre in queryResults[i].RGenres)
                    {
                        if (!firstGenre) Console.Write("/ ");
                        Console.Write($"{genre} ");
                        firstGenre = false;
                    }
                    Console.WriteLine();
                }


                Console.WriteLine("Choose your option:");
                Console.WriteLine("1 - Choose your title. |" +
                    "2 - Exit the search. | Any other key to continue search.");
                switch (Console.ReadLine())
                {
                    case "1":
                        ChooseTitle();
                        break;
                    case "2":
                        // Setting the numTitlesShown to the same as the length
                        // forces us out of the loop.
                        numTitlesShown = queryResults.Length;
                        break;
                    default:
                        break;
                }
                // Próximos 10
                numTitlesShown += numTitlesToShowOnScreen;
            }
            // Clear the filter when we're done.
            queryResults = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private bool ContainString(string property, string? varstring)
        {
            bool b;

            if (varstring == null)
                b = true;
            else
                b = property.ToLower().Contains(varstring);

            return b;
        }

        private void ChooseTitle()
        {
            int choice;
            Console.Write("Type the number of your title:");
            choice = Convert.ToInt32(Console.ReadLine());

            //ver agora os detalhos do titulo selecionado//
            Console.WriteLine($"Type: {queryResults[choice].RTitleType}" +
                $"\nName: {queryResults[choice].RPrimaryTitle}" +
                $"\nAdult: {queryResults[choice].RForAdults}" +
                $"\nStart Year: {queryResults[choice].RStartYear}" +
                $"\nEnd Year: {queryResults[choice].REndYear?.ToString() ?? "N/A" }");
            foreach (string genre in queryResults[choice].RGenres)
                Console.WriteLine($"\nGenres: {genre} /");

            Console.WriteLine("Press any key to return to listing...\n");

            // Exit when a key is pressed. Phase 3 implementation goes here.
            Console.ReadKey(true);
            //Detail(queryResults[choice]);
            
        }
    }
        
}
