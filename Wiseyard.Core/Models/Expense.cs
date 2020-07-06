using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }

        public string Resource
        {
            get
            {
                Product product = ProductService.GetProductById(ResourceId);

                if (product != null)
                {
                    return product.Name;
                }

                Chemical chemical = ChemicalService.GetChemicalById(ResourceId);

                if (chemical != null)
                {
                    return chemical.Name;
                }

                Util util = UtilService.GetUtilById(ResourceId);

                if (util != null)
                {
                    return util.Name;
                }

                return "Payment";
            }
        }

        public string DateString
        {
            get
            {
                return Date.ToString("dd.MM.yyyy.");
            }
        }
    }
}
