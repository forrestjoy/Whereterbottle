using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Whereterbottle.Utilities;
using globals;

namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFriendPrompt : Rg.Plugins.Popup.Pages.PopupPage
    {
        HttpHandler httpHandle = new HttpHandler();
        ModelHandler modelHandle = new ModelHandler();
        private FriendNotFound friendNotFound = new FriendNotFound();
        private SuccessAlert successAlert = new SuccessAlert();
        public Action<string> addFriend;

        public AddFriendPrompt(Action<string> addFriend)
        {
            InitializeComponent();
            this.addFriend = addFriend;
        }

        protected override void OnAppearing()
        {
            modelHandle.wipeFriendData();
            base.OnAppearing();
        }

        private async void addFriendBtn_Clicked(object sender, EventArgs e)
        {
            await httpHandle.getUserByName(friendName.Text).ConfigureAwait(true);
            if ((Globals.friend.id != "") && (Globals.friend.id != null))
            {
                Task addingFriend = httpHandle.addFriendToUser(Globals.friend.id);
                await addingFriend;
                addFriend(Globals.friend.id);
                AddFriendPromptWindow.IsVisible = false;
                await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
                await PopupNavigation.Instance.PushAsync(successAlert).ConfigureAwait(true);
                await httpHandle.getUser(Globals.user.email);
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(friendNotFound).ConfigureAwait(true);
            }
        }

        private async void cancelBtn_Clicked(object sender, EventArgs e)
        {
            AddFriendPromptWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
        }
    }
}