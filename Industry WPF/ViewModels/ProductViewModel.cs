using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using ModelLibrary.Models;

namespace Industry_WPF.ViewModels
{
    public class ProductViewModel : Screen
    {
        private BindableCollection<Product> _products;
        public BindableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        //List<Product> Products { get; set; }
        public ProductViewModel()
        {
            Products = new BindableCollection<Product>(Product.ProductD.Values.ToList());
            //Products = new BindableCollection<Product>();
            //World world = new World();
        }

        public void Load()
        {
            Products = new BindableCollection<Product>(Product.ProductD.Values.ToList());
        }
    }
}
