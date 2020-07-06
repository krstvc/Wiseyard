using System;
using System.Collections.Generic;
using System.Text;

namespace Wiseyard.Core.Models
{
    public class JobType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Type;
        }
    }
}
