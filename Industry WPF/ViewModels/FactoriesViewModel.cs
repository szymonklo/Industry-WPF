using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace Industry_WPF.ViewModels
{
    class FactoriesViewModel : Conductor<object>, INotifyPropertyChangedEx
    {
        private BindableCollection<Factory> _factories;// = new BindableCollection<Factory>(World.Factories);
        private Factory _selectedFactory;

        private BindableCollection<FactoryViewModel> Items = new BindableCollection<FactoryViewModel>();
        public Factory SelectedFactory
        {
            get { return _selectedFactory; }
            set
            {
                _selectedFactory = value;
                NotifyOfPropertyChange(() => SelectedFactory);
            }
        }
        public BindableCollection<Factory> Factories
        {
            get { return _factories; }
            set
            {
                _factories = value;
                NotifyOfPropertyChange(() => Factories);
            }
        }

        public FactoriesViewModel()
        {
            //Factories = new BindableCollection<Factory>(World.Factories);
            Items.Add(new FactoryViewModel());
        }

        public void Load()
        {
            Factories = new BindableCollection<Factory>(Factory.Factories);
        }

        public void ShowFactory()
        {
            var conductor = this.Parent as IConductor;
            var f = new FactoryViewModel(SelectedFactory);

            conductor.ActivateItem(f);
            
            //var f = new FactoryViewModel(SelectedFactory);
            //Items.Add(f);
            //ActivateItem(f);
        }
    }
}
