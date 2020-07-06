using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Wiseyard.ViewModels;

namespace Wiseyard.Views
{
    public sealed partial class ExpenseChartPage : Page
    {
        public ExpenseChartViewModel ViewModel { get; } = new ExpenseChartViewModel();

        public ExpenseChartPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.LoadData();
        }
    }
}
