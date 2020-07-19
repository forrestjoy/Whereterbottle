using Newtonsoft.Json;

namespace Whereterbottle.Models
{
    class MakeBottle
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("size")]
        public string size { get; set; }
    }
}
