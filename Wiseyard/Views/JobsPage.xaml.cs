using System;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Wiseyard.ViewModels;

namespace Wiseyard.Views
{
    public sealed partial class JobsPage : Page
    {
        public JobsViewModel ViewModel { get; } = new JobsViewModel();

        public JobsPage()
        {
            InitializeComponent();
            GridLocalizationManager.Instance.UserResourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.LoadData();
        }
    }
}
