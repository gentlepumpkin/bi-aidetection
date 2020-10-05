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
        public int Image_PointsOutsideMask = 0;
        public int Dynamic_Threshold_Count = 0;


    }
}
