﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Wiseyard.Activation;

namespace Wiseyard.Services
{
    internal class ActivationService
    {
        private readonly App _app;
        private readonly Type _defaultNavItem;
        private Lazy<UIElement> _shell;

        private object _lastActivationArgs;

        public ActivationService(App app, Type defaultNavItem, Lazy<UIElement> shell = null)
        {
            _app = app;
            _shell = shell;
            _defaultNavItem = defaultNavItem;
        }

        public async Task ActivateAsync(object activationArgs)
        {
            if (IsInteractive(activationArgs))
            {
                await InitializeAsync();

                if (Window.Current.Content == null)
                {
                    Window.Current.Content = _shell?.Value ?? new Frame();
                }
            }

            await HandleActivationAsync(activationArgs);
            _lastActivationArgs = activationArgs;

            if (IsInteractive(activationArgs))
            {
                Window.Current.Activate();

                await StartupAsync();
            }
        }

        private async Task InitializeAsync()
        {
            await ThemeSelectorService.InitializeAsync().ConfigureAwait(false);
            await LanguageService.InitializeAsync().ConfigureAwait(false);
        }

        private async Task HandleActivationAsync(object activationArgs)
        {
            var activationHandler = GetActivationHandlers()
                                                .FirstOrDefault(h => h.CanHandle(activationArgs));

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(activationArgs);
            }

            if (IsInteractive(activationArgs))
            {
                var defaultHandler = new DefaultActivationHandler(_defaultNavItem);
                if (defaultHandler.CanHandle(activationArgs))
                {
                    await defaultHandler.HandleAsync(activationArgs);
                }
            }
        }

        private async Task StartupAsync()
        {
            await ThemeSelectorService.SetRequestedThemeAsync();
            await LanguageService.SetRequestedLanguageAsync();
        }

        private IEnumerable<ActivationHandler> GetActivationHandlers()
        {
            yield break;
        }

        private bool IsInteractive(object args)
        {
            return args is IActivatedEventArgs;
        }
    }
}
