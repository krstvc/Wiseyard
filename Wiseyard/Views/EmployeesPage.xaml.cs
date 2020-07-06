using System;
using System.Linq;
using Telerik.UI.Xaml.Controls.Grid;
using Telerik.UI.Xaml.Controls.Input;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Wiseyard.ViewModels;

namespace Wiseyard.Views
{
    public sealed partial class EmployeesPage : Page
    {
        public EmployeesViewModel ViewModel { get; } = new EmployeesViewModel();

        public EmployeesPage()
        {
            InitializeComponent();
            GridLocalizationManager.Instance.UserResourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.LoadData();
        }

        private void TextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsLetter(c));
        }
    }
}
