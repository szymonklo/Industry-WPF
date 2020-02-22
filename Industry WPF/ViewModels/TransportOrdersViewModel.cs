using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace Industry_WPF.ViewModels
{
    class TransportOrdersViewModel : Screen, INotifyPropertyChangedEx
    {
        private BindableCollection<TransportOrder> _transportOrders;

        public BindableCollection<TransportOrder> TransportOrders
        {
            get { return _transportOrders; }
            set
            {
                _transportOrders = value;
                NotifyOfPropertyChange(() => TransportOrders);
            }
        }

        public TransportOrdersViewModel()
        {
            //Factories = new BindableCollection<Factory>(World.Factories);
        }

        public void Load()
        {
            TransportOrders = new BindableCollection<TransportOrder>(TransportOrder.TransportOrders.Values);
        }
    }
}
