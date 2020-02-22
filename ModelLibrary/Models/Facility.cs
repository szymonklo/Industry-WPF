using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.Models
{
    public abstract class Facility
    {
        public string Name { get; set; }
        //public virtual ProductsKeyed Products { get; set; } = new ProductsKeyed();
        public abstract int Id { get; set; }
        //private int _type;
        public List<Product> Products { get; set; } = new List<Product>();
        public int Type()
        {
            if (this is Factory)
                return 1;
            else if (this is City)
                return 2;
            else
                return 3;
        }
        //{
        //    get
        //    {
        //        return _type;
        //    }
        //    private set
        //    {
        //        //if (this is Factory)
        //        //    _type = 1;
        //        //else if (this is City)
        //        //    _type = 2;
        //        //else
        //        //    _type = 0;
        //    }
        //}
    }
}
