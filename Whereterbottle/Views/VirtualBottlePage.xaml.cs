using globals;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Whereterbottle.Utilities;

namespace Whereterbottle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VirtualBottlePage : ContentPage
    {
        HttpHandler httpHandle = new HttpHandler();

        public VirtualBottlePage()
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
            base.OnAppearing();
        }

        private void FillLevelSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Globals.globalVariables.waterLevel = ((Slider)sender).Value;
        }

        private async void Bt_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            await httpHandle.updateCoord().ConfigureAwait(true);
        }

        private void refillBtn_Clicked(object sender, System.EventArgs e)
        {
            Globals.globalVariables.waterLevel = 100;
            FillLevelSlider.Value = 100;
        }
    }
}