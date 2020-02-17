using System;
using System.Collections.Generic;
using System.Text;
using ModelLibrary.Models;
using System.Linq;

namespace ModelLibrary.Models
{
    public class Product : ProductType
    {
        public int AmountIn { get; set; }
        public int AmountOut { get; set; }
        public int AmountDone { get; set; }
        public double ProductPrice { get; set; }
        public double ProductCost { get; set; }
        public double ProductionCost { get; set; }
        public double ProductProfit { get; set; }

        public static Dictionary<Tuple<int, int, int>, Product> ProductD { get; private set; } = new Dictionary<Tuple<int, int, int>, Product>();

        public Product(int id, byte group, string productName, double defPrice, List<ProductType> components)//, int amount = 0)
            : base(id, group, productName, defPrice, components) { }

        public Product(ProductType productType, Facility facility, int amount = 0)
            : this(productType.Id, productType.Group, productType.Name, productType.DefPrice, productType.Components)
        {
            ProductionCost = productType.Group;
            AmountIn = amount;
            Add(this, facility);
        }

        public void Add(Product product, Facility facility)
        {
            Tuple<int, int, int> pkey = new Tuple<int, int, int>(facility.Type(), facility.Id, product.Id);
            ProductD.Add(pkey, product);
        }

        public static Product GetProduct(ProductType productType, Facility facility)
        {
            Tuple<int, int, int> pkey = new Tuple<int, int, int>(facility.Type(), facility.Id, productType.Id);
            return ProductD[pkey];
        }

        public static Product GetProduct(int productId, Facility facility)
        {
            Tuple<int, int, int> pkey = new Tuple<int, int, int>(facility.Type(), facility.Id, productId);
            return ProductD[pkey];
        }
        public Tuple<int, int, int> GetProductKey()
        {
            return ProductD.Where(p => p.Value == this).Select(p => p.Key).Single();
        }


        public Tuple<int, int> GetMostAndLeastProfitableCities()
        {
            int idMost = ProductD.Where(product => product.Key.Item1 == 2).Where(product => product.Key.Item3 == this.Id).OrderByDescending(product => product.Value.ProductProfit).First().Key.Item2;
            int idLeast = ProductD.Where(product => product.Key.Item1 == 2).Where(product => product.Key.Item3 == this.Id).OrderByDescending(product => product.Value.ProductProfit).Last().Key.Item2;
            return new Tuple<int, int>(idMost, idLeast);
        }

        public City GetCity()
        {
            return World.Cities[GetProductKey().Item2];
        }

        public static void Demand(City city)
        {
            int _defDemand = 1;
            var what = (ProductD.Where(p => p.Key.Item1 == 2).Where(p => p.Key.Item2 == city.Id)).ToList();
            //what.ForEach(i => i.Value.AmountOut = i.Value.Group switch
            //{
            //    1 => _defDemand * city.Population,
            //    _ => 0,
            //});

            //switch (what.Value.Group)
            //{
            //    case 1:
            //        what.Value.AmountOut = _defDemand * city.Population;
            //    default:
            //        what.Value.AmountOut = 0;
            //}
        }

        public static void Consume()
        {
            foreach (Product product in ProductD.Where(p => p.Key.Item1 == 2).Select(p => p.Value))
                if (true)   //warunki? demand > 0
                {
                    product.ProductPrice = product.DefPrice * product.MarketPriceMod();
                    product.AmountDone = Math.Min(product.AmountOut, product.AmountIn);
                    product.AmountOut -= product.AmountDone;
                    product.AmountIn -= product.AmountDone;
                    product.ProductProfit = product.ProductPrice - product.ProductCost;

                    double income = product.AmountDone * product.ProductPrice;
                    Program.Income += income;
                    Program.Money += income;
                    double cost = product.AmountDone * product.ProductCost;
                    double profit = income - cost;
                    product.ProductProfit = profit / product.AmountDone;

                    //activate event
                    ProductWasSold?.Invoke(product.GetCity(), EventArgs.Empty);
                    TransactionDone?.Invoke(product.GetCity(), new ProductEventArgs(product));

                    Console.WriteLine($"{product.GetCity().Name} consumed {product.AmountDone} {product.Name}");
                    Console.WriteLine($"{product.GetCity().Name} still demands {product.AmountOut} {product.Name}");
                    Console.WriteLine($"{product.GetCity().Name} stil has {product.AmountIn} {product.Name}");
                    Console.WriteLine($"{product.GetCity().Name} paid {income:c} ({product.ProductPrice:c} per 1 pc)");
                    Console.WriteLine($"Company profit is {profit:c} ({product.ProductProfit:c} per 1 pc\n");
                }
        }

        //przygotowanie delegata
        public delegate void EventHandler(Facility c, EventArgs e);
        public delegate void ProductEventHandler(object sender, ProductEventArgs a);

        //przygotować deklarację zdarzenia na podstawie powyższego delagata:
        public static event EventHandler ProductWasSold;
        public static event EventHandler<ProductEventArgs> TransactionDone;

        public double MarketPriceMod()
        {
            double p = (AmountOut - AmountIn);
            p /= (AmountOut);
            return p + 1;
        }
    }
}