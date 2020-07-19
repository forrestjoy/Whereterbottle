using Newtonsoft.Json;

namespace Whereterbottle.Models
{
    class Address
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("newaddress")]
        public string newAddress { get; set; }
    }
}
