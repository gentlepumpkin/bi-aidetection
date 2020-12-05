using Newtonsoft.Json;

namespace AITool
{
    public class ClsDoodsRequest
    {
        [JsonProperty("detector_name")]
        public string DetectorName { get; set; } = "default";

        [JsonProperty("data")]
        public string Data { get; set; } = "";

        [JsonProperty("file")]
        public string File { get; set; } = "";

        [JsonProperty("detect")]
        public Detect Detect { get; set; } = new Detect { MinPercentMatch = 0 };
    }


    //public partial class Region
    //{
    //    [JsonProperty("top")]
    //    public double Top { get; set; } = 0;

    //    [JsonProperty("left")]
    //    public double Left { get; set; } = 0;

    //    [JsonProperty("bottom")]
    //    public double Bottom { get; set; } = 0;

    //    [JsonProperty("right")]
    //    public double Right { get; set; } = 0;

    //    [JsonProperty("detect")]
    //    public Detect[] Detect { get; set; } = new Detect[] { new Detect { MinPercentMatch = 0 } };

    //    [JsonProperty("covers")]
    //    public bool Covers { get; set; }
    //}

    public partial class Detect
    {
        [JsonProperty("*")]
        public long MinPercentMatch { get; set; } = 0;

    }
}
