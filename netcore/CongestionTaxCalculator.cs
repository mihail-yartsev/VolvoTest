using System;
using System.Collections.Generic;
using System.Linq;

namespace CongestionTaxCalculator
{
    public class CongestionTaxCalculator : ICongestionTaxCalculator
    {
        private readonly ICongestionTaxCalculatorSettings _congestionTaxCalculatorSettings;

        private readonly (TimeSpan Begin, int Amount)[] _feeIntervals;

        public CongestionTaxCalculator(ICongestionTaxCalculatorSettings congestionTaxCalculatorSettings)
        {
            _congestionTaxCalculatorSettings = congestionTaxCalculatorSettings;
            _feeIntervals = _congestionTaxCalculatorSettings.FeeIntervals.OrderBy(i => i.TimeSpan)
                .Prepend((default, _congestionTaxCalculatorSettings.FeeIntervals[^1].Amount)).ToArray();
        }


        /// <summary>
        /// Calculate the total toll fee for one vehicle
        /// </summary>
        /// <param name="vehicleType">The type of the vehicle</param>
        /// <param name="dates">dates and times of all passes</param>
        /// <returns></returns>
        public IEnumerable<(DateTime Date, int Amount)> GetTax(string vehicleType, DateTime[] dates)
        {
            if (IsTollFreeVehicle(vehicleType))
            {
                return Enumerable.Empty<(DateTime Date, int Amount)>();
            }

            return dates.OrderBy(d => d)
                .Select(d => (Timestamp: d, Amount: GetTollFee(d)))
                .Where(d => d.Amount > 0)
                .GroupIntoTimeWindows(TimeSpan.FromHours(1))
                .Select(timeWindow => (
                    WindowEnd: timeWindow.Last().Timestamp,
                    Amount: timeWindow.Select(t => t.Value).Max()))
                .GroupBy(t => t.WindowEnd.Date,
                    (date, dayGroup) => (
                        Date: date,
                        Amount: Math.Min(_congestionTaxCalculatorSettings.MaxDayPayment, dayGroup.Sum(t => t.Amount))));
        }

        private bool IsTollFreeVehicle(string vehicleType)
        {
            return _congestionTaxCalculatorSettings.TollFreeVehicleTypes.Contains(vehicleType);
        }

        private int GetTollFee(DateTime date)
        {
            if (IsTollFreeDate(date)) return 0;
            var timeOfDay = date.TimeOfDay;
            return _feeIntervals.Last(i => timeOfDay >= i.Begin).Amount;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                return true;
            }

            var month = date.Month;
            if (_congestionTaxCalculatorSettings.NonTaxedMonths.Any(d => d.Date.Month == month))
            {
                return true;
            }

            if (_congestionTaxCalculatorSettings.Holidays.Contains(date.Date.Date))
            {
                return true;
            }
            
            return false;
        }
    }
}