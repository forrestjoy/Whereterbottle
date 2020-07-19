using System.Collections.Generic;
using Whereterbottle.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whereterbottle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VirtualFountainPage : ContentPage
    {
        public VirtualFountainPage()
        {
            InitializeComponent();

            reviewList.ItemsSource = new List<Review>
            {
                new Review { Name = "Mark", Rating = "5", Rev = "I thought it was great!"},
                new Review { Name = "Johnny", Rating = "4", Rev = "I thought it was awful!"},
                new Review { Name = "Jack", Rating = "3.5", Rev = "I thought it was okay!"},
                new Review { Name = "Mason", Rating = "3.0", Rev = "I thought it was bad!"},
                new Review { Name = "Martin", Rating = "1.0", Rev = "I thought it was bad!"}
            };
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
    }
}