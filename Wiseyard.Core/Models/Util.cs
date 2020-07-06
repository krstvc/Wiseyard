using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class Util : Resource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UtilTypeId { get; set; }

        public UtilType UtilType
        {
            get { return UtilTypeService.GetUtilTypeById(UtilTypeId); }
        }
    }
}
