using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services;
using Wiseyard.Helpers;

namespace Wiseyard.ViewModels
{
    public class IncomeViewModel : Observable
    {
        private bool _showCreateForm;
        private bool _isSelected;
        private DateTimeOffset _dateTimeOffset = new DateTimeOffset(DateTime.UtcNow);
        private string _amount;
        private string _price;
        private ResourceSelectModel _selectedResource;
        private Income _selectedItem;
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

        public string Amount
        {
            get => _amount ?? String.Empty;
            set
            {
                Set(ref _amount, value);
                if (String.IsNullOrEmpty(value))
                {
                    ButtonEnabled = false;
                }
                else
                {
                    ButtonEnabled = (SelectedResource != null) && !String.IsNullOrEmpty(Price);
                }
            }
        }

        public string Price
        {
            get => _price ?? String.Empty;
            set
            {
                Set(ref _price, value);
                if (String.IsNullOrEmpty(value))
                {
                    ButtonEnabled = false;
                }
                else
                {
                    ButtonEnabled = (SelectedResource != null) && !String.IsNullOrEmpty(Amount);
                }
            }
        }

        public ResourceSelectModel SelectedResource
        {
            get => _selectedResource;
            set
            {
                Set(ref _selectedResource, value);
                if (value == null)
                {
                    ButtonEnabled = false;
                }
                else
                {
                    ButtonEnabled = !String.IsNullOrEmpty(Amount) && !String.IsNullOrEmpty(Price);
                }
            }
        }

        public Income SelectedItem
        {
            get => _selectedItem;
            set
            {
                Set(ref _selectedItem, value);
                IsSelected = _selectedItem != null;
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

        public ObservableCollection<Income> Source { get; } = new ObservableCollection<Income>();
        public ObservableCollection<ResourceSelectModel> ResourceCollection { get; } = new ObservableCollection<ResourceSelectModel>();

        public IncomeViewModel()
        {
            GetAllResources();
        }

        public void LoadData()
        {
            Source.Clear();

            var data = IncomeService.GetAllIncomes();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        public void GetAllResources()
        {
            foreach (var product in ProductService.GetAllProducts())
            {
                ResourceCollection.Add(new ResourceSelectModel { Id = product.Id, Name = product.Name });
            }
            foreach (var util in ProductService.GetAllProducts())
            {
                ResourceCollection.Add(new ResourceSelectModel { Id = util.Id, Name = util.Name });
            }
            foreach (var chemical in ChemicalService.GetAllChemicals())
            {
                ResourceCollection.Add(new ResourceSelectModel { Id = chemical.Id, Name = chemical.Name });
            }
        }

        public void ShowCreateForm_Click()
        {
            ShowCreateForm = true;
        }

        public void CreateNewIncome()
        {
            Income model = new Income
            {
                ResourceId = SelectedResource.Id,
                Amount = Double.Parse(Amount),
                Price = Double.Parse(Price),
                Date = DateTimeOffset.UtcDateTime
            };

            model.Id = IncomeService.CreateIncome(model);

            SelectedResource = null;
            Amount = String.Empty;
            Price = String.Empty;
            DateTimeOffset = new DateTimeOffset(DateTime.Now);
            ShowCreateForm = false;
            Source.Add(model);
        }

        public void DeleteIncome()
        {
            Source.Remove(SelectedItem);
            IncomeService.DeleteIncome(SelectedItem.Id);
        }
    }
}
