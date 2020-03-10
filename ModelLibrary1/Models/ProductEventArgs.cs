using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.Models
{
    //TODO - zaktualizować eventy
    public class ProductEventArgs : EventArgs
    {
        public Product Product { get; set; }

        public ProductEventArgs(Product product)
        {
            Product = product;
        }
    }
}