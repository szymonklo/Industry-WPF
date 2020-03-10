﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    public class FactoryType
    {
        public string Name { get; set; }
        public byte Tier { get; set; }
        public int DefProduction { get; set; }
        public double BaseCost { get; set; } = 10;
        public int ConstructionCost { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public static List<FactoryType> FactoryTypes { get; private set; }

        public FactoryType (string name, byte tier, int defProduction, double baseCost, int construcionCost, List<ProductType> productTypes)
        {
            Name = name;
            Tier = tier;
            DefProduction = defProduction;
            BaseCost = baseCost;
            ConstructionCost = construcionCost;
            ProductTypes = productTypes;
        }

        //TODO - add this class to TextConnector
    }
}
