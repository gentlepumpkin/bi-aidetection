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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea34 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series78 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series79 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea35 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series80 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series81 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series82 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series83 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea36 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend12 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series84 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint34 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 30D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint35 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 30D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint36 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 30D);
            System.Windows.Forms.DataVisualization.Charting.Title title12 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tmrCollapse = new System.Windows.Forms.Timer(this.components);
            this.tmrExpand = new System.Windows.Forms.Timer(this.components);
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel5 = new AITool.DBLayoutPanel(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel25 = new AITool.DBLayoutPanel(this.components);
            this.btn_open_log = new System.Windows.Forms.Button();
            this.cb_log = new System.Windows.Forms.ComboBox();
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
            this.tbInput = new System.Windows.Forms.TextBox();
            this.btn_input_path = new System.Windows.Forms.Button();
            this.BtnSettingsSave = new System.Windows.Forms.Button();
            this.tabCameras = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel3 = new AITool.DBLayoutPanel(this.components);
            this.list2 = new System.Windows.Forms.ListView();
            this.btnCameraAdd = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel7 = new AITool.DBLayoutPanel(this.components);
            this.label21 = new System.Windows.Forms.Label();
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
            this.label5 = new System.Windows.Forms.Label();
            this.tb_cooldown = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_telegram = new System.Windows.Forms.CheckBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tableLayoutPanel12 = new AITool.DBLayoutPanel(this.components);
            this.tbPrefix = new System.Windows.Forms.TextBox();
            this.lbl_prefix = new System.Windows.Forms.Label();
            this.tableLayoutPanel13 = new AITool.DBLayoutPanel(this.components);
            this.tbName = new System.Windows.Forms.TextBox();
            this.cb_enabled = new System.Windows.Forms.CheckBox();
            this.lblRelevantObjects = new System.Windows.Forms.Label();
            this.lbl_threshold = new System.Windows.Forms.Label();
            this.tableLayoutPanel24 = new AITool.DBLayoutPanel(this.components);
            this.lbl_threshold_lower = new System.Windows.Forms.Label();
            this.tb_threshold_upper = new System.Windows.Forms.TextBox();
            this.lbl_threshold_upper = new System.Windows.Forms.Label();
            this.tb_threshold_lower = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel26 = new System.Windows.Forms.TableLayoutPanel();
            this.panMasking = new System.Windows.Forms.Panel();
            this.tableLayoutPanel29 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.num_history_mins = new System.Windows.Forms.NumericUpDown();
            this.num_mask_create = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.num_mask_remove = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.num_percent_var = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.cb_masking_enabled = new System.Windows.Forms.CheckBox();
            this.lblAdvSettings = new System.Windows.Forms.Label();
            this.tableLayoutPanel11 = new AITool.DBLayoutPanel(this.components);
            this.btnCameraSave = new System.Windows.Forms.Button();
            this.btnCameraDel = new System.Windows.Forms.Button();
            this.lbl_camstats = new System.Windows.Forms.Label();
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
            this.tabOverview = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel14 = new AITool.DBLayoutPanel(this.components);
            this.tableLayoutPanel15 = new AITool.DBLayoutPanel(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_version = new System.Windows.Forms.Label();
            this.lbl_errors = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSettings.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel25.SuspendLayout();
            this.tableLayoutPanel18.SuspendLayout();
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
            this.panMasking.SuspendLayout();
            this.tableLayoutPanel29.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_history_mins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mask_create)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mask_remove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_percent_var)).BeginInit();
            this.tableLayoutPanel11.SuspendLayout();
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
            this.tabStats.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tableLayoutPanel23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_confidence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeline)).BeginInit();
            this.tableLayoutPanel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabOverview.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabControl1.SuspendLayout();
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
            // tmrCollapse
            // 
            this.tmrCollapse.Interval = 10;
            this.tmrCollapse.Tick += new System.EventHandler(this.tmrCollapse_Tick);
            // 
            // tmrExpand
            // 
            this.tmrExpand.Interval = 10;
            this.tmrExpand.Tick += new System.EventHandler(this.tmrExpand_Tick);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tableLayoutPanel4);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(981, 566);
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
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.22615F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.773851F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(981, 566);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(975, 516);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(76, 374);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Log Level";
            // 
            // tableLayoutPanel25
            // 
            this.tableLayoutPanel25.ColumnCount = 2;
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel25.Controls.Add(this.btn_open_log, 1, 0);
            this.tableLayoutPanel25.Controls.Add(this.cb_log, 0, 0);
            this.tableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel25.Location = new System.Drawing.Point(149, 343);
            this.tableLayoutPanel25.Name = "tableLayoutPanel25";
            this.tableLayoutPanel25.RowCount = 1;
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel25.Size = new System.Drawing.Size(823, 79);
            this.tableLayoutPanel25.TabIndex = 14;
            // 
            // btn_open_log
            // 
            this.btn_open_log.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_open_log.Location = new System.Drawing.Point(744, 28);
            this.btn_open_log.Name = "btn_open_log";
            this.btn_open_log.Size = new System.Drawing.Size(74, 23);
            this.btn_open_log.TabIndex = 2;
            this.btn_open_log.Text = "Open Log";
            this.btn_open_log.UseVisualStyleBackColor = true;
            this.btn_open_log.Click += new System.EventHandler(this.btn_open_log_Click);
            // 
            // cb_log
            // 
            this.cb_log.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_log.FormattingEnabled = true;
            this.cb_log.Items.AddRange(new object[] {
            "Trace",
            "Debug",
            "Info",
            "Warn",
            "Error",
            "Off"});
            this.cb_log.Location = new System.Drawing.Point(3, 29);
            this.cb_log.Name = "cb_log";
            this.cb_log.Size = new System.Drawing.Size(121, 21);
            this.cb_log.TabIndex = 3;
            this.cb_log.SelectedValueChanged += new System.EventHandler(this.cb_log_SelectedValueChanged);
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(65, 462);
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
            this.cb_send_errors.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cb_send_errors.Location = new System.Drawing.Point(149, 428);
            this.cb_send_errors.Name = "cb_send_errors";
            this.cb_send_errors.Size = new System.Drawing.Size(823, 85);
            this.cb_send_errors.TabIndex = 12;
            this.cb_send_errors.Text = "Send Errors and Warnings to Telegram";
            this.cb_send_errors.UseVisualStyleBackColor = true;
            // 
            // lbl_deepstackurl
            // 
            this.lbl_deepstackurl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_deepstackurl.AutoSize = true;
            this.lbl_deepstackurl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_deepstackurl.Location = new System.Drawing.Point(44, 119);
            this.lbl_deepstackurl.Name = "lbl_deepstackurl";
            this.lbl_deepstackurl.Size = new System.Drawing.Size(99, 17);
            this.lbl_deepstackurl.TabIndex = 4;
            this.lbl_deepstackurl.Text = "Deepstack URL";
            // 
            // lbl_input
            // 
            this.lbl_input.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_input.AutoSize = true;
            this.lbl_input.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_input.Location = new System.Drawing.Point(70, 34);
            this.lbl_input.Name = "lbl_input";
            this.lbl_input.Size = new System.Drawing.Size(73, 17);
            this.lbl_input.TabIndex = 1;
            this.lbl_input.Text = "Input Path";
            // 
            // tbDeepstackUrl
            // 
            this.tbDeepstackUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDeepstackUrl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDeepstackUrl.Location = new System.Drawing.Point(149, 115);
            this.tbDeepstackUrl.Name = "tbDeepstackUrl";
            this.tbDeepstackUrl.Size = new System.Drawing.Size(823, 25);
            this.tbDeepstackUrl.TabIndex = 5;
            // 
            // lbl_telegram_token
            // 
            this.lbl_telegram_token.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_telegram_token.AutoSize = true;
            this.lbl_telegram_token.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_telegram_token.Location = new System.Drawing.Point(37, 204);
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
            this.lbl_telegram_chatid.Location = new System.Drawing.Point(28, 289);
            this.lbl_telegram_chatid.Name = "lbl_telegram_chatid";
            this.lbl_telegram_chatid.Size = new System.Drawing.Size(115, 17);
            this.lbl_telegram_chatid.TabIndex = 7;
            this.lbl_telegram_chatid.Text = "Telegram Chat ID";
            // 
            // tb_telegram_token
            // 
            this.tb_telegram_token.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_telegram_token.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_telegram_token.Location = new System.Drawing.Point(149, 200);
            this.tb_telegram_token.Name = "tb_telegram_token";
            this.tb_telegram_token.Size = new System.Drawing.Size(823, 25);
            this.tb_telegram_token.TabIndex = 8;
            // 
            // tb_telegram_chatid
            // 
            this.tb_telegram_chatid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_telegram_chatid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_telegram_chatid.Location = new System.Drawing.Point(149, 285);
            this.tb_telegram_chatid.Name = "tb_telegram_chatid";
            this.tb_telegram_chatid.Size = new System.Drawing.Size(823, 25);
            this.tb_telegram_chatid.TabIndex = 9;
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.ColumnCount = 2;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel18.Controls.Add(this.tbInput, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.btn_input_path, 1, 0);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(149, 3);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 1;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(823, 79);
            this.tableLayoutPanel18.TabIndex = 12;
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInput.Location = new System.Drawing.Point(3, 27);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(734, 25);
            this.tbInput.TabIndex = 1;
            // 
            // btn_input_path
            // 
            this.btn_input_path.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_input_path.Location = new System.Drawing.Point(752, 28);
            this.btn_input_path.Name = "btn_input_path";
            this.btn_input_path.Size = new System.Drawing.Size(59, 23);
            this.btn_input_path.TabIndex = 2;
            this.btn_input_path.Text = "Select...";
            this.btn_input_path.UseVisualStyleBackColor = true;
            this.btn_input_path.Click += new System.EventHandler(this.btn_input_path_Click);
            // 
            // BtnSettingsSave
            // 
            this.BtnSettingsSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.BtnSettingsSave.BackColor = System.Drawing.Color.Transparent;
            this.BtnSettingsSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSettingsSave.Location = new System.Drawing.Point(440, 525);
            this.BtnSettingsSave.Name = "BtnSettingsSave";
            this.BtnSettingsSave.Size = new System.Drawing.Size(100, 38);
            this.BtnSettingsSave.TabIndex = 2;
            this.BtnSettingsSave.TabStop = false;
            this.BtnSettingsSave.Text = "Save";
            this.BtnSettingsSave.UseVisualStyleBackColor = false;
            this.BtnSettingsSave.Click += new System.EventHandler(this.BtnSettingsSave_Click_1);
            // 
            // tabCameras
            // 
            this.tabCameras.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabCameras.Controls.Add(this.tableLayoutPanel2);
            this.tabCameras.Location = new System.Drawing.Point(4, 22);
            this.tabCameras.Name = "tabCameras";
            this.tabCameras.Padding = new System.Windows.Forms.Padding(3);
            this.tabCameras.Size = new System.Drawing.Size(981, 566);
            this.tabCameras.TabIndex = 2;
            this.tabCameras.Text = "Cameras";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0505F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.94949F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(975, 560);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.list2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnCameraAdd, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.05776F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.942238F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(140, 554);
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
            this.list2.Size = new System.Drawing.Size(134, 504);
            this.list2.TabIndex = 1;
            this.list2.UseCompatibleStateImageBehavior = false;
            this.list2.View = System.Windows.Forms.View.Details;
            this.list2.SelectedIndexChanged += new System.EventHandler(this.list2_SelectedIndexChanged);
            this.list2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.list2_KeyDown);
            // 
            // btnCameraAdd
            // 
            this.btnCameraAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCameraAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnCameraAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCameraAdd.Location = new System.Drawing.Point(20, 513);
            this.btnCameraAdd.Name = "btnCameraAdd";
            this.btnCameraAdd.Size = new System.Drawing.Size(100, 38);
            this.btnCameraAdd.TabIndex = 4;
            this.btnCameraAdd.Text = "Add Camera";
            this.btnCameraAdd.UseVisualStyleBackColor = false;
            this.btnCameraAdd.Click += new System.EventHandler(this.btnCameraAdd_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel11, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.lbl_camstats, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(149, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.317689F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.55957F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.303249F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(823, 554);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.21665F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.78336F));
            this.tableLayoutPanel7.Controls.Add(this.label21, 0, 5);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel9, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.lblPrefix, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel12, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel13, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.lblRelevantObjects, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.lbl_threshold, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel24, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel26, 1, 5);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 37);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 6;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.419106F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.879212F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.34261F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.993576F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.77302F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.47538F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(817, 467);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(53, 313);
            this.label21.Margin = new System.Windows.Forms.Padding(1, 8, 0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(104, 17);
            this.label21.TabIndex = 20;
            this.label21.Text = "Object Masking";
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
            this.tableLayoutPanel8.Location = new System.Drawing.Point(160, 88);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(654, 89);
            this.tableLayoutPanel8.TabIndex = 14;
            // 
            // cb_person
            // 
            this.cb_person.AutoSize = true;
            this.cb_person.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_person.Location = new System.Drawing.Point(3, 5);
            this.cb_person.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.cb_person.Name = "cb_person";
            this.cb_person.Size = new System.Drawing.Size(67, 21);
            this.cb_person.TabIndex = 0;
            this.cb_person.Text = "Person";
            this.cb_person.UseVisualStyleBackColor = true;
            // 
            // cb_bicycle
            // 
            this.cb_bicycle.AutoSize = true;
            this.cb_bicycle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_bicycle.Location = new System.Drawing.Point(3, 34);
            this.cb_bicycle.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.cb_bicycle.Name = "cb_bicycle";
            this.cb_bicycle.Size = new System.Drawing.Size(65, 21);
            this.cb_bicycle.TabIndex = 8;
            this.cb_bicycle.Text = "Bicycle";
            this.cb_bicycle.UseVisualStyleBackColor = true;
            // 
            // cb_motorcycle
            // 
            this.cb_motorcycle.AutoSize = true;
            this.cb_motorcycle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_motorcycle.Location = new System.Drawing.Point(3, 63);
            this.cb_motorcycle.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.cb_motorcycle.Name = "cb_motorcycle";
            this.cb_motorcycle.Size = new System.Drawing.Size(92, 21);
            this.cb_motorcycle.TabIndex = 10;
            this.cb_motorcycle.Text = "Motorcycle";
            this.cb_motorcycle.UseVisualStyleBackColor = true;
            // 
            // cb_bear
            // 
            this.cb_bear.AutoSize = true;
            this.cb_bear.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_bear.Location = new System.Drawing.Point(540, 63);
            this.cb_bear.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_bear.Name = "cb_bear";
            this.cb_bear.Size = new System.Drawing.Size(53, 21);
            this.cb_bear.TabIndex = 13;
            this.cb_bear.Text = "Bear";
            this.cb_bear.UseVisualStyleBackColor = true;
            // 
            // cb_cow
            // 
            this.cb_cow.AutoSize = true;
            this.cb_cow.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_cow.Location = new System.Drawing.Point(540, 34);
            this.cb_cow.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_cow.Name = "cb_cow";
            this.cb_cow.Size = new System.Drawing.Size(52, 21);
            this.cb_cow.TabIndex = 11;
            this.cb_cow.Text = "Cow";
            this.cb_cow.UseVisualStyleBackColor = true;
            // 
            // cb_sheep
            // 
            this.cb_sheep.AutoSize = true;
            this.cb_sheep.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_sheep.Location = new System.Drawing.Point(540, 5);
            this.cb_sheep.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_sheep.Name = "cb_sheep";
            this.cb_sheep.Size = new System.Drawing.Size(63, 21);
            this.cb_sheep.TabIndex = 9;
            this.cb_sheep.Text = "Sheep";
            this.cb_sheep.UseVisualStyleBackColor = true;
            // 
            // cb_horse
            // 
            this.cb_horse.AutoSize = true;
            this.cb_horse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_horse.Location = new System.Drawing.Point(410, 63);
            this.cb_horse.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_horse.Name = "cb_horse";
            this.cb_horse.Size = new System.Drawing.Size(62, 21);
            this.cb_horse.TabIndex = 7;
            this.cb_horse.Text = "Horse";
            this.cb_horse.UseVisualStyleBackColor = true;
            // 
            // cb_bird
            // 
            this.cb_bird.AutoSize = true;
            this.cb_bird.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_bird.Location = new System.Drawing.Point(410, 34);
            this.cb_bird.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_bird.Name = "cb_bird";
            this.cb_bird.Size = new System.Drawing.Size(50, 21);
            this.cb_bird.TabIndex = 5;
            this.cb_bird.Text = "Bird";
            this.cb_bird.UseVisualStyleBackColor = true;
            // 
            // cb_dog
            // 
            this.cb_dog.AutoSize = true;
            this.cb_dog.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_dog.Location = new System.Drawing.Point(410, 5);
            this.cb_dog.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_dog.Name = "cb_dog";
            this.cb_dog.Size = new System.Drawing.Size(52, 21);
            this.cb_dog.TabIndex = 3;
            this.cb_dog.Text = "Dog";
            this.cb_dog.UseVisualStyleBackColor = true;
            // 
            // cb_cat
            // 
            this.cb_cat.AutoSize = true;
            this.cb_cat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_cat.Location = new System.Drawing.Point(280, 63);
            this.cb_cat.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_cat.Name = "cb_cat";
            this.cb_cat.Size = new System.Drawing.Size(46, 21);
            this.cb_cat.TabIndex = 1;
            this.cb_cat.Text = "Cat";
            this.cb_cat.UseVisualStyleBackColor = true;
            // 
            // cb_airplane
            // 
            this.cb_airplane.AutoSize = true;
            this.cb_airplane.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_airplane.Location = new System.Drawing.Point(280, 34);
            this.cb_airplane.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_airplane.Name = "cb_airplane";
            this.cb_airplane.Size = new System.Drawing.Size(75, 21);
            this.cb_airplane.TabIndex = 14;
            this.cb_airplane.Text = "Airplane";
            this.cb_airplane.UseVisualStyleBackColor = true;
            // 
            // cb_boat
            // 
            this.cb_boat.AutoSize = true;
            this.cb_boat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_boat.Location = new System.Drawing.Point(280, 5);
            this.cb_boat.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_boat.Name = "cb_boat";
            this.cb_boat.Size = new System.Drawing.Size(53, 21);
            this.cb_boat.TabIndex = 12;
            this.cb_boat.Text = "Boat";
            this.cb_boat.UseVisualStyleBackColor = true;
            // 
            // cb_bus
            // 
            this.cb_bus.AutoSize = true;
            this.cb_bus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_bus.Location = new System.Drawing.Point(150, 63);
            this.cb_bus.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_bus.Name = "cb_bus";
            this.cb_bus.Size = new System.Drawing.Size(47, 21);
            this.cb_bus.TabIndex = 4;
            this.cb_bus.Text = "Bus";
            this.cb_bus.UseVisualStyleBackColor = true;
            // 
            // cb_truck
            // 
            this.cb_truck.AutoSize = true;
            this.cb_truck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_truck.Location = new System.Drawing.Point(150, 34);
            this.cb_truck.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_truck.Name = "cb_truck";
            this.cb_truck.Size = new System.Drawing.Size(57, 21);
            this.cb_truck.TabIndex = 6;
            this.cb_truck.Text = "Truck";
            this.cb_truck.UseVisualStyleBackColor = true;
            // 
            // cb_car
            // 
            this.cb_car.AutoSize = true;
            this.cb_car.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_car.Location = new System.Drawing.Point(150, 5);
            this.cb_car.Margin = new System.Windows.Forms.Padding(20, 5, 3, 3);
            this.cb_car.Name = "cb_car";
            this.cb_car.Size = new System.Drawing.Size(47, 21);
            this.cb_car.TabIndex = 2;
            this.cb_car.Text = "Car";
            this.cb_car.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(100, 236);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 14, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Actions";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel20, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(160, 225);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(654, 77);
            this.tableLayoutPanel9.TabIndex = 8;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.9582F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.04179F));
            this.tableLayoutPanel10.Controls.Add(this.tbTriggerUrl, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblTriggerUrl, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 41);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(648, 33);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // tbTriggerUrl
            // 
            this.tbTriggerUrl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTriggerUrl.Location = new System.Drawing.Point(129, 3);
            this.tbTriggerUrl.Margin = new System.Windows.Forms.Padding(0, 3, 20, 3);
            this.tbTriggerUrl.Name = "tbTriggerUrl";
            this.tbTriggerUrl.Size = new System.Drawing.Size(496, 25);
            this.tbTriggerUrl.TabIndex = 0;
            this.tbTriggerUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTriggerUrl_KeyDown);
            this.tbTriggerUrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbTriggerUrl_KeyUp);
            // 
            // lblTriggerUrl
            // 
            this.lblTriggerUrl.AutoSize = true;
            this.lblTriggerUrl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTriggerUrl.Location = new System.Drawing.Point(0, 6);
            this.lblTriggerUrl.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblTriggerUrl.MinimumSize = new System.Drawing.Size(80, 0);
            this.lblTriggerUrl.Name = "lblTriggerUrl";
            this.lblTriggerUrl.Size = new System.Drawing.Size(91, 17);
            this.lblTriggerUrl.TabIndex = 1;
            this.lblTriggerUrl.Text = "Trigger URL(s)";
            // 
            // tableLayoutPanel20
            // 
            this.tableLayoutPanel20.ColumnCount = 4;
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.85371F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.210032F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.65217F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.48447F));
            this.tableLayoutPanel20.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel20.Controls.Add(this.tb_cooldown, 1, 0);
            this.tableLayoutPanel20.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel20.Controls.Add(this.cb_telegram, 3, 0);
            this.tableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel20.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel20.Name = "tableLayoutPanel20";
            this.tableLayoutPanel20.RowCount = 1;
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel20.Size = new System.Drawing.Size(648, 32);
            this.tableLayoutPanel20.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 7, 3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cooldown Time";
            // 
            // tb_cooldown
            // 
            this.tb_cooldown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_cooldown.Location = new System.Drawing.Point(128, 3);
            this.tb_cooldown.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.tb_cooldown.MaxLength = 3;
            this.tb_cooldown.Name = "tb_cooldown";
            this.tb_cooldown.Size = new System.Drawing.Size(41, 25);
            this.tb_cooldown.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(177, 7);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "minutes";
            // 
            // cb_telegram
            // 
            this.cb_telegram.AutoSize = true;
            this.cb_telegram.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cb_telegram.Location = new System.Drawing.Point(309, 7);
            this.cb_telegram.Margin = new System.Windows.Forms.Padding(2, 7, 2, 2);
            this.cb_telegram.Name = "cb_telegram";
            this.cb_telegram.Size = new System.Drawing.Size(206, 21);
            this.cb_telegram.TabIndex = 3;
            this.cb_telegram.Text = "Send alert images to Telegram";
            this.cb_telegram.UseVisualStyleBackColor = true;
            // 
            // lblPrefix
            // 
            this.lblPrefix.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrefix.Location = new System.Drawing.Point(13, 56);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(141, 17);
            this.lblPrefix.TabIndex = 2;
            this.lblPrefix.Text = "Input file begins with";
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(110, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 17);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "Name";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Controls.Add(this.tbPrefix, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.lbl_prefix, 1, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(160, 47);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 1;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(654, 35);
            this.tableLayoutPanel12.TabIndex = 12;
            // 
            // tbPrefix
            // 
            this.tbPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPrefix.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPrefix.Location = new System.Drawing.Point(3, 5);
            this.tbPrefix.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tbPrefix.Name = "tbPrefix";
            this.tbPrefix.Size = new System.Drawing.Size(324, 25);
            this.tbPrefix.TabIndex = 5;
            this.tbPrefix.TextChanged += new System.EventHandler(this.tbPrefix_TextChanged);
            // 
            // lbl_prefix
            // 
            this.lbl_prefix.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_prefix.AutoSize = true;
            this.lbl_prefix.Location = new System.Drawing.Point(490, 11);
            this.lbl_prefix.Name = "lbl_prefix";
            this.lbl_prefix.Size = new System.Drawing.Size(0, 13);
            this.lbl_prefix.TabIndex = 6;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.Controls.Add(this.tbName, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.cb_enabled, 1, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(160, 3);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(654, 38);
            this.tableLayoutPanel13.TabIndex = 13;
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.Location = new System.Drawing.Point(3, 6);
            this.tbName.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(324, 25);
            this.tbName.TabIndex = 12;
            // 
            // cb_enabled
            // 
            this.cb_enabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_enabled.AutoSize = true;
            this.cb_enabled.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_enabled.Location = new System.Drawing.Point(347, 8);
            this.cb_enabled.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.cb_enabled.Name = "cb_enabled";
            this.cb_enabled.Size = new System.Drawing.Size(232, 21);
            this.cb_enabled.TabIndex = 13;
            this.cb_enabled.Text = "Enable AI Detection for this camera";
            this.cb_enabled.UseVisualStyleBackColor = true;
            // 
            // lblRelevantObjects
            // 
            this.lblRelevantObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRelevantObjects.AutoSize = true;
            this.lblRelevantObjects.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRelevantObjects.Location = new System.Drawing.Point(43, 93);
            this.lblRelevantObjects.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lblRelevantObjects.Name = "lblRelevantObjects";
            this.lblRelevantObjects.Size = new System.Drawing.Size(111, 17);
            this.lblRelevantObjects.TabIndex = 1;
            this.lblRelevantObjects.Text = "Relevant Objects";
            // 
            // lbl_threshold
            // 
            this.lbl_threshold.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_threshold.AutoSize = true;
            this.lbl_threshold.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_threshold.Location = new System.Drawing.Point(38, 195);
            this.lbl_threshold.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lbl_threshold.Name = "lbl_threshold";
            this.lbl_threshold.Size = new System.Drawing.Size(116, 17);
            this.lbl_threshold.TabIndex = 15;
            this.lbl_threshold.Text = "Confidence limits";
            // 
            // tableLayoutPanel24
            // 
            this.tableLayoutPanel24.ColumnCount = 6;
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.880734F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.798165F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.99694F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.574924F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.71865F));
            this.tableLayoutPanel24.Controls.Add(this.lbl_threshold_lower, 0, 0);
            this.tableLayoutPanel24.Controls.Add(this.tb_threshold_upper, 4, 0);
            this.tableLayoutPanel24.Controls.Add(this.lbl_threshold_upper, 3, 0);
            this.tableLayoutPanel24.Controls.Add(this.tb_threshold_lower, 1, 0);
            this.tableLayoutPanel24.Controls.Add(this.label9, 5, 0);
            this.tableLayoutPanel24.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel24.Location = new System.Drawing.Point(160, 183);
            this.tableLayoutPanel24.Name = "tableLayoutPanel24";
            this.tableLayoutPanel24.RowCount = 1;
            this.tableLayoutPanel24.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel24.Size = new System.Drawing.Size(654, 36);
            this.tableLayoutPanel24.TabIndex = 16;
            // 
            // lbl_threshold_lower
            // 
            this.lbl_threshold_lower.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_threshold_lower.AutoSize = true;
            this.lbl_threshold_lower.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_threshold_lower.Location = new System.Drawing.Point(1, 10);
            this.lbl_threshold_lower.Margin = new System.Windows.Forms.Padding(1, 5, 3, 3);
            this.lbl_threshold_lower.Name = "lbl_threshold_lower";
            this.lbl_threshold_lower.Size = new System.Drawing.Size(71, 17);
            this.lbl_threshold_lower.TabIndex = 19;
            this.lbl_threshold_lower.Text = "Lower limit";
            // 
            // tb_threshold_upper
            // 
            this.tb_threshold_upper.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tb_threshold_upper.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_threshold_upper.Location = new System.Drawing.Point(311, 6);
            this.tb_threshold_upper.Margin = new System.Windows.Forms.Padding(0, 5, 3, 3);
            this.tb_threshold_upper.MaxLength = 3;
            this.tb_threshold_upper.Name = "tb_threshold_upper";
            this.tb_threshold_upper.Size = new System.Drawing.Size(40, 25);
            this.tb_threshold_upper.TabIndex = 20;
            this.tb_threshold_upper.Leave += new System.EventHandler(this.tb_threshold_upper_Leave);
            // 
            // lbl_threshold_upper
            // 
            this.lbl_threshold_upper.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_threshold_upper.AutoSize = true;
            this.lbl_threshold_upper.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_threshold_upper.Location = new System.Drawing.Point(229, 12);
            this.lbl_threshold_upper.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lbl_threshold_upper.Name = "lbl_threshold_upper";
            this.lbl_threshold_upper.Size = new System.Drawing.Size(73, 17);
            this.lbl_threshold_upper.TabIndex = 21;
            this.lbl_threshold_upper.Text = "Upper limit";
            // 
            // tb_threshold_lower
            // 
            this.tb_threshold_lower.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tb_threshold_lower.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_threshold_lower.Location = new System.Drawing.Point(130, 6);
            this.tb_threshold_lower.Margin = new System.Windows.Forms.Padding(0, 5, 3, 3);
            this.tb_threshold_lower.MaxLength = 3;
            this.tb_threshold_lower.Name = "tb_threshold_lower";
            this.tb_threshold_lower.Size = new System.Drawing.Size(42, 25);
            this.tb_threshold_lower.TabIndex = 18;
            this.tb_threshold_lower.Leave += new System.EventHandler(this.tb_threshold_lower_Leave);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(357, 12);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "%";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(178, 12);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 17);
            this.label10.TabIndex = 23;
            this.label10.Text = "%";
            // 
            // tableLayoutPanel26
            // 
            this.tableLayoutPanel26.ColumnCount = 1;
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel26.Controls.Add(this.panMasking, 0, 2);
            this.tableLayoutPanel26.Controls.Add(this.cb_masking_enabled, 0, 0);
            this.tableLayoutPanel26.Controls.Add(this.lblAdvSettings, 0, 1);
            this.tableLayoutPanel26.Location = new System.Drawing.Point(159, 307);
            this.tableLayoutPanel26.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel26.Name = "tableLayoutPanel26";
            this.tableLayoutPanel26.RowCount = 3;
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel26.Size = new System.Drawing.Size(650, 153);
            this.tableLayoutPanel26.TabIndex = 18;
            // 
            // panMasking
            // 
            this.panMasking.Controls.Add(this.tableLayoutPanel29);
            this.panMasking.Location = new System.Drawing.Point(0, 39);
            this.panMasking.Margin = new System.Windows.Forms.Padding(0);
            this.panMasking.MaximumSize = new System.Drawing.Size(662, 112);
            this.panMasking.Name = "panMasking";
            this.panMasking.Size = new System.Drawing.Size(650, 112);
            this.panMasking.TabIndex = 1;
            // 
            // tableLayoutPanel29
            // 
            this.tableLayoutPanel29.ColumnCount = 3;
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.84342F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.620529F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.65163F));
            this.tableLayoutPanel29.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel29.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel29.Controls.Add(this.label14, 0, 1);
            this.tableLayoutPanel29.Controls.Add(this.num_history_mins, 1, 0);
            this.tableLayoutPanel29.Controls.Add(this.num_mask_create, 1, 1);
            this.tableLayoutPanel29.Controls.Add(this.label15, 2, 1);
            this.tableLayoutPanel29.Controls.Add(this.label16, 2, 0);
            this.tableLayoutPanel29.Controls.Add(this.num_mask_remove, 1, 2);
            this.tableLayoutPanel29.Controls.Add(this.label17, 2, 2);
            this.tableLayoutPanel29.Controls.Add(this.label18, 0, 3);
            this.tableLayoutPanel29.Controls.Add(this.num_percent_var, 1, 3);
            this.tableLayoutPanel29.Controls.Add(this.label19, 2, 3);
            this.tableLayoutPanel29.Location = new System.Drawing.Point(1, 0);
            this.tableLayoutPanel29.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel29.Name = "tableLayoutPanel29";
            this.tableLayoutPanel29.RowCount = 4;
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel29.Size = new System.Drawing.Size(643, 110);
            this.tableLayoutPanel29.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label11.Location = new System.Drawing.Point(3, 59);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 17);
            this.label11.TabIndex = 3;
            this.label11.Text = "Remove mask after ";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label13.Location = new System.Drawing.Point(3, 5);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(135, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Clear object history in";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label14.Location = new System.Drawing.Point(3, 32);
            this.label14.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(111, 17);
            this.label14.TabIndex = 1;
            this.label14.Text = "Create mask after";
            // 
            // num_history_mins
            // 
            this.num_history_mins.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_history_mins.Location = new System.Drawing.Point(161, 3);
            this.num_history_mins.Margin = new System.Windows.Forms.Padding(2);
            this.num_history_mins.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.num_history_mins.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_history_mins.Name = "num_history_mins";
            this.num_history_mins.Size = new System.Drawing.Size(44, 20);
            this.num_history_mins.TabIndex = 6;
            this.num_history_mins.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_history_mins.Leave += new System.EventHandler(this.num_history_mins_Leave);
            // 
            // num_mask_create
            // 
            this.num_mask_create.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_mask_create.Location = new System.Drawing.Point(161, 30);
            this.num_mask_create.Margin = new System.Windows.Forms.Padding(2);
            this.num_mask_create.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.num_mask_create.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_mask_create.Name = "num_mask_create";
            this.num_mask_create.Size = new System.Drawing.Size(44, 20);
            this.num_mask_create.TabIndex = 7;
            this.num_mask_create.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_mask_create.Leave += new System.EventHandler(this.num_mask_create_Leave);
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label15.Location = new System.Drawing.Point(209, 32);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(407, 17);
            this.label15.TabIndex = 8;
            this.label15.Text = "detection(s).  Number of history detections needed to create a mask";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label16.Location = new System.Drawing.Point(209, 5);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(352, 17);
            this.label16.TabIndex = 9;
            this.label16.Text = "minute(s).   Clears list of objects detected in same location ";
            // 
            // num_mask_remove
            // 
            this.num_mask_remove.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_mask_remove.Location = new System.Drawing.Point(161, 57);
            this.num_mask_remove.Margin = new System.Windows.Forms.Padding(2);
            this.num_mask_remove.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_mask_remove.Name = "num_mask_remove";
            this.num_mask_remove.Size = new System.Drawing.Size(44, 20);
            this.num_mask_remove.TabIndex = 10;
            this.num_mask_remove.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_mask_remove.Leave += new System.EventHandler(this.num_mask_remove_Leave);
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label17.Location = new System.Drawing.Point(209, 59);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(110, 17);
            this.label17.TabIndex = 11;
            this.label17.Text = "time(s) not visible";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label18.Location = new System.Drawing.Point(3, 87);
            this.label18.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(113, 17);
            this.label18.TabIndex = 12;
            this.label18.Text = "Object variance %";
            // 
            // num_percent_var
            // 
            this.num_percent_var.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_percent_var.Location = new System.Drawing.Point(161, 85);
            this.num_percent_var.Margin = new System.Windows.Forms.Padding(2);
            this.num_percent_var.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.num_percent_var.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.num_percent_var.Name = "num_percent_var";
            this.num_percent_var.Size = new System.Drawing.Size(44, 20);
            this.num_percent_var.TabIndex = 13;
            this.num_percent_var.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.num_percent_var.Leave += new System.EventHandler(this.num_percent_var_Leave);
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label19.Location = new System.Drawing.Point(209, 87);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(354, 17);
            this.label19.TabIndex = 14;
            this.label19.Text = "percent.  Adjusts for variations in object\'s detected location";
            // 
            // cb_masking_enabled
            // 
            this.cb_masking_enabled.AutoSize = true;
            this.cb_masking_enabled.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cb_masking_enabled.Location = new System.Drawing.Point(3, 6);
            this.cb_masking_enabled.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
            this.cb_masking_enabled.Name = "cb_masking_enabled";
            this.cb_masking_enabled.Size = new System.Drawing.Size(170, 21);
            this.cb_masking_enabled.TabIndex = 0;
            this.cb_masking_enabled.Text = "Enable dynamic masking";
            this.cb_masking_enabled.UseVisualStyleBackColor = true;
            // 
            // lblAdvSettings
            // 
            this.lblAdvSettings.AutoSize = true;
            this.lblAdvSettings.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblAdvSettings.Location = new System.Drawing.Point(15, 27);
            this.lblAdvSettings.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lblAdvSettings.Name = "lblAdvSettings";
            this.lblAdvSettings.Size = new System.Drawing.Size(134, 12);
            this.lblAdvSettings.TabIndex = 2;
            this.lblAdvSettings.Text = "+ Show Advanced settings";
            this.lblAdvSettings.Click += new System.EventHandler(this.lblAdvSettings_Click);
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.btnCameraSave, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.btnCameraDel, 1, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 510);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(817, 41);
            this.tableLayoutPanel11.TabIndex = 3;
            // 
            // btnCameraSave
            // 
            this.btnCameraSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCameraSave.BackColor = System.Drawing.Color.Transparent;
            this.btnCameraSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCameraSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCameraSave.Location = new System.Drawing.Point(154, 3);
            this.btnCameraSave.Name = "btnCameraSave";
            this.btnCameraSave.Size = new System.Drawing.Size(100, 35);
            this.btnCameraSave.TabIndex = 4;
            this.btnCameraSave.Text = "Save";
            this.btnCameraSave.UseVisualStyleBackColor = false;
            this.btnCameraSave.Click += new System.EventHandler(this.btnCameraSave_Click_1);
            // 
            // btnCameraDel
            // 
            this.btnCameraDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCameraDel.BackColor = System.Drawing.Color.Transparent;
            this.btnCameraDel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCameraDel.Location = new System.Drawing.Point(555, 3);
            this.btnCameraDel.Name = "btnCameraDel";
            this.btnCameraDel.Size = new System.Drawing.Size(115, 35);
            this.btnCameraDel.TabIndex = 5;
            this.btnCameraDel.Text = " Delete Camera ";
            this.btnCameraDel.UseVisualStyleBackColor = false;
            this.btnCameraDel.Click += new System.EventHandler(this.btnCameraDel_Click);
            // 
            // lbl_camstats
            // 
            this.lbl_camstats.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_camstats.AutoSize = true;
            this.lbl_camstats.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_camstats.Location = new System.Drawing.Point(3, 8);
            this.lbl_camstats.Name = "lbl_camstats";
            this.lbl_camstats.Size = new System.Drawing.Size(36, 17);
            this.lbl_camstats.TabIndex = 4;
            this.lbl_camstats.Text = "Stats";
            // 
            // tabHistory
            // 
            this.tabHistory.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabHistory.Controls.Add(this.tableLayoutPanel1);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(981, 566);
            this.tabHistory.TabIndex = 0;
            this.tabHistory.Text = "History";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(975, 560);
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
            this.tableLayoutPanel21.Location = new System.Drawing.Point(295, 3);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            this.tableLayoutPanel21.RowCount = 2;
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel21.Size = new System.Drawing.Size(677, 554);
            this.tableLayoutPanel21.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(671, 487);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
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
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel22.Controls.Add(this.cb_showObjects, 0, 0);
            this.tableLayoutPanel22.Controls.Add(this.cb_showMask, 0, 0);
            this.tableLayoutPanel22.Controls.Add(this.lbl_objects, 2, 0);
            this.tableLayoutPanel22.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel22.Name = "tableLayoutPanel22";
            this.tableLayoutPanel22.RowCount = 1;
            this.tableLayoutPanel22.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel22.Size = new System.Drawing.Size(671, 55);
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
            this.cb_showObjects.Location = new System.Drawing.Point(137, 14);
            this.cb_showObjects.Name = "cb_showObjects";
            this.cb_showObjects.Size = new System.Drawing.Size(128, 27);
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
            this.cb_showMask.Location = new System.Drawing.Point(3, 14);
            this.cb_showMask.Name = "cb_showMask";
            this.cb_showMask.Size = new System.Drawing.Size(128, 27);
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
            this.lbl_objects.Location = new System.Drawing.Point(271, 0);
            this.lbl_objects.Name = "lbl_objects";
            this.lbl_objects.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lbl_objects.Size = new System.Drawing.Size(397, 55);
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
            this.splitContainer1.Size = new System.Drawing.Size(286, 554);
            this.splitContainer1.SplitterDistance = 312;
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
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(286, 312);
            this.tableLayoutPanel19.TabIndex = 0;
            // 
            // cb_showFilters
            // 
            this.cb_showFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_showFilters.Appearance = System.Windows.Forms.Appearance.Button;
            this.cb_showFilters.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_showFilters.Location = new System.Drawing.Point(3, 285);
            this.cb_showFilters.MinimumSize = new System.Drawing.Size(0, 27);
            this.cb_showFilters.Name = "cb_showFilters";
            this.cb_showFilters.Size = new System.Drawing.Size(280, 27);
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
            this.list1.Size = new System.Drawing.Size(280, 276);
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
            this.panel1.Size = new System.Drawing.Size(280, 232);
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
            this.comboBox_filter_camera.Size = new System.Drawing.Size(278, 25);
            this.comboBox_filter_camera.TabIndex = 2;
            this.comboBox_filter_camera.SelectedIndexChanged += new System.EventHandler(this.comboBox_filter_camera_SelectedIndexChanged);
            // 
            // cb_filter_nosuccess
            // 
            this.cb_filter_nosuccess.AutoSize = true;
            this.cb_filter_nosuccess.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_filter_nosuccess.Location = new System.Drawing.Point(6, 64);
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
            this.cb_filter_success.Location = new System.Drawing.Point(6, 37);
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
            this.cb_filter_person.Location = new System.Drawing.Point(6, 91);
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
            this.cb_filter_vehicle.Location = new System.Drawing.Point(6, 118);
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
            this.cb_filter_animal.Location = new System.Drawing.Point(6, 145);
            this.cb_filter_animal.Name = "cb_filter_animal";
            this.cb_filter_animal.Size = new System.Drawing.Size(162, 21);
            this.cb_filter_animal.TabIndex = 0;
            this.cb_filter_animal.Text = "only alerts with animals";
            this.cb_filter_animal.UseVisualStyleBackColor = true;
            this.cb_filter_animal.CheckedChanged += new System.EventHandler(this.cb_filter_animal_CheckedChanged);
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.tableLayoutPanel16);
            this.tabStats.Location = new System.Drawing.Point(4, 22);
            this.tabStats.Name = "tabStats";
            this.tabStats.Size = new System.Drawing.Size(981, 566);
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
            this.tableLayoutPanel16.Size = new System.Drawing.Size(981, 566);
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
            this.tableLayoutPanel23.Location = new System.Drawing.Point(297, 3);
            this.tableLayoutPanel23.Name = "tableLayoutPanel23";
            this.tableLayoutPanel23.RowCount = 3;
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel23.Size = new System.Drawing.Size(681, 560);
            this.tableLayoutPanel23.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 283);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(675, 27);
            this.label8.TabIndex = 9;
            this.label8.Text = "Frequencies of alert result confidences";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chart_confidence
            // 
            this.chart_confidence.BackColor = System.Drawing.Color.Transparent;
            this.chart_confidence.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea34.AxisX.Interval = 10D;
            chartArea34.AxisX.MajorGrid.Interval = 6D;
            chartArea34.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea34.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea34.AxisX.MajorTickMark.Interval = 1D;
            chartArea34.AxisX.Maximum = 100D;
            chartArea34.AxisX.Minimum = 0D;
            chartArea34.AxisX.Title = "Alert confidence";
            chartArea34.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea34.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea34.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea34.AxisY.Title = "Frequency";
            chartArea34.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea34.BackColor = System.Drawing.Color.Transparent;
            chartArea34.Name = "ChartArea1";
            this.chart_confidence.ChartAreas.Add(chartArea34);
            this.chart_confidence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_confidence.Location = new System.Drawing.Point(3, 316);
            this.chart_confidence.Name = "chart_confidence";
            series78.BorderWidth = 4;
            series78.ChartArea = "ChartArea1";
            series78.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series78.Color = System.Drawing.Color.Orange;
            series78.Name = "no alert";
            series79.BorderWidth = 3;
            series79.ChartArea = "ChartArea1";
            series79.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series79.Color = System.Drawing.Color.Green;
            series79.Legend = "Legend1";
            series79.Name = "alert";
            this.chart_confidence.Series.Add(series78);
            this.chart_confidence.Series.Add(series79);
            this.chart_confidence.Size = new System.Drawing.Size(675, 241);
            this.chart_confidence.TabIndex = 8;
            // 
            // timeline
            // 
            this.timeline.BackColor = System.Drawing.Color.Transparent;
            this.timeline.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea35.AxisX.Interval = 3D;
            chartArea35.AxisX.MajorGrid.Interval = 6D;
            chartArea35.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea35.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea35.AxisX.MajorTickMark.Interval = 1D;
            chartArea35.AxisX.Maximum = 24D;
            chartArea35.AxisX.Minimum = 0D;
            chartArea35.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea35.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea35.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea35.AxisY.Title = "Number";
            chartArea35.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea35.BackColor = System.Drawing.Color.Transparent;
            chartArea35.Name = "ChartArea1";
            this.timeline.ChartAreas.Add(chartArea35);
            this.timeline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeline.Location = new System.Drawing.Point(3, 36);
            this.timeline.Name = "timeline";
            series80.ChartArea = "ChartArea1";
            series80.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
            series80.Color = System.Drawing.Color.Silver;
            series80.Legend = "Legend1";
            series80.Name = "all";
            series81.BorderWidth = 3;
            series81.ChartArea = "ChartArea1";
            series81.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series81.Color = System.Drawing.Color.OrangeRed;
            series81.Legend = "Legend1";
            series81.Name = "falses";
            series82.BorderWidth = 3;
            series82.ChartArea = "ChartArea1";
            series82.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series82.Color = System.Drawing.Color.Orange;
            series82.Legend = "Legend1";
            series82.Name = "irrelevant";
            series83.BorderWidth = 4;
            series83.ChartArea = "ChartArea1";
            series83.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series83.Color = System.Drawing.Color.Green;
            series83.Legend = "Legend1";
            series83.Name = "relevant";
            this.timeline.Series.Add(series80);
            this.timeline.Series.Add(series81);
            this.timeline.Series.Add(series82);
            this.timeline.Series.Add(series83);
            this.timeline.Size = new System.Drawing.Size(675, 241);
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
            this.label7.Size = new System.Drawing.Size(675, 27);
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
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(288, 560);
            this.tableLayoutPanel17.TabIndex = 3;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea36.Area3DStyle.Enable3D = true;
            chartArea36.Area3DStyle.Inclination = 35;
            chartArea36.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.None;
            chartArea36.BackColor = System.Drawing.Color.Transparent;
            chartArea36.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea36);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend12.Alignment = System.Drawing.StringAlignment.Center;
            legend12.BackColor = System.Drawing.Color.Transparent;
            legend12.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend12.IsTextAutoFit = false;
            legend12.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
            legend12.Name = "Legend1";
            this.chart1.Legends.Add(legend12);
            this.chart1.Location = new System.Drawing.Point(3, 36);
            this.chart1.Name = "chart1";
            series84.ChartArea = "ChartArea1";
            series84.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series84.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series84.IsValueShownAsLabel = true;
            series84.Legend = "Legend1";
            series84.Name = "s1";
            dataPoint34.IsVisibleInLegend = true;
            series84.Points.Add(dataPoint34);
            series84.Points.Add(dataPoint35);
            series84.Points.Add(dataPoint36);
            this.chart1.Series.Add(series84);
            this.chart1.Size = new System.Drawing.Size(282, 521);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            title12.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title12.Name = "Title1";
            title12.Text = "Input Rates";
            this.chart1.Titles.Add(title12);
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
            this.comboBox1.Size = new System.Drawing.Size(282, 25);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // tabOverview
            // 
            this.tabOverview.Controls.Add(this.tableLayoutPanel14);
            this.tabOverview.Location = new System.Drawing.Point(4, 22);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Size = new System.Drawing.Size(981, 566);
            this.tabOverview.TabIndex = 4;
            this.tabOverview.Text = "Overview";
            this.tabOverview.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel14.ColumnCount = 1;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel14.Controls.Add(this.tableLayoutPanel15, 0, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(981, 566);
            this.tableLayoutPanel14.TabIndex = 3;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 1;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Controls.Add(this.pictureBox2, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel15.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel15.Controls.Add(this.lbl_version, 0, 4);
            this.tableLayoutPanel15.Controls.Add(this.lbl_errors, 0, 3);
            this.tableLayoutPanel15.Controls.Add(this.lbl_info, 0, 4);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 5;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.5F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.5F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(973, 558);
            this.tableLayoutPanel15.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = global::AITool.Properties.Resources.logo;
            this.pictureBox2.Location = new System.Drawing.Point(3, 139);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(967, 124);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(3, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(967, 92);
            this.label2.TabIndex = 3;
            this.label2.Text = "Running";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(60, 266);
            this.label3.Margin = new System.Windows.Forms.Padding(60, 0, 60, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(853, 2);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // lbl_version
            // 
            this.lbl_version.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_version.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_version.Location = new System.Drawing.Point(3, 536);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(967, 22);
            this.lbl_version.TabIndex = 6;
            this.lbl_version.Text = "Version 1.67 preview 7";
            this.lbl_version.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lbl_errors
            // 
            this.lbl_errors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_errors.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_errors.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_errors.Location = new System.Drawing.Point(3, 432);
            this.lbl_errors.Name = "lbl_errors";
            this.lbl_errors.Size = new System.Drawing.Size(967, 78);
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
            this.lbl_info.Location = new System.Drawing.Point(3, 510);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(967, 26);
            this.lbl_info.TabIndex = 8;
            this.lbl_info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabOverview);
            this.tabControl1.Controls.Add(this.tabStats);
            this.tabControl1.Controls.Add(this.tabHistory);
            this.tabControl1.Controls.Add(this.tabCameras);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(989, 592);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // Shell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(989, 592);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1005, 631);
            this.Name = "Shell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AI Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Shell_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tabSettings.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel25.ResumeLayout(false);
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel18.PerformLayout();
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
            this.panMasking.ResumeLayout(false);
            this.tableLayoutPanel29.ResumeLayout(false);
            this.tableLayoutPanel29.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_history_mins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mask_create)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mask_remove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_percent_var)).EndInit();
            this.tableLayoutPanel11.ResumeLayout(false);
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
            this.tabStats.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel23.ResumeLayout(false);
            this.tableLayoutPanel23.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_confidence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeline)).EndInit();
            this.tableLayoutPanel17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabOverview.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer tmrCollapse;
        private System.Windows.Forms.Timer tmrExpand;
        private System.Windows.Forms.TabPage tabSettings;
        private DBLayoutPanel tableLayoutPanel4;
        private DBLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label4;
        private DBLayoutPanel tableLayoutPanel25;
        private System.Windows.Forms.Button btn_open_log;
        private System.Windows.Forms.ComboBox cb_log;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cb_send_errors;
        private System.Windows.Forms.Label lbl_deepstackurl;
        private System.Windows.Forms.Label lbl_input;
        private System.Windows.Forms.TextBox tbDeepstackUrl;
        private System.Windows.Forms.Label lbl_telegram_token;
        private System.Windows.Forms.Label lbl_telegram_chatid;
        private System.Windows.Forms.TextBox tb_telegram_token;
        private System.Windows.Forms.TextBox tb_telegram_chatid;
        private DBLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Button btn_input_path;
        private System.Windows.Forms.Button BtnSettingsSave;
        private System.Windows.Forms.TabPage tabCameras;
        private DBLayoutPanel tableLayoutPanel2;
        private DBLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ListView list2;
        private System.Windows.Forms.Button btnCameraAdd;
        private DBLayoutPanel tableLayoutPanel6;
        private DBLayoutPanel tableLayoutPanel7;
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
        private System.Windows.Forms.Label label1;
        private DBLayoutPanel tableLayoutPanel9;
        private DBLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Label lblTriggerUrl;
        private System.Windows.Forms.TextBox tbTriggerUrl;
        private DBLayoutPanel tableLayoutPanel20;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_cooldown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cb_telegram;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.Label lblName;
        private DBLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.TextBox tbPrefix;
        private System.Windows.Forms.Label lbl_prefix;
        private DBLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.CheckBox cb_enabled;
        private System.Windows.Forms.Label lblRelevantObjects;
        private System.Windows.Forms.Label lbl_threshold;
        private DBLayoutPanel tableLayoutPanel24;
        private System.Windows.Forms.Label lbl_threshold_lower;
        private System.Windows.Forms.TextBox tb_threshold_upper;
        private System.Windows.Forms.Label lbl_threshold_upper;
        private System.Windows.Forms.TextBox tb_threshold_lower;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel26;
        private System.Windows.Forms.Panel panMasking;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel29;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown num_history_mins;
        private System.Windows.Forms.NumericUpDown num_mask_create;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown num_mask_remove;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown num_percent_var;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblAdvSettings;
        private DBLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Button btnCameraSave;
        private System.Windows.Forms.Button btnCameraDel;
        private System.Windows.Forms.Label lbl_camstats;
        private System.Windows.Forms.TabPage tabHistory;
        private DBLayoutPanel tableLayoutPanel1;
        private DBLayoutPanel tableLayoutPanel21;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DBLayoutPanel tableLayoutPanel22;
        private System.Windows.Forms.CheckBox cb_showObjects;
        private System.Windows.Forms.CheckBox cb_showMask;
        private System.Windows.Forms.Label lbl_objects;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DBLayoutPanel tableLayoutPanel19;
        private System.Windows.Forms.CheckBox cb_showFilters;
        private System.Windows.Forms.ListView list1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_filter_camera;
        private System.Windows.Forms.CheckBox cb_filter_nosuccess;
        private System.Windows.Forms.CheckBox cb_filter_success;
        private System.Windows.Forms.CheckBox cb_filter_person;
        private System.Windows.Forms.CheckBox cb_filter_vehicle;
        private System.Windows.Forms.CheckBox cb_filter_animal;
        private System.Windows.Forms.TabPage tabStats;
        private DBLayoutPanel tableLayoutPanel16;
        private DBLayoutPanel tableLayoutPanel23;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_confidence;
        private System.Windows.Forms.DataVisualization.Charting.Chart timeline;
        private System.Windows.Forms.Label label7;
        private DBLayoutPanel tableLayoutPanel17;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TabPage tabOverview;
        private DBLayoutPanel tableLayoutPanel14;
        private DBLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.Label lbl_errors;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.TabControl tabControl1;
        protected internal System.Windows.Forms.CheckBox cb_masking_enabled;
        private System.Windows.Forms.Label label21;
    }
}

