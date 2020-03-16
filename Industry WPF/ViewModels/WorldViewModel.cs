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
        //TODO - add properties
        private FactoriesViewModel _factoriesViewModel = new FactoriesViewModel();
        private CitiesViewModel _citiesViewModel = new CitiesViewModel();
        private TransportOrdersViewModel _transportOrdersViewModel = new TransportOrdersViewModel();
        private ProductsViewModel _productsViewModel = new ProductsViewModel();

        private Company _company;
        private int _RoundNumber = Round.RoundNumber;

        public Company Company
        {
            get { return _company; }
            set
            {
                _company = value;
                NotifyOfPropertyChange(() => Company);
            }
        }
        public int RoundNumber
        {
            get { return _RoundNumber; }
            set
            {
                _RoundNumber = value;
                NotifyOfPropertyChange(() => RoundNumber);
            }
        }
        

        public WorldViewModel()
        {
            World.CreateNewWorld();
            Company = Company.Companies[0];
        }

        
        public void RefreshView()
        {
            NotifyOfPropertyChange(() => RoundNumber);

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

        public void NextRound()
        {
            Round.Go();
            RoundNumber = Round.RoundNumber;
            RefreshView();

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
    }
}
