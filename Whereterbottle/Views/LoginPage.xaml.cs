using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Whereterbottle.Utilities;
using Whereterbottle.Alerts;
using Rg.Plugins.Popup.Services;
using globals;

namespace Whereterbottle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        // Pop up window definitions
        private InvalidCreds invalidCreds = new InvalidCreds();
        private NewUserPrompt newUserPrompt = new NewUserPrompt();

        // Utility definitions
        private HttpHandler httpHandle = new HttpHandler();

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            if (usernameEntry.Text != null && passwordEntry.Text == "1234")
            {
                await httpHandle.getUser(usernameEntry.Text).ConfigureAwait(true);
                if (Globals.user.id != "")
                {

                    MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE);
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(invalidCreds).ConfigureAwait(true);
                }
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(invalidCreds).ConfigureAwait(true);
            }
        }

        private async void CreateAccountBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(newUserPrompt).ConfigureAwait(true);
        }
    }
}