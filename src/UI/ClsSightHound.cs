using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    //https://www.sighthound.com/products/cloud


    //==========================================================================
    //                                 SHARED
    //==========================================================================


    public class SightHoundError
    {
        [JsonProperty("error")]
        public string Error { get; set; } = "";

        [JsonProperty("reason")]
        public string Reason { get; set; } = "";

        [JsonProperty("reasonCode")]
        public int ReasonCode { get; set; } = 0;

        [JsonProperty("details")]
        public SightHoundErrorDetails Details { get; set; } = new SightHoundErrorDetails();
    }

    public class SightHoundErrorDetails
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; } = 0;

        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; } = "";

        [JsonProperty("body")]
        public string Body { get; set; } = "";
    }

    public class SightHoundImage
    {
        [JsonProperty("width")]
        public int Width { get; set; } = 0;
        [JsonProperty("height")]
        public int Height { get; set; } = 0;
        [JsonProperty("orientation")]
        public int Orientation { get; set; } = 0;
    }


    //==========================================================================
    //                                 VEHICLE
    //==========================================================================

    public class SightHoundVehicleRoot
    {
        [JsonProperty("image")]
        public SightHoundImage Image { get; set; }

        [JsonProperty("objects")]

        public List<SightHoundVehicleObject> Objects { get; set; }
        [JsonProperty("requestId")]
        public string RequestId { get; set; } = "";

    }


    public class SightHoundVehicleObject
    {
        [JsonProperty("vehicleAnnotation")]
        public SightHoundVehicleAnnotation VehicleAnnotation { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("objectType")]
        public string ObjectType { get; set; }
    }

    public class SightHoundVehicleAnnotation
    {
        [JsonProperty("bounding")]
        public SightHoundBounding Bounding { get; set; }

        [JsonProperty("attributes")]
        public SightHoundAttributes Attributes { get; set; }

        [JsonProperty("recognitionConfidence")]
        public float RecognitionConfidence { get; set; }

        [JsonProperty("licenseplate")]
        public Licenseplate Licenseplate { get; set; }
    }

    public class Licenseplate
    {
        [JsonProperty("bounding")]
        public SightHoundBounding Bounding { get; set; }

        [JsonProperty("attributes")]
        public SightHoundAttributes Attributes { get; set; }
    }

    public class SightHoundAttributes
    {
        [JsonProperty("system")]
        public SightHoundSystem System { get; set; }
    }

    public class SightHoundSystem
    {
        [JsonProperty("color")]
        public SightHoundColor Color { get; set; }

        [JsonProperty("make")]
        public SightHoundMake Make { get; set; }

        [JsonProperty("model")]
        public SightHoundModel Model { get; set; }

        [JsonProperty("vehicleType")]
        public string VehicleType { get; set; }

        [JsonProperty("region")]
        public SightHoundRegion Region { get; set; }

        [JsonProperty("string")]
        public SightHoundString String { get; set; }

        [JsonProperty("characters")]
        public List<SightHoundCharacter> Characters { get; set; }
    }

    public class SightHoundString
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }

    public class SightHoundCharacter
    {
        [JsonProperty("bounding")]
        public SightHoundBounding Bounding { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("character")]
        public string character { get; set; }
    }
    public class SightHoundColor
    {
        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class SightHoundMake
    {
        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class SightHoundModel
    {
        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class SightHoundRegion
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("confidence")]
        public double Confidence { get; set; }
    }

    public class SightHoundVertex
    {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }

    public class SightHoundBounding
    {
        [JsonProperty("vertices")]
        public List<SightHoundVertex> Vertices { get; set; }
    }

    //==========================================================================
    //                                 PERSON
    //==========================================================================
    public class SightHoundPersonRoot
    {
        [JsonProperty("image")]
        public SightHoundImage Image { get; set; }

        [JsonProperty("objects")]

        public List<SightHoundPersonObject> Objects { get; set; }
        [JsonProperty("requestId")]
        public string RequestId { get; set; } = "";

    }

    public class SightHoundPersonObject
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "";

        [JsonProperty("boundingBox")]
        public SightHoundPersonBoundingBox BoundingBox { get; set; }

        [JsonProperty("attributes")]
        public SightHoundPersonAttributes? Attributes { get; set; }
        [JsonProperty("landmarks")]
        public Dictionary<string, int[][]>? Landmarks { get; set; }

    }

    public class SightHoundPersonBoundingBox
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
    public class SightHoundPersonAttributes
    {
        [JsonProperty("gender")]
        public string Gender { get; set; } = "";
        [JsonProperty("genderConfidence")]
        public float GenderConfidence { get; set; } = 0;
        [JsonProperty("age")]
        public int Age { get; set; } = 0;
        [JsonProperty("emotion")]
        public string Emotion { get; set; } = "";
        [JsonProperty("emotionsAll")]
        public SightHoundEmotionsAll EmotionsAll { get; set; }
        [JsonProperty("frontal")]
        public bool Frontal { get; set; } = false;

    }

    public class SightHoundEmotionsAll
    {
        [JsonProperty("neutral")]
        public double Neutral { get; set; }

        [JsonProperty("sadness")]
        public double Sadness { get; set; }

        [JsonProperty("disgust")]
        public double Disgust { get; set; }

        [JsonProperty("anger")]
        public double Anger { get; set; }

        [JsonProperty("surprise")]
        public double Surprise { get; set; }

        [JsonProperty("fear")]
        public double Fear { get; set; }

        [JsonProperty("happiness")]
        public double Happiness { get; set; }

    }

    public class SightHoundEmotionItem
    {
        public SightHoundEmotionItem(string name, double confidence)
        {
            Name = name;
            Confidence = confidence;
        }

        public string Name { get; set; } = "";
        public double Confidence { get; set; } = 0;

        public override string ToString()
        {
            return Name;
        }
    }

}
