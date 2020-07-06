using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class Job
    {
        public int Id { get; set; }
        public int JobTypeId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public JobType JobType
        {
            get { return JobTypeService.GetJobTypeById(JobTypeId); }
        }

        public string JobTypeString
        {
            get { return JobType.Type; }
        }

        public string DateString
        {
            get => Date.ToString("dd.MM.yyyy.");
        }
    }
}
