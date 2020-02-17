using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.Models
{
    public class City : Facility
    {
        public int Population { get; set; }
        //public double Income { get; set; }
        public override int Id { get; set; }
        private static int lastId { get; set; }
        public new int Type { get; private set; }

        public City(string name, int population)//, ProductsKeyed productsOut = null)
        {
            Name = name;
            Population = population;
            Id = lastId;
            lastId++;
            Type = 2;
        }

        //public void Demand()
        //{
        //    int _defDemand = 1;
        //    foreach (Product product in Products)
        //    {
        //        product.AmountOut = product.Group switch
        //        {
        //            1 => _defDemand * Population,
        //            _ => 0,
        //        };
        //        Console.WriteLine($"{Name} demands {product.AmountOut} {product.Name}");
        //    }
        //}

        //public void Consume()
        //{
        //    foreach (Product product in Products)
        //    {
        //        if (true)   //warunki? demand > 0
        //        {
        //            product.ProductPrice = product.DefPrice * MarketPriceMod(product.AmountOut, product.AmountIn);
        //            product.AmountDone = Math.Min(product.AmountOut, product.AmountIn);
        //            product.AmountOut -= product.AmountDone;
        //            product.AmountIn -= product.AmountDone;
        //            product.ProductProfit = product.ProductPrice - product.ProductCost;

        //            Income = product.AmountDone * product.ProductPrice;
        //            Program.Income += this.Income;
        //            Program.Money += Income;
        //            double cost = product.AmountDone * product.ProductCost;
        //            double profit = Income - cost;
        //            product.ProductProfit = profit / product.AmountDone;

        //            //activate event
        //            ProductWasSold?.Invoke(this, EventArgs.Empty);
        //            TransactionDone?.Invoke(this, new ProductEventArgs(product));

        //            Console.WriteLine($"{Name} consumed {product.AmountDone} {product.Name}");
        //            Console.WriteLine($"{Name} still demands {product.AmountOut} {product.Name}");
        //            Console.WriteLine($"{Name} stil has {product.AmountIn} {product.Name}");
        //            Console.WriteLine($"{Name} paid {Income:c} ({product.ProductPrice:c} per 1 pc)");
        //            Console.WriteLine($"Company profit is {profit:c} ({product.ProductProfit:c} per 1 pc\n");
        //        }
        //    }
        //}
        ////przygotowanie delegata
        //public delegate void EventHandler(Facility c, EventArgs e);
        //public delegate void ProductEventHandler(object sender, ProductEventArgs a);

        ////przygotować deklarację zdarzenia na podstawie powyższego delagata:
        //public event EventHandler ProductWasSold;
        //public event EventHandler <ProductEventArgs> TransactionDone;


        //public static double MarketPriceMod(int amountOut, int amountIn)
        //{
        //    double p = (amountOut - amountIn);
        //    p/=(amountOut);
        //    return p+1;
        //}
    }
}
