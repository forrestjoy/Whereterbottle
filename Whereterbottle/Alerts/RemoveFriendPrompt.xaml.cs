using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Whereterbottle.Utilities;
using Rg.Plugins.Popup.Services;

namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RemoveFriendPrompt : Rg.Plugins.Popup.Pages.PopupPage
    {
        string FRIEND_ID;
        private SuccessAlert successAlert = new SuccessAlert();
        public Action<string> removeFriend;

        HttpHandler httpHandle = new HttpHandler();

        public RemoveFriendPrompt(string friendID, Action<string> removeFriend)
        {
            InitializeComponent();
            FRIEND_ID = friendID;
            this.removeFriend = removeFriend;
        }

        private async void yesBtn_Clicked(object sender, EventArgs e)
        {
            Task removingFriend = httpHandle.removeFriendToUser(FRIEND_ID);
            await removingFriend;
            removeFriend(FRIEND_ID);
            RemoveFriendPromptWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
            await PopupNavigation.Instance.PushAsync(successAlert).ConfigureAwait(true);
            await httpHandle.getUser(globals.Globals.user.email).ConfigureAwait(true);
        }

        private async void noBtn_Clicked(object sender, EventArgs e)
        {
            RemoveFriendPromptWindow.IsVisible = false;
            await PopupNavigation.Instance.PopAllAsync().ConfigureAwait(true);
        }
    }
}