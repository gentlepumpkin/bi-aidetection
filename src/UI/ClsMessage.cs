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
        SettingsLoaded

    }

    //This will be sent between processes or modules - will be used for service eventually
    public class ClsMessage
    {
        private string description = "";
        private string jsonpayload = "";
        private DateTime messageCreateDate = DateTime.Now;
        private MessageType messageType = MessageType.LogEntry;
        private string memberName = "";
        public string Description { get => description; set => description = value; }
        public string JSONPayload { get => jsonpayload; set => jsonpayload = value; }
        public MessageType MessageType { get => messageType; set => messageType = value; }
        public DateTime MessageCreateDate { get => messageCreateDate; set => messageCreateDate = value; }
        public string MemberName { get => memberName; set => memberName = value; }

        //pass a class object or string in payloadobject, gets converted to json string
        public ClsMessage(MessageType MT = MessageType.LogEntry, string Descript = "", object PayloadObject = null, string MemberName = "")
        {
            this.description = Descript;
            this.messageType = MT;
            this.memberName = MemberName;
            this.messageCreateDate = DateTime.Now;

            if (PayloadObject !=null)
            {
                this.jsonpayload = Global.GetJSONString(PayloadObject);
            }
        }
    }
        
}
