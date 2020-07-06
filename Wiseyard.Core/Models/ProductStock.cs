using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class ProductStock
    {
        public int ProductId { get; set; }
        public double Amount { get; set; }

        public Product Product
        {
            get { return ProductService.GetProductById(ProductId); }
        }
    }
}
