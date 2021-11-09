using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator
{
    public class CongestionTaxCalculatorSettings : ICongestionTaxCalculatorSettings
    {
        // todo: fetch settings from ex app settings, db or settings api
        public int MaxDayPayment => 60;

        public HashSet<string> TollFreeVehicleTypes { get; } = new[]
        {
            "Emergency",
            "Bus",
            "Diplomat",
            "Motorcycle",
            "Military",
            "Foreign",
        }.ToHashSet();

        public (TimeSpan TimeSpan, int Amount)[] FeeIntervals { get; } = ConvertTimeIntervals(new[]
        {
            ("06:00", 8),
            ("06:30", 13),
            ("07:00", 18),
            ("08:00", 13),
            ("08:30", 8),
            ("15:00", 13),
            ("15:30", 18),
            ("17:00", 13),
            ("18:00", 8),
            ("18:30", 0),
        }).ToArray();

        public DateTime[] Holidays { get; } = new[]
        {
            "2013-01-01",
            "2013-03-28",
            "2013-03-29",
            "2013-04-01",
            "2013-04-30",
            "2013-05-01",
            "2013-05-08",
            "2013-05-09",
            "2013-06-05",
            "2013-06-06",
            "2013-06-21",
            "2013-11-01",
            "2013-12-24",
            "2013-12-25",
            "2013-12-26",
            "2013-12-31",
        }.Select(DateTime.Parse).ToArray();

        public DateTime[] NonTaxedMonths { get; } = {new(2013, 7, 1)};

        private static IEnumerable<(TimeSpan Begin, int Amount)> ConvertTimeIntervals((string StartTimeStr, int Amount)[] rawIntervals)
        {
            return rawIntervals
                .Select(i => (TimeSpan.ParseExact(i.StartTimeStr, @"hh\:mm", null), i.Amount));
        }
    }
}
