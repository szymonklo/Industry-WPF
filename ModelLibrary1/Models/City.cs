using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.Models
{
    public class City : Facility
    {
        public int Population { get; set; }
        public override int Id { get; set; }
        private static int lastId { get; set; }
        public new int FacilityType { get; private set; } = 2;

        public static List<City> Cities = new List<City>();

        public City(string name, int population)
        {
            Name = name;
            Population = population;
            Id = lastId;
            lastId++;
            //Type = 2;

            Cities.Add(this);
        }

        public static void ResetId()
        {
            lastId = 0;
        }
    }
}
