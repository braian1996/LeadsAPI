using Newtonsoft.Json;

namespace LeadsAPI.DTOs
{
    public class PlaceDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
