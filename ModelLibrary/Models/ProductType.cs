using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ModelLibrary.Models
{
    public class ProductType : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ProductType()
        { }
        public ProductType(int id, byte group, string productName, double defPrice, List<ProductType> components = null)
        {
            Id = id;// _nextId++;                    //dodać zabezpieczenie przed użyciem ponownie numeru id
            Group = group;
            Name = productName;
            DefPrice = defPrice;
            Components = components;
        }
        public int Id { get; }

        private string _name;
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
        public List<ProductType> Components { get; set; }
        //private static int _nextId = 1;

    }
}
