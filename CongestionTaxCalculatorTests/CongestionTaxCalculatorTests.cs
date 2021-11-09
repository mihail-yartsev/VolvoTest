using System;
using System.Collections.Generic;
using System.Linq;
using CongestionTaxCalculator;
using FluentAssertions;
using Xunit;

namespace CongestionTaxCalculatorTests
{
    public class CongestionTaxCalculatorTests
    {
        // todo: separate this test into multiple tests based on the functionality being tested
        [Fact]
        public void Test()
        {
            var inputs = new[]
            {
                "2013-01-14 05:00:00",
                "2013-01-15 21:00:00",
                "2013-02-07 06:23:27",
                "2013-02-07 14:27:00",
                "2013-02-07 15:07:00",
                "2013-02-07 15:27:00",
                "2013-02-08 06:27:00",
                "2013-02-08 06:20:27",
                "2013-02-08 14:35:00",
                "2013-02-08 15:29:00",
                "2013-02-08 15:47:00",
                "2013-02-08 16:01:00",
                "2013-02-08 16:48:00",
                "2013-02-08 17:49:00",
                "2013-02-08 18:29:00",
                "2013-02-08 18:35:00",
                "2013-03-26 14:25:00",
                "2013-03-28 14:07:27",
                "2013-07-28 14:07:27"
            }.Select(DateTime.Parse).ToArray();
            var sut = new CongestionTaxCalculator.CongestionTaxCalculator(new CongestionTaxCalculatorSettings());
            
            var result = sut.GetTax("NormalCar", inputs);

            result.Should().BeEquivalentTo(new List<(DateTime, int)>
            {
                (DateTime.Parse("2013-02-07"), 8 + 13),
                (DateTime.Parse("2013-02-08"), 60),
                (DateTime.Parse("2013-03-26"), 8),
            });
        }

        // todo: add vehicle type tests
    }
}
