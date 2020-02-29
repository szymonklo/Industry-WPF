using Caliburn.Micro;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industry_WPF.ViewModels
{
    class FactoryViewModel
    {
        private string _name;
        private string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                //NotifyOfPropertyChange(() => Name);
            }
        }

        public FactoryViewModel()
        {

        }
        public FactoryViewModel(Factory factory)
        {
            Name = factory.Name;
        }
    }
}
