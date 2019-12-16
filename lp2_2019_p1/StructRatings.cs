using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    public struct StructRatings
    {
        // Unique identifier.
        public string RatingsIdentifier
            => Line.Split("\t")[0];

        // Rating given to the title.
        public float RatingsAverage
            => float.Parse(Line.Split("\t")[1]);

        // Line used to assign, and divide.
        private string Line { get; }

        public StructRatings(string ratingsIdentifier, float ratingsAverage)
        {
            Line = string.Join('\t',
                new string[]
                {
                    ratingsIdentifier,
                    ratingsAverage.ToString()
                });
        }
    }
}
