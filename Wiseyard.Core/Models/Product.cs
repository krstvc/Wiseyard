using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class Product : Resource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }

        public ProductType ProductType
        {
            get { return ProductTypeService.GetProductTypeById(ProductTypeId); }
        }
    }
}
