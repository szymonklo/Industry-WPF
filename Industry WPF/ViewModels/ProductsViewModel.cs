using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using ModelLibrary.Models;
using Industry_WPF.Views;

namespace Industry_WPF.ViewModels
{
    public class ProductsViewModel : Screen, INotifyPropertyChangedEx
    {
        private BindableCollection<Product> _products;
        public BindableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        public ProductsViewModel()
        {
        }

        public void Load()
        {
            Products = new BindableCollection<Product>(Product.GetAll());
        }
    }
}
