using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Models;
using ModelLibrary.DataAccess;

namespace Industry_WPF.ViewModels
{
    public class WorldViewModel : Conductor<object>, INotifyPropertyChangedEx
    {
        private FactoriesViewModel _factoriesViewModel = new FactoriesViewModel();
        private CitiesViewModel _citiesViewModel = new CitiesViewModel();
        private TransportOrdersViewModel _transportOrdersViewModel = new TransportOrdersViewModel();
        private ProductsViewModel _productsViewModel = new ProductsViewModel();

        //private World _world;
        private int _RoundNumber = Round.RoundNumber;
        private Company _company;

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
        //public World World
        //{
        //    get { return _world; }
        //    set { _world = value; }
        //}
        public WorldViewModel()
        {
            World.CreateNewWorld();
            Company = Company.Companies[0];
        }

        public void SaveAll()
        {
            TextConnector.SaveProductTypesToFile();
            TextConnector.SaveFactoriesToFile();
            TextConnector.SaveCitiesToFile();
            TextConnector.SaveProductsToFile();
            TextConnector.SaveTransportOrdersToFile();
            TextConnector.SaveCompaniesToFile();
            TextConnector.SaveRoundsToFile();
        }
        
        public void LoadAll()
        {
            World.ClearWorld();

            TextConnector.LoadProductTypesFromFile();
            TextConnector.LoadFactoriesFromFile();
            TextConnector.LoadCitiesFromFile();
            TextConnector.LoadProductsFromFile();
            TextConnector.LoadTransportOrdersFromFile();
            TextConnector.LoadCompaniesFromFile();
            TextConnector.LoadRoundFromFile();

            RefreshView();
        }

        public void NextRound()
        {
            Round.Go();
            RoundNumber = Round.RoundNumber;
            RefreshView();

        }
        public void RefreshView()
        {
            NotifyOfPropertyChange(() => RoundNumber);

            //refreshing
            if (Company.Companies.Count > 0)
                Company = Company.Companies[0];

            _factoriesViewModel.Load();
            _transportOrdersViewModel.Load();
            _productsViewModel.Load();
            _citiesViewModel.Load();
            
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
        public void ShowProducts()
        {
            ActivateItem(_productsViewModel);
            _productsViewModel.Load();
        }
    }
}
