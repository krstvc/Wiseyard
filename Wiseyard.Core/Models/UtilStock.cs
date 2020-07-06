using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class UtilStock
    {
        public int UtilId { get; set; }
        public int Amount { get; set; }

        public Util Util
        {
            get { return UtilService.GetUtilById(UtilId); }
        }
    }
}
