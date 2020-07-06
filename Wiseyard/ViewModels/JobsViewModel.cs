using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services;
using Wiseyard.Helpers;

namespace Wiseyard.ViewModels
{
    public class JobsViewModel : Observable
    {
        private bool _showCreateForm;
        private bool _isSelected;
        private DateTimeOffset _dateTimeOffset = new DateTimeOffset(DateTime.UtcNow);
        private string _description;
        private JobType _selectedJobType;
        private Job _selectedItem;
        private bool _buttonEnabled;

        public bool ShowCreateForm
        {
            get => _showCreateForm;
            set { Set(ref _showCreateForm, value); }
        }

        public bool EnableCommandBar => !ShowCreateForm;

        public DateTimeOffset DateTimeOffset
        {
            get => _dateTimeOffset;
            set { Set(ref _dateTimeOffset, value); }
        }

        public string Description
        {
            get => _description;
            set { Set(ref _description, value); }
        }

        public JobType SelectedJobType
        {
            get => _selectedJobType;
            set
            {
                Set(ref _selectedJobType, value);
                if(value != null)
                {
                    ButtonEnabled = true;
                }
                else
                {
                    ButtonEnabled = false;
                }
            }
        }

        public Job SelectedItem
        {
            get => _selectedItem;
            set
            {
                Set(ref _selectedItem, value);
                IsSelected = SelectedItem != null;
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set { Set(ref _isSelected, value); }
        }

        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set { Set(ref _buttonEnabled, value); }
        }

        public ObservableCollection<Job> Source { get; } = new ObservableCollection<Job>();
        public ObservableCollection<JobType> JobTypeCollection { get; } = new ObservableCollection<JobType>();

        public JobsViewModel()
        {
            GetAllJobTypes();
        }

        public void LoadData()
        {
            Source.Clear();

            var data = JobService.GetAllJobs();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        public void GetAllJobTypes()
        {
            foreach(var type in JobTypeService.GetAllJobTypes())
            {
                JobTypeCollection.Add(type);
            }
        }

        public void ShowCreateForm_Click()
        {
            ShowCreateForm = true;
        }

        public void CreateNewJob()
        {
            Job model = new Job
            {
                JobTypeId = SelectedJobType.Id,
                Description = Description,
                Date = DateTimeOffset.UtcDateTime
            };

            model.Id = JobService.CreateJob(model);

            SelectedJobType = null;
            Description = String.Empty;
            DateTimeOffset = new DateTimeOffset(DateTime.Now);
            ShowCreateForm = false;
            Source.Add(model);
        }

        public void DeleteJob()
        {
            Source.Remove(SelectedItem);
            JobService.DeleteJob(SelectedItem.Id);
        }
    }
}
