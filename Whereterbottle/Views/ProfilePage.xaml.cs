using globals;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using Whereterbottle.Alerts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whereterbottle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private LogoutPrompt _logoutPopup;

        public ProfilePage()
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
            userFlName.Text = Globals.user.firstName + " " + Globals.user.lastName;
            base.OnAppearing();
        }

        private async void friendsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendsPage()).ConfigureAwait(true);
        }

        private async void accountDetailsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountDetails(Globals.user.id)).ConfigureAwait(true);
        }

        private async void PopNavigationStackAsync()
        {
            Page page = new LoginPage(); //replace 'page' with the page you want to reset to
            if (page == null) return; // If the new page did not create, do nothing
            // Place the login page at the "Root" of the stack
            Navigation.InsertPageBefore(page, Navigation.NavigationStack.First());
            // Pop everything but the root (login)
            await Navigation.PopToRootAsync();
        }

        private async void logoutButton_Clicked(object sender, EventArgs e)
        {
            _logoutPopup = new LogoutPrompt(PopNavigationStackAsync);
            await PopupNavigation.Instance.PushAsync(_logoutPopup);
        }

    }
}