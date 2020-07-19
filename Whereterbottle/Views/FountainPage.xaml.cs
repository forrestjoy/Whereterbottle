using Whereterbottle.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using globals;
using Whereterbottle.Utilities;
using Whereterbottle.Alerts;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Whereterbottle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FountainPage : ContentPage
    {
        HttpHandler httpHandle = new HttpHandler();
        AddFountainPrompt addFountainPrompt = new AddFountainPrompt();
        // Declare Fountain List
        public ObservableCollection<Fountain> favoriteFountains { get; set; }

        /// <summary>
        /// Constructor:
        /// Responsible for Instantiating Favorite Fountains List
        /// </summary>
        public FountainPage()
        {
            InitializeComponent();
            // Create an ObservableCollection that will be binded to XAML
            favoriteFountains = new ObservableCollection<Fountain>();
            BindingContext = this; // Complete the binding context to "this"
        }

        /// <summary>
        /// Executes on screen show, fetches all fountains and builds favorites
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Clear the list and refetch: important incase user updates from maps
            favoriteFountains.Clear();
            // Configure await false allows the work to continue on a new thread
            await httpHandle.getFountains().ConfigureAwait(false);
            // Build the Fav fountain list from user data
            await constructFavoriteFountainList();
        }

        /// <summary>
        /// Builds the Favorite Fountain list
        /// </summary>
        /// <returns>A task for whether this function has completed or not</returns>
        public async Task constructFavoriteFountainList()
        {
            foreach (string fountain in Globals.user.favorites)
            {
                Task<Fountain> response = httpHandle.getFountain(fountain);
                await response;

                if (response != null)
                {
                    // Since the Fountain list has been binded, updating it here will update the UI
                    response.Result._id = response.Result._id.Substring(19);
                    favoriteFountains.Add(response.Result);
                }
            }
        }

        private async void viewNearbyBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapsPage(favoriteFountains)).ConfigureAwait(true);
        }

        private async void addFountainBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(addFountainPrompt).ConfigureAwait(true);
        }

        private async void favoriteFountainList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Fountain selectedFountain = (Fountain)e.SelectedItem;
            await Navigation.PushAsync(new MapsPage(selectedFountain)).ConfigureAwait(true);
        }
    }
}