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

                ProductType productType = new ProductType(id, group, name, defPrice);

                output.Add(productType);
            }

            return output;
        }

        public static void SaveToProductTypeFile (this List<ProductType> productTypes, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (ProductType p in productTypes)
            {
                lines.Add($"{p.Id};{p.Group};{p.Name};{p.DefPrice}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

    }
}
