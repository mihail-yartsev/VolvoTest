using System;
using System.Collections.Generic;

namespace CongestionTaxCalculator
{
    public interface ICongestionTaxCalculator
    {
        /// <summary>
        /// Calculate the total toll fee for one vehicle
        /// </summary>
        /// <param name="vehicleType">The type of the vehicle</param>
        /// <param name="dates">dates and times of all passes</param>
        /// <returns></returns>
        // todo: replace tuple with proper type
        IEnumerable<(DateTime Date, int Amount)> GetTax(string vehicleType, DateTime[] dates);
    }
}