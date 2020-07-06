using System;
using System.Collections.Generic;
using System.Text;

namespace Wiseyard.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateEmployed { get; set; }

        public string Date
        {
            get
            {
                return DateEmployed.ToString("dd.MM.yyyy.");
            }
        }
    }
}
