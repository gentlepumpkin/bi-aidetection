using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    class MessageManager
    {

        public IProgress<ClsMessage> progress = null;

        public MessageManager()
        {

        }

        public void SendMessage(MessageType MT, string Descript = "", object Payload = null, [CallerMemberName] string memberName = null)
        {
            if (progress == null)
                return;

            ClsMessage msg = new ClsMessage(MT, Descript, Payload, memberName);

            progress.Report(msg);

        }

        public void DeleteHistoryItem(string filename, [CallerMemberName] string memberName = null)
        {
            if (progress == null)
                return;

            ClsMessage msg = new ClsMessage(MessageType.DeleteHistoryItem, filename, null, memberName);

            progress.Report(msg);

        }

        public void CreateHistoryItem(History hist, [CallerMemberName] string memberName = null)
        {
            if (progress == null)
                return;

            ClsMessage msg = new ClsMessage(MessageType.CreateHistoryItem, "", hist, memberName);

            progress.Report(msg);

        }

        public void UpdateLabel(string Message, string LabelControlName, [CallerMemberName] string memberName = null)
        {
            if (progress == null)
                return;

            ClsMessage msg = new ClsMessage(MessageType.UpdateLabel, Message, LabelControlName, memberName);

            progress.Report(msg);

        }

        public void Log(string Message, [CallerMemberName] string memberName = null)
        {
            if (progress == null)
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

            Global.SaveSetting("LastLogEntry", msg.Description);
            Global.SaveSetting("LastShutdownState", $"checkpoint: Global.Log: {DateTime.Now}");


            progress.Report(msg);

        }

    }
}
