using Caliburn.Micro;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industry_WPF.ViewModels
{
    class TransportOrderViewModel : Screen, INotifyPropertyChangedEx
    {
        private TransportOrder _transportOrder;
        public TransportOrder TransportOrder
        {
            get { return _transportOrder; }
            set
            {
                _transportOrder = value;
                NotifyOfPropertyChange(() => TransportOrder);
            }
        }

        private string _transportOrderName;
        public string TransportOrderName
        {
            get { return _transportOrderName; }
            set
            {
                _transportOrderName = value;
                NotifyOfPropertyChange(() => TransportOrderName);
                //test
                TransportOrder.Name = value;
            }
        }
        private int _transportOrderId;
        public int TransportOrderId
        {
            get { return _transportOrderId; }
            set
            {
                _transportOrderId = value;
                NotifyOfPropertyChange(() => TransportOrderId);
            }
        }

        private BindableCollection<Facility> _facilities;
        public BindableCollection<Facility> Facilities
        {
            get { return _facilities; }
            set
            {
                _facilities = value;
                NotifyOfPropertyChange(() => Facilities);
            }
        }

        private Facility _sender;
        public Facility Sender
        {
            get { return _sender; }
            set
            {
                _sender = value;
                NotifyOfPropertyChange(() => Sender);
            }
        }
        private Facility _receiver;
        public Facility Receiver
        {
            get { return _receiver; }
            set
            {
                _receiver = value;
                NotifyOfPropertyChange(() => Receiver);
            }
        }
        private ProductType _productType;
        public ProductType ProductType
        {
            get { return _productType; }
            set
            {
                _productType = value;
                NotifyOfPropertyChange(() => ProductType);
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

        public TransportOrderViewModel()
        {

        }
        public TransportOrderViewModel(TransportOrder transportOrder)
        {
            TransportOrder = transportOrder;
            TransportOrderName = transportOrder.Name;
            TransportOrderId = transportOrder.Id;
            ProductType = transportOrder.ProductType;
            Facilities = new BindableCollection<Facility>(Facility.Facilities);
            ProductTypes = new BindableCollection<ProductType>(ProductType.ProductTypes);
            Sender = transportOrder.Sender;
            Receiver = transportOrder.Receiver;
        }

        public void Load()
        {
            TransportOrderName = TransportOrder.Name;
            TransportOrderId = TransportOrder.Id;
            Facilities = new BindableCollection<Facility>(Facility.Facilities);
            ProductType = TransportOrder.ProductType;
            ProductTypes = new BindableCollection<ProductType>(ProductType.ProductTypes);
            Sender = TransportOrder.Sender;
            Receiver = TransportOrder.Receiver;
        }
        public void Apply()
        {
            TransportOrder.Sender = Sender;
            TransportOrder.Receiver = Receiver;
            TransportOrder.ProductType = ProductType;
        }

        //public void SetProduct()
        //{
        //    TransportOrder.Set(SelectedProductType);
        //    Load();
        //}
    }
}
