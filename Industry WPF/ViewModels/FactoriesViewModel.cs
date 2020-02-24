using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace Industry_WPF.ViewModels
{
    class FactoriesViewModel : Screen, INotifyPropertyChangedEx
    {
        private BindableCollection<Factory> _factories;// = new BindableCollection<Factory>(World.Factories);

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
        }

        public void Load()
        {
            Factories = new BindableCollection<Factory>(Factory.Factories);
        }
    }
}
