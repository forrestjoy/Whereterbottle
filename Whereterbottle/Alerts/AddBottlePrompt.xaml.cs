using System;
using globals;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using Whereterbottle.Utilities;


namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBottlePrompt : Rg.Plugins.Popup.Pages.PopupPage
    {
        private HttpHandler httpHandle = new HttpHandler();
        public Action updateBottle;

        public AddBottlePrompt()
        {
            InitializeComponent();
        }

        public AddBottlePrompt(Action updateBottle)
        {
            InitializeComponent();
            this.updateBottle = updateBottle;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            if (numOunces.Text != null)
            {
                Globals.globalVariables.numOunces = numOunces.Text;
                await httpHandle.makeBottle(numOunces.Text).ConfigureAwait(true);
                await httpHandle.getUser(Globals.user.email).ConfigureAwait(true);
                await httpHandle.getBottle().ConfigureAwait(true);
                AddBottlePromptWindow.IsVisible = false;
                updateBottle();
                await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
            }
            else
            {
                await DisplayAlert("Invalid Entry", "Incorrect Ounces Input", "Okay").ConfigureAwait(true);
            }

        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            AddBottlePromptWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
        }

    }
}