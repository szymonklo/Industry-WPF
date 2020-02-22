using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Models;

namespace Industry_WPF.ViewModels
{
    public class WorldViewModel : Conductor<object>, INotifyPropertyChangedEx
    {
        private FactoriesViewModel _factoriesViewModel = new FactoriesViewModel();

        private World _world;
        private int _RoundNumber = Round.RoundNumber;

        public int RoundNumber
        {
            get { return _RoundNumber; }
            set
            {
                _RoundNumber = value;
                NotifyOfPropertyChange(() => RoundNumber);
            }
        }

        public World World
        {
            get { return _world; }
            set { _world = value; }
        }
        public WorldViewModel()
        {
            World world = World.CreateNewWorld();
        }

        public void NextRound()
        {
            Round.Go();
            RoundNumber = Round.RoundNumber;
            NotifyOfPropertyChange(() => RoundNumber);
            _factoriesViewModel.Load();

        }

        public void ShowFactories()
        {
            ActivateItem(_factoriesViewModel);
            _factoriesViewModel.Load();
        }
    }
}
