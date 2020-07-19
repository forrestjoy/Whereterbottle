using Xamarin.Forms.Xaml;

namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessAlert : Rg.Plugins.Popup.Pages.PopupPage
    {
        public SuccessAlert()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

    }
}