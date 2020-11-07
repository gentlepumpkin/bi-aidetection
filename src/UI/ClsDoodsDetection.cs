using Newtonsoft.Json;

namespace AITool
{
    public class ClsDoodsDetection
    {
        [JsonProperty("top")]
        public int Top { get; set; } = 0;

        [JsonProperty("left")]
        public double Left { get; set; } = 0;

        [JsonProperty("bottom")]
        public double Bottom { get; set; } = 0;

        [JsonProperty("right")]
        public double Right { get; set; } = 0;

        [JsonProperty("label")]
        public string Label { get; set; } = "";

        [JsonProperty("confidence")]
        public double Confidence { get; set; } = 0;
    }
}
