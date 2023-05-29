using System;
using System.Runtime.CompilerServices;

namespace AITool
{
    class MessageManager
    {

        public IProgress<ClsMessage> progress { get; set; } = null;

        public MessageManager()
        {

        }

        public void SendMessage(MessageType MT, string Descript = "", object Payload = null, [CallerMemberName] string memberName = null)
        {
            if (this.progress == null)
                return;

            ClsMessage msg = new ClsMessage(MT, Descript, Payload, memberName);

            this.progress.Report(msg);

        }

        public void DeleteHistoryItem(string filename, [CallerMemberName] string memberName = null)
        {
            if (this.progress == null)
                return;

            ClsMessage msg = new ClsMessage(MessageType.DeleteHistoryItem, filename, null, memberName);

            this.progress.Report(msg);

        }

        public void CreateHistoryItem(History hist, [CallerMemberName] string memberName = null)
        {
            if (this.progress == null)
                return;

            ClsMessage msg = new ClsMessage(MessageType.CreateHistoryItem, "", hist, memberName);

            this.progress.Report(msg);

        }

        public void UpdateLabel(string Message, string LabelControlName, [CallerMemberName] string memberName = null)
        {
            if (this.progress == null)
                return;

            ClsMessage msg = new ClsMessage(MessageType.UpdateLabel, Message, LabelControlName, memberName);

            this.progress.Report(msg);

        }

        public void Log(string Message, [CallerMemberName] string memberName = null)
        {
            if (this.progress == null)
                return;

            ClsMessage msg = new ClsMessage(MessageType.LogEntry, "", null, memberName);

            //this is for logging in non-gui classes.  Reports back to real logger
            //progress needs to be subscribed to in main gui
            string mn = "";
            if (memberName != null && !string.IsNullOrEmpty(memberName))
            {
                mn = $"{memberName}>> ";
            }
            msg.Description = $"{mn}{Message}";

            Global.SaveRegSetting("LastLogEntry", msg.Description);
            Global.SaveRegSetting("LastShutdownState", $"checkpoint: Global.Log: {DateTime.Now}");


            this.progress.Report(msg);

        }

    }
}
