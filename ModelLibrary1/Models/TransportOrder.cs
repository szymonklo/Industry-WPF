﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.Models
{
    public class TransportOrder
    {
        public string Name { get; set; }
        public int TransportCost { get; set; }
        public int TransportCostPerUnit { get; set; } = 1;
        public int Amount { get; set; }
        public Facility Sender { get; set; }
        public Facility Receiver { get; set; }
        public ProductType ProductType { get; set; }    //zmienić nazwę, żeby nie było konieczności użycia this.ProductType
        public int Capacity { get; set; }// = 20;
        public int ReceiversNumber { get; set; }// = 20;
        public int Id { get; private set; }
        private static int lastId { get; set; }


        private static Dictionary<Tuple<int, int, int, int, int>, TransportOrder> _transportOrders
            = new Dictionary<Tuple<int, int, int, int, int>, TransportOrder>();

        public static Dictionary<Tuple<int, int, int, int, int>, TransportOrder> TransportOrders
        {
            get => _transportOrders;
            set => _transportOrders = value;
        }

        //przygotowanie delegata
        public delegate void FewProductsToSendDelegate(Facility c, ProductEventArgs e);
        //przygotować deklarację zdarzenia na podstawie powyższego delagata:
        public event FewProductsToSendDelegate FewProductsToSend;

        public TransportOrder(int capacity = 20, int receiversNumber = 1)
        {
            Id = lastId;
            lastId++;
            Capacity = capacity;
            ReceiversNumber = receiversNumber;
        }
        public TransportOrder(Facility sender, Facility receiver, ProductType productType, int capacity, int receiversNumber)
            :this()
        {
            Sender = sender;
            Receiver = receiver;
            ProductType = productType;
            Capacity = capacity;
            ReceiversNumber = receiversNumber;
            SetName();
            Add();

        }

        public void SetName()
        {
            Name = $"{ProductType.Name} from {Sender.Name} to {Receiver.Name}";
        }

        public TransportOrder(int senderType, int senderId, int receiverType, int receiverId, int productTypeId, int capacity, int receiversNumber)
            : this(Facility.GetFacility(senderType, senderId), Facility.GetFacility(receiverType, receiverId), ProductType.GetProductType(productTypeId), capacity, receiversNumber)
        {
        }

        public void Add()
        {
            Tuple<int, int, int, int, int> tokey = new Tuple<int, int, int, int, int>
                (Sender.FacilityType, Sender.Id, Receiver.FacilityType, Receiver.Id, ProductType.Id);
            TransportOrders.Add(tokey, this);
        }
        public static TransportOrder GetTransportOrder(Facility sender, Facility receiver, ProductType productType)
        {
            Tuple<int, int, int, int, int> tokey = new Tuple<int, int, int, int, int>
                (sender.FacilityType, sender.Id, receiver.FacilityType, receiver.Id, productType.Id);

            TransportOrder transportOrder = null;
            if (TransportOrders.ContainsKey(tokey))
                transportOrder = TransportOrders[tokey];
            return transportOrder;
        }
        public void Go()
        {
            Product productS = Product.GetProduct(ProductType, Sender);

            if (Round.RoundNumber <=8)  //test
            Capacity = productS.AmountOut / ReceiversNumber;
            if (productS != null)// && productS.AmountOut > 0)
            //Sender.Products.Contains(ProductType.Id) && Sender.Products[ProductType.Id].AmountOut > 0)
            {
                if (productS.AmountOut < Capacity)
                {
                    //FewProductsToSend?.Invoke(Sender, new ProductEventArgs(Sender.Products[ProductType.Id]));
                }

                Amount = Math.Min(Capacity, productS.AmountOut);
                productS.AmountOut -= Amount;
                productS.ValueOut -= Amount* productS.ProductCost;
                Product productIn;
                Product productR = Product.GetProduct(ProductType, Receiver);
                if (productR != null)
                {
                    productIn = productR;
                }
                else
                {
                    productIn = new Product(ProductType, Receiver, 0);
                    //Receiver.Products.Add(new Product(ProductType, Receiver, Amount));
                }

                double productInCost = productIn.AmountIn * productIn.ProductCost;
                productIn.AmountIn += Amount;
                TransportCost = TransportCostPerUnit * Amount;
                productInCost += Amount * productS.ProductCost + TransportCost;
                Company.Companies[0].Cost += TransportCost;
                Company.Companies[0].Money -= TransportCost;
                if (productIn.AmountIn > 0)
                    productIn.ProductCost = productInCost / productIn.AmountIn;

                Console.WriteLine($"Transported {Capacity} {ProductType.Name}");
                Console.WriteLine($"In {Sender.Name} (origin) left {productS.AmountOut} {ProductType.Name}");
                Console.WriteLine($"In {Receiver.Name} (destination) there are {productIn.AmountIn} {productIn.Name}\n");
            }
            else
            {
                Console.WriteLine("no product to send");
                FewProductsToSend?.Invoke(Sender, new ProductEventArgs(productS));
            }
        }
    }
}