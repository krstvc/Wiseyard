using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Windows.ApplicationModel;
using Windows.UI.Xaml;

using Wiseyard.Helpers;
using Wiseyard.Services;

namespace Wiseyard.ViewModels
{
    public class SettingsViewModel : Observable
    {
        private ObservableCollection<Language> _languages;
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;
        private Language _selectedLanguage;

        public ObservableCollection<Language> Languages
        {
            get => _languages;
            set { Set(ref _languages, value); }
        }

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { Set(ref _elementTheme, value); }
        }

        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set { Set(ref _selectedLanguage, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }

        private ICommand _switchThemeCommand;
        private ICommand _switchLanguageCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }

        public ICommand SwitchLanguageCommand
        {
            get
            {
                if(_switchLanguageCommand == null)
                {
                    _switchLanguageCommand = new RelayCommand<Language>(
                        async (param) =>
                        {
                            SelectedLanguage = param;
                            await LanguageService.SetLanguageAsync(param);
                        });
                }

                return _switchLanguageCommand;
            }
        }

        public SettingsViewModel()
        {
            Languages = new ObservableCollection<Language>
            {
                new Language { DisplayName="English", LanguageCode="en-US" },
                new Language { DisplayName="Srpski", LanguageCode="sr-Latn" }
            };
        }

        public async Task InitializeAsync()
        {
            VersionDescription = GetVersionDescription();
            await Task.CompletedTask;
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
