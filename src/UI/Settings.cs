using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml.XPath;

namespace WindowsFormsApp2
{

    public static class AppSettings
    {
        public static ClsSettings Settings = new ClsSettings();

        public class ClsSettings
        {
            public string SettingsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json");
            public string LogFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt");
            public string HistoryFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cameras\\history.csv");
            public string telegram_token = "";
            public string input_path = "";
            public List<string> telegram_chatids = new List<string>();
            public bool log_everything = false;
            public bool send_errors = true;
            public int close_instantly = -1;
            public string deepstack_url = "127.0.0.1:81";
            public string deepstack_adminkey = "";
            public string deepstack_apikey = "";
            public string deepstack_installfolder = "C:\\DeepStack";
            public string deepstack_port = "81";
            public string deepstack_mode = "Medium";
            public bool deepstack_autostart = false;
            public bool deepstack_sceneapienabled = false;
            public bool deepstack_faceapienabled = false;
            public bool deepstack_detectionapienabled = false;
            public int file_access_delay = 50;
            public int retry_delay = 10;
            public bool SettingsValid = false;
        }

        public static bool Save()
        {
            bool Ret = false;
            try
            {
                //keep a backup file in case of corruption
                if (File.Exists(AppSettings.Settings.SettingsFileName + ".bak"))
                {
                    File.Delete(AppSettings.Settings.SettingsFileName + ".bak");
                }
                if (File.Exists(AppSettings.Settings.SettingsFileName))
                {
                    File.Move(AppSettings.Settings.SettingsFileName, AppSettings.Settings.SettingsFileName + ".bak");
                }

                Settings.SettingsValid = true;

                Ret = SharedFunctions.WriteToJsonFile<ClsSettings>(AppSettings.Settings.SettingsFileName, Settings);

                if (!Ret)
                {
                    Settings.SettingsValid = false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: Could not save settings: " + ex.Message);
            }

            if (!Ret)
            {
                try
                {
                    //Revert to backup copy if exists
                    if (File.Exists(AppSettings.Settings.SettingsFileName + ".bak"))
                    {
                        Console.WriteLine("Settings save failed, reverting to backup copy...");
                        
                        if (File.Exists(AppSettings.Settings.SettingsFileName))
                           File.Delete(AppSettings.Settings.SettingsFileName);

                        File.Move(AppSettings.Settings.SettingsFileName + ".bak", AppSettings.Settings.SettingsFileName);
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }

            return Ret;
        }

        public static bool Load()
        {
            bool Ret = false;
            try
            {
                Settings.SettingsValid = false;  //assume failure

                //keep a backup file in case of corruption
                if (!File.Exists(AppSettings.Settings.SettingsFileName))
                {
                    //--------------------------------------------------------------------------------------------------------------------
                    //try to read in OLD aitool.exe.config or user.config files - they were
                    //unreliable because of strict versioning, etc
                    //
                    // NO NEED to add NEW settings to this area
                    //--------------------------------------------------------------------------------------------------------------------

                    List<FileInfo> filist = new List<FileInfo>();
                    FileInfo fi = new FileInfo(Path.Combine(AppDomain.CurrentDomain. BaseDirectory, Path.GetFileName(Assembly.GetEntryAssembly().Location) + ".config"));
                    if (fi.Exists)
                        filist.Add(fi);
                    filist.AddRange(SharedFunctions.GetFiles(Path.Combine(Environment.GetEnvironmentVariable("LOCALAPPDATA"),"WindowsFormsApp2"),"user.config"));
                    //sort by date
                    filist = filist.OrderByDescending((d) => d.LastWriteTime).ToList();
                    XDocument xmlfile = XDocument.Load(filist[0].FullName);
                    //<configuration>
                    //    <userSettings>
                    //        <WindowsFormsApp2.Properties.Settings>
                    //            <setting name="telegram_token" serializeAs="String">
                    //                <value />
                    //            </setting>
                    //            <setting name="telegram_chatid" serializeAs="String">
                    //                <value />
                    //            </setting>
                    //            <setting name="input_path" serializeAs="String">
                    //                <value>D:\BlueIrisStorage\AIInput</value>
                    //            </setting>

                    IEnumerable<XElement> els = xmlfile.XPathSelectElements("/configuration/userSettings/WindowsFormsApp2.Properties.Settings/setting");
                    if (els == null || els.Count() == 0)
                        els = xmlfile.XPathSelectElements("/configuration/applicationSettings/WindowsFormsApp2.Properties.Settings/setting");

                    int cnt = 0;
                    foreach (XElement el in els)
                    {
                        string val = el.Value;
                        if (!string.IsNullOrEmpty(val))
                        {
                            if (el.ToString().Contains("telegram_token"))   { Settings.telegram_token = val; cnt += 1; }
                            if (el.ToString().Contains("telegram_chatids")) { Settings.telegram_chatids = SharedFunctions.Split(val, ","); cnt += 1; }
                            if (el.ToString().Contains("input_path"))       { Settings.input_path = val; cnt += 1; }
                            if (el.ToString().Contains("deepstack_url"))    { Settings.deepstack_url = val; cnt += 1; }
                            if (el.ToString().Contains("log_everything"))   { Settings.log_everything = Convert.ToBoolean(val); cnt += 1; }
                            if (el.ToString().Contains("send_errors"))      { Settings.send_errors = Convert.ToBoolean(val); cnt += 1; }
                            if (el.ToString().Contains("close_instantly"))  { Settings.close_instantly = Convert.ToInt32(val); cnt += 1; }

                        }
                    }

                    Ret = (cnt > 0);

                    //if (setel != null)
                    //{
                    //    Settings.log_everything = Convert.ToBoolean(SharedFunctions.GetXValue(setel, "log_everything"));
                    //    Settings.send_errors = Convert.ToBoolean(SharedFunctions.GetXValue(setel, "send_errors"));
                    //    Settings.close_instantly = Convert.ToInt32(SharedFunctions.GetXValue(setel, "close_instantly"));
                    //    Ret = true;
                    //}

                    //}

                }
                else
                {
                    if (new FileInfo(AppSettings.Settings.SettingsFileName).Length > 32)
                    {
                        Settings = SharedFunctions.ReadFromJsonFile<ClsSettings>(AppSettings.Settings.SettingsFileName);
                    }
                    else
                    {
                        //file too small, must be corrupt - try to revert to backup
                        //Revert to backup copy if exists
                        if (File.Exists(AppSettings.Settings.SettingsFileName + ".bak"))
                        {
                            Console.WriteLine("Settings save failed, reverting to backup copy...");

                            if (File.Exists(AppSettings.Settings.SettingsFileName))
                                File.Delete(AppSettings.Settings.SettingsFileName);

                            File.Move(AppSettings.Settings.SettingsFileName + ".bak", AppSettings.Settings.SettingsFileName);
                            Settings = SharedFunctions.ReadFromJsonFile<ClsSettings>(AppSettings.Settings.SettingsFileName);
                        }

                    }
                    if (Settings != null && Settings.SettingsValid == true)
                    {
                        Ret = true;
                    }
                }


            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: Could not save settings: " + ex.Message);
            }

            return Ret;
        }

    }


}
