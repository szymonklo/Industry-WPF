using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ModelLibrary.Models
{
    public class ProductType : INotifyPropertyChanged
    {
        public int Id { get; protected set; }
        private string _name;
        private static int _lastId;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public byte Group { get; set; }
        public double DefPrice { get; set; }
        public double DefCost { get; set; }

        public List<ProductType> ComponentTypes { get; set; } = new List<ProductType>();

        public static List<ProductType> ProductTypes = new List<ProductType>();

        public ProductType()
        { }
        public ProductType(byte group, string productName, double defPrice, List<ProductType> components = null)
        {
            //TODO - dodać zabezpieczenie przed użyciem ponownie numeru id
            if (!(this is Product))
            {
                Id = _lastId;// _nextId++;
                _lastId++;
            }
            Group = group;
            Name = productName;
            DefPrice = defPrice;
            if (components != null)
                ComponentTypes = components;

            //something better...
            if (!(this is Product))
                ProductTypes.Add(this);
        }

        public static void ResetId()
        {
            _lastId = 0;
        }

        public static ProductType GetProductType(int id)
        {
            return ProductTypes.Where(x => x.Id == id).First();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
