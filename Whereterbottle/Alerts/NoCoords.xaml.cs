using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoCoords : Rg.Plugins.Popup.Pages.PopupPage
    {
        public NoCoords()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void btnOkay_Clicked(object sender, EventArgs e)
        {
            NoCoordsWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
        }
    }
}