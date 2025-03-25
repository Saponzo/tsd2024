using System;
using System.Collections.Generic;
using System.Linq;
using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public class GoldAnalysisService
    {
        private readonly List<GoldPrice> _goldPrices;

        public GoldAnalysisService(List<GoldPrice> goldPrices)
        {
            _goldPrices = goldPrices;
        }
        public double GetAveragePrice()
        {
            return _goldPrices.Average(p => p.Price);
        }

        public List<GoldPrice> Get3HighestPricesLastYearQuerry()
        {
            var startDate = new DateTime(2024,01,01);
            var endDate = new DateTime(2024,12,31);
            var lastYearPrices = from p in _goldPrices
                     where p.Date >= startDate && p.Date <= endDate
                     orderby p.Price descending
                     select p;

            var highest = lastYearPrices.Take(3).ToList();

            return highest;
        }

        public List<GoldPrice> Get3HighestPricesLastYear()
        {
            var startDate = new DateTime(2024,01,01);
            var endDate = new DateTime(2024,12,31);
            var lastYearPrices = _goldPrices.Where(p => p.Date >= startDate).Where(p => p.Date <= endDate).ToList();

            var highest = lastYearPrices.OrderByDescending(p => p.Price).Take(3).ToList();
            

            return highest;
        }

        public List<GoldPrice> Get3LowestPricesLastYearQuerry()
        {
            var startDate = new DateTime(2024,01,01);
            var endDate = new DateTime(2024,12,31);
            var lastYearPrices = from p in _goldPrices
                     where p.Date >= startDate && p.Date <= endDate
                     orderby p.Price
                     select p;

            var lowest = lastYearPrices.Take(3).ToList();

            return lowest;
        }


        public List<GoldPrice> Get3LowestPricesLastYear()
        {
            var startDate = new DateTime(2024,01,01);
            var endDate = new DateTime(2024,12,31);
            var lastYearPrices = _goldPrices.Where(p => p.Date >= startDate).Where(p => p.Date <= endDate).ToList();

            var lowest = lastYearPrices.OrderBy(p => p.Price).Take(3).ToList();
            

            return lowest;
        }
        
        public bool WouldHaveEarnedMoreThan5Percent()
        {
            var startDate = new DateTime(2020, 01, 01);
            var endDate = new DateTime(2020, 01, 31);
            var january2020Prices = from p in _goldPrices
                                    where p.Date >= startDate && p.Date <= endDate
                                    select p.Price;

            if (!january2020Prices.Any())
            {
                return false;
            }

            var initialPrice = january2020Prices.Average();

            var currentPrice = _goldPrices.OrderByDescending(p => p.Date).FirstOrDefault()?.Price ?? 0;

            return currentPrice > initialPrice * 1.05;
        }
    }
}
