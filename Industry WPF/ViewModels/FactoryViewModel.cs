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
        private string _factoryName;
        private BindableCollection<Product> _components;
        private BindableCollection<Product> _products;
        private BindableCollection<ProductType> _productTypes;
        private ProductType _selectedProductType;
        private Product _product;
        private BindableCollection<KeyValuePair<int, int>> _productionAmountHistory;
        private int _productionAmount;

        public Factory Factory
        {
            get { return _factory; }
            set
            {
                _factory = value;
                NotifyOfPropertyChange(() => Factory);
            }
        }
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
        //TODO - add list of ProductIn in separate GroupBox
        public BindableCollection<Product> Components
        {
            get { return _components; }
            set
            {
                _components = value;
                NotifyOfPropertyChange(() => Components);
            }
        }
        //TODO - add list of ProductOut in separate GroupBox
        public BindableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        public BindableCollection<ProductType> ProductTypes
        {
            get { return _productTypes; }
            set
            {
                _productTypes = value;
                NotifyOfPropertyChange(() => ProductTypes);
            }
        }
        public ProductType SelectedProductType
        {
            get { return _selectedProductType; }
            set
            {
                _selectedProductType = value;
                
                NotifyOfPropertyChange(() => SelectedProductType);
                NotifyOfPropertyChange(() => ProductTypes);
                NotifyOfPropertyChange(() => Components);
            }
        }
        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                NotifyOfPropertyChange(() => Product);
            }
        }
        public BindableCollection<KeyValuePair<int, int>> ProductionAmountHistory
        {
            get { return _productionAmountHistory; }
            set
            {
                _productionAmountHistory = value;
                NotifyOfPropertyChange(() => ProductionAmountHistory);
            }
        }
        public int ProductionAmount
        {
            get { return _productionAmount; }
            set
            {
                _productionAmount = value;
                NotifyOfPropertyChange(() => ProductionAmount);
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
            ProductTypes = new BindableCollection<ProductType>(Factory.ProductTypes);
            SelectedProductType = Factory.ProductType;
            ProductionAmountHistory = new BindableCollection<KeyValuePair<int, int>>(Factory.ProductionAmountHistory);
        }

        public void Load()
        {
            FactoryName = Factory.Name;
            Components = new BindableCollection<Product>(Factory.Components);
            Products = new BindableCollection<Product>(Factory.Products);
            
            if (SelectedProductType ==  null)
                ProductTypes = new BindableCollection<ProductType>(Factory.ProductTypes);

            SelectedProductType = Factory.ProductType;
            ProductionAmountHistory = new BindableCollection<KeyValuePair<int, int>>(Factory.ProductionAmountHistory);
        }

        public void SetProduct()
        {
            Factory.Set(SelectedProductType);
            Load();
        }
    }
}
