namespace AITool
{
    public class ClsDeepstackDetection
    {

        public string label { get; set; } = "";
        public string Detail { get; set; } = "";   //only used internally, deepstack does not ever send this
        public string UserID { get; set; } = "";   //only for face detection
        public float confidence { get; set; }      //only for face detection or scene detection
        public int y_min { get; set; }
        public int x_min { get; set; }
        public int y_max { get; set; }
        public int x_max { get; set; }

    }
}


