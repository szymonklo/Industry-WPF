using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.IO;
using System.Linq;
using ModelLibrary.Models;

namespace ModelLibrary.DataAccess
{
    public static class TextConnectorProcessor
    {
        //General methods
        public static string FullFilePath(this string fileName)
        {
            return $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
        }
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }
            return File.ReadAllLines(file).ToList();
        }

        //ProductTypes
        public static List<ProductType> ConvertToProductTypes (this List<string> lines)
        {
            List<ProductType> output = new List<ProductType>();

            foreach (string line in lines)
            {
                string[] columns = line.Split(';');

                int id = int.Parse(columns[0]);
                byte group = byte.Parse(columns[1]);
                string name = columns[2];
                double defPrice = double.Parse(columns[3]);
                double defCost = double.Parse(columns[3]);

                ProductType productType = new ProductType(id, group, name, defPrice);
                productType.DefCost = defCost;

                output.Add(productType);
            }

            return output;
        }
        public static void SaveToProductTypeFile (this List<ProductType> productTypes, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (ProductType p in productTypes)
            {
                lines.Add($"{p.Id};{p.Group};{p.Name};{p.DefPrice};{p.DefCost}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        //Products
        public static Dictionary<Tuple<int, int, int>, Product> ConvertToProducts(this List<string> lines)
        {
            Dictionary<Tuple<int, int, int>, Product> output = new Dictionary<Tuple<int, int, int>, Product>();

            foreach (string line in lines)
            {
                string[] columns = line.Split(';');

                int facilityType = int.Parse(columns[0]);
                int facilityId = int.Parse(columns[1]);
                int id = int.Parse(columns[2]);

                Product product = new Product(ProductType.GetProductType(id), Facility.GetFacility(facilityType, facilityId));

                product.AmountIn = int.Parse(columns[3]);
                product.AmountOut = int.Parse(columns[4]);
                product.AmountDone = int.Parse(columns[5]);
                product.ProductPrice = double.Parse(columns[6]);
                product.MarketPriceMod = double.Parse(columns[7]);
                product.ProductionCost = double.Parse(columns[8]);
                product.ProductCost = double.Parse(columns[9]);
                product.ProductProfit = double.Parse(columns[10]);

                //output.Add(product);  //dodanie w konstruktorze
            }

            return output;
        }
        public static void SaveToProductFile(this Dictionary<Tuple<int, int, int>, Product> products, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Tuple<int, int, int> t in products.Keys)
            {
                Product p = products[t];
                lines.Add($"{t.Item1};{t.Item2};{t.Item3};{p.AmountIn};{p.AmountOut};{p.AmountDone}" +
                    $";{p.ProductPrice};{p.MarketPriceMod};{p.ProductionCost};{p.ProductCost};{p.ProductProfit}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

    }
}
