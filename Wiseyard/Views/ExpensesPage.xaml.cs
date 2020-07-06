using System;
using System.Linq;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Wiseyard.ViewModels;

namespace Wiseyard.Views
{
    public sealed partial class ExpenseDataGridPage : Page
    {
        public ExpenseViewModel ViewModel { get; } = new ExpenseViewModel();

        public ExpenseDataGridPage()
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
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
    }
}
