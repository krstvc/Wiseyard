using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class EmployeeJob
    {
        public int EmployeeId { get; set; }
        public int JobId { get; set; }

        public Employee Employee
        {
            get { return EmployeeService.GetEmployeeById(EmployeeId); }
        }
        public Job Job
        {
            get { return JobService.GetJobById(JobId); }
        }
    }
}
