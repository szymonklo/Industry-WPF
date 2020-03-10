using Caliburn.Micro;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industry_WPF.ViewModels
{
    class FactoryViewModel : Screen, INotifyPropertyChangedEx
    {
        private Factory _factory;
        public Factory Factory
        {
            get { return _factory; }
            set
            {
                _factory = value;
                NotifyOfPropertyChange(() => Factory);
            }
        }

        private string _factoryName;
        public string FactoryName
        {
            get { return _factoryName; }
            set
            {
                _factoryName = value;
                NotifyOfPropertyChange(() => FactoryName);
                //test
                Factory.Name = value;
            }
        }
        private BindableCollection<Product> _components;
        public BindableCollection<Product> Components
        {
            get { return _components; }
            set
            {
                _components = value;
                NotifyOfPropertyChange(() => Components);
            }
        }

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
        private Product _product;
        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                NotifyOfPropertyChange(() => Product);
            }
        }

        public FactoryViewModel()
        {

        }
        public FactoryViewModel(Factory factory)
        {
            Factory = factory;
            FactoryName = factory.Name;
            Components = new BindableCollection<Product>(factory.Components);
            Products = new BindableCollection<Product>(Factory.Products);
        }

        public void Load()
        {
            FactoryName = Factory.Name;
            Components = new BindableCollection<Product>(Factory.Components);
            Products = new BindableCollection<Product>(Factory.Products);
        }
    }
}
