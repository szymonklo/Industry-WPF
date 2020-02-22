using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLibrary.Models;

namespace ModelLibrary.Models
{
    
    public class World
    {
        public static List<City> Cities = new List<City>();
        public static List<Factory> Factories = new List<Factory>();
        public static World CreateNewWorld()
        {
            return new World();
        }
        public World()
        {
            ProductType water = new ProductType(0, 1, "water", 1);
            ProductType water1 = new ProductType(2, 1, "water", 1);

            ProductType bread = new ProductType(1, 1, "bread", 3, new List<ProductType> { water, water1 });
            List<ProductType> ProductsTypes = new List<ProductType>
            {
                water,
                bread
            };
            //ProductsKeyed products = new ProductsKeyed();
            //foreach (ProductType productType in ProductsTypes)
            //{
            //    products.Add(new Product(productType));
            //}

            Factory waterSupply = new Factory("Water supply", 100, water, 4);
            Factory bakery = new Factory("Bakery", 60, bread, 3);
            Factories.Add(waterSupply);
            Factories.Add(bakery);
            //temp
            Product.GetProduct(water, bakery).AmountIn = 400;
            Product.GetProduct(water, bakery).ProductPrice = 1.1;

            City krakow = new City("Krakow", 80);
            City warszawa = new City("Warszawa", 100);
            Cities.Add(krakow);
            Cities.Add(warszawa);
            //Create cities demand
            //Console.WriteLine("**** Cities demand ****\n");
            //usniete
            //Product.ProductWasSold += new Write().HandleProductSold;   //event
            //usuniete
            //Product.TransactionDone += Form1.OnTransactionDone;
            foreach (City city in Cities)
            {
                //form1.AddCityLabel(city);
                foreach (ProductType productType in ProductsTypes)
                {
                    new Product(productType, city);
                }
                //city.ProductWasSold += new Write().HandleProductSold;   //event
                ////city.ProductWasSold += Form1.OnTransactionDone;
                //city.TransactionDone += Form1.OnTransactionDone;

            }
            /*
            foreach (Factory factory in Factories)
            {
                factory.NoComponents += Form1.OnNoComponentsMessage;
                factory.TransactionDone += Form1.OnTransactionDone;
            }*/
            // Products are transported from "tier n" to "tier n-1" factories
            //Console.WriteLine("**** Products are transported from tier n to tier n - 1 factories ****\n");
            for (int t = 4; t > 0; t--)
            {
                foreach (Factory factoryS in World.Factories.Where(factoryS => factoryS.Tier == t))
                {
                    int receiversNumber = World.Factories.Where(factoryR => factoryR.Tier == t - 1).Where(factoryR => factoryR.ProductType.Components != null).Where(factoryR => factoryR.ProductType.Components.Contains(factoryS.ProductType)).Count();
                    receiversNumber += World.Cities.Count();
                    int capacity = factoryS.DefProduction / receiversNumber;
                    factoryS.AmounToSend = capacity;

                    foreach (Factory factoryR in World.Factories.Where(factoryR => factoryR.Tier == t - 1).Where(factoryR => factoryR.ProductType.Components != null))
                    {
                        if (factoryR.ProductType.Components.Contains(factoryS.ProductType))
                        {

                            TransportOrder transportOrder = new TransportOrder(factoryS, factoryR, factoryS.ProductType, capacity);
                            //transportOrder.FewProductsToSend += Form1.OnFewProductsToSendMessage;
                            //transportOrder.Go();
                        }
                    }
                }
            }
            //Console.WriteLine("\n");

            //Products are transported from factories to cities
            //Console.WriteLine("**** Products are transported from factories to cities ****\n");
            foreach (Factory factory in World.Factories)
            {
                foreach (City city in World.Cities)
                {
                    //int receiversNumber = World.Factories.Where(factoryR => factoryR.ProductType.Components.Contains(factoryS.ProductType)).Count();
                    //receiversNumber += World.Cities.Count();
                    int capacity = factory.AmounToSend;
                    TransportOrder transportOrder = new TransportOrder(factory, city, factory.ProductType, capacity);
                    //transportOrder.FewProductsToSend += Form1.OnFewProductsToSendMessage;
                    //transportOrder.Go();
                }
            }
            //Console.WriteLine("\n");
        }
    }
}