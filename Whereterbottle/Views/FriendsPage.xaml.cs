using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using Whereterbottle.Alerts;
using globals;
using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Whereterbottle.Models;
using Whereterbottle.Utilities;
using System.Linq;

namespace Whereterbottle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPage : ContentPage
    {
        private AddFriendPrompt addFriendPrompt;
        private RemoveFriendPrompt removeFriendPrompt;
        HttpHandler httpHandle = new HttpHandler();
        // Declare Fountain List
        public ObservableCollection<User> userFriendsList { get; set; }

        public FriendsPage()
        {
            InitializeComponent();
            // friendsList.ItemsSource = Globals.friendList;
            // Create an ObservableCollection that will be binded to XAML
            userFriendsList = new ObservableCollection<User>();
            BindingContext = this; // Complete the binding context to "this"
        }

        /// <summary>
        /// Executes on screen show, fetches all fountains and builds favorites
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Clear the list and refetch: important incase user updates from maps
            userFriendsList.Clear();
            // Build the Fav fountain list from user data
            await constructFriendsList();
        }

        /// <summary>
        /// Builds the Favorite Fountain list
        /// </summary>
        /// <returns>A task for whether this function has completed or not</returns>
        public async Task constructFriendsList()
        {
            foreach (string friend in Globals.user.friends)
            {
                Task<User> response = httpHandle.getFriendUser(friend);
                await response;

                if (response.Result != null)
                {
                    // Since the Fountain list has been binded, updating it here will update the UI
                    userFriendsList.Add(response.Result);
                }
            }
        }

        public async void addFriend(string friendId)
        {
            Task<User> response = httpHandle.getFriendUser(friendId);
            await response;

            if (response.Result != null)
            {
                // Since the Fountain list has been binded, updating it here will update the UI
                userFriendsList.Add(response.Result);
            }
        }

        public void RemoveItem(ObservableCollection<User> collection, User instance)
        {
            collection.Remove(collection.Where(i => i.id == instance.id).Single());
        }

        public async void removeFriend(string friendId)
        {
            Task<User> response = httpHandle.getFriendUser(friendId);
            await response;

            if (response.Result != null)
            {
                // Since the Fountain list has been binded, updating it here will update the UI
                RemoveItem(userFriendsList, response.Result);
            }
        }

        private async void returnButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage()).ConfigureAwait(true);
        }

        private async void addFriendButton_Clicked(object sender, EventArgs e)
        {
            addFriendPrompt = new AddFriendPrompt(addFriend);
            await PopupNavigation.Instance.PushAsync(addFriendPrompt).ConfigureAwait(true);
        }

        private async void friendsList1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            User tempUser = new User();
            tempUser = (User)e.SelectedItem;
            removeFriendPrompt = new RemoveFriendPrompt(tempUser.id, removeFriend);
            await PopupNavigation.Instance.PushAsync(removeFriendPrompt).ConfigureAwait(false);
            
        }
    }
}