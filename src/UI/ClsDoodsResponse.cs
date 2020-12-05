using Newtonsoft.Json;
using System.Collections.Generic;

namespace AITool
{
    //  Deserialization of 'Response' from 'DOODS' failed: Input string '0.09833781' is not a valid integer. Path 'detections[0].top', line 1, position 32. [JsonReaderException] Mod: <GetDetectionsFromAIServer>d__35 Line:1147:37, JSON: '{"detections":[{"top":0.09833781,"left":0.62415826,"bottom":0.14295554,"right":0.7029755,"label":"car","confidence":63.28125},{"top":0.05073979,"left":0.040147513,"bottom":0.08883451,"right":0.07647807,"label":"car","confidence":53.90625},{"top":0.106752336,"left":0.6479672,"bottom":0.15137008,"right":0.73192835,"label":"car","confidence":51.171875},{"top":0.047032133,"left":0.11679672,"bottom":0.08336268,"right":0.14591543,"label":"car","confidence":46.09375},{"top":0.8483164,"left":0.59002864,"bottom":0.87565106,"right":0.6246767,"label":"kite","confidence":46.09375},{"top":0.03276477,"left":0.8568243,"bottom":0.13587794,"right":0.8809123,"label":"traffic light","confidence":42.578125},{"top":0.039902665,"left":0.013500711,"bottom":0.08743232,"right":0.064132325,"label":"car","confidence":41.40625},{"top":0.043502297,"left":0.78453726,"bottom":0.11749081,"right":0.8064459,"label":"traffic light","confidence":39.0625},{"top":0.03285755,"left":0.7472904,"bottom":0.12813556,"right":0.78787124,"label":"traffic light","confidence":39.0625},{"top":0.0127313435,"left":0.16018775,"bottom":0.058782034,"right":0.18043163,"label":"traffic light","confidence":36.71875}]}'
    public class ClsDoodsResponse
    {
        //[JsonProperty("id")]
        //public string Id { get; set; } = "";

        [JsonProperty("detections")]
        public List<ClsDoodsDetection> Detections { get; set; } = new List<ClsDoodsDetection>();
    }
}
