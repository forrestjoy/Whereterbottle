using Newtonsoft.Json;

namespace Whereterbottle.Models
{
    public class Bottle
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("size")]
        public string size { get; set; }

        [JsonProperty("total_refills")]
        public int totalRefills { get; set; }

        [JsonProperty("last_refill_day")]
        public string lastRefillDay { get; set; }

        [JsonProperty("day_refills")]
        public int dayRefills { get; set; }

        [JsonProperty("x_coord")]
        public string xCoord { get; set; }

        [JsonProperty("y_coord")]
        public string yCoord { get; set; }
    }

}