using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Helpers
{
    class TransportOrdersHelper
    {
        public static void OptimizeTransportOrders()
        {
            foreach (Factory factory in Factory.Factories)
            {
                Product product = factory.Product;
                TransportOrder cheapTransportOrder = TransportOrder.GetOrder(factory, City.Cities[product.GetMostAndLeastProfitableCities().Item2], factory.ProductType);
                TransportOrder expensiveTransportOrder = TransportOrder.GetOrder(factory, City.Cities[product.GetMostAndLeastProfitableCities().Item1], factory.ProductType);
                double amountChange = (cheapTransportOrder.Capacity + expensiveTransportOrder.Capacity) * product.GetMostAndLeastProfitableCities().Item3;
                cheapTransportOrder.Capacity -= (int)amountChange;
                expensiveTransportOrder.Capacity += (int)amountChange;
            }
        }
    }
}
