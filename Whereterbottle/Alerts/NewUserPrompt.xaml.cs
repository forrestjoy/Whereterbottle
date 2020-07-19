using Xamarin.Forms.Xaml;
using Whereterbottle.Utilities;
using Rg.Plugins.Popup.Services;

namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUserPrompt : Rg.Plugins.Popup.Pages.PopupPage
    {
        HttpHandler httpHandle = new HttpHandler();
        private SuccessAlert successAlert = new SuccessAlert();

        public NewUserPrompt()
        {
            InitializeComponent();
        }

        private async void btnSubmit_Clicked(object sender, System.EventArgs e)
        {
            await httpHandle.makeUser(firstName.Text, lastName.Text, emailEntry.Text, addressEntry.Text).ConfigureAwait(true);
            NewUserPromptWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
            await PopupNavigation.Instance.PushAsync(successAlert).ConfigureAwait(true);

        }

        private async void btnCancel_Clicked(object sender, System.EventArgs e)
        {
            NewUserPromptWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
        }
    }
}