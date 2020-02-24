using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace Industry_WPF.ViewModels
{
    class CitiesViewModel : Screen, INotifyPropertyChangedEx
    {
        private BindableCollection<City> _cities;

        public BindableCollection<City> Cities
        {
            get { return _cities; }
            set
            {
                _cities = value;
                NotifyOfPropertyChange(() => Cities);
            }
        }

        public CitiesViewModel()
        {

        }

        public void Load()
        {
            Cities = new BindableCollection<City>(City.Cities);
        }
    }
}
