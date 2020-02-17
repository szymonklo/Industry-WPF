using System;
using System.Collections.Generic;
using System.Text;
using ModelLibrary.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ModelLibrary.Models
{
    class Round
    {
        public static int RoundNumber { get; set; }
        public static void Go()
        {
            Program.Income = 0;
            Program.Cost = 0;
            Program.Profit = 0;
            Console.WriteLine("Round: " + RoundNumber);
            //Cities demand
            Console.WriteLine("**** Cities demand ****\n");
            foreach (City city in World.Cities)
            {
                //city.Demand();
                Product.Demand(city);
            }
            Console.WriteLine("\n");

            //Factories produce
            Console.WriteLine("**** Factories produce ****\n");
            foreach (Factory factory in World.Factories)
            {
                factory.Produce(factory.Product);
            }
            Console.WriteLine("\n");

            // Products are transported from "tier n" to "tier n-1" factories
            Console.WriteLine("**** Products are transported from tier n to tier n - 1 factories ****\n");
            for (int t = 4; t > 0; t--)
            {
                foreach (Factory factoryS in World.Factories.Where(factory => factory.Tier == t))
                {
                    foreach (Factory factoryR in World.Factories.Where(factory => factory.Tier == t - 1))
                    {
                        if (factoryR.ProductType.Components.Contains(factoryS.ProductType))
                        {
                            TransportOrder transportOrder = TransportOrder.GetOrder(factoryS, factoryR, factoryS.ProductType);
                            //usuniete
                            //transportOrder.FewProductsToSend += Form1.OnFewProductsToSendMessage;
                            transportOrder.Go();
                        }
                    }
                }
            }
            Console.WriteLine("\n");

            //Products are transported from factories to cities
            Console.WriteLine("**** Products are transported from factories to cities ****\n");
            foreach (Factory factory in World.Factories)
            {
                //optimize transportorders
                Product product = factory.Product;
                var citiesId = product.GetMostAndLeastProfitableCities();
                TransportOrder cheapTransportOrder = TransportOrder.GetOrder(factory, World.Cities[citiesId.Item2], factory.ProductType);
                TransportOrder expensiveTransportOrder = TransportOrder.GetOrder(factory, World.Cities[citiesId.Item1], factory.ProductType);
                int amountChange = cheapTransportOrder.Capacity / 3;
                cheapTransportOrder.Capacity -= amountChange;
                expensiveTransportOrder.Capacity += amountChange;
                //end optimize

                foreach (City city in World.Cities)
                {
                    TransportOrder transportOrder = TransportOrder.GetOrder(factory, city, factory.ProductType);
                    //usuniete
                    //transportOrder.FewProductsToSend += Form1.OnFewProductsToSendMessage;
                    transportOrder.Go();
                }
            }
            Console.WriteLine("\n");

            //Cities consume
            Console.WriteLine("**** Cities consume ****\n");
            Product.Consume();
            //foreach (City city in World.Cities)
            //{
            //    city.Consume();
            //}
            Console.WriteLine("\n");

            RoundNumber++;

        }
    }
}
