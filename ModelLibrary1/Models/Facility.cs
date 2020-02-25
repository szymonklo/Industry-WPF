using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelLibrary.Models
{
    public abstract class Facility
    {
        public string Name { get; set; }
        public abstract int Id { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        private int _type;
        public int Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (this is Factory)
                    _type = 1;
                else if (this is City)
                    _type = 2;
                else
                    _type = 0;
            }
        }

        public static Facility GetFacility(int type, int id)
        {
            if (type == 1)
                return Factory.Factories.Where(x => x.Id == id).First();
            else if (type == 2)
                return City.Cities.Where(x => x.Id == id).First();
            else
                return null;
        }
    }
}
