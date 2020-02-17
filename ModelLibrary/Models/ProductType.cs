using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.Models
{
    public class ProductType
    {
        public ProductType()
        { }
        public ProductType(int id, byte group, string productName, double defPrice, List<ProductType> components = null)
        {
            Id = id;// _nextId++;                    //dodać zabezpieczenie przed użyciem ponownie numeru id
            Group = group;
            Name = productName;
            DefPrice = defPrice;
            Components = components;
        }
        public int Id { get; }
        public string Name { get; set; }
        public byte Group { get; set; }
        public double DefPrice { get; set; }
        public double DefCost { get; set; }
        public List<ProductType> Components { get; set; }
        //private static int _nextId = 1;

    }
}
