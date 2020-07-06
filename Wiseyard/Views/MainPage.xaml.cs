using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services;
using Wiseyard.Helpers;
using Wiseyard.ViewModels;

namespace Wiseyard.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel HomeViewModel { get; } = new MainViewModel();
        public LoginViewModel LoginViewModel { get; } = new LoginViewModel();

        static MainPage()
        {
            ShellPage.IsLogged = false;
        }

        public MainPage()
        {
            InitializeComponent();
            if (ShellPage.IsLogged)
            {
                LoginContentArea.Visibility = Visibility.Collapsed;
                HomeContentArea.Visibility = Visibility.Visible;
            }
            else
            {
                LoginContentArea.Visibility = Visibility.Visible;
                HomeContentArea.Visibility = Visibility.Collapsed;
            }
        }

        private void LoginButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AttemptLogin();
        }

        private void AttemptLogin()
        {
            var username = LoginViewModel.Username;
            var passwordHash = GetHashed(LoginViewModel.Password);

            var user = UserService.GetUserByUsername(username);

            if (user == null)
            {
                ErrorLabel.Text = new ResourceLoader().GetString("LoginPage_UserDoesNotExist");
                return;
            }

            if (!passwordHash.Equals(user.PasswordHash))
            {
                ErrorLabel.Text = new ResourceLoader().GetString("LoginPage_IncorrectPassword");
                return;
            }

            ShellPage.IsLogged = true;
            if (username.Equals("admin"))
            {
                ShellPage.IsAdminLogged = true;
            }

            UnlockMenus();
            SwitchViews();
        }

        private void UnlockMenus()
        {
            var current = Window.Current.Content as Page;
            var incomeTab = current.FindName("IncomeTab") as Microsoft.UI.Xaml.Controls.NavigationViewItem;
            var incomeChartTab = current.FindName("IncomeChartTab") as Microsoft.UI.Xaml.Controls.NavigationViewItem;
            var expenseTab = current.FindName("ExpenseTab") as Microsoft.UI.Xaml.Controls.NavigationViewItem;
            var expenseChartTab = current.FindName("ExpenseChartTab") as Microsoft.UI.Xaml.Controls.NavigationViewItem;
            var jobsTab = current.FindName("JobsTab") as Microsoft.UI.Xaml.Controls.NavigationViewItem;
            var employeesTab = current.FindName("EmployeesTab") as Microsoft.UI.Xaml.Controls.NavigationViewItem;

            incomeTab.Visibility = Visibility.Visible;
            incomeChartTab.Visibility = Visibility.Visible;
            expenseTab.Visibility = Visibility.Visible;
            expenseChartTab.Visibility = Visibility.Visible;
            jobsTab.Visibility = Visibility.Visible;
            employeesTab.Visibility = Visibility.Visible;

            if (LoginViewModel.Username.Equals("admin"))
            {
                employeesTab.Visibility = Visibility.Visible;
            }
            else
            {
                employeesTab.Visibility = Visibility.Collapsed;
            }

            var mainFrame = current.FindName("navigationView") as Microsoft.UI.Xaml.Controls.NavigationView;
            mainFrame.IsSettingsVisible = true;
        }

        private void SwitchViews()
        {
            LoginContentArea.Visibility = Visibility.Collapsed;
            HomeContentArea.Visibility = Visibility.Visible;
        }

        private string GetHashed(string password)
        {
            return password;
        }

        private void PasswordBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter && LoginViewModel.ButtonEnabled)
            {
                AttemptLogin();
            }
        }
    }
}
