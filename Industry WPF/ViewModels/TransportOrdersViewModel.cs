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
                return _command ?? (_command = new Commands.RelayCommand( x => { ExecuteCommand(); }));
            }
        }
        private static void ExecuteCommand()
        {
            MessageBox.Show("Button clicked");
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

        public TransportOrdersViewModel()
        {
            //Factories = new BindableCollection<Factory>(World.Factories);
        }

        public void Load()
        {
            TransportOrders = new BindableCollection<TransportOrder>(TransportOrder.TransportOrders.Values);
        }

        public void Details(object sender, EventArgs e)
        {
            
        }

        public void Details2(object sender, EventArgs e)
        {
            
        }
    }
}
