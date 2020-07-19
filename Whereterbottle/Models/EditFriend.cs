using Newtonsoft.Json;

namespace Whereterbottle.Models
{
    class EditFriend
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("friend_id")]
        public string friendID{ get; set; }
    }
}
