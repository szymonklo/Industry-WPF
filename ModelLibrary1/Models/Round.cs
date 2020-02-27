using System;
using System.Collections.Generic;
using System.Text;
using ModelLibrary.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ModelLibrary.Models
{
    public class Round
    {
        //public static List<Round> Rounds = new List<Round>();
        public static int RoundNumber { get; set; }
        
        public static void Go()
        {
            foreach (Company c in Company.Companies)
            {
                c.Income = 0;
                c.Cost = 0;
                c.Profit = 0;
            }

            Console.WriteLine("Round: " + RoundNumber);
            
            //Cities demand
            Console.WriteLine("**** Cities demand ****\n");
            foreach (City city in City.Cities)
            {
                Product.Demand(city);
            }

            //Factories produce
            Console.WriteLine("**** Factories produce ****\n");
            foreach (Factory factory in Factory.Factories)
            {
                factory.Produce();
            }

            /*
            //optimize transportorders
            foreach (Factory factory in World.Factories)
            {
                Product product = factory.Product;
                TransportOrder cheapTransportOrder = TransportOrder.GetOrder(factory, World.Cities[product.GetMostAndLeastProfitableCities().Item2], factory.ProductType);
                TransportOrder expensiveTransportOrder = TransportOrder.GetOrder(factory, World.Cities[product.GetMostAndLeastProfitableCities().Item1], factory.ProductType);
                double amountChange = (cheapTransportOrder.Capacity + expensiveTransportOrder.Capacity) * product.GetMostAndLeastProfitableCities().Item3;
                cheapTransportOrder.Capacity -= (int) amountChange;
                expensiveTransportOrder.Capacity += (int) amountChange;
            }
            */

            //DONE - uwzglednic rowniez transport do miast (po zmianie metody optymalizacji)

            // Products are transported
            Console.WriteLine("**** Products are transported ****\n");
            foreach (TransportOrder transportOrder in TransportOrder.TransportOrders.Values)
            {
                transportOrder.Go();
            }

            //Cities consume
            Console.WriteLine("**** Cities consume ****\n");
            Product.Consume();

            //Closing the sach regiter
            foreach (Company c in Company.Companies)
            {
                Company.Companies[0].CalculateProfit();
            }

            //Ending the round
            RoundNumber++;
        }
    }
}
