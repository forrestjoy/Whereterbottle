using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Whereterbottle.Models
{
    public class User
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("first_name")]
        public string firstName { get; set; }

        [JsonProperty("last_name")]
        public string lastName { get; set; }

        [JsonProperty("bottle_id")]
        public string bottleID { get; set; } //array of item id's

        [JsonProperty("last_fill")]
        public string lastFill { get; set; } //array of note id's

        [JsonProperty("favorites")]
        public ObservableCollection<string> favorites { get; set; } //array of item id's

        [JsonProperty("friends")]
        public ObservableCollection<string> friends { get; set; } //array of item id's

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("address")]
        public string address { get; set; }
    }

    public class ReceivedUsers
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string bottleID { get; set; }
        public string lastFill { get; set; }
        public ObservableCollection<string> favorites { get; set; }
        public ObservableCollection<string> friends { get; set; }
        public string email { get; set; }
        public string address { get; set; }
    }
}