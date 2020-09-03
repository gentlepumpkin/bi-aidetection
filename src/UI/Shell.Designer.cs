using System.IO;
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel21 = new AITool.DBLayoutPanel(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel22 = new AITool.DBLayoutPanel(this.components);
            this.cb_showObjects = new System.Windows.Forms.CheckBox();
            this.cb_showMask = new System.Windows.Forms.CheckBox();
            this.lbl_objects = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel19 = new AITool.DBLayoutPanel(this.components);
            this.cb_showFilters = new System.Windows.Forms.CheckBox();
            this.list1 = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_filter_camera = new System.Windows.Forms.ComboBox();
            this.cb_filter_nosuccess = new System.Windows.Forms.CheckBox();
            this.cb_filter_success = new System.Windows.Forms.CheckBox();
            this.cb_filter_person = new System.Windows.Forms.CheckBox();
            this.cb_filter_vehicle = new System.Windows.Forms.CheckBox();
            this.cb_filter_animal = new System.Windows.Forms.CheckBox();
            this.tabCameras = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel3 = new AITool.DBLayoutPanel(this.components);
            this.list2 = new System.Windows.Forms.ListView();
            this.tableLayoutPanel6 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel7 = new AITool.DBLayoutPanel(this.components);
            this.label14 = new System.Windows.Forms.Label();
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
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel9 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel10 = new AITool.DBLayoutPanel(this.components);
            this.tbTriggerUrl = new System.Windows.Forms.TextBox();
            this.lblTriggerUrl = new System.Windows.Forms.Label();
            this.tableLayoutPanel20 = new AITool.DBLayoutPanel(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.cb_telegram = new System.Windows.Forms.CheckBox();
            this.cb_TriggerCancels = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_cooldown = new System.Windows.Forms.TextBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tableLayoutPanel12 = new AITool.DBLayoutPanel(this.components);
            this.lbl_prefix = new System.Windows.Forms.Label();
            this.tbPrefix = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel13 = new AITool.DBLayoutPanel(this.components);
            this.cb_enabled = new System.Windows.Forms.CheckBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblRelevantObjects = new System.Windows.Forms.Label();
            this.lbl_threshold = new System.Windows.Forms.Label();
            this.tableLayoutPanel24 = new AITool.DBLayoutPanel(this.components);
            this.lbl_threshold_lower = new System.Windows.Forms.Label();
            this.tb_threshold_upper = new System.Windows.Forms.TextBox();
            this.lbl_threshold_upper = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_threshold_lower = new System.Windows.Forms.TextBox();
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
            this.tableLayoutPanel11 = new AITool.DBLayoutPanel(this.components);
            this.btnCameraAdd = new System.Windows.Forms.Button();
            this.btnCameraDel = new System.Windows.Forms.Button();
            this.btnCameraSave = new System.Windows.Forms.Button();
            this.lbl_camstats = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel5 = new AITool.DBLayoutPanel(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel25 = new AITool.DBLayoutPanel(this.components);
            this.btn_open_log = new System.Windows.Forms.Button();
            this.cb_log = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cb_send_errors = new System.Windows.Forms.CheckBox();
            this.lbl_deepstackurl = new System.Windows.Forms.Label();
            this.lbl_input = new System.Windows.Forms.Label();
            this.tbDeepstackUrl = new System.Windows.Forms.TextBox();
            this.lbl_telegram_token = new System.Windows.Forms.Label();
            this.lbl_telegram_chatid = new System.Windows.Forms.Label();
            this.tb_telegram_token = new System.Windows.Forms.TextBox();
            this.tb_telegram_chatid = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel18 = new AITool.DBLayoutPanel(this.components);
            this.btn_input_path = new System.Windows.Forms.Button();
            this.cmbInput = new System.Windows.Forms.ComboBox();
            this.cb_inputpathsubfolders = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbStartWithWindows = new System.Windows.Forms.CheckBox();
            this.BtnSettingsSave = new System.Windows.Forms.Button();
            this.tabDeepStack = new System.Windows.Forms.TabPage();
            this.chk_HighPriority = new System.Windows.Forms.CheckBox();
            this.Chk_DSDebug = new System.Windows.Forms.CheckBox();
            this.Lbl_BlueStackRunning = new System.Windows.Forms.Label();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.Txt_APIKey = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Chk_DetectionAPI = new System.Windows.Forms.CheckBox();
            this.Chk_FaceAPI = new System.Windows.Forms.CheckBox();
            this.Chk_SceneAPI = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Txt_AdminKey = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RB_High = new System.Windows.Forms.RadioButton();
            this.RB_Medium = new System.Windows.Forms.RadioButton();
            this.RB_Low = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Txt_DeepStackInstallFolder = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Txt_Port = new System.Windows.Forms.TextBox();
            this.Chk_AutoStart = new System.Windows.Forms.CheckBox();
            this.Btn_Start = new System.Windows.Forms.Button();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.RTF_Log = new System.Windows.Forms.RichTextBox();
            this.Chk_AutoScroll = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel19.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabCameras.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel20.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel24.SuspendLayout();
            this.tableLayoutPanel26.SuspendLayout();
            this.tableLayoutPanel27.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel25.SuspendLayout();
            this.tableLayoutPanel18.SuspendLayout();
            this.tabDeepStack.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.groupBox7.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tabOverview);
            this.tabControl1.Controls.Add(this.tabStats);
            this.tabControl1.Controls.Add(this.tabHistory);
            this.tabControl1.Controls.Add(this.tabCameras);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Controls.Add(this.tabDeepStack);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1065, 535);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // tabOverview
            // 
            this.tabOverview.Controls.Add(this.tableLayoutPanel14);
            this.tabOverview.Location = new System.Drawing.Point(4, 22);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Size = new System.Drawing.Size(1057, 509);
            this.tabOverview.TabIndex = 4;
            this.tabOverview.Text = "Overview";
            this.tabOverview.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel14.ColumnCount = 1;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel14.Controls.Add(this.tableLayoutPanel15, 0, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(1057, 509);
            this.tableLayoutPanel14.TabIndex = 3;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 1;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Controls.Add(this.pictureBox2, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel15.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel15.Controls.Add(this.lbl_version, 0, 5);
            this.tableLayoutPanel15.Controls.Add(this.lbl_errors, 0, 3);
            this.tableLayoutPanel15.Controls.Add(this.lbl_info, 0, 5);
            this.tableLayoutPanel15.Controls.Add(this.lblQueue, 0, 4);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.tableLayoutPanel15.Size = new System.Drawing.Size(1049, 501);
            this.tableLayoutPanel15.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = global::AITool.Properties.Resources.logo;
            this.pictureBox2.Location = new System.Drawing.Point(3, 92);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1043, 131);
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
            this.label2.Location = new System.Drawing.Point(3, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1043, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Running";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(63, 226);
            this.label3.Margin = new System.Windows.Forms.Padding(63, 0, 63, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(923, 2);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // lbl_version
            // 
            this.lbl_version.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_version.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbl_version.Location = new System.Drawing.Point(3, 455);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(1043, 22);
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
            this.lbl_errors.Location = new System.Drawing.Point(3, 367);
            this.lbl_errors.Name = "lbl_errors";
            this.lbl_errors.Size = new System.Drawing.Size(1043, 66);
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
            this.lbl_info.Location = new System.Drawing.Point(3, 477);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(1043, 24);
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
            this.lblQueue.Location = new System.Drawing.Point(3, 433);
            this.lblQueue.Name = "lblQueue";
            this.lblQueue.Size = new System.Drawing.Size(1043, 22);
            this.lblQueue.TabIndex = 9;
            this.lblQueue.Text = "Images in Queue:";
            this.lblQueue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.tableLayoutPanel16);
            this.tabStats.Location = new System.Drawing.Point(4, 22);
            this.tabStats.Name = "tabStats";
            this.tabStats.Size = new System.Drawing.Size(1057, 509);
            this.tabStats.TabIndex = 5;
            this.tabStats.Text = "Stats";
            this.tabStats.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 2;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel16.Controls.Add(this.tableLayoutPanel23, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.tableLayoutPanel17, 0, 0);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 1;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(1057, 509);
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
            this.tableLayoutPanel23.Location = new System.Drawing.Point(320, 3);
            this.tableLayoutPanel23.Name = "tableLayoutPanel23";
            this.tableLayoutPanel23.RowCount = 3;
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel23.Size = new System.Drawing.Size(734, 503);
            this.tableLayoutPanel23.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 254);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(728, 29);
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
            this.chart_confidence.Location = new System.Drawing.Point(3, 289);
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
            this.chart_confidence.Size = new System.Drawing.Size(728, 211);
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
            this.timeline.Series.Add(series3);
            this.timeline.Series.Add(series4);
            this.timeline.Series.Add(series5);
            this.timeline.Series.Add(series6);
            this.timeline.Size = new System.Drawing.Size(728, 210);
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
            this.label7.Size = new System.Drawing.Size(728, 29);
            this.label7.TabIndex = 0;
            this.label7.Text = "Timeline";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.ColumnCount = 1;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Controls.Add(this.chart1, 0, 1);
            this.tableLayoutPanel17.Controls.Add(this.comboBox1, 0, 0);
            this.tableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel17.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 2;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(311, 503);
            this.tableLayoutPanel17.TabIndex = 3;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea3.Area3DStyle.Enable3D = true;
            chartArea3.Area3DStyle.Inclination = 35;
            chartArea3.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.None;
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
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 38);
            this.chart1.Name = "chart1";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series7.IsValueShownAsLabel = true;
            series7.Legend = "Legend1";
            series7.Name = "s1";
            dataPoint1.IsVisibleInLegend = true;
            series7.Points.Add(dataPoint1);
            series7.Points.Add(dataPoint2);
            series7.Points.Add(dataPoint3);
            this.chart1.Series.Add(series7);
            this.chart1.Size = new System.Drawing.Size(305, 462);
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
            this.comboBox1.Size = new System.Drawing.Size(305, 25);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.tableLayoutPanel1);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(1057, 509);
            this.tabHistory.TabIndex = 0;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel21, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1051, 503);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel21
            // 
            this.tableLayoutPanel21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel21.ColumnCount = 1;
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel21.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel21.Controls.Add(this.tableLayoutPanel22, 0, 0);
            this.tableLayoutPanel21.Location = new System.Drawing.Point(318, 3);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            this.tableLayoutPanel21.RowCount = 2;
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel21.Size = new System.Drawing.Size(730, 497);
            this.tableLayoutPanel21.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(724, 436);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // tableLayoutPanel22
            // 
            this.tableLayoutPanel22.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel22.ColumnCount = 3;
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel22.Controls.Add(this.cb_showObjects, 0, 0);
            this.tableLayoutPanel22.Controls.Add(this.cb_showMask, 0, 0);
            this.tableLayoutPanel22.Controls.Add(this.lbl_objects, 2, 0);
            this.tableLayoutPanel22.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel22.Name = "tableLayoutPanel22";
            this.tableLayoutPanel22.RowCount = 1;
            this.tableLayoutPanel22.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel22.Size = new System.Drawing.Size(724, 49);
            this.tableLayoutPanel22.TabIndex = 9;
            // 
            // cb_showObjects
            // 
            this.cb_showObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_showObjects.Appearance = System.Windows.Forms.Appearance.Button;
            this.cb_showObjects.AutoSize = true;
            this.cb_showObjects.Checked = true;
            this.cb_showObjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_showObjects.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_showObjects.Location = new System.Drawing.Point(147, 11);
            this.cb_showObjects.Name = "cb_showObjects";
            this.cb_showObjects.Size = new System.Drawing.Size(138, 27);
            this.cb_showObjects.TabIndex = 12;
            this.cb_showObjects.Text = "Show Objects";
            this.cb_showObjects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_showObjects.UseVisualStyleBackColor = true;
            this.cb_showObjects.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cb_showObjects_MouseUp);
            // 
            // cb_showMask
            // 
            this.cb_showMask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_showMask.Appearance = System.Windows.Forms.Appearance.Button;
            this.cb_showMask.AutoSize = true;
            this.cb_showMask.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_showMask.Location = new System.Drawing.Point(3, 11);
            this.cb_showMask.Name = "cb_showMask";
            this.cb_showMask.Size = new System.Drawing.Size(138, 27);
            this.cb_showMask.TabIndex = 11;
            this.cb_showMask.Text = "Show Mask";
            this.cb_showMask.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_showMask.UseVisualStyleBackColor = true;
            this.cb_showMask.CheckedChanged += new System.EventHandler(this.cb_showMask_CheckedChanged);
            // 
            // lbl_objects
            // 
            this.lbl_objects.AutoSize = true;
            this.lbl_objects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_objects.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_objects.Location = new System.Drawing.Point(291, 0);
            this.lbl_objects.Name = "lbl_objects";
            this.lbl_objects.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lbl_objects.Size = new System.Drawing.Size(430, 49);
            this.lbl_objects.TabIndex = 14;
            this.lbl_objects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel19);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Size = new System.Drawing.Size(309, 497);
            this.splitContainer1.SplitterDistance = 273;
            this.splitContainer1.TabIndex = 6;
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.ColumnCount = 1;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel19.Controls.Add(this.cb_showFilters, 0, 1);
            this.tableLayoutPanel19.Controls.Add(this.list1, 0, 0);
            this.tableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel19.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 2;
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(309, 273);
            this.tableLayoutPanel19.TabIndex = 0;
            // 
            // cb_showFilters
            // 
            this.cb_showFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_showFilters.Appearance = System.Windows.Forms.Appearance.Button;
            this.cb_showFilters.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_showFilters.Location = new System.Drawing.Point(3, 245);
            this.cb_showFilters.MinimumSize = new System.Drawing.Size(0, 29);
            this.cb_showFilters.Name = "cb_showFilters";
            this.cb_showFilters.Size = new System.Drawing.Size(303, 29);
            this.cb_showFilters.TabIndex = 9;
            this.cb_showFilters.Text = "˄ Filter";
            this.cb_showFilters.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_showFilters.UseVisualStyleBackColor = true;
            this.cb_showFilters.CheckedChanged += new System.EventHandler(this.cb_showFilters_CheckedChanged);
            // 
            // list1
            // 
            this.list1.AllowColumnReorder = true;
            this.list1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list1.FullRowSelect = true;
            this.list1.GridLines = true;
            this.list1.HideSelection = false;
            this.list1.Location = new System.Drawing.Point(3, 3);
            this.list1.Name = "list1";
            this.list1.Size = new System.Drawing.Size(303, 236);
            this.list1.TabIndex = 3;
            this.list1.UseCompatibleStateImageBehavior = false;
            this.list1.View = System.Windows.Forms.View.Details;
            this.list1.SelectedIndexChanged += new System.EventHandler(this.list1_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.comboBox_filter_camera);
            this.panel1.Controls.Add(this.cb_filter_nosuccess);
            this.panel1.Controls.Add(this.cb_filter_success);
            this.panel1.Controls.Add(this.cb_filter_person);
            this.panel1.Controls.Add(this.cb_filter_vehicle);
            this.panel1.Controls.Add(this.cb_filter_animal);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 214);
            this.panel1.TabIndex = 2;
            // 
            // comboBox_filter_camera
            // 
            this.comboBox_filter_camera.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox_filter_camera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_filter_camera.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_filter_camera.FormattingEnabled = true;
            this.comboBox_filter_camera.Location = new System.Drawing.Point(0, 0);
            this.comboBox_filter_camera.Name = "comboBox_filter_camera";
            this.comboBox_filter_camera.Size = new System.Drawing.Size(301, 25);
            this.comboBox_filter_camera.TabIndex = 2;
            this.comboBox_filter_camera.SelectedIndexChanged += new System.EventHandler(this.comboBox_filter_camera_SelectedIndexChanged);
            // 
            // cb_filter_nosuccess
            // 
            this.cb_filter_nosuccess.AutoSize = true;
            this.cb_filter_nosuccess.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_filter_nosuccess.Location = new System.Drawing.Point(6, 67);
            this.cb_filter_nosuccess.Name = "cb_filter_nosuccess";
            this.cb_filter_nosuccess.Size = new System.Drawing.Size(185, 21);
            this.cb_filter_nosuccess.TabIndex = 1;
            this.cb_filter_nosuccess.Text = "only false / irrelevant alerts";
            this.cb_filter_nosuccess.UseVisualStyleBackColor = true;
            this.cb_filter_nosuccess.CheckedChanged += new System.EventHandler(this.cb_filter_nosuccess_CheckedChanged);
            // 
            // cb_filter_success
            // 
            this.cb_filter_success.AutoSize = true;
            this.cb_filter_success.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_filter_success.Location = new System.Drawing.Point(6, 39);
            this.cb_filter_success.Name = "cb_filter_success";
            this.cb_filter_success.Size = new System.Drawing.Size(137, 21);
            this.cb_filter_success.TabIndex = 0;
            this.cb_filter_success.Text = "only relevant alerts";
            this.cb_filter_success.UseVisualStyleBackColor = true;
            this.cb_filter_success.CheckedChanged += new System.EventHandler(this.cb_filter_success_CheckedChanged);
            // 
            // cb_filter_person
            // 
            this.cb_filter_person.AutoSize = true;
            this.cb_filter_person.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_filter_person.Location = new System.Drawing.Point(6, 96);
            this.cb_filter_person.Name = "cb_filter_person";
            this.cb_filter_person.Size = new System.Drawing.Size(159, 21);
            this.cb_filter_person.TabIndex = 0;
            this.cb_filter_person.Text = "only alerts with people";
            this.cb_filter_person.UseVisualStyleBackColor = true;
            this.cb_filter_person.CheckedChanged += new System.EventHandler(this.cb_filter_person_CheckedChanged);
            // 
            // cb_filter_vehicle
            // 
            this.cb_filter_vehicle.AutoSize = true;
            this.cb_filter_vehicle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_filter_vehicle.Location = new System.Drawing.Point(6, 125);
            this.cb_filter_vehicle.Name = "cb_filter_vehicle";
            this.cb_filter_vehicle.Size = new System.Drawing.Size(163, 21);
            this.cb_filter_vehicle.TabIndex = 0;
            this.cb_filter_vehicle.Text = "only alerts with vehicles";
            this.cb_filter_vehicle.UseVisualStyleBackColor = true;
            this.cb_filter_vehicle.CheckedChanged += new System.EventHandler(this.cb_filter_vehicle_CheckedChanged);
            // 
            // cb_filter_animal
            // 
            this.cb_filter_animal.AutoSize = true;
            this.cb_filter_animal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_filter_animal.Location = new System.Drawing.Point(6, 153);
            this.cb_filter_animal.Name = "cb_filter_animal";
            this.cb_filter_animal.Size = new System.Drawing.Size(162, 21);
            this.cb_filter_animal.TabIndex = 0;
            this.cb_filter_animal.Text = "only alerts with animals";
            this.cb_filter_animal.UseVisualStyleBackColor = true;
            this.cb_filter_animal.CheckedChanged += new System.EventHandler(this.cb_filter_animal_CheckedChanged);
            // 
            // tabCameras
            // 
            this.tabCameras.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabCameras.Controls.Add(this.tableLayoutPanel2);
            this.tabCameras.Location = new System.Drawing.Point(4, 22);
            this.tabCameras.Name = "tabCameras";
            this.tabCameras.Padding = new System.Windows.Forms.Padding(3);
            this.tabCameras.Size = new System.Drawing.Size(1057, 509);
            this.tabCameras.TabIndex = 2;
            this.tabCameras.Text = "Cameras";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.05679F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.94321F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1051, 503);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.list2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 497F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(194, 497);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // list2
            // 
            this.list2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list2.GridLines = true;
            this.list2.HideSelection = false;
            this.list2.Location = new System.Drawing.Point(3, 3);
            this.list2.Name = "list2";
            this.list2.Size = new System.Drawing.Size(188, 491);
            this.list2.TabIndex = 1;
            this.list2.UseCompatibleStateImageBehavior = false;
            this.list2.View = System.Windows.Forms.View.Details;
            this.list2.SelectedIndexChanged += new System.EventHandler(this.list2_SelectedIndexChanged);
            this.list2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.list2_KeyDown);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel11, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.lbl_camstats, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(203, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.281157F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.74277F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.976071F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(845, 497);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel7.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.60259F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.39741F));
            this.tableLayoutPanel7.Controls.Add(this.label14, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.label1, 0, 5);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel9, 1, 5);
            this.tableLayoutPanel7.Controls.Add(this.lblPrefix, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel12, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel13, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.lblRelevantObjects, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.lbl_threshold, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel24, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel26, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.label15, 0, 6);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel27, 1, 6);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 34);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 7;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.276018F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.823529F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.049774F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.56561F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.276018F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.38902F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0358F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(839, 420);
            this.tableLayoutPanel7.TabIndex = 2;
            this.tableLayoutPanel7.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel7_Paint);
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(70, 88);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 15);
            this.label14.TabIndex = 17;
            this.label14.Text = "Input Folder";
            // 
            // tableLayoutPanel8
            // 
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
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(152, 118);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(683, 98);
            this.tableLayoutPanel8.TabIndex = 14;
            // 
            // cb_person
            // 
            this.cb_person.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_person.AutoSize = true;
            this.cb_person.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_person.Location = new System.Drawing.Point(21, 6);
            this.cb_person.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_person.Name = "cb_person";
            this.cb_person.Size = new System.Drawing.Size(62, 19);
            this.cb_person.TabIndex = 4;
            this.cb_person.Text = "Person";
            this.cb_person.UseVisualStyleBackColor = true;
            // 
            // cb_bicycle
            // 
            this.cb_bicycle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_bicycle.AutoSize = true;
            this.cb_bicycle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_bicycle.Location = new System.Drawing.Point(21, 38);
            this.cb_bicycle.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_bicycle.Name = "cb_bicycle";
            this.cb_bicycle.Size = new System.Drawing.Size(63, 19);
            this.cb_bicycle.TabIndex = 9;
            this.cb_bicycle.Text = "Bicycle";
            this.cb_bicycle.UseVisualStyleBackColor = true;
            // 
            // cb_motorcycle
            // 
            this.cb_motorcycle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_motorcycle.AutoSize = true;
            this.cb_motorcycle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_motorcycle.Location = new System.Drawing.Point(21, 71);
            this.cb_motorcycle.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_motorcycle.Name = "cb_motorcycle";
            this.cb_motorcycle.Size = new System.Drawing.Size(86, 19);
            this.cb_motorcycle.TabIndex = 14;
            this.cb_motorcycle.Text = "Motorcycle";
            this.cb_motorcycle.UseVisualStyleBackColor = true;
            // 
            // cb_bear
            // 
            this.cb_bear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_bear.AutoSize = true;
            this.cb_bear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_bear.Location = new System.Drawing.Point(565, 71);
            this.cb_bear.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_bear.Name = "cb_bear";
            this.cb_bear.Size = new System.Drawing.Size(49, 19);
            this.cb_bear.TabIndex = 18;
            this.cb_bear.Text = "Bear";
            this.cb_bear.UseVisualStyleBackColor = true;
            // 
            // cb_cow
            // 
            this.cb_cow.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_cow.AutoSize = true;
            this.cb_cow.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_cow.Location = new System.Drawing.Point(565, 38);
            this.cb_cow.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_cow.Name = "cb_cow";
            this.cb_cow.Size = new System.Drawing.Size(50, 19);
            this.cb_cow.TabIndex = 13;
            this.cb_cow.Text = "Cow";
            this.cb_cow.UseVisualStyleBackColor = true;
            // 
            // cb_sheep
            // 
            this.cb_sheep.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_sheep.AutoSize = true;
            this.cb_sheep.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_sheep.Location = new System.Drawing.Point(565, 6);
            this.cb_sheep.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_sheep.Name = "cb_sheep";
            this.cb_sheep.Size = new System.Drawing.Size(58, 19);
            this.cb_sheep.TabIndex = 8;
            this.cb_sheep.Text = "Sheep";
            this.cb_sheep.UseVisualStyleBackColor = true;
            // 
            // cb_horse
            // 
            this.cb_horse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_horse.AutoSize = true;
            this.cb_horse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_horse.Location = new System.Drawing.Point(429, 71);
            this.cb_horse.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_horse.Name = "cb_horse";
            this.cb_horse.Size = new System.Drawing.Size(57, 19);
            this.cb_horse.TabIndex = 17;
            this.cb_horse.Text = "Horse";
            this.cb_horse.UseVisualStyleBackColor = true;
            // 
            // cb_bird
            // 
            this.cb_bird.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_bird.AutoSize = true;
            this.cb_bird.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_bird.Location = new System.Drawing.Point(429, 38);
            this.cb_bird.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_bird.Name = "cb_bird";
            this.cb_bird.Size = new System.Drawing.Size(47, 19);
            this.cb_bird.TabIndex = 12;
            this.cb_bird.Text = "Bird";
            this.cb_bird.UseVisualStyleBackColor = true;
            // 
            // cb_dog
            // 
            this.cb_dog.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_dog.AutoSize = true;
            this.cb_dog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_dog.Location = new System.Drawing.Point(429, 6);
            this.cb_dog.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_dog.Name = "cb_dog";
            this.cb_dog.Size = new System.Drawing.Size(48, 19);
            this.cb_dog.TabIndex = 7;
            this.cb_dog.Text = "Dog";
            this.cb_dog.UseVisualStyleBackColor = true;
            // 
            // cb_cat
            // 
            this.cb_cat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_cat.AutoSize = true;
            this.cb_cat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_cat.Location = new System.Drawing.Point(293, 71);
            this.cb_cat.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_cat.Name = "cb_cat";
            this.cb_cat.Size = new System.Drawing.Size(44, 19);
            this.cb_cat.TabIndex = 16;
            this.cb_cat.Text = "Cat";
            this.cb_cat.UseVisualStyleBackColor = true;
            // 
            // cb_airplane
            // 
            this.cb_airplane.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_airplane.AutoSize = true;
            this.cb_airplane.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_airplane.Location = new System.Drawing.Point(293, 38);
            this.cb_airplane.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_airplane.Name = "cb_airplane";
            this.cb_airplane.Size = new System.Drawing.Size(70, 19);
            this.cb_airplane.TabIndex = 11;
            this.cb_airplane.Text = "Airplane";
            this.cb_airplane.UseVisualStyleBackColor = true;
            // 
            // cb_boat
            // 
            this.cb_boat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_boat.AutoSize = true;
            this.cb_boat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_boat.Location = new System.Drawing.Point(293, 6);
            this.cb_boat.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_boat.Name = "cb_boat";
            this.cb_boat.Size = new System.Drawing.Size(50, 19);
            this.cb_boat.TabIndex = 6;
            this.cb_boat.Text = "Boat";
            this.cb_boat.UseVisualStyleBackColor = true;
            // 
            // cb_bus
            // 
            this.cb_bus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_bus.AutoSize = true;
            this.cb_bus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_bus.Location = new System.Drawing.Point(157, 71);
            this.cb_bus.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_bus.Name = "cb_bus";
            this.cb_bus.Size = new System.Drawing.Size(45, 19);
            this.cb_bus.TabIndex = 15;
            this.cb_bus.Text = "Bus";
            this.cb_bus.UseVisualStyleBackColor = true;
            // 
            // cb_truck
            // 
            this.cb_truck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_truck.AutoSize = true;
            this.cb_truck.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_truck.Location = new System.Drawing.Point(157, 38);
            this.cb_truck.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_truck.Name = "cb_truck";
            this.cb_truck.Size = new System.Drawing.Size(54, 19);
            this.cb_truck.TabIndex = 10;
            this.cb_truck.Text = "Truck";
            this.cb_truck.UseVisualStyleBackColor = true;
            // 
            // cb_car
            // 
            this.cb_car.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_car.AutoSize = true;
            this.cb_car.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_car.Location = new System.Drawing.Point(157, 6);
            this.cb_car.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_car.Name = "cb_car";
            this.cb_car.Size = new System.Drawing.Size(44, 19);
            this.cb_car.TabIndex = 5;
            this.cb_car.Text = "Car";
            this.cb_car.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(97, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Actions";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel20, 0, 0);
            this.tableLayoutPanel9.Location = new System.Drawing.Point(152, 268);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(683, 77);
            this.tableLayoutPanel9.TabIndex = 8;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.70996F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.29005F));
            this.tableLayoutPanel10.Controls.Add(this.tbTriggerUrl, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblTriggerUrl, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 41);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(677, 33);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // tbTriggerUrl
            // 
            this.tbTriggerUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTriggerUrl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbTriggerUrl.Location = new System.Drawing.Point(118, 5);
            this.tbTriggerUrl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tbTriggerUrl.Name = "tbTriggerUrl";
            this.tbTriggerUrl.Size = new System.Drawing.Size(554, 23);
            this.tbTriggerUrl.TabIndex = 22;
            this.tbTriggerUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTriggerUrl_KeyDown);
            this.tbTriggerUrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbTriggerUrl_KeyUp);
            // 
            // lblTriggerUrl
            // 
            this.lblTriggerUrl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTriggerUrl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTriggerUrl.Location = new System.Drawing.Point(18, 8);
            this.lblTriggerUrl.Margin = new System.Windows.Forms.Padding(18, 0, 3, 0);
            this.lblTriggerUrl.MinimumSize = new System.Drawing.Size(83, 0);
            this.lblTriggerUrl.Name = "lblTriggerUrl";
            this.lblTriggerUrl.Size = new System.Drawing.Size(83, 17);
            this.lblTriggerUrl.TabIndex = 1;
            this.lblTriggerUrl.Text = "Trigger URL(s)";
            // 
            // tableLayoutPanel20
            // 
            this.tableLayoutPanel20.ColumnCount = 5;
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.70996F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.730669F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.46481F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.245F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.13901F));
            this.tableLayoutPanel20.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel20.Controls.Add(this.cb_telegram, 4, 0);
            this.tableLayoutPanel20.Controls.Add(this.cb_TriggerCancels, 3, 0);
            this.tableLayoutPanel20.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel20.Controls.Add(this.tb_cooldown, 1, 0);
            this.tableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel20.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel20.Name = "tableLayoutPanel20";
            this.tableLayoutPanel20.RowCount = 1;
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel20.Size = new System.Drawing.Size(677, 32);
            this.tableLayoutPanel20.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(178, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Minutes";
            // 
            // cb_telegram
            // 
            this.cb_telegram.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_telegram.AutoSize = true;
            this.cb_telegram.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_telegram.Location = new System.Drawing.Point(409, 6);
            this.cb_telegram.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.cb_telegram.Name = "cb_telegram";
            this.cb_telegram.Size = new System.Drawing.Size(184, 19);
            this.cb_telegram.TabIndex = 24;
            this.cb_telegram.Text = "Send alert images to Telegram";
            this.cb_telegram.UseVisualStyleBackColor = false;
            // 
            // cb_TriggerCancels
            // 
            this.cb_TriggerCancels.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_TriggerCancels.AutoSize = true;
            this.cb_TriggerCancels.Location = new System.Drawing.Point(286, 6);
            this.cb_TriggerCancels.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.cb_TriggerCancels.Name = "cb_TriggerCancels";
            this.cb_TriggerCancels.Size = new System.Drawing.Size(106, 19);
            this.cb_TriggerCancels.TabIndex = 25;
            this.cb_TriggerCancels.Text = "Trigger Cancels";
            this.toolTip1.SetToolTip(this.cb_TriggerCancels, "Rather than triggering an alert, the URL will cancel an alert already active in B" +
        "I.  Make sure\r\nto use flagalert=0 rather than 1 in the URL.");
            this.cb_TriggerCancels.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(18, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(18, 0, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cooldown Time";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_cooldown
            // 
            this.tb_cooldown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tb_cooldown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tb_cooldown.Location = new System.Drawing.Point(117, 4);
            this.tb_cooldown.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tb_cooldown.Name = "tb_cooldown";
            this.tb_cooldown.Size = new System.Drawing.Size(51, 23);
            this.tb_cooldown.TabIndex = 21;
            // 
            // lblPrefix
            // 
            this.lblPrefix.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.Location = new System.Drawing.Point(20, 50);
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
            this.lblName.Location = new System.Drawing.Point(105, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(40, 15);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "Name";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Controls.Add(this.lbl_prefix, 1, 0);
            this.tableLayoutPanel12.Controls.Add(this.tbPrefix, 0, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(151, 42);
            this.tableLayoutPanel12.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 1;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(685, 32);
            this.tableLayoutPanel12.TabIndex = 12;
            // 
            // lbl_prefix
            // 
            this.lbl_prefix.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_prefix.AutoSize = true;
            this.lbl_prefix.Location = new System.Drawing.Point(513, 8);
            this.lbl_prefix.Name = "lbl_prefix";
            this.lbl_prefix.Size = new System.Drawing.Size(0, 15);
            this.lbl_prefix.TabIndex = 6;
            // 
            // tbPrefix
            // 
            this.tbPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPrefix.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbPrefix.Location = new System.Drawing.Point(21, 4);
            this.tbPrefix.Margin = new System.Windows.Forms.Padding(21, 3, 21, 3);
            this.tbPrefix.Name = "tbPrefix";
            this.tbPrefix.Size = new System.Drawing.Size(300, 23);
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
            this.tableLayoutPanel13.Location = new System.Drawing.Point(152, 4);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(683, 32);
            this.tableLayoutPanel13.TabIndex = 13;
            // 
            // cb_enabled
            // 
            this.cb_enabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_enabled.AutoSize = true;
            this.cb_enabled.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_enabled.Location = new System.Drawing.Point(362, 6);
            this.cb_enabled.Margin = new System.Windows.Forms.Padding(21, 3, 3, 3);
            this.cb_enabled.Name = "cb_enabled";
            this.cb_enabled.Size = new System.Drawing.Size(211, 19);
            this.cb_enabled.TabIndex = 2;
            this.cb_enabled.Text = "Enable AI Detection for this camera";
            this.cb_enabled.UseVisualStyleBackColor = true;
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbName.Location = new System.Drawing.Point(21, 4);
            this.tbName.Margin = new System.Windows.Forms.Padding(21, 3, 21, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(299, 23);
            this.tbName.TabIndex = 1;
            // 
            // lblRelevantObjects
            // 
            this.lblRelevantObjects.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRelevantObjects.AutoSize = true;
            this.lblRelevantObjects.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRelevantObjects.Location = new System.Drawing.Point(43, 159);
            this.lblRelevantObjects.Name = "lblRelevantObjects";
            this.lblRelevantObjects.Size = new System.Drawing.Size(102, 15);
            this.lblRelevantObjects.TabIndex = 1;
            this.lblRelevantObjects.Text = "Relevant Objects";
            // 
            // lbl_threshold
            // 
            this.lbl_threshold.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_threshold.AutoSize = true;
            this.lbl_threshold.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_threshold.Location = new System.Drawing.Point(42, 231);
            this.lbl_threshold.Name = "lbl_threshold";
            this.lbl_threshold.Size = new System.Drawing.Size(103, 15);
            this.lbl_threshold.TabIndex = 15;
            this.lbl_threshold.Text = "Confidence limits";
            // 
            // tableLayoutPanel24
            // 
            this.tableLayoutPanel24.ColumnCount = 6;
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.08154F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.587813F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.494624F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.57324F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.679928F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.46236F));
            this.tableLayoutPanel24.Controls.Add(this.lbl_threshold_lower, 0, 0);
            this.tableLayoutPanel24.Controls.Add(this.tb_threshold_upper, 4, 0);
            this.tableLayoutPanel24.Controls.Add(this.lbl_threshold_upper, 3, 0);
            this.tableLayoutPanel24.Controls.Add(this.label9, 5, 0);
            this.tableLayoutPanel24.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel24.Controls.Add(this.tb_threshold_lower, 1, 0);
            this.tableLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel24.Location = new System.Drawing.Point(152, 223);
            this.tableLayoutPanel24.Name = "tableLayoutPanel24";
            this.tableLayoutPanel24.RowCount = 1;
            this.tableLayoutPanel24.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel24.Size = new System.Drawing.Size(683, 32);
            this.tableLayoutPanel24.TabIndex = 16;
            // 
            // lbl_threshold_lower
            // 
            this.lbl_threshold_lower.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_threshold_lower.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbl_threshold_lower.Location = new System.Drawing.Point(21, 7);
            this.lbl_threshold_lower.Margin = new System.Windows.Forms.Padding(21, 0, 3, 0);
            this.lbl_threshold_lower.Name = "lbl_threshold_lower";
            this.lbl_threshold_lower.Size = new System.Drawing.Size(65, 17);
            this.lbl_threshold_lower.TabIndex = 19;
            this.lbl_threshold_lower.Text = "Lower limit";
            // 
            // tb_threshold_upper
            // 
            this.tb_threshold_upper.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tb_threshold_upper.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tb_threshold_upper.Location = new System.Drawing.Point(288, 4);
            this.tb_threshold_upper.Margin = new System.Windows.Forms.Padding(5, 3, 1, 3);
            this.tb_threshold_upper.MaxLength = 3;
            this.tb_threshold_upper.Name = "tb_threshold_upper";
            this.tb_threshold_upper.Size = new System.Drawing.Size(47, 23);
            this.tb_threshold_upper.TabIndex = 20;
            this.tb_threshold_upper.Leave += new System.EventHandler(this.tb_threshold_upper_Leave);
            // 
            // lbl_threshold_upper
            // 
            this.lbl_threshold_upper.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_threshold_upper.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbl_threshold_upper.Location = new System.Drawing.Point(225, 7);
            this.lbl_threshold_upper.Margin = new System.Windows.Forms.Padding(21, 0, 3, 0);
            this.lbl_threshold_upper.Name = "lbl_threshold_upper";
            this.lbl_threshold_upper.Size = new System.Drawing.Size(43, 17);
            this.lbl_threshold_upper.TabIndex = 21;
            this.lbl_threshold_upper.Text = "Upper limit";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(343, 7);
            this.label9.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "%";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label10.Location = new System.Drawing.Point(182, 8);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 15);
            this.label10.TabIndex = 23;
            this.label10.Text = "%";
            // 
            // tb_threshold_lower
            // 
            this.tb_threshold_lower.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tb_threshold_lower.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tb_threshold_lower.Location = new System.Drawing.Point(121, 4);
            this.tb_threshold_lower.Margin = new System.Windows.Forms.Padding(5, 3, 1, 3);
            this.tb_threshold_lower.MaxLength = 3;
            this.tb_threshold_lower.Name = "tb_threshold_lower";
            this.tb_threshold_lower.Size = new System.Drawing.Size(51, 23);
            this.tb_threshold_lower.TabIndex = 19;
            this.tb_threshold_lower.Leave += new System.EventHandler(this.tb_threshold_lower_Leave);
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
            this.tableLayoutPanel26.Location = new System.Drawing.Point(151, 79);
            this.tableLayoutPanel26.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel26.Name = "tableLayoutPanel26";
            this.tableLayoutPanel26.RowCount = 1;
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel26.Size = new System.Drawing.Size(685, 33);
            this.tableLayoutPanel26.TabIndex = 18;
            // 
            // cmbcaminput
            // 
            this.cmbcaminput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbcaminput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcaminput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcaminput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbcaminput.FormattingEnabled = true;
            this.cmbcaminput.Location = new System.Drawing.Point(21, 5);
            this.cmbcaminput.Margin = new System.Windows.Forms.Padding(21, 2, 21, 2);
            this.cmbcaminput.Name = "cmbcaminput";
            this.cmbcaminput.Size = new System.Drawing.Size(386, 23);
            this.cmbcaminput.TabIndex = 7;
            // 
            // cb_monitorCamInputfolder
            // 
            this.cb_monitorCamInputfolder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cb_monitorCamInputfolder.AutoSize = true;
            this.cb_monitorCamInputfolder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_monitorCamInputfolder.Location = new System.Drawing.Point(449, 7);
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
            this.button2.Location = new System.Drawing.Point(611, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 25);
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
            this.label15.Location = new System.Drawing.Point(93, 379);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 15);
            this.label15.TabIndex = 19;
            this.label15.Text = "Masking";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel27
            // 
            this.tableLayoutPanel27.ColumnCount = 5;
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.63504F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.65693F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.21898F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.51825F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.40876F));
            this.tableLayoutPanel27.Controls.Add(this.cb_masking_enabled, 0, 0);
            this.tableLayoutPanel27.Controls.Add(this.BtnDynamicMaskingSettings, 1, 0);
            this.tableLayoutPanel27.Controls.Add(this.btnDetails, 2, 0);
            this.tableLayoutPanel27.Controls.Add(this.btnCustomMask, 4, 0);
            this.tableLayoutPanel27.Controls.Add(this.lblDrawMask, 3, 0);
            this.tableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel27.Location = new System.Drawing.Point(151, 356);
            this.tableLayoutPanel27.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.tableLayoutPanel27.Name = "tableLayoutPanel27";
            this.tableLayoutPanel27.RowCount = 1;
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel27.Size = new System.Drawing.Size(685, 62);
            this.tableLayoutPanel27.TabIndex = 20;
            // 
            // cb_masking_enabled
            // 
            this.cb_masking_enabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_masking_enabled.AutoSize = true;
            this.cb_masking_enabled.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_masking_enabled.Location = new System.Drawing.Point(21, 23);
            this.cb_masking_enabled.Margin = new System.Windows.Forms.Padding(21, 3, 5, 0);
            this.cb_masking_enabled.Name = "cb_masking_enabled";
            this.cb_masking_enabled.Size = new System.Drawing.Size(158, 19);
            this.cb_masking_enabled.TabIndex = 21;
            this.cb_masking_enabled.Text = "Enable dynamic masking";
            this.cb_masking_enabled.UseVisualStyleBackColor = true;
            // 
            // BtnDynamicMaskingSettings
            // 
            this.BtnDynamicMaskingSettings.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnDynamicMaskingSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnDynamicMaskingSettings.Location = new System.Drawing.Point(207, 18);
            this.BtnDynamicMaskingSettings.Margin = new System.Windows.Forms.Padding(5, 1, 5, 1);
            this.BtnDynamicMaskingSettings.Name = "BtnDynamicMaskingSettings";
            this.BtnDynamicMaskingSettings.Size = new System.Drawing.Size(62, 25);
            this.BtnDynamicMaskingSettings.TabIndex = 22;
            this.BtnDynamicMaskingSettings.Text = "Settings";
            this.BtnDynamicMaskingSettings.UseVisualStyleBackColor = true;
            this.BtnDynamicMaskingSettings.Click += new System.EventHandler(this.BtnDynamicMaskingSettings_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDetails.Location = new System.Drawing.Point(279, 18);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(59, 25);
            this.btnDetails.TabIndex = 23;
            this.btnDetails.Text = "Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // btnCustomMask
            // 
            this.btnCustomMask.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCustomMask.Location = new System.Drawing.Point(463, 18);
            this.btnCustomMask.Margin = new System.Windows.Forms.Padding(1, 2, 5, 2);
            this.btnCustomMask.Name = "btnCustomMask";
            this.btnCustomMask.Size = new System.Drawing.Size(62, 25);
            this.btnCustomMask.TabIndex = 24;
            this.btnCustomMask.Text = "Custom";
            this.btnCustomMask.UseVisualStyleBackColor = true;
            this.btnCustomMask.Click += new System.EventHandler(this.btnCustomMask_Click);
            // 
            // lblDrawMask
            // 
            this.lblDrawMask.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDrawMask.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDrawMask.Location = new System.Drawing.Point(395, 23);
            this.lblDrawMask.Margin = new System.Windows.Forms.Padding(0);
            this.lblDrawMask.Name = "lblDrawMask";
            this.lblDrawMask.Size = new System.Drawing.Size(67, 16);
            this.lblDrawMask.TabIndex = 25;
            this.lblDrawMask.Text = "Draw Mask";
            this.lblDrawMask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel11.ColumnCount = 4;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.02243F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.32586F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.32586F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.32586F));
            this.tableLayoutPanel11.Controls.Add(this.btnCameraAdd, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.btnCameraDel, 2, 0);
            this.tableLayoutPanel11.Controls.Add(this.btnCameraSave, 3, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 460);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(839, 34);
            this.tableLayoutPanel11.TabIndex = 3;
            // 
            // btnCameraAdd
            // 
            this.btnCameraAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCameraAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCameraAdd.Location = new System.Drawing.Point(208, 3);
            this.btnCameraAdd.Name = "btnCameraAdd";
            this.btnCameraAdd.Size = new System.Drawing.Size(115, 28);
            this.btnCameraAdd.TabIndex = 24;
            this.btnCameraAdd.Text = "Add";
            this.btnCameraAdd.UseVisualStyleBackColor = true;
            this.btnCameraAdd.Click += new System.EventHandler(this.btnCameraAdd_Click);
            // 
            // btnCameraDel
            // 
            this.btnCameraDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCameraDel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCameraDel.Location = new System.Drawing.Point(437, 3);
            this.btnCameraDel.Name = "btnCameraDel";
            this.btnCameraDel.Size = new System.Drawing.Size(115, 28);
            this.btnCameraDel.TabIndex = 25;
            this.btnCameraDel.Text = "Delete";
            this.btnCameraDel.UseVisualStyleBackColor = true;
            this.btnCameraDel.Click += new System.EventHandler(this.btnCameraDel_Click);
            // 
            // btnCameraSave
            // 
            this.btnCameraSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCameraSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCameraSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCameraSave.Location = new System.Drawing.Point(666, 3);
            this.btnCameraSave.Name = "btnCameraSave";
            this.btnCameraSave.Size = new System.Drawing.Size(115, 28);
            this.btnCameraSave.TabIndex = 26;
            this.btnCameraSave.Text = "Save";
            this.btnCameraSave.UseVisualStyleBackColor = false;
            this.btnCameraSave.Click += new System.EventHandler(this.btnCameraSave_Click_1);
            // 
            // lbl_camstats
            // 
            this.lbl_camstats.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_camstats.AutoSize = true;
            this.lbl_camstats.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_camstats.Location = new System.Drawing.Point(3, 7);
            this.lbl_camstats.Name = "lbl_camstats";
            this.lbl_camstats.Size = new System.Drawing.Size(38, 17);
            this.lbl_camstats.TabIndex = 4;
            this.lbl_camstats.Text = "Stats";
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tableLayoutPanel4);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(1057, 509);
            this.tabSettings.TabIndex = 3;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.BtnSettingsSave, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.32024F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.679764F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1057, 509);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel25, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.label12, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.cb_send_errors, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.lbl_deepstackurl, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.lbl_input, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tbDeepstackUrl, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.lbl_telegram_token, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.lbl_telegram_chatid, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.tb_telegram_token, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.tb_telegram_chatid, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel18, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label13, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.cbStartWithWindows, 1, 6);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 7;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2141F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.05616F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.34571F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.34571F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.34571F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.34571F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.3469F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1051, 469);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(124, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Log";
            // 
            // tableLayoutPanel25
            // 
            this.tableLayoutPanel25.ColumnCount = 2;
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel25.Controls.Add(this.btn_open_log, 1, 0);
            this.tableLayoutPanel25.Controls.Add(this.cb_log, 0, 0);
            this.tableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel25.Location = new System.Drawing.Point(162, 269);
            this.tableLayoutPanel25.Name = "tableLayoutPanel25";
            this.tableLayoutPanel25.RowCount = 1;
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel25.Size = new System.Drawing.Size(885, 60);
            this.tableLayoutPanel25.TabIndex = 14;
            // 
            // btn_open_log
            // 
            this.btn_open_log.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_open_log.Location = new System.Drawing.Point(810, 18);
            this.btn_open_log.Name = "btn_open_log";
            this.btn_open_log.Size = new System.Drawing.Size(61, 24);
            this.btn_open_log.TabIndex = 2;
            this.btn_open_log.Text = "Open Log";
            this.btn_open_log.UseVisualStyleBackColor = true;
            this.btn_open_log.Click += new System.EventHandler(this.btn_open_log_Click);
            // 
            // cb_log
            // 
            this.cb_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_log.Location = new System.Drawing.Point(3, 3);
            this.cb_log.Name = "cb_log";
            this.cb_log.Size = new System.Drawing.Size(790, 54);
            this.cb_log.TabIndex = 11;
            this.cb_log.Text = "Log everything";
            this.cb_log.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(77, 357);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 17);
            this.label12.TabIndex = 13;
            this.label12.Text = "Send Errors";
            // 
            // cb_send_errors
            // 
            this.cb_send_errors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_send_errors.Location = new System.Drawing.Point(162, 336);
            this.cb_send_errors.Name = "cb_send_errors";
            this.cb_send_errors.Size = new System.Drawing.Size(885, 60);
            this.cb_send_errors.TabIndex = 12;
            this.cb_send_errors.Text = "Send Errors and Warnings to Telegram";
            this.cb_send_errors.UseVisualStyleBackColor = true;
            // 
            // lbl_deepstackurl
            // 
            this.lbl_deepstackurl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_deepstackurl.AutoSize = true;
            this.lbl_deepstackurl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_deepstackurl.Location = new System.Drawing.Point(40, 90);
            this.lbl_deepstackurl.Name = "lbl_deepstackurl";
            this.lbl_deepstackurl.Size = new System.Drawing.Size(115, 17);
            this.lbl_deepstackurl.TabIndex = 4;
            this.lbl_deepstackurl.Text = "Deepstack URL(s)";
            // 
            // lbl_input
            // 
            this.lbl_input.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_input.AutoSize = true;
            this.lbl_input.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_input.Location = new System.Drawing.Point(32, 25);
            this.lbl_input.Name = "lbl_input";
            this.lbl_input.Size = new System.Drawing.Size(123, 17);
            this.lbl_input.TabIndex = 1;
            this.lbl_input.Text = "Default Input Path";
            // 
            // tbDeepstackUrl
            // 
            this.tbDeepstackUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDeepstackUrl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDeepstackUrl.Location = new System.Drawing.Point(162, 86);
            this.tbDeepstackUrl.Name = "tbDeepstackUrl";
            this.tbDeepstackUrl.Size = new System.Drawing.Size(885, 25);
            this.tbDeepstackUrl.TabIndex = 5;
            this.toolTip1.SetToolTip(this.tbDeepstackUrl, "Enter SERVER:PORT for DeepStack server - Use ; to separate more than one URL for " +
        "parallel processing");
            // 
            // lbl_telegram_token
            // 
            this.lbl_telegram_token.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_telegram_token.AutoSize = true;
            this.lbl_telegram_token.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_telegram_token.Location = new System.Drawing.Point(49, 156);
            this.lbl_telegram_token.Name = "lbl_telegram_token";
            this.lbl_telegram_token.Size = new System.Drawing.Size(106, 17);
            this.lbl_telegram_token.TabIndex = 6;
            this.lbl_telegram_token.Text = "Telegram Token";
            // 
            // lbl_telegram_chatid
            // 
            this.lbl_telegram_chatid.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_telegram_chatid.AutoSize = true;
            this.lbl_telegram_chatid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_telegram_chatid.Location = new System.Drawing.Point(40, 223);
            this.lbl_telegram_chatid.Name = "lbl_telegram_chatid";
            this.lbl_telegram_chatid.Size = new System.Drawing.Size(115, 17);
            this.lbl_telegram_chatid.TabIndex = 7;
            this.lbl_telegram_chatid.Text = "Telegram Chat ID";
            // 
            // tb_telegram_token
            // 
            this.tb_telegram_token.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_telegram_token.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_telegram_token.Location = new System.Drawing.Point(162, 152);
            this.tb_telegram_token.Name = "tb_telegram_token";
            this.tb_telegram_token.Size = new System.Drawing.Size(885, 25);
            this.tb_telegram_token.TabIndex = 8;
            // 
            // tb_telegram_chatid
            // 
            this.tb_telegram_chatid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_telegram_chatid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_telegram_chatid.Location = new System.Drawing.Point(162, 219);
            this.tb_telegram_chatid.Name = "tb_telegram_chatid";
            this.tb_telegram_chatid.Size = new System.Drawing.Size(885, 25);
            this.tb_telegram_chatid.TabIndex = 9;
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.ColumnCount = 3;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.9927F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.897F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.110301F));
            this.tableLayoutPanel18.Controls.Add(this.btn_input_path, 2, 0);
            this.tableLayoutPanel18.Controls.Add(this.cmbInput, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.cb_inputpathsubfolders, 1, 0);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(162, 4);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 1;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(885, 59);
            this.tableLayoutPanel18.TabIndex = 12;
            // 
            // btn_input_path
            // 
            this.btn_input_path.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_input_path.Location = new System.Drawing.Point(818, 17);
            this.btn_input_path.Name = "btn_input_path";
            this.btn_input_path.Size = new System.Drawing.Size(61, 24);
            this.btn_input_path.TabIndex = 2;
            this.btn_input_path.Text = "Select...";
            this.btn_input_path.UseVisualStyleBackColor = true;
            this.btn_input_path.Click += new System.EventHandler(this.btn_input_path_Click);
            // 
            // cmbInput
            // 
            this.cmbInput.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbInput.FormattingEnabled = true;
            this.cmbInput.Location = new System.Drawing.Point(3, 19);
            this.cmbInput.Margin = new System.Windows.Forms.Padding(3, 2, 2, 2);
            this.cmbInput.Name = "cmbInput";
            this.cmbInput.Size = new System.Drawing.Size(599, 21);
            this.cmbInput.TabIndex = 3;
            // 
            // cb_inputpathsubfolders
            // 
            this.cb_inputpathsubfolders.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cb_inputpathsubfolders.AutoSize = true;
            this.cb_inputpathsubfolders.Location = new System.Drawing.Point(671, 21);
            this.cb_inputpathsubfolders.Margin = new System.Windows.Forms.Padding(2);
            this.cb_inputpathsubfolders.Name = "cb_inputpathsubfolders";
            this.cb_inputpathsubfolders.Size = new System.Drawing.Size(114, 17);
            this.cb_inputpathsubfolders.TabIndex = 4;
            this.cb_inputpathsubfolders.Text = "Monitor Subfolders";
            this.cb_inputpathsubfolders.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(119, 425);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 17);
            this.label13.TabIndex = 16;
            this.label13.Text = "Start";
            // 
            // cbStartWithWindows
            // 
            this.cbStartWithWindows.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbStartWithWindows.AutoSize = true;
            this.cbStartWithWindows.Location = new System.Drawing.Point(161, 425);
            this.cbStartWithWindows.Margin = new System.Windows.Forms.Padding(2);
            this.cbStartWithWindows.Name = "cbStartWithWindows";
            this.cbStartWithWindows.Size = new System.Drawing.Size(182, 17);
            this.cbStartWithWindows.TabIndex = 17;
            this.cbStartWithWindows.Text = "Start with user login (non-service)";
            this.cbStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // BtnSettingsSave
            // 
            this.BtnSettingsSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.BtnSettingsSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSettingsSave.Location = new System.Drawing.Point(471, 478);
            this.BtnSettingsSave.Name = "BtnSettingsSave";
            this.BtnSettingsSave.Size = new System.Drawing.Size(115, 28);
            this.BtnSettingsSave.TabIndex = 2;
            this.BtnSettingsSave.Text = "Save";
            this.BtnSettingsSave.UseVisualStyleBackColor = true;
            this.BtnSettingsSave.Click += new System.EventHandler(this.BtnSettingsSave_Click_1);
            // 
            // tabDeepStack
            // 
            this.tabDeepStack.Controls.Add(this.chk_HighPriority);
            this.tabDeepStack.Controls.Add(this.Chk_DSDebug);
            this.tabDeepStack.Controls.Add(this.Lbl_BlueStackRunning);
            this.tabDeepStack.Controls.Add(this.Btn_Save);
            this.tabDeepStack.Controls.Add(this.label11);
            this.tabDeepStack.Controls.Add(this.groupBox6);
            this.tabDeepStack.Controls.Add(this.groupBox1);
            this.tabDeepStack.Controls.Add(this.groupBox5);
            this.tabDeepStack.Controls.Add(this.groupBox2);
            this.tabDeepStack.Controls.Add(this.groupBox4);
            this.tabDeepStack.Controls.Add(this.groupBox3);
            this.tabDeepStack.Controls.Add(this.Chk_AutoStart);
            this.tabDeepStack.Controls.Add(this.Btn_Start);
            this.tabDeepStack.Controls.Add(this.Btn_Stop);
            this.tabDeepStack.Location = new System.Drawing.Point(4, 22);
            this.tabDeepStack.Margin = new System.Windows.Forms.Padding(2);
            this.tabDeepStack.Name = "tabDeepStack";
            this.tabDeepStack.Size = new System.Drawing.Size(1057, 509);
            this.tabDeepStack.TabIndex = 6;
            this.tabDeepStack.Text = "DeepStack";
            this.tabDeepStack.UseVisualStyleBackColor = true;
            // 
            // chk_HighPriority
            // 
            this.chk_HighPriority.AutoSize = true;
            this.chk_HighPriority.Location = new System.Drawing.Point(342, 301);
            this.chk_HighPriority.Margin = new System.Windows.Forms.Padding(2);
            this.chk_HighPriority.Name = "chk_HighPriority";
            this.chk_HighPriority.Size = new System.Drawing.Size(102, 17);
            this.chk_HighPriority.TabIndex = 15;
            this.chk_HighPriority.Text = "Run high priority";
            this.chk_HighPriority.UseVisualStyleBackColor = true;
            // 
            // Chk_DSDebug
            // 
            this.Chk_DSDebug.AutoSize = true;
            this.Chk_DSDebug.Location = new System.Drawing.Point(206, 301);
            this.Chk_DSDebug.Margin = new System.Windows.Forms.Padding(2);
            this.Chk_DSDebug.Name = "Chk_DSDebug";
            this.Chk_DSDebug.Size = new System.Drawing.Size(113, 17);
            this.Chk_DSDebug.TabIndex = 14;
            this.Chk_DSDebug.Text = "Debug Deepstack";
            this.toolTip1.SetToolTip(this.Chk_DSDebug, "Show all output from Deepstack\'s python.exe, redis.exe and server.exe  (Windows v" +
        "ersion, installed on same machine)");
            this.Chk_DSDebug.UseVisualStyleBackColor = true;
            // 
            // Lbl_BlueStackRunning
            // 
            this.Lbl_BlueStackRunning.AutoSize = true;
            this.Lbl_BlueStackRunning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_BlueStackRunning.Location = new System.Drawing.Point(282, 348);
            this.Lbl_BlueStackRunning.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lbl_BlueStackRunning.Name = "Lbl_BlueStackRunning";
            this.Lbl_BlueStackRunning.Size = new System.Drawing.Size(105, 13);
            this.Lbl_BlueStackRunning.TabIndex = 13;
            this.Lbl_BlueStackRunning.Text = "*NOT RUNNING*";
            // 
            // Btn_Save
            // 
            this.Btn_Save.Location = new System.Drawing.Point(191, 338);
            this.Btn_Save.Margin = new System.Windows.Forms.Padding(2);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(76, 33);
            this.Btn_Save.TabIndex = 12;
            this.Btn_Save.Text = "Save";
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label11.Location = new System.Drawing.Point(6, 10);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1159, 34);
            this.label11.TabIndex = 0;
            this.label11.Text = "When the WINDOWS version of DeepStack will be running on the same machine as AI T" +
    "ool, this tab can replace the DeepStack UI. (Deepstack.exe) - No control over Do" +
    "cker version yet.";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.Txt_APIKey);
            this.groupBox6.Location = new System.Drawing.Point(11, 146);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(483, 45);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "API Key (Optional)";
            // 
            // Txt_APIKey
            // 
            this.Txt_APIKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_APIKey.Location = new System.Drawing.Point(7, 17);
            this.Txt_APIKey.Margin = new System.Windows.Forms.Padding(2);
            this.Txt_APIKey.Name = "Txt_APIKey";
            this.Txt_APIKey.Size = new System.Drawing.Size(472, 20);
            this.Txt_APIKey.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Chk_DetectionAPI);
            this.groupBox1.Controls.Add(this.Chk_FaceAPI);
            this.groupBox1.Controls.Add(this.Chk_SceneAPI);
            this.groupBox1.Location = new System.Drawing.Point(8, 203);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(155, 82);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "API";
            // 
            // Chk_DetectionAPI
            // 
            this.Chk_DetectionAPI.AutoSize = true;
            this.Chk_DetectionAPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_DetectionAPI.Location = new System.Drawing.Point(11, 56);
            this.Chk_DetectionAPI.Margin = new System.Windows.Forms.Padding(2);
            this.Chk_DetectionAPI.Name = "Chk_DetectionAPI";
            this.Chk_DetectionAPI.Size = new System.Drawing.Size(105, 17);
            this.Chk_DetectionAPI.TabIndex = 2;
            this.Chk_DetectionAPI.Text = "Detection API";
            this.Chk_DetectionAPI.UseVisualStyleBackColor = true;
            // 
            // Chk_FaceAPI
            // 
            this.Chk_FaceAPI.AutoSize = true;
            this.Chk_FaceAPI.Location = new System.Drawing.Point(11, 36);
            this.Chk_FaceAPI.Margin = new System.Windows.Forms.Padding(2);
            this.Chk_FaceAPI.Name = "Chk_FaceAPI";
            this.Chk_FaceAPI.Size = new System.Drawing.Size(70, 17);
            this.Chk_FaceAPI.TabIndex = 1;
            this.Chk_FaceAPI.Text = "Face API";
            this.Chk_FaceAPI.UseVisualStyleBackColor = true;
            // 
            // Chk_SceneAPI
            // 
            this.Chk_SceneAPI.AutoSize = true;
            this.Chk_SceneAPI.Location = new System.Drawing.Point(11, 17);
            this.Chk_SceneAPI.Margin = new System.Windows.Forms.Padding(2);
            this.Chk_SceneAPI.Name = "Chk_SceneAPI";
            this.Chk_SceneAPI.Size = new System.Drawing.Size(77, 17);
            this.Chk_SceneAPI.TabIndex = 0;
            this.Chk_SceneAPI.Text = "Scene API";
            this.Chk_SceneAPI.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Txt_AdminKey);
            this.groupBox5.Location = new System.Drawing.Point(11, 96);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(483, 45);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Admin Key (Optional)";
            // 
            // Txt_AdminKey
            // 
            this.Txt_AdminKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_AdminKey.Location = new System.Drawing.Point(7, 16);
            this.Txt_AdminKey.Margin = new System.Windows.Forms.Padding(2);
            this.Txt_AdminKey.Name = "Txt_AdminKey";
            this.Txt_AdminKey.Size = new System.Drawing.Size(472, 20);
            this.Txt_AdminKey.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RB_High);
            this.groupBox2.Controls.Add(this.RB_Medium);
            this.groupBox2.Controls.Add(this.RB_Low);
            this.groupBox2.Location = new System.Drawing.Point(175, 203);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(154, 82);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mode";
            // 
            // RB_High
            // 
            this.RB_High.AutoSize = true;
            this.RB_High.Location = new System.Drawing.Point(11, 57);
            this.RB_High.Margin = new System.Windows.Forms.Padding(2);
            this.RB_High.Name = "RB_High";
            this.RB_High.Size = new System.Drawing.Size(47, 17);
            this.RB_High.TabIndex = 3;
            this.RB_High.TabStop = true;
            this.RB_High.Text = "High";
            this.RB_High.UseVisualStyleBackColor = true;
            // 
            // RB_Medium
            // 
            this.RB_Medium.AutoSize = true;
            this.RB_Medium.Location = new System.Drawing.Point(11, 37);
            this.RB_Medium.Margin = new System.Windows.Forms.Padding(2);
            this.RB_Medium.Name = "RB_Medium";
            this.RB_Medium.Size = new System.Drawing.Size(62, 17);
            this.RB_Medium.TabIndex = 2;
            this.RB_Medium.TabStop = true;
            this.RB_Medium.Text = "Medium";
            this.RB_Medium.UseVisualStyleBackColor = true;
            // 
            // RB_Low
            // 
            this.RB_Low.AutoSize = true;
            this.RB_Low.Location = new System.Drawing.Point(11, 17);
            this.RB_Low.Margin = new System.Windows.Forms.Padding(2);
            this.RB_Low.Name = "RB_Low";
            this.RB_Low.Size = new System.Drawing.Size(45, 17);
            this.RB_Low.TabIndex = 1;
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
            this.groupBox3.Controls.Add(this.Txt_Port);
            this.groupBox3.Location = new System.Drawing.Point(342, 203);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(151, 82);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Port";
            // 
            // Txt_Port
            // 
            this.Txt_Port.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Port.Location = new System.Drawing.Point(10, 19);
            this.Txt_Port.Margin = new System.Windows.Forms.Padding(2);
            this.Txt_Port.Name = "Txt_Port";
            this.Txt_Port.Size = new System.Drawing.Size(139, 20);
            this.Txt_Port.TabIndex = 0;
            // 
            // Chk_AutoStart
            // 
            this.Chk_AutoStart.AutoSize = true;
            this.Chk_AutoStart.Location = new System.Drawing.Point(13, 301);
            this.Chk_AutoStart.Margin = new System.Windows.Forms.Padding(2);
            this.Chk_AutoStart.Name = "Chk_AutoStart";
            this.Chk_AutoStart.Size = new System.Drawing.Size(170, 17);
            this.Chk_AutoStart.TabIndex = 8;
            this.Chk_AutoStart.Text = "Automatically Start DeepStack";
            this.Chk_AutoStart.UseVisualStyleBackColor = true;
            // 
            // Btn_Start
            // 
            this.Btn_Start.Location = new System.Drawing.Point(8, 338);
            this.Btn_Start.Margin = new System.Windows.Forms.Padding(2);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(76, 33);
            this.Btn_Start.TabIndex = 6;
            this.Btn_Start.Text = "Start";
            this.Btn_Start.UseVisualStyleBackColor = true;
            this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // Btn_Stop
            // 
            this.Btn_Stop.Location = new System.Drawing.Point(99, 338);
            this.Btn_Stop.Margin = new System.Windows.Forms.Padding(2);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(76, 33);
            this.Btn_Stop.TabIndex = 7;
            this.Btn_Stop.Text = "Stop";
            this.Btn_Stop.UseVisualStyleBackColor = true;
            this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.groupBox7);
            this.tabLog.Controls.Add(this.Chk_AutoScroll);
            this.tabLog.Controls.Add(this.button1);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Margin = new System.Windows.Forms.Padding(2);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(1057, 509);
            this.tabLog.TabIndex = 7;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            this.tabLog.Click += new System.EventHandler(this.tabLog_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.RTF_Log);
            this.groupBox7.Location = new System.Drawing.Point(5, 35);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(1101, 531);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Log";
            // 
            // RTF_Log
            // 
            this.RTF_Log.BackColor = System.Drawing.Color.RoyalBlue;
            this.RTF_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTF_Log.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RTF_Log.ForeColor = System.Drawing.Color.White;
            this.RTF_Log.Location = new System.Drawing.Point(2, 15);
            this.RTF_Log.Margin = new System.Windows.Forms.Padding(2);
            this.RTF_Log.Name = "RTF_Log";
            this.RTF_Log.Size = new System.Drawing.Size(1097, 514);
            this.RTF_Log.TabIndex = 0;
            this.RTF_Log.Text = "";
            // 
            // Chk_AutoScroll
            // 
            this.Chk_AutoScroll.AutoSize = true;
            this.Chk_AutoScroll.Checked = true;
            this.Chk_AutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_AutoScroll.Location = new System.Drawing.Point(86, 11);
            this.Chk_AutoScroll.Margin = new System.Windows.Forms.Padding(2);
            this.Chk_AutoScroll.Name = "Chk_AutoScroll";
            this.Chk_AutoScroll.Size = new System.Drawing.Size(140, 17);
            this.Chk_AutoScroll.TabIndex = 4;
            this.Chk_AutoScroll.Text = "Auto Scroll Log Window";
            this.Chk_AutoScroll.UseVisualStyleBackColor = true;
            this.Chk_AutoScroll.CheckedChanged += new System.EventHandler(this.Chk_AutoScroll_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "Open Log";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Shell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1065, 535);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F);
            this.MinimumSize = new System.Drawing.Size(1002, 483);
            this.Name = "Shell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "SAVE";
            this.Text = "AI Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Shell_FormClosing);
            this.Load += new System.EventHandler(this.Shell_Load);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel22.ResumeLayout(false);
            this.tableLayoutPanel22.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel19.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabCameras.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel20.ResumeLayout(false);
            this.tableLayoutPanel20.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tableLayoutPanel24.ResumeLayout(false);
            this.tableLayoutPanel24.PerformLayout();
            this.tableLayoutPanel26.ResumeLayout(false);
            this.tableLayoutPanel26.PerformLayout();
            this.tableLayoutPanel27.ResumeLayout(false);
            this.tableLayoutPanel27.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel25.ResumeLayout(false);
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel18.PerformLayout();
            this.tabDeepStack.ResumeLayout(false);
            this.tabDeepStack.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private DBLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.TabPage tabCameras;
        private System.Windows.Forms.TabPage tabSettings;
        private DBLayoutPanel tableLayoutPanel4;
        private DBLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lbl_deepstackurl;
        private System.Windows.Forms.Label lbl_input;
        private System.Windows.Forms.Button BtnSettingsSave;
        private DBLayoutPanel tableLayoutPanel2;
        private DBLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnCameraAdd;
        private DBLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.Label lblRelevantObjects;
        private DBLayoutPanel tableLayoutPanel9;
        private DBLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TextBox tbTriggerUrl;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ListView list2;
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
        private System.Windows.Forms.TextBox tb_telegram_token;
        private System.Windows.Forms.TextBox tb_telegram_chatid;
        private System.Windows.Forms.TabPage tabOverview;
        private System.Windows.Forms.Label lblTriggerUrl;
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
        private System.Windows.Forms.CheckBox cb_log;
        private System.Windows.Forms.TextBox tbDeepstackUrl;
        private DBLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.Button btn_input_path;
        private DBLayoutPanel tableLayoutPanel20;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_cooldown;
        private System.Windows.Forms.Label label6;
        private DBLayoutPanel tableLayoutPanel21;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DBLayoutPanel tableLayoutPanel19;
        private System.Windows.Forms.CheckBox cb_showFilters;
        private System.Windows.Forms.ListView list1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cb_filter_nosuccess;
        private System.Windows.Forms.CheckBox cb_filter_success;
        private System.Windows.Forms.CheckBox cb_filter_person;
        private System.Windows.Forms.CheckBox cb_filter_vehicle;
        private System.Windows.Forms.CheckBox cb_filter_animal;
        private System.Windows.Forms.ComboBox comboBox_filter_camera;
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
        private System.Windows.Forms.Label lbl_threshold;
        private DBLayoutPanel tableLayoutPanel24;
        private System.Windows.Forms.Label lbl_threshold_lower;
        private System.Windows.Forms.TextBox tb_threshold_upper;
        private System.Windows.Forms.Label lbl_threshold_upper;
        private System.Windows.Forms.TextBox tb_threshold_lower;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cb_send_errors;
        private System.Windows.Forms.Label label4;
        private DBLayoutPanel tableLayoutPanel25;
        private System.Windows.Forms.Button btn_open_log;
        private DBLayoutPanel tableLayoutPanel22;
        private System.Windows.Forms.CheckBox cb_showObjects;
        private System.Windows.Forms.CheckBox cb_showMask;
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
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox Txt_APIKey;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox Txt_AdminKey;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label Lbl_BlueStackRunning;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.RichTextBox RTF_Log;
        private System.Windows.Forms.ComboBox cmbInput;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox cbStartWithWindows;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox Chk_AutoScroll;
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
        private System.Windows.Forms.CheckBox cb_telegram;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel27;
        protected internal System.Windows.Forms.CheckBox cb_masking_enabled;
        private System.Windows.Forms.Button BtnDynamicMaskingSettings;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Label lblQueue;
        private System.Windows.Forms.CheckBox cb_TriggerCancels;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnCustomMask;
        private System.Windows.Forms.Label lblDrawMask;
    }
}

