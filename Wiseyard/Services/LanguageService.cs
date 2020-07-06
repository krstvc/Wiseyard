using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Wiseyard.Helpers;

namespace Wiseyard.Services
{
    public class LanguageService
    {
        private const string LangName = "AppLanguageName";
        private const string LangCode = "AppLanguageCode";

        public static Language Language { get; set; }

        public static async Task InitializeAsync()
        {
            Language = await LoadLanguageFromSettingsAsync();
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = Language.LanguageCode;
        }

        public static async Task SetLanguageAsync(Language language)
        {
            Language = language;

            await SetRequestedLanguageAsync();
            await SaveLanguageInSettingsAsync(language);
        }

        public static async Task SetRequestedLanguageAsync()
        {
            foreach (var view in CoreApplication.Views)
            {
                await view.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (Window.Current.Content is FrameworkElement frameworkElement)
                    {
                        frameworkElement.Language = Language.LanguageCode;
                    }
                });
            }
        }

        private static async Task<Language> LoadLanguageFromSettingsAsync()
        {
            Language cacheLanguage = new Language { DisplayName = "English", LanguageCode = "en-US" };
            string langName = await ApplicationData.Current.LocalSettings.ReadAsync<string>(LangName);
            string langCode = await ApplicationData.Current.LocalSettings.ReadAsync<string>(LangCode);

            if (!string.IsNullOrEmpty(langCode) && !string.IsNullOrEmpty(langName))
            {
                cacheLanguage.DisplayName = langName;
                cacheLanguage.LanguageCode = langCode;
            }

            return cacheLanguage;
        }

        private static async Task SaveLanguageInSettingsAsync(Language language)
        {
            await ApplicationData.Current.LocalSettings.SaveAsync(LangName, language.DisplayName.ToString());
            await ApplicationData.Current.LocalSettings.SaveAsync(LangCode, language.LanguageCode.ToString());
        }
    }
}
