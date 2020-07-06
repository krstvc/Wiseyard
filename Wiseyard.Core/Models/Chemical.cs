using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class Chemical : Resource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ChemicalTypeId { get; set; }

        public ChemicalType ChemicalType
        {
            get { return ChemicalTypeService.GetChemicalTypeById(ChemicalTypeId); }
        }
    }
}
