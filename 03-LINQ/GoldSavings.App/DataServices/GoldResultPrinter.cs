using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public static class GoldResultPrinter
    {
        public static void PrintPrices(List<GoldPrice> prices, string title)
        {
            Console.WriteLine($"\n--- {title} ---");
            foreach (var price in prices)
            {
                Console.WriteLine($"{price.Date:yyyy-MM-dd} - {price.Price} PLN");
            }
        }

        public static void PrintSingleValue<T>(T value, string title)
        {
            Console.WriteLine($"\n{title}: {value}");
        }

        public static void SavePricesToXml(List<GoldPrice> prices, string filePath)
        {
            var xml = new System.Xml.Linq.XElement("GoldPrices",
            new System.Xml.Linq.XElement("Prices",
                from price in prices
                select new System.Xml.Linq.XElement("GoldPrice",
                new System.Xml.Linq.XAttribute("Date", price.Date.ToString("yyyy-MM-dd")),
                new System.Xml.Linq.XAttribute("Price", price.Price)
                )
            )
            );

            xml.Save(filePath);
        }

        public static List<GoldPrice> ReadPricesFromXml(string filePath) =>
            File.Exists(filePath) ? (new XmlSerializer(typeof(List<GoldPrice>)).Deserialize(new StreamReader(filePath)) as List<GoldPrice>) ?? new List<GoldPrice>() : new List<GoldPrice>();
        
    }
}