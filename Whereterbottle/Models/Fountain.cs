using Newtonsoft.Json;

namespace Whereterbottle.Models
{
    public class Fountain
    {
        [JsonProperty("_id")]
        public string _id { get; set; }

        [JsonProperty("x_coord")]
        public string x_coord { get; set; }

        [JsonProperty("y_coord")]
        public string y_coord { get; set; }

        [JsonProperty("filter_staus")]
        public string filter_status { get; set; }

        [JsonProperty("rating")]
        public string rating { get; set; }

        [JsonProperty("num_ratings")]
        public int num_ratings { get; set; }

        [JsonProperty("coldness")]
        public string coldness { get; set; }
    }

    public class GetFountain
    {
        [JsonProperty("_id")]
        public string id { get; set; }
    }

    public class ReceivedFountains
    {
        public string id { get; set; }
        public string xCoord { get; set; }
        public string yCoord { get; set; }
        public string filter_status { get; set; }
        public string rating { get; set; }
        public int num_ratings { get; set; }
        public string coldness { get; set; }
    }
}