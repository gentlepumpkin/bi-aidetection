using Newtonsoft.Json;
using System.Collections.Generic;

namespace AITool
{
    public class ClsDoodsResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; } = "";

        [JsonProperty("detections")]
        public List<ClsDoodsDetection> Detections { get; set; } = new List<ClsDoodsDetection>();
    }
}
