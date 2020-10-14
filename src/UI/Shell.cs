using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json; //deserialize DeepquestAI response

using static AITool.AITOOL;

namespace AITool
{

    public partial class Shell:Form
    {
        private ThreadSafe.Datetime LastListUpdate = new ThreadSafe.Datetime(DateTime.MinValue);

        private ThreadSafe.Boolean DatabaseInitialized = new ThreadSafe.Boolean(false);

        private ThreadSafe.Boolean IsListUpdating = new ThreadSafe.Boolean(false);

        private ThreadSafe.Boolean DoneLoading = new ThreadSafe.Boolean(false);

        //Dictionary<string, History> HistoryDic = new Dictionary<string, History>();
        private ThreadSafe.Boolean FilterChanged = new ThreadSafe.Boolean(true);

        //Instantiate a Singleton of the Semaphore with a value of 1. This means that only 1 thread can be granted access at a time.
        public static SemaphoreSlim Semaphore_List_Updating = new SemaphoreSlim(1, 1);

        //public static ConcurrentQueue<History> AddedHistoryItems = new ConcurrentQueue<History>();
        //public static ConcurrentQueue<History> DeletedHistoryItems = new ConcurrentQueue<History>();


        public Shell()
        {
            InitializeComponent();


            //this is to log messages from other classes to the RTF in Shell form, and to log file...
            Global.progress = new Progress<ClsMessage>(EventMessage);

            //Initialize the rich text log window writer.   You can use any 'color' name in your log text
            //for example {red}Error!{white}.  Note if you use $ for the string, you have use two brackets like this: {{red}}
            RTFLogger = new RichTextBoxEx(RTF_Log, true);
            RTFLogger.AutoScroll.WriteFullFence(AppSettings.Settings.Autoscroll_log);

            this.Show();

            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar1.MarqueeAnimationSpeed = 30;

            //---------------------------------------------------------------------------
            //HISTORY TAB

            Global_GUI.ConfigureFOLV(folv_history, typeof(History), new Font("Segoe UI", (float)9.75, FontStyle.Regular), HistoryImageList, "Date", SortOrder.Descending);

            folv_history.EmptyListMsg = "Initializing database";

            Application.DoEvents();

            AITOOL.InitializeBackend();

            string AssemVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lbl_version.Text = $"Version {AssemVer} built on {Global.RetrieveLinkerTimestamp()}";

            //---------------------------------------------------------------------------------------------------------

            this.Resize += new System.EventHandler(this.Form1_Resize); //resize event to enable 'minimize to tray'


            //---------------------------------------------------------------------------
            //CAMERAS TAB

            //left list column setup
            list2.Columns.Add("Camera");

            //set left list column width segmentation (because of some bug -4 is necessary to achieve the correct width)
            list2.Columns[0].Width = list2.Width - 4;
            list2.FullRowSelect = true; //make all columns clickable

            LoadCameras(); //load camera list


            //load entries from history.csv into history ListView
            //LoadFromCSV(); not neccessary because below, comboBox_filter_camera.SelectedIndex will call LoadFromCSV()

            //splitContainer1.Panel2Collapsed = true; //collapse filter panel under left list
            //comboBox_filter_camera.Items.Add("All Cameras"); //add "all cameras" entry in filter dropdown combobox
            comboBox_filter_camera.SelectedIndex = comboBox_filter_camera.FindStringExact("All Cameras"); //select all cameras entry


            //---------------------------------------------------------------------------
            //SETTINGS TAB

            //fill settings tab with stored settings 

            cmbInput.Text = AppSettings.Settings.input_path;
            cb_inputpathsubfolders.Checked = AppSettings.Settings.input_path_includesubfolders;
            cmbInput.Items.Clear();
            foreach (string pth in BlueIrisInfo.ClipPaths)
            {
                cmbInput.Items.Add(pth);

            }

            tbDeepstackUrl.Text = AppSettings.Settings.deepstack_url;
            cb_DeepStackURLsQueued.Checked = AppSettings.Settings.deepstack_urls_are_queued;
            Chk_AutoScroll.Checked = AppSettings.Settings.Autoscroll_log;

            tb_telegram_chatid.Text = String.Join(",", AppSettings.Settings.telegram_chatids);
            tb_telegram_token.Text = AppSettings.Settings.telegram_token;
            tb_telegram_cooldown.Text = AppSettings.Settings.telegram_cooldown_minutes.ToString();
            cb_log.Checked = AppSettings.Settings.log_everything;
            cb_send_errors.Checked = AppSettings.Settings.send_errors;
            cbStartWithWindows.Checked = AppSettings.Settings.startwithwindows;

            //---------------------------------------------------------------------------
            //STATS TAB
            comboBox1.Items.Add("All Cameras"); //add all cameras stats entry
            comboBox1.SelectedIndex = comboBox1.FindStringExact("All Cameras"); //select all cameras entry


            //---------------------------------------------------------------------------
            //Deepstack server TAB


            if (!DeepStackServerControl.IsInstalled)
            {
                //remove deepstack tab if not installed
                Log("Removing DeepStack tab since it not installed as a Windows app (No docker support yet)");
                tabControl1.TabPages.Remove(tabControl1.TabPages["tabDeepStack"]);
            }
            else
            {
                if (DeepStackServerControl.NeedsSaving)
                {
                    //this may happen if the already running instance has a different port, etc, so we update the config
                    SaveDeepStackTab();
                }
                LoadDeepStackTab(true);
            }

            //---------------------------------------------------------------------------
            // finish up
            cb_showMask.Checked = AppSettings.Settings.HistoryShowMask;
            cb_showObjects.Checked = AppSettings.Settings.HistoryShowObjects;
            cb_follow.Checked = AppSettings.Settings.HistoryFollow;
            automaticallyRefreshToolStripMenuItem.Checked = AppSettings.Settings.HistoryAutoRefresh;

            storeFalseAlertsToolStripMenuItem.Checked = AppSettings.Settings.HistoryStoreFalseAlerts;
            storeMaskedAlertsToolStripMenuItem.Checked = AppSettings.Settings.HistoryStoreMaskedAlerts;
            showOnlyRelevantObjectsToolStripMenuItem.Checked = AppSettings.Settings.HistoryOnlyDisplayRelevantObjects;

            HistoryUpdateListTimer.Interval = AppSettings.Settings.TimeBetweenListRefreshsMS;

            this.DoneLoading.WriteFullFence(true);

            Log("APP START complete.");
        }


        async Task UpdateHistoryAddedRemoved()
        {
            //Log("===Enter");
            //this should be a quicker list update
            if (AppSettings.Settings.HistoryAutoRefresh &&
                !this.IsListUpdating.ReadFullFence() &&
                tabControl1.SelectedIndex == 2 &&
                this.Visible &&
                !(this.WindowState == FormWindowState.Minimized) &&
                (DateTime.Now - this.LastListUpdate.Read()).TotalMilliseconds >= AppSettings.Settings.TimeBetweenListRefreshsMS &&
                this.DatabaseInitialized.ReadFullFence() &&
                this.DoneLoading.ReadFullFence() &&
                await HistoryDB.HasUpdates())
            {
                this.IsListUpdating.WriteFullFence(true);

                //Global.Log($"Debug:  Updating list...({AddedHistoryItems.Count} added, {DeletedHistoryItems.Count} deleted)");

                //UpdateToolstrip("Updating list...");

                List<History> added = HistoryDB.GetRecentlyAdded();

                if (added.Count > 0)
                    Global_GUI.UpdateFOLV_AddObjects(folv_history, added.ToArray(), AppSettings.Settings.HistoryFollow);

                List<History> removed = HistoryDB.GetRecentlyDeleted();

                if (removed.Count > 0)
                    Global_GUI.UpdateFOLV_DeleteObjects(folv_history, removed.ToArray(), AppSettings.Settings.HistoryFollow);


                this.LastListUpdate.Write(DateTime.Now);

                this.IsListUpdating.WriteFullFence(false);

                //UpdateToolstrip("");

            }
            else
            {
                //Global.Log($"Debug: List not updated - Refresh={AppSettings.Settings.HistoryAutoRefresh}, Visible={tabControl1.SelectedIndex == 2 && this.Visible && !(this.WindowState == FormWindowState.Minimized)}, IsListUpdating={this.IsListUpdating.ReadFullFence()}, LastListUpdateMS={(DateTime.Now - this.LastListUpdate.Read()).TotalMilliseconds}");
            }
            //Log("===Exit");


        }


        async void EventMessage(ClsMessage msg)
        {
            //output messages from the deepstack, blueiris, etc class to the text log window and log file
            if (msg.MessageType == MessageType.LogEntry)
            {
                Log(msg.Description, "");
            }
            else if (msg.MessageType == MessageType.DatabaseInitialized)
            {
             
                Log("debug: Database initialized.");
                this.DatabaseInitialized.WriteFullFence(true);
                await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow);
                if (AppSettings.Settings.HistoryAutoRefresh)
                {
                    HistoryUpdateListTimer.Enabled = true;
                    HistoryUpdateListTimer.Start();
                }
                else
                {
                    HistoryUpdateListTimer.Enabled = false;
                    HistoryUpdateListTimer.Stop();
                }

            }
            else if (msg.MessageType == MessageType.CreateHistoryItem)
            {
                JsonSerializerSettings jset = new JsonSerializerSettings { };
                jset.TypeNameHandling = TypeNameHandling.All;
                jset.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                History hist = JsonConvert.DeserializeObject<History>(msg.JSONPayload, jset);


                if (!HistoryDB.ReadOnly)
                {
                    bool StoreMasked = hist.Success || !hist.WasMasked || (hist.WasMasked && AppSettings.Settings.HistoryStoreMaskedAlerts);
                    bool StoreFalse = hist.Success || !hist.Detections.ToLower().Contains("false alert") || (hist.Detections.ToLower().Contains("false alert") && AppSettings.Settings.HistoryStoreFalseAlerts);
                    bool Save = true;
                    if (!StoreMasked || !StoreFalse)
                        Save = false;

                    if (Save)
                    {
                        HistoryDB.InsertHistoryQueue(hist);
                    }
                    else
                    {
                        Log($"debug: Not storing item in db - StoreMasked={StoreMasked}, StoreFalse={StoreFalse}: {hist.Detections}");
                    }
                }

                //UpdateHistoryAddedRemoved();

                UpdateToolstrip();
            }
            else if (msg.MessageType == MessageType.DeleteHistoryItem)
            {

                if (!HistoryDB.ReadOnly)  //assume service or other instance will be handling 
                {
                    HistoryDB.DeleteHistoryQueue(msg.Description);
                }

                //UpdateHistoryAddedRemoved();

                UpdateToolstrip();

            }
            else if (msg.MessageType == MessageType.ImageAddedToQueue)
            {
                UpdateQueueLabel();
                UpdateToolstrip();
            }
            else if (msg.MessageType == MessageType.UpdateStatus)
            {
                UpdateQueueLabel();
                UpdateToolstrip();
            }
            else if (msg.MessageType == MessageType.UpdateProgressBar)
            {
                if (toolStripProgressBar1.Maximum != msg.MaxVal)
                    toolStripProgressBar1.Maximum = msg.MaxVal;

                toolStripProgressBar1.Value = msg.CurVal;

                if (toolStripProgressBar1.Style != ProgressBarStyle.Continuous)
                    toolStripProgressBar1.Style = ProgressBarStyle.Continuous;

                UpdateToolstrip(msg.Description);
            }
            else if (msg.MessageType == MessageType.BeginProcessImage)
            {
                BeginProcessImage(msg.Description);
                UpdateToolstrip();
            }
            else if (msg.MessageType == MessageType.EndProcessImage)
            {
                EndProcessImage(msg.Description);
                UpdateToolstrip();
            }
            else if (msg.MessageType == MessageType.UpdateLabel)
            {
                string lblcontrolname = (string)Global.SetJSONString<object>(msg.JSONPayload);

                if (!string.IsNullOrWhiteSpace(lblcontrolname))
                {
                    Label lbl = null;
                    try
                    {
                        lbl = this.Controls.Find(lblcontrolname, true).FirstOrDefault() as Label;
                    }
                    catch (Exception ex)
                    {

                        Log($"Error: Could not find label '{lblcontrolname}': {Global.ExMsg(ex)}");
                    }

                    if (lbl != null)
                    {
                        if (this.Visible)
                        {
                            MethodInvoker LabelUpdate = delegate
                            {
                                lbl.Show();
                                lbl.Text = msg.Description;
                            };
                            //getting error here when called too early - had to check if Visible or not -Vorlon
                            Invoke(LabelUpdate);

                        }
                    }

                }
                else
                {
                    Log($"Error: No label name passed - '{msg.Description}'");

                }

                UpdateToolstrip();
            }
            else
            {
                Log($"Error: Unhandled message type '{msg.MessageType}'");
            }
        }
        //----------------------------------------------------------------------------------------------------------
        //CORE
        //----------------------------------------------------------------------------------------------------------



        //save how many times an error happened
        public void IncrementErrorCounter(string text, string ModName)
        {
            errors.Add(text,"","", LogType.Unknown, DateTime.Now, ModName);

            try
            {
                if (this.Visible)
                {
                    MethodInvoker LabelUpdate = delegate
                    {
                        lbl_errors.Show();
                        lbl_errors.Text = $"{errors.Values.Count.ToString()} error(s) occurred. Click to open Log."; //update error counter label
                        UpdateToolstrip();
                    };
                    //getting error here when called too early - had to check if Visible or not -Vorlon
                    Invoke(LabelUpdate);

                }

            }
            catch (Exception)
            {

            }
        }

        //add text to log
        public async void Log(string text, [CallerMemberName] string memberName = null)
        {

            if (IsClosing.ReadFullFence())
                return;

            try
            {

                //get current date and time

                string time = DateTime.Now.ToString("dd.MM.yyyy, HH:mm:ss");
                string rtftime = DateTime.Now.ToString("dHH:mm:ss");  //no need for date in log tab
                string ModName = "";
                if (memberName == ".ctor")
                    memberName = "Constructor";

                if (AppSettings.Settings.log_everything == true || AppSettings.Settings.deepstack_debug)
                {
                    time = DateTime.Now.ToString("dd.MM.yyyy, HH:mm:ss.fff");
                    rtftime = DateTime.Now.ToString("HH:mm:ss.fff");
                    if (memberName != null && !string.IsNullOrEmpty(memberName))
                        ModName = memberName.PadLeft(24) + "> ";

                    //when the global logger reports back to the progress logger we cant use CallerMemberName, so extract the member name from text

                    int gg = text.IndexOf(">> ");

                    if (gg > 0 && gg <= 24)
                    {
                        string modfromglobal = Global.GetWordBetween(text, "", ">> ");
                        if (!string.IsNullOrEmpty(modfromglobal))
                        {
                            ModName = modfromglobal.PadLeft(24) + "> ";
                            text = Global.GetWordBetween(text, ">> ", "");
                        }

                    }
                }

                //check for messages coming from deepstack processes and kill them if we didnt ask for debugging messages
                if (!AppSettings.Settings.deepstack_debug)
                {
                    if (text.ToLower().Contains("redis-server.exe>") || text.ToLower().Contains("python.exe>"))
                    {
                        return;
                    }
                }

                //make the error and warning detection case insensitive:
                bool HasError = (text.IndexOf("error", StringComparison.InvariantCultureIgnoreCase) > -1) || (text.IndexOf("exception", StringComparison.InvariantCultureIgnoreCase) > -1);
                bool HasWarning = (text.IndexOf("warning:", StringComparison.InvariantCultureIgnoreCase) > -1);
                bool HasInfo = (text.IndexOf("info:", StringComparison.InvariantCultureIgnoreCase) > -1);
                bool HasDebug = (text.IndexOf("debug:", StringComparison.InvariantCultureIgnoreCase) > -1);
                bool IsDeepStackMsg = (memberName.IndexOf("deepstack", StringComparison.InvariantCultureIgnoreCase) > -1) || (text.IndexOf("deepstack", StringComparison.InvariantCultureIgnoreCase) > -1) || (ModName.IndexOf("deepstack", StringComparison.InvariantCultureIgnoreCase) > -1);

                string RTFText = "";

                //set the color for RTF text window:
                if (HasError)
                {
                    RTFText = $"{{gray}}[{rtftime}]: {ModName}{{red}}{text}";
                }
                else if (HasWarning)
                {
                    RTFText = $"{{gray}}[{rtftime}]: {ModName}{{mediumorchid}}{text}";
                }
                else if (IsDeepStackMsg)
                {
                    RTFText = $"{{gray}}[{rtftime}]: {ModName}{{lime}}{text}";
                }
                else if (HasInfo)
                {
                    RTFText = $"{{gray}}[{rtftime}]: {ModName}{{yellow}}{text}";
                }
                else if (HasDebug)
                {
                    RTFText = $"{{gray}}[{rtftime}]: {ModName}{text}";
                }
                else
                {
                    RTFText = $"{{gray}}[{rtftime}]: {ModName}{{white}}{text}";
                }

                if (!AppSettings.AlreadyRunning)
                {
                    Global.SaveSetting("LastLogEntry", RTFText);
                    Global.SaveSetting("LastShutdownState", $"checkpoint: GUI.Log: {DateTime.Now}");
                }

                //get rid of any common color coding before logging to file or console
                text = text.Replace("{yellow}", "").Replace("{red}", "").Replace("{white}", "").Replace("{orange}", "").Replace("{lime}", "").Replace("{orange}", "mediumorchid");

                //if log everything is disabled and the text is neither an ERROR, nor a WARNING: write only to console and ABORT
                if (AppSettings.Settings.log_everything == false && !HasError && !HasWarning)
                {
                    //Creates a lot of extra text in immediate window while debugging, disabling -Vorlon
                    //text += "Enabling \'Log everything\' might give more information.";
                    Console.WriteLine($"[{rtftime}]: {ModName}{text}");

                    return;
                }



                RTFLogger.LogToRTF(RTFText);
                LogWriter.WriteToLog($"[{time}]:  {ModName}{text}", HasError);


                if (AppSettings.Settings.send_errors == true && (HasError || HasWarning) && !text.ToLower().Contains("telegram"))
                {
                    //await TelegramText($"[{time}]: {text}"); //upload text to Telegram
                    AITOOL.TriggerActionQueue.AddTriggerActionAsync(TriggerType.TelegramText, null, null, null, true, false, null, $"[{time}]: {text}") ;

                }

                                //add log text to console
                Console.WriteLine($"[{rtftime}]: {ModName}{text}");

                //increment error counter
                if (HasError || HasWarning)
                {
                    IncrementErrorCounter(text, ModName);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: In LOG, got: " + ex.Message);
            }

        }



        //----------------------------------------------------------------------------------------------------------
        //GUI
        //----------------------------------------------------------------------------------------------------------

        //minimize to tray
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.Visible = true;
            }
            else
            {
                ResizeListViews();
            }
        }

        //open from tray
        private async void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow);
        }

        //open Log when clicking or error message
        private void lbl_errors_Click(object sender, EventArgs e)
        {
            ShowErrors();

        }

        private void ShowErrors()
        {

            using (Frm_Errors frm = new Frm_Errors())
            {
                frm.errors = errors.Values;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lbl_errors.Text = "";
                    errors.Clear();
                    UpdateToolstrip();
                }
            }
        }

        //adapt list views (history tab and cameras tab) to window size while considering scrollbar influence
        private void ResizeListViews()
        {
            //suspend layout of most complex tablelayout elements (gives a few milliseconds)
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            //tableLayoutPanel9.SuspendLayout();


            if (list2.Columns.Count > 0)
                list2.Columns[0].Width = list2.Width - 4; //resize camera list column

            //resume layout again
            tableLayoutPanel7.ResumeLayout();
            tableLayoutPanel8.ResumeLayout();
            //tableLayoutPanel9.ResumeLayout();
        }


        //EVENTS:

        //event: mouse click on tab control
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            ResizeListViews();
        }

        //event: another tab selected (Only load certain things in tabs if they are actually open)
        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.DoEvents();

            if (tabControl1.SelectedIndex == 1)
            {
                UpdatePieChart(); UpdateTimeline(); UpdateConfidenceChart();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabDeepStack"])
            {
                LoadDeepStackTab(true);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabLog"])
            {
                //scroll to bottom, only when tab is active for better performance 

                Global_GUI.InvokeIFRequired(this.RTF_Log, () =>
                {
                    if (Chk_AutoScroll.Checked)
                    {
                        this.RTF_Log.SelectionStart = this.RTF_Log.Text.Length;
                        this.RTF_Log.ScrollToCaret();
                    }
                });
            }
            Application.DoEvents();

        }


        //----------------------------------------------------------------------------------------------------------
        //STATS TAB
        //----------------------------------------------------------------------------------------------------------

        //other camera in combobox selected, display according PieChart
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        //update pie chart
        public void UpdatePieChart()
        {

            if (tabControl1.SelectedIndex != 1 || !this.Visible || this.WindowState == FormWindowState.Minimized || string.IsNullOrEmpty(comboBox1.Text))
                return;

            int alerts = 0;
            int irrelevantalerts = 0;
            int falsealerts = 0;
            int skipped = 0;

            if (comboBox1.Text == "All Cameras")
            {
                foreach (Camera cam in AppSettings.Settings.CameraList)
                {
                    alerts += cam.stats_alerts;
                    irrelevantalerts += cam.stats_irrelevant_alerts;
                    falsealerts += cam.stats_false_alerts;
                    skipped += cam.stats_skipped_images;
                }
            }
            else
            {

                Camera cam = AITOOL.GetCamera(comboBox1.Text);  //int i = AppSettings.Settings.CameraList.FindIndex(x => x.name.ToLower().Trim() == comboBox1.Text.ToLower().Trim());
                if (cam != null)
                {
                    alerts = cam.stats_alerts;
                    irrelevantalerts = cam.stats_irrelevant_alerts;
                    falsealerts = cam.stats_false_alerts;
                    skipped = cam.stats_skipped_images;
                }
                else
                {
                    alerts = 0;
                    irrelevantalerts = 0;
                    falsealerts = 0;
                    skipped = 0;
                    Log($"Error: Could not match combobox dropdown '{comboBox1.Text}' to a known camera name?");
                }
            }

            chart1.Series[0].Points.Clear();

            chart1.Series[0].LegendText = "#VALY"; //"#VALY #VALX"
            chart1.Series[0]["PieLabelStyle"] = "Disabled";

            int index = -1;

            //show Alerts label
            index = chart1.Series[0].Points.AddXY("Alerts", alerts);
            chart1.Series[0].Points[index].Color = System.Drawing.Color.Green;

            //show irrelevant Alerts label
            index = chart1.Series[0].Points.AddXY("irrelevant Alerts", irrelevantalerts);
            chart1.Series[0].Points[index].Color = System.Drawing.Color.Orange;

            //show false Alerts label
            index = chart1.Series[0].Points.AddXY("false Alerts", falsealerts);
            chart1.Series[0].Points[index].Color = System.Drawing.Color.OrangeRed;

            //show skipped label
            index = chart1.Series[0].Points.AddXY("Skipped Images", skipped);
            chart1.Series[0].Points[index].Color = System.Drawing.Color.Purple;



        }

        //update timeline
        public async void UpdateTimeline()
        {

            if (tabControl1.SelectedIndex != 1 || !this.Visible || this.WindowState == FormWindowState.Minimized || string.IsNullOrEmpty(comboBox1.Text))
                return;

            Log("Loading time line from cameras/history.csv ...");

            //clear previous values
            timeline.Series[0].Points.Clear();
            timeline.Series[1].Points.Clear();
            timeline.Series[2].Points.Clear();
            timeline.Series[3].Points.Clear();
            timeline.Series[4].Points.Clear();

            Stopwatch SW = Stopwatch.StartNew();

            try
            {
                List<History> result = HistoryDB.HistoryDic.Values.ToList();

                if (comboBox1.Text.Trim().ToLower() != "All Cameras".ToLower()) //all cameras selected
                {
                    result = result.Where(hist => hist.Camera.ToLower().StartsWith(comboBox1.Text.Trim().ToLower())).ToList();
                }

                //every int represents the number of ai calls in successive half hours (p.e. relevant[0] is 0:00-0:30 o'clock, relevant[1] is 0:30-1:00 o'clock) 
                int[] all = new int[48];
                int[] falses = new int[48];
                int[] irrelevant = new int[48];
                int[] relevant = new int[48];
                int[] skipped = new int[48];

                //fill arrays with amount of calls/half hour
                foreach (History hist in result)
                {               //example of time column entry: 23.08.19, 18:31:09
                                //get hour
                    int hour = hist.Date.Hour;

                    //get minute
                    int minute = hist.Date.Minute;

                    int halfhour; //stores the half hour in which the alert occured

                    //add +1 to counter for corresponding half-hour
                    if (minute > 30) //if alert occurred after half o clock
                    {
                        halfhour = hour * 2 + 1;
                    }
                    else //if alert occured before half o clock
                    {
                        halfhour = hour * 2;
                    }

                    //if detection was successful
                    if (hist.Success)
                    {
                        relevant[halfhour]++;
                    }
                    //if it was a false alert
                    else if (hist.Detections.ToLower() == "false alert")
                    {
                        falses[halfhour]++;
                    }
                    //if something irrelevant was detected
                    else
                    {
                        irrelevant[halfhour]++;
                    }

                    if (hist.WasSkipped)
                        skipped[halfhour]++;

                    all[halfhour]++;
                }

                //add to graph "all":

                /*the graph will have a gap at the end and at the beginning if we don'f specify a value
                * with an x value outside the visible area at the end and before the first visible point. 
                * So the first point is at -0.25 and has the value of the last visible point and the 
                * last point is at 24.25 and has the value of the first visible point. */

                timeline.Series[0].Points.AddXY(-0.25, all[47]); // beginning point with value of last visible point

                //and now add all visible points 
                double x = 0.25;
                foreach (int halfhour in all)
                {
                    int index = timeline.Series[0].Points.AddXY(x, halfhour);
                    x = x + 0.5;
                }

                timeline.Series[0].Points.AddXY(24.25, all[0]); // finally add last point with value of first visible point

                //add to graph "falses":

                timeline.Series[1].Points.AddXY(-0.25, falses[47]); // beginning point with value of last visible point
                                                                    //and now add all visible points 
                x = 0.25;
                foreach (int halfhour in falses)
                {
                    int index = timeline.Series[1].Points.AddXY(x, halfhour);
                    x = x + 0.5;
                }
                timeline.Series[1].Points.AddXY(24.25, falses[0]); // finally add last point with value of first visible point

                //add to graph "irrelevant":

                timeline.Series[2].Points.AddXY(-0.25, irrelevant[47]); // beginning point with value of last visible point
                                                                        //and now add all visible points 
                x = 0.25;
                foreach (int halfhour in irrelevant)
                {
                    int index = timeline.Series[2].Points.AddXY(x, halfhour);
                    x = x + 0.5;
                }
                timeline.Series[2].Points.AddXY(24.25, irrelevant[0]); // finally add last point with value of first visible point

                //add to graph "relevant":

                timeline.Series[3].Points.AddXY(-0.25, relevant[47]); // beginning point with value of last visible point
                                                                      //and now add all visible points 
                x = 0.25;
                foreach (int halfhour in relevant)
                {
                    int index = timeline.Series[3].Points.AddXY(x, halfhour);
                    x = x + 0.5;
                }
                timeline.Series[3].Points.AddXY(24.25, relevant[0]); // finally add last point with value of first visible point


                //add to graph "skipped":

                timeline.Series[4].Points.AddXY(-0.25, skipped[47]); // beginning point with value of last visible point
                                                                     //and now add all visible points 
                x = 0.25;
                foreach (int halfhour in skipped)
                {
                    int index = timeline.Series[4].Points.AddXY(x, halfhour);
                    x = x + 0.5;
                }
                timeline.Series[4].Points.AddXY(24.25, skipped[0]); // finally add last point with value of first visible point



            }
            catch (Exception ex)
            {

                Log("Error: " + Global.ExMsg(ex));
            }




        }

        //update confidence_frequency chart
        public async void UpdateConfidenceChart()
        {

            if (tabControl1.SelectedIndex != 1 || !this.Visible || this.WindowState == FormWindowState.Minimized || string.IsNullOrEmpty(comboBox1.Text))
                return;


            Log("Loading confidence-frequency chart from cameras/history.csv ...");

            //clear previous values
            chart_confidence.Series[0].Points.Clear();
            chart_confidence.Series[1].Points.Clear();


            Stopwatch SW = Stopwatch.StartNew();

            try
            {
                List<History> result = HistoryDB.HistoryDic.Values.ToList();

                if (comboBox1.Text.Trim().ToLower() != "All Cameras".ToLower()) //all cameras selected
                {
                    result = result.Where(hist => hist.Camera.ToLower().StartsWith(comboBox1.Text.Trim().ToLower())).ToList();
                }

                //this array stores the Absolute frequencies of all possible confidence values (0%-100%)
                int[] green_values = new int[101];
                int[] orange_values = new int[101];

                //fill array with frequencies
                foreach (History hist in result)
                {
                    //example of detections column entry: "person (41%); person (97%);" or "masked: person (41%); person (97%);"
                    string detections_column = hist.Detections;
                    if (detections_column.Contains(':'))
                    {
                        detections_column = detections_column.Split(':')[1];

                        string[] detections = detections_column.Split(';');

                        //write the confidence of every detection into the green_values string
                        foreach (string detection in detections)
                        {
                            int x_value = Global.GetNumberInt(detection);  // gets a number anywhere in the string

                            //fix temp bug from earlier where whole number percentages were multiplied by 100 when they shouldnt have been.
                            if (x_value > 500)
                                x_value = x_value / 100;

                            if (x_value > 0)
                            {
                                //example: -> "person (41%)"
                                //Int32.TryParse(detection.Split('(')[1].Split('%')[0], out int x_value); //example: -> "41"
                                orange_values[x_value]++;
                            }
                        }
                    }
                    else
                    {
                        string[] detections = detections_column.Split(';');

                        //write the confidence of every detection into the green_values string
                        foreach (string detection in detections)
                        {
                            int x_value = Global.GetNumberInt(detection);  // gets a number anywhere in the string
                            if (x_value > 0)
                            {
                                //example: -> "person (41%)"
                                //Int32.TryParse(detection.Split('(')[1].Split('%')[0], out int x_value); //example: -> "41"
                                green_values[x_value]++;
                            }
                        }
                    }
                }


                //write green series in chart
                int i = 0;
                foreach (int y_value in green_values)
                {
                    chart_confidence.Series[1].Points.AddXY(i, y_value);
                    i++;
                }

                //write orange series in chart
                i = 0;
                foreach (int y_value in orange_values)
                {
                    chart_confidence.Series[0].Points.AddXY(i, y_value);
                    i++;
                }



            }
            catch (Exception ex)
            {

                Log("Error: " + Global.ExMsg(ex));
            }


        }


        private void showHideMask()
        {
            if (cb_showMask.Checked == true) //show overlay
            {
                //Log("Show mask toggled.");

                if (folv_history.SelectedObjects != null && folv_history.SelectedObjects.Count > 0)
                {
                    History hist = (History)folv_history.SelectedObjects[0];

                    string imagefile = AITOOL.GetMaskFile(hist.Camera);

                    if (File.Exists(imagefile))
                    {
                        using (var img = new Bitmap(imagefile))
                        {
                            pictureBox1.Image = new Bitmap(img); //load mask as overlay
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null; //if file does not exist, empty mask overlay (from possible overlays of previous images)
                    }

                }

            }
            else //if showmask toggle-button is not checked, hide the mask overlay
            {
                pictureBox1.Image = null;
            }

        }

        //show rectangle overlay
        private void showObject(PaintEventArgs e, int _xmin, int _ymin, int _xmax, int _ymax, string text, ResultType result)
        {
            try
            {
                if (folv_history.SelectedObjects != null && folv_history.SelectedObjects.Count > 0 && (pictureBox1 != null) && (pictureBox1.BackgroundImage != null))
                {

                    System.Drawing.Color color = new System.Drawing.Color();
                    int BorderWidth = AppSettings.Settings.RectBorderWidth
;

                    if (result == ResultType.Relevant)
                    {
                        color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectRelevantColorAlpha, AppSettings.Settings.RectRelevantColor);
                    }
                    else if (result == ResultType.DynamicMasked || result == ResultType.ImageMasked || result == ResultType.StaticMasked)
                    {
                        color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectMaskedColorAlpha, AppSettings.Settings.RectMaskedColor);
                    }
                    else
                    {
                        color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectIrrelevantColorAlpha, AppSettings.Settings.RectIrrelevantColor);
                    }

                    //1. get the padding between the image and the picturebox border

                    //get dimensions of the image and the picturebox
                    float imgWidth = pictureBox1.BackgroundImage.Width;
                    float imgHeight = pictureBox1.BackgroundImage.Height;
                    float boxWidth = pictureBox1.Width;
                    float boxHeight = pictureBox1.Height;

                    //these variables store the padding between image border and picturebox border
                    int absX = 0;
                    int absY = 0;

                    //because the sizemode of the picturebox is set to 'zoom', the image is scaled down
                    float scale = 1;


                    //Comparing the aspect ratio of both the control and the image itself.
                    if (imgWidth / imgHeight > boxWidth / boxHeight) //if the image is p.e. 16:9 and the picturebox is 4:3
                    {
                        scale = boxWidth / imgWidth; //get scale factor
                        absY = (int)(boxHeight - scale * imgHeight) / 2; //padding on top and below the image
                    }
                    else //if the image is p.e. 4:3 and the picturebox is widescreen 16:9
                    {
                        scale = boxHeight / imgHeight; //get scale factor
                        absX = (int)(boxWidth - scale * imgWidth) / 2; //padding left and right of the image
                    }

                    //2. inputted position values are for the original image size. As the image is probably smaller in the picturebox, the positions must be adapted. 
                    int xmin = (int)(scale * _xmin) + absX;
                    int xmax = (int)(scale * _xmax) + absX;
                    int ymin = (int)(scale * _ymin) + absY;
                    int ymax = (int)(scale * _ymax) + absY;


                    //3. paint rectangle
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
                    using (Pen pen = new Pen(color, BorderWidth))
                    {
                        e.Graphics.DrawRectangle(pen, rect); //draw rectangle
                    }

                    //we need this since people can change the border width in the json file
                    int halfbrd = BorderWidth / 2;

                    //object name text below rectangle
                    rect = new System.Drawing.Rectangle(xmin - halfbrd, ymax + halfbrd, (int)boxWidth, (int)boxHeight); //sets bounding box for drawn text

                    Brush brush = new SolidBrush(color); //sets background rectangle color

                    System.Drawing.SizeF size = e.Graphics.MeasureString(text, new Font(AppSettings.Settings.RectDetectionTextFont, AppSettings.Settings.RectDetectionTextSize)); //finds size of text to draw the background rectangle
                    e.Graphics.FillRectangle(brush, xmin - halfbrd, ymax + halfbrd, size.Width, size.Height); //draw grey background rectangle for detection text
                    e.Graphics.DrawString(text, new Font(AppSettings.Settings.RectDetectionTextFont, AppSettings.Settings.RectDetectionTextSize), Brushes.Black, rect); //draw detection text



                }

            }
            catch (Exception ex)
            {

                Log("Error: " + Global.ExMsg(ex));
            }
        }

        //load object rectangle overlays
        //TODO: refactor detections
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (AppSettings.Settings.HistoryShowObjects && folv_history.SelectedObjects != null && folv_history.SelectedObjects.Count > 0) //if checkbox button is enabled
            {
                //Log("Loading object rectangles...");
                History hist = (History)folv_history.SelectedObjects[0];

                string positions = hist.Positions;
                string detections = hist.Detections;

                int XOffset = 0;
                int YOffset = 0;

                Camera cam = AITOOL.GetCamera(hist.Camera);
                if (cam != null)
                {
                    //apply offset if one is defined by user in json file
                    XOffset = cam.XOffset;
                    YOffset = cam.YOffset;
                }

                try
                {

                    if (!string.IsNullOrEmpty(hist.PredictionsJSON))
                    {
                        List<ClsPrediction> predictions = new List<ClsPrediction>();

                        predictions = Global.SetJSONString<List<ClsPrediction>>(hist.PredictionsJSON);

                        foreach (var pred in predictions)
                        {
                            if (AppSettings.Settings.HistoryOnlyDisplayRelevantObjects && pred.Result == ResultType.Relevant)
                            {
                                showObject(e, pred.xmin + XOffset, pred.ymin + YOffset, pred.xmax, pred.ymax, pred.ToString(), pred.Result); //call rectangle drawing method, calls appropriate detection text
                            }
                            else if (!AppSettings.Settings.HistoryOnlyDisplayRelevantObjects)
                            {
                                showObject(e, pred.xmin + XOffset, pred.ymin + YOffset, pred.xmax, pred.ymax, pred.ToString(), pred.Result); //call rectangle drawing method, calls appropriate detection text
                            }

                        }

                    }
                    else
                    {
                        List<string> positionssArray = Global.Split(positions, ";");//creates array of detected objects, used for adding text overlay

                        int countr = positionssArray.Count();

                        ResultType result = ResultType.Unknown;

                        if (detections.ToLower().Contains("irrelevant") || detections.ToLower().Contains("masked") || detections.ToLower().Contains("confidence"))
                        {
                            detections = detections.Split(':')[1]; //removes the "1x masked, 3x irrelevant:" before the actual detection, otherwise this would be displayed in the detection tags
                            if (detections.Contains("masked"))
                            {
                                result = ResultType.ImageMasked;
                            }
                            else
                            {
                                result = ResultType.Unknown;
                            }
                        }
                        else
                        {
                            result = ResultType.Relevant;
                        }

                        List<string> detectionsArray = Global.Split(detections, ";");//creates array of detected objects, used for adding text overlay

                        if (cam != null)
                        {
                            //apply offset if one is defined by user in json file
                            XOffset = cam.XOffset;
                            YOffset = cam.YOffset;
                        }

                        //display a rectangle around each relevant object
                        for (int i = 0; i < countr; i++)
                        {

                            //load 'xmin,ymin,xmax,ymax' from third column into a string
                            List<string> positionsplt = Global.Split(positionssArray[i], ",");

                            //store xmin, ymin, xmax, ymax in separate variables
                            Int32.TryParse(positionsplt[0], out int xmin);
                            Int32.TryParse(positionsplt[1], out int ymin);
                            Int32.TryParse(positionsplt[2], out int xmax);
                            Int32.TryParse(positionsplt[3], out int ymax);


                            showObject(e, xmin + XOffset, ymin + YOffset, xmax, ymax, detectionsArray[i], result); //call rectangle drawing method, calls appropriate detection text

                        }

                    }


                }
                catch (Exception ex)
                {

                    Log($"Error: Positions (subitem4) ='{positions}', Detections (subitem3) ='{detections}': {Global.ExMsg(ex)}");
                }

            }
        }

        // add new entry in left list


        private void UpdateToolstrip(string Message = "")
        {
            try
            {


                if (!this.Visible || (this.WindowState == FormWindowState.Minimized))
                    return;  //save a tree

                int alerts = 0;
                int irrelevantalerts = 0;
                int falsealerts = 0;
                int newskipped = 0;
                int skipped = 0;
                foreach (Camera cam in AppSettings.Settings.CameraList)
                {
                    alerts += cam.stats_alerts;
                    irrelevantalerts += cam.stats_irrelevant_alerts;
                    falsealerts += cam.stats_false_alerts;
                    skipped += cam.stats_skipped_images;
                    newskipped += cam.stats_skipped_images_session;
                }

                Global_GUI.InvokeIFRequired(toolStripStatusLabelHistoryItems.GetCurrentParent(), () =>
                {

                    double hpm = 0;
                    int items = 0;
                    int removed = 0;

                    if (HistoryDB != null)
                    {
                        items = HistoryDB.HistoryDic.Count();
                        removed = HistoryDB.DeletedCount.ReadFullFence();
                    }

                    if (HistoryDB != null && HistoryDB.AddedCount.ReadFullFence() > 0)
                        hpm = HistoryDB.AddedCount.ReadFullFence() / (DateTime.Now - HistoryDB.InitializeTime).TotalMinutes;

                    toolStripStatusLabelHistoryItems.Text = $"{items} history items ({hpm.ToString("###0.0")}/MIN) | {removed} removed";

                    toolStripStatusLabel1.Text = $"| {alerts} Alerts | {irrelevantalerts} Irrelevant | {falsealerts} False | {skipped} Skipped ({newskipped} new) | {ImageProcessQueue.Count} ImgQueued | {TriggerActionQueue.Count} Action Queued";

                    toolStripStatusErrors.Text = $"| {errors.Values.Count} Errors";

                    if (!string.IsNullOrEmpty(Message))
                    {
                        toolStripStatusLabelInfo.Text = Message;
                    }
                    else if (ImageProcessQueue.Count > 0 && toolStripProgressBar1.Value == 0)
                    {
                        toolStripStatusLabelInfo.Text = "Working...";
                    }
                    else if (toolStripProgressBar1.Value == 0)
                    {
                        toolStripStatusLabelInfo.Text = "Idle.";
                    }

                    //if (toolStripProgressBar1.Style == ProgressBarStyle.Marquee && toolStripStatusLabelInfo.Text == "Idle.")
                    //{
                    //    toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
                    //}


                    if (errors.Values.Count() > 0)
                    {
                        toolStripStatusErrors.ForeColor = Color.Red;
                    }
                    else
                    {
                        toolStripStatusErrors.ForeColor = toolStripStatusLabelHistoryItems.GetCurrentParent().ForeColor;
                    }

                });

            }
            catch (Exception ex)
            {

                Log("Error: " + Global.ExMsg(ex));
            }
        }
        //load stored entries in history CSV into history ListView
        private async Task LoadHistoryAsync(bool FilterChanged, bool Follow)
        {

            //Log("---Enter");

            if (!this.DoneLoading.ReadFullFence())  //when you set checkboxes during init, it may trigger the event to load the history
            {
                //Log("---Exit (still loading)");
                return;
            }

            //make sure only one thread updating at a time
            //await Semaphore_List_Updating.WaitAsync();

            if (this.IsListUpdating.ReadFullFence())
            {
                Log("---Exit (already updating)");
                return;
            }

            Stopwatch semsw = Stopwatch.StartNew();

            this.IsListUpdating.WriteFullFence(true);
            this.LastListUpdate.Write(DateTime.Now);

            Global_GUI.CursorWait cw = null;

            try
            {
                if (semsw.ElapsedMilliseconds > 50)
                    Log($"debug: Waited {semsw.ElapsedMilliseconds}ms while waiting for other threads to finish.");

                //wait a bit for the list to be available
                Stopwatch sw = Stopwatch.StartNew();
                bool displayed = false;
                do
                {
                    if (HistoryDB != null && HistoryDB.HistoryDic != null && folv_history != null && this.DatabaseInitialized.ReadFullFence())
                        break;
                    else if (!displayed)
                    {

                        Log("debug: Waiting for database to finish initializing...");
                        displayed = true;
                    }
                    await Task.Delay(50);

                } while (sw.ElapsedMilliseconds < 60000);

                if (displayed)
                    Log($"...debug: Waited {sw.ElapsedMilliseconds}ms for the database to finish initializing/cleaning.");

                //Dont update list unless we are on the tab and it is visible for performance reasons.
                if (FilterChanged || folv_history.Items.Count == 0 || (tabControl1.SelectedIndex == 2 && this.Visible && !(this.WindowState == FormWindowState.Minimized)))
                {


                    if (this.Visible && !(this.WindowState == FormWindowState.Minimized))
                        this.UpdateToolstrip("Updating History List...");

                    if (FilterChanged)
                        cw = new Global_GUI.CursorWait();

                    if (await HistoryDB.HasUpdates() || FilterChanged)
                    {
                        Global_GUI.UpdateFOLV_add(folv_history, HistoryDB.HistoryDic.Values.ToList(), FilterChanged, Follow);

                        //reset any that snuck in while waiting since we just did a full list update
                        HistoryDB.GetRecentlyAdded();
                        HistoryDB.GetRecentlyDeleted();

                        if (FilterChanged)
                        {
                            Global_GUI.InvokeIFRequired(folv_history, () =>
                            {

                                if (comboBox_filter_camera.Text != "All Cameras" || cb_filter_animal.Checked || cb_filter_nosuccess.Checked || cb_filter_person.Checked || cb_filter_success.Checked || cb_filter_vehicle.Checked || cb_filter_skipped.Checked)
                                {
                                    //filter
                                    folv_history.ModelFilter = new BrightIdeasSoftware.ModelFilter(delegate (object x)
                                {
                                    History hist = (History)x;
                                    return checkListFilters(hist);
                                });
                                }
                                else
                                {
                                    folv_history.ModelFilter = null;
                                }

                            });

                        }


                    }
                    else
                    {
                        //Log("debug: No history file updates.");
                    }

                    if (this.Visible && !(this.WindowState == FormWindowState.Minimized))
                        this.UpdateToolstrip("Idle.");

                }
                else
                {
                    //Log("debug: Not updating history, window not visible or history tab not selected.");
                }

            }
            catch (Exception ex)
            {
                Log("Error: " + Global.ExMsg(ex));
            }
            finally
            {
                if (cw != null)
                    cw.Dispose();
                this.LastListUpdate.Write(DateTime.Now);
                this.IsListUpdating.WriteFullFence(false);
                //Semaphore_List_Updating.Release();
                //Log("---Exit");

            }

        }

        //check if a filter applies on given string of history list entry 
        private bool checkListFilters(History hist)   //string cameraname, string success, string objects_and_confidence
        {

            bool ret = true;

            if (!hist.Success && cb_filter_success.Checked)
                ret = false;
            
            if (hist.Success && cb_filter_nosuccess.Checked)
                ret = false;
            
            if (!hist.WasSkipped && cb_filter_skipped.Checked)
                ret = false;
            
            if (!hist.WasMasked && cb_filter_masked.Checked)
                ret = false;  
            
            if (!hist.IsPerson && cb_filter_person.Checked)
                ret = false;
            
            if (!hist.IsVehicle && cb_filter_vehicle.Checked)
                ret = false;
            
            if (!hist.IsAnimal && cb_filter_animal.Checked)
                ret = false;

            bool CameraValid = ((comboBox_filter_camera.Text.Trim() == "All Cameras") || hist.Camera.Trim().ToLower() == comboBox_filter_camera.Text.Trim().ToLower());

            if (!CameraValid)
                ret = false;

            return ret;


        }



        //EVENTS

        private void BeginProcessImage(string image_path)
        {

            string filename = Path.GetFileName(image_path);

            MethodInvoker LabelUpdate = delegate { label2.Text = $"Accessing {filename}..."; };
            Invoke(LabelUpdate);

            //output "Processing Image" to Overview Tab
            LabelUpdate = delegate { label2.Text = $"Processing {filename}..."; };
            Invoke(LabelUpdate);

            UpdateQueueLabel();

        }

        private void EndProcessImage(string image_path)
        {

            string filename = Path.GetFileName(image_path);


            //output Running on Overview Tab
            MethodInvoker LabelUpdate = delegate { label2.Text = "Running"; };
            Invoke(LabelUpdate);

            //only update charts if stats tab is open

            LabelUpdate = delegate
            {

                UpdatePieChart(); UpdateTimeline(); UpdateConfidenceChart();
            };
            Invoke(LabelUpdate);

            //load updated camera stats info in camera tab if a camera is selected
            LabelUpdate = delegate
            {
                if (list2.SelectedItems.Count > 0)
                {
                    //load only stats from Camera.cs object

                    //all camera objects are stored in the list CameraList, so firstly the position (stored in the second column for each entry) is gathered
                    Camera cam = AITOOL.GetCamera(list2.SelectedItems[0].Text);

                    //load cameras stats
                    string stats = $"Alerts: {cam.stats_alerts.ToString()} | Irrelevant Alerts: {cam.stats_irrelevant_alerts.ToString()} | False Alerts: {cam.stats_false_alerts.ToString()}";
                    if (cam.maskManager.MaskingEnabled)
                    {
                        stats += $" | Mask History Count: {cam.maskManager.LastPositionsHistory.Count()} | Current Dynamic Masks: {cam.maskManager.MaskedPositions.Count()}";
                    }
                    lbl_camstats.Text = stats;
                }


            };
            Invoke(LabelUpdate);


            UpdateQueueLabel();

        }


        //event: load selected image to picturebox


        //event: show mask button clicked
        private void cb_showMask_CheckedChanged(object sender, EventArgs e)
        {
            if (folv_history.SelectedObjects != null && folv_history.SelectedObjects.Count > 0)
            {
                showHideMask();
            }
        }

        //event: show objects button clicked
        private void cb_showObjects_MouseUp(object sender, MouseEventArgs e)
        {
            if (folv_history.SelectedObjects != null && folv_history.SelectedObjects.Count > 0)
            {
                pictureBox1.Refresh();
            }

        }

        //event: show history list filters button clicked
        //private void cb_showFilters_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (cb_showFilters.Checked)
        //    {
        //        cb_showFilters.Text = "˅ Filter";
        //        splitContainer1.Panel2Collapsed = false;
        //    }
        //    else
        //    {
        //        cb_showFilters.Text = "˄ Filter";
        //        splitContainer1.Panel2Collapsed = true;
        //    }

        //    ResizeListViews();

        //}

        //event: filter "only revelant alerts" checked or unchecked



        //----------------------------------------------------------------------------------------------------------
        //CAMERAS TAB
        //----------------------------------------------------------------------------------------------------------

        //BASIC METHODS

        // load cameras to camera list
        public void LoadCameras()
        {

            try
            {

                //start by getting last selected camera if any
                string oldnamecameras = "";
                if (list2.SelectedItems != null && list2.SelectedItems.Count > 0)
                    oldnamecameras = list2.SelectedItems[0].Text;

                string oldnamefilters = "";
                if (comboBox_filter_camera.Items.Count > 0)
                    oldnamefilters = comboBox_filter_camera.Text;

                string oldnamestats = "";
                if (comboBox1.Items.Count > 0)
                    oldnamestats = comboBox1.Text;

                list2.Items.Clear();
                comboBox1.Items.Clear();
                comboBox1.Items.Add("All Cameras");
                comboBox_filter_camera.Items.Clear();
                comboBox_filter_camera.Items.Add("All Cameras");

                int i = 0;
                int oldidxcameras = 0;
                int oldidxfilters = 0;
                int oldidxstats = 0;
                foreach (Camera cam in AppSettings.Settings.CameraList)
                {
                    //Add loaded camera to list2
                    ListViewItem item = new ListViewItem(new string[] { cam.name });
                    if (!cam.enabled)
                    {
                        item.ForeColor = System.Drawing.Color.Gray;
                    }
                    //item.Tag = file; //tag is not used anywhere I can see
                    list2.Items.Add(item);
                    //add camera to combobox on overview tab and to camera filter combobox in the History tab 
                    comboBox1.Items.Add($"   {cam.name}");
                    comboBox_filter_camera.Items.Add($"   {cam.name}");
                    if (oldnamecameras.Trim().ToLower() == cam.name.Trim().ToLower())
                    {
                        oldidxcameras = i;
                    }
                    if (oldnamefilters.Trim().ToLower() == cam.name.Trim().ToLower())
                    {
                        oldidxfilters = i + 1;
                    }
                    if (oldnamestats.Trim().ToLower() == cam.name.Trim().ToLower())
                    {
                        oldidxstats = i + 1;
                    }
                    i++;

                }

                //select first camera, or last selected camera
                if (list2.Items.Count > 0 && list2.Items.Count >= oldidxcameras)
                {
                    list2.Items[oldidxcameras].Selected = true;
                }

                if (comboBox_filter_camera.Items.Count > 0)
                    comboBox_filter_camera.SelectedIndex = oldidxfilters;

                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = oldidxstats;


            }
            catch
            {
                Log("ERROR LoadCameras() failed.");
                MessageBox.Show("ERROR LoadCameras() failed.");
            }

        }

        //load existing camera (settings file exists) into CameraList, into Stats dropdown and into History filter dropdown 
        //private string LoadCamera(string config_path)
        //{
        //    //check if camera with specified name or its prefix already exists. If yes, then abort.
        //    foreach (Camera c in AppSettings.Settings.CameraList)
        //    {
        //        if (c.name == Path.GetFileNameWithoutExtension(config_path))
        //        {
        //            return ($"ERROR: Camera name must be unique,{Path.GetFileNameWithoutExtension(config_path)} already exists.");
        //        }
        //        if (c.prefix == System.IO.File.ReadAllLines(config_path)[2].Split('"')[1])
        //        {
        //            return ($"ERROR: Every camera must have a unique prefix ('Input file begins with'), but the prefix of {Path.GetFileNameWithoutExtension(config_path)} equals the prefix of the existing camera {c.name} .");
        //        }
        //    }
        //    Camera cam = new Camera(); //create new camera object
        //    Log("read config");
        //    cam.ReadConfig(config_path); //read camera's config from file
        //    Log("add");
        //    AppSettings.Settings.CameraList.Add(cam); //add created camera object to CameraList

        //    //add camera to combobox on overview tab and to camera filter combobox in the History tab 
        //    comboBox1.Items.Add($"   {cam.name}");
        //    comboBox_filter_camera.Items.Add($"   {cam.name}");

        //    return ($"SUCCESS: {Path.GetFileNameWithoutExtension(config_path)} loaded.");
        //}

        //add camera
        private string AddCamera(Camera cam) //, int history_mins, int mask_create_counter, int mask_remove_counter, double percent_variance)
        {
            //check if camera with specified name already exists. If yes, then abort.
            foreach (Camera c in AppSettings.Settings.CameraList)
            {
                if (c.name.Trim().ToLower() == cam.name.Trim().ToLower())
                {
                    MessageBox.Show($"ERROR: Camera name must be unique,{cam.name} already exists.");
                    return ($"ERROR: Camera name must be unique,{cam.name} already exists.");
                }
            }

            //check if name is empty
            if (cam.name == "")
            {
                MessageBox.Show($"ERROR: Camera name may not be empty.");
                return ($"ERROR: Camera name may not be empty.");
            }


            if (BlueIrisInfo.IsValid && !String.IsNullOrWhiteSpace(BlueIrisInfo.URL))
            {
                //http://10.0.1.99:81/admin?trigger&camera=BACKFOSCAM&user=AITools&pw=haha&memo=[summary]
                cam.trigger_urls_as_string = $"{BlueIrisInfo.URL}/admin?trigger&camera=[camera]&user=ENTERUSERNAMEHERE&pw=ENTERPASSWORDHERE&flagalert=1&memo=[summary]";
            }


            cam.triggering_objects = Global.Split(cam.triggering_objects_as_string, ",").ToArray();   //triggering_objects_as_string.Split(','); //split the row of triggering objects between every ','

            //Split by cr/lf or other common delimiters
            cam.trigger_urls = Global.Split(cam.trigger_urls_as_string, "\r\n|;,").ToArray();  //all trigger urls in an array
            cam.cancel_urls = Global.Split(cam.cancel_urls_as_string, "\r\n|;,").ToArray();  //all trigger urls in an array

            AppSettings.Settings.CameraList.Add(cam); //add created camera object to CameraList

            LoadCameras();

            return ($"SUCCESS: {cam.name} created.");
        }



        //remove camera
        private void RemoveCamera(string name)
        {
            Log($"Removing camera {name}...");
            if (list2.Items.Count > 0) //if list is empty, nothing can be deleted
            {
                if (AppSettings.Settings.CameraList.Exists(x => x.name.ToLower() == name.ToLower())) //check if camera with specified name exists in list
                {

                    //find index of specified camera in list
                    int index = -1;

                    //check for each camera in the cameralist if its name equals the name of the camera that is selected to be deleted
                    for (int i = 0; i < AppSettings.Settings.CameraList.Count; i++)
                    {
                        if (AppSettings.Settings.CameraList[i].name.ToLower().Equals(name.ToLower()))
                        {
                            index = i;

                        }
                    }

                    if (index != -1) //only delete camera if index is known (!= its default value -1)
                    {
                        //AppSettings.Settings.CameraList[index].Delete(); //delete settings file of specified camera

                        //move all cameras following the specified camera one position forward in the list
                        //the position of the specified camera is overridden with the following camera, the position of the following camera is overridden with its follower, and so on
                        for (int i = index; i < AppSettings.Settings.CameraList.Count - 1; i++)
                        {
                            AppSettings.Settings.CameraList[i] = AppSettings.Settings.CameraList[i + 1];
                        }

                        AppSettings.Settings.CameraList.Remove(AppSettings.Settings.CameraList[AppSettings.Settings.CameraList.Count - 1]); //lastly, remove camera from list

                        //remove list2 entry
                        var item = list2.FindItemWithText(name);
                        list2.Items[list2.Items.IndexOf(item)].Remove();

                        //remove camera from combobox on overview tab and from camera filter combobox in the History tab 
                        comboBox1.Items.Remove($"   {name}");
                        comboBox_filter_camera.Items.Remove($"   {name}");

                        //select first camera
                        if (list2.Items.Count > 0)
                        {
                            list2.Items[0].Selected = true;
                        }

                        //if list2 is empty, clear settings fields (to prevent that values of a deleted camera are shown)
                        if (list2.Items.Count == 0)
                        {
                            tbName.Text = "";
                            tbPrefix.Text = "";
                            cb_enabled.Checked = false;
                            CheckBox[] cbarray = new CheckBox[] { cb_airplane, cb_bear, cb_bicycle, cb_bird, cb_boat, cb_bus, cb_car, cb_cat, cb_cow, cb_dog, cb_horse, cb_motorcycle, cb_person, cb_sheep, cb_truck };
                            foreach (CheckBox c in cbarray)
                            {
                                c.Checked = false;
                            }
                            //tbTriggerUrl.Text = "";
                            //cb_telegram.Checked = false;
                            //disable camera settings if there are no cameras setup yet
                            tableLayoutPanel6.Enabled = false;
                        }
                    }
                    else
                    {
                        Log("ERROR: Can't find the selected camera, camera wasn't deleted.");
                    }


                }
            }
        }

        //display camera settings for selected camera
        private void DisplayCameraSettings()
        {
            if (list2.SelectedItems.Count > 0)
            {

                tableLayoutPanel6.Enabled = true;

                tbName.Text = list2.SelectedItems[0].Text; //load name textbox from name in list2

                //load remaining settings from Camera.cs object

                //all camera objects are stored in the list CameraList, so firstly the position (stored in the second column for each entry) is gathered
                Camera cam = AITOOL.GetCamera(tbName.Text);   //int i = AppSettings.Settings.CameraList.FindIndex(x => x.name.Trim().ToLower() == list2.SelectedItems[0].Text.Trim().ToLower());

                //load cameras stats

                string stats = $"Alerts: {cam.stats_alerts.ToString()} | Irrelevant Alerts: {cam.stats_irrelevant_alerts.ToString()} | False Alerts: {cam.stats_false_alerts.ToString()}";

                if (cam.maskManager.MaskingEnabled)
                {
                    stats += $" | Mask History Count: {cam.maskManager.LastPositionsHistory.Count()} | Current Dynamic Masks: {cam.maskManager.MaskedPositions.Count()}";
                }
                lbl_camstats.Text = stats;

                //load if ai detection is active for the camera
                if (cam.enabled == true)
                {
                    cb_enabled.Checked = true;
                }
                else
                {
                    cb_enabled.Checked = false;
                }
                tbPrefix.Text = cam.prefix; //load 'input file begins with'
                lbl_prefix.Text = tbPrefix.Text + ".××××××.jpg"; //prefix live preview

                cmbcaminput.Text = cam.input_path;
                cmbcaminput.Items.Clear();
                foreach (string pth in BlueIrisInfo.ClipPaths)
                {
                    cmbcaminput.Items.Add(pth);
                    //try to automatically pick the path that starts with AI if not already set
                    //if ((pth.ToLower().Contains(tbName.Text.ToLower()) || pth.ToLower().Contains(tbPrefix.Text.ToLower())) && string.IsNullOrWhiteSpace(cmbcaminput.Text))
                    //{
                    //    cmbcaminput.Text = pth;
                    //}
                }
                cb_monitorCamInputfolder.Checked = cam.input_path_includesubfolders;

                tb_threshold_lower.Text = cam.threshold_lower.ToString(); //load lower threshold value
                tb_threshold_upper.Text = cam.threshold_upper.ToString(); // load upper threshold value

                //load is masking enabled 
                cb_masking_enabled.Checked = cam.maskManager.MaskingEnabled;



                //load triggering objects
                //first create arrays with all checkboxes stored in
                CheckBox[] cbarray = new CheckBox[] { cb_airplane, cb_bear, cb_bicycle, cb_bird, cb_boat, cb_bus, cb_car, cb_cat, cb_cow, cb_dog, cb_horse, cb_motorcycle, cb_person, cb_sheep, cb_truck };
                //create array with strings of the triggering_objects related to the checkboxes in the same order
                string[] cbstringarray = new string[] { "Airplane", "Bear", "Bicycle", "Bird", "Boat", "Bus", "Car", "Cat", "Cow", "Dog", "Horse", "Motorcycle", "Person", "Sheep", "Truck" };

                //clear all checkmarks
                foreach (CheckBox cb in cbarray)
                {
                    cb.Checked = false;
                }

                //check for every triggering_object string if it is active in the settings file. If yes, check according checkbox
                for (int j = 0; j < cbarray.Length; j++)
                {
                    if (cam.triggering_objects_as_string.ToLower().Contains(cbstringarray[j].ToLower()))
                    {
                        cbarray[j].Checked = true;
                    }
                }
            }
        }



        // SPECIAL METHODS

        //input file begins with live preview
        private void tbPrefix_TextChanged(object sender, EventArgs e)
        {
            lbl_prefix.Text = tbPrefix.Text + ".××××××.jpg";
        }



        //event: camera list another item selected
        private void list2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayCameraSettings(); //display new item's settings
        }

        //event: camera add button
        private void btnCameraAdd_Click(object sender, EventArgs e)
        {

            using (var form = new InputForm("Camera Name:", "New Camera", cbitems: BlueIrisInfo.Cameras))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Camera cam = new Camera(form.text);

                    string camresult = AddCamera(cam);

                    // Old way...
                    //string name, string prefix, string trigger_urls_as_string, string triggering_objects_as_string, bool telegram_enabled, bool enabled, double cooldown_time, int threshold_lower, int threshold_upper,
                    //                                 string _input_path, bool _input_path_includesubfolders,
                    //                                 bool masking_enabled,
                    //                                 bool trigger_cancels

                    MessageBox.Show(camresult);
                }
            }
        }

        //event: save camera settings button
        private void btnCameraSave_Click_1(object sender, EventArgs e)
        {

            CameraSave(false);
        }

        private void CameraSave(bool SaveTo)
        {
            if (list2.Items.Count > 0)
            {
                //check if name is empty
                if (String.IsNullOrWhiteSpace(tbName.Text))
                {
                    DisplayCameraSettings(); //reset displayed settings
                    MessageBox.Show($"WARNING: Camera name may not be empty.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (list2.SelectedItems[0].Text.Trim().ToLower() != tbName.Text.Trim().ToLower())
                {
                    //camera renamed, make sure name doesnt exist
                    Camera CamCheck = AITOOL.GetCamera(tbName.Text, false);
                    if (CamCheck != null)
                    {
                        //Its a dupe
                        MessageBox.Show($"WARNING: Camera name must be unique, but new camera name '{tbName.Text}' already exists.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DisplayCameraSettings(); //reset displayed settings
                        return;
                    }
                    else
                    {
                        Log($"SUCCESS: Camera {list2.SelectedItems[0].Text} was updated to {tbName.Text}.");
                    }
                }


                Camera cam = AITOOL.GetCamera(list2.SelectedItems[0].Text, false);

                if (cam == null)
                {
                    //should not happen, but...
                    MessageBox.Show($"WARNING: Camera not found???  '{list2.SelectedItems[0].Text}'", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DisplayCameraSettings(); //reset displayed settings
                    return;
                }


                //1. GET SETTINGS INPUTTED
                //all checkboxes in one array

                //person,   bicycle,   car,   motorcycle,   airplane,
                //bus,   train,   truck,   boat,   traffic light,   fire hydrant,   stop_sign,
                //parking meter,   bench,   bird,   cat,   dog,   horse,   sheep,   cow,   elephant,
                //bear,   zebra, giraffe,   backpack,   umbrella,   handbag,   tie,   suitcase,
                //frisbee,   skis,   snowboard, sports ball,   kite,   baseball bat,   baseball glove,
                //skateboard,   surfboard,   tennis racket, bottle,   wine glass,   cup,   fork,
                //knife,   spoon,   bowl,   banana,   apple,   sandwich,   orange, broccoli,   carrot,
                //hot dog,   pizza,   donot,   cake,   chair,   couch,   potted plant,   bed, dining table,
                //toilet,   tv,   laptop,   mouse,   remote,   keyboard,   cell phone,   microwave,
                //oven,   toaster,   sink,   refrigerator,   book,   clock,   vase,   scissors,   teddy bear,
                //hair dryer, toothbrush.

                CheckBox[] cbarray = new CheckBox[] { cb_airplane, cb_bear, cb_bicycle, cb_bird, cb_boat, cb_bus, cb_car, cb_cat, cb_cow, cb_dog, cb_horse, cb_motorcycle, cb_person, cb_sheep, cb_truck };
                //create array with strings of the triggering_objects related to the checkboxes in the same order
                string[] cbstringarray = new string[] { "Airplane", "Bear", "Bicycle", "Bird", "Boat", "Bus", "Car", "Cat", "Cow", "Dog", "Horse", "Motorcycle", "Person", "Sheep", "Truck" };

                //go through all checkboxes and write all triggering_objects in one string
                cam.triggering_objects_as_string = "";
                for (int i = 0; i < cbarray.Length; i++)
                {
                    if (cbarray[i].Checked == true)
                    {
                        cam.triggering_objects_as_string += $"{cbstringarray[i].Trim()}, ";
                    }
                }

                //get lower and upper threshold values from textboxes
                cam.threshold_lower = Convert.ToInt32(tb_threshold_lower.Text.Trim());
                cam.threshold_upper = Convert.ToInt32(tb_threshold_upper.Text.Trim());

                cam.triggering_objects = Global.Split(cam.triggering_objects_as_string, ",").ToArray();   //triggering_objects_as_string.Split(','); //split the row of triggering objects between every ','

                cam.trigger_urls = Global.Split(cam.trigger_urls_as_string, "\r\n|;,").ToArray();  //all trigger urls in an array
                cam.cancel_urls = Global.Split(cam.cancel_urls_as_string, "\r\n|;,").ToArray();

                cam.name = tbName.Text.Trim();  //just in case we needed to rename it
                cam.prefix = tbPrefix.Text.Trim();
                cam.enabled = cb_enabled.Checked;
                cam.maskManager.MaskingEnabled = cb_masking_enabled.Checked;
                cam.input_path = cmbcaminput.Text.Trim();
                cam.input_path_includesubfolders = cb_monitorCamInputfolder.Checked;

                int ccnt = 0;

                if (SaveTo)
                {
                    using (Frm_ApplyCameraTo frm = new Frm_ApplyCameraTo())
                    {
                        foreach (Camera ccam in AppSettings.Settings.CameraList)
                        {
                            if (ccam.name != cam.name)
                                frm.checkedListBoxCameras.Items.Add(ccam.name, false);
                        }

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            for (int i = 0; i < frm.checkedListBoxCameras.Items.Count; i++)
                            {
                                if (frm.checkedListBoxCameras.GetItemChecked(i))
                                {
                                    Camera icam = AITOOL.GetCamera(frm.checkedListBoxCameras.Items[i].ToString(),false);
                                    if (icam != null)
                                    {
                                        ccnt++;
                                        
                                        Log($"Updating camera '{cam.name}' with settings from '{icam.name}'...");

                                        if (frm.cb_apply_confidence_limits.Checked)
                                        {
                                            icam.threshold_lower = cam.threshold_lower;
                                            icam.threshold_upper = cam.threshold_upper;
                                        }
                                        if (frm.cb_apply_objects.Checked)
                                        {
                                            icam.triggering_objects_as_string = cam.triggering_objects_as_string;
                                            icam.triggering_objects = Global.Split(icam.triggering_objects_as_string, ",").ToArray();   //triggering_objects_as_string.Split(','); //split the row of triggering objects between every ','
                                        }
                                        if (frm.cb_apply_actions.Checked)
                                        {
                                            icam.trigger_urls_as_string = cam.trigger_urls_as_string;
                                            icam.trigger_urls = cam.trigger_urls;
                                            icam.cancel_urls_as_string = cam.cancel_urls_as_string;
                                            icam.cancel_urls = cam.cancel_urls;
                                            icam.cooldown_time = cam.cooldown_time;
                                            icam.telegram_enabled = cam.telegram_enabled;
                                            icam.telegram_caption = cam.telegram_caption;
                                            icam.Action_image_copy_enabled = cam.Action_image_copy_enabled;
                                            icam.Action_network_folder = cam.Action_network_folder;
                                            icam.Action_network_folder_filename = cam.Action_network_folder_filename;
                                            icam.Action_RunProgram = cam.Action_RunProgram;
                                            icam.Action_RunProgramString = cam.Action_RunProgramString;
                                            icam.Action_RunProgramArgsString = cam.Action_RunProgramArgsString;
                                            icam.Action_PlaySounds = cam.Action_PlaySounds;
                                            icam.Action_Sounds = cam.Action_Sounds;
                                            icam.Action_mqtt_enabled = cam.Action_mqtt_enabled;
                                            icam.Action_mqtt_payload = cam.Action_mqtt_payload;
                                            icam.Action_mqtt_topic = cam.Action_mqtt_topic;
                                            icam.Action_mqtt_payload_cancel = cam.Action_mqtt_payload_cancel;
                                            icam.Action_mqtt_topic_cancel = cam.Action_mqtt_topic_cancel;
                                            icam.Action_image_merge_detections = cam.Action_image_merge_detections;
                                            icam.Action_image_merge_jpegquality = cam.Action_image_merge_jpegquality;
                                            icam.Action_queued = cam.Action_queued;
                                        }
                                        if (frm.cb_apply_mask_settings.Checked)
                                        {
                                            icam.maskManager.MaskingEnabled = cam.maskManager.MaskingEnabled;

                                            icam.maskManager.HistorySaveMins = cam.maskManager.HistorySaveMins;
                                            icam.maskManager.HistoryThresholdCount = cam.maskManager.HistoryThresholdCount;
                                            icam.maskManager.MaskRemoveThreshold = cam.maskManager.MaskRemoveThreshold;
                                            icam.maskManager.MaskRemoveMins = cam.maskManager.MaskRemoveMins;
                                            icam.maskManager.MaskSaveMins = cam.maskManager.MaskSaveMins;

                                            icam.maskManager.PercentMatch = cam.maskManager.PercentMatch;
                                            icam.maskManager.Objects = cam.maskManager.Objects;

                                            icam.maskManager.ScaleConfig.IsScaledObject = cam.maskManager.ScaleConfig.IsScaledObject;
                                            icam.maskManager.ScaleConfig.MediumObjectMatchPercent = cam.maskManager.ScaleConfig.MediumObjectMatchPercent;
                                            icam.maskManager.ScaleConfig.MediumObjectMaxPercent = cam.maskManager.ScaleConfig.MediumObjectMaxPercent;
                                            icam.maskManager.ScaleConfig.SmallObjectMatchPercent = cam.maskManager.ScaleConfig.SmallObjectMatchPercent;
                                            icam.maskManager.ScaleConfig.SmallObjectMaxPercent = cam.maskManager.ScaleConfig.SmallObjectMaxPercent;
                                        }

                                    }
                                }

                            }

                        }
                    }
                }
                else
                {
                    ccnt = 1;
                }

                LoadCameras();

                AppSettings.Save();

                UpdateWatchers(true);

                string saved = $"{ccnt} Camera(s) saved.";
                Log(saved);

                MessageBox.Show(saved, "", MessageBoxButtons.OK, MessageBoxIcon.Information);


                ////2. UPDATE SETTINGS
                //// save new camera settings, display result in MessageBox
                //string result = UpdateCamera(list2.SelectedItems[0].Text, tbName.Text, tbPrefix.Text, tbTriggerUrl.Text, triggering_objects_as_string, cb_telegram.Checked, cb_enabled.Checked, cooldown_time, threshold_lower, threshold_upper,
                //                             cmbcaminput.Text, cb_monitorCamInputfolder.Checked,
                //                             cb_masking_enabled.Checked,
                //                             cb_TriggerCancels.Checked); //, history_mins, mask_create_counter, mask_remove_counter, percent_variance);



                //1     2       3                             4                       5                 6        7              8                9
                //name, prefix, triggering_objects_as_string, trigger_urls_as_string, telegram_enabled, enabled, cooldown_time, threshold_lower, threshold_upper,
                //                                                               10           11  
                //                                                               _input_path, _input_path_includesubfolders,
                //                                                          12   masking_enabled,
                //                                                          13   trigger_cancel



            }
            DisplayCameraSettings();

        }
        //event: delete camera button
        private void btnCameraDel_Click(object sender, EventArgs e)
        {
            if (list2.Items.Count > 0)
            {
                using (var form = new InputForm($"Delete camera {list2.SelectedItems[0].Text} ?", "Delete Camera?", false))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        //Log("about to del cam");
                        RemoveCamera(list2.SelectedItems[0].Text);
                    }
                }
            }
        }

        //event: DELETE key pressed
        private void list2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (list2.Items.Count > 0)
                {
                    using (var form = new InputForm($"Delete camera {list2.SelectedItems[0].Text} ?", "Delete Camera?", false))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            RemoveCamera(list2.SelectedItems[0].Text);
                        }
                    }
                }
            }
        }

        //event: leaving empty lower confidence limit textbox
        private void tb_threshold_lower_Leave(object sender, EventArgs e)
        {
            if (tb_threshold_lower.Text == "")
            {
                tb_threshold_lower.Text = "0";
            }
        }

        //event: leaving empty upper confidence limit textbox
        private void tb_threshold_upper_Leave(object sender, EventArgs e)
        {
            if (tb_threshold_upper.Text == "")
            {
                tb_threshold_upper.Text = "100";
            }
        }



        //----------------------------------------------------------------------------------------------------------
        //SETTING TAB
        //----------------------------------------------------------------------------------------------------------


        //settings save button
        private void BtnSettingsSave_Click_1(object sender, EventArgs e)
        {
            Log($"Saving settings to {AppSettings.Settings.SettingsFileName}");
            //save inputted settings into App.settings
            AppSettings.Settings.input_path = cmbInput.Text;
            AppSettings.Settings.input_path_includesubfolders = cb_inputpathsubfolders.Checked;
            AppSettings.Settings.deepstack_url = tbDeepstackUrl.Text;
            AppSettings.Settings.deepstack_urls_are_queued = cb_DeepStackURLsQueued.Checked;
            AppSettings.Settings.telegram_chatids = Global.Split(tb_telegram_chatid.Text, "|;,", true, true);
            AppSettings.Settings.telegram_token = tb_telegram_token.Text;
            AppSettings.Settings.telegram_cooldown_minutes = Convert.ToDouble(tb_telegram_cooldown.Text);
            AppSettings.Settings.log_everything = cb_log.Checked;
            AppSettings.Settings.send_errors = cb_send_errors.Checked;
            AppSettings.Settings.startwithwindows = cbStartWithWindows.Checked;

            Global.Startup(AppSettings.Settings.startwithwindows);

            //let the image loop (running in another thread) know to recheck ai server url settings.
            AIURLSettingsChanged.WriteFullFence(true);

            if (AppSettings.Save())
            {
                Log("...Saved.");
            }
            else
            {
                Log("...Not saved.  No changes?");
            }
            //update variables
            //input_path = AppSettings.Settings.input_path;
            //deepstack_url = AppSettings.Settings.deepstack_url;
            //telegram_chatid = AppSettings.Settings.telegram_chatid;
            //telegram_chatids = telegram_chatid.Replace(" ", "").Split(','); //for multiple Telegram chats that receive alert images
            //telegram_token = AppSettings.Settings.telegram_token;
            //log_everything = AppSettings.Settings.log_everything;
            //send_errors = AppSettings.Settings.send_errors;

            //update fswatcher to watch new input folder
            UpdateWatchers(true);


            bool noneg = false;
            if (AppSettings.Settings.telegram_chatids.Count > 0)
            {
                foreach (string id in AppSettings.Settings.telegram_chatids)
                {
                    if (!string.IsNullOrWhiteSpace(id) && !id.TrimStart().StartsWith("-"))
                    {
                        noneg = true;
                    }
                }
            }

            if (noneg)
            {
                MessageBox.Show("Please note that the Telegram Chat ID may need to start with a negative sign. -1234567890", "Telegram Chat ID format", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //input path select dialog button
        private void btn_input_path_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                if (!string.IsNullOrEmpty(cmbInput.Text))
                {
                    dialog.InitialDirectory = cmbInput.Text;

                }
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    cmbInput.Text = dialog.FileName;
                }
            }
        }

        //open log button
        private void btn_open_log_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(AppSettings.Settings.LogFileName))
            {
                System.Diagnostics.Process.Start(AppSettings.Settings.LogFileName);
                lbl_errors.Text = "";
            }
            else
            {
                MessageBox.Show("log missing");
            }

        }

        //ask before closing AI Tool to prevent accidentally closing
        private void Shell_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosing.WriteFullFence(true);

            if (AppSettings.Settings.close_instantly <= 0) //if it's eigther enabled or not set  -1 = not set | 0 = ask for confirmation | 1 = don't ask
            {
                using (var form = new InputForm($"Stop and close AI Tool?", "AI Tool", false))
                {
                    var result = form.ShowDialog();
                    if (AppSettings.Settings.close_instantly == -1)
                    {
                        //if it's the first time, ask if the confirmation dialog should ever appear again
                        using (var form1 = new InputForm($"Confirm closing AI Tool every time?", "AI Tool", false, "NO, Never!", "YES"))
                        {
                            var result1 = form1.ShowDialog();
                            if (result1 == DialogResult.Cancel)
                            {
                                AppSettings.Settings.close_instantly = 0;
                            }
                            else
                            {
                                AppSettings.Settings.close_instantly = 1;
                            }
                        }
                    }

                    e.Cancel = (result == DialogResult.Cancel);
                }
            }


            Global_GUI.SaveWindowState(this);

            AppSettings.Save();  //save settings in any case

            if (!AppSettings.AlreadyRunning)
                Global.SaveSetting("LastShutdownState", "graceful shutdown");



        }

        private void Shell_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);
        }
        private void SaveDeepStackTab()
        {

            if (DeepStackServerControl == null)
                DeepStackServerControl = new DeepStack(AppSettings.Settings.deepstack_adminkey, AppSettings.Settings.deepstack_apikey, AppSettings.Settings.deepstack_mode, AppSettings.Settings.deepstack_sceneapienabled, AppSettings.Settings.deepstack_faceapienabled, AppSettings.Settings.deepstack_detectionapienabled, AppSettings.Settings.deepstack_port);

            DeepStackServerControl.GetDeepStackRun();

            if (RB_Medium.Checked)
                AppSettings.Settings.deepstack_mode = "Medium";
            if (RB_Low.Checked)
                AppSettings.Settings.deepstack_mode = "Low";
            if (RB_High.Checked)
                AppSettings.Settings.deepstack_mode = "High";

            AppSettings.Settings.deepstack_detectionapienabled = Chk_DetectionAPI.Checked;
            AppSettings.Settings.deepstack_faceapienabled = Chk_FaceAPI.Checked;
            AppSettings.Settings.deepstack_sceneapienabled = Chk_SceneAPI.Checked;
            AppSettings.Settings.deepstack_autostart = Chk_AutoStart.Checked;
            AppSettings.Settings.deepstack_debug = Chk_DSDebug.Checked;
            AppSettings.Settings.deepstack_highpriority = chk_HighPriority.Checked;
            AppSettings.Settings.deepstack_adminkey = Txt_AdminKey.Text.Trim();
            AppSettings.Settings.deepstack_apikey = Txt_APIKey.Text.Trim();
            AppSettings.Settings.deepstack_installfolder = Txt_DeepStackInstallFolder.Text.Trim();
            AppSettings.Settings.deepstack_port = Txt_Port.Text.Trim();


            AppSettings.Save();


            if (DeepStackServerControl.IsInstalled)
            {
                if (DeepStackServerControl.IsStarted)
                {


                    if (DeepStackServerControl.IsActivated)
                    {
                        MethodInvoker LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*RUNNING*"; };
                        Invoke(LabelUpdate);

                        Btn_Start.Enabled = false;
                        Btn_Stop.Enabled = true;
                    }
                    else
                    {
                        MethodInvoker LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*NOT ACTIVATED, RUNNING*"; };
                        Invoke(LabelUpdate);

                        Btn_Start.Enabled = false;
                        Btn_Stop.Enabled = true;
                    }
                }
                else
                {
                    MethodInvoker LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*NOT RUNNING*"; };
                    Invoke(LabelUpdate);

                    Btn_Start.Enabled = true;
                    Btn_Stop.Enabled = false;
                }
            }
            else
            {
                Btn_Start.Enabled = false;
                Btn_Stop.Enabled = false;
                MethodInvoker LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*NOT INSTALLED*"; };
                Invoke(LabelUpdate);


            }

            DeepStackServerControl.Update(AppSettings.Settings.deepstack_adminkey, AppSettings.Settings.deepstack_apikey, AppSettings.Settings.deepstack_mode, AppSettings.Settings.deepstack_sceneapienabled, AppSettings.Settings.deepstack_faceapienabled, AppSettings.Settings.deepstack_detectionapienabled, AppSettings.Settings.deepstack_port);

        }

        private async void LoadDeepStackTab(bool StartIfNeeded)
        {

            try
            {
                if (DeepStackServerControl == null)
                    DeepStackServerControl = new DeepStack(AppSettings.Settings.deepstack_adminkey, AppSettings.Settings.deepstack_apikey, AppSettings.Settings.deepstack_mode, AppSettings.Settings.deepstack_sceneapienabled, AppSettings.Settings.deepstack_faceapienabled, AppSettings.Settings.deepstack_detectionapienabled, AppSettings.Settings.deepstack_port);


                //first update the port in the deepstack_url if found
                //string prt = Global.GetWordBetween(AppSettings.Settings.deepstack_url, ":", " |/");
                //if (!string.IsNullOrEmpty(prt) && (Convert.ToInt32(prt) > 0))
                //{
                //    DeepStackServerControl.Port = prt;
                //}

                //This will OVERRIDE the port if the deepstack processes found running already have a different port, mode, etc:
                DeepStackServerControl.GetDeepStackRun();

                if (DeepStackServerControl.Mode.ToLower() == "medium")
                    RB_Medium.Checked = true;
                if (DeepStackServerControl.Mode.ToLower() == "low")
                    RB_Low.Checked = true;
                if (DeepStackServerControl.Mode.ToLower() == "high")
                    RB_High.Checked = true;

                Chk_DetectionAPI.Checked = DeepStackServerControl.DetectionAPIEnabled;
                Chk_FaceAPI.Checked = DeepStackServerControl.FaceAPIEnabled;
                Chk_SceneAPI.Checked = DeepStackServerControl.SceneAPIEnabled;

                //have seen a few cases nothing is checked but it is required
                if (!Chk_DetectionAPI.Checked && !Chk_FaceAPI.Checked && !Chk_SceneAPI.Checked)
                {
                    Chk_DetectionAPI.Checked = true;
                    DeepStackServerControl.DetectionAPIEnabled = true;
                }

                Chk_AutoStart.Checked = AppSettings.Settings.deepstack_autostart;
                Chk_DSDebug.Checked = AppSettings.Settings.deepstack_debug;
                chk_HighPriority.Checked = AppSettings.Settings.deepstack_highpriority;
                Txt_AdminKey.Text = DeepStackServerControl.AdminKey;
                Txt_APIKey.Text = DeepStackServerControl.APIKey;
                Txt_DeepStackInstallFolder.Text = DeepStackServerControl.DeepStackFolder;
                Txt_Port.Text = DeepStackServerControl.Port;

                //if (prt != Txt_Port.Text)
                //{
                //    //server:port/maybe/more/path
                //    string serv = Global.GetWordBetween(AppSettings.Settings.deepstack_url, "", ":");
                //    if (!string.IsNullOrEmpty(serv))
                //    {
                //        tbDeepstackUrl.Text = serv + ":" + Txt_Port.Text;
                //        //AppSettings.Settings.deepstack_url = serv + ":" + Txt_Port.Text;
                //        //AppSettings.Settings.deepstack_url = tbDeepstackUrl.Text;
                //        //AppSettings.Save();
                //    }
                //}

                if (DeepStackServerControl.IsInstalled)
                {
                    if (DeepStackServerControl.IsStarted && !DeepStackServerControl.HasError)
                    {
                        if (DeepStackServerControl.IsActivated && (DeepStackServerControl.VisionDetectionRunning || DeepStackServerControl.DetectionAPIEnabled))
                        {

                            MethodInvoker LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*RUNNING*"; };
                            Invoke(LabelUpdate);

                            Btn_Start.Enabled = false;
                            Btn_Stop.Enabled = true;
                        }
                        else if (!DeepStackServerControl.IsActivated)
                        {
                            MethodInvoker LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*NOT ACTIVATED, RUNNING*"; };
                            Invoke(LabelUpdate);

                            Btn_Start.Enabled = false;
                            Btn_Stop.Enabled = true;
                        }
                        else if (!DeepStackServerControl.VisionDetectionRunning || DeepStackServerControl.DetectionAPIEnabled)
                        {
                            MethodInvoker LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*DETECTION API NOT RUNNING*"; };
                            Invoke(LabelUpdate);

                            Btn_Start.Enabled = false;
                            Btn_Stop.Enabled = true;
                        }

                    }
                    else if (DeepStackServerControl.HasError)
                    {
                        MethodInvoker LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*ERROR*"; };
                        Invoke(LabelUpdate);

                        Btn_Start.Enabled = false;
                        Btn_Stop.Enabled = true;
                    }
                    else
                    {
                        MethodInvoker LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*NOT RUNNING*"; };
                        Invoke(LabelUpdate);

                        Btn_Start.Enabled = true;
                        Btn_Stop.Enabled = false;
                        if (Chk_AutoStart.Checked && StartIfNeeded)
                        {
                            if (await DeepStackServerControl.StartAsync())
                            {
                                if (DeepStackServerControl.IsStarted && !DeepStackServerControl.HasError)
                                {
                                    LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*RUNNING*"; };
                                    Invoke(LabelUpdate);
                                    Btn_Start.Enabled = false;
                                    Btn_Stop.Enabled = true;
                                }
                                else if (DeepStackServerControl.HasError)
                                {
                                    LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*ERROR*"; };
                                    Invoke(LabelUpdate);

                                    Btn_Start.Enabled = false;
                                    Btn_Stop.Enabled = true;
                                }

                            }
                            else
                            {
                                LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*ERROR*"; };
                                Invoke(LabelUpdate);

                                Btn_Start.Enabled = false;
                                Btn_Stop.Enabled = true;
                            }
                        }
                    }
                }
                else
                {
                    Btn_Start.Enabled = false;
                    Btn_Stop.Enabled = false;
                    MethodInvoker LabelUpdate = delegate { Lbl_BlueStackRunning.Text = "*NOT INSTALLED*"; };
                    Invoke(LabelUpdate);


                }

            }
            catch (Exception ex)
            {

                Log(Global.ExMsg(ex));
            }
        }

        private async void Btn_Start_Click(object sender, EventArgs e)
        {
            Lbl_BlueStackRunning.Text = "STARTING...";
            Btn_Start.Enabled = false;
            Btn_Stop.Enabled = false;
            SaveDeepStackTab();
            await DeepStackServerControl.StartAsync();
            LoadDeepStackTab(true);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            SaveDeepStackTab();
        }

        private async void Btn_Stop_Click(object sender, EventArgs e)
        {
            Lbl_BlueStackRunning.Text = "STOPPING...";
            Btn_Start.Enabled = false;
            Btn_Stop.Enabled = false;
            await DeepStackServerControl.StopAsync();
            LoadDeepStackTab(false);
        }

        private void btnStopscroll_Click(object sender, EventArgs e)
        {
            RTFLogger.AutoScroll.WriteFullFence(false);
        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(AppSettings.Settings.LogFileName))
            {
                System.Diagnostics.Process.Start(AppSettings.Settings.LogFileName);
                lbl_errors.Text = "";
            }
            else
            {
                MessageBox.Show("log missing");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowErrors();
        }

        private void Chk_AutoScroll_CheckedChanged(object sender, EventArgs e)
        {
            RTFLogger.AutoScroll.WriteFullFence(Chk_AutoScroll.Checked);
            AppSettings.Settings.Autoscroll_log = Chk_AutoScroll.Checked;
        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                if (!string.IsNullOrEmpty(cmbcaminput.Text))
                {
                    dialog.InitialDirectory = cmbcaminput.Text;

                }
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    cmbcaminput.Text = dialog.FileName;
                }
            }
        }


        private void BtnDynamicMaskingSettings_Click(object sender, EventArgs e)
        {
            using (Frm_DynamicMasking frm = new Frm_DynamicMasking())
            {
                Camera cam = AITOOL.GetCamera(list2.SelectedItems[0].Text);
                frm.cam = cam;
                frm.Text = "Dynamic Masking Settings - " + cam.name;

                //Merge ClassObject's code
                frm.num_history_mins.Value = cam.maskManager.HistorySaveMins;//load minutes to retain history objects that have yet to become masks
                frm.num_mask_create.Value = cam.maskManager.HistoryThresholdCount; // load mask create counter
                frm.num_mask_remove.Value = cam.maskManager.MaskRemoveMins; //load mask remove counter
                frm.num_percent_var.Value = (decimal)cam.maskManager.PercentMatch;
                frm.numMaskThreshold.Value = cam.maskManager.MaskRemoveThreshold;

                frm.cb_enabled.Checked = this.cb_masking_enabled.Checked;

                frm.tb_objects.Text = cam.maskManager.Objects;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ////get masking values from textboxes
                    Int32.TryParse(frm.num_history_mins.Text, out int history_mins);
                    Int32.TryParse(frm.num_mask_create.Text, out int mask_create_counter);
                    Int32.TryParse(frm.num_mask_remove.Text, out int mask_remove_mins);
                    Int32.TryParse(frm.numMaskThreshold.Text, out int maskRemoveThreshold);
                    Int32.TryParse(frm.num_percent_var.Text, out int variance);

                    cam.maskManager.HistorySaveMins = history_mins;
                    cam.maskManager.HistoryThresholdCount = mask_create_counter;
                    cam.maskManager.MaskRemoveMins = mask_remove_mins;
                    cam.maskManager.MaskRemoveThreshold = maskRemoveThreshold;
                    cam.maskManager.PercentMatch = variance;

                    this.cb_masking_enabled.Checked = frm.cb_enabled.Checked;
                    cam.maskManager.MaskingEnabled = cb_masking_enabled.Checked;
                    cam.maskManager.Objects = frm.tb_objects.Text.Trim();

                    AppSettings.Save();
                }
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            ShowMaskDetailsDialog(list2.SelectedItems[0].Text);
        }

        private void ShowMaskDetailsDialog(string cameraname)
        {
            using (Frm_DynamicMaskDetails frm = new Frm_DynamicMaskDetails())
            {

                Camera CurCam = GetCamera(cameraname);
                frm.cam = CurCam;

                frm.ShowDialog();

                cb_masking_enabled.Checked = CurCam.maskManager.MaskingEnabled;

            }

        }


        private void QueueLblTmr_Tick(object sender, EventArgs e)
        {

            UpdateQueueLabel();
        }

        private void UpdateQueueLabel()
        {
            MethodInvoker LabelUpdate = delegate { lblQueue.Text = $"Images in queue: {ImageProcessQueue.Count}, Max: {qsizecalc.Max} ({qcalc.Max}ms), Average: {qsizecalc.Average.ToString("#####")} ({qcalc.Average.ToString("#####")}ms queue wait time)"; };
            Invoke(LabelUpdate);


        }

        private void btnCustomMask_Click(object sender, EventArgs e)
        {
            ShowEditImageMaskDialog(list2.SelectedItems[0].Text);
        }

        private void ShowEditImageMaskDialog(string cameraname)
        {
            using (Frm_CustomMasking frm = new Frm_CustomMasking())
            {
                Camera cam = AITOOL.GetCamera(cameraname);
                frm.cam = cam;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    cam.mask_brush_size = frm.brushSize;
                }
            }

        }

        //private void btnActions_Click(object sender, EventArgs e)
        //{
        //    using (Frm_LegacyActions frm = new Frm_LegacyActions())
        //    {


        //        Camera cam = AITOOL.GetCamera(list2.SelectedItems[0].Text);
        //        frm.cam = cam;

        //        frm.tbTriggerUrl.Text = string.Join("\r\n", Global.Split(cam.trigger_urls_as_string, "\r\n|;,"));
        //        frm.tbCancelUrl.Text = string.Join("\r\n", Global.Split(cam.cancel_urls_as_string, "\r\n|;,"));
        //        frm.tb_cooldown.Text = cam.cooldown_time.ToString(); //load cooldown time
        //        //load telegram image sending on/off option
        //        frm.cb_telegram.Checked = cam.telegram_enabled;

        //        frm.cb_copyAlertImages.Checked = cam.Action_image_copy_enabled;
        //        frm.tb_network_folder_filename.Text = cam.Action_network_folder_filename;
        //        frm.tb_network_folder.Text = cam.Action_network_folder;
        //        frm.cb_RunProgram.Checked = cam.Action_RunProgram;

        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {
        //            cam.trigger_urls_as_string = string.Join(",", Global.Split(frm.tbTriggerUrl.Text.Trim(), "\r\n|;,"));
        //            cam.cancel_urls_as_string = string.Join(",", Global.Split(frm.tbCancelUrl.Text.Trim(), "\r\n|;,"));
        //            cam.cancel_urls = Global.Split(cam.cancel_urls_as_string, "\r\n|;,").ToArray();
        //            cam.cooldown_time = Convert.ToDouble(frm.tb_cooldown.Text.Trim());
        //            cam.telegram_enabled = frm.cb_telegram.Checked;
        //            cam.Action_image_copy_enabled = frm.cb_copyAlertImages.Checked;
        //            cam.Action_network_folder = frm.tb_network_folder.Text.Trim();
        //            cam.Action_network_folder_filename = frm.tb_network_folder_filename.Text;
        //            cam.Action_RunProgram = frm.cb_RunProgram.Checked;
        //            cam.Action_RunProgramString = frm.tb_RunExternalProgram.Text;

        //            AppSettings.Save();

        //        }
        //    }
        //}

        private void btnActions_Click_1(object sender, EventArgs e)
        {
            using (Frm_LegacyActions frm = new Frm_LegacyActions())
            {


                Camera cam = AITOOL.GetCamera(list2.SelectedItems[0].Text);

                frm.cam = cam;

                frm.tbTriggerUrl.Text = string.Join("\r\n", Global.Split(cam.trigger_urls_as_string, "\r\n|;,"));
                frm.tbCancelUrl.Text = string.Join("\r\n", Global.Split(cam.cancel_urls_as_string, "\r\n|;,"));
                frm.tb_cooldown.Text = cam.cooldown_time.ToString(); //load cooldown time
                //load telegram image sending on/off option
                frm.cb_telegram.Checked = cam.telegram_enabled;
                frm.tb_telegram_caption.Text = cam.telegram_caption;


                frm.cb_copyAlertImages.Checked = cam.Action_image_copy_enabled;
                frm.tb_network_folder_filename.Text = cam.Action_network_folder_filename;
                frm.tb_network_folder.Text = cam.Action_network_folder;

                frm.cb_RunProgram.Checked = cam.Action_RunProgram;
                frm.tb_RunExternalProgram.Text = cam.Action_RunProgramString;
                frm.tb_RunExternalProgramArgs.Text = cam.Action_RunProgramArgsString;

                frm.cb_PlaySound.Checked = cam.Action_PlaySounds;
                frm.tb_Sounds.Text = cam.Action_Sounds;

                frm.cb_MQTT_enabled.Checked = cam.Action_mqtt_enabled;
                frm.tb_MQTT_Payload.Text = cam.Action_mqtt_payload;
                frm.tb_MQTT_Topic.Text = cam.Action_mqtt_topic;
                frm.tb_MQTT_Payload_cancel.Text = cam.Action_mqtt_payload_cancel;
                frm.tb_MQTT_Topic_Cancel.Text = cam.Action_mqtt_topic_cancel;

                frm.cb_queue_actions.Checked = cam.Action_queued;

                frm.cb_mergeannotations.Checked = cam.Action_image_merge_detections;

                frm.tb_jpeg_merge_quality.Text = cam.Action_image_merge_jpegquality.ToString();

                if (frm.cb_mergeannotations.Checked)
                    frm.tb_jpeg_merge_quality.Enabled = true;
                else
                    frm.tb_jpeg_merge_quality.Enabled = false;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    cam.trigger_urls_as_string = string.Join(",", Global.Split(frm.tbTriggerUrl.Text.Trim(), "\r\n|;,"));
                    cam.trigger_urls = Global.Split(cam.trigger_urls_as_string, "\r\n|;,").ToArray();
                    cam.cancel_urls_as_string = string.Join(",", Global.Split(frm.tbCancelUrl.Text.Trim(), "\r\n|;,"));
                    cam.cancel_urls = Global.Split(cam.cancel_urls_as_string, "\r\n|;,").ToArray();

                    cam.cooldown_time = Convert.ToDouble(frm.tb_cooldown.Text.Trim());
                    cam.telegram_enabled = frm.cb_telegram.Checked;
                    cam.telegram_caption = frm.tb_telegram_caption.Text.Trim();

                    cam.Action_image_copy_enabled = frm.cb_copyAlertImages.Checked;
                    cam.Action_network_folder = frm.tb_network_folder.Text.Trim();
                    cam.Action_network_folder_filename = frm.tb_network_folder_filename.Text;

                    cam.Action_RunProgram = frm.cb_RunProgram.Checked;
                    cam.Action_RunProgramString = frm.tb_RunExternalProgram.Text.Trim();
                    cam.Action_RunProgramArgsString = frm.tb_RunExternalProgramArgs.Text.Trim();

                    cam.Action_PlaySounds = frm.cb_PlaySound.Checked;
                    cam.Action_Sounds = frm.tb_Sounds.Text.Trim();

                    cam.Action_mqtt_enabled = frm.cb_MQTT_enabled.Checked;
                    cam.Action_mqtt_payload = frm.tb_MQTT_Payload.Text.Trim();
                    cam.Action_mqtt_topic = frm.tb_MQTT_Topic.Text.Trim();
                    cam.Action_mqtt_payload_cancel = frm.tb_MQTT_Payload_cancel.Text.Trim();
                    cam.Action_mqtt_topic_cancel = frm.tb_MQTT_Topic_Cancel.Text.Trim();

                    cam.Action_image_merge_detections = frm.cb_mergeannotations.Checked;
                    
                    cam.Action_image_merge_jpegquality = Convert.ToInt64(frm.tb_jpeg_merge_quality.Text);

                    cam.Action_queued = frm.cb_queue_actions.Checked;
                    
                    AppSettings.Save();

                }
            }
        }

        private void tbDeepstackUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabLog_Click(object sender, EventArgs e)
        {

        }

        private void folv_history_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (folv_history.SelectedObjects != null && folv_history.SelectedObjects.Count > 0)
                {
                    History hist = (History)folv_history.SelectedObjects[0];

                    if (!String.IsNullOrEmpty(hist.Filename) && hist.Filename.Contains("\\") && File.Exists(hist.Filename))
                    {
                        using (var img = new Bitmap(hist.Filename))
                        {
                            pictureBox1.BackgroundImage = new Bitmap(img); //load actual image as background, so that an overlay can be added as the image
                        }
                        showHideMask();
                        lbl_objects.Text = hist.Detections;
                    }
                    else
                    {
                        Log("Removing missing file from database: " + hist.Filename);
                        HistoryDB.DeleteHistoryQueue(hist.Filename);
                        lbl_objects.Text = "Image not found";
                        pictureBox1.BackgroundImage = null;
                    }

                    if (!string.IsNullOrEmpty(hist.PredictionsJSON))
                    {
                        toolStripButtonDetails.Enabled = true;
                    }
                    else
                    {
                        toolStripButtonDetails.Enabled = false;
                    }

                    toolStripButtonEditImageMask.Enabled = true;
                    toolStripButtonMaskDetails.Enabled = true;

                }
                else
                {
                    lbl_objects.Text = "No selection";
                    pictureBox1.BackgroundImage = null;
                    toolStripButtonDetails.Enabled = false;
                    toolStripButtonEditImageMask.Enabled = false;
                    toolStripButtonMaskDetails.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                Log($"Error: {Global.ExMsg(ex)}");

            }



        }

        private void folv_history_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            FormatHistoryRow(sender, e);
        }

        private async void FormatHistoryRow(object Sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            try
            {
                History hist = (History)e.Model;

                // If SPI IsNot Nothing Then
                if (hist.Success && !hist.HasError)
                    e.Item.ForeColor = Color.Green;
                else if (hist.HasError)
                {
                    e.Item.ForeColor = Color.Black;
                    e.Item.BackColor = Color.Red;
                }
                else if (hist.WasSkipped)
                    e.Item.ForeColor = Color.Red;
                else if (hist.WasMasked)
                {
                    e.Item.ForeColor = Color.Black;
                    e.Item.BackColor = Color.LightGray;
                }
                else if (!hist.Success && hist.Detections.ToLower().Contains("false alert"))
                    e.Item.ForeColor = Color.Gray;
                else
                    e.Item.ForeColor = Color.Black;
            }


            catch (Exception ex)
            {
            }
            // Log("Error: " & ExMsg(ex))
            finally
            {
            }
        }

        private void btn_resetstats_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text == "All Cameras")
            {
                foreach (Camera cam in AppSettings.Settings.CameraList)
                {
                    cam.stats_alerts = 0;
                    cam.stats_irrelevant_alerts = 0;
                    cam.stats_false_alerts = 0;
                    cam.stats_skipped_images = 0;
                    cam.stats_skipped_images_session = 0;
                }
            }
            else
            {

                Camera cam = AITOOL.GetCamera(comboBox1.Text);  //int i = AppSettings.Settings.CameraList.FindIndex(x => x.name.ToLower().Trim() == comboBox1.Text.ToLower().Trim());
                if (cam != null)
                {
                    cam.stats_alerts = 0;
                    cam.stats_irrelevant_alerts = 0;
                    cam.stats_false_alerts = 0;
                    cam.stats_skipped_images = 0;
                    cam.stats_skipped_images_session = 0;
                }
            }

            UpdatePieChart(); UpdateTimeline(); UpdateConfidenceChart();

            UpdateQueueLabel();

            AppSettings.Save();

        }

        private async void cb_filter_skipped_CheckedChanged(object sender, EventArgs e)
        {
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private async void HistoryUpdateListTimer_Tick(object sender, EventArgs e)
        {
            if (IsClosing.ReadFullFence())
                return;

            if (!AppSettings.AlreadyRunning)
                Global.SaveSetting("LastShutdownState", $"checkpoint: HistoryUpdateTimer: {DateTime.Now}");

            await UpdateHistoryAddedRemoved();

        }

        private void folv_history_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripStatusErrors_Click(object sender, EventArgs e)
        {
            ShowErrors();
        }

        private async void cb_follow_CheckedChanged(object sender, EventArgs e)
        {
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private async void comboBox_filter_camera_SelectionChangeCommitted(object sender, EventArgs e)
        {
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {

                UpdatePieChart(); UpdateTimeline(); UpdateConfidenceChart();
            }
        }

        private void Shell_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            //Controls get really messed up when DPI changes when app is open.   Just trying to figure out the right way to handle....
            Log($"[System DPI Changed from {e.DeviceDpiOld} to {e.DeviceDpiNew}]");

            //we need to figure out how to force a full resize of all components - something is preventing that from happening
            //automatically upon DPI change.   A suspicion is the custom DBLayoutPanel, but its a clusterfuck to try to 
            //revert back to TableLayoutPanel for everything.

        }

        private async void cb_filter_success_Click(object sender, EventArgs e)
        {
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private async void cb_filter_nosuccess_Click(object sender, EventArgs e)
        {
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private async void cb_filter_person_Click(object sender, EventArgs e)
        {
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private async void cb_filter_animal_Click(object sender, EventArgs e)
        {
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private async void cb_filter_vehicle_Click(object sender, EventArgs e)
        {
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private async void cb_filter_skipped_Click(object sender, EventArgs e)
        {
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private async void cb_filter_masked_Click(object sender, EventArgs e)
        {
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private async void comboBox_filter_camera_DropDownClosed(object sender, EventArgs e)
        {
            await LoadHistoryAsync(true, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private void cb_showMask_Click(object sender, EventArgs e)
        {
            AppSettings.Settings.HistoryShowMask = cb_showMask.Checked;
            AppSettings.Save();
            showHideMask();
        }

        private void cb_showObjects_CheckedChanged(object sender, EventArgs e)
        {
            if (folv_history.SelectedObjects != null && folv_history.SelectedObjects.Count > 0)
            {
                pictureBox1.Refresh();
            }
        }

        private void automaticallyRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (automaticallyRefreshToolStripMenuItem.Checked)
            {
                HistoryUpdateListTimer.Enabled = true;
                HistoryUpdateListTimer.Start();
            }
            else
            {
                HistoryUpdateListTimer.Enabled = false;
                HistoryUpdateListTimer.Stop();
            }

            AppSettings.Settings.HistoryAutoRefresh = automaticallyRefreshToolStripMenuItem.Checked;
            AppSettings.Save();

        }

        private void cb_showObjects_Click(object sender, EventArgs e)
        {
            AppSettings.Settings.HistoryShowObjects = cb_showObjects.Checked;
            AppSettings.Save();
            pictureBox1.Refresh();
        }

        private void cb_follow_Click(object sender, EventArgs e)
        {
            AppSettings.Settings.HistoryFollow = cb_follow.Checked;
            AppSettings.Save();
        }

        private void toolStripButtonDetails_Click(object sender, EventArgs e)
        {
            ViewPredictionDetails();
        }

        private void ViewPredictionDetails()
        {
            try
            {
                List<ClsPrediction> allpredictions = new List<ClsPrediction>();

                foreach (History hist in folv_history.SelectedObjects)
                {
                    List<ClsPrediction> predictions = Global.SetJSONString<List<ClsPrediction>>(hist.PredictionsJSON);

                    if (predictions != null && predictions.Count > 0)
                    {
                        allpredictions.AddRange(predictions);
                    }
                    else
                    {
                        Log($"debug: No predictions for image {hist.Filename}: Json='{hist.PredictionsJSON}'");
                    }

                }

                Frm_ObjectDetail frm = new Frm_ObjectDetail();
                frm.PredictionObjectDetail = allpredictions;
                frm.Show();

            }
            catch (Exception ex)
            {

                Log("Error: " + Global.ExMsg(ex));
            }
        }



        private void testDetectionAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (folv_history.SelectedObjects != null && folv_history.SelectedObjects.Count > 0)
            {
                Global.Log("----------------------- TESTING TRIGGERS ----------------------------");

                foreach (History hist in folv_history.SelectedObjects)
                {
                    if (!string.IsNullOrEmpty(hist.Filename) && File.Exists(hist.Filename))
                    {
                        //test by copying the file as a new file into the watched folder'
                        string folder = Path.GetDirectoryName(hist.Filename);
                        string filename = Path.GetFileNameWithoutExtension(hist.Filename);
                        string ext = Path.GetExtension(hist.Filename);
                        string testfile = Path.Combine(folder, $"{filename}_AITOOLTEST_{DateTime.Now.TimeOfDay.TotalSeconds}{ext}");
                        File.Copy(hist.Filename, testfile, true);
                        string str = "Created test image file based on last detected object for the camera: " + testfile;
                        Global.Log(str);
                    }
                    else
                    {
                        Global.Log("Error: File does not exist for testing: " + hist.Filename);

                    }


                }

                Global.Log("---------------------- DONE TESTING TRIGGERS -------------------------");

            }

        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewPredictionDetails();
        }

        private async void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await LoadHistoryAsync(false, AppSettings.Settings.HistoryFollow).ConfigureAwait(false);
        }

        private void folv_history_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ViewPredictionDetails();
        }

        private void toolStripButtonMaskDetails_Click(object sender, EventArgs e)
        {

            if (folv_history.SelectedObjects != null && folv_history.SelectedObjects.Count > 0)
            {
                History hist = (History)folv_history.SelectedObjects[0];
                ShowMaskDetailsDialog(hist.Camera);

            }

        }

        private void dynamicMaskDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folv_history.SelectedObjects != null && folv_history.SelectedObjects.Count > 0)
            {
                History hist = (History)folv_history.SelectedObjects[0];
                ShowMaskDetailsDialog(hist.Camera);

            }
        }

        private void toolStripButtonEditImageMask_Click(object sender, EventArgs e)
        {
            if (folv_history.SelectedObjects != null && folv_history.SelectedObjects.Count > 0)
            {
                History hist = (History)folv_history.SelectedObjects[0];
                ShowEditImageMaskDialog(hist.Camera);

            }
        }

        private void btn_enabletelegram_Click(object sender, EventArgs e)
        {
            foreach (Camera cam in AppSettings.Settings.CameraList)
            {
                if (!cam.telegram_enabled)
                {
                    cam.telegram_enabled = true;
                    Log($"Enabled Telegram on camera '{cam.name}'.");
                }
            }
            AppSettings.Save();
        }

        private void btn_disabletelegram_Click(object sender, EventArgs e)
        {
            foreach (Camera cam in AppSettings.Settings.CameraList)
            {
                if (cam.telegram_enabled)
                {
                    cam.telegram_enabled = false;
                    Log($"Disabled Telegram on camera '{cam.name}'.");
                }
            }
            AppSettings.Save();
        }

        private void storeFalseAlertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppSettings.Settings.HistoryStoreFalseAlerts = storeFalseAlertsToolStripMenuItem.Checked;
            AppSettings.Save();
        }

        private void storeMaskedAlertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppSettings.Settings.HistoryStoreMaskedAlerts = storeMaskedAlertsToolStripMenuItem.Checked;
            AppSettings.Save();

        }

        private void showOnlyRelevantObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppSettings.Settings.HistoryOnlyDisplayRelevantObjects = showOnlyRelevantObjectsToolStripMenuItem.Checked;
            AppSettings.Save();
            pictureBox1.Refresh();

        }

        private void btnSaveTo_Click(object sender, EventArgs e)
        {
            CameraSave(true);
        }
    }


    //enhanced TableLayoutPanel loads faster
    public partial class DBLayoutPanel:TableLayoutPanel
    {
        public DBLayoutPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.UserPaint, true);
        }

        public DBLayoutPanel(IContainer container)
        {
            container.Add(this);
            SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.UserPaint, true);
        }
    }
}


