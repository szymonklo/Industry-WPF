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
    public class ProductViewModel : Screen, INotifyPropertyChangedEx
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
            Products = new BindableCollection<Product>(Product.GetAll());
            //Products = new BindableCollection<Product>();
            //World world = new World();
        }

        public void Load()
        {
            Products = new BindableCollection<Product>(Product.GetAll());

            //Products = new BindableCollection<Product>(Product.ProductD.Values.ToList());
            //ProductView.ProductDataGrid.Columns.Where(x => x.Header.ToString() == "Name").First().DisplayIndex = 0;
        }
    }
}
