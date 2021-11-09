using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace CongestionTaxCalculator
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Takes timestamped tuples of events (first elem of tuple should be DateTime Timestamp) and
        /// groups them into batches (windows) with duration up to <paramref name="windowDuration"/>
        /// </summary>
        public static IEnumerable<IReadOnlyCollection<(DateTime Timestamp, TSource Value)>> GroupIntoTimeWindows<TSource>(
            this IEnumerable<(DateTime Timestamp, TSource Value)> source, TimeSpan windowDuration)
        {
            DateTime? timeWindowStart = null;

            // Segment() is an extension method from the MoreLinq library. It's more convenient to use here compared to GroupBy()
            // since here one doesn't need any keys for groups
            return source.Segment(d =>
            {
                if (d.Timestamp - timeWindowStart <= windowDuration)
                {
                    return false;
                }

                timeWindowStart = d.Timestamp;
                return true;

            }).Select(e => (List<(DateTime Timestamp, TSource Value)>) e);
        }
    }
}
