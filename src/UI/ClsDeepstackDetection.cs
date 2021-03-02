namespace AITool
{
    public class ClsDeepstackDetection
    {

        public string label { get; set; } = "";
        public string Detail { get; set; } = "";   //only used internally, deepstack does not ever send this
        public string UserID { get; set; } = "";   //only for face detection
        public double confidence { get; set; }      //only for face detection or scene detection
        public double y_min { get; set; } = 0;
        public double x_min { get; set; } = 0;
        public double y_max { get; set; } = 0;
        public double x_max { get; set; } = 0;
        public string Server { get; set; } = "";

    }
}


