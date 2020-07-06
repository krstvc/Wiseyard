using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class ChemicalStock
    {
        public int ChemicalId { get; set; }
        public double Amount { get; set; }

        public Chemical Chemical
        {
            get { return ChemicalService.GetChemicalById(ChemicalId); }
        }
    }
}
