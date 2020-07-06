using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class SeasonalProduction
    {
        public int SeasonId { get; set; }
        public int ProductId { get; set; }
        public double Amount { get; set; }

        public Season Season
        {
            get { return SeasonService.GetSeasonById(SeasonId); }
        }
        public Product Product
        {
            get { return ProductService.GetProductById(ProductId); }
        }
    }
}
