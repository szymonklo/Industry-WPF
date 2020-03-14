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
        public static void ConvertToProductTypes (this List<string> lines)
        {
            ProductType.ResetId();
            foreach (string line in lines)
            {
                string[] columns = line.Split(';');

                int id = int.Parse(columns[0]);
                byte group = byte.Parse(columns[1]);
                string name = columns[2];
                double defPrice = double.Parse(columns[3]);
                double defCost = double.Parse(columns[4]);

                ProductType productType = new ProductType(group, name, defPrice);
                productType.DefCost = defCost;

                List<ProductType> components = new List<ProductType>();
                string[] list = columns[5].Split('|');
                foreach (string c in list)
                {
                    if (c.Length > 0)
                        productType.ComponentTypes.Add(ProductType.GetProductType(int.Parse(c)));
                }
            }
        }
        public static void SaveToProductTypesFile (this List<ProductType> productTypes, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (ProductType p in productTypes.OrderBy(x => x.Id))
            {
                StringBuilder components = new StringBuilder();

                if (p.ComponentTypes != null)
                {
                    foreach (ProductType c in p.ComponentTypes)
                        components.Append(c.Id).Append('|');
                    components.Remove(components.Length-1, 1);
                }
                lines.Add($"{p.Id};{p.Group};{p.Name};{p.DefPrice};{p.DefCost};{components.ToString()}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        //Products
        public static void ConvertToProducts(this List<string> lines)
        {
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
                product.FacilityName = columns[11];
            }
        }
        public static void SaveToProductsFile(this Dictionary<Tuple<int, int, int>, Product> products, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Tuple<int, int, int> t in products.Keys)
            {
                Product p = products[t];
                lines.Add($"{t.Item1};{t.Item2};{t.Item3};{p.AmountIn};{p.AmountOut};{p.AmountDone}" +
                    $";{p.ProductPrice};{p.MarketPriceMod};{p.ProductionCost};{p.ProductCost};{p.ProductProfit};{p.FacilityName}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        //Factories
        public static void ConvertToFactories(this List<string> lines)
        {
            Factory.ResetId();
            foreach (string line in lines)
            {
                string[] columns = line.Split(';');

                int id = int.Parse(columns[0]);
                byte tier = byte.Parse(columns[1]);
                string name = columns[2];
                int productId = int.Parse(columns[3]);
                int defProduction = int.Parse(columns[4]);

                Factory factory = new Factory(name, defProduction, ProductType.GetProductType(productId), tier);

                factory.BaseCost = double.Parse(columns[5]);
                factory.ProductionAmount = int.Parse(columns[6]);
                factory.AmountOfAvailableComponents = int.Parse(columns[7]);
            }
        }
        public static void SaveToFactoriesFile(this List<Factory> factories, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Factory f in factories.OrderBy(x => x.Id))
            {
                lines.Add($"{f.Id};{f.Tier};{f.Name};{f.ProductType.Id};{f.DefProduction};{f.BaseCost}" +
                    $";{f.ProductionAmount};{f.AmountOfAvailableComponents}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        //Cities
        public static void ConvertToCities(this List<string> lines)
        {
            City.ResetId();
            foreach (string line in lines)
            {
                string[] columns = line.Split(';');

                int id = int.Parse(columns[0]);
                string name = columns[1];
                int population = int.Parse(columns[2]);

                City city = new City(name, population);
            }
        }
        public static void SaveToCitiesFile(this List<City> cities, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (City c in cities.OrderBy(x => x.Id))
            {
                lines.Add($"{c.Id};{c.Name};{c.Population}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        //TransportOrders
        public static void ConvertToTransportOrders(this List<string> lines)
        {
            foreach (string line in lines)
            {
                string[] columns = line.Split(';');

                int senderType = int.Parse(columns[0]);
                int senderId = int.Parse(columns[1]);
                int receiverType = int.Parse(columns[2]);
                int receiverId = int.Parse(columns[3]);
                int productTypeId = int.Parse(columns[4]);
                int capacity = int.Parse(columns[5]);
                int receiversNumber = int.Parse(columns[6]);

                TransportOrder transportOrder = new TransportOrder(senderType, senderId, receiverType, receiverId, productTypeId, capacity, receiversNumber);

                transportOrder.Amount = int.Parse(columns[7]);
                transportOrder.TransportCost = int.Parse(columns[8]);
                transportOrder.TransportCostPerUnit = int.Parse(columns[9]);
            }
        }
        public static void SaveToTransportOrdersFile(this Dictionary<Tuple<int, int, int, int, int>, TransportOrder> transportOrders, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Tuple<int, int, int, int, int> k in transportOrders.Keys)
            {
                TransportOrder t = transportOrders[k];
                lines.Add($"{t.Sender.FacilityType};{t.Sender.Id};{t.Receiver.FacilityType};{t.Receiver.Id};{t.ProductType.Id};{t.Capacity}" +
                    $";{t.ReceiversNumber};{t.Amount};{t.TransportCost};{t.TransportCostPerUnit}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        //Company
        public static void ConvertToCompanies(this List<string> lines)
        {
            foreach (string line in lines)
            {
                string[] columns = line.Split(';');

                string name = columns[0];

                Company company = new Company(name);

                company.Money = double.Parse(columns[1]);
                company.Income = double.Parse(columns[2]);
                company.Cost = double.Parse(columns[3]);
                company.Profit = double.Parse(columns[4]);
            }
        }
        public static void SaveToCompaniesFile(this List<Company> companies, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Company c in companies)
            {
                lines.Add($"{c.Name};{c.Money};{c.Income};{c.Cost};{c.Profit}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        //Round
        public static void ConvertToRounds(this List<string> lines)
        {
            foreach (string line in lines)
            {
                string[] columns = line.Split(';');

                Round round = new Round();

                Round.RoundNumber = int.Parse(columns[0]);
            }
        }
        public static void SaveToRoundsFile(string fileName)
        {
            List<string> lines = new List<string>();

            lines.Add($"{Round.RoundNumber}");

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        //TODO - listy w klasach: dla klasy lista statyczna zapisywana do csv jako lista ID
    }
}
