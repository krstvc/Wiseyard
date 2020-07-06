using System;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.Globalization;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Wiseyard.Helpers;
using Wiseyard.Services;
using Wiseyard.ViewModels;

namespace Wiseyard.Views
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsViewModel ViewModel { get; } = new SettingsViewModel();

        public SettingsPage()
        {
            InitializeComponent();
            LanguageComboBox.SelectedItem = LanguageService.Language;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeAsync();
        }

        private async void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = ViewModel.SelectedLanguage.LanguageCode;
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().Reset();
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForViewIndependentUse().Reset();

            await LanguageService.SetLanguageAsync(ViewModel.SelectedLanguage);

            var current = Window.Current as Window;
            current.Content = new ShellPage(new SettingsPage());
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            ShellPage.IsLogged = false;
            ShellPage.IsAdminLogged = false;

            var current = Window.Current as Window;
            current.Content = new ShellPage(new MainPage());
        }
    }
}
