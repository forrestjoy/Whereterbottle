using System;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using Whereterbottle.Utilities;
using globals;

namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteBottlePrompt : Rg.Plugins.Popup.Pages.PopupPage
    {
        private HttpHandler httpHandle = new HttpHandler();
        private ModelHandler modelHandle = new ModelHandler();

        public DeleteBottlePrompt()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void okayBtn_Clicked(object sender, EventArgs e)
        {
            httpHandle.deleteBottle();
            modelHandle.wipeUserBottleData();
            httpHandle.getUser(Globals.user.email);
            httpHandle.getBottle();
            DeleteBottlePromptWindow.IsVisible = false;
            PopupNavigation.Instance.PopAllAsync();
        }
    }
}