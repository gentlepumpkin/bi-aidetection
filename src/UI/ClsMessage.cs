using System;

namespace AITool
{
    public enum MessageType
    {
        LogEntry,
        UpdateLabel,
        UpdateStatus,
        UpdateDeepstackStatus,
        UpdateProgressBar,
        ImageAddedToQueue,
        CreateHistoryItem,
        DeleteHistoryItem,
        BeginProcessImage,
        EndProcessImage,
        SettingsSaved,
        SettingsLoaded,
        DatabaseInitialized

    }

    //This will be sent between processes or modules - will be used for service eventually
    public class ClsMessage
    {
        private string description = "";
        private string jsonpayload = "";
        private DateTime messageCreateDate = DateTime.Now;
        private MessageType messageType = MessageType.LogEntry;
        private string memberName = "";
        private int curval = 0;
        private int maxval = 0;
        private int minval = 0;
        public string Description { get => this.description; set => this.description = value; }
        public string JSONPayload { get => this.jsonpayload; set => this.jsonpayload = value; }
        public MessageType MessageType { get => this.messageType; set => this.messageType = value; }
        public DateTime MessageCreateDate { get => this.messageCreateDate; set => this.messageCreateDate = value; }
        public string MemberName { get => this.memberName; set => this.memberName = value; }
        public int CurVal { get => this.curval; set => this.curval = value; }
        public int MaxVal { get => this.maxval; set => this.maxval = value; }
        public int MinVal { get => this.minval; set => this.minval = value; }

        //pass a class object or string in payloadobject, gets converted to json string
        public ClsMessage(MessageType MT = MessageType.LogEntry, string Descript = "", object PayloadObject = null, string MemberName = "", int CurVal = -1, int MinVal = -1, int MaxVal = -1)
        {
            this.description = Descript;
            this.messageType = MT;
            this.memberName = MemberName;
            this.messageCreateDate = DateTime.Now;
            this.curval = CurVal;
            this.maxval = MaxVal;
            this.minval = MinVal;

            if (PayloadObject != null)
            {
                this.jsonpayload = Global.GetJSONString(PayloadObject);
            }
        }
    }

}
