using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Wiseyard.Core.Models;
using Wiseyard.Core.Services;
using Wiseyard.Helpers;

namespace Wiseyard.ViewModels
{
    public class EmployeesViewModel : Observable
    {
        private bool _showCreateForm;
        private bool _isSelected;
        private DateTimeOffset _dateTimeOffset = new DateTimeOffset(DateTime.UtcNow);
        private string _firstName;
        private string _lastName;
        private Employee _selectedItem;
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

        public string FirstName
        {
            get => _firstName;
            set
            {
                Set(ref _firstName, value);
                if (String.IsNullOrEmpty(value))
                {
                    ButtonEnabled = false;
                }
                else
                {
                    ButtonEnabled = !String.IsNullOrEmpty(LastName);
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                Set(ref _lastName, value);
                if (String.IsNullOrEmpty(value))
                {
                    ButtonEnabled = false;
                }
                else
                {
                    ButtonEnabled = !String.IsNullOrEmpty(FirstName);
                }
            }
        }

        public Employee SelectedItem
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

        public ObservableCollection<Employee> Source { get; } = new ObservableCollection<Employee>();

        public EmployeesViewModel()
        {
        }

        public void LoadData()
        {
            Source.Clear();

            var data = EmployeeService.GetAllEmployees();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        public void ShowCreateForm_Click()
        {
            ShowCreateForm = true;
        }

        public void CreateNewEmployee()
        {
            Employee model = new Employee
            {
                FirstName = FirstName,
                LastName = LastName,
                DateEmployed = DateTimeOffset.UtcDateTime
            };

            model.Id = EmployeeService.CreateEmployee(model);

            FirstName = String.Empty;
            LastName = String.Empty;
            DateTimeOffset = new DateTimeOffset(DateTime.Now);
            ShowCreateForm = false;
            Source.Add(model);
        }

        public void DeleteEmployee()
        {
            Source.Remove(SelectedItem);
            EmployeeService.DeleteEmployee(SelectedItem.Id);
        }
    }
}
