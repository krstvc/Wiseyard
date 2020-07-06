using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Wiseyard.Services;
using Wiseyard.ViewModels;

namespace Wiseyard.Views
{
    public sealed partial class ShellPage : Page
    {
        public static bool IsLogged;
        public static bool IsAdminLogged;

        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
            Task.Run(() => ThemeSelectorService.SetRequestedThemeAsync());
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);

            if (!IsLogged)
            {
                IncomeTab.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                IncomeChartTab.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                ExpenseTab.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                ExpenseChartTab.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                JobsTab.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                navigationView.IsSettingsVisible = false;
            }
            else
            {
                IncomeTab.Visibility = Windows.UI.Xaml.Visibility.Visible;
                IncomeChartTab.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ExpenseTab.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ExpenseChartTab.Visibility = Windows.UI.Xaml.Visibility.Visible;
                JobsTab.Visibility = Windows.UI.Xaml.Visibility.Visible;
                navigationView.IsSettingsVisible = true;
            }
            if (!IsAdminLogged)
            {
                EmployeesTab.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                EmployeesTab.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        public ShellPage(UIElement element) : this()
        {
            shellFrame.Content = element;
        }
    }
}
