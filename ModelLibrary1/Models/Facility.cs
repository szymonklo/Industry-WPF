﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelLibrary.Models
{
    public abstract class Facility
    {
        protected string _name;
        public string Name
        { 
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public abstract int Id { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public abstract int FacilityType { get; protected set; }
        public static List<Facility> Facilities = new List<Facility>();


        public Facility()
        {
            //SetType();
        }

        //DONE  - zmienić i nie odwoływać się do klas dziedziczących z Facility
        //public void SetType()
        //{
        //    if (this is Factory)
        //        FacilityType = 1;
        //    else if (this is City)
        //        FacilityType = 2;
        //    else
        //        FacilityType = 0;
        //}


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
