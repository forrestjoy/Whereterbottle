using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Whereterbottle.Models;
using Xamarin.Forms;

namespace Whereterbottle.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        private Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            Detail = new NavigationPage(new BottlePage()) { BarBackgroundColor = Color.LightSkyBlue };
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

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Profile:
                        MenuPages.Add(id, new NavigationPage(new ProfilePage()) { BarBackgroundColor = Color.LightSkyBlue });
                        break;

                    case (int)MenuItemType.Bottle:
                        MenuPages.Add(id, new NavigationPage(new BottlePage()) { BarBackgroundColor = Color.LightSkyBlue });
                        break;

                    case (int)MenuItemType.FavoriteFountains:
                        MenuPages.Add(id, new NavigationPage(new FountainPage()) { BarBackgroundColor = Color.LightSkyBlue });
                        break;

                    case (int)MenuItemType.VirtualBottle:
                        MenuPages.Add(id, new NavigationPage(new VirtualBottlePage()) { BarBackgroundColor = Color.LightSkyBlue });
                        break;

                    case (int)MenuItemType.FountainReview:
                        MenuPages.Add(id, new NavigationPage(new VirtualFountainPage()) { BarBackgroundColor = Color.LightSkyBlue });
                        break;

                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()) { BarBackgroundColor = Color.LightSkyBlue });
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100).ConfigureAwait(true);

                IsPresented = false;
            }
        }
    }
}