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
                //test
                //TransportOrder.Id = value;
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
        }

        public void Load()
        {
            TransportOrderName = TransportOrder.Name;
            TransportOrderId = TransportOrder.Id;
        }

        //public void SetProduct()
        //{
        //    TransportOrder.Set(SelectedProductType);
        //    Load();
        //}
    }
}
