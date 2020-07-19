using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whereterbottle.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendNotFound : Rg.Plugins.Popup.Pages.PopupPage
    {
        public FriendNotFound()
        {
            InitializeComponent();
        }
    }
}