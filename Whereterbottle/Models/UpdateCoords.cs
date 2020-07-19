using Newtonsoft.Json;

namespace Whereterbottle.Models
{
    class UpdateCoords
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("x_coord")]
        public string xCoord { get; set; }

        [JsonProperty("y_coord")]
        public string yCoord { get; set; }
    }
}
