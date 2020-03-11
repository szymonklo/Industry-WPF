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

        private BindableCollection<FactoryType> _factoryTypes;
        private FactoryType _selectedFactoryType;

        private BindableCollection<FactoryViewModel> Items = new BindableCollection<FactoryViewModel>();

        public FactoryViewModel FactoryViewModel;
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
        public BindableCollection<FactoryType> FactoryTypes
        {
            get { return _factoryTypes; }
            set
            {
                _factoryTypes = value;
                NotifyOfPropertyChange(() => FactoryTypes);
            }
        }
        public FactoryType SelectedFactoryType
        {
            get { return _selectedFactoryType; }
            set
            {
                _selectedFactoryType = value;
                NotifyOfPropertyChange(() => SelectedFactoryType);
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
            FactoryTypes = new BindableCollection<FactoryType>(FactoryType.FactoryTypes);
            FactoryViewModel?.Load();
        }

        public void ShowFactory()
        {
            if (SelectedFactory is null)
                return;
            var conductor = this.Parent as IConductor;
            FactoryViewModel = new FactoryViewModel(SelectedFactory);
            
            conductor.ActivateItem(FactoryViewModel);
            
            //var f = new FactoryViewModel(SelectedFactory);
            //Items.Add(f);
            //ActivateItem(f);
        }

        public void BuildNewFactory()
        {
            if (SelectedFactoryType is null)
                return;

            SelectedFactory = new Factory(SelectedFactoryType);
            var conductor = this.Parent as IConductor;
            FactoryViewModel = new FactoryViewModel(SelectedFactory);

            conductor.ActivateItem(FactoryViewModel);
        }
    }
}
