using Whereterbottle.Views;
using Xamarin.Forms;
using globals;
using System.Collections.Generic;
using Whereterbottle.Models;
using Whereterbottle.Utilities;

namespace Whereterbottle
{
    public partial class App : Application
    {
        public static string EVENT_LAUNCH_LOGIN_PAGE = "EVENT_LAUNCH_LOGIN_PAGE";
        public static string EVENT_LAUNCH_MAIN_PAGE = "EVENT_LAUNCH_MAIN_PAGE";

        public App()
        {
            InitializeComponent();

            Globals.user = new User();
            Globals.friend = new User();
            Globals.userBottle = new Bottle();
            Globals.currentFountain = new Fountain();
            Globals.currentUser = new User();
            Globals.friendList = new List<User>();
            // Globals.favFountList = new List<Fountain>();
            Globals.allFountList = new List<Fountain>();
            Globals.globalVariables = new GlobalVariables();
            Globals.globalVariables.waterLevel = 0.0;
            Globals.globalVariables.numOunces = "";
            MainPage = new LoginPage();
                    // Utility definitions
            HttpHandler httpHandle = new HttpHandler();

            MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_LOGIN_PAGE, SetLoginPageAsRootPage);
            MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_MAIN_PAGE, SetMainPageAsRootPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void SetLoginPageAsRootPage(object sender)
        {
            MainPage = new LoginPage();
        }

        private void SetMainPageAsRootPage(object sender)
        {
            MainPage = new MainPage();
        }
    }
}