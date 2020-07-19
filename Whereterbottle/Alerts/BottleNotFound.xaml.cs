using System;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottleNotFound : Rg.Plugins.Popup.Pages.PopupPage
    {
        public BottleNotFound()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void btnOkay_Clicked(object sender, EventArgs e)
        {
            BottleNotFoundWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
        }
    }
}