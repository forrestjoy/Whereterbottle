using globals;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Whereterbottle.Utilities;
using Whereterbottle.Alerts;
using Rg.Plugins.Popup.Services;

namespace Whereterbottle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountDetails : ContentPage
    {
        HttpHandler httpHandle = new HttpHandler();
        private SuccessAlert successAlert = new SuccessAlert();

        public AccountDetails()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            // Remove LoginPage from NavigationStack
            if (Navigation.NavigationStack.Count > 1)
            {
                Page page = Navigation.NavigationStack[0];
                if (page != null && page != this)
                {
                    Navigation.RemovePage(page);
                }
            }
            updateWindowData();

            base.OnAppearing();
        }

        public AccountDetails(string id)
        {
            InitializeComponent();
            updateWindowData();
        }

        private async void returnButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage()).ConfigureAwait(true);
        }

        private async void submitChangesButton_Clicked(object sender, System.EventArgs e)
        {
            await httpHandle.changeUserAddress(userAddress.Text).ConfigureAwait(true);
            await httpHandle.changeUserEmail(userEmail.Text).ConfigureAwait(true);
            await httpHandle.getUser(Globals.user.email).ConfigureAwait(true);
            await PopupNavigation.Instance.PushAsync(successAlert).ConfigureAwait(true);
        }

        private void updateWindowData()
        {
            userFirstName.Text = Globals.user.firstName;
            userLastName.Text = Globals.user.lastName;
            userAddress.Text = Globals.user.address;
            userEmail.Text = Globals.user.email;
        }
    }
}