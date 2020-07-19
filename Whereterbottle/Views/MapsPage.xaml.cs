using System;
using System.Collections.Generic;
using Whereterbottle.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Whereterbottle.Alerts;
using Rg.Plugins.Popup.Services;

namespace Whereterbottle.Views
{
    public partial class MapsPage : ContentPage
    {
        AddFavoriteFountainPrompt addFavoriteFountainPrompt;
        public IEnumerable<Fountain> favoriteFountains;

        public MapsPage()
        {
            InitializeComponent();
            GetCurrentLocation();
        }

        public MapsPage(double latitude, double longitude)
        {
            InitializeComponent();
            AppMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromMiles(.05)));
            AddDummyPinBottle(latitude, longitude);
        }

        public MapsPage(IEnumerable<Fountain> favFountainList)
        {
            InitializeComponent();
            // TODO Fix Coords to currrent location - currently hardcoded for demo purposes
            favoriteFountains = favFountainList;
            AppMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(33.129148, -117.159134), Distance.FromMiles(.25)));
            int count = 0;
            foreach(Fountain fountain in globals.Globals.allFountList)
            {
                AddDummyPinFountain((Convert.ToDouble(fountain.y_coord)), (Convert.ToDouble(fountain.x_coord)));
            }
        }

        public MapsPage(Fountain selectedFountain)
        {
            InitializeComponent();
            double longitude = Convert.ToDouble(selectedFountain.x_coord);
            double latitude = Convert.ToDouble(selectedFountain.y_coord);
            AppMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromMiles(.05)));
            AddDummyPinFountain(latitude, longitude);
        }


        async public void GetCurrentLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request).ConfigureAwait(true);

            if (location != null)
            {
                AppMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(new Position(33.129148, -117.159134/*location.Latitude, location.Longitude*/), Distance.FromMiles(.25)));
            }
        }

        public void AddDummyPinFountain(double latitude, double longitude)
        {
            var pin = new Pin()
            {
                Position = new Position(latitude, longitude),
                Label = "Water Fountain",
            };

            pin.MarkerClicked += (sender, e) =>
            {
                addFavoriteFountainPrompt = new AddFavoriteFountainPrompt(pin.Position.Longitude, pin.Position.Latitude);
                PopupNavigation.Instance.PushAsync(addFavoriteFountainPrompt).ConfigureAwait(true);
            };

            AppMap.Pins.Add(pin);
        }

        public void AddDummyPinBottle(double latitude, double longitude)
        {
            int test = 0;
            var pin = new Pin()
            {
                Position = new Position(latitude, longitude),
                Label = "Water Bottle"
            };
            AppMap.Pins.Add(pin);
        }

        private async void returnButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync().ConfigureAwait(true);
        }
 
        private async void addFountainBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(addFavoriteFountainPrompt).ConfigureAwait(true);
        }
    }
}