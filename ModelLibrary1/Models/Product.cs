﻿using System;
using System.Collections.Generic;
using System.Text;
using ModelLibrary.Models;
using System.Linq;
using System.ComponentModel;

namespace ModelLibrary.Models
{
    public class Product : ProductType, INotifyPropertyChanged
    {
        public int AmountIn { get; set; }
        public int AmountOut { get; set; }
        private int _amountDone;
        public int AmountDone
        {
            get
            {
                return _amountDone;
            }
            set
            {
                _amountDone = value;
                AmountDoneQueue.Enqueue(value);
                if (AmountDoneQueue.Count >= 10)
                    AmountDoneQueue.Dequeue();
            }
        }
        public double ProductPrice { get; set; }
        public double MarketPriceMod { get; set; }
        public double ProductionCost { get; set; }
        public double ProductCost { get; set; }
        public double ProductProfit { get; set; }
        public string FacilityName { get; set; }    //TODO - usunąć po ręcznym zdefiniowaniu kolumn
        public double Income { get; set; }
        public double Cost { get; set; }
        public double Profit { get; set; }
        public double ValueIn { get; set; }
        public double ValueOut { get; set; }
        public Queue<int> AmountDoneQueue { get; set; } = new Queue<int>(10);
        /// <summary>
        /// Dictionary of Product, key is <facility.Type, Facility.Id, Id>
        /// </summary>
        public static Dictionary<Tuple<int, int, int>, Product> Products { get; set; } = new Dictionary<Tuple<int, int, int>, Product>();

        public static List<Product> GetAll()
        {
            //var p = Products.Select(x => Facility.GetFacility(x.Key.Item1, x.Key.Item2));
            return Products.Values.ToList();
        }

        public Product(byte group, string productName, double defPrice, List<ProductType> components)//, int amount = 0)
            : base(group, productName, defPrice, components) { }

        public Product(ProductType productType, Facility facility, int amount = 0)
            : this(productType.Group, productType.Name, productType.DefPrice, productType.ComponentTypes)
        {
            Id = productType.Id;
            ProductionCost = productType.Group;
            AmountIn = amount;
            Add(facility);
        }

        private void Add(Facility facility)
        {
            FacilityName = facility.Name;
            Tuple<int, int, int> pkey = new Tuple<int, int, int>(facility.FacilityType, facility.Id, Id);
            Products.Add(pkey, this);

            //przypisanie produktu
            if (facility is Factory && ((Factory)facility).ProductType.Id == this.Id)
            {
                ((Factory)facility).Product = this;
                facility.Products.Add(this);
            }
            OnPropertyChanged();
        }

        public static Product GetProduct(ProductType productType, Facility facility)
        {
            Tuple<int, int, int> pkey = new Tuple<int, int, int>(facility.FacilityType, facility.Id, productType.Id);
            Product product = null;
            if (Products.ContainsKey(pkey))
                product = Products[pkey];
            return product;
        }
        //dlaczego nie używane
        public static Product GetProduct(int productId, Facility facility)
        {
            Tuple<int, int, int> pkey = new Tuple<int, int, int>(facility.FacilityType, facility.Id, productId);
            return Products[pkey];
        }
        public Tuple<int, int, int> GetProductKey()
        {
            return Products.Where(p => p.Value == this).Select(p => p.Key).Single();
        }

        public Tuple<int, int, double> GetMostAndLeastProfitableCities()
        {
            var orderedProducts = Products.Where(product => product.Key.Item1 == 2).Where(product => product.Key.Item3 == this.Id).OrderByDescending(product => product.Value.ProductProfit);
            int idMost = orderedProducts.First().Key.Item2;
            int idLeast = orderedProducts.Last().Key.Item2;
            double difference = 0;
            if ((orderedProducts.First().Value.ProductProfit + orderedProducts.Last().Value.ProductProfit) > 0.1)
                difference = (orderedProducts.First().Value.ProductProfit - orderedProducts.Last().Value.ProductProfit) / (orderedProducts.First().Value.ProductProfit + orderedProducts.Last().Value.ProductProfit);
            return new Tuple<int, int, double>(idMost, idLeast, difference);
        }

        public City GetCity()
        {
            return City.Cities[GetProductKey().Item2];
        }

        public static void Demand(City city)
        {
            int _defDemand = 1;
            var what = (Products.Where(p => p.Key.Item1 == 2).Where(p => p.Key.Item2 == city.Id)).Select(x => x.Value).ToList();
            
            foreach (Product product in what)
            {
                switch (product.Group)
                {
                    case 1:
                        product.AmountOut = _defDemand * city.Population;
                        break;
                    default:
                        product.AmountOut = _defDemand * city.Population;
                        break;
                }
            }
        }

        public static void Consume()
        {
            foreach (Product product in Products.Where(p => p.Key.Item1 == 2).Select(p => p.Value))
                if (true)   //warunki? demand > 0
                {
                    product.CalculateMarketPriceMod();
                    product.ProductPrice = product.DefPrice * product.MarketPriceMod;
                    product.AmountDone = Math.Min(product.AmountOut, product.AmountIn);
                    product.AmountOut -= product.AmountDone;
                    product.AmountIn -= product.AmountDone;
                    product.ProductProfit = product.ProductPrice - product.ProductCost;

                    product.Income = product.AmountDone * product.ProductPrice;
                    Company.Companies[0].Income += product.Income;
                    Company.Companies[0].Money += product.Income;
                    product.Cost = product.AmountDone * product.ProductCost;
                    product.Profit = product.Income - product.Cost;
                    if (product.AmountDone > 0)
                        product.ProductProfit = product.Profit / product.AmountDone;

                    //activate event
                    ProductWasSold?.Invoke(product.GetCity(), EventArgs.Empty);
                    TransactionDone?.Invoke(product.GetCity(), new ProductEventArgs(product));

                    Console.WriteLine($"{product.GetCity().Name} consumed {product.AmountDone} {product.Name}");
                    Console.WriteLine($"{product.GetCity().Name} still demands {product.AmountOut} {product.Name}");
                    Console.WriteLine($"{product.GetCity().Name} stil has {product.AmountIn} {product.Name}");
                    Console.WriteLine($"{product.GetCity().Name} paid {product.Income:c} ({product.ProductPrice:c} per 1 pc)");
                    Console.WriteLine($"Company profit is {product.Profit:c} ({product.ProductProfit:c} per 1 pc\n");
                }
        }

        //przygotowanie delegata
        public delegate void EventHandler(Facility c, EventArgs e);
        public delegate void ProductEventHandler(object sender, ProductEventArgs a);

        //przygotować deklarację zdarzenia na podstawie powyższego delagata:
        public static event EventHandler ProductWasSold;
        public static event EventHandler<ProductEventArgs> TransactionDone;

        private void CalculateMarketPriceMod()
        {
            if (AmountIn <= AmountOut)
                MarketPriceMod = 2 - (double) AmountIn / AmountOut;
            else
                MarketPriceMod = (double) AmountOut / AmountIn;
        }
    }
}