using System;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvalidCreds : Rg.Plugins.Popup.Pages.PopupPage
    {
        public InvalidCreds()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void btnOkay_Clicked(object sender, EventArgs e)
        {
            InvalidCredsWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
        }
    }
}