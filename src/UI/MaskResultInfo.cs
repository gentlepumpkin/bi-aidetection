using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Unknown
    }
    public class MaskResultInfo
    {
        public bool IsMasked = false;
        public MaskType MaskType = MaskType.Unknown;
        public MaskResult Result = MaskResult.Unknown;
        public int ImagePointsOutsideMask = 0;
        public int DynamicThresholdCount = 0;
        public float PercentMatch = 0;

        public void SetResults(MaskType type, MaskResult result)
        {
            switch(type) {
                case MaskType.Dynamic:
                case MaskType.Static:
                    IsMasked = true;
                    break;
                case MaskType.Unknown:
                case MaskType.History:
                    IsMasked = false;
                    break;
            }

            MaskType = type;
            Result = result;
        }

        public void SetResults(MaskType type, MaskResult result, ObjectPosition op)
        {
            DynamicThresholdCount = op.Counter;
            PercentMatch = op.LastPercentMatch;

            SetResults(type, result);
        }
    }

}
