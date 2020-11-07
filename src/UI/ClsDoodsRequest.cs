using Newtonsoft.Json;

namespace AITool
{
    public class ClsDoodsRequest
    {
        [JsonProperty("detector_name")]
        public string DetectorName { get; set; } = "default";

        [JsonProperty("data")]
        public string Data { get; set; } = "";  //base64 encoded

        [JsonProperty("file")]
        public string File { get; set; } = "";

        [JsonProperty("detect")]
        public Detect Detect { get; set; } = new Detect();
    }

    public class Detect
    {
        public string ObjectName { get; set; } = "*";
        public int MinPercentageMatch { get; set; } = 0;
    }
}
