namespace AITool
{
    //classes for AI analysis

    public class ClsDeepStackResponse
    {
        //output = {
        //                        "success": False,
        //                        "error": "invalid image file",
        //                        "code": 400,
        //                    }


        //output = {"success": True, "predictions": outputs}


        //output = {
        //                        "success": False,
        //                        "error": "error occured on the server",
        //                        "code": 500,
        //                    }

        //https://www.codeproject.com/AI/docs/api/api_reference.html

        public bool success { get; set; }
        public string error { get; set; }
        public string message { get; set; }
        public string imageBase64 { get; set; }
        public int code { get; set; }
        public int count { get; set; }
        public int inferenceMs { get; set; }
        public int processMs { get; set; }
        public int analysisRoundTripMs { get; set; }
        public string moduleId { get; set; }
        public string moduleName { get; set; }
        public string command { get; set; }
        public string executionProvider { get; set; }
        public bool canUseGPU { get; set; }
        public string label { get; set; }  //this is used for scene detection  {'success': True, 'confidence': 0.7373981, 'label': 'conference_room'
        public string plate { get; set; }
        public float confidence { get; set; }  //this is used for scene detection  {'success': True, 'confidence': 0.7373981, 'label': 'conference_room'
        public ClsDeepstackDetection[] predictions { get; set; }

    }
}


