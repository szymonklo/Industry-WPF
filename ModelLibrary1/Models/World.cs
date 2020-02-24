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
        public static Company Company = new Company("Noble House");
        public static World CreateNewWorld()
        {
            return new World();
        }
        public World()
        {
            ProductType water = new ProductType(0, 1, "water", 2);
            ProductType wheat = new ProductType(1, 1, "wheat", 2);
            ProductType flour = new ProductType(2, 2, "flour", 6, new List<ProductType> { wheat });
            ProductType bread = new ProductType(3, 3, "bread", 14, new List<ProductType> { water, flour });
            
            ProductType.ProductTypes.AddRange(new List<ProductType> { water, wheat, flour, bread });

            Factory waterSupply = new Factory("Water Supply", 100, water, 4);
            Factory cropFarm = new Factory("Crop Farm", 100, wheat, 4);
            Factory mill = new Factory("Mill", 100, flour, 3);
            Factory bakery = new Factory("Bakery", 60, bread, 2);

            Factories.AddRange(new List<Factory> {waterSupply, cropFarm, mill, bakery });

            City krakow = new City("Krakow", 80);
            City warszawa = new City("Warszawa", 100);

            Cities.AddRange(new List<City> { krakow, warszawa });
            
            foreach (City city in Cities)
            {
                foreach (ProductType productType in ProductType.ProductTypes)
                {
                    new Product(productType, city);
                }
                //TODO - events!
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

            //Console.WriteLine("**** Products are transported from factories ****\n");

            //TODO - method for creating transport orders and calculating capacity based on something better than DefProduction and number of receivers
                foreach (Factory factoryS in World.Factories)
                {
                    
                    List<Facility> receivers = World.Factories
                        .Where(factoryR => factoryR.ProductType.Components != null)
                        .Where(factoryR => factoryR.ProductType.Components.Contains(factoryS.ProductType))
                        .Cast<Facility>().ToList();

                    receivers.AddRange(World.Cities.Cast<Facility>().ToList());
                    int receiversNumber = receivers.Count();
                    int capacity = factoryS.DefProduction / receiversNumber;

                    //dlaczego usuniete?
                    //factoryS.AmounToSend = capacity;

                    foreach (Facility facilityR in receivers)
                    {
                        //dlaczego?
                        //if (factoryR.ProductType.Components.Contains(factoryS.ProductType))
                        {

                            TransportOrder transportOrder = new TransportOrder(factoryS, facilityR, factoryS.ProductType, capacity, receiversNumber);
                            //transportOrder.FewProductsToSend += Form1.OnFewProductsToSendMessage;
                        }
                    }
                }
        }
    }
}