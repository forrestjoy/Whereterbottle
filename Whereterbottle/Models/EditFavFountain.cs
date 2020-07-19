using Newtonsoft.Json;

namespace Whereterbottle.Models
{
    class EditFavFountain
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("fountain_id")]
        public string fountainID { get; set; }
    }
}
