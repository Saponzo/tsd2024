using GoldSavings.App.Model;
using GoldSavings.App.Client;
using GoldSavings.App.Services;
using Microsoft.VisualBasic;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Investor!");

        // Step 1: Get gold prices
        GoldDataService dataService = new GoldDataService();

        List<GoldPrice> goldPrices = new List<GoldPrice>();
        for (int year = 2019; year <= 2024; year++)
        {
            DateTime startDate = new DateTime(year, 01, 01);
            DateTime endDate = new DateTime(year, 12, 31);
            var yearlyPrices = dataService.GetGoldPrices(startDate, endDate).GetAwaiter().GetResult();
            goldPrices.AddRange(yearlyPrices);
        }

        if (goldPrices.Count == 0)
        {
            Console.WriteLine("No data found. Exiting.");
            return;
        }

        Console.WriteLine($"Retrieved {goldPrices.Count} records. Ready for analysis.");

        // Step 2: Perform analysis
        GoldAnalysisService analysisService = new GoldAnalysisService(goldPrices);
        var avgPrice = analysisService.GetAveragePrice();

        // Step 3: Print results
        GoldResultPrinter.PrintSingleValue(Math.Round(avgPrice, 2), "Average Gold Price Last Half Year");

        List<GoldPrice> higherPrices = analysisService.Get3HighestPricesLastYearQuerry();
        List<GoldPrice> lowerPrices = analysisService.Get3LowestPricesLastYearQuerry();
        Console.WriteLine("\n3 higher prices :\n");

        foreach (var price in higherPrices) {
            Console.WriteLine($"{price.Price} ");
        } 

        Console.WriteLine("\n3 lower prices :\n");

        foreach (var price in lowerPrices) {
            Console.WriteLine($"{price.Price} ");
        } 

        Console.WriteLine("\nNumber of dates with more than 5% gain :\n");
        var datePrice = analysisService.GetDatesWith5PercentGain();
        
        Console.WriteLine($"{datePrice.Count} ");

        Console.WriteLine("\n3 dates :\n");

        var dates = analysisService.Get3DatesOpeningSecondTenPricesRanking();
        foreach (var date in dates) {
            Console.WriteLine($"{date.Date} ");
        } 
        

        Console.WriteLine("\nGold Analyis Queries with LINQ Completed.");

    }
}

// Question 2.a 
/* 3 Highest : 361.74, 358.35, 357.14
   3 Lowest : 257.59, 258.4, 258.87
*/

// Question 2.b
// Yes there is 1224 dates with more than 5% gain if someone bought on January 2020

// Question 2.c
// 29/04/2022, 20/04/2022, 02/05/2022