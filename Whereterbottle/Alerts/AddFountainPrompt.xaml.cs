using Xamarin.Forms.Xaml;
using Whereterbottle.Utilities;
using Rg.Plugins.Popup.Services;
using Whereterbottle.Alerts;
using Xamarin.Essentials;

namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFountainPrompt : Rg.Plugins.Popup.Pages.PopupPage
    {
        HttpHandler httpHandle = new HttpHandler();
        private SuccessAlert successAlert = new SuccessAlert();

        public AddFountainPrompt()
        {
            InitializeComponent();

        }

        private async void btnSubmit_Clicked(object sender, System.EventArgs e)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request).ConfigureAwait(true);
            await httpHandle.makeFountain(location.Longitude.ToString(), location.Latitude.ToString(), filterStatusEntry.Text, ratingEntry.Text, coldnessEntry.Text).ConfigureAwait(true);
            await PopupNavigation.Instance.PushAsync(successAlert).ConfigureAwait(true);
            AddFountainPromptWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
        }

        private async void btnCancel_Clicked(object sender, System.EventArgs e)
        {
            AddFountainPromptWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
        }
    }
}