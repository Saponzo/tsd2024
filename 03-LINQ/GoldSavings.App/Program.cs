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
        DateTime startDate = new DateTime(2024,01,01);
        DateTime endDate = new DateTime(2024,12,31);
        List<GoldPrice> goldPrices = dataService.GetGoldPrices(startDate, endDate).GetAwaiter().GetResult();

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

        Console.WriteLine($"\n{analysisService.WouldHaveEarnedMoreThan5Percent()}");

        

        Console.WriteLine("\nGold Analyis Queries with LINQ Completed.");

    }
}

// Question 2.a 
/* 3 Highest : 361.74, 358.35, 357.14
   3 Lowest : 257.59, 258.4, 258.87
*/

// Question 2.b
// No there is no date for this increase