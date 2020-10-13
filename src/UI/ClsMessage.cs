using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AITool
{
    public enum MessageType
    {
        LogEntry,
        UpdateLabel,
        UpdateStatus,
        ImageAddedToQueue,
        CreateHistoryItem,
        DeleteHistoryItem,
        BeginProcessImage,
        EndProcessImage,
        SettingsSaved,
        SettingsLoaded,
        UpdateProgressBar,
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
        public string Description { get => description; set => description = value; }
        public string JSONPayload { get => jsonpayload; set => jsonpayload = value; }
        public MessageType MessageType { get => messageType; set => messageType = value; }
        public DateTime MessageCreateDate { get => messageCreateDate; set => messageCreateDate = value; }
        public string MemberName { get => memberName; set => memberName = value; }
        public int CurVal { get => curval; set => curval = value; }
        public int MaxVal { get => maxval; set => maxval = value; }

        //pass a class object or string in payloadobject, gets converted to json string
        public ClsMessage(MessageType MT = MessageType.LogEntry, string Descript = "", object PayloadObject = null, string MemberName = "", int CurVal = 0, int MaxVal = 0)
        {
            this.description = Descript;
            this.messageType = MT;
            this.memberName = MemberName;
            this.messageCreateDate = DateTime.Now;
            this.curval = CurVal;
            this.maxval = MaxVal;
                

            if (PayloadObject !=null)
            {
                this.jsonpayload = Global.GetJSONString(PayloadObject);
            }
        }
    }
        
}
