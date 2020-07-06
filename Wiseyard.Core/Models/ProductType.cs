using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int MeasurementUnitId { get; set; }

        public MeasurementUnit MeasurementUnit
        {
            get { return MeasurementUnitService.GetMeasurementUnitById(MeasurementUnitId); }
        }
    }
}
