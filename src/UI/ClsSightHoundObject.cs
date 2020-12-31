using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    //https://www.sighthound.com/products/cloud

    public class ClsSightHoundImage
    {
        [JsonProperty("width")]
        public int? Width { get; set; } = 0;
        [JsonProperty("height")]
        public int? Height { get; set; } = 0;
        [JsonProperty("orientation")]
        public int? Orientation { get; set; } = 0;
    }

    public class ClsSightHoundObject
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "";

        [JsonProperty("boundingBox")]
        public ClsSightHoundBoundingBox BoundingBox { get; set; }

        [JsonProperty("landmarks")]
        public Dictionary<string, int[][]> Landmarks { get; set; }

        [JsonProperty("attributes")]
        public ClsSightHoundAttributes Attributes { get; set; }
    }

    public class ClsSightHoundBoundingBox
    {
        [JsonProperty("x")]
        public int X { get; set; } = 0;
        [JsonProperty("y")]
        public int Y { get; set; } = 0;
        [JsonProperty("height")]
        public int Height { get; set; } = 0;
        [JsonProperty("width")]
        public int Width { get; set; } = 0;

    }

    public class ClsSightHoundAttributes
    {
        [JsonProperty("gender")]
        public string Gender { get; set; } = "";
        [JsonProperty("genderConfidence")]
        public double? GenderConfidence { get; set; } = 0;
        [JsonProperty("frontal")]
        public bool? Frontal { get; set; } = false;
    }
}
