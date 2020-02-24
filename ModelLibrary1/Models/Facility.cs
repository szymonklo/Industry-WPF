using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.Models
{
    public abstract class Facility
    {
        public string Name { get; set; }
        public abstract int Id { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public int Type()
        {
            if (this is Factory)
                return 1;
            else if (this is City)
                return 2;
            else
                return 0;
        }
    }
}
