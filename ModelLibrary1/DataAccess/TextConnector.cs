using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.DataAccess
{
    public class TextConnector
    {
        private const string ProductTypesFile = "ProductTypes.csv";
        private const string ProductsFile = "Products.csv";

        //ProductTypes
        public static void LoadProductTypesFromFile()
        {
            ProductType.ProductTypes = ProductTypesFile.FullFilePath().LoadFile().ConvertToProductTypes();
        }
        public static void SaveProductTypesToFile()
        {
            ProductType.ProductTypes.SaveToProductTypeFile(ProductTypesFile);
        }
        //Products
        public static void LoadProductsFromFile()
        {
            Product.Products = ProductsFile.FullFilePath().LoadFile().ConvertToProducts();
        }
        public static void SaveProductsToFile()
        {
            Product.Products.SaveToProductFile(ProductsFile);
        }
    }
}
