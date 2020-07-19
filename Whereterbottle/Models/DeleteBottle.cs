using Newtonsoft.Json;

namespace Whereterbottle.Models
{
    class DeleteBottle
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("_uid")]
        public string uid { get; set; }
    }
}
