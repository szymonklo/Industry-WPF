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
        public int Type { get; set; }


        public Facility()
        {
            SetType();
        }

        public void SetType()
        {
            if (this is Factory)
                Type = 1;
            else if (this is City)
                Type = 2;
            else
                Type = 0;
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
