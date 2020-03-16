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
        private int _transportOrderId;
        private string _transportOrderName;

        private BindableCollection<Facility> _facilities;
        private Facility _sender;
        private Facility _receiver;
        private ProductType _productType;
        private BindableCollection<ProductType> _productTypes;
        private int _capacity;


        public TransportOrder TransportOrder
        {
            get { return _transportOrder; }
            set
            {
                _transportOrder = value;
                NotifyOfPropertyChange(() => TransportOrder);
            }
        }
        public int TransportOrderId
        {
            get { return _transportOrderId; }
            set
            {
                _transportOrderId = value;
                NotifyOfPropertyChange(() => TransportOrderId);
            }
        }
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
        public BindableCollection<Facility> Facilities
        {
            get { return _facilities; }
            set
            {
                _facilities = value;
                NotifyOfPropertyChange(() => Facilities);
            }
        }
        public Facility Sender
        {
            get { return _sender; }
            set
            {
                _sender = value;
                NotifyOfPropertyChange(() => Sender);
            }
        }
        public Facility Receiver
        {
            get { return _receiver; }
            set
            {
                _receiver = value;
                NotifyOfPropertyChange(() => Receiver);
            }
        }
        public ProductType ProductType
        {
            get { return _productType; }
            set
            {
                _productType = value;
                NotifyOfPropertyChange(() => ProductType);
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
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                _capacity = value;
                NotifyOfPropertyChange(() => Capacity);
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
            Capacity = TransportOrder.Capacity;
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
            Capacity = TransportOrder.Capacity;
        }
        public void Apply()
        {
            if (Sender != null && Receiver != null && ProductType != null)
            {
                TransportOrder.Sender = Sender;
                TransportOrder.Receiver = Receiver;
                TransportOrder.ProductType = ProductType;
                TransportOrder.Capacity = Capacity;

                TransportOrder.SetName();
                TransportOrderName = TransportOrder.Name;

                if (TransportOrder.GetTransportOrder(Sender, Receiver, ProductType) == null)
                    TransportOrder.Add();
            }
        }
    }
}
