﻿using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    /// <summary>
    /// Struct class used to store our title information obtained from
    /// the 'titles.basics' file.
    /// </summary>
    public struct StructTitle
    {
        // Unique identifier.
        public string TitleIdentifier { get; }

        // Type of title.
        public string TitleType { get; }

        // Primary name of the title.
        public string PrimaryTitle { get; }

        // If its for adults or not.
        public bool ForAdults { get; }

        // The start year (if it exists).
        public short? StartYear { get; }

        // The end year (if it exists).
        public short? EndYear { get; }

        // Title's genres.
        public IEnumerable<string> Genres { get; }

        public StructTitle(
            string titleIdentifier, string titleType, string primaryTitle,
            bool forAdults, short? startYear, short? endYear,
            IEnumerable<string> genres)
        {
            TitleIdentifier = titleIdentifier;
            TitleType = titleType;
            PrimaryTitle = primaryTitle;
            ForAdults = forAdults;
            StartYear = startYear;
            EndYear = endYear;
            Genres = genres;
        }
    }
}
