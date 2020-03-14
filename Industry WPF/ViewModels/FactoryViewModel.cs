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
        //TODO - add list of ProductIn in separate GroupBox
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
        //TODO - add list of ProductOut in separate GroupBox

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
        private BindableCollection<ProductType> _productTypes;
        public BindableCollection<ProductType> ProductTypes
        {
            get { return _productTypes; }
            set
            {
                _productTypes = value;
                NotifyOfPropertyChange(() => ProductTypes);
            }
        }
        private ProductType _selectedProductType;
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
        private BindableCollection<KeyValuePair<int, int>> _productionAmountHistory;

        public BindableCollection<KeyValuePair<int, int>> ProductionAmountHistory
        {
            get { return _productionAmountHistory; }
            set
            {
                _productionAmountHistory = value;
                NotifyOfPropertyChange(() => ProductionAmountHistory);
            }
        }

        private int _productionAmount;
        public int ProductionAmount
        {
            get { return _productionAmount; }
            set
            {
                _productionAmount = value;
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
