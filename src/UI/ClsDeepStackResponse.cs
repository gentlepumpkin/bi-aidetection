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


        public bool success { get; set; }
        public string error { get; set; }
        public int code { get; set; }
        public ClsDeepstackDetection[] predictions { get; set; }

    }
}


