using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    public struct StructTitle
    {
        // Type of title.
        public string TitleType { get; }
        public string PrimaryTitle { get; }
        public bool ForAdults { get; }
        public short? StartYear { get; }
        public short? EndYear { get; }
        public IEnumerable<string> Genres { get; }

        public StructTitle(
            string titleType, string primaryTitle, bool forAdults,
            short? startYear, short? endYear, IEnumerable<string> genres)
        {
            TitleType = titleType;
            PrimaryTitle = primaryTitle;
            ForAdults = forAdults;
            StartYear = startYear;
            EndYear = endYear;
            Genres = genres;
        }
    }
}
