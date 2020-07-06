using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class Payment : Resource
    {
        public int EmployeeId { get; set; }
        public double Amount { get; set; }

        public Employee Employee
        {
            get { return EmployeeService.GetEmployeeById(EmployeeId); }
        }
    }
}
