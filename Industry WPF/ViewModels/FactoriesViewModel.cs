using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Models;
using System.Windows.Input;

namespace Industry_WPF.ViewModels
{
    class FactoriesViewModel : Conductor<object>, INotifyPropertyChangedEx
    {
        private FactoryViewModel _factoryViewModel;
        private BindableCollection<FactoryViewModel> _items = new BindableCollection<FactoryViewModel>();

        private Factory _selectedFactory;
        private BindableCollection<Factory> _factories;

        private FactoryType _selectedFactoryType;
        private BindableCollection<FactoryType> _factoryTypes;

        private ICommand _command;

        public FactoryViewModel FactoryViewModel
        {
            get { return _factoryViewModel; }
            set
            {
                _factoryViewModel = value;
                NotifyOfPropertyChange(() => SelectedFactory);
            }
        }
        public BindableCollection<FactoryViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value; 
                NotifyOfPropertyChange(() => Items);
            }
        }
        
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
        public FactoryType SelectedFactoryType
        {
            get { return _selectedFactoryType; }
            set
            {
                _selectedFactoryType = value;
                NotifyOfPropertyChange(() => SelectedFactoryType);
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
        

        public FactoriesViewModel()
        {
            Items.Add(new FactoryViewModel());
        }


        public void Load()
        {
            Factories = new BindableCollection<Factory>(Factory.Factories);
            FactoryTypes = new BindableCollection<FactoryType>(FactoryType.FactoryTypes);
            FactoryViewModel?.Load();
        }

        public void ShowFactory(Factory factory)
        {
            if (factory is null)
                return;
            var conductor = this.Parent as IConductor;
            FactoryViewModel = new FactoryViewModel(factory);

            conductor.ActivateItem(FactoryViewModel);
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
            this.ShowFactory((Factory)x);
        }
    }
}
