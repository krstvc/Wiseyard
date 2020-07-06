using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Wiseyard.ViewModels;

namespace Wiseyard.Views
{
    public sealed partial class IncomeChartPage : Page
    {
        public IncomeChartViewModel ViewModel { get; } = new IncomeChartViewModel();

        public IncomeChartPage()
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
