using System;
using System.Collections.Generic;

namespace CongestionTaxCalculator
{
    public interface ICongestionTaxCalculatorSettings
    {
        int MaxDayPayment { get; }
        HashSet<string> TollFreeVehicleTypes { get; }
        (TimeSpan TimeSpan, int Amount)[] FeeIntervals { get; }
        DateTime[] Holidays { get; }
        DateTime[] NonTaxedMonths { get; }
    }
}
