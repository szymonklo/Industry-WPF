using System;
using System.Collections.Generic;
using System.Text;
using ModelLibrary.Models;

namespace ModelLibrary.Models
{
    public class Factory : Facility
    {
        public new int Type { get; private set; } = 1;
        public override int Id { get; set; }
        public byte Tier { get; set; }
        public int DefProduction { get; set; }
        public int ProductionAmount { get; set; }
        public int BaseCost { get; set; } = 10;
        public ProductType ProductType { get; set; }
        public Product Product { get; set; }
        private static int lastId { get; set; }
        //public int AmounToSend { get; set; }
        public int AmountOfAvailableComponents { get; set; }

        public Factory(string factoryName, int factoryDefProduction, ProductType productType, byte tier)
        {
            Name = factoryName;
            DefProduction = factoryDefProduction;
            ProductType = productType;
            Tier = tier;
            Id = lastId;
            lastId++;
            //Type = 1;

            Product = new Product(productType, this);
            //Product.Add(Product, this);
            if (productType.Components != null)
            {
                foreach (ProductType component in productType.Components)
                {
                    new Product(component, this);
                }
            }
        }

        //przygotowanie delegata
        public delegate void NoComponentsDelegate(Facility c, ProductEventArgs e);
        public delegate void TransactionDoneDelegate(Facility c, ProductEventArgs e);
        //przygotować deklarację zdarzenia na podstawie powyższego delagata:
        public event NoComponentsDelegate NoComponents;
        public event TransactionDoneDelegate TransactionDone;

        public void Produce(ProductType productType)
        {
            //test
            BaseCost = 10 + Round.RoundNumber;
            
            if (productType.Id != Product.Id)
            {
                Product = Product.GetProduct(productType, this);
            }

            CheckComponents();
            CalculateProductionAmount();

            //TODO - uwzglednic poprzednia runde
            double produktsOnStockCosts = 0;

            if (productType.Components == null)
                Product.AmountDone = ProductionAmount;
            else if (AmountOfAvailableComponents > 0)
            {
                Product.AmountDone = Math.Min(ProductionAmount, AmountOfAvailableComponents);

                foreach (ProductType component in productType.Components)
                {
                    Product factoryComponent = Product.GetProduct(component, this);
                    produktsOnStockCosts += factoryComponent.ProductPrice * Product.AmountDone;
                    factoryComponent.AmountIn -= Product.AmountDone;
                    Console.WriteLine($"{Name} used: {Product.AmountDone} {factoryComponent.Name} (Components remained: {factoryComponent.AmountIn} {factoryComponent.Name})");
                }
            }
            Product.AmountOut += Product.AmountDone;
            produktsOnStockCosts += Product.ProductionCost * Product.AmountDone + BaseCost;
            World.Company.Cost += produktsOnStockCosts;
            World.Company.Money -= produktsOnStockCosts;
                
            /*//checking dividing by 0)
            if (Product.AmountOut > 0)
                Product.ProductCost = produktsOnStockCosts / Product.AmountOut;
            else
                Product.ProductCost = produktsOnStockCosts;*/

            Product.ProductCost = produktsOnStockCosts / (Product.AmountOut > 0 ? Product.AmountOut : 1);

            TransactionDone?.Invoke(this, new ProductEventArgs(Product.GetProduct(productType, this)));
            Console.WriteLine($"{Name} produced: {Product.AmountDone} {Product.Name} (On stock: {Product.AmountOut} {Product.Name})");
            Console.WriteLine($"{Product.Name} cost is {Product.ProductCost:c} per 1 pc.");
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
            AmountOfAvailableComponents = 0;

            if (ProductType.Components != null)
            {
                foreach (ProductType component in ProductType.Components)
                {
                    Product factoryComponent = Product.GetProduct(component, this);
                    if (factoryComponent.AmountIn > 0)
                    {
                        if (factoryComponent.AmountIn > AmountOfAvailableComponents)
                            AmountOfAvailableComponents = factoryComponent.AmountIn;
                        continue;
                    }
                    else
                    {
                        //activate event
                        NoComponents?.Invoke(this, new ProductEventArgs(factoryComponent));
                        break;
                    }
                }
            }
        }
    }
}