namespace AITool
{
    partial class Shell
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shell));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 30D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 30D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 30D);
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabOverview = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel14 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel15 = new AITool.DBLayoutPanel(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_version = new System.Windows.Forms.Label();
            this.lbl_errors = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.lblQueue = new System.Windows.Forms.Label();
            this.tabStats = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel16 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel23 = new AITool.DBLayoutPanel(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.chart_confidence = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timeline = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel17 = new AITool.DBLayoutPanel(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_resetstats = new System.Windows.Forms.Button();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.comboBox_filter_camera = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButtonFilters = new System.Windows.Forms.ToolStripDropDownButton();
            this.cb_filter_success = new System.Windows.Forms.ToolStripMenuItem();
            this.cb_filter_nosuccess = new System.Windows.Forms.ToolStripMenuItem();
            this.cb_filter_person = new System.Windows.Forms.ToolStripMenuItem();
            this.cb_filter_animal = new System.Windows.Forms.ToolStripMenuItem();
            this.cb_filter_vehicle = new System.Windows.Forms.ToolStripMenuItem();
            this.cb_filter_skipped = new System.Windows.Forms.ToolStripMenuItem();
            this.cb_filter_masked = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.cb_showMask = new System.Windows.Forms.ToolStripMenuItem();
            this.cb_showObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnlyRelevantObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cb_follow = new System.Windows.Forms.ToolStripMenuItem();
            this.automaticallyRefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storeFalseAlertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storeMaskedAlertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restrictThresholdAtSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonDetails = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMaskDetails = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditImageMask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonEditURL = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.folv_history = new BrightIdeasSoftware.FastObjectListView();
            this.contextMenuStripHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testDetectionAgainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dynamicMaskDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locateInLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manuallyAddImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jumpToImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_objects = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabCameras = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FOLV_Cameras = new BrightIdeasSoftware.FastObjectListView();
            this.tableLayoutPanel6 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel11 = new AITool.DBLayoutPanel(this.components);
            this.btnCameraSave = new System.Windows.Forms.Button();
            this.btnCameraAdd = new System.Windows.Forms.Button();
            this.btnCameraDel = new System.Windows.Forms.Button();
            this.btnSaveTo = new System.Windows.Forms.Button();
            this.lbl_camstats = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new AITool.DBLayoutPanel(this.components);
            this.label26 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tableLayoutPanel12 = new AITool.DBLayoutPanel(this.components);
            this.lbl_prefix = new System.Windows.Forms.Label();
            this.tbPrefix = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel13 = new AITool.DBLayoutPanel(this.components);
            this.cb_enabled = new System.Windows.Forms.CheckBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblRelevantObjects = new System.Windows.Forms.Label();
            this.tableLayoutPanel26 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbcaminput = new System.Windows.Forms.ComboBox();
            this.cb_monitorCamInputfolder = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.tableLayoutPanel27 = new System.Windows.Forms.TableLayoutPanel();
            this.cb_masking_enabled = new System.Windows.Forms.CheckBox();
            this.BtnDynamicMaskingSettings = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.btnCustomMask = new System.Windows.Forms.Button();
            this.lblDrawMask = new System.Windows.Forms.Label();
            this.btnActions = new System.Windows.Forms.Button();
            this.tableLayoutPanel8 = new AITool.DBLayoutPanel(this.components);
            this.cb_person = new System.Windows.Forms.CheckBox();
            this.cb_bicycle = new System.Windows.Forms.CheckBox();
            this.cb_motorcycle = new System.Windows.Forms.CheckBox();
            this.cb_bear = new System.Windows.Forms.CheckBox();
            this.cb_cow = new System.Windows.Forms.CheckBox();
            this.cb_sheep = new System.Windows.Forms.CheckBox();
            this.cb_horse = new System.Windows.Forms.CheckBox();
            this.cb_bird = new System.Windows.Forms.CheckBox();
            this.cb_dog = new System.Windows.Forms.CheckBox();
            this.cb_cat = new System.Windows.Forms.CheckBox();
            this.cb_airplane = new System.Windows.Forms.CheckBox();
            this.cb_boat = new System.Windows.Forms.CheckBox();
            this.cb_bus = new System.Windows.Forms.CheckBox();
            this.cb_truck = new System.Windows.Forms.CheckBox();
            this.cb_car = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbAdditionalRelevantObjects = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.dbLayoutPanel6 = new AITool.DBLayoutPanel(this.components);
            this.tbBiCamName = new System.Windows.Forms.TextBox();
            this.dbLayoutPanel7 = new AITool.DBLayoutPanel(this.components);
            this.tb_camera_telegram_chatid = new System.Windows.Forms.TextBox();
            this.tbCustomMaskFile = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.BtnPredictionSize = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel5 = new AITool.DBLayoutPanel(this.components);
            this.lbl_input = new System.Windows.Forms.Label();
            this.lbl_telegram_token = new System.Windows.Forms.Label();
            this.tableLayoutPanel18 = new AITool.DBLayoutPanel(this.components);
            this.btn_input_path = new System.Windows.Forms.Button();
            this.cmbInput = new System.Windows.Forms.ComboBox();
            this.cb_inputpathsubfolders = new System.Windows.Forms.CheckBox();
            this.lbl_deepstackurl = new System.Windows.Forms.Label();
            this.dbLayoutPanel1 = new AITool.DBLayoutPanel(this.components);
            this.cb_DeepStackURLsQueued = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dbLayoutPanel2 = new AITool.DBLayoutPanel(this.components);
            this.tb_telegram_cooldown = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_telegram_chatid = new System.Windows.Forms.TextBox();
            this.lbl_telegram_chatid = new System.Windows.Forms.Label();
            this.tb_telegram_token = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dbLayoutPanel3 = new AITool.DBLayoutPanel(this.components);
            this.cb_send_telegram_errors = new System.Windows.Forms.CheckBox();
            this.btn_enabletelegram = new System.Windows.Forms.Button();
            this.btn_disabletelegram = new System.Windows.Forms.Button();
            this.cb_send_pushover_errors = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dbLayoutPanel4 = new AITool.DBLayoutPanel(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.dbLayoutPanel5 = new AITool.DBLayoutPanel(this.components);
            this.tb_BlueIrisServer = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.dbLayoutPanel8 = new AITool.DBLayoutPanel(this.components);
            this.tb_Pushover_Cooldown = new System.Windows.Forms.TextBox();
            this.tb_Pushover_APIKey = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.tb_Pushover_UserKey = new System.Windows.Forms.TextBox();
            this.dbLayoutPanel9 = new AITool.DBLayoutPanel(this.components);
            this.cbStartWithWindows = new System.Windows.Forms.CheckBox();
            this.cbMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dbLayoutPanel10 = new AITool.DBLayoutPanel(this.components);
            this.BtnSettingsSave = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tabDeepStack = new System.Windows.Forms.TabPage();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txt_DeepstackNoMoreOftenThanMins = new System.Windows.Forms.TextBox();
            this.txt_DeepstackRestartFailCount = new System.Windows.Forms.TextBox();
            this.Chk_AutoReStart = new System.Windows.Forms.CheckBox();
            this.Btn_ViewLog = new System.Windows.Forms.Button();
            this.Btn_DeepstackReset = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.tb_DeepStackURLs = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tb_DeepstackCommandLine = new System.Windows.Forms.TextBox();
            this.lbl_DeepstackType = new System.Windows.Forms.Label();
            this.lbl_Deepstackversion = new System.Windows.Forms.Label();
            this.lbl_deepstackname = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.chk_stopbeforestart = new System.Windows.Forms.CheckBox();
            this.chk_HighPriority = new System.Windows.Forms.CheckBox();
            this.Chk_DSDebug = new System.Windows.Forms.CheckBox();
            this.Lbl_BlueStackRunning = new System.Windows.Forms.Label();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Chk_DetectionAPI = new System.Windows.Forms.CheckBox();
            this.Chk_FaceAPI = new System.Windows.Forms.CheckBox();
            this.Chk_SceneAPI = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RB_High = new System.Windows.Forms.RadioButton();
            this.RB_Medium = new System.Windows.Forms.RadioButton();
            this.RB_Low = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Txt_DeepStackInstallFolder = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.Txt_Port = new System.Windows.Forms.TextBox();
            this.Chk_AutoStart = new System.Windows.Forms.CheckBox();
            this.Btn_Start = new System.Windows.Forms.Button();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.groupBoxCustomModel = new System.Windows.Forms.GroupBox();
            this.Chk_CustomModelAPI = new System.Windows.Forms.CheckBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.Txt_CustomModelPort = new System.Windows.Forms.TextBox();
            this.Txt_CustomModelName = new System.Windows.Forms.TextBox();
            this.Txt_CustomModelPath = new System.Windows.Forms.TextBox();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripComboBoxSearch = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnu_Filter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Highlight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButtonSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.Chk_AutoScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRecentErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLogLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_log_filter_off = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_log_filter_fatal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_log_filter_error = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_log_filter_warn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_log_filter_info = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_log_filter_debug = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_log_filter_trace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonReload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPauseLog = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.chk_filterErrors = new System.Windows.Forms.ToolStripButton();
            this.chk_filterErrorsAll = new System.Windows.Forms.ToolStripButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.folv_log = new BrightIdeasSoftware.FastObjectListView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.HistoryImageList = new System.Windows.Forms.ImageList(this.components);
            this.HistoryUpdateListTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelHistoryItems = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusErrors = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.LogUpdateListTimer = new System.Windows.Forms.Timer(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabOverview.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabStats.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tableLayoutPanel23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_confidence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeline)).BeginInit();
            this.tableLayoutPanel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabHistory.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.folv_history)).BeginInit();
            this.contextMenuStripHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabCameras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Cameras)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel26.SuspendLayout();
            this.tableLayoutPanel27.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.dbLayoutPanel6.SuspendLayout();
            this.dbLayoutPanel7.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel18.SuspendLayout();
            this.dbLayoutPanel1.SuspendLayout();
            this.dbLayoutPanel2.SuspendLayout();
            this.dbLayoutPanel3.SuspendLayout();
            this.dbLayoutPanel4.SuspendLayout();
            this.dbLayoutPanel5.SuspendLayout();
            this.dbLayoutPanel8.SuspendLayout();
            this.dbLayoutPanel9.SuspendLayout();
            this.dbLayoutPanel10.SuspendLayout();
            this.tabDeepStack.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxCustomModel.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.folv_log)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "Running in the background";
            this.notifyIcon.BalloonTipTitle = "AI Tool";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "AI Tool";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabOverview);
            this.tabControl1.Controls.Add(this.tabStats);
            this.tabControl1.Controls.Add(this.tabHistory);
            this.tabControl1.Controls.Add(this.tabCameras);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Controls.Add(this.tabDeepStack);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Location = new System.Drawing.Point(0, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1028, 484);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // tabOverview
            // 
            this.tabOverview.Controls.Add(this.tableLayoutPanel14);
            this.tabOverview.Location = new System.Drawing.Point(4, 22);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Size = new System.Drawing.Size(1020, 458);
            this.tabOverview.TabIndex = 4;
            this.tabOverview.Text = "Overview";
            this.tabOverview.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel14.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel14.ColumnCount = 1;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel14.Controls.Add(this.tableLayoutPanel15, 0, 0);
            this.tableLayoutPanel14.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(1023, 455);
            this.tableLayoutPanel14.TabIndex = 3;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel15.ColumnCount = 1;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Controls.Add(this.pictureBox2, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel15.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel15.Controls.Add(this.lbl_version, 0, 5);
            this.tableLayoutPanel15.Controls.Add(this.lbl_errors, 0, 3);
            this.tableLayoutPanel15.Controls.Add(this.lbl_info, 0, 5);
            this.tableLayoutPanel15.Controls.Add(this.lblQueue, 0, 4);
            this.tableLayoutPanel15.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 6;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.14285F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0.9523811F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.80951F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.761905F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.761905F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(1015, 447);
            this.tableLayoutPanel15.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = global::AITool.Properties.Resources.logo;
            this.pictureBox2.Location = new System.Drawing.Point(3, 66);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1009, 131);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(3, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1009, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Running";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(63, 200);
            this.label3.Margin = new System.Windows.Forms.Padding(63, 0, 63, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(889, 2);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // lbl_version
            // 
            this.lbl_version.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_version.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbl_version.Location = new System.Drawing.Point(3, 403);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(1009, 20);
            this.lbl_version.TabIndex = 6;
            this.lbl_version.Text = "Version 1.67 preview 7  (VorlonCD MOD)";
            this.lbl_version.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lbl_errors
            // 
            this.lbl_errors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_errors.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_errors.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_errors.Location = new System.Drawing.Point(3, 325);
            this.lbl_errors.Name = "lbl_errors";
            this.lbl_errors.Size = new System.Drawing.Size(1009, 58);
            this.lbl_errors.TabIndex = 7;
            this.lbl_errors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_errors.Visible = false;
            this.lbl_errors.Click += new System.EventHandler(this.lbl_errors_Click);
            // 
            // lbl_info
            // 
            this.lbl_info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_info.AutoSize = true;
            this.lbl_info.Location = new System.Drawing.Point(3, 423);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(1009, 24);
            this.lbl_info.TabIndex = 8;
            this.lbl_info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQueue
            // 
            this.lblQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQueue.AutoSize = true;
            this.lblQueue.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblQueue.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblQueue.Location = new System.Drawing.Point(3, 383);
            this.lblQueue.Name = "lblQueue";
            this.lblQueue.Size = new System.Drawing.Size(1009, 20);
            this.lblQueue.TabIndex = 9;
            this.lblQueue.Text = "Images in Queue: 0";
            this.lblQueue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.tableLayoutPanel16);
            this.tabStats.Location = new System.Drawing.Point(4, 22);
            this.tabStats.Name = "tabStats";
            this.tabStats.Size = new System.Drawing.Size(1020, 458);
            this.tabStats.TabIndex = 5;
            this.tabStats.Text = "Stats";
            this.tabStats.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel16.ColumnCount = 2;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel16.Controls.Add(this.tableLayoutPanel23, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.tableLayoutPanel17, 0, 0);
            this.tableLayoutPanel16.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 1;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(1020, 454);
            this.tableLayoutPanel16.TabIndex = 0;
            // 
            // tableLayoutPanel23
            // 
            this.tableLayoutPanel23.ColumnCount = 1;
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel23.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel23.Controls.Add(this.chart_confidence, 0, 2);
            this.tableLayoutPanel23.Controls.Add(this.timeline, 0, 1);
            this.tableLayoutPanel23.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel23.Location = new System.Drawing.Point(309, 3);
            this.tableLayoutPanel23.Name = "tableLayoutPanel23";
            this.tableLayoutPanel23.RowCount = 3;
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel23.Size = new System.Drawing.Size(708, 448);
            this.tableLayoutPanel23.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 227);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(702, 29);
            this.label8.TabIndex = 9;
            this.label8.Text = "Frequencies of alert result confidences";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chart_confidence
            // 
            this.chart_confidence.BackColor = System.Drawing.Color.Transparent;
            this.chart_confidence.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.Interval = 10D;
            chartArea1.AxisX.MajorGrid.Interval = 6D;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.MajorTickMark.Interval = 1D;
            chartArea1.AxisX.Maximum = 100D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.Title = "Alert confidence";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.Title = "Frequency";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chart_confidence.ChartAreas.Add(chartArea1);
            this.chart_confidence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_confidence.Location = new System.Drawing.Point(3, 262);
            this.chart_confidence.Name = "chart_confidence";
            series1.BorderWidth = 4;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.Orange;
            series1.Name = "no alert";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.Green;
            series2.Legend = "Legend1";
            series2.Name = "alert";
            this.chart_confidence.Series.Add(series1);
            this.chart_confidence.Series.Add(series2);
            this.chart_confidence.Size = new System.Drawing.Size(702, 183);
            this.chart_confidence.TabIndex = 8;
            // 
            // timeline
            // 
            this.timeline.BackColor = System.Drawing.Color.Transparent;
            this.timeline.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.Interval = 3D;
            chartArea2.AxisX.MajorGrid.Interval = 6D;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisX.MajorTickMark.Interval = 1D;
            chartArea2.AxisX.Maximum = 24D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.Title = "Number";
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartArea1";
            this.timeline.ChartAreas.Add(chartArea2);
            this.timeline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeline.Location = new System.Drawing.Point(3, 38);
            this.timeline.Name = "timeline";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
            series3.Color = System.Drawing.Color.Silver;
            series3.Legend = "Legend1";
            series3.Name = "all";
            series4.BorderWidth = 3;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Color = System.Drawing.Color.OrangeRed;
            series4.Legend = "Legend1";
            series4.Name = "falses";
            series5.BorderWidth = 3;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Color = System.Drawing.Color.Orange;
            series5.Legend = "Legend1";
            series5.Name = "irrelevant";
            series6.BorderWidth = 4;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Color = System.Drawing.Color.Green;
            series6.Legend = "Legend1";
            series6.Name = "relevant";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series7.Color = System.Drawing.Color.Purple;
            series7.Name = "skipped";
            this.timeline.Series.Add(series3);
            this.timeline.Series.Add(series4);
            this.timeline.Series.Add(series5);
            this.timeline.Series.Add(series6);
            this.timeline.Series.Add(series7);
            this.timeline.Size = new System.Drawing.Size(702, 183);
            this.timeline.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 3);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(702, 29);
            this.label7.TabIndex = 0;
            this.label7.Text = "Timeline";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.ColumnCount = 1;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Controls.Add(this.chart1, 0, 2);
            this.tableLayoutPanel17.Controls.Add(this.comboBox1, 0, 0);
            this.tableLayoutPanel17.Controls.Add(this.btn_resetstats, 0, 1);
            this.tableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel17.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 3;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(300, 448);
            this.tableLayoutPanel17.TabIndex = 3;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.DimGray;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.Area3DStyle.Enable3D = true;
            chartArea3.Area3DStyle.IsRightAngleAxes = false;
            chartArea3.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;
            chartArea3.Area3DStyle.Perspective = 10;
            chartArea3.Area3DStyle.WallWidth = 6;
            chartArea3.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.StaggeredLabels) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep30) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea3.AxisY.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)(((((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.StaggeredLabels) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep30) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep90) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea3.BackColor = System.Drawing.Color.Transparent;
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
            legend1.Name = "Legend1";
            legend1.Title = "Legend";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 59);
            this.chart1.Name = "chart1";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series8.IsValueShownAsLabel = true;
            series8.Legend = "Legend1";
            series8.Name = "s1";
            dataPoint1.IsVisibleInLegend = true;
            series8.Points.Add(dataPoint1);
            series8.Points.Add(dataPoint2);
            series8.Points.Add(dataPoint3);
            this.chart1.Series.Add(series8);
            this.chart1.Size = new System.Drawing.Size(294, 386);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            title1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Input Rates";
            this.chart1.Titles.Add(title1);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(294, 25);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // btn_resetstats
            // 
            this.btn_resetstats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_resetstats.Location = new System.Drawing.Point(2, 33);
            this.btn_resetstats.Margin = new System.Windows.Forms.Padding(2);
            this.btn_resetstats.Name = "btn_resetstats";
            this.btn_resetstats.Size = new System.Drawing.Size(296, 21);
            this.btn_resetstats.TabIndex = 4;
            this.btn_resetstats.Text = "Reset Stats";
            this.btn_resetstats.UseVisualStyleBackColor = true;
            this.btn_resetstats.Click += new System.EventHandler(this.btn_resetstats_Click);
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.toolStrip1);
            this.tabHistory.Controls.Add(this.splitContainer2);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(1020, 458);
            this.tabHistory.TabIndex = 0;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboBox_filter_camera,
            this.toolStripSeparator1,
            this.toolStripDropDownButtonFilters,
            this.toolStripDropDownButtonOptions,
            this.toolStripButtonDetails,
            this.toolStripButtonMaskDetails,
            this.toolStripButtonEditImageMask,
            this.toolStripSeparator9,
            this.toolStripButtonEditURL});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1014, 38);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // comboBox_filter_camera
            // 
            this.comboBox_filter_camera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_filter_camera.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.comboBox_filter_camera.Name = "comboBox_filter_camera";
            this.comboBox_filter_camera.Size = new System.Drawing.Size(173, 38);
            this.comboBox_filter_camera.DropDownClosed += new System.EventHandler(this.comboBox_filter_camera_DropDownClosed);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripDropDownButtonFilters
            // 
            this.toolStripDropDownButtonFilters.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cb_filter_success,
            this.cb_filter_nosuccess,
            this.cb_filter_person,
            this.cb_filter_animal,
            this.cb_filter_vehicle,
            this.cb_filter_skipped,
            this.cb_filter_masked});
            this.toolStripDropDownButtonFilters.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonFilters.Image")));
            this.toolStripDropDownButtonFilters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonFilters.Name = "toolStripDropDownButtonFilters";
            this.toolStripDropDownButtonFilters.Size = new System.Drawing.Size(120, 35);
            this.toolStripDropDownButtonFilters.Text = "History Filters";
            // 
            // cb_filter_success
            // 
            this.cb_filter_success.CheckOnClick = true;
            this.cb_filter_success.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cb_filter_success.Name = "cb_filter_success";
            this.cb_filter_success.Size = new System.Drawing.Size(188, 22);
            this.cb_filter_success.Text = "Only Relevant";
            this.cb_filter_success.Click += new System.EventHandler(this.cb_filter_success_Click);
            // 
            // cb_filter_nosuccess
            // 
            this.cb_filter_nosuccess.CheckOnClick = true;
            this.cb_filter_nosuccess.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cb_filter_nosuccess.Name = "cb_filter_nosuccess";
            this.cb_filter_nosuccess.Size = new System.Drawing.Size(188, 22);
            this.cb_filter_nosuccess.Text = "Only False / Irrelevant";
            this.cb_filter_nosuccess.Click += new System.EventHandler(this.cb_filter_nosuccess_Click);
            // 
            // cb_filter_person
            // 
            this.cb_filter_person.CheckOnClick = true;
            this.cb_filter_person.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cb_filter_person.Name = "cb_filter_person";
            this.cb_filter_person.Size = new System.Drawing.Size(188, 22);
            this.cb_filter_person.Text = "Only People";
            this.cb_filter_person.Click += new System.EventHandler(this.cb_filter_person_Click);
            // 
            // cb_filter_animal
            // 
            this.cb_filter_animal.CheckOnClick = true;
            this.cb_filter_animal.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cb_filter_animal.Name = "cb_filter_animal";
            this.cb_filter_animal.Size = new System.Drawing.Size(188, 22);
            this.cb_filter_animal.Text = "Only Animals";
            this.cb_filter_animal.Click += new System.EventHandler(this.cb_filter_animal_Click);
            // 
            // cb_filter_vehicle
            // 
            this.cb_filter_vehicle.CheckOnClick = true;
            this.cb_filter_vehicle.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cb_filter_vehicle.Name = "cb_filter_vehicle";
            this.cb_filter_vehicle.Size = new System.Drawing.Size(188, 22);
            this.cb_filter_vehicle.Text = "Only Vehicles";
            this.cb_filter_vehicle.Click += new System.EventHandler(this.cb_filter_vehicle_Click);
            // 
            // cb_filter_skipped
            // 
            this.cb_filter_skipped.CheckOnClick = true;
            this.cb_filter_skipped.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cb_filter_skipped.Name = "cb_filter_skipped";
            this.cb_filter_skipped.Size = new System.Drawing.Size(188, 22);
            this.cb_filter_skipped.Text = "Only Skipped";
            this.cb_filter_skipped.Click += new System.EventHandler(this.cb_filter_skipped_Click);
            // 
            // cb_filter_masked
            // 
            this.cb_filter_masked.CheckOnClick = true;
            this.cb_filter_masked.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cb_filter_masked.Name = "cb_filter_masked";
            this.cb_filter_masked.Size = new System.Drawing.Size(188, 22);
            this.cb_filter_masked.Text = "Only Masked";
            this.cb_filter_masked.Click += new System.EventHandler(this.cb_filter_masked_Click);
            // 
            // toolStripDropDownButtonOptions
            // 
            this.toolStripDropDownButtonOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cb_showMask,
            this.cb_showObjects,
            this.showOnlyRelevantObjectsToolStripMenuItem,
            this.cb_follow,
            this.automaticallyRefreshToolStripMenuItem,
            this.storeFalseAlertsToolStripMenuItem,
            this.storeMaskedAlertsToolStripMenuItem,
            this.restrictThresholdAtSourceToolStripMenuItem});
            this.toolStripDropDownButtonOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonOptions.Image")));
            this.toolStripDropDownButtonOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonOptions.Name = "toolStripDropDownButtonOptions";
            this.toolStripDropDownButtonOptions.Size = new System.Drawing.Size(131, 35);
            this.toolStripDropDownButtonOptions.Text = "History Settings";
            // 
            // cb_showMask
            // 
            this.cb_showMask.CheckOnClick = true;
            this.cb_showMask.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cb_showMask.Name = "cb_showMask";
            this.cb_showMask.Size = new System.Drawing.Size(222, 22);
            this.cb_showMask.Text = "Show Mask";
            this.cb_showMask.Click += new System.EventHandler(this.cb_showMask_Click);
            // 
            // cb_showObjects
            // 
            this.cb_showObjects.Checked = true;
            this.cb_showObjects.CheckOnClick = true;
            this.cb_showObjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_showObjects.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cb_showObjects.Name = "cb_showObjects";
            this.cb_showObjects.Size = new System.Drawing.Size(222, 22);
            this.cb_showObjects.Text = "Show Objects";
            this.cb_showObjects.CheckedChanged += new System.EventHandler(this.cb_showObjects_CheckedChanged);
            this.cb_showObjects.Click += new System.EventHandler(this.cb_showObjects_Click);
            // 
            // showOnlyRelevantObjectsToolStripMenuItem
            // 
            this.showOnlyRelevantObjectsToolStripMenuItem.CheckOnClick = true;
            this.showOnlyRelevantObjectsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showOnlyRelevantObjectsToolStripMenuItem.Name = "showOnlyRelevantObjectsToolStripMenuItem";
            this.showOnlyRelevantObjectsToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.showOnlyRelevantObjectsToolStripMenuItem.Text = "Show Only Relevant Objects";
            this.showOnlyRelevantObjectsToolStripMenuItem.ToolTipText = resources.GetString("showOnlyRelevantObjectsToolStripMenuItem.ToolTipText");
            this.showOnlyRelevantObjectsToolStripMenuItem.Click += new System.EventHandler(this.showOnlyRelevantObjectsToolStripMenuItem_Click);
            // 
            // cb_follow
            // 
            this.cb_follow.CheckOnClick = true;
            this.cb_follow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cb_follow.Name = "cb_follow";
            this.cb_follow.Size = new System.Drawing.Size(222, 22);
            this.cb_follow.Text = "Follow History List";
            this.cb_follow.ToolTipText = "Automatically select the latest history item in the list for every update";
            this.cb_follow.Click += new System.EventHandler(this.cb_follow_Click);
            // 
            // automaticallyRefreshToolStripMenuItem
            // 
            this.automaticallyRefreshToolStripMenuItem.Checked = true;
            this.automaticallyRefreshToolStripMenuItem.CheckOnClick = true;
            this.automaticallyRefreshToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.automaticallyRefreshToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.automaticallyRefreshToolStripMenuItem.Name = "automaticallyRefreshToolStripMenuItem";
            this.automaticallyRefreshToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.automaticallyRefreshToolStripMenuItem.Text = "Automatically Refresh";
            this.automaticallyRefreshToolStripMenuItem.Click += new System.EventHandler(this.automaticallyRefreshToolStripMenuItem_Click);
            // 
            // storeFalseAlertsToolStripMenuItem
            // 
            this.storeFalseAlertsToolStripMenuItem.CheckOnClick = true;
            this.storeFalseAlertsToolStripMenuItem.DoubleClickEnabled = true;
            this.storeFalseAlertsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.storeFalseAlertsToolStripMenuItem.Name = "storeFalseAlertsToolStripMenuItem";
            this.storeFalseAlertsToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.storeFalseAlertsToolStripMenuItem.Text = "Store False Alerts";
            this.storeFalseAlertsToolStripMenuItem.ToolTipText = resources.GetString("storeFalseAlertsToolStripMenuItem.ToolTipText");
            this.storeFalseAlertsToolStripMenuItem.Click += new System.EventHandler(this.storeFalseAlertsToolStripMenuItem_Click);
            // 
            // storeMaskedAlertsToolStripMenuItem
            // 
            this.storeMaskedAlertsToolStripMenuItem.CheckOnClick = true;
            this.storeMaskedAlertsToolStripMenuItem.DoubleClickEnabled = true;
            this.storeMaskedAlertsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.storeMaskedAlertsToolStripMenuItem.Name = "storeMaskedAlertsToolStripMenuItem";
            this.storeMaskedAlertsToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.storeMaskedAlertsToolStripMenuItem.Text = "Store Masked Alerts";
            this.storeMaskedAlertsToolStripMenuItem.ToolTipText = "If disabled the database will be smaller, leave enabled for better troubleshootin" +
    "g";
            this.storeMaskedAlertsToolStripMenuItem.Click += new System.EventHandler(this.storeMaskedAlertsToolStripMenuItem_Click);
            // 
            // restrictThresholdAtSourceToolStripMenuItem
            // 
            this.restrictThresholdAtSourceToolStripMenuItem.CheckOnClick = true;
            this.restrictThresholdAtSourceToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.restrictThresholdAtSourceToolStripMenuItem.Name = "restrictThresholdAtSourceToolStripMenuItem";
            this.restrictThresholdAtSourceToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.restrictThresholdAtSourceToolStripMenuItem.Text = "Restrict Threshold at Source";
            this.restrictThresholdAtSourceToolStripMenuItem.ToolTipText = resources.GetString("restrictThresholdAtSourceToolStripMenuItem.ToolTipText");
            this.restrictThresholdAtSourceToolStripMenuItem.Click += new System.EventHandler(this.restrictThresholdAtSourceToolStripMenuItem_Click);
            // 
            // toolStripButtonDetails
            // 
            this.toolStripButtonDetails.Enabled = false;
            this.toolStripButtonDetails.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDetails.Image")));
            this.toolStripButtonDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDetails.Name = "toolStripButtonDetails";
            this.toolStripButtonDetails.Size = new System.Drawing.Size(131, 35);
            this.toolStripButtonDetails.Text = "Prediction Details";
            this.toolStripButtonDetails.Click += new System.EventHandler(this.toolStripButtonDetails_Click);
            // 
            // toolStripButtonMaskDetails
            // 
            this.toolStripButtonMaskDetails.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMaskDetails.Image")));
            this.toolStripButtonMaskDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMaskDetails.Name = "toolStripButtonMaskDetails";
            this.toolStripButtonMaskDetails.Size = new System.Drawing.Size(155, 35);
            this.toolStripButtonMaskDetails.Text = "Dynamic Mask Details";
            this.toolStripButtonMaskDetails.Click += new System.EventHandler(this.toolStripButtonMaskDetails_Click);
            // 
            // toolStripButtonEditImageMask
            // 
            this.toolStripButtonEditImageMask.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditImageMask.Image")));
            this.toolStripButtonEditImageMask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditImageMask.Name = "toolStripButtonEditImageMask";
            this.toolStripButtonEditImageMask.Size = new System.Drawing.Size(126, 35);
            this.toolStripButtonEditImageMask.Text = "Edit Image Mask";
            this.toolStripButtonEditImageMask.Click += new System.EventHandler(this.toolStripButtonEditImageMask_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripButtonEditURL
            // 
            this.toolStripButtonEditURL.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditURL.Image")));
            this.toolStripButtonEditURL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditURL.Name = "toolStripButtonEditURL";
            this.toolStripButtonEditURL.Size = new System.Drawing.Size(108, 35);
            this.toolStripButtonEditURL.Text = "Edit AI Server";
            this.toolStripButtonEditURL.Click += new System.EventHandler(this.toolStripButtonEditURL_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(2, 43);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox8);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lbl_objects);
            this.splitContainer2.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer2.Panel2MinSize = 300;
            this.splitContainer2.Size = new System.Drawing.Size(1018, 418);
            this.splitContainer2.SplitterDistance = 284;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 4;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.folv_history);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox8.Size = new System.Drawing.Size(280, 414);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "History";
            // 
            // folv_history
            // 
            this.folv_history.ContextMenuStrip = this.contextMenuStripHistory;
            this.folv_history.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folv_history.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.folv_history.HideSelection = false;
            this.folv_history.Location = new System.Drawing.Point(2, 15);
            this.folv_history.Margin = new System.Windows.Forms.Padding(2);
            this.folv_history.Name = "folv_history";
            this.folv_history.ShowGroups = false;
            this.folv_history.Size = new System.Drawing.Size(276, 397);
            this.folv_history.TabIndex = 10;
            this.folv_history.UseCellFormatEvents = true;
            this.folv_history.UseCompatibleStateImageBehavior = false;
            this.folv_history.UseFiltering = true;
            this.folv_history.View = System.Windows.Forms.View.Details;
            this.folv_history.VirtualMode = true;
            this.folv_history.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.folv_history_FormatRow);
            this.folv_history.SelectionChanged += new System.EventHandler(this.folv_history_SelectionChanged);
            this.folv_history.SelectedIndexChanged += new System.EventHandler(this.folv_history_SelectedIndexChanged);
            this.folv_history.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.folv_history_MouseDoubleClick);
            // 
            // contextMenuStripHistory
            // 
            this.contextMenuStripHistory.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testDetectionAgainToolStripMenuItem,
            this.detailsToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.dynamicMaskDetailsToolStripMenuItem,
            this.locateInLogToolStripMenuItem,
            this.manuallyAddImagesToolStripMenuItem,
            this.viewImageToolStripMenuItem,
            this.jumpToImageToolStripMenuItem});
            this.contextMenuStripHistory.Name = "contextMenuStripHistory";
            this.contextMenuStripHistory.Size = new System.Drawing.Size(191, 180);
            // 
            // testDetectionAgainToolStripMenuItem
            // 
            this.testDetectionAgainToolStripMenuItem.Name = "testDetectionAgainToolStripMenuItem";
            this.testDetectionAgainToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.testDetectionAgainToolStripMenuItem.Text = "Test Detection Again";
            this.testDetectionAgainToolStripMenuItem.Click += new System.EventHandler(this.testDetectionAgainToolStripMenuItem_Click);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.detailsToolStripMenuItem.Text = "Prediction Details";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.detailsToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // dynamicMaskDetailsToolStripMenuItem
            // 
            this.dynamicMaskDetailsToolStripMenuItem.Name = "dynamicMaskDetailsToolStripMenuItem";
            this.dynamicMaskDetailsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.dynamicMaskDetailsToolStripMenuItem.Text = "Dynamic Mask Details";
            this.dynamicMaskDetailsToolStripMenuItem.Click += new System.EventHandler(this.dynamicMaskDetailsToolStripMenuItem_Click);
            // 
            // locateInLogToolStripMenuItem
            // 
            this.locateInLogToolStripMenuItem.Name = "locateInLogToolStripMenuItem";
            this.locateInLogToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.locateInLogToolStripMenuItem.Text = "Locate in log";
            this.locateInLogToolStripMenuItem.Click += new System.EventHandler(this.locateInLogToolStripMenuItem_Click);
            // 
            // manuallyAddImagesToolStripMenuItem
            // 
            this.manuallyAddImagesToolStripMenuItem.Name = "manuallyAddImagesToolStripMenuItem";
            this.manuallyAddImagesToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.manuallyAddImagesToolStripMenuItem.Text = "Manually Add Images";
            this.manuallyAddImagesToolStripMenuItem.Click += new System.EventHandler(this.manuallyAddImagesToolStripMenuItem_Click);
            // 
            // viewImageToolStripMenuItem
            // 
            this.viewImageToolStripMenuItem.Name = "viewImageToolStripMenuItem";
            this.viewImageToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.viewImageToolStripMenuItem.Text = "View Image";
            this.viewImageToolStripMenuItem.Click += new System.EventHandler(this.viewImageToolStripMenuItem_Click);
            // 
            // jumpToImageToolStripMenuItem
            // 
            this.jumpToImageToolStripMenuItem.Name = "jumpToImageToolStripMenuItem";
            this.jumpToImageToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.jumpToImageToolStripMenuItem.Text = "Jump To Image";
            this.jumpToImageToolStripMenuItem.Click += new System.EventHandler(this.jumpToImageToolStripMenuItem_Click);
            // 
            // lbl_objects
            // 
            this.lbl_objects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_objects.BackColor = System.Drawing.SystemColors.Info;
            this.lbl_objects.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_objects.Location = new System.Drawing.Point(1, 0);
            this.lbl_objects.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lbl_objects.Name = "lbl_objects";
            this.lbl_objects.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lbl_objects.Size = new System.Drawing.Size(730, 20);
            this.lbl_objects.TabIndex = 14;
            this.lbl_objects.Text = "No selection";
            this.lbl_objects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(4, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(725, 388);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // tabCameras
            // 
            this.tabCameras.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabCameras.Controls.Add(this.splitContainer1);
            this.tabCameras.Location = new System.Drawing.Point(4, 22);
            this.tabCameras.Name = "tabCameras";
            this.tabCameras.Padding = new System.Windows.Forms.Padding(3);
            this.tabCameras.Size = new System.Drawing.Size(1020, 458);
            this.tabCameras.TabIndex = 2;
            this.tabCameras.Text = "Cameras";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FOLV_Cameras);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel6);
            this.splitContainer1.Size = new System.Drawing.Size(1014, 452);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.TabIndex = 1;
            // 
            // FOLV_Cameras
            // 
            this.FOLV_Cameras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FOLV_Cameras.HideSelection = false;
            this.FOLV_Cameras.Location = new System.Drawing.Point(0, 0);
            this.FOLV_Cameras.Name = "FOLV_Cameras";
            this.FOLV_Cameras.ShowGroups = false;
            this.FOLV_Cameras.Size = new System.Drawing.Size(149, 448);
            this.FOLV_Cameras.TabIndex = 0;
            this.FOLV_Cameras.UseCompatibleStateImageBehavior = false;
            this.FOLV_Cameras.View = System.Windows.Forms.View.Details;
            this.FOLV_Cameras.VirtualMode = true;
            this.FOLV_Cameras.SelectionChanged += new System.EventHandler(this.FOLV_Cameras_SelectionChanged);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel11, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.lbl_camstats, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.82557F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.17443F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(853, 448);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel11.ColumnCount = 4;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.02243F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.32586F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.32586F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.32586F));
            this.tableLayoutPanel11.Controls.Add(this.btnCameraSave, 3, 0);
            this.tableLayoutPanel11.Controls.Add(this.btnCameraAdd, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.btnCameraDel, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.btnSaveTo, 2, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 410);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.Size = new System.Drawing.Size(847, 35);
            this.tableLayoutPanel11.TabIndex = 3;
            // 
            // btnCameraSave
            // 
            this.btnCameraSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCameraSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCameraSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCameraSave.Location = new System.Drawing.Point(695, 3);
            this.btnCameraSave.Name = "btnCameraSave";
            this.btnCameraSave.Size = new System.Drawing.Size(70, 30);
            this.btnCameraSave.TabIndex = 34;
            this.btnCameraSave.Text = "Save";
            this.btnCameraSave.UseVisualStyleBackColor = false;
            this.btnCameraSave.Click += new System.EventHandler(this.btnCameraSave_Click_1);
            // 
            // btnCameraAdd
            // 
            this.btnCameraAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCameraAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCameraAdd.Location = new System.Drawing.Point(41, 3);
            this.btnCameraAdd.Name = "btnCameraAdd";
            this.btnCameraAdd.Size = new System.Drawing.Size(70, 30);
            this.btnCameraAdd.TabIndex = 31;
            this.btnCameraAdd.Text = "Add";
            this.btnCameraAdd.UseVisualStyleBackColor = true;
            this.btnCameraAdd.Click += new System.EventHandler(this.btnCameraAdd_Click);
            // 
            // btnCameraDel
            // 
            this.btnCameraDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCameraDel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCameraDel.Location = new System.Drawing.Point(232, 3);
            this.btnCameraDel.Name = "btnCameraDel";
            this.btnCameraDel.Size = new System.Drawing.Size(70, 30);
            this.btnCameraDel.TabIndex = 32;
            this.btnCameraDel.Text = "Delete";
            this.btnCameraDel.UseVisualStyleBackColor = true;
            this.btnCameraDel.Click += new System.EventHandler(this.btnCameraDel_Click);
            // 
            // btnSaveTo
            // 
            this.btnSaveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnSaveTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSaveTo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveTo.Location = new System.Drawing.Point(463, 3);
            this.btnSaveTo.Name = "btnSaveTo";
            this.btnSaveTo.Size = new System.Drawing.Size(70, 30);
            this.btnSaveTo.TabIndex = 33;
            this.btnSaveTo.Text = "Apply to";
            this.btnSaveTo.UseVisualStyleBackColor = false;
            this.btnSaveTo.Click += new System.EventHandler(this.btnSaveTo_Click);
            // 
            // lbl_camstats
            // 
            this.lbl_camstats.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_camstats.AutoSize = true;
            this.lbl_camstats.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_camstats.Location = new System.Drawing.Point(3, 5);
            this.lbl_camstats.Name = "lbl_camstats";
            this.lbl_camstats.Size = new System.Drawing.Size(38, 17);
            this.lbl_camstats.TabIndex = 4;
            this.lbl_camstats.Text = "Stats";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel7.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.60259F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.39741F));
            this.tableLayoutPanel7.Controls.Add(this.label26, 0, 9);
            this.tableLayoutPanel7.Controls.Add(this.label14, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.label1, 0, 7);
            this.tableLayoutPanel7.Controls.Add(this.lblPrefix, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel12, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel13, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.lblRelevantObjects, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel26, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.label15, 0, 8);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel27, 1, 8);
            this.tableLayoutPanel7.Controls.Add(this.btnActions, 1, 7);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.label20, 0, 5);
            this.tableLayoutPanel7.Controls.Add(this.tbAdditionalRelevantObjects, 1, 5);
            this.tableLayoutPanel7.Controls.Add(this.label25, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.dbLayoutPanel6, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.dbLayoutPanel7, 1, 9);
            this.tableLayoutPanel7.Controls.Add(this.label39, 0, 6);
            this.tableLayoutPanel7.Controls.Add(this.BtnPredictionSize, 1, 6);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 30);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 10;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.792577F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.79654F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.792996F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.792996F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.84341F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.795638F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.799398F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.796359F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.794436F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.795647F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(847, 374);
            this.tableLayoutPanel7.TabIndex = 2;
            this.tableLayoutPanel7.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel7_Paint);
            // 
            // label26
            // 
            this.label26.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label26.Location = new System.Drawing.Point(44, 345);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(103, 15);
            this.label26.TabIndex = 19;
            this.label26.Text = "Custom Mask File";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(71, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 15);
            this.label14.TabIndex = 17;
            this.label14.Text = "Input Folder";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(98, 277);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Actions";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.Location = new System.Drawing.Point(21, 73);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(125, 15);
            this.lblPrefix.TabIndex = 2;
            this.lblPrefix.Text = "Input file begins with";
            this.lblPrefix.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(20, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(126, 15);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "AI Tool Camera Name";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Controls.Add(this.lbl_prefix, 1, 0);
            this.tableLayoutPanel12.Controls.Add(this.tbPrefix, 0, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(152, 66);
            this.tableLayoutPanel12.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 1;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(692, 29);
            this.tableLayoutPanel12.TabIndex = 12;
            // 
            // lbl_prefix
            // 
            this.lbl_prefix.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_prefix.AutoSize = true;
            this.lbl_prefix.Location = new System.Drawing.Point(519, 7);
            this.lbl_prefix.Name = "lbl_prefix";
            this.lbl_prefix.Size = new System.Drawing.Size(0, 15);
            this.lbl_prefix.TabIndex = 6;
            // 
            // tbPrefix
            // 
            this.tbPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPrefix.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbPrefix.Location = new System.Drawing.Point(21, 3);
            this.tbPrefix.Margin = new System.Windows.Forms.Padding(21, 3, 21, 3);
            this.tbPrefix.Name = "tbPrefix";
            this.tbPrefix.Size = new System.Drawing.Size(304, 23);
            this.tbPrefix.TabIndex = 3;
            this.tbPrefix.TextChanged += new System.EventHandler(this.tbPrefix_TextChanged);
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.Controls.Add(this.cb_enabled, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.tbName, 0, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(153, 2);
            this.tableLayoutPanel13.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(690, 29);
            this.tableLayoutPanel13.TabIndex = 13;
            // 
            // cb_enabled
            // 
            this.cb_enabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_enabled.AutoSize = true;
            this.cb_enabled.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_enabled.Location = new System.Drawing.Point(366, 5);
            this.cb_enabled.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_enabled.Name = "cb_enabled";
            this.cb_enabled.Size = new System.Drawing.Size(211, 19);
            this.cb_enabled.TabIndex = 1;
            this.cb_enabled.Text = "Enable AI Detection for this camera";
            this.cb_enabled.UseVisualStyleBackColor = true;
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbName.Location = new System.Drawing.Point(21, 3);
            this.tbName.Margin = new System.Windows.Forms.Padding(21, 3, 21, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(303, 23);
            this.tbName.TabIndex = 0;
            // 
            // lblRelevantObjects
            // 
            this.lblRelevantObjects.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRelevantObjects.AutoSize = true;
            this.lblRelevantObjects.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRelevantObjects.Location = new System.Drawing.Point(44, 159);
            this.lblRelevantObjects.Name = "lblRelevantObjects";
            this.lblRelevantObjects.Size = new System.Drawing.Size(102, 15);
            this.lblRelevantObjects.TabIndex = 1;
            this.lblRelevantObjects.Text = "Relevant Objects";
            // 
            // tableLayoutPanel26
            // 
            this.tableLayoutPanel26.ColumnCount = 3;
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.5F));
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel26.Controls.Add(this.cmbcaminput, 0, 0);
            this.tableLayoutPanel26.Controls.Add(this.cb_monitorCamInputfolder, 1, 0);
            this.tableLayoutPanel26.Controls.Add(this.button2, 2, 0);
            this.tableLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel26.Location = new System.Drawing.Point(152, 98);
            this.tableLayoutPanel26.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.tableLayoutPanel26.Name = "tableLayoutPanel26";
            this.tableLayoutPanel26.RowCount = 1;
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel26.Size = new System.Drawing.Size(692, 29);
            this.tableLayoutPanel26.TabIndex = 18;
            // 
            // cmbcaminput
            // 
            this.cmbcaminput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbcaminput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcaminput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcaminput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbcaminput.FormattingEnabled = true;
            this.cmbcaminput.Location = new System.Drawing.Point(21, 3);
            this.cmbcaminput.Margin = new System.Windows.Forms.Padding(21, 2, 21, 2);
            this.cmbcaminput.Name = "cmbcaminput";
            this.cmbcaminput.Size = new System.Drawing.Size(390, 23);
            this.cmbcaminput.TabIndex = 4;
            // 
            // cb_monitorCamInputfolder
            // 
            this.cb_monitorCamInputfolder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cb_monitorCamInputfolder.AutoSize = true;
            this.cb_monitorCamInputfolder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_monitorCamInputfolder.Location = new System.Drawing.Point(454, 5);
            this.cb_monitorCamInputfolder.Margin = new System.Windows.Forms.Padding(2);
            this.cb_monitorCamInputfolder.Name = "cb_monitorCamInputfolder";
            this.cb_monitorCamInputfolder.Size = new System.Drawing.Size(128, 19);
            this.cb_monitorCamInputfolder.TabIndex = 5;
            this.cb_monitorCamInputfolder.Text = "Monitor Subfolders";
            this.cb_monitorCamInputfolder.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button2.Location = new System.Drawing.Point(613, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 21);
            this.button2.TabIndex = 6;
            this.button2.Text = "Select...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(94, 309);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 15);
            this.label15.TabIndex = 19;
            this.label15.Text = "Masking";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel27
            // 
            this.tableLayoutPanel27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel27.ColumnCount = 5;
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.63504F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.54895F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.23776F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.8986F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.94406F));
            this.tableLayoutPanel27.Controls.Add(this.cb_masking_enabled, 0, 0);
            this.tableLayoutPanel27.Controls.Add(this.BtnDynamicMaskingSettings, 1, 0);
            this.tableLayoutPanel27.Controls.Add(this.btnDetails, 2, 0);
            this.tableLayoutPanel27.Controls.Add(this.btnCustomMask, 4, 0);
            this.tableLayoutPanel27.Controls.Add(this.lblDrawMask, 3, 0);
            this.tableLayoutPanel27.Location = new System.Drawing.Point(152, 302);
            this.tableLayoutPanel27.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.tableLayoutPanel27.Name = "tableLayoutPanel27";
            this.tableLayoutPanel27.RowCount = 1;
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel27.Size = new System.Drawing.Size(692, 29);
            this.tableLayoutPanel27.TabIndex = 20;
            // 
            // cb_masking_enabled
            // 
            this.cb_masking_enabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_masking_enabled.AutoSize = true;
            this.cb_masking_enabled.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_masking_enabled.Location = new System.Drawing.Point(21, 6);
            this.cb_masking_enabled.Margin = new System.Windows.Forms.Padding(21, 3, 5, 0);
            this.cb_masking_enabled.Name = "cb_masking_enabled";
            this.cb_masking_enabled.Size = new System.Drawing.Size(158, 19);
            this.cb_masking_enabled.TabIndex = 25;
            this.cb_masking_enabled.Text = "Enable dynamic masking";
            this.cb_masking_enabled.UseVisualStyleBackColor = true;
            // 
            // BtnDynamicMaskingSettings
            // 
            this.BtnDynamicMaskingSettings.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnDynamicMaskingSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnDynamicMaskingSettings.Location = new System.Drawing.Point(209, 2);
            this.BtnDynamicMaskingSettings.Margin = new System.Windows.Forms.Padding(5, 1, 5, 1);
            this.BtnDynamicMaskingSettings.Name = "BtnDynamicMaskingSettings";
            this.BtnDynamicMaskingSettings.Size = new System.Drawing.Size(70, 25);
            this.BtnDynamicMaskingSettings.TabIndex = 26;
            this.BtnDynamicMaskingSettings.Text = "Settings";
            this.BtnDynamicMaskingSettings.UseVisualStyleBackColor = true;
            this.BtnDynamicMaskingSettings.Click += new System.EventHandler(this.BtnDynamicMaskingSettings_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDetails.Location = new System.Drawing.Point(302, 3);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(70, 23);
            this.btnDetails.TabIndex = 27;
            this.btnDetails.Text = "Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // btnCustomMask
            // 
            this.btnCustomMask.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCustomMask.Location = new System.Drawing.Point(477, 3);
            this.btnCustomMask.Margin = new System.Windows.Forms.Padding(1, 2, 5, 2);
            this.btnCustomMask.Name = "btnCustomMask";
            this.btnCustomMask.Size = new System.Drawing.Size(70, 23);
            this.btnCustomMask.TabIndex = 28;
            this.btnCustomMask.Text = "Custom";
            this.btnCustomMask.UseVisualStyleBackColor = true;
            this.btnCustomMask.Click += new System.EventHandler(this.btnCustomMask_Click);
            // 
            // lblDrawMask
            // 
            this.lblDrawMask.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDrawMask.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDrawMask.Location = new System.Drawing.Point(409, 6);
            this.lblDrawMask.Margin = new System.Windows.Forms.Padding(0);
            this.lblDrawMask.Name = "lblDrawMask";
            this.lblDrawMask.Size = new System.Drawing.Size(67, 16);
            this.lblDrawMask.TabIndex = 25;
            this.lblDrawMask.Text = "Draw Mask";
            this.lblDrawMask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnActions
            // 
            this.btnActions.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnActions.Location = new System.Drawing.Point(171, 272);
            this.btnActions.Margin = new System.Windows.Forms.Padding(21, 2, 2, 2);
            this.btnActions.Name = "btnActions";
            this.btnActions.Size = new System.Drawing.Size(70, 25);
            this.btnActions.TabIndex = 24;
            this.btnActions.Text = "Settings";
            this.btnActions.UseVisualStyleBackColor = true;
            this.btnActions.Click += new System.EventHandler(this.btnActions_Click_1);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.ColumnCount = 5;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.Controls.Add(this.cb_person, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.cb_bicycle, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.cb_motorcycle, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.cb_bear, 4, 2);
            this.tableLayoutPanel8.Controls.Add(this.cb_cow, 4, 1);
            this.tableLayoutPanel8.Controls.Add(this.cb_sheep, 4, 0);
            this.tableLayoutPanel8.Controls.Add(this.cb_horse, 3, 2);
            this.tableLayoutPanel8.Controls.Add(this.cb_bird, 3, 1);
            this.tableLayoutPanel8.Controls.Add(this.cb_dog, 3, 0);
            this.tableLayoutPanel8.Controls.Add(this.cb_cat, 2, 2);
            this.tableLayoutPanel8.Controls.Add(this.cb_airplane, 2, 1);
            this.tableLayoutPanel8.Controls.Add(this.cb_boat, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.cb_bus, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.cb_truck, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.cb_car, 1, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(153, 132);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(690, 69);
            this.tableLayoutPanel8.TabIndex = 14;
            // 
            // cb_person
            // 
            this.cb_person.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_person.AutoSize = true;
            this.cb_person.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_person.Location = new System.Drawing.Point(21, 3);
            this.cb_person.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_person.Name = "cb_person";
            this.cb_person.Size = new System.Drawing.Size(62, 17);
            this.cb_person.TabIndex = 7;
            this.cb_person.Text = "Person";
            this.cb_person.UseVisualStyleBackColor = true;
            this.cb_person.CheckedChanged += new System.EventHandler(this.cb_person_CheckedChanged);
            // 
            // cb_bicycle
            // 
            this.cb_bicycle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_bicycle.AutoSize = true;
            this.cb_bicycle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_bicycle.Location = new System.Drawing.Point(21, 26);
            this.cb_bicycle.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_bicycle.Name = "cb_bicycle";
            this.cb_bicycle.Size = new System.Drawing.Size(63, 17);
            this.cb_bicycle.TabIndex = 12;
            this.cb_bicycle.Text = "Bicycle";
            this.cb_bicycle.UseVisualStyleBackColor = true;
            // 
            // cb_motorcycle
            // 
            this.cb_motorcycle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_motorcycle.AutoSize = true;
            this.cb_motorcycle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_motorcycle.Location = new System.Drawing.Point(21, 49);
            this.cb_motorcycle.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_motorcycle.Name = "cb_motorcycle";
            this.cb_motorcycle.Size = new System.Drawing.Size(86, 17);
            this.cb_motorcycle.TabIndex = 17;
            this.cb_motorcycle.Text = "Motorcycle";
            this.cb_motorcycle.UseVisualStyleBackColor = true;
            // 
            // cb_bear
            // 
            this.cb_bear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_bear.AutoSize = true;
            this.cb_bear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_bear.Location = new System.Drawing.Point(573, 49);
            this.cb_bear.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_bear.Name = "cb_bear";
            this.cb_bear.Size = new System.Drawing.Size(49, 17);
            this.cb_bear.TabIndex = 21;
            this.cb_bear.Text = "Bear";
            this.cb_bear.UseVisualStyleBackColor = true;
            // 
            // cb_cow
            // 
            this.cb_cow.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_cow.AutoSize = true;
            this.cb_cow.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_cow.Location = new System.Drawing.Point(573, 26);
            this.cb_cow.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_cow.Name = "cb_cow";
            this.cb_cow.Size = new System.Drawing.Size(50, 17);
            this.cb_cow.TabIndex = 16;
            this.cb_cow.Text = "Cow";
            this.cb_cow.UseVisualStyleBackColor = true;
            // 
            // cb_sheep
            // 
            this.cb_sheep.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_sheep.AutoSize = true;
            this.cb_sheep.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_sheep.Location = new System.Drawing.Point(573, 3);
            this.cb_sheep.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_sheep.Name = "cb_sheep";
            this.cb_sheep.Size = new System.Drawing.Size(58, 17);
            this.cb_sheep.TabIndex = 11;
            this.cb_sheep.Text = "Sheep";
            this.cb_sheep.UseVisualStyleBackColor = true;
            // 
            // cb_horse
            // 
            this.cb_horse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_horse.AutoSize = true;
            this.cb_horse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_horse.Location = new System.Drawing.Point(435, 49);
            this.cb_horse.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_horse.Name = "cb_horse";
            this.cb_horse.Size = new System.Drawing.Size(57, 17);
            this.cb_horse.TabIndex = 21;
            this.cb_horse.Text = "Horse";
            this.cb_horse.UseVisualStyleBackColor = true;
            // 
            // cb_bird
            // 
            this.cb_bird.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_bird.AutoSize = true;
            this.cb_bird.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_bird.Location = new System.Drawing.Point(435, 26);
            this.cb_bird.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_bird.Name = "cb_bird";
            this.cb_bird.Size = new System.Drawing.Size(47, 17);
            this.cb_bird.TabIndex = 15;
            this.cb_bird.Text = "Bird";
            this.cb_bird.UseVisualStyleBackColor = true;
            // 
            // cb_dog
            // 
            this.cb_dog.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_dog.AutoSize = true;
            this.cb_dog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_dog.Location = new System.Drawing.Point(435, 3);
            this.cb_dog.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_dog.Name = "cb_dog";
            this.cb_dog.Size = new System.Drawing.Size(48, 17);
            this.cb_dog.TabIndex = 10;
            this.cb_dog.Text = "Dog";
            this.cb_dog.UseVisualStyleBackColor = true;
            // 
            // cb_cat
            // 
            this.cb_cat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_cat.AutoSize = true;
            this.cb_cat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_cat.Location = new System.Drawing.Point(297, 49);
            this.cb_cat.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_cat.Name = "cb_cat";
            this.cb_cat.Size = new System.Drawing.Size(44, 17);
            this.cb_cat.TabIndex = 19;
            this.cb_cat.Text = "Cat";
            this.cb_cat.UseVisualStyleBackColor = true;
            // 
            // cb_airplane
            // 
            this.cb_airplane.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_airplane.AutoSize = true;
            this.cb_airplane.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_airplane.Location = new System.Drawing.Point(297, 26);
            this.cb_airplane.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_airplane.Name = "cb_airplane";
            this.cb_airplane.Size = new System.Drawing.Size(70, 17);
            this.cb_airplane.TabIndex = 14;
            this.cb_airplane.Text = "Airplane";
            this.cb_airplane.UseVisualStyleBackColor = true;
            // 
            // cb_boat
            // 
            this.cb_boat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_boat.AutoSize = true;
            this.cb_boat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_boat.Location = new System.Drawing.Point(297, 3);
            this.cb_boat.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_boat.Name = "cb_boat";
            this.cb_boat.Size = new System.Drawing.Size(50, 17);
            this.cb_boat.TabIndex = 9;
            this.cb_boat.Text = "Boat";
            this.cb_boat.UseVisualStyleBackColor = true;
            // 
            // cb_bus
            // 
            this.cb_bus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_bus.AutoSize = true;
            this.cb_bus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_bus.Location = new System.Drawing.Point(159, 49);
            this.cb_bus.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_bus.Name = "cb_bus";
            this.cb_bus.Size = new System.Drawing.Size(45, 17);
            this.cb_bus.TabIndex = 18;
            this.cb_bus.Text = "Bus";
            this.cb_bus.UseVisualStyleBackColor = true;
            // 
            // cb_truck
            // 
            this.cb_truck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_truck.AutoSize = true;
            this.cb_truck.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_truck.Location = new System.Drawing.Point(159, 26);
            this.cb_truck.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_truck.Name = "cb_truck";
            this.cb_truck.Size = new System.Drawing.Size(54, 17);
            this.cb_truck.TabIndex = 13;
            this.cb_truck.Text = "Truck";
            this.cb_truck.UseVisualStyleBackColor = true;
            // 
            // cb_car
            // 
            this.cb_car.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_car.AutoSize = true;
            this.cb_car.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_car.Location = new System.Drawing.Point(159, 3);
            this.cb_car.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_car.Name = "cb_car";
            this.cb_car.Size = new System.Drawing.Size(44, 17);
            this.cb_car.TabIndex = 8;
            this.cb_car.Text = "Car";
            this.cb_car.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(30, 205);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(116, 30);
            this.label20.TabIndex = 1;
            this.label20.Text = "Additional Relevant Objects";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbAdditionalRelevantObjects
            // 
            this.tbAdditionalRelevantObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAdditionalRelevantObjects.Location = new System.Drawing.Point(153, 209);
            this.tbAdditionalRelevantObjects.Name = "tbAdditionalRelevantObjects";
            this.tbAdditionalRelevantObjects.Size = new System.Drawing.Size(690, 23);
            this.tbAdditionalRelevantObjects.TabIndex = 22;
            this.toolTip1.SetToolTip(this.tbAdditionalRelevantObjects, "comma separated list of custom object names to be accepted as relevant.");
            // 
            // label25
            // 
            this.label25.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label25.Location = new System.Drawing.Point(46, 41);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(100, 15);
            this.label25.TabIndex = 10;
            this.label25.Text = "BI Camera Name";
            // 
            // dbLayoutPanel6
            // 
            this.dbLayoutPanel6.ColumnCount = 2;
            this.dbLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dbLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dbLayoutPanel6.Controls.Add(this.tbBiCamName, 0, 0);
            this.dbLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbLayoutPanel6.Location = new System.Drawing.Point(153, 34);
            this.dbLayoutPanel6.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dbLayoutPanel6.Name = "dbLayoutPanel6";
            this.dbLayoutPanel6.RowCount = 1;
            this.dbLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dbLayoutPanel6.Size = new System.Drawing.Size(690, 29);
            this.dbLayoutPanel6.TabIndex = 24;
            // 
            // tbBiCamName
            // 
            this.tbBiCamName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBiCamName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbBiCamName.Location = new System.Drawing.Point(21, 3);
            this.tbBiCamName.Margin = new System.Windows.Forms.Padding(21, 3, 21, 3);
            this.tbBiCamName.Name = "tbBiCamName";
            this.tbBiCamName.Size = new System.Drawing.Size(303, 23);
            this.tbBiCamName.TabIndex = 2;
            // 
            // dbLayoutPanel7
            // 
            this.dbLayoutPanel7.ColumnCount = 3;
            this.dbLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.62048F));
            this.dbLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.86747F));
            this.dbLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.51205F));
            this.dbLayoutPanel7.Controls.Add(this.tb_camera_telegram_chatid, 2, 0);
            this.dbLayoutPanel7.Controls.Add(this.tbCustomMaskFile, 0, 0);
            this.dbLayoutPanel7.Controls.Add(this.label21, 1, 0);
            this.dbLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbLayoutPanel7.Location = new System.Drawing.Point(153, 334);
            this.dbLayoutPanel7.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dbLayoutPanel7.Name = "dbLayoutPanel7";
            this.dbLayoutPanel7.RowCount = 1;
            this.dbLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbLayoutPanel7.Size = new System.Drawing.Size(690, 38);
            this.dbLayoutPanel7.TabIndex = 25;
            // 
            // tb_camera_telegram_chatid
            // 
            this.tb_camera_telegram_chatid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_camera_telegram_chatid.Location = new System.Drawing.Point(413, 7);
            this.tb_camera_telegram_chatid.Name = "tb_camera_telegram_chatid";
            this.tb_camera_telegram_chatid.Size = new System.Drawing.Size(274, 23);
            this.tb_camera_telegram_chatid.TabIndex = 30;
            this.toolTip1.SetToolTip(this.tb_camera_telegram_chatid, "This overrides the chatid in the settings tab.");
            // 
            // tbCustomMaskFile
            // 
            this.tbCustomMaskFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCustomMaskFile.Location = new System.Drawing.Point(3, 7);
            this.tbCustomMaskFile.Name = "tbCustomMaskFile";
            this.tbCustomMaskFile.Size = new System.Drawing.Size(288, 23);
            this.tbCustomMaskFile.TabIndex = 29;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(296, 9);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(112, 19);
            this.label21.TabIndex = 19;
            this.label21.Text = "Telegram Chat ID Override";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label39
            // 
            this.label39.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label39.Location = new System.Drawing.Point(17, 245);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(129, 15);
            this.label39.TabIndex = 15;
            this.label39.Text = "Prediction Tolerances:";
            // 
            // BtnPredictionSize
            // 
            this.BtnPredictionSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnPredictionSize.Location = new System.Drawing.Point(171, 240);
            this.BtnPredictionSize.Margin = new System.Windows.Forms.Padding(21, 2, 2, 2);
            this.BtnPredictionSize.Name = "BtnPredictionSize";
            this.BtnPredictionSize.Size = new System.Drawing.Size(70, 25);
            this.BtnPredictionSize.TabIndex = 23;
            this.BtnPredictionSize.Text = "Settings";
            this.BtnPredictionSize.UseVisualStyleBackColor = true;
            this.BtnPredictionSize.Click += new System.EventHandler(this.BtnPredictionSize_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tableLayoutPanel4);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(1020, 458);
            this.tabSettings.TabIndex = 3;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.dbLayoutPanel10, 0, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1022, 458);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel5.Controls.Add(this.lbl_input, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.lbl_telegram_token, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel18, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.lbl_deepstackurl, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.dbLayoutPanel1, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.dbLayoutPanel2, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.label12, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.dbLayoutPanel3, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.dbLayoutPanel4, 1, 6);
            this.tableLayoutPanel5.Controls.Add(this.label18, 0, 7);
            this.tableLayoutPanel5.Controls.Add(this.dbLayoutPanel5, 1, 7);
            this.tableLayoutPanel5.Controls.Add(this.label29, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.dbLayoutPanel8, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.dbLayoutPanel9, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.label13, 0, 5);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 8;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.42959F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.29149F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.54469F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5499F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.54591F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.54572F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.54632F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.54638F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1016, 412);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // lbl_input
            // 
            this.lbl_input.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_input.AutoSize = true;
            this.lbl_input.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_input.Location = new System.Drawing.Point(26, 17);
            this.lbl_input.Name = "lbl_input";
            this.lbl_input.Size = new System.Drawing.Size(123, 17);
            this.lbl_input.TabIndex = 1;
            this.lbl_input.Text = "Default Input Path";
            // 
            // lbl_telegram_token
            // 
            this.lbl_telegram_token.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_telegram_token.AutoSize = true;
            this.lbl_telegram_token.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_telegram_token.Location = new System.Drawing.Point(43, 118);
            this.lbl_telegram_token.Name = "lbl_telegram_token";
            this.lbl_telegram_token.Size = new System.Drawing.Size(106, 17);
            this.lbl_telegram_token.TabIndex = 6;
            this.lbl_telegram_token.Text = "Telegram Token";
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.ColumnCount = 3;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.09702F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.45378F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.4492F));
            this.tableLayoutPanel18.Controls.Add(this.btn_input_path, 2, 0);
            this.tableLayoutPanel18.Controls.Add(this.cmbInput, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.cb_inputpathsubfolders, 1, 0);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(156, 4);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 1;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(856, 44);
            this.tableLayoutPanel18.TabIndex = 12;
            // 
            // btn_input_path
            // 
            this.btn_input_path.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_input_path.Location = new System.Drawing.Point(763, 7);
            this.btn_input_path.Name = "btn_input_path";
            this.btn_input_path.Size = new System.Drawing.Size(70, 30);
            this.btn_input_path.TabIndex = 2;
            this.btn_input_path.Text = "Select...";
            this.btn_input_path.UseVisualStyleBackColor = true;
            this.btn_input_path.Click += new System.EventHandler(this.btn_input_path_Click);
            // 
            // cmbInput
            // 
            this.cmbInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbInput.FormattingEnabled = true;
            this.cmbInput.Location = new System.Drawing.Point(3, 11);
            this.cmbInput.Margin = new System.Windows.Forms.Padding(3, 2, 2, 2);
            this.cmbInput.Name = "cmbInput";
            this.cmbInput.Size = new System.Drawing.Size(620, 21);
            this.cmbInput.TabIndex = 0;
            // 
            // cb_inputpathsubfolders
            // 
            this.cb_inputpathsubfolders.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_inputpathsubfolders.AutoSize = true;
            this.cb_inputpathsubfolders.Location = new System.Drawing.Point(636, 13);
            this.cb_inputpathsubfolders.Margin = new System.Windows.Forms.Padding(11, 2, 2, 2);
            this.cb_inputpathsubfolders.Name = "cb_inputpathsubfolders";
            this.cb_inputpathsubfolders.Size = new System.Drawing.Size(76, 17);
            this.cb_inputpathsubfolders.TabIndex = 1;
            this.cb_inputpathsubfolders.Text = "Subfolders";
            this.cb_inputpathsubfolders.UseVisualStyleBackColor = true;
            // 
            // lbl_deepstackurl
            // 
            this.lbl_deepstackurl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_deepstackurl.AutoSize = true;
            this.lbl_deepstackurl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_deepstackurl.Location = new System.Drawing.Point(41, 68);
            this.lbl_deepstackurl.Name = "lbl_deepstackurl";
            this.lbl_deepstackurl.Size = new System.Drawing.Size(108, 17);
            this.lbl_deepstackurl.TabIndex = 4;
            this.lbl_deepstackurl.Text = "AI Server URL(s)";
            // 
            // dbLayoutPanel1
            // 
            this.dbLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbLayoutPanel1.ColumnCount = 3;
            this.dbLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.96037F));
            this.dbLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.51981F));
            this.dbLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.45F));
            this.dbLayoutPanel1.Controls.Add(this.cb_DeepStackURLsQueued, 1, 0);
            this.dbLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.dbLayoutPanel1.Location = new System.Drawing.Point(155, 54);
            this.dbLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.dbLayoutPanel1.Name = "dbLayoutPanel1";
            this.dbLayoutPanel1.RowCount = 1;
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbLayoutPanel1.Size = new System.Drawing.Size(858, 45);
            this.dbLayoutPanel1.TabIndex = 18;
            // 
            // cb_DeepStackURLsQueued
            // 
            this.cb_DeepStackURLsQueued.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_DeepStackURLsQueued.AutoSize = true;
            this.cb_DeepStackURLsQueued.Location = new System.Drawing.Point(637, 14);
            this.cb_DeepStackURLsQueued.Margin = new System.Windows.Forms.Padding(11, 2, 2, 2);
            this.cb_DeepStackURLsQueued.Name = "cb_DeepStackURLsQueued";
            this.cb_DeepStackURLsQueued.Size = new System.Drawing.Size(64, 17);
            this.cb_DeepStackURLsQueued.TabIndex = 4;
            this.cb_DeepStackURLsQueued.Text = "Queued";
            this.toolTip1.SetToolTip(this.cb_DeepStackURLsQueued, "When checked, all urls will take turns processing the images.\r\nWhen unchecked, th" +
        "e original order will always be used.");
            this.cb_DeepStackURLsQueued.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.Location = new System.Drawing.Point(3, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "Edit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // dbLayoutPanel2
            // 
            this.dbLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbLayoutPanel2.ColumnCount = 5;
            this.dbLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.75058F));
            this.dbLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.275059F));
            this.dbLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.05128F));
            this.dbLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.dbLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.dbLayoutPanel2.Controls.Add(this.tb_telegram_cooldown, 4, 0);
            this.dbLayoutPanel2.Controls.Add(this.label5, 3, 0);
            this.dbLayoutPanel2.Controls.Add(this.tb_telegram_chatid, 2, 0);
            this.dbLayoutPanel2.Controls.Add(this.lbl_telegram_chatid, 1, 0);
            this.dbLayoutPanel2.Controls.Add(this.tb_telegram_token, 0, 0);
            this.dbLayoutPanel2.Location = new System.Drawing.Point(155, 104);
            this.dbLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.dbLayoutPanel2.Name = "dbLayoutPanel2";
            this.dbLayoutPanel2.RowCount = 1;
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbLayoutPanel2.Size = new System.Drawing.Size(858, 46);
            this.dbLayoutPanel2.TabIndex = 19;
            // 
            // tb_telegram_cooldown
            // 
            this.tb_telegram_cooldown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_telegram_cooldown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_telegram_cooldown.Location = new System.Drawing.Point(743, 10);
            this.tb_telegram_cooldown.Name = "tb_telegram_cooldown";
            this.tb_telegram_cooldown.Size = new System.Drawing.Size(112, 25);
            this.tb_telegram_cooldown.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(637, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Cooldown Secs";
            // 
            // tb_telegram_chatid
            // 
            this.tb_telegram_chatid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_telegram_chatid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_telegram_chatid.Location = new System.Drawing.Point(353, 10);
            this.tb_telegram_chatid.Name = "tb_telegram_chatid";
            this.tb_telegram_chatid.Size = new System.Drawing.Size(268, 25);
            this.tb_telegram_chatid.TabIndex = 6;
            // 
            // lbl_telegram_chatid
            // 
            this.lbl_telegram_chatid.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_telegram_chatid.AutoSize = true;
            this.lbl_telegram_chatid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_telegram_chatid.Location = new System.Drawing.Point(293, 14);
            this.lbl_telegram_chatid.Name = "lbl_telegram_chatid";
            this.lbl_telegram_chatid.Size = new System.Drawing.Size(54, 17);
            this.lbl_telegram_chatid.TabIndex = 7;
            this.lbl_telegram_chatid.Text = "Chat ID";
            // 
            // tb_telegram_token
            // 
            this.tb_telegram_token.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_telegram_token.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_telegram_token.Location = new System.Drawing.Point(3, 10);
            this.tb_telegram_token.Name = "tb_telegram_token";
            this.tb_telegram_token.Size = new System.Drawing.Size(274, 25);
            this.tb_telegram_token.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(71, 220);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 17);
            this.label12.TabIndex = 13;
            this.label12.Text = "Send Errors";
            // 
            // dbLayoutPanel3
            // 
            this.dbLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbLayoutPanel3.ColumnCount = 4;
            this.dbLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.16822F));
            this.dbLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.85514F));
            this.dbLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.918678F));
            this.dbLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.9018F));
            this.dbLayoutPanel3.Controls.Add(this.cb_send_telegram_errors, 0, 0);
            this.dbLayoutPanel3.Controls.Add(this.btn_enabletelegram, 2, 0);
            this.dbLayoutPanel3.Controls.Add(this.btn_disabletelegram, 3, 0);
            this.dbLayoutPanel3.Controls.Add(this.cb_send_pushover_errors, 1, 0);
            this.dbLayoutPanel3.Location = new System.Drawing.Point(156, 207);
            this.dbLayoutPanel3.Name = "dbLayoutPanel3";
            this.dbLayoutPanel3.RowCount = 1;
            this.dbLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbLayoutPanel3.Size = new System.Drawing.Size(856, 44);
            this.dbLayoutPanel3.TabIndex = 20;
            // 
            // cb_send_telegram_errors
            // 
            this.cb_send_telegram_errors.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_send_telegram_errors.AutoSize = true;
            this.cb_send_telegram_errors.Location = new System.Drawing.Point(3, 13);
            this.cb_send_telegram_errors.Name = "cb_send_telegram_errors";
            this.cb_send_telegram_errors.Size = new System.Drawing.Size(70, 17);
            this.cb_send_telegram_errors.TabIndex = 11;
            this.cb_send_telegram_errors.Text = "Telegram";
            this.cb_send_telegram_errors.UseVisualStyleBackColor = true;
            // 
            // btn_enabletelegram
            // 
            this.btn_enabletelegram.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_enabletelegram.Location = new System.Drawing.Point(693, 7);
            this.btn_enabletelegram.Name = "btn_enabletelegram";
            this.btn_enabletelegram.Size = new System.Drawing.Size(69, 30);
            this.btn_enabletelegram.TabIndex = 13;
            this.btn_enabletelegram.Text = "Enable All";
            this.toolTip1.SetToolTip(this.btn_enabletelegram, "Enable Telegram or Pushover on all cameras");
            this.btn_enabletelegram.UseVisualStyleBackColor = true;
            this.btn_enabletelegram.Click += new System.EventHandler(this.btn_enabletelegram_Click);
            // 
            // btn_disabletelegram
            // 
            this.btn_disabletelegram.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_disabletelegram.Location = new System.Drawing.Point(778, 7);
            this.btn_disabletelegram.Name = "btn_disabletelegram";
            this.btn_disabletelegram.Size = new System.Drawing.Size(70, 30);
            this.btn_disabletelegram.TabIndex = 14;
            this.btn_disabletelegram.Text = "Disable All";
            this.toolTip1.SetToolTip(this.btn_disabletelegram, "Disable Telegram or Pushover on all Cameras");
            this.btn_disabletelegram.UseVisualStyleBackColor = true;
            this.btn_disabletelegram.Click += new System.EventHandler(this.btn_disabletelegram_Click);
            // 
            // cb_send_pushover_errors
            // 
            this.cb_send_pushover_errors.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_send_pushover_errors.AutoSize = true;
            this.cb_send_pushover_errors.Location = new System.Drawing.Point(227, 13);
            this.cb_send_pushover_errors.Name = "cb_send_pushover_errors";
            this.cb_send_pushover_errors.Size = new System.Drawing.Size(71, 17);
            this.cb_send_pushover_errors.TabIndex = 12;
            this.cb_send_pushover_errors.Text = "Pushover";
            this.cb_send_pushover_errors.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(23, 322);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Default Credentials";
            // 
            // dbLayoutPanel4
            // 
            this.dbLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbLayoutPanel4.ColumnCount = 5;
            this.dbLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.dbLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.dbLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.dbLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.dbLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 406F));
            this.dbLayoutPanel4.Controls.Add(this.label6, 0, 0);
            this.dbLayoutPanel4.Controls.Add(this.tb_username, 1, 0);
            this.dbLayoutPanel4.Controls.Add(this.tb_password, 3, 0);
            this.dbLayoutPanel4.Controls.Add(this.label16, 2, 0);
            this.dbLayoutPanel4.Controls.Add(this.label17, 4, 0);
            this.dbLayoutPanel4.Location = new System.Drawing.Point(156, 309);
            this.dbLayoutPanel4.Name = "dbLayoutPanel4";
            this.dbLayoutPanel4.RowCount = 1;
            this.dbLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbLayoutPanel4.Size = new System.Drawing.Size(856, 44);
            this.dbLayoutPanel4.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Username:";
            // 
            // tb_username
            // 
            this.tb_username.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_username.Location = new System.Drawing.Point(78, 12);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(144, 20);
            this.tb_username.TabIndex = 17;
            // 
            // tb_password
            // 
            this.tb_password.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_password.Location = new System.Drawing.Point(303, 12);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(144, 20);
            this.tb_password.TabIndex = 18;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(241, 15);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Password:";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(453, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(400, 26);
            this.label17.TabIndex = 3;
            this.label17.Text = "These will be used with the [Username] and [Password] variables in Camera Actions" +
    ".";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(9, 375);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(141, 17);
            this.label18.TabIndex = 16;
            this.label18.Text = "BlueIris Server Name:";
            // 
            // dbLayoutPanel5
            // 
            this.dbLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbLayoutPanel5.ColumnCount = 2;
            this.dbLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.28505F));
            this.dbLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.71495F));
            this.dbLayoutPanel5.Controls.Add(this.tb_BlueIrisServer, 0, 0);
            this.dbLayoutPanel5.Controls.Add(this.label19, 1, 0);
            this.dbLayoutPanel5.Location = new System.Drawing.Point(156, 360);
            this.dbLayoutPanel5.Name = "dbLayoutPanel5";
            this.dbLayoutPanel5.RowCount = 1;
            this.dbLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbLayoutPanel5.Size = new System.Drawing.Size(856, 48);
            this.dbLayoutPanel5.TabIndex = 22;
            // 
            // tb_BlueIrisServer
            // 
            this.tb_BlueIrisServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_BlueIrisServer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_BlueIrisServer.Location = new System.Drawing.Point(3, 11);
            this.tb_BlueIrisServer.Name = "tb_BlueIrisServer";
            this.tb_BlueIrisServer.Size = new System.Drawing.Size(219, 25);
            this.tb_BlueIrisServer.TabIndex = 19;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(228, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(625, 48);
            this.label19.TabIndex = 3;
            this.label19.Text = resources.GetString("label19.Text");
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(33, 169);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(116, 17);
            this.label29.TabIndex = 6;
            this.label29.Text = "Pushover API Key";
            // 
            // dbLayoutPanel8
            // 
            this.dbLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbLayoutPanel8.ColumnCount = 5;
            this.dbLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.75058F));
            this.dbLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.391608F));
            this.dbLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.81818F));
            this.dbLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.28671F));
            this.dbLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.dbLayoutPanel8.Controls.Add(this.tb_Pushover_Cooldown, 4, 0);
            this.dbLayoutPanel8.Controls.Add(this.tb_Pushover_APIKey, 0, 0);
            this.dbLayoutPanel8.Controls.Add(this.label31, 3, 0);
            this.dbLayoutPanel8.Controls.Add(this.label30, 1, 0);
            this.dbLayoutPanel8.Controls.Add(this.tb_Pushover_UserKey, 2, 0);
            this.dbLayoutPanel8.Location = new System.Drawing.Point(155, 155);
            this.dbLayoutPanel8.Margin = new System.Windows.Forms.Padding(2);
            this.dbLayoutPanel8.Name = "dbLayoutPanel8";
            this.dbLayoutPanel8.RowCount = 1;
            this.dbLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbLayoutPanel8.Size = new System.Drawing.Size(858, 46);
            this.dbLayoutPanel8.TabIndex = 23;
            // 
            // tb_Pushover_Cooldown
            // 
            this.tb_Pushover_Cooldown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Pushover_Cooldown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Pushover_Cooldown.Location = new System.Drawing.Point(743, 10);
            this.tb_Pushover_Cooldown.Name = "tb_Pushover_Cooldown";
            this.tb_Pushover_Cooldown.Size = new System.Drawing.Size(112, 25);
            this.tb_Pushover_Cooldown.TabIndex = 10;
            // 
            // tb_Pushover_APIKey
            // 
            this.tb_Pushover_APIKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Pushover_APIKey.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Pushover_APIKey.Location = new System.Drawing.Point(3, 10);
            this.tb_Pushover_APIKey.Name = "tb_Pushover_APIKey";
            this.tb_Pushover_APIKey.Size = new System.Drawing.Size(275, 25);
            this.tb_Pushover_APIKey.TabIndex = 8;
            // 
            // label31
            // 
            this.label31.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(637, 14);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(100, 17);
            this.label31.TabIndex = 11;
            this.label31.Text = "Cooldown Secs";
            // 
            // label30
            // 
            this.label30.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(285, 14);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(65, 17);
            this.label30.TabIndex = 7;
            this.label30.Text = "User Key:";
            // 
            // tb_Pushover_UserKey
            // 
            this.tb_Pushover_UserKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Pushover_UserKey.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Pushover_UserKey.Location = new System.Drawing.Point(356, 10);
            this.tb_Pushover_UserKey.Name = "tb_Pushover_UserKey";
            this.tb_Pushover_UserKey.Size = new System.Drawing.Size(267, 25);
            this.tb_Pushover_UserKey.TabIndex = 9;
            // 
            // dbLayoutPanel9
            // 
            this.dbLayoutPanel9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbLayoutPanel9.ColumnCount = 3;
            this.dbLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.82635F));
            this.dbLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.17365F));
            this.dbLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.dbLayoutPanel9.Controls.Add(this.cbStartWithWindows, 0, 0);
            this.dbLayoutPanel9.Controls.Add(this.cbMinimizeToTray, 1, 0);
            this.dbLayoutPanel9.Location = new System.Drawing.Point(156, 258);
            this.dbLayoutPanel9.Name = "dbLayoutPanel9";
            this.dbLayoutPanel9.RowCount = 1;
            this.dbLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dbLayoutPanel9.Size = new System.Drawing.Size(856, 44);
            this.dbLayoutPanel9.TabIndex = 24;
            // 
            // cbStartWithWindows
            // 
            this.cbStartWithWindows.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbStartWithWindows.AutoSize = true;
            this.cbStartWithWindows.Location = new System.Drawing.Point(2, 13);
            this.cbStartWithWindows.Margin = new System.Windows.Forms.Padding(2);
            this.cbStartWithWindows.Name = "cbStartWithWindows";
            this.cbStartWithWindows.Size = new System.Drawing.Size(182, 17);
            this.cbStartWithWindows.TabIndex = 15;
            this.cbStartWithWindows.Text = "Start with user login (non-service)";
            this.cbStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // cbMinimizeToTray
            // 
            this.cbMinimizeToTray.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbMinimizeToTray.AutoSize = true;
            this.cbMinimizeToTray.Location = new System.Drawing.Point(217, 13);
            this.cbMinimizeToTray.Name = "cbMinimizeToTray";
            this.cbMinimizeToTray.Size = new System.Drawing.Size(102, 17);
            this.cbMinimizeToTray.TabIndex = 16;
            this.cbMinimizeToTray.Text = "Minimize to Tray";
            this.cbMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(59, 271);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 17);
            this.label13.TabIndex = 13;
            this.label13.Text = "Misc Settings";
            // 
            // dbLayoutPanel10
            // 
            this.dbLayoutPanel10.ColumnCount = 2;
            this.dbLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dbLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dbLayoutPanel10.Controls.Add(this.BtnSettingsSave, 1, 0);
            this.dbLayoutPanel10.Controls.Add(this.button3, 0, 0);
            this.dbLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbLayoutPanel10.Location = new System.Drawing.Point(3, 421);
            this.dbLayoutPanel10.Name = "dbLayoutPanel10";
            this.dbLayoutPanel10.RowCount = 1;
            this.dbLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dbLayoutPanel10.Size = new System.Drawing.Size(1016, 34);
            this.dbLayoutPanel10.TabIndex = 4;
            // 
            // BtnSettingsSave
            // 
            this.BtnSettingsSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.BtnSettingsSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSettingsSave.Location = new System.Drawing.Point(727, 3);
            this.BtnSettingsSave.Name = "BtnSettingsSave";
            this.BtnSettingsSave.Size = new System.Drawing.Size(69, 28);
            this.BtnSettingsSave.TabIndex = 21;
            this.BtnSettingsSave.Text = "Save";
            this.BtnSettingsSave.UseVisualStyleBackColor = true;
            this.BtnSettingsSave.Click += new System.EventHandler(this.BtnSettingsSave_Click_1);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(219, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 28);
            this.button3.TabIndex = 20;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabDeepStack
            // 
            this.tabDeepStack.Controls.Add(this.label34);
            this.tabDeepStack.Controls.Add(this.label33);
            this.tabDeepStack.Controls.Add(this.label32);
            this.tabDeepStack.Controls.Add(this.txt_DeepstackNoMoreOftenThanMins);
            this.tabDeepStack.Controls.Add(this.txt_DeepstackRestartFailCount);
            this.tabDeepStack.Controls.Add(this.Chk_AutoReStart);
            this.tabDeepStack.Controls.Add(this.Btn_ViewLog);
            this.tabDeepStack.Controls.Add(this.Btn_DeepstackReset);
            this.tabDeepStack.Controls.Add(this.linkLabel1);
            this.tabDeepStack.Controls.Add(this.groupBox11);
            this.tabDeepStack.Controls.Add(this.groupBox10);
            this.tabDeepStack.Controls.Add(this.lbl_DeepstackType);
            this.tabDeepStack.Controls.Add(this.lbl_Deepstackversion);
            this.tabDeepStack.Controls.Add(this.lbl_deepstackname);
            this.tabDeepStack.Controls.Add(this.label28);
            this.tabDeepStack.Controls.Add(this.label24);
            this.tabDeepStack.Controls.Add(this.label23);
            this.tabDeepStack.Controls.Add(this.label22);
            this.tabDeepStack.Controls.Add(this.chk_stopbeforestart);
            this.tabDeepStack.Controls.Add(this.chk_HighPriority);
            this.tabDeepStack.Controls.Add(this.Chk_DSDebug);
            this.tabDeepStack.Controls.Add(this.Lbl_BlueStackRunning);
            this.tabDeepStack.Controls.Add(this.Btn_Save);
            this.tabDeepStack.Controls.Add(this.label11);
            this.tabDeepStack.Controls.Add(this.groupBox1);
            this.tabDeepStack.Controls.Add(this.groupBox2);
            this.tabDeepStack.Controls.Add(this.groupBox4);
            this.tabDeepStack.Controls.Add(this.groupBox3);
            this.tabDeepStack.Controls.Add(this.Chk_AutoStart);
            this.tabDeepStack.Controls.Add(this.Btn_Start);
            this.tabDeepStack.Controls.Add(this.Btn_Stop);
            this.tabDeepStack.Controls.Add(this.groupBoxCustomModel);
            this.tabDeepStack.Location = new System.Drawing.Point(4, 22);
            this.tabDeepStack.Margin = new System.Windows.Forms.Padding(2);
            this.tabDeepStack.Name = "tabDeepStack";
            this.tabDeepStack.Size = new System.Drawing.Size(1020, 458);
            this.tabDeepStack.TabIndex = 6;
            this.tabDeepStack.Text = "DeepStack";
            this.tabDeepStack.UseVisualStyleBackColor = true;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(463, 361);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(29, 13);
            this.label34.TabIndex = 26;
            this.label34.Text = "Mins";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(311, 362);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(98, 13);
            this.label33.TabIndex = 25;
            this.label33.Text = "No more often than";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(176, 362);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(111, 13);
            this.label32.TabIndex = 24;
            this.label32.Text = "URL Failures in a row.";
            // 
            // txt_DeepstackNoMoreOftenThanMins
            // 
            this.txt_DeepstackNoMoreOftenThanMins.Location = new System.Drawing.Point(415, 358);
            this.txt_DeepstackNoMoreOftenThanMins.Name = "txt_DeepstackNoMoreOftenThanMins";
            this.txt_DeepstackNoMoreOftenThanMins.Size = new System.Drawing.Size(42, 20);
            this.txt_DeepstackNoMoreOftenThanMins.TabIndex = 17;
            // 
            // txt_DeepstackRestartFailCount
            // 
            this.txt_DeepstackRestartFailCount.Location = new System.Drawing.Point(128, 358);
            this.txt_DeepstackRestartFailCount.Name = "txt_DeepstackRestartFailCount";
            this.txt_DeepstackRestartFailCount.Size = new System.Drawing.Size(42, 20);
            this.txt_DeepstackRestartFailCount.TabIndex = 16;
            // 
            // Chk_AutoReStart
            // 
            this.Chk_AutoReStart.AutoSize = true;
            this.Chk_AutoReStart.Location = new System.Drawing.Point(13, 361);
            this.Chk_AutoReStart.Name = "Chk_AutoReStart";
            this.Chk_AutoReStart.Size = new System.Drawing.Size(109, 17);
            this.Chk_AutoReStart.TabIndex = 15;
            this.Chk_AutoReStart.Text = "Auto Restart after";
            this.Chk_AutoReStart.UseVisualStyleBackColor = true;
            // 
            // Btn_ViewLog
            // 
            this.Btn_ViewLog.ForeColor = System.Drawing.Color.Maroon;
            this.Btn_ViewLog.Location = new System.Drawing.Point(363, 388);
            this.Btn_ViewLog.Name = "Btn_ViewLog";
            this.Btn_ViewLog.Size = new System.Drawing.Size(70, 30);
            this.Btn_ViewLog.TabIndex = 22;
            this.Btn_ViewLog.Text = "stderr.txt";
            this.toolTip1.SetToolTip(this.Btn_ViewLog, "Open STDERR.TXT which contains any errors deepstack may have had.");
            this.Btn_ViewLog.UseVisualStyleBackColor = true;
            this.Btn_ViewLog.Click += new System.EventHandler(this.Btn_ViewLog_Click);
            // 
            // Btn_DeepstackReset
            // 
            this.Btn_DeepstackReset.Location = new System.Drawing.Point(275, 388);
            this.Btn_DeepstackReset.Name = "Btn_DeepstackReset";
            this.Btn_DeepstackReset.Size = new System.Drawing.Size(70, 30);
            this.Btn_DeepstackReset.TabIndex = 21;
            this.Btn_DeepstackReset.Text = "Reset";
            this.toolTip1.SetToolTip(this.Btn_DeepstackReset, "Delete all Deepstack temp files");
            this.Btn_DeepstackReset.UseVisualStyleBackColor = true;
            this.Btn_DeepstackReset.Click += new System.EventHandler(this.bt_DeepstackReset_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.Location = new System.Drawing.Point(0, 26);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(1020, 13);
            this.linkLabel1.TabIndex = 19;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://docs.deepstack.cc/windows/index.html";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.Controls.Add(this.tb_DeepStackURLs);
            this.groupBox11.Location = new System.Drawing.Point(511, 300);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(502, 118);
            this.groupBox11.TabIndex = 18;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "URL";
            // 
            // tb_DeepStackURLs
            // 
            this.tb_DeepStackURLs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_DeepStackURLs.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_DeepStackURLs.Location = new System.Drawing.Point(3, 16);
            this.tb_DeepStackURLs.Multiline = true;
            this.tb_DeepStackURLs.Name = "tb_DeepStackURLs";
            this.tb_DeepStackURLs.ReadOnly = true;
            this.tb_DeepStackURLs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_DeepStackURLs.Size = new System.Drawing.Size(496, 99);
            this.tb_DeepStackURLs.TabIndex = 0;
            this.tb_DeepStackURLs.WordWrap = false;
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox10.Controls.Add(this.tb_DeepstackCommandLine);
            this.groupBox10.Location = new System.Drawing.Point(510, 167);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(502, 118);
            this.groupBox10.TabIndex = 18;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Command line";
            // 
            // tb_DeepstackCommandLine
            // 
            this.tb_DeepstackCommandLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_DeepstackCommandLine.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_DeepstackCommandLine.Location = new System.Drawing.Point(3, 16);
            this.tb_DeepstackCommandLine.Multiline = true;
            this.tb_DeepstackCommandLine.Name = "tb_DeepstackCommandLine";
            this.tb_DeepstackCommandLine.ReadOnly = true;
            this.tb_DeepstackCommandLine.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_DeepstackCommandLine.Size = new System.Drawing.Size(496, 99);
            this.tb_DeepstackCommandLine.TabIndex = 0;
            this.tb_DeepstackCommandLine.WordWrap = false;
            // 
            // lbl_DeepstackType
            // 
            this.lbl_DeepstackType.AutoSize = true;
            this.lbl_DeepstackType.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbl_DeepstackType.Location = new System.Drawing.Point(558, 96);
            this.lbl_DeepstackType.Name = "lbl_DeepstackType";
            this.lbl_DeepstackType.Size = new System.Drawing.Size(10, 13);
            this.lbl_DeepstackType.TabIndex = 16;
            this.lbl_DeepstackType.Text = ".";
            // 
            // lbl_Deepstackversion
            // 
            this.lbl_Deepstackversion.AutoSize = true;
            this.lbl_Deepstackversion.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbl_Deepstackversion.Location = new System.Drawing.Point(558, 75);
            this.lbl_Deepstackversion.Name = "lbl_Deepstackversion";
            this.lbl_Deepstackversion.Size = new System.Drawing.Size(10, 13);
            this.lbl_Deepstackversion.TabIndex = 16;
            this.lbl_Deepstackversion.Text = ".";
            // 
            // lbl_deepstackname
            // 
            this.lbl_deepstackname.AutoSize = true;
            this.lbl_deepstackname.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbl_deepstackname.Location = new System.Drawing.Point(558, 56);
            this.lbl_deepstackname.Name = "lbl_deepstackname";
            this.lbl_deepstackname.Size = new System.Drawing.Size(10, 13);
            this.lbl_deepstackname.TabIndex = 16;
            this.lbl_deepstackname.Text = ".";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(512, 115);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(40, 13);
            this.label28.TabIndex = 16;
            this.label28.Text = "Status:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(518, 96);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(34, 13);
            this.label24.TabIndex = 16;
            this.label24.Text = "Type:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(507, 75);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(45, 13);
            this.label23.TabIndex = 16;
            this.label23.Text = "Version:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(514, 56);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(38, 13);
            this.label22.TabIndex = 16;
            this.label22.Text = "Name:";
            // 
            // chk_stopbeforestart
            // 
            this.chk_stopbeforestart.AutoSize = true;
            this.chk_stopbeforestart.Location = new System.Drawing.Point(360, 333);
            this.chk_stopbeforestart.Margin = new System.Windows.Forms.Padding(2);
            this.chk_stopbeforestart.Name = "chk_stopbeforestart";
            this.chk_stopbeforestart.Size = new System.Drawing.Size(138, 17);
            this.chk_stopbeforestart.TabIndex = 14;
            this.chk_stopbeforestart.Text = "Always stop before start";
            this.toolTip1.SetToolTip(this.chk_stopbeforestart, "If deepstack exe files are running when a START is requested, stop them first.");
            this.chk_stopbeforestart.UseVisualStyleBackColor = true;
            // 
            // chk_HighPriority
            // 
            this.chk_HighPriority.AutoSize = true;
            this.chk_HighPriority.Location = new System.Drawing.Point(232, 333);
            this.chk_HighPriority.Margin = new System.Windows.Forms.Padding(2);
            this.chk_HighPriority.Name = "chk_HighPriority";
            this.chk_HighPriority.Size = new System.Drawing.Size(102, 17);
            this.chk_HighPriority.TabIndex = 13;
            this.chk_HighPriority.Text = "Run high priority";
            this.chk_HighPriority.UseVisualStyleBackColor = true;
            // 
            // Chk_DSDebug
            // 
            this.Chk_DSDebug.AutoSize = true;
            this.Chk_DSDebug.Location = new System.Drawing.Point(146, 333);
            this.Chk_DSDebug.Margin = new System.Windows.Forms.Padding(2);
            this.Chk_DSDebug.Name = "Chk_DSDebug";
            this.Chk_DSDebug.Size = new System.Drawing.Size(58, 17);
            this.Chk_DSDebug.TabIndex = 12;
            this.Chk_DSDebug.Text = "Debug";
            this.toolTip1.SetToolTip(this.Chk_DSDebug, "Show all output from Deepstack\'s python.exe, redis.exe and server.exe  (Windows v" +
        "ersion, installed on same machine)");
            this.Chk_DSDebug.UseVisualStyleBackColor = true;
            // 
            // Lbl_BlueStackRunning
            // 
            this.Lbl_BlueStackRunning.AutoSize = true;
            this.Lbl_BlueStackRunning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_BlueStackRunning.Location = new System.Drawing.Point(558, 115);
            this.Lbl_BlueStackRunning.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lbl_BlueStackRunning.Name = "Lbl_BlueStackRunning";
            this.Lbl_BlueStackRunning.Size = new System.Drawing.Size(105, 13);
            this.Lbl_BlueStackRunning.TabIndex = 13;
            this.Lbl_BlueStackRunning.Text = "*NOT RUNNING*";
            // 
            // Btn_Save
            // 
            this.Btn_Save.Location = new System.Drawing.Point(187, 388);
            this.Btn_Save.Margin = new System.Windows.Forms.Padding(2);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(70, 30);
            this.Btn_Save.TabIndex = 20;
            this.Btn_Save.Text = "Save";
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.BackColor = System.Drawing.SystemColors.Info;
            this.label11.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label11.Location = new System.Drawing.Point(6, 4);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1007, 22);
            this.label11.TabIndex = 0;
            this.label11.Text = "This tab is only for the WINDOWS version of Deepstack";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Chk_DetectionAPI);
            this.groupBox1.Controls.Add(this.Chk_FaceAPI);
            this.groupBox1.Controls.Add(this.Chk_SceneAPI);
            this.groupBox1.Location = new System.Drawing.Point(11, 230);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(162, 88);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "API";
            // 
            // Chk_DetectionAPI
            // 
            this.Chk_DetectionAPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_DetectionAPI.Location = new System.Drawing.Point(11, 63);
            this.Chk_DetectionAPI.Margin = new System.Windows.Forms.Padding(2);
            this.Chk_DetectionAPI.Name = "Chk_DetectionAPI";
            this.Chk_DetectionAPI.Size = new System.Drawing.Size(147, 19);
            this.Chk_DetectionAPI.TabIndex = 6;
            this.Chk_DetectionAPI.Text = "Detection API";
            this.Chk_DetectionAPI.UseVisualStyleBackColor = true;
            // 
            // Chk_FaceAPI
            // 
            this.Chk_FaceAPI.Location = new System.Drawing.Point(11, 40);
            this.Chk_FaceAPI.Margin = new System.Windows.Forms.Padding(2);
            this.Chk_FaceAPI.Name = "Chk_FaceAPI";
            this.Chk_FaceAPI.Size = new System.Drawing.Size(147, 19);
            this.Chk_FaceAPI.TabIndex = 5;
            this.Chk_FaceAPI.Text = "Face API";
            this.Chk_FaceAPI.UseVisualStyleBackColor = true;
            // 
            // Chk_SceneAPI
            // 
            this.Chk_SceneAPI.Location = new System.Drawing.Point(11, 17);
            this.Chk_SceneAPI.Margin = new System.Windows.Forms.Padding(2);
            this.Chk_SceneAPI.Name = "Chk_SceneAPI";
            this.Chk_SceneAPI.Size = new System.Drawing.Size(147, 19);
            this.Chk_SceneAPI.TabIndex = 4;
            this.Chk_SceneAPI.Text = "Scene API";
            this.Chk_SceneAPI.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RB_High);
            this.groupBox2.Controls.Add(this.RB_Medium);
            this.groupBox2.Controls.Add(this.RB_Low);
            this.groupBox2.Location = new System.Drawing.Point(178, 230);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(154, 88);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mode";
            // 
            // RB_High
            // 
            this.RB_High.Location = new System.Drawing.Point(11, 63);
            this.RB_High.Margin = new System.Windows.Forms.Padding(2);
            this.RB_High.Name = "RB_High";
            this.RB_High.Size = new System.Drawing.Size(139, 19);
            this.RB_High.TabIndex = 9;
            this.RB_High.TabStop = true;
            this.RB_High.Text = "High";
            this.RB_High.UseVisualStyleBackColor = true;
            // 
            // RB_Medium
            // 
            this.RB_Medium.Location = new System.Drawing.Point(11, 40);
            this.RB_Medium.Margin = new System.Windows.Forms.Padding(2);
            this.RB_Medium.Name = "RB_Medium";
            this.RB_Medium.Size = new System.Drawing.Size(139, 19);
            this.RB_Medium.TabIndex = 8;
            this.RB_Medium.TabStop = true;
            this.RB_Medium.Text = "Medium";
            this.RB_Medium.UseVisualStyleBackColor = true;
            // 
            // RB_Low
            // 
            this.RB_Low.Location = new System.Drawing.Point(11, 17);
            this.RB_Low.Margin = new System.Windows.Forms.Padding(2);
            this.RB_Low.Name = "RB_Low";
            this.RB_Low.Size = new System.Drawing.Size(139, 19);
            this.RB_Low.TabIndex = 7;
            this.RB_Low.TabStop = true;
            this.RB_Low.Text = "Low";
            this.RB_Low.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Txt_DeepStackInstallFolder);
            this.groupBox4.Location = new System.Drawing.Point(11, 47);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(483, 45);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DeepStack Install Folder";
            // 
            // Txt_DeepStackInstallFolder
            // 
            this.Txt_DeepStackInstallFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_DeepStackInstallFolder.Location = new System.Drawing.Point(7, 17);
            this.Txt_DeepStackInstallFolder.Margin = new System.Windows.Forms.Padding(2);
            this.Txt_DeepStackInstallFolder.Name = "Txt_DeepStackInstallFolder";
            this.Txt_DeepStackInstallFolder.ReadOnly = true;
            this.Txt_DeepStackInstallFolder.Size = new System.Drawing.Size(472, 20);
            this.Txt_DeepStackInstallFolder.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.Txt_Port);
            this.groupBox3.Location = new System.Drawing.Point(345, 230);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(151, 88);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Port(s)";
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.DarkGray;
            this.label27.Location = new System.Drawing.Point(5, 41);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(142, 45);
            this.label27.TabIndex = 1;
            this.label27.Text = "Enter one or more ports with commas between to start more than one instance.";
            // 
            // Txt_Port
            // 
            this.Txt_Port.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Port.Location = new System.Drawing.Point(10, 19);
            this.Txt_Port.Margin = new System.Windows.Forms.Padding(2);
            this.Txt_Port.Name = "Txt_Port";
            this.Txt_Port.Size = new System.Drawing.Size(127, 20);
            this.Txt_Port.TabIndex = 10;
            this.toolTip1.SetToolTip(this.Txt_Port, resources.GetString("Txt_Port.ToolTip"));
            // 
            // Chk_AutoStart
            // 
            this.Chk_AutoStart.AutoSize = true;
            this.Chk_AutoStart.Location = new System.Drawing.Point(13, 333);
            this.Chk_AutoStart.Margin = new System.Windows.Forms.Padding(2);
            this.Chk_AutoStart.Name = "Chk_AutoStart";
            this.Chk_AutoStart.Size = new System.Drawing.Size(113, 17);
            this.Chk_AutoStart.TabIndex = 11;
            this.Chk_AutoStart.Text = "Automatically Start";
            this.Chk_AutoStart.UseVisualStyleBackColor = true;
            // 
            // Btn_Start
            // 
            this.Btn_Start.Location = new System.Drawing.Point(11, 388);
            this.Btn_Start.Margin = new System.Windows.Forms.Padding(2);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(70, 30);
            this.Btn_Start.TabIndex = 18;
            this.Btn_Start.Text = "Start";
            this.Btn_Start.UseVisualStyleBackColor = true;
            this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // Btn_Stop
            // 
            this.Btn_Stop.Location = new System.Drawing.Point(99, 388);
            this.Btn_Stop.Margin = new System.Windows.Forms.Padding(2);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(70, 30);
            this.Btn_Stop.TabIndex = 19;
            this.Btn_Stop.Text = "Stop";
            this.Btn_Stop.UseVisualStyleBackColor = true;
            this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // groupBoxCustomModel
            // 
            this.groupBoxCustomModel.Controls.Add(this.Chk_CustomModelAPI);
            this.groupBoxCustomModel.Controls.Add(this.label38);
            this.groupBoxCustomModel.Controls.Add(this.label37);
            this.groupBoxCustomModel.Controls.Add(this.label36);
            this.groupBoxCustomModel.Controls.Add(this.label35);
            this.groupBoxCustomModel.Controls.Add(this.Txt_CustomModelPort);
            this.groupBoxCustomModel.Controls.Add(this.Txt_CustomModelName);
            this.groupBoxCustomModel.Controls.Add(this.Txt_CustomModelPath);
            this.groupBoxCustomModel.Location = new System.Drawing.Point(11, 96);
            this.groupBoxCustomModel.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxCustomModel.Name = "groupBoxCustomModel";
            this.groupBoxCustomModel.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxCustomModel.Size = new System.Drawing.Size(483, 130);
            this.groupBoxCustomModel.TabIndex = 17;
            this.groupBoxCustomModel.TabStop = false;
            // 
            // Chk_CustomModelAPI
            // 
            this.Chk_CustomModelAPI.AutoSize = true;
            this.Chk_CustomModelAPI.BackColor = System.Drawing.SystemColors.Control;
            this.Chk_CustomModelAPI.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Chk_CustomModelAPI.Location = new System.Drawing.Point(7, -2);
            this.Chk_CustomModelAPI.Name = "Chk_CustomModelAPI";
            this.Chk_CustomModelAPI.Size = new System.Drawing.Size(99, 18);
            this.Chk_CustomModelAPI.TabIndex = 0;
            this.Chk_CustomModelAPI.Text = "Custom Model";
            this.Chk_CustomModelAPI.UseVisualStyleBackColor = false;
            this.Chk_CustomModelAPI.CheckedChanged += new System.EventHandler(this.Chk_CustomModelAPI_CheckedChanged);
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.DarkGray;
            this.label38.Location = new System.Drawing.Point(8, 88);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(470, 39);
            this.label38.TabIndex = 2;
            this.label38.Text = "If needed, specify more than one custom model instance by supplying an equal numb" +
    "er of items for PATH/NAME/PORT.    For example PATH=C:\\PTH1, C:\\PTH2, NAME=DOGS," +
    "CATS, PORT=82,83";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(11, 70);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(40, 13);
            this.label37.TabIndex = 1;
            this.label37.Text = "Port(s):";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(2, 46);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(49, 13);
            this.label36.TabIndex = 1;
            this.label36.Text = "Name(s):";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(8, 21);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(43, 13);
            this.label35.TabIndex = 1;
            this.label35.Text = "Path(s):";
            // 
            // Txt_CustomModelPort
            // 
            this.Txt_CustomModelPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_CustomModelPort.Location = new System.Drawing.Point(56, 67);
            this.Txt_CustomModelPort.Margin = new System.Windows.Forms.Padding(2);
            this.Txt_CustomModelPort.Name = "Txt_CustomModelPort";
            this.Txt_CustomModelPort.Size = new System.Drawing.Size(423, 20);
            this.Txt_CustomModelPort.TabIndex = 3;
            this.Txt_CustomModelPort.TextChanged += new System.EventHandler(this.Txt_CustomModelName_TextChanged);
            // 
            // Txt_CustomModelName
            // 
            this.Txt_CustomModelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_CustomModelName.Location = new System.Drawing.Point(56, 43);
            this.Txt_CustomModelName.Margin = new System.Windows.Forms.Padding(2);
            this.Txt_CustomModelName.Name = "Txt_CustomModelName";
            this.Txt_CustomModelName.Size = new System.Drawing.Size(423, 20);
            this.Txt_CustomModelName.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Txt_CustomModelName, "The custom model name");
            this.Txt_CustomModelName.TextChanged += new System.EventHandler(this.Txt_CustomModelName_TextChanged);
            // 
            // Txt_CustomModelPath
            // 
            this.Txt_CustomModelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_CustomModelPath.Location = new System.Drawing.Point(56, 18);
            this.Txt_CustomModelPath.Margin = new System.Windows.Forms.Padding(2);
            this.Txt_CustomModelPath.Name = "Txt_CustomModelPath";
            this.Txt_CustomModelPath.Size = new System.Drawing.Size(423, 20);
            this.Txt_CustomModelPath.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Txt_CustomModelPath, "The custom model path not including filename");
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.toolStrip2);
            this.tabLog.Controls.Add(this.groupBox7);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Margin = new System.Windows.Forms.Padding(2);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(1020, 458);
            this.tabLog.TabIndex = 7;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            this.tabLog.Click += new System.EventHandler(this.tabLog_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.ToolStripComboBoxSearch,
            this.toolStripDropDownButton1,
            this.toolStripSeparator3,
            this.toolStripSeparator,
            this.toolStripDropDownButtonSettings,
            this.toolStripSeparator4,
            this.toolStripSeparator2,
            this.openToolStripButton,
            this.toolStripButtonLoad,
            this.toolStripSeparator7,
            this.toolStripButtonReload,
            this.toolStripSeparator5,
            this.toolStripButtonPauseLog,
            this.toolStripSeparator6,
            this.toolStripSeparator8,
            this.chk_filterErrors,
            this.chk_filterErrorsAll});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1020, 31);
            this.toolStrip2.TabIndex = 6;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(45, 28);
            this.toolStripLabel1.Text = "Search:";
            // 
            // ToolStripComboBoxSearch
            // 
            this.ToolStripComboBoxSearch.BackColor = System.Drawing.SystemColors.Info;
            this.ToolStripComboBoxSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ToolStripComboBoxSearch.Items.AddRange(new object[] {
            "person",
            "error|warn|fatal|problem|exception",
            "this | orthat",
            "imagefilename.jpg | key=1234"});
            this.ToolStripComboBoxSearch.Name = "ToolStripComboBoxSearch";
            this.ToolStripComboBoxSearch.Size = new System.Drawing.Size(200, 31);
            this.ToolStripComboBoxSearch.ToolTipText = "The search can be normal text OR a valid \'RegEx\' statement.\r\n";
            this.ToolStripComboBoxSearch.Leave += new System.EventHandler(this.ToolStripComboBoxSearch_Leave);
            this.ToolStripComboBoxSearch.TextChanged += new System.EventHandler(this.ToolStripComboBoxSearch_TextChanged);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Filter,
            this.mnu_Highlight});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(37, 28);
            this.toolStripDropDownButton1.Text = "Filter or highlight search box";
            // 
            // mnu_Filter
            // 
            this.mnu_Filter.CheckOnClick = true;
            this.mnu_Filter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnu_Filter.Name = "mnu_Filter";
            this.mnu_Filter.Size = new System.Drawing.Size(124, 22);
            this.mnu_Filter.Text = "Filter";
            this.mnu_Filter.CheckStateChanged += new System.EventHandler(this.mnu_Filter_CheckStateChanged);
            this.mnu_Filter.Click += new System.EventHandler(this.mnu_Filter_Click);
            // 
            // mnu_Highlight
            // 
            this.mnu_Highlight.CheckOnClick = true;
            this.mnu_Highlight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnu_Highlight.Name = "mnu_Highlight";
            this.mnu_Highlight.Size = new System.Drawing.Size(124, 22);
            this.mnu_Highlight.Text = "Highlight";
            this.mnu_Highlight.CheckStateChanged += new System.EventHandler(this.mnu_highlight_CheckStateChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripDropDownButtonSettings
            // 
            this.toolStripDropDownButtonSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Chk_AutoScroll,
            this.clearRecentErrorsToolStripMenuItem,
            this.toolStripMenuItemLogLevel});
            this.toolStripDropDownButtonSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonSettings.Image")));
            this.toolStripDropDownButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonSettings.Name = "toolStripDropDownButtonSettings";
            this.toolStripDropDownButtonSettings.Size = new System.Drawing.Size(109, 28);
            this.toolStripDropDownButtonSettings.Text = "Log Settings";
            // 
            // Chk_AutoScroll
            // 
            this.Chk_AutoScroll.CheckOnClick = true;
            this.Chk_AutoScroll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Chk_AutoScroll.Name = "Chk_AutoScroll";
            this.Chk_AutoScroll.Size = new System.Drawing.Size(204, 22);
            this.Chk_AutoScroll.Text = "Auto Scroll";
            this.Chk_AutoScroll.Click += new System.EventHandler(this.Chk_AutoScroll_Click_1);
            // 
            // clearRecentErrorsToolStripMenuItem
            // 
            this.clearRecentErrorsToolStripMenuItem.Name = "clearRecentErrorsToolStripMenuItem";
            this.clearRecentErrorsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.clearRecentErrorsToolStripMenuItem.Text = "Clear Recent Error Count";
            this.clearRecentErrorsToolStripMenuItem.Click += new System.EventHandler(this.clearRecentErrorsToolStripMenuItem_Click);
            // 
            // toolStripMenuItemLogLevel
            // 
            this.toolStripMenuItemLogLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_log_filter_off,
            this.mnu_log_filter_fatal,
            this.mnu_log_filter_error,
            this.mnu_log_filter_warn,
            this.mnu_log_filter_info,
            this.mnu_log_filter_debug,
            this.mnu_log_filter_trace});
            this.toolStripMenuItemLogLevel.Name = "toolStripMenuItemLogLevel";
            this.toolStripMenuItemLogLevel.Size = new System.Drawing.Size(204, 22);
            this.toolStripMenuItemLogLevel.Text = "Logging Level";
            // 
            // mnu_log_filter_off
            // 
            this.mnu_log_filter_off.CheckOnClick = true;
            this.mnu_log_filter_off.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnu_log_filter_off.Name = "mnu_log_filter_off";
            this.mnu_log_filter_off.Size = new System.Drawing.Size(109, 22);
            this.mnu_log_filter_off.Text = "Off";
            this.mnu_log_filter_off.CheckStateChanged += new System.EventHandler(this.mnu_log_filter_off_CheckStateChanged);
            this.mnu_log_filter_off.Click += new System.EventHandler(this.mnu_log_filter_off_Click);
            // 
            // mnu_log_filter_fatal
            // 
            this.mnu_log_filter_fatal.CheckOnClick = true;
            this.mnu_log_filter_fatal.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnu_log_filter_fatal.Name = "mnu_log_filter_fatal";
            this.mnu_log_filter_fatal.Size = new System.Drawing.Size(109, 22);
            this.mnu_log_filter_fatal.Text = "Fatal";
            this.mnu_log_filter_fatal.CheckStateChanged += new System.EventHandler(this.mnu_log_filter_fatal_CheckStateChanged);
            // 
            // mnu_log_filter_error
            // 
            this.mnu_log_filter_error.CheckOnClick = true;
            this.mnu_log_filter_error.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnu_log_filter_error.Name = "mnu_log_filter_error";
            this.mnu_log_filter_error.Size = new System.Drawing.Size(109, 22);
            this.mnu_log_filter_error.Text = "Error";
            this.mnu_log_filter_error.CheckStateChanged += new System.EventHandler(this.mnu_log_filter_error_CheckStateChanged);
            // 
            // mnu_log_filter_warn
            // 
            this.mnu_log_filter_warn.CheckOnClick = true;
            this.mnu_log_filter_warn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnu_log_filter_warn.Name = "mnu_log_filter_warn";
            this.mnu_log_filter_warn.Size = new System.Drawing.Size(109, 22);
            this.mnu_log_filter_warn.Text = "Warn";
            this.mnu_log_filter_warn.CheckStateChanged += new System.EventHandler(this.mnu_log_filter_warn_CheckStateChanged);
            // 
            // mnu_log_filter_info
            // 
            this.mnu_log_filter_info.CheckOnClick = true;
            this.mnu_log_filter_info.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnu_log_filter_info.Name = "mnu_log_filter_info";
            this.mnu_log_filter_info.Size = new System.Drawing.Size(109, 22);
            this.mnu_log_filter_info.Text = "Info";
            this.mnu_log_filter_info.CheckStateChanged += new System.EventHandler(this.mnu_log_filter_info_CheckStateChanged);
            // 
            // mnu_log_filter_debug
            // 
            this.mnu_log_filter_debug.CheckOnClick = true;
            this.mnu_log_filter_debug.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnu_log_filter_debug.Name = "mnu_log_filter_debug";
            this.mnu_log_filter_debug.Size = new System.Drawing.Size(109, 22);
            this.mnu_log_filter_debug.Text = "Debug";
            this.mnu_log_filter_debug.CheckStateChanged += new System.EventHandler(this.mnu_log_filter_debug_CheckStateChanged);
            // 
            // mnu_log_filter_trace
            // 
            this.mnu_log_filter_trace.CheckOnClick = true;
            this.mnu_log_filter_trace.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnu_log_filter_trace.Name = "mnu_log_filter_trace";
            this.mnu_log_filter_trace.Size = new System.Drawing.Size(109, 22);
            this.mnu_log_filter_trace.Text = "Trace";
            this.mnu_log_filter_trace.CheckStateChanged += new System.EventHandler(this.mnu_log_filter_trace_CheckStateChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(64, 28);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.ToolTipText = "Open Log File in external editor";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // toolStripButtonLoad
            // 
            this.toolStripButtonLoad.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLoad.Image")));
            this.toolStripButtonLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoad.Name = "toolStripButtonLoad";
            this.toolStripButtonLoad.Size = new System.Drawing.Size(61, 28);
            this.toolStripButtonLoad.Text = "Load";
            this.toolStripButtonLoad.ToolTipText = "Load a specific log file into the list";
            this.toolStripButtonLoad.Click += new System.EventHandler(this.toolStripButtonLoad_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonReload
            // 
            this.toolStripButtonReload.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReload.Image")));
            this.toolStripButtonReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReload.Name = "toolStripButtonReload";
            this.toolStripButtonReload.Size = new System.Drawing.Size(71, 28);
            this.toolStripButtonReload.Text = "Reload";
            this.toolStripButtonReload.ToolTipText = "Reloads the entire current log file from file without limiting the max number of " +
    "lines.  \r\nUse this after loading other files or filtering to reset view";
            this.toolStripButtonReload.Click += new System.EventHandler(this.toolStripButtonReload_ClickAsync);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonPauseLog
            // 
            this.toolStripButtonPauseLog.CheckOnClick = true;
            this.toolStripButtonPauseLog.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPauseLog.Image")));
            this.toolStripButtonPauseLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPauseLog.Name = "toolStripButtonPauseLog";
            this.toolStripButtonPauseLog.Size = new System.Drawing.Size(66, 28);
            this.toolStripButtonPauseLog.Text = "Pause";
            this.toolStripButtonPauseLog.ToolTipText = "Pause log tab auto refresh";
            this.toolStripButtonPauseLog.Click += new System.EventHandler(this.toolStripButtonPauseLog_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 31);
            // 
            // chk_filterErrors
            // 
            this.chk_filterErrors.CheckOnClick = true;
            this.chk_filterErrors.Image = ((System.Drawing.Image)(resources.GetObject("chk_filterErrors.Image")));
            this.chk_filterErrors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chk_filterErrors.Name = "chk_filterErrors";
            this.chk_filterErrors.Size = new System.Drawing.Size(65, 28);
            this.chk_filterErrors.Text = "Errors";
            this.chk_filterErrors.ToolTipText = "Show errors from current loaded log";
            this.chk_filterErrors.Click += new System.EventHandler(this.chk_filterErrors_Click_1);
            // 
            // chk_filterErrorsAll
            // 
            this.chk_filterErrorsAll.CheckOnClick = true;
            this.chk_filterErrorsAll.Image = ((System.Drawing.Image)(resources.GetObject("chk_filterErrorsAll.Image")));
            this.chk_filterErrorsAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chk_filterErrorsAll.Name = "chk_filterErrorsAll";
            this.chk_filterErrorsAll.Size = new System.Drawing.Size(90, 28);
            this.chk_filterErrorsAll.Text = "Errors (All)";
            this.chk_filterErrorsAll.ToolTipText = "Show errors from ALL logs";
            this.chk_filterErrorsAll.Click += new System.EventHandler(this.chk_filterErrorsAll_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.folv_log);
            this.groupBox7.Location = new System.Drawing.Point(5, 36);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(1015, 412);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Log";
            // 
            // folv_log
            // 
            this.folv_log.BackColor = System.Drawing.Color.Navy;
            this.folv_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folv_log.ForeColor = System.Drawing.Color.White;
            this.folv_log.HideSelection = false;
            this.folv_log.Location = new System.Drawing.Point(2, 15);
            this.folv_log.Name = "folv_log";
            this.folv_log.ShowGroups = false;
            this.folv_log.Size = new System.Drawing.Size(1011, 395);
            this.folv_log.TabIndex = 0;
            this.folv_log.UseCompatibleStateImageBehavior = false;
            this.folv_log.View = System.Windows.Forms.View.Details;
            this.folv_log.VirtualMode = true;
            this.folv_log.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.folv_log_FormatCell);
            this.folv_log.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.folv_log_FormatRow);
            // 
            // HistoryImageList
            // 
            this.HistoryImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("HistoryImageList.ImageStream")));
            this.HistoryImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.HistoryImageList.Images.SetKeyName(0, "error16");
            this.HistoryImageList.Images.SetKeyName(1, "person16");
            this.HistoryImageList.Images.SetKeyName(2, "nothing16");
            this.HistoryImageList.Images.SetKeyName(3, "detection16");
            this.HistoryImageList.Images.SetKeyName(4, "success16");
            this.HistoryImageList.Images.SetKeyName(5, "bear16");
            this.HistoryImageList.Images.SetKeyName(6, "cat16");
            this.HistoryImageList.Images.SetKeyName(7, "dog16");
            this.HistoryImageList.Images.SetKeyName(8, "horse16");
            this.HistoryImageList.Images.SetKeyName(9, "bird16");
            this.HistoryImageList.Images.SetKeyName(10, "alien16");
            this.HistoryImageList.Images.SetKeyName(11, "cow16");
            this.HistoryImageList.Images.SetKeyName(12, "car16");
            this.HistoryImageList.Images.SetKeyName(13, "truck16");
            this.HistoryImageList.Images.SetKeyName(14, "motorcycle16");
            this.HistoryImageList.Images.SetKeyName(15, "bicycle32.png");
            this.HistoryImageList.Images.SetKeyName(16, "airplane.png");
            this.HistoryImageList.Images.SetKeyName(17, "bear.png");
            this.HistoryImageList.Images.SetKeyName(18, "bicycle.png");
            this.HistoryImageList.Images.SetKeyName(19, "bird.png");
            this.HistoryImageList.Images.SetKeyName(20, "boat.png");
            this.HistoryImageList.Images.SetKeyName(21, "bus.png");
            this.HistoryImageList.Images.SetKeyName(22, "car.png");
            this.HistoryImageList.Images.SetKeyName(23, "cat.png");
            this.HistoryImageList.Images.SetKeyName(24, "cow.png");
            this.HistoryImageList.Images.SetKeyName(25, "dog.png");
            this.HistoryImageList.Images.SetKeyName(26, "horse.png");
            this.HistoryImageList.Images.SetKeyName(27, "motorcycle.png");
            this.HistoryImageList.Images.SetKeyName(28, "person.png");
            this.HistoryImageList.Images.SetKeyName(29, "sheep.png");
            this.HistoryImageList.Images.SetKeyName(30, "truck.png");
            this.HistoryImageList.Images.SetKeyName(31, "error32.png");
            this.HistoryImageList.Images.SetKeyName(32, "person32.png");
            this.HistoryImageList.Images.SetKeyName(33, "nothing32.png");
            this.HistoryImageList.Images.SetKeyName(34, "success32.png");
            this.HistoryImageList.Images.SetKeyName(35, "detection32.png");
            this.HistoryImageList.Images.SetKeyName(36, "bear32.png");
            this.HistoryImageList.Images.SetKeyName(37, "cat32.png");
            this.HistoryImageList.Images.SetKeyName(38, "dog32.png");
            this.HistoryImageList.Images.SetKeyName(39, "horse32.png");
            this.HistoryImageList.Images.SetKeyName(40, "bird32.png");
            this.HistoryImageList.Images.SetKeyName(41, "alien32.png");
            this.HistoryImageList.Images.SetKeyName(42, "cow32.png");
            this.HistoryImageList.Images.SetKeyName(43, "car32.png");
            this.HistoryImageList.Images.SetKeyName(44, "truck32.png");
            this.HistoryImageList.Images.SetKeyName(45, "motorcycle32.png");
            // 
            // HistoryUpdateListTimer
            // 
            this.HistoryUpdateListTimer.Interval = 3000;
            this.HistoryUpdateListTimer.Tick += new System.EventHandler(this.HistoryUpdateListTimer_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabelHistoryItems,
            this.toolStripStatusErrors,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 489);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 8, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1028, 24);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 27);
            this.toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabelHistoryItems
            // 
            this.toolStripStatusLabelHistoryItems.ForeColor = System.Drawing.Color.DodgerBlue;
            this.toolStripStatusLabelHistoryItems.Name = "toolStripStatusLabelHistoryItems";
            this.toolStripStatusLabelHistoryItems.Size = new System.Drawing.Size(86, 19);
            this.toolStripStatusLabelHistoryItems.Text = "0 History Items";
            // 
            // toolStripStatusErrors
            // 
            this.toolStripStatusErrors.Name = "toolStripStatusErrors";
            this.toolStripStatusErrors.Size = new System.Drawing.Size(10, 19);
            this.toolStripStatusErrors.Text = ".";
            this.toolStripStatusErrors.Click += new System.EventHandler(this.toolStripStatusErrors_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 19);
            this.toolStripStatusLabel1.Text = ".";
            // 
            // toolStripStatusLabelInfo
            // 
            this.toolStripStatusLabelInfo.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelInfo.ForeColor = System.Drawing.Color.DarkOrange;
            this.toolStripStatusLabelInfo.Name = "toolStripStatusLabelInfo";
            this.toolStripStatusLabelInfo.Size = new System.Drawing.Size(34, 19);
            this.toolStripStatusLabelInfo.Text = "Idle";
            // 
            // LogUpdateListTimer
            // 
            this.LogUpdateListTimer.Interval = 2000;
            this.LogUpdateListTimer.Tick += new System.EventHandler(this.LogUpdateListTimer_Tick);
            // 
            // Shell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 513);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(755, 440);
            this.Name = "Shell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "SAVE";
            this.Text = "AI Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Shell_FormClosing);
            this.Load += new System.EventHandler(this.Shell_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.Shell_DpiChanged);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabOverview.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabStats.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel23.ResumeLayout(false);
            this.tableLayoutPanel23.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_confidence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeline)).EndInit();
            this.tableLayoutPanel17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabHistory.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.folv_history)).EndInit();
            this.contextMenuStripHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabCameras.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Cameras)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tableLayoutPanel26.ResumeLayout(false);
            this.tableLayoutPanel26.PerformLayout();
            this.tableLayoutPanel27.ResumeLayout(false);
            this.tableLayoutPanel27.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.dbLayoutPanel6.ResumeLayout(false);
            this.dbLayoutPanel6.PerformLayout();
            this.dbLayoutPanel7.ResumeLayout(false);
            this.dbLayoutPanel7.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel18.PerformLayout();
            this.dbLayoutPanel1.ResumeLayout(false);
            this.dbLayoutPanel1.PerformLayout();
            this.dbLayoutPanel2.ResumeLayout(false);
            this.dbLayoutPanel2.PerformLayout();
            this.dbLayoutPanel3.ResumeLayout(false);
            this.dbLayoutPanel3.PerformLayout();
            this.dbLayoutPanel4.ResumeLayout(false);
            this.dbLayoutPanel4.PerformLayout();
            this.dbLayoutPanel5.ResumeLayout(false);
            this.dbLayoutPanel5.PerformLayout();
            this.dbLayoutPanel8.ResumeLayout(false);
            this.dbLayoutPanel8.PerformLayout();
            this.dbLayoutPanel9.ResumeLayout(false);
            this.dbLayoutPanel9.PerformLayout();
            this.dbLayoutPanel10.ResumeLayout(false);
            this.tabDeepStack.ResumeLayout(false);
            this.tabDeepStack.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxCustomModel.ResumeLayout(false);
            this.groupBoxCustomModel.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.folv_log)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.TabPage tabCameras;
        private System.Windows.Forms.TabPage tabSettings;
        private DBLayoutPanel tableLayoutPanel4;
        private DBLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lbl_deepstackurl;
        private System.Windows.Forms.Label lbl_input;
        private System.Windows.Forms.Button BtnSettingsSave;
        private System.Windows.Forms.Button btnCameraAdd;
        private DBLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.Label lblRelevantObjects;
        private System.Windows.Forms.Label lblName;
        private DBLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Button btnCameraSave;
        private System.Windows.Forms.Button btnCameraDel;
        private DBLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.TextBox tbPrefix;
        private System.Windows.Forms.Label lbl_prefix;
        private DBLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.CheckBox cb_enabled;
        private System.Windows.Forms.Label lbl_telegram_token;
        private System.Windows.Forms.Label lbl_telegram_chatid;
        private System.Windows.Forms.TabPage tabOverview;
        private System.Windows.Forms.Label lbl_camstats;
        private DBLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabStats;
        private DBLayoutPanel tableLayoutPanel16;
        private DBLayoutPanel tableLayoutPanel17;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.Label lbl_errors;
        private DBLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.Button btn_input_path;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_info;
        private DBLayoutPanel tableLayoutPanel23;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_confidence;
        private System.Windows.Forms.DataVisualization.Charting.Chart timeline;
        private System.Windows.Forms.Label label7;
        private DBLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.CheckBox cb_person;
        private System.Windows.Forms.CheckBox cb_bicycle;
        private System.Windows.Forms.CheckBox cb_motorcycle;
        private System.Windows.Forms.CheckBox cb_bear;
        private System.Windows.Forms.CheckBox cb_cow;
        private System.Windows.Forms.CheckBox cb_sheep;
        private System.Windows.Forms.CheckBox cb_horse;
        private System.Windows.Forms.CheckBox cb_bird;
        private System.Windows.Forms.CheckBox cb_dog;
        private System.Windows.Forms.CheckBox cb_cat;
        private System.Windows.Forms.CheckBox cb_airplane;
        private System.Windows.Forms.CheckBox cb_boat;
        private System.Windows.Forms.CheckBox cb_bus;
        private System.Windows.Forms.CheckBox cb_truck;
        private System.Windows.Forms.CheckBox cb_car;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cb_send_telegram_errors;
        private System.Windows.Forms.Label lbl_objects;
        private DBLayoutPanel tableLayoutPanel6;
        private DBLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.TabPage tabDeepStack;
        private System.Windows.Forms.CheckBox Chk_AutoStart;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox Txt_Port;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RB_High;
        private System.Windows.Forms.RadioButton RB_Medium;
        private System.Windows.Forms.RadioButton RB_Low;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox Chk_DetectionAPI;
        private System.Windows.Forms.CheckBox Chk_FaceAPI;
        private System.Windows.Forms.CheckBox Chk_SceneAPI;
        private System.Windows.Forms.TextBox Txt_DeepStackInstallFolder;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label Lbl_BlueStackRunning;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.ComboBox cmbInput;
        private System.Windows.Forms.CheckBox cbStartWithWindows;
        private System.Windows.Forms.CheckBox cb_inputpathsubfolders;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel26;
        private System.Windows.Forms.ComboBox cmbcaminput;
        private System.Windows.Forms.CheckBox cb_monitorCamInputfolder;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox Chk_DSDebug;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chk_HighPriority;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel27;
        protected internal System.Windows.Forms.CheckBox cb_masking_enabled;
        private System.Windows.Forms.Button BtnDynamicMaskingSettings;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Label lblQueue;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnCustomMask;
        private System.Windows.Forms.Label lblDrawMask;
        private System.Windows.Forms.Button btnActions;
        private DBLayoutPanel dbLayoutPanel1;
        private System.Windows.Forms.CheckBox cb_DeepStackURLsQueued;
        private DBLayoutPanel dbLayoutPanel2;
        private System.Windows.Forms.TextBox tb_telegram_cooldown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_telegram_chatid;
        private System.Windows.Forms.TextBox tb_telegram_token;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private BrightIdeasSoftware.FastObjectListView folv_history;
        private System.Windows.Forms.Button btn_resetstats;
        private System.Windows.Forms.ImageList HistoryImageList;
        private System.Windows.Forms.Timer HistoryUpdateListTimer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelHistoryItems;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusErrors;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox comboBox_filter_camera;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonFilters;
        private System.Windows.Forms.ToolStripMenuItem cb_filter_success;
        private System.Windows.Forms.ToolStripMenuItem cb_filter_nosuccess;
        private System.Windows.Forms.ToolStripMenuItem cb_filter_person;
        private System.Windows.Forms.ToolStripMenuItem cb_filter_animal;
        private System.Windows.Forms.ToolStripMenuItem cb_filter_vehicle;
        private System.Windows.Forms.ToolStripMenuItem cb_filter_skipped;
        private System.Windows.Forms.ToolStripMenuItem cb_filter_masked;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonOptions;
        private System.Windows.Forms.ToolStripMenuItem cb_showMask;
        private System.Windows.Forms.ToolStripMenuItem cb_showObjects;
        private System.Windows.Forms.ToolStripMenuItem cb_follow;
        private System.Windows.Forms.ToolStripMenuItem automaticallyRefreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelInfo;
        private System.Windows.Forms.ToolStripButton toolStripButtonDetails;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripHistory;
        private System.Windows.Forms.ToolStripMenuItem testDetectionAgainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonMaskDetails;
        private System.Windows.Forms.ToolStripMenuItem dynamicMaskDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditImageMask;
        private DBLayoutPanel dbLayoutPanel3;
        private System.Windows.Forms.Button btn_enabletelegram;
        private System.Windows.Forms.Button btn_disabletelegram;
        private System.Windows.Forms.ToolStripMenuItem storeFalseAlertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storeMaskedAlertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOnlyRelevantObjectsToolStripMenuItem;
        private System.Windows.Forms.Button btnSaveTo;
        private BrightIdeasSoftware.FastObjectListView folv_log;
        private System.Windows.Forms.Timer LogUpdateListTimer;
        private System.Windows.Forms.ToolStripMenuItem locateInLogToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox ToolStripComboBoxSearch;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem mnu_Filter;
        private System.Windows.Forms.ToolStripMenuItem mnu_Highlight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSettings;
        private System.Windows.Forms.ToolStripMenuItem Chk_AutoScroll;
        private System.Windows.Forms.ToolStripMenuItem clearRecentErrorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLogLevel;
        private System.Windows.Forms.ToolStripMenuItem mnu_log_filter_off;
        private System.Windows.Forms.ToolStripMenuItem mnu_log_filter_fatal;
        private System.Windows.Forms.ToolStripMenuItem mnu_log_filter_error;
        private System.Windows.Forms.ToolStripMenuItem mnu_log_filter_warn;
        private System.Windows.Forms.ToolStripMenuItem mnu_log_filter_info;
        private System.Windows.Forms.ToolStripMenuItem mnu_log_filter_debug;
        private System.Windows.Forms.ToolStripMenuItem mnu_log_filter_trace;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolStripButtonReload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonPauseLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoad;
        private System.Windows.Forms.ToolStripButton chk_filterErrors;
        private System.Windows.Forms.ToolStripButton chk_filterErrorsAll;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label label4;
        private DBLayoutPanel dbLayoutPanel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private DBLayoutPanel dbLayoutPanel5;
        private System.Windows.Forms.TextBox tb_BlueIrisServer;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ToolStripMenuItem manuallyAddImagesToolStripMenuItem;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbAdditionalRelevantObjects;
        private System.Windows.Forms.ToolStripMenuItem restrictThresholdAtSourceToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tb_camera_telegram_chatid;
        private System.Windows.Forms.Label lbl_Deepstackversion;
        private System.Windows.Forms.Label lbl_deepstackname;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBoxCustomModel;
        private System.Windows.Forms.TextBox Txt_CustomModelPath;
        private System.Windows.Forms.Label lbl_DeepstackType;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private DBLayoutPanel dbLayoutPanel6;
        private System.Windows.Forms.TextBox tbBiCamName;
        private System.Windows.Forms.Label label26;
        private DBLayoutPanel dbLayoutPanel7;
        private System.Windows.Forms.TextBox tbCustomMaskFile;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox tb_DeepstackCommandLine;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TextBox tb_DeepStackURLs;
        private System.Windows.Forms.Button Btn_DeepstackReset;
        private System.Windows.Forms.Button Btn_ViewLog;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private DBLayoutPanel dbLayoutPanel8;
        private System.Windows.Forms.TextBox tb_Pushover_Cooldown;
        private System.Windows.Forms.TextBox tb_Pushover_APIKey;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tb_Pushover_UserKey;
        private System.Windows.Forms.CheckBox chk_stopbeforestart;
        private System.Windows.Forms.CheckBox cb_send_pushover_errors;
        private DBLayoutPanel dbLayoutPanel9;
        private System.Windows.Forms.CheckBox cbMinimizeToTray;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txt_DeepstackRestartFailCount;
        private System.Windows.Forms.CheckBox Chk_AutoReStart;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txt_DeepstackNoMoreOftenThanMins;
        private System.Windows.Forms.TextBox Txt_CustomModelName;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox Txt_CustomModelPort;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.CheckBox Chk_CustomModelAPI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditURL;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private DBLayoutPanel dbLayoutPanel10;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripMenuItem viewImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jumpToImageToolStripMenuItem;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Button BtnPredictionSize;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private BrightIdeasSoftware.FastObjectListView FOLV_Cameras;
    }
}

