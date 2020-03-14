using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLibrary.Models;

namespace ModelLibrary.Models
{
    public class World
    {
        //DONE - move to classes as static properties
        //public Company Company { get; set; }

        //public static World CreateNewWorld()
        //{
        //    return new World();
        //}

        public static void ClearWorld()
        {
            ProductType.ProductTypes.Clear();
            FactoryType.FactoryTypes.Clear();
            Factory.Factories.Clear();
            City.Cities.Clear();
            Product.Products.Clear();
            TransportOrder.TransportOrders.Clear();
            Company.Companies.Clear();
            Round.RoundNumber = 0;
        }
        public static void CreateNewWorld()
        {
            new Company("Noble House");
            
            ProductType water = new ProductType(1, "water", 2);
            ProductType wheat = new ProductType(1, "wheat", 2);
            ProductType rye = new ProductType(1, "rye", 3);
            ProductType wheatFlour = new ProductType(2, "wheatFlour", 6, new List<ProductType> { wheat });
            ProductType ryeFlour = new ProductType(2, "ryeFlour", 7, new List<ProductType> { rye });
            ProductType wheatBread = new ProductType(3, "wheatBread", 14, new List<ProductType> { water, wheatFlour });
            ProductType ryeBread = new ProductType(3, "ryeBread", 16, new List<ProductType> { water, ryeFlour });
            ProductType mixedBread = new ProductType(3, "mixedBread", 15, new List<ProductType> { water, wheatFlour, ryeFlour});

            //ProductType.ProductTypes.AddRange(new List<ProductType> { water, wheat, flour, bread });

            FactoryType farm = new FactoryType("Farm", 4, 100, 10, 2000, new List<ProductType> { wheat, rye });
            FactoryType gather = new FactoryType("Gather", 4, 100, 10, 2000, new List<ProductType> { water });
            FactoryType mill = new FactoryType("Mill", 3, 100, 10, 3000, new List<ProductType> { wheatFlour, ryeFlour });
            FactoryType foodFactory = new FactoryType("Food Factory", 2, 100, 10, 5000, new List<ProductType> { wheatBread, ryeBread, mixedBread });


            Factory waterSupply = new Factory(gather);
            Factory cropFarm = new Factory(farm, rye);
            Factory windMill = new Factory(mill, ryeFlour);
            Factory bakery = new Factory(foodFactory, ryeBread);

            //Factory.Factories.AddRange(new List<Factory> {waterSupply, cropFarm, mill, bakery });

            City krakow = new City("Krakow", 80);
            City warszawa = new City("Warszawa", 100);

            //City.Cities.AddRange(new List<City> { krakow, warszawa });
            
            foreach (City city in City.Cities)
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
                foreach (Factory factoryS in Factory.Factories)
                {
                    
                    List<Facility> receivers = Factory.Factories
                        .Where(factoryR => factoryR.ProductType.ComponentTypes != null)
                        .Where(factoryR => factoryR.ProductType.ComponentTypes.Contains(factoryS.ProductType))
                        .Cast<Facility>().ToList();

                    receivers.AddRange(City.Cities.Cast<Facility>().ToList());
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