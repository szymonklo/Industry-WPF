using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace ModelLibrary.Models
{
    public class Company
    {
        public string Name { get; set; }
        public double Money { get; set; } = 1000;
        public double Income { get; set; }
        public double Cost { get; set; }
        public double Profit { get; set; }

        public Company(string name)
        {
            Name = name;
        }

        public void CalculateProfit()
        {
            Profit = Income - Cost;
        }
    }
}
