using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.DataAccess
{
    public class TextConnector
    {
        private const string ProductTypesFile = "ProductTypes.csv";
        private const string FactoriesFile = "Factories.csv";
        private const string CitiesFile = "Cities.csv";
        private const string ProductsFile = "Products.csv";
        private const string TransportOrdersFile = "TransportOrders.csv";
        private const string CompaniesFile = "Companies.csv";
        private const string RoundsFile = "Rounds.csv";

        //ProductTypes
        public static void LoadProductTypesFromFile()
        {
            ProductTypesFile.FullFilePath().LoadFile().ConvertToProductTypes();
        }
        public static void SaveProductTypesToFile()
        {
            ProductType.ProductTypes.SaveToProductTypesFile(ProductTypesFile);
        }
        //Factories
        public static void LoadFactoriesFromFile()
        {
            FactoriesFile.FullFilePath().LoadFile().ConvertToFactories();
        }
        public static void SaveFactoriesToFile()
        {
            Factory.Factories.SaveToFactoriesFile(FactoriesFile);
        }
        //Cities
        public static void LoadCitiesFromFile()
        {
            CitiesFile.FullFilePath().LoadFile().ConvertToCities();
        }
        public static void SaveCitiesToFile()
        {
            City.Cities.SaveToCitiesFile(CitiesFile);
        }
        //Products
        public static void LoadProductsFromFile()
        {
            ProductsFile.FullFilePath().LoadFile().ConvertToProducts();
        }
        public static void SaveProductsToFile()
        {
            Product.Products.SaveToProductsFile(ProductsFile);
        }
        //TransportOrders
        public static void LoadTransportOrdersFromFile()
        {
            TransportOrdersFile.FullFilePath().LoadFile().ConvertToTransportOrders();
        }
        public static void SaveTransportOrdersToFile()
        {
            TransportOrder.TransportOrders.SaveToTransportOrdersFile(TransportOrdersFile);
        }
        //Company
        public static void LoadCompaniesFromFile()
        {
            CompaniesFile.FullFilePath().LoadFile().ConvertToCompanies();
        }
        public static void SaveCompaniesToFile()
        {
            Company.Companies.SaveToCompaniesFile(CompaniesFile);
        }
        //Round
        public static void LoadRoundFromFile()
        {
            RoundsFile.FullFilePath().LoadFile().ConvertToRounds();
        }
        public static void SaveRoundsToFile()
        {
            TextConnectorProcessor.SaveToRoundsFile(RoundsFile);
        }
    }
}
