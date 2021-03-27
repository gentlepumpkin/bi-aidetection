namespace AITool
{
    public enum MaskType
    {
        History,
        Dynamic,
        Static,
        Image,
        None,
        Unknown
    }
    public enum MaskResult
    {
        New,
        ThresholdNotMet,
        NewDynamicCreated,
        Found,
        NotFound,
        MajorityOutsideMask,
        CompletlyOutsideMask,
        MajorityInsideMask,
        CompletlyInsideMask,
        NoMaskImageFile,
        Unwanted,
        Error,
        SkippedDynamicMaskCheck,
        NotEnabled,
        Unknown,
        ObjectIgnoredMask
    }
    public class MaskResultInfo
    {
        public bool IsMasked = false;
        public MaskType MaskType = MaskType.Unknown;
        public MaskResult Result = MaskResult.Unknown;
        public int ImagePointsOutsideMask = 0;
        public int DynamicThresholdCount = 0;
        public double PercentMatch = 0;

        public void SetResults(MaskType type, MaskResult result)
        {
            switch (type)
            {
                case MaskType.Dynamic:
                case MaskType.Static:
                    this.IsMasked = true;
                    break;
                case MaskType.Unknown:
                case MaskType.History:
                    this.IsMasked = false;
                    break;
            }

            this.MaskType = type;
            this.Result = result;
        }

        public void SetResults(MaskType type, MaskResult result, ObjectPosition op)
        {
            this.DynamicThresholdCount = op.Counter;
            this.PercentMatch = op.LastPercentMatch;

            this.SetResults(type, result);
        }
    }

}
