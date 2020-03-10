using System;
using System.Collections.Generic;
using System.Text;
using ModelLibrary.Models;
using System.Collections.ObjectModel;
using System.Linq;
using ModelLibrary.Helpers;

namespace ModelLibrary.Models
{
    public class Round
    {
        //public static List<Round> Rounds = new List<Round>();
        public static int RoundNumber { get; set; }
        
        public static void Go()
        {
            //Round starts
            Console.WriteLine("Round: " + RoundNumber);

            //Companies start new billing period
            Console.WriteLine("**** Companies start new billing period ****\n");
            foreach (Company company in Company.Companies)
                company.ResetFinancesBeforeRound();
            
            //Cities demand
            Console.WriteLine("**** Cities demand ****\n");
            foreach (City city in City.Cities)
                Product.Demand(city);

            //Factories produce
            Console.WriteLine("**** Factories produce ****\n");
            foreach (Factory factory in Factory.Factories)
                factory.Produce();

            //Transport orders are being optimized
            //Console.WriteLine("**** Transport orders are being optimized ****\n");
            //TransportOrdersHelper.OptimizeTransportOrders();

            // Products are transported
            Console.WriteLine("**** Products are transported ****\n");
            foreach (TransportOrder transportOrder in TransportOrder.TransportOrders.Values)
                transportOrder.Go();

            //Cities consume
            Console.WriteLine("**** Cities consume ****\n");
            Product.Consume();

            //Companies close the cach regiter
            Console.WriteLine("**** Companies close the cach regiter ****\n");
            foreach (Company company in Company.Companies)
                company.CalculateProfit();

            //Round ends
            RoundNumber++;
        }
    }
}
