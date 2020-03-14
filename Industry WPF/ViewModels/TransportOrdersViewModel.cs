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
        private ICommand _command;
        public ICommand Command
        {
            get
            {
                return _command ?? (_command = new Commands.RelayCommand(x => { ExecuteCommand(x); }));
            }
        }
        private void ExecuteCommand(object x)
        {
            //MessageBox.Show("Button clicked");
            this.ShowTransportOrder((TransportOrder)x);
        }

        public void ShowTransportOrder(TransportOrder transportOrder)
        {
            if (transportOrder is null)
                return;
            var conductor = this.Parent as IConductor;
            TransportOrderViewModel = new TransportOrderViewModel(transportOrder);

            conductor.ActivateItem(TransportOrderViewModel);
        }

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
        TransportOrderViewModel TransportOrderViewModel { get; set; }
        TransportOrder TransportOrder { get; set; }

        public TransportOrdersViewModel()
        {
        }

        public void Load()
        {
            TransportOrders = new BindableCollection<TransportOrder>(TransportOrder.TransportOrders.Values);
        }

        public void CreateNewTransportOrder()
        {
            TransportOrder = new TransportOrder();
            var conductor = this.Parent as IConductor;
            TransportOrderViewModel = new TransportOrderViewModel(TransportOrder);

            conductor.ActivateItem(TransportOrderViewModel);
        }

    }
}
