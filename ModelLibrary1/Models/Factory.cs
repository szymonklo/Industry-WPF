using System;
using System.Collections.Generic;
using System.Text;
using ModelLibrary.Models;

namespace ModelLibrary.Models
{
    public class Factory : Facility
    {
        //TODO - przemyśleć wybór produkowanego produktu (rodzaj fabryki, sprawdzenie, czy jest produkowany, ..., czy zapisywać w klasie produkt, czy tylko id?
        //ograniczyć publiczne metody, właściwości, szczególnie dotyczące ID, statycznej listy obiektów klasy, obliczania i w miarę możliwości zastąpić je przez metody zdefiniowane wewnątrz klasy
        public new int FacilityType { get; private set; } = 1;
        public override int Id { get; set; }
        //do usuniecia?
        public byte Tier { get; set; }
        public int DefProduction { get; set; }
        public int ProductionAmount { get; set; }
        public double BaseCost { get; set; } = 10;
        public ProductType ProductType { get; set; }
        public Product Product { get; set; }
        public List<Product> Components { get; set; } = new List<Product>();
        private static int lastId { get; set; }
        //public int AmounToSend { get; set; }
        public int AmountOfAvailableComponents { get; set; }

        public static List<Factory> Factories = new List<Factory>();


        public Factory(string factoryName, int factoryDefProduction, ProductType productType, byte tier)
        {
            Name = factoryName;
            DefProduction = factoryDefProduction;
            ProductType = productType;
            Tier = tier;
            Id = lastId;
            lastId++;
            
            Factories.Add(this);
        }

        public Factory(FactoryType factoryType, ProductType productType = null)
            :this (factoryType.Name, factoryType.DefProduction, factoryType.ProductTypes[0], factoryType.Tier)
        {
            BaseCost = factoryType.BaseCost;
            if (productType != null)
                ProductType = productType;
            //TODO - dodać koszt budowy
        }

        public static void ResetId()
        {
            lastId = 0;
        }

        //przygotowanie delegata
        public delegate void NoComponentsDelegate(Facility c, ProductEventArgs e);
        public delegate void TransactionDoneDelegate(Facility c, ProductEventArgs e);
        //przygotować deklarację zdarzenia na podstawie powyższego delagata:
        public event NoComponentsDelegate NoComponents;
        public event TransactionDoneDelegate TransactionDone;

        public void Produce()
        {
            //TODO - uwzglednic sytuacje, gdy produkt staje się komponentem i odwrotnie (zmiana typu produktu)
            SetProduct();

            SetComponents();
            
            CheckComponents();

            CalculateProductionAmount();

            //DONE - uwzglednic poprzednia runde
            Product.ValueOut = Product.ProductCost * Product.AmountOut;
            Product.Cost = 0;

            if (ProductType.ComponentTypes == null)
                Product.AmountDone = ProductionAmount;
            else if (AmountOfAvailableComponents > 0)
            {
                Product.AmountDone = Math.Min(ProductionAmount, AmountOfAvailableComponents);

                foreach (Product component in Components)
                {
                    component.ValueIn = component.AmountIn * component.ProductCost;
                    Product.Cost += component.ProductCost * Product.AmountDone;
                    Product.ValueOut = +Product.Cost;
                    component.AmountIn -= Product.AmountDone;
                    component.AmountDone = -Product.AmountDone;
                    Console.WriteLine($"{Name} used: {Product.AmountDone} {component.Name} (Components remained: {component.AmountIn} {component.Name})");
                }
            }
            Product.AmountOut += Product.AmountDone;
            Product.Cost += (Product.ProductionCost * Product.AmountDone + BaseCost);
            Product.ValueOut += Product.ProductionCost * Product.AmountDone + BaseCost;
            Company.Companies[0].Cost += Product.Cost;
            Company.Companies[0].Money -= Product.Cost;
            
            if (Product.AmountOut > 0)
                Product.ProductCost = Product.ValueOut / Product.AmountOut;

            TransactionDone?.Invoke(this, new ProductEventArgs(Product.GetProduct(ProductType, this)));
            Console.WriteLine($"{Name} produced: {Product.AmountDone} {Product.Name} (On stock: {Product.AmountOut} {Product.Name})");
            Console.WriteLine($"{Product.Name} cost is {Product.ProductCost:c} per 1 pc.");
        }

        private void SetProduct()
        {
            Tuple<int, int, int> productKey = new Tuple<int, int, int>(FacilityType, Id, ProductType.Id);

            if (Product.Products.ContainsKey(productKey))
                Product = Product.GetProduct(ProductType, this);
            else
                Product = new Product(ProductType, this);
        }

        private void SetComponents()
        {
            if (ProductType.ComponentTypes != null)
            {
                Components.Clear();
                foreach (ProductType componentType in ProductType.ComponentTypes)
                {
                    Tuple<int, int, int> componentKey = new Tuple<int, int, int>(FacilityType, Id, componentType.Id);

                    if (!(Product.Products.ContainsKey(componentKey)))
                        Components.Add(new Product(componentType, this));
                    else
                        Components.Add(Product.GetProduct(componentType, this));
                }
            }
        }
        private void CalculateProductionAmount()
        {
            switch (ProductType.Group)
            {
                case 1:
                    ProductionAmount = DefProduction;
                    break;
                default:
                    ProductionAmount = DefProduction;
                    break;
            };
        }

        private void CheckComponents()
        {
            AmountOfAvailableComponents = Int32.MaxValue;

            if (ProductType.ComponentTypes != null)
            {
                foreach (ProductType component in ProductType.ComponentTypes)
                {
                    Product factoryComponent = Product.GetProduct(component, this);
                    if (factoryComponent.AmountIn > 0)
                    {
                        if (factoryComponent.AmountIn < AmountOfAvailableComponents)
                            AmountOfAvailableComponents = factoryComponent.AmountIn;
                    }
                    else
                    {
                        AmountOfAvailableComponents = 0;
                        //activate event
                        NoComponents?.Invoke(this, new ProductEventArgs(factoryComponent));
                    }
                }
            }
        }
    }
}