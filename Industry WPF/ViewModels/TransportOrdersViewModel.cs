using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Models;
using System.Windows.Input;
using System.Windows;

namespace Industry_WPF.ViewModels
{
    class TransportOrdersViewModel : Screen, INotifyPropertyChangedEx
    {
        private BindableCollection<TransportOrder> _transportOrders;

        private ICommand _command;


        TransportOrderViewModel TransportOrderViewModel { get; set; }
        TransportOrder TransportOrder { get; set; }

        public BindableCollection<TransportOrder> TransportOrders
        {
            get { return _transportOrders; }
            set
            {
                _transportOrders = value;
                NotifyOfPropertyChange(() => TransportOrders);
            }
        }
        public ICommand Command
        {
            get
            {
                return _command ?? (_command = new Commands.RelayCommand(x => { ExecuteCommand(x); }));
            }
        }
        
        public TransportOrdersViewModel()
        {
        }

        public void Load()
        {
            TransportOrders = new BindableCollection<TransportOrder>(TransportOrder.TransportOrders.Values);
        }

        public void ShowTransportOrder(TransportOrder transportOrder)
        {
            if (transportOrder is null)
                return;
            var conductor = this.Parent as IConductor;
            TransportOrderViewModel = new TransportOrderViewModel(transportOrder);

            conductor.ActivateItem(TransportOrderViewModel);
        }
        public void CreateNewTransportOrder()
        {
            TransportOrder = new TransportOrder();
            var conductor = this.Parent as IConductor;
            TransportOrderViewModel = new TransportOrderViewModel(TransportOrder);

            conductor.ActivateItem(TransportOrderViewModel);
        }
        private void ExecuteCommand(object x)
        {
            this.ShowTransportOrder((TransportOrder)x);
        }
    }
}
