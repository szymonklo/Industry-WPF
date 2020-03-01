using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Caliburn.Micro;

namespace Industry_WPF.ViewModels
{
    class ShellViewModel : NavigationWindow//, INotifyPropertyChangedEx
    {
        public bool IsNotifying { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyOfPropertyChange(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
