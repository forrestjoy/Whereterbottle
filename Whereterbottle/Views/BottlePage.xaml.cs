using globals;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Whereterbottle.Alerts;
using Whereterbottle.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whereterbottle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottlePage : ContentPage
    {
        // Pop up window definitions
        private BottleNotFound bottleNotFound = new BottleNotFound();
        private DeleteBottlePrompt deleteBottlePrompt = new DeleteBottlePrompt();
        private AddBottlePrompt addBottlePrompt;
        private NoCoords noCoords = new NoCoords();

        // Utility definitions
        private HttpHandler httpHandle = new HttpHandler();

        public BottlePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            Task fetchBottle = httpHandle.getBottle(updateBottleData);
            await fetchBottle;
            if (Globals.userBottle.id == "")
            {
                await PopupNavigation.Instance.PushAsync(bottleNotFound).ConfigureAwait(true);
            }
            base.OnAppearing();
        }

        private async void findBtn_Clicked(object sender, EventArgs e)
        {
            if (Globals.user.bottleID != "")
            {
                await httpHandle.getBottle().ConfigureAwait(true);
                // TODO: X and Y coordinates are swapped - fix in DB
                if ((Globals.userBottle.xCoord != "") && (Globals.userBottle.yCoord != ""))
                {
                    await Navigation.PushAsync(new MapsPage(Convert.ToDouble(Globals.userBottle.xCoord), Convert.ToDouble(Globals.userBottle.yCoord))).ConfigureAwait(true);
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(noCoords).ConfigureAwait(true);
                }
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(bottleNotFound).ConfigureAwait(true);
            }
        }

        private void updateBottleData()
        {
            if (Globals.user.bottleID != "")
            {
                globalVariables.Progress = Globals.globalVariables.waterLevel;
                fillSize.Text = Globals.userBottle.size + "oz Bottle";
                dailyRefills.Text = "Daily Refills: " + Convert.ToString(Globals.userBottle.dayRefills);
                totalRefills.Text = "Total Refills: " + Convert.ToString(Globals.userBottle.totalRefills);
                lastRefillDate.Text = "Last Refill: " + Globals.userBottle.lastRefillDay;
            }
            else
            {
                globalVariables.Progress = 0;
                fillSize.Text = "--";
                dailyRefills.Text = "--";
                totalRefills.Text = "--";
                lastRefillDate.Text = "--";
            }
        }

        private async void addBtn_Clicked(object sender, EventArgs e)
        {
            addBottlePrompt = new AddBottlePrompt(updateBottleData);
            await PopupNavigation.Instance.PushAsync(addBottlePrompt).ConfigureAwait(true);
            // TODO: Solve real time data updates for window data
            globalVariables.Progress = 0;
            fillSize.Text = Globals.globalVariables.numOunces + "oz Bottle";
            dailyRefills.Text = "Daily Refills: 0";
            totalRefills.Text = "Total Refills: 0";
            lastRefillDate.Text = "Last Refill: ----";
        }

        private async void deleteBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(deleteBottlePrompt).ConfigureAwait(true);

            // TODO: Solve real time data updates for window data
            //-------------------------------------------------------------------------------
            globalVariables.Progress = 0;
            fillSize.Text = "--";
            dailyRefills.Text = "--";
            totalRefills.Text = "--";
            lastRefillDate.Text = "--";
            //--------------------------------------------------------------------------------
        }
    }
}