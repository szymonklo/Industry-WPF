﻿using Caliburn.Micro;
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
        private CitiesViewModel _citiesViewModel = new CitiesViewModel();
        private TransportOrdersViewModel _transportOrdersViewModel = new TransportOrdersViewModel();

        private World _world;
        private int _RoundNumber = Round.RoundNumber;
        private Company _company = World.Company;

        public int RoundNumber
        {
            get { return _RoundNumber; }
            set
            {
                _RoundNumber = value;
                NotifyOfPropertyChange(() => RoundNumber);
            }
        }
        public Company Company
        {
            get { return _company; }
            set
            {
                _company = value;
                NotifyOfPropertyChange(() => Company);
            }
        }
        public World World
        {
            get { return _world; }
            set { _world = value; }
        }
        public WorldViewModel()
        {
            _world = World.CreateNewWorld();
        }

        public void NextRound()
        {
            Round.Go();
            RoundNumber = Round.RoundNumber;
            NotifyOfPropertyChange(() => RoundNumber);
            _factoriesViewModel.Load();
            _transportOrdersViewModel.Load();
            _citiesViewModel.Load();
            Company = World.Company;
        }

        public void ShowFactories()
        {
            ActivateItem(_factoriesViewModel);
            _factoriesViewModel.Load();
        }

        public void ShowCities()
        {
            ActivateItem(_citiesViewModel);
            _citiesViewModel.Load();
        }

        public void ShowTransportOrders()
        {
            ActivateItem(_transportOrdersViewModel);
            _transportOrdersViewModel.Load();
        }
    }
}
