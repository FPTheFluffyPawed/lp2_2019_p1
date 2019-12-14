using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    public struct StructRatings
    {
        // Unique identifier.
        public string RatingsIdentifier { get; }

        // Rating given to the title.
        public float RatingsAverage { get; }

        public StructRatings(string ratingsIdentifier, float ratingsAverage)
        {
            RatingsIdentifier = ratingsIdentifier;
            RatingsAverage = ratingsAverage;
        }
    }
}
