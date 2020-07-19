using globals;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Whereterbottle.Alerts;
using Whereterbottle.Models;
using Xamarin.Essentials;

namespace Whereterbottle.Utilities
{
    internal class HttpHandler
    {
        // Global strings for http request and response
        private string RequestString;

        private string ResponseString;

        // Collections for catching DB objects
        private ObservableCollection<User> userReceived = new ObservableCollection<User>();
        private ObservableCollection<User> friendReceived = new ObservableCollection<User>();
        private ObservableCollection<Bottle> bottleReceived = new ObservableCollection<Bottle>();
        private ObservableCollection<Fountain> fountainReceived = new ObservableCollection<Fountain>();

        // Route Definitions
        private const string getuser = "http://3.17.133.193:3000/getuser";
        private const string getUserById = "http://3.17.133.193:3000/getuserbyid";
        private const string getbottle = "http://3.17.133.193:3000/getbottle";
        private const string makebottle = "http://3.17.133.193:3000/makebottle";
        private const string deletebottle = "http://3.17.133.193:3000/deletebottle";
        private const string updateCoords = "http://3.17.133.193:3000/updatecoord";
        private const string makefountain = "http://3.17.133.193:3000/makefountain"; // not implemented
        private const string updaterating = "http://3.17.133.193:3000/updaterating"; // not working
        private const string updatefilter = "http://3.17.133.193:3000/updatefilter"; // not working
        private const string getfountain = "http://3.17.133.193:3000/getfountain";
        private const string getfountains = "http://3.17.133.193:3000/getfountains";
        private const string changeuseraddress = "http://3.17.133.193:3000/changeuseraddress";
        private const string changeuseremail = "http://3.17.133.193:3000/changeuseremail";
        private const string addfriendtouser = "http://3.17.133.193:3000/addfriendtouser";
        private const string removefriendtouser = "http://3.17.133.193:3000/removefriendtouser";
        private const string makeuser = "http://3.17.133.193:3000/makeuser";    //not implemented
        private const string deleteuser = "http://3.17.133.193:3000/deleteuser";
        private const string addfavoritetouser = "http://3.17.133.193:3000/addfavoritetouser";
        private const string removefavoritetouser = "http://3.17.133.193:3000/removefavoritetouser";

        // Alert Window Definitions
        private SuccessAlert successAlert = new SuccessAlert();

        public async Task getUser(string userEmail)
        {
            RequestString = createGetUserRequest(userEmail); // create login request json string
            await sendHttp(getuser).ConfigureAwait(true);
            userReceived = JsonConvert.DeserializeObject<ObservableCollection<User>>(ResponseString); // deserialize and put into an array of user models

            if (userReceived.Count > 0)
            {
                Globals.user = userReceived[0]; // push user to array
            }
        }

        public static string createGetUserRequest(string email)
        {
            User tempUser = new User();
            tempUser.email = email;

            var jsonString = JsonConvert.SerializeObject(tempUser,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task<User> getFriendUser(string userID)
        {
            RequestString = createGetFriendUserRequest(userID); // create login request json string
            await sendHttp(getUserById).ConfigureAwait(true);
            friendReceived = JsonConvert.DeserializeObject<ObservableCollection<User>>(ResponseString); // deserialize and put into an array of user models

            if (friendReceived.Count > 0)
            {
                Globals.currentUser = friendReceived[0]; // push user to array
                return friendReceived[0];
            }

            return null;
        }

        public static string createGetFriendUserRequest(string userID)
        {
            User tempUser = new User();
            tempUser.id = userID;

            var jsonString = JsonConvert.SerializeObject(tempUser,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task getUserByName(string userFName)
        {
            RequestString = createGetUserByNameRequest(userFName); // create login request json string
            await sendHttp(getuser).ConfigureAwait(true);
            friendReceived = JsonConvert.DeserializeObject<ObservableCollection<User>>(ResponseString); // deserialize and put into an array of user models

            if (friendReceived.Count > 0)
            {
                Globals.friend = friendReceived[0]; // push user to array
            }
        }

        public static string createGetUserByNameRequest(string userFName)
        {
            User tempUser = new User();
            tempUser.firstName = userFName;

            var jsonString = JsonConvert.SerializeObject(tempUser,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task makeUser(string fName, string lName, string email, string address)
        {
            RequestString = createMakeUserRequest(fName,lName,email,address); // create login request json string
            await sendHttp(makeuser).ConfigureAwait(true);

        }

        public static string createMakeUserRequest(string fName, string lName, string email, string address)
        {
            User tempUser = new User();
            tempUser.firstName = fName;
            tempUser.lastName = lName;
            tempUser.email = email;
            tempUser.address = address;


            var jsonString = JsonConvert.SerializeObject(tempUser,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task getBottle()
        {
            RequestString = createGetBottleRequest(); // create user JSON string
            await sendHttp(getbottle).ConfigureAwait(false);
            bottleReceived = JsonConvert.DeserializeObject<ObservableCollection<Bottle>>(ResponseString); // deserialize and put into an array of user models

            if (bottleReceived.Count > 0)
            {
                Globals.userBottle = bottleReceived[0]; // push user to array
            }
        }

        public async Task getBottle(Action updateBottle)
        {
            RequestString = createGetBottleRequest(); // create user JSON string
            Task request = sendHttp(getbottle);
            await request;
            bottleReceived = JsonConvert.DeserializeObject<ObservableCollection<Bottle>>(ResponseString); // deserialize and put into an array of user models

            if (bottleReceived.Count > 0)
            {
                Globals.userBottle = bottleReceived[0]; // push user to array
                updateBottle();
            }
        }

        public static string createGetBottleRequest()
        {
            Bottle tempBottle = new Bottle();
            tempBottle.id = Globals.user.bottleID;

            var jsonString = JsonConvert.SerializeObject(tempBottle,
                                                         Formatting.None,
                                                         new JsonSerializerSettings
                                                         {
                                                             NullValueHandling = NullValueHandling.Ignore
                                                         });

            return jsonString;
        }

        public async Task makeBottle(string numOunces)
        {
            RequestString = createMakeBottleRequest(numOunces); // create login request json string
            await sendHttp(makebottle).ConfigureAwait(true);
            await PopupNavigation.Instance.PushAsync(successAlert).ConfigureAwait(true);
        }

        public static string createMakeBottleRequest(string ounces)
        {
            Bottle temp = new Bottle();
            temp.id = Globals.user.id;
            temp.size = ounces;

            var jsonString = JsonConvert.SerializeObject(temp,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task deleteBottle()
        {
            RequestString = createDeleteBottleRequest(); // create login request json string
            await sendHttp(deletebottle).ConfigureAwait(true);
        }

        public static string createDeleteBottleRequest()
        {
            DeleteBottle temp = new DeleteBottle();
            temp.id = Globals.userBottle.id;
            temp.uid = Globals.user.id;

            var jsonString = JsonConvert.SerializeObject(temp,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task updateCoord()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request).ConfigureAwait(true);

            RequestString = createUpdateCoordRequest(Convert.ToString(33.129148/*location.Longitude*/), Convert.ToString(-117.159134/*location.Latitude*/)); // create login request json string
            await sendHttp(updateCoords).ConfigureAwait(true);
        }

        public static string createUpdateCoordRequest(string longitude, string latitude)
        {
            UpdateCoords temp = new UpdateCoords();
            temp.id = Globals.userBottle.id;
            temp.yCoord = latitude;
            temp.xCoord = longitude;

            var jsonString = JsonConvert.SerializeObject(temp,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task sendHttp(string route)
        {
            using (var httpClient = new HttpClient())
            {
                var httpContent = new StringContent(RequestString, Encoding.UTF8, "application/json");
                var httpResponse = await httpClient.PostAsync(route, httpContent).ConfigureAwait(true);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(true);
                    ResponseString = responseContent;
                }

                httpContent.Dispose();
                httpResponse.Dispose();
            }
        }

        public async Task<Fountain> getFountain(string fountainID)
        {
            RequestString = createGetFountainRequest(fountainID); // create login request json string
            await sendHttp(getfountain).ConfigureAwait(true);
            fountainReceived = JsonConvert.DeserializeObject<ObservableCollection<Fountain>>(ResponseString); // deserialize and put into an array of user models

            if (fountainReceived.Count > 0)
            {
                Globals.currentFountain = fountainReceived[0]; // push user to array
                return fountainReceived[0];
            }

            return null;
        }

        public static string createGetFountainRequest(string fountainID)
        {
            GetFountain temp = new GetFountain();
            temp.id = fountainID;

            var jsonString = JsonConvert.SerializeObject(temp,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task makeFountain(string xCoord, string yCoord, string filterStatus, string rating, string coldness)
        {
            RequestString = createMakeFountainRequest(xCoord, yCoord, filterStatus, rating, coldness); // create login request json string
            await sendHttp(makefountain).ConfigureAwait(true);

        }

        public static string createMakeFountainRequest(string xCoord, string yCoord, string filterStatus, string rating, string coldness)
        {
            Fountain temp = new Fountain();
            temp.x_coord = xCoord;
            temp.y_coord = yCoord;
            temp.filter_status = filterStatus;
            temp.rating = rating;
            temp.coldness = coldness;

            var jsonString = JsonConvert.SerializeObject(temp,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task getFountains()
        {
            RequestString = createGetFountainsRequest(); // create login request json string
            await sendHttp(getfountains).ConfigureAwait(true);
            Globals.allFountList = JsonConvert.DeserializeObject<List<Fountain>>(ResponseString); // deserialize and put into an array of user models

        }

        public static string createGetFountainsRequest()
        {
            GetFountain temp = new GetFountain();

            var jsonString = JsonConvert.SerializeObject(temp,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task changeUserAddress(string newAddress)
        {
            RequestString = createChangeAddressRequest(newAddress); // create login request json string
            await sendHttp(changeuseraddress).ConfigureAwait(true);

        }

        public static string createChangeAddressRequest(string newAddress)
        {
            Address temp = new Address();
            temp.id = Globals.user.id;
            temp.newAddress = newAddress;

            var jsonString = JsonConvert.SerializeObject(temp,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task changeUserEmail(string newEmail)
        {
            RequestString = createChangeEmailRequest(newEmail); // create login request json string
            await sendHttp(changeuseremail).ConfigureAwait(true);
        }

        public static string createChangeEmailRequest(string newEmail)
        {
            User temp = new User();
            temp.id = Globals.user.id;
            temp.email = newEmail;

            var jsonString = JsonConvert.SerializeObject(temp,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task addFriendToUser(string friendIdent)
        {
            RequestString = createAddFriendRequest(friendIdent); // create login request json string
            await sendHttp(addfriendtouser).ConfigureAwait(true);

        }

        public static string createAddFriendRequest(string friendIdent)
        {
            EditFriend temp = new EditFriend();
            temp.id = Globals.user.id;
            temp.friendID = friendIdent;

            var jsonString = JsonConvert.SerializeObject(temp,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task removeFriendToUser(string friendIdent)
        {
            RequestString = createRemoveFriendRequest(friendIdent); // create login request json string
            await sendHttp(removefriendtouser).ConfigureAwait(true);

        }

        public static string createRemoveFriendRequest(string friendIdent)
        {
            EditFriend temp = new EditFriend();
            temp.id = Globals.user.id;
            temp.friendID = friendIdent;

            var jsonString = JsonConvert.SerializeObject(temp,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }

        public async Task addFavFountainToUser(string fountainID)
        {
            RequestString = createAddFavFountRequest(fountainID); // create login request json string
            await sendHttp(addfavoritetouser).ConfigureAwait(true);

        }

        public static string createAddFavFountRequest(string fountainID)
        {
            EditFavFountain temp = new EditFavFountain();
            temp.id = Globals.user.id;
            temp.fountainID = fountainID;

            var jsonString = JsonConvert.SerializeObject(temp,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonString;
        }
    }
}