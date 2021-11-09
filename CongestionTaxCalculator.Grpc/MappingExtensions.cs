using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.WellKnownTypes;

namespace CongestionTaxCalculator.Grpc
{
    public static class MappingExtensions
    {
        public static CalculateReply ToReply(this IEnumerable<(DateTime Date, int Amount)> calculationResult)
        {
            var calculateReply = new CalculateReply();
            calculateReply.AmountPerDay.AddRange(calculationResult.Select(t => new DateFeePair{Date = t.Date.ToTimestamp(), Amount = t.Amount}));
            return calculateReply;
        }
    }
}
