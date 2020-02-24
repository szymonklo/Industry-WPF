using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.DataAccess
{
    public class TextConnector
    {
        private const string ProductTypesFile = "ProductTypes.csv";

        public static void LoadProductTypesFromFile()
        {
            ProductType.ProductTypes = ProductTypesFile.FullFilePath().LoadFile().ConvertToProductTypes();
        }
        public static void SaveProductTypesToFile()
        {
            ProductType.ProductTypes.SaveToProductTypeFile(ProductTypesFile);
        }
    }
}
