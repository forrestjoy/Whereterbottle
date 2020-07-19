using Rg.Plugins.Popup.Services;
using System;
using Whereterbottle.Models;
using Whereterbottle.Utilities;
using globals;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFavoriteFountainPrompt : Rg.Plugins.Popup.Pages.PopupPage
    {
        private HttpHandler httpHandle = new HttpHandler();
        private string LONG;
        private string LAT;

        public AddFavoriteFountainPrompt(double longitude, double latitude)
        {
            InitializeComponent();
            LONG = longitude.ToString();
            LAT = latitude.ToString();
        }

        private async void yesBtn_Clicked(object sender, EventArgs e)
        {
            foreach (Fountain fountain in Globals.allFountList)
            {
                if (fountain.x_coord == LONG && fountain.y_coord == LAT)
                {
                    if (!favFountainExists(fountain))
                    {
                        Task addFav = httpHandle.addFavFountainToUser(fountain._id);
                        await addFav;

                        await httpHandle.getUser(Globals.user.email).ConfigureAwait(true);
                    }

                    //TODO consider breaking here
                }
            }
            AddFavoriteFountainPromptWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
        }

        private async void noBtn_Clicked(object sender, EventArgs e)
        {
            AddFavoriteFountainPromptWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
        }

        public bool favFountainExists(Fountain fountain)
        {
           foreach (string favFountain in Globals.user.favorites)
            {
                if (fountain._id == favFountain)
                {
                    return true;
                }
            }
                   
            return false;
        }

    }
}