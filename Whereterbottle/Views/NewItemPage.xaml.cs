using System;
using System.ComponentModel;
using Whereterbottle.Models;
using Xamarin.Forms;

namespace Whereterbottle.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync().ConfigureAwait(true);
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync().ConfigureAwait(true);
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