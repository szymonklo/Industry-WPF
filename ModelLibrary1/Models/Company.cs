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
        public static List<Company> Companies = new List<Company>();

        public Company(string name)
        {
            Name = name;
            Companies.Add(this);
        }

        public void CalculateProfit()
        {
            Profit = Income - Cost;
        }
    }
}
