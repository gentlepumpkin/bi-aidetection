namespace AITool
{
    partial class Frm_LegacyActions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LegacyActions));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBoxMQTT = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.cb_MQTT_enabled = new System.Windows.Forms.CheckBox();
            this.linkLabelMqttSettings = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_MQTT_Topic = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_MQTT_Topic_Cancel = new System.Windows.Forms.TextBox();
            this.tb_MQTT_Payload = new System.Windows.Forms.TextBox();
            this.cb_MQTT_SendImage = new System.Windows.Forms.CheckBox();
            this.tb_MQTT_Payload_cancel = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBoxTelegram = new System.Windows.Forms.GroupBox();
            this.lnkTelegramTriggeringObjects = new System.Windows.Forms.LinkLabel();
            this.cb_telegram = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_telegram_caption = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cb_telegram_active_time = new System.Windows.Forms.TextBox();
            this.tb_RunExternalProgram = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tb_network_folder = new System.Windows.Forms.TextBox();
            this.groupBoxPushover = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.LnkPushoverObjects = new System.Windows.Forms.LinkLabel();
            this.cb_Pushover_Enabled = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tb_Pushover_Title = new System.Windows.Forms.TextBox();
            this.cb_pushover_active_time = new System.Windows.Forms.TextBox();
            this.tb_Pushover_Message = new System.Windows.Forms.TextBox();
            this.tb_Pushover_Device = new System.Windows.Forms.TextBox();
            this.tb_Pushover_sound = new System.Windows.Forms.TextBox();
            this.tb_Pushover_Priority = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_ActionCancelSecs = new System.Windows.Forms.TextBox();
            this.tb_jpeg_merge_quality = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_ShowOnlyRelevant = new System.Windows.Forms.CheckBox();
            this.cb_queue_actions = new System.Windows.Forms.CheckBox();
            this.cb_mergeannotations = new System.Windows.Forms.CheckBox();
            this.tb_network_folder_filename = new System.Windows.Forms.TextBox();
            this.tb_Sounds = new System.Windows.Forms.TextBox();
            this.cb_PlaySound = new System.Windows.Forms.CheckBox();
            this.tb_RunExternalProgramArgs = new System.Windows.Forms.TextBox();
            this.cb_RunProgram = new System.Windows.Forms.CheckBox();
            this.cb_copyAlertImages = new System.Windows.Forms.CheckBox();
            this.tbCancelUrl = new System.Windows.Forms.TextBox();
            this.tbTriggerUrl = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_Confidence = new System.Windows.Forms.Label();
            this.lbl_DetectionFormat = new System.Windows.Forms.Label();
            this.tb_ConfidenceFormat = new System.Windows.Forms.TextBox();
            this.tb_DetectionFormat = new System.Windows.Forms.TextBox();
            this.tb_sound_cooldown = new System.Windows.Forms.TextBox();
            this.tb_cooldown = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bt_variables = new System.Windows.Forms.Button();
            this.tb_ActionDelayMS = new System.Windows.Forms.TextBox();
            this.tb_NetworkFolderCleanupDays = new System.Windows.Forms.TextBox();
            this.cb_ActivateBlueIrisWindow = new System.Windows.Forms.CheckBox();
            this.btTest = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxUrlCancel = new System.Windows.Forms.GroupBox();
            this.cb_UrlCancelEnabled = new System.Windows.Forms.CheckBox();
            this.groupBoxUrlTrigger = new System.Windows.Forms.GroupBox();
            this.cb_UrlTriggerEnabled = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxMQTT.SuspendLayout();
            this.groupBoxTelegram.SuspendLayout();
            this.groupBoxPushover.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxUrlCancel.SuspendLayout();
            this.groupBoxUrlTrigger.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(810, 701);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 30);
            this.btnCancel.TabIndex = 37;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(732, 701);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 30);
            this.btnSave.TabIndex = 36;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBoxMQTT
            // 
            this.groupBoxMQTT.Controls.Add(this.linkLabel1);
            this.groupBoxMQTT.Controls.Add(this.cb_MQTT_enabled);
            this.groupBoxMQTT.Controls.Add(this.linkLabelMqttSettings);
            this.groupBoxMQTT.Controls.Add(this.label1);
            this.groupBoxMQTT.Controls.Add(this.tb_MQTT_Topic);
            this.groupBoxMQTT.Controls.Add(this.label9);
            this.groupBoxMQTT.Controls.Add(this.tb_MQTT_Topic_Cancel);
            this.groupBoxMQTT.Controls.Add(this.tb_MQTT_Payload);
            this.groupBoxMQTT.Controls.Add(this.cb_MQTT_SendImage);
            this.groupBoxMQTT.Controls.Add(this.tb_MQTT_Payload_cancel);
            this.groupBoxMQTT.Controls.Add(this.label12);
            this.groupBoxMQTT.Controls.Add(this.label13);
            this.groupBoxMQTT.Location = new System.Drawing.Point(6, 296);
            this.groupBoxMQTT.Name = "groupBoxMQTT";
            this.groupBoxMQTT.Size = new System.Drawing.Size(852, 87);
            this.groupBoxMQTT.TabIndex = 53;
            this.groupBoxMQTT.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(738, 54);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(102, 13);
            this.linkLabel1.TabIndex = 46;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Triggering Objects";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // cb_MQTT_enabled
            // 
            this.cb_MQTT_enabled.AutoSize = true;
            this.cb_MQTT_enabled.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_MQTT_enabled.Location = new System.Drawing.Point(6, 0);
            this.cb_MQTT_enabled.Name = "cb_MQTT_enabled";
            this.cb_MQTT_enabled.Size = new System.Drawing.Size(136, 17);
            this.cb_MQTT_enabled.TabIndex = 17;
            this.cb_MQTT_enabled.Text = "Send MQTT Message:";
            this.cb_MQTT_enabled.UseVisualStyleBackColor = true;
            this.cb_MQTT_enabled.CheckedChanged += new System.EventHandler(this.cb_MQTT_enabled_CheckedChanged);
            // 
            // linkLabelMqttSettings
            // 
            this.linkLabelMqttSettings.AutoSize = true;
            this.linkLabelMqttSettings.Location = new System.Drawing.Point(150, 1);
            this.linkLabelMqttSettings.Name = "linkLabelMqttSettings";
            this.linkLabelMqttSettings.Size = new System.Drawing.Size(49, 13);
            this.linkLabelMqttSettings.TabIndex = 41;
            this.linkLabelMqttSettings.TabStop = true;
            this.linkLabelMqttSettings.Text = "Settings";
            this.toolTip1.SetToolTip(this.linkLabelMqttSettings, "Global MQTT Settings");
            this.linkLabelMqttSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMqttSettings_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Trigger Topic:";
            // 
            // tb_MQTT_Topic
            // 
            this.tb_MQTT_Topic.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_MQTT_Topic.Location = new System.Drawing.Point(87, 25);
            this.tb_MQTT_Topic.Name = "tb_MQTT_Topic";
            this.tb_MQTT_Topic.Size = new System.Drawing.Size(219, 20);
            this.tb_MQTT_Topic.TabIndex = 18;
            this.tb_MQTT_Topic.Tag = "";
            this.toolTip1.SetToolTip(this.tb_MQTT_Topic, "Specify more than one topic/payload by using the PIPE | symbol between each.");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DarkRed;
            this.label9.Location = new System.Drawing.Point(4, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Cancel Topic:";
            // 
            // tb_MQTT_Topic_Cancel
            // 
            this.tb_MQTT_Topic_Cancel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_MQTT_Topic_Cancel.Location = new System.Drawing.Point(87, 53);
            this.tb_MQTT_Topic_Cancel.Name = "tb_MQTT_Topic_Cancel";
            this.tb_MQTT_Topic_Cancel.Size = new System.Drawing.Size(219, 20);
            this.tb_MQTT_Topic_Cancel.TabIndex = 21;
            this.tb_MQTT_Topic_Cancel.Tag = "";
            this.toolTip1.SetToolTip(this.tb_MQTT_Topic_Cancel, "Specify more than one topic/payload by using the PIPE | symbol between each.");
            // 
            // tb_MQTT_Payload
            // 
            this.tb_MQTT_Payload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_MQTT_Payload.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_MQTT_Payload.Location = new System.Drawing.Point(370, 25);
            this.tb_MQTT_Payload.Name = "tb_MQTT_Payload";
            this.tb_MQTT_Payload.Size = new System.Drawing.Size(340, 20);
            this.tb_MQTT_Payload.TabIndex = 19;
            this.tb_MQTT_Payload.Tag = "";
            this.toolTip1.SetToolTip(this.tb_MQTT_Payload, "Specify more than one topic/payload by using the PIPE | symbol between each.");
            // 
            // cb_MQTT_SendImage
            // 
            this.cb_MQTT_SendImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_MQTT_SendImage.AutoSize = true;
            this.cb_MQTT_SendImage.Location = new System.Drawing.Point(758, 27);
            this.cb_MQTT_SendImage.Name = "cb_MQTT_SendImage";
            this.cb_MQTT_SendImage.Size = new System.Drawing.Size(86, 17);
            this.cb_MQTT_SendImage.TabIndex = 20;
            this.cb_MQTT_SendImage.Text = "Send Image";
            this.toolTip1.SetToolTip(this.cb_MQTT_SendImage, "If one of the topics has /image and the image checkbox is checked, then the actua" +
        "l image with the detection will be sent.\r\n (use | to Separate multiple topics or" +
        " payloads.)");
            this.cb_MQTT_SendImage.UseVisualStyleBackColor = true;
            // 
            // tb_MQTT_Payload_cancel
            // 
            this.tb_MQTT_Payload_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_MQTT_Payload_cancel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_MQTT_Payload_cancel.Location = new System.Drawing.Point(370, 51);
            this.tb_MQTT_Payload_cancel.Name = "tb_MQTT_Payload_cancel";
            this.tb_MQTT_Payload_cancel.Size = new System.Drawing.Size(340, 20);
            this.tb_MQTT_Payload_cancel.TabIndex = 22;
            this.tb_MQTT_Payload_cancel.Tag = "";
            this.toolTip1.SetToolTip(this.tb_MQTT_Payload_cancel, "Specify more than one topic/payload by using the PIPE | symbol between each.");
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(312, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "Payload:";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.DarkRed;
            this.label13.Location = new System.Drawing.Point(312, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 13);
            this.label13.TabIndex = 39;
            this.label13.Text = "Payload:";
            // 
            // groupBoxTelegram
            // 
            this.groupBoxTelegram.Controls.Add(this.lnkTelegramTriggeringObjects);
            this.groupBoxTelegram.Controls.Add(this.cb_telegram);
            this.groupBoxTelegram.Controls.Add(this.label7);
            this.groupBoxTelegram.Controls.Add(this.tb_telegram_caption);
            this.groupBoxTelegram.Controls.Add(this.label20);
            this.groupBoxTelegram.Controls.Add(this.cb_telegram_active_time);
            this.groupBoxTelegram.Location = new System.Drawing.Point(6, 125);
            this.groupBoxTelegram.Name = "groupBoxTelegram";
            this.groupBoxTelegram.Size = new System.Drawing.Size(852, 58);
            this.groupBoxTelegram.TabIndex = 52;
            this.groupBoxTelegram.TabStop = false;
            // 
            // lnkTelegramTriggeringObjects
            // 
            this.lnkTelegramTriggeringObjects.AutoSize = true;
            this.lnkTelegramTriggeringObjects.Location = new System.Drawing.Point(738, 28);
            this.lnkTelegramTriggeringObjects.Name = "lnkTelegramTriggeringObjects";
            this.lnkTelegramTriggeringObjects.Size = new System.Drawing.Size(102, 13);
            this.lnkTelegramTriggeringObjects.TabIndex = 46;
            this.lnkTelegramTriggeringObjects.TabStop = true;
            this.lnkTelegramTriggeringObjects.Text = "Triggering Objects";
            this.lnkTelegramTriggeringObjects.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTelegramTriggeringObjects_LinkClicked);
            // 
            // cb_telegram
            // 
            this.cb_telegram.AutoSize = true;
            this.cb_telegram.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_telegram.Location = new System.Drawing.Point(8, 0);
            this.cb_telegram.Margin = new System.Windows.Forms.Padding(10, 6, 5, 6);
            this.cb_telegram.Name = "cb_telegram";
            this.cb_telegram.Size = new System.Drawing.Size(184, 19);
            this.cb_telegram.TabIndex = 6;
            this.cb_telegram.Text = "Send alert images to Telegram";
            this.cb_telegram.UseVisualStyleBackColor = false;
            this.cb_telegram.CheckedChanged += new System.EventHandler(this.cb_telegram_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 43;
            this.label7.Text = "Title:";
            // 
            // tb_telegram_caption
            // 
            this.tb_telegram_caption.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_telegram_caption.Location = new System.Drawing.Point(55, 25);
            this.tb_telegram_caption.Name = "tb_telegram_caption";
            this.tb_telegram_caption.Size = new System.Drawing.Size(219, 20);
            this.tb_telegram_caption.TabIndex = 7;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(298, 28);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(34, 13);
            this.label20.TabIndex = 44;
            this.label20.Text = "Time:";
            // 
            // cb_telegram_active_time
            // 
            this.cb_telegram_active_time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_telegram_active_time.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_telegram_active_time.Location = new System.Drawing.Point(340, 25);
            this.cb_telegram_active_time.Name = "cb_telegram_active_time";
            this.cb_telegram_active_time.Size = new System.Drawing.Size(219, 20);
            this.cb_telegram_active_time.TabIndex = 9;
            this.toolTip1.SetToolTip(this.cb_telegram_active_time, "Time range (24 hr) when sending to Telegram is active");
            // 
            // tb_RunExternalProgram
            // 
            this.tb_RunExternalProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_RunExternalProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tb_RunExternalProgram.Location = new System.Drawing.Point(191, 619);
            this.tb_RunExternalProgram.Name = "tb_RunExternalProgram";
            this.tb_RunExternalProgram.Size = new System.Drawing.Size(295, 22);
            this.tb_RunExternalProgram.TabIndex = 27;
            this.toolTip1.SetToolTip(this.tb_RunExternalProgram, "Path to EXE, BAT, etc");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(488, 591);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 13);
            this.label15.TabIndex = 44;
            this.label15.Text = "Filename:";
            // 
            // tb_network_folder
            // 
            this.tb_network_folder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_network_folder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tb_network_folder.Location = new System.Drawing.Point(191, 588);
            this.tb_network_folder.Name = "tb_network_folder";
            this.tb_network_folder.Size = new System.Drawing.Size(295, 22);
            this.tb_network_folder.TabIndex = 24;
            // 
            // groupBoxPushover
            // 
            this.groupBoxPushover.Controls.Add(this.label11);
            this.groupBoxPushover.Controls.Add(this.LnkPushoverObjects);
            this.groupBoxPushover.Controls.Add(this.cb_Pushover_Enabled);
            this.groupBoxPushover.Controls.Add(this.label8);
            this.groupBoxPushover.Controls.Add(this.label21);
            this.groupBoxPushover.Controls.Add(this.tb_Pushover_Title);
            this.groupBoxPushover.Controls.Add(this.cb_pushover_active_time);
            this.groupBoxPushover.Controls.Add(this.tb_Pushover_Message);
            this.groupBoxPushover.Controls.Add(this.tb_Pushover_Device);
            this.groupBoxPushover.Controls.Add(this.tb_Pushover_sound);
            this.groupBoxPushover.Controls.Add(this.tb_Pushover_Priority);
            this.groupBoxPushover.Controls.Add(this.label10);
            this.groupBoxPushover.Controls.Add(this.label16);
            this.groupBoxPushover.Controls.Add(this.label18);
            this.groupBoxPushover.Controls.Add(this.label19);
            this.groupBoxPushover.Location = new System.Drawing.Point(6, 189);
            this.groupBoxPushover.Name = "groupBoxPushover";
            this.groupBoxPushover.Size = new System.Drawing.Size(852, 101);
            this.groupBoxPushover.TabIndex = 51;
            this.groupBoxPushover.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Gray;
            this.label11.Location = new System.Drawing.Point(5, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(639, 13);
            this.label11.TabIndex = 47;
            this.label11.Text = "Specify more than one Sound, Prioirty and Time by using the PIPE symbol.   Use of" +
    " SUNRISE-SUNSET is allowed for all TIMES";
            // 
            // LnkPushoverObjects
            // 
            this.LnkPushoverObjects.AutoSize = true;
            this.LnkPushoverObjects.Location = new System.Drawing.Point(738, 79);
            this.LnkPushoverObjects.Name = "LnkPushoverObjects";
            this.LnkPushoverObjects.Size = new System.Drawing.Size(102, 13);
            this.LnkPushoverObjects.TabIndex = 46;
            this.LnkPushoverObjects.TabStop = true;
            this.LnkPushoverObjects.Text = "Triggering Objects";
            this.LnkPushoverObjects.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkPushoverObjects_LinkClicked);
            // 
            // cb_Pushover_Enabled
            // 
            this.cb_Pushover_Enabled.AutoSize = true;
            this.cb_Pushover_Enabled.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_Pushover_Enabled.Location = new System.Drawing.Point(8, 0);
            this.cb_Pushover_Enabled.Name = "cb_Pushover_Enabled";
            this.cb_Pushover_Enabled.Size = new System.Drawing.Size(181, 17);
            this.cb_Pushover_Enabled.TabIndex = 10;
            this.cb_Pushover_Enabled.Text = "Send alert images to Pushover";
            this.cb_Pushover_Enabled.UseVisualStyleBackColor = true;
            this.cb_Pushover_Enabled.CheckedChanged += new System.EventHandler(this.cb_Pushover_Enabled_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 36;
            this.label8.Text = "Title:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(580, 50);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(34, 13);
            this.label21.TabIndex = 44;
            this.label21.Text = "Time:";
            // 
            // tb_Pushover_Title
            // 
            this.tb_Pushover_Title.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Pushover_Title.Location = new System.Drawing.Point(55, 20);
            this.tb_Pushover_Title.Name = "tb_Pushover_Title";
            this.tb_Pushover_Title.Size = new System.Drawing.Size(219, 20);
            this.tb_Pushover_Title.TabIndex = 11;
            this.tb_Pushover_Title.Tag = "";
            this.toolTip1.SetToolTip(this.tb_Pushover_Title, "Specify more than one title/message/device by using the PIPE | symbol between eac" +
        "h.");
            // 
            // cb_pushover_active_time
            // 
            this.cb_pushover_active_time.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_pushover_active_time.Location = new System.Drawing.Point(622, 49);
            this.cb_pushover_active_time.Name = "cb_pushover_active_time";
            this.cb_pushover_active_time.Size = new System.Drawing.Size(219, 20);
            this.cb_pushover_active_time.TabIndex = 45;
            this.toolTip1.SetToolTip(this.cb_pushover_active_time, "Time range (24 hr) when sending to Pushover is active");
            // 
            // tb_Pushover_Message
            // 
            this.tb_Pushover_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Pushover_Message.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Pushover_Message.Location = new System.Drawing.Point(340, 20);
            this.tb_Pushover_Message.Name = "tb_Pushover_Message";
            this.tb_Pushover_Message.Size = new System.Drawing.Size(219, 20);
            this.tb_Pushover_Message.TabIndex = 12;
            this.tb_Pushover_Message.Tag = "";
            this.toolTip1.SetToolTip(this.tb_Pushover_Message, "Specify more than one title/message/device by using the PIPE | symbol between eac" +
        "h.");
            // 
            // tb_Pushover_Device
            // 
            this.tb_Pushover_Device.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Pushover_Device.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Pushover_Device.Location = new System.Drawing.Point(622, 20);
            this.tb_Pushover_Device.Name = "tb_Pushover_Device";
            this.tb_Pushover_Device.Size = new System.Drawing.Size(219, 20);
            this.tb_Pushover_Device.TabIndex = 13;
            this.tb_Pushover_Device.Tag = "";
            this.toolTip1.SetToolTip(this.tb_Pushover_Device, "Specify more than one title/message/device by using the PIPE | symbol between eac" +
        "h.");
            // 
            // tb_Pushover_sound
            // 
            this.tb_Pushover_sound.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Pushover_sound.Location = new System.Drawing.Point(55, 49);
            this.tb_Pushover_sound.Name = "tb_Pushover_sound";
            this.tb_Pushover_sound.Size = new System.Drawing.Size(219, 20);
            this.tb_Pushover_sound.TabIndex = 14;
            this.tb_Pushover_sound.Tag = "";
            this.toolTip1.SetToolTip(this.tb_Pushover_sound, "specify a sound name Pushover supports such as bike, bugle, cosmic, etc");
            // 
            // tb_Pushover_Priority
            // 
            this.tb_Pushover_Priority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Pushover_Priority.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Pushover_Priority.Location = new System.Drawing.Point(340, 49);
            this.tb_Pushover_Priority.Name = "tb_Pushover_Priority";
            this.tb_Pushover_Priority.Size = new System.Drawing.Size(219, 20);
            this.tb_Pushover_Priority.TabIndex = 15;
            this.tb_Pushover_Priority.Tag = "";
            this.toolTip1.SetToolTip(this.tb_Pushover_Priority, "Lowest, Low, Normal, High, Emergency");
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(278, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "Message:";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(562, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 13);
            this.label16.TabIndex = 39;
            this.label16.Text = "Device(s):";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(5, 52);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 13);
            this.label18.TabIndex = 39;
            this.label18.Text = "Sound:";
            this.toolTip1.SetToolTip(this.label18, "The name of the sound you want to play");
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(286, 52);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(46, 13);
            this.label19.TabIndex = 39;
            this.label19.Text = "Priority:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(682, 652);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Cooldown Seconds:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(497, 623);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 40;
            this.label14.Text = "Params:";
            // 
            // tb_ActionCancelSecs
            // 
            this.tb_ActionCancelSecs.Location = new System.Drawing.Point(803, 41);
            this.tb_ActionCancelSecs.Name = "tb_ActionCancelSecs";
            this.tb_ActionCancelSecs.Size = new System.Drawing.Size(44, 22);
            this.tb_ActionCancelSecs.TabIndex = 3;
            this.toolTip1.SetToolTip(this.tb_ActionCancelSecs, resources.GetString("tb_ActionCancelSecs.ToolTip"));
            // 
            // tb_jpeg_merge_quality
            // 
            this.tb_jpeg_merge_quality.Location = new System.Drawing.Point(803, 13);
            this.tb_jpeg_merge_quality.Name = "tb_jpeg_merge_quality";
            this.tb_jpeg_merge_quality.Size = new System.Drawing.Size(44, 22);
            this.tb_jpeg_merge_quality.TabIndex = 3;
            this.toolTip1.SetToolTip(this.tb_jpeg_merge_quality, "The larger the number, the higher the image quality AND SIZE.   If you lower this" +
        " number to\r\n50 or below, images will be smaller and sent to Telegram faster.");
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(666, 45);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(126, 13);
            this.label23.TabIndex = 48;
            this.label23.Text = "Action Cancel Seconds:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(620, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Merge JPEG Save Quality (1-100):";
            // 
            // cb_ShowOnlyRelevant
            // 
            this.cb_ShowOnlyRelevant.AutoSize = true;
            this.cb_ShowOnlyRelevant.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_ShowOnlyRelevant.Location = new System.Drawing.Point(14, 13);
            this.cb_ShowOnlyRelevant.Name = "cb_ShowOnlyRelevant";
            this.cb_ShowOnlyRelevant.Size = new System.Drawing.Size(171, 17);
            this.cb_ShowOnlyRelevant.TabIndex = 1;
            this.cb_ShowOnlyRelevant.Text = "Show Only Relevant Objects";
            this.toolTip1.SetToolTip(this.cb_ShowOnlyRelevant, "(Applies to ALL cameras) - If checked, only Relevant Objects will be shown for ac" +
        "tions.  Note this also effects the History tab.");
            this.cb_ShowOnlyRelevant.UseVisualStyleBackColor = true;
            // 
            // cb_queue_actions
            // 
            this.cb_queue_actions.AutoSize = true;
            this.cb_queue_actions.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_queue_actions.Location = new System.Drawing.Point(237, 13);
            this.cb_queue_actions.Name = "cb_queue_actions";
            this.cb_queue_actions.Size = new System.Drawing.Size(101, 17);
            this.cb_queue_actions.TabIndex = 1;
            this.cb_queue_actions.Text = "Queue Actions";
            this.toolTip1.SetToolTip(this.cb_queue_actions, resources.GetString("cb_queue_actions.ToolTip"));
            this.cb_queue_actions.UseVisualStyleBackColor = true;
            this.cb_queue_actions.CheckedChanged += new System.EventHandler(this.cb_queue_actions_CheckedChanged);
            // 
            // cb_mergeannotations
            // 
            this.cb_mergeannotations.AutoSize = true;
            this.cb_mergeannotations.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_mergeannotations.Location = new System.Drawing.Point(395, 13);
            this.cb_mergeannotations.Name = "cb_mergeannotations";
            this.cb_mergeannotations.Size = new System.Drawing.Size(189, 17);
            this.cb_mergeannotations.TabIndex = 2;
            this.cb_mergeannotations.Text = "Merge Annotations Into Images";
            this.toolTip1.SetToolTip(this.cb_mergeannotations, "Merge detected object text and rectangles into actual image.");
            this.cb_mergeannotations.UseVisualStyleBackColor = true;
            this.cb_mergeannotations.CheckedChanged += new System.EventHandler(this.cb_mergeannotations_CheckedChanged);
            // 
            // tb_network_folder_filename
            // 
            this.tb_network_folder_filename.Location = new System.Drawing.Point(549, 588);
            this.tb_network_folder_filename.Name = "tb_network_folder_filename";
            this.tb_network_folder_filename.Size = new System.Drawing.Size(156, 22);
            this.tb_network_folder_filename.TabIndex = 25;
            this.toolTip1.SetToolTip(this.tb_network_folder_filename, "The filename to be created in the network folder NOT including file extension.  F" +
        "or example, [camera] would be saved as MYCAMERA.JPG");
            // 
            // tb_Sounds
            // 
            this.tb_Sounds.Location = new System.Drawing.Point(191, 647);
            this.tb_Sounds.Name = "tb_Sounds";
            this.tb_Sounds.Size = new System.Drawing.Size(465, 22);
            this.tb_Sounds.TabIndex = 30;
            this.toolTip1.SetToolTip(this.tb_Sounds, resources.GetString("tb_Sounds.ToolTip"));
            // 
            // cb_PlaySound
            // 
            this.cb_PlaySound.AutoSize = true;
            this.cb_PlaySound.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_PlaySound.Location = new System.Drawing.Point(9, 649);
            this.cb_PlaySound.Name = "cb_PlaySound";
            this.cb_PlaySound.Size = new System.Drawing.Size(86, 17);
            this.cb_PlaySound.TabIndex = 29;
            this.cb_PlaySound.Text = "Play Sound:";
            this.cb_PlaySound.UseVisualStyleBackColor = true;
            this.cb_PlaySound.CheckedChanged += new System.EventHandler(this.cb_PlaySound_CheckedChanged);
            // 
            // tb_RunExternalProgramArgs
            // 
            this.tb_RunExternalProgramArgs.Location = new System.Drawing.Point(549, 620);
            this.tb_RunExternalProgramArgs.Name = "tb_RunExternalProgramArgs";
            this.tb_RunExternalProgramArgs.Size = new System.Drawing.Size(295, 22);
            this.tb_RunExternalProgramArgs.TabIndex = 28;
            this.toolTip1.SetToolTip(this.tb_RunExternalProgramArgs, "Command line arguments to run the external app or script");
            // 
            // cb_RunProgram
            // 
            this.cb_RunProgram.AutoSize = true;
            this.cb_RunProgram.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_RunProgram.Location = new System.Drawing.Point(9, 620);
            this.cb_RunProgram.Name = "cb_RunProgram";
            this.cb_RunProgram.Size = new System.Drawing.Size(141, 17);
            this.cb_RunProgram.TabIndex = 26;
            this.cb_RunProgram.Text = "Run external program:";
            this.cb_RunProgram.UseVisualStyleBackColor = true;
            this.cb_RunProgram.CheckedChanged += new System.EventHandler(this.cb_RunProgram_CheckedChanged);
            // 
            // cb_copyAlertImages
            // 
            this.cb_copyAlertImages.AutoSize = true;
            this.cb_copyAlertImages.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_copyAlertImages.Location = new System.Drawing.Point(9, 589);
            this.cb_copyAlertImages.Margin = new System.Windows.Forms.Padding(40, 8, 7, 8);
            this.cb_copyAlertImages.Name = "cb_copyAlertImages";
            this.cb_copyAlertImages.Size = new System.Drawing.Size(168, 17);
            this.cb_copyAlertImages.TabIndex = 23;
            this.cb_copyAlertImages.Text = "Copy alert images to folder:";
            this.toolTip1.SetToolTip(this.cb_copyAlertImages, "When an object in an image is detected, copy the image to the\r\n folder specified");
            this.cb_copyAlertImages.UseVisualStyleBackColor = false;
            this.cb_copyAlertImages.CheckedChanged += new System.EventHandler(this.cb_copyAlertImages_CheckedChanged);
            // 
            // tbCancelUrl
            // 
            this.tbCancelUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCancelUrl.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCancelUrl.Location = new System.Drawing.Point(3, 18);
            this.tbCancelUrl.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tbCancelUrl.Multiline = true;
            this.tbCancelUrl.Name = "tbCancelUrl";
            this.tbCancelUrl.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCancelUrl.Size = new System.Drawing.Size(846, 71);
            this.tbCancelUrl.TabIndex = 33;
            this.toolTip1.SetToolTip(this.tbCancelUrl, "URLs that cancel the alert - For BI, use ");
            this.tbCancelUrl.WordWrap = false;
            // 
            // tbTriggerUrl
            // 
            this.tbTriggerUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTriggerUrl.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTriggerUrl.Location = new System.Drawing.Point(3, 18);
            this.tbTriggerUrl.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tbTriggerUrl.Multiline = true;
            this.tbTriggerUrl.Name = "tbTriggerUrl";
            this.tbTriggerUrl.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbTriggerUrl.Size = new System.Drawing.Size(846, 71);
            this.tbTriggerUrl.TabIndex = 32;
            this.tbTriggerUrl.Text = "test\r\ntest2\r\ntest3";
            this.toolTip1.SetToolTip(this.tbTriggerUrl, "A list of URLs each on their own line OR seperated with commas that will be trigg" +
        "ered on an alert");
            this.tbTriggerUrl.WordWrap = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(13, 98);
            this.label24.Margin = new System.Windows.Forms.Padding(35, 0, 5, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(112, 13);
            this.label24.TabIndex = 0;
            this.label24.Text = "[Confidence] format:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(23, 70);
            this.label22.Margin = new System.Windows.Forms.Padding(35, 0, 5, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(103, 13);
            this.label22.TabIndex = 0;
            this.label22.Text = "[Detection] format:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(682, 73);
            this.label5.Margin = new System.Windows.Forms.Padding(35, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cooldown Seconds:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Confidence
            // 
            this.lbl_Confidence.AutoSize = true;
            this.lbl_Confidence.Location = new System.Drawing.Point(357, 98);
            this.lbl_Confidence.Margin = new System.Windows.Forms.Padding(2, 0, 5, 0);
            this.lbl_Confidence.Name = "lbl_Confidence";
            this.lbl_Confidence.Size = new System.Drawing.Size(10, 13);
            this.lbl_Confidence.TabIndex = 2;
            this.lbl_Confidence.Text = ".";
            // 
            // lbl_DetectionFormat
            // 
            this.lbl_DetectionFormat.AutoSize = true;
            this.lbl_DetectionFormat.Location = new System.Drawing.Point(357, 70);
            this.lbl_DetectionFormat.Margin = new System.Windows.Forms.Padding(2, 0, 5, 0);
            this.lbl_DetectionFormat.Name = "lbl_DetectionFormat";
            this.lbl_DetectionFormat.Size = new System.Drawing.Size(10, 13);
            this.lbl_DetectionFormat.TabIndex = 2;
            this.lbl_DetectionFormat.Text = ".";
            this.lbl_DetectionFormat.Click += new System.EventHandler(this.lbl_DetectionFormat_Click);
            // 
            // tb_ConfidenceFormat
            // 
            this.tb_ConfidenceFormat.Location = new System.Drawing.Point(134, 95);
            this.tb_ConfidenceFormat.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tb_ConfidenceFormat.Name = "tb_ConfidenceFormat";
            this.tb_ConfidenceFormat.Size = new System.Drawing.Size(211, 22);
            this.tb_ConfidenceFormat.TabIndex = 5;
            this.toolTip1.SetToolTip(this.tb_ConfidenceFormat, "(Applies to ALL cameras)");
            this.tb_ConfidenceFormat.TextChanged += new System.EventHandler(this.tb_ConfidenceFormat_TextChanged);
            // 
            // tb_DetectionFormat
            // 
            this.tb_DetectionFormat.Location = new System.Drawing.Point(134, 67);
            this.tb_DetectionFormat.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tb_DetectionFormat.Name = "tb_DetectionFormat";
            this.tb_DetectionFormat.Size = new System.Drawing.Size(211, 22);
            this.tb_DetectionFormat.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tb_DetectionFormat, "This is the format for each individual detection");
            this.tb_DetectionFormat.TextChanged += new System.EventHandler(this.tb_DetectionFormat_TextChanged);
            // 
            // tb_sound_cooldown
            // 
            this.tb_sound_cooldown.Location = new System.Drawing.Point(800, 647);
            this.tb_sound_cooldown.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tb_sound_cooldown.Name = "tb_sound_cooldown";
            this.tb_sound_cooldown.Size = new System.Drawing.Size(44, 22);
            this.tb_sound_cooldown.TabIndex = 31;
            // 
            // tb_cooldown
            // 
            this.tb_cooldown.Location = new System.Drawing.Point(803, 70);
            this.tb_cooldown.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tb_cooldown.Name = "tb_cooldown";
            this.tb_cooldown.Size = new System.Drawing.Size(44, 22);
            this.tb_cooldown.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tb_cooldown, "Minimum time between actions.  Note the actual value used is HALF of this value.");
            // 
            // bt_variables
            // 
            this.bt_variables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_variables.BackColor = System.Drawing.SystemColors.Info;
            this.bt_variables.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_variables.Location = new System.Drawing.Point(8, 700);
            this.bt_variables.Name = "bt_variables";
            this.bt_variables.Size = new System.Drawing.Size(70, 30);
            this.bt_variables.TabIndex = 34;
            this.bt_variables.Text = "Variables";
            this.toolTip1.SetToolTip(this.bt_variables, "A list of [variable] names you can use in the settings below.   This list is read" +
        "-only.");
            this.bt_variables.UseVisualStyleBackColor = false;
            this.bt_variables.Click += new System.EventHandler(this.bt_variables_Click);
            // 
            // tb_ActionDelayMS
            // 
            this.tb_ActionDelayMS.Location = new System.Drawing.Point(803, 97);
            this.tb_ActionDelayMS.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tb_ActionDelayMS.Name = "tb_ActionDelayMS";
            this.tb_ActionDelayMS.Size = new System.Drawing.Size(44, 22);
            this.tb_ActionDelayMS.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tb_ActionDelayMS, "(Applies to ALL cameras) - Millisecond delay between each Action, including betwe" +
        "en each trigger URL if you have more than one.");
            // 
            // tb_NetworkFolderCleanupDays
            // 
            this.tb_NetworkFolderCleanupDays.Location = new System.Drawing.Point(800, 588);
            this.tb_NetworkFolderCleanupDays.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tb_NetworkFolderCleanupDays.Name = "tb_NetworkFolderCleanupDays";
            this.tb_NetworkFolderCleanupDays.Size = new System.Drawing.Size(44, 22);
            this.tb_NetworkFolderCleanupDays.TabIndex = 31;
            this.toolTip1.SetToolTip(this.tb_NetworkFolderCleanupDays, "Files in this folder than this many days will be deleted.   This cleanup will hap" +
        "pen only once a day.");
            // 
            // cb_ActivateBlueIrisWindow
            // 
            this.cb_ActivateBlueIrisWindow.AutoSize = true;
            this.cb_ActivateBlueIrisWindow.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_ActivateBlueIrisWindow.Location = new System.Drawing.Point(14, 38);
            this.cb_ActivateBlueIrisWindow.Name = "cb_ActivateBlueIrisWindow";
            this.cb_ActivateBlueIrisWindow.Size = new System.Drawing.Size(156, 17);
            this.cb_ActivateBlueIrisWindow.TabIndex = 1;
            this.cb_ActivateBlueIrisWindow.Text = "Activate Blue Iris Window";
            this.toolTip1.SetToolTip(this.cb_ActivateBlueIrisWindow, "If BlueIris is installed on the same machine it will activate  its window and max" +
        "imize it.");
            this.cb_ActivateBlueIrisWindow.UseVisualStyleBackColor = true;
            this.cb_ActivateBlueIrisWindow.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btTest
            // 
            this.btTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btTest.Location = new System.Drawing.Point(655, 701);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(70, 30);
            this.btTest.TabIndex = 35;
            this.btTest.Text = "Test";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBoxUrlCancel);
            this.panel1.Controls.Add(this.groupBoxUrlTrigger);
            this.panel1.Controls.Add(this.cb_ActivateBlueIrisWindow);
            this.panel1.Controls.Add(this.cb_ShowOnlyRelevant);
            this.panel1.Controls.Add(this.groupBoxMQTT);
            this.panel1.Controls.Add(this.tb_ActionDelayMS);
            this.panel1.Controls.Add(this.tb_cooldown);
            this.panel1.Controls.Add(this.groupBoxTelegram);
            this.panel1.Controls.Add(this.tb_NetworkFolderCleanupDays);
            this.panel1.Controls.Add(this.tb_sound_cooldown);
            this.panel1.Controls.Add(this.tb_RunExternalProgram);
            this.panel1.Controls.Add(this.tb_DetectionFormat);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.tb_ConfidenceFormat);
            this.panel1.Controls.Add(this.tb_network_folder);
            this.panel1.Controls.Add(this.lbl_DetectionFormat);
            this.panel1.Controls.Add(this.groupBoxPushover);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lbl_Confidence);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.tb_ActionCancelSecs);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.tb_jpeg_merge_quality);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cb_queue_actions);
            this.panel1.Controls.Add(this.cb_copyAlertImages);
            this.panel1.Controls.Add(this.cb_mergeannotations);
            this.panel1.Controls.Add(this.cb_RunProgram);
            this.panel1.Controls.Add(this.tb_network_folder_filename);
            this.panel1.Controls.Add(this.tb_RunExternalProgramArgs);
            this.panel1.Controls.Add(this.tb_Sounds);
            this.panel1.Controls.Add(this.cb_PlaySound);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(867, 681);
            this.panel1.TabIndex = 54;
            // 
            // groupBoxUrlCancel
            // 
            this.groupBoxUrlCancel.Controls.Add(this.tbCancelUrl);
            this.groupBoxUrlCancel.Controls.Add(this.cb_UrlCancelEnabled);
            this.groupBoxUrlCancel.Location = new System.Drawing.Point(6, 487);
            this.groupBoxUrlCancel.Name = "groupBoxUrlCancel";
            this.groupBoxUrlCancel.Size = new System.Drawing.Size(852, 92);
            this.groupBoxUrlCancel.TabIndex = 55;
            this.groupBoxUrlCancel.TabStop = false;
            // 
            // cb_UrlCancelEnabled
            // 
            this.cb_UrlCancelEnabled.AutoSize = true;
            this.cb_UrlCancelEnabled.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_UrlCancelEnabled.Location = new System.Drawing.Point(6, 0);
            this.cb_UrlCancelEnabled.Name = "cb_UrlCancelEnabled";
            this.cb_UrlCancelEnabled.Size = new System.Drawing.Size(94, 17);
            this.cb_UrlCancelEnabled.TabIndex = 17;
            this.cb_UrlCancelEnabled.Text = "Cancel URL(s)";
            this.cb_UrlCancelEnabled.UseVisualStyleBackColor = true;
            this.cb_UrlCancelEnabled.CheckedChanged += new System.EventHandler(this.cb_CancelURLEnabled_CheckedChanged);
            // 
            // groupBoxUrlTrigger
            // 
            this.groupBoxUrlTrigger.Controls.Add(this.tbTriggerUrl);
            this.groupBoxUrlTrigger.Controls.Add(this.cb_UrlTriggerEnabled);
            this.groupBoxUrlTrigger.Location = new System.Drawing.Point(6, 389);
            this.groupBoxUrlTrigger.Name = "groupBoxUrlTrigger";
            this.groupBoxUrlTrigger.Size = new System.Drawing.Size(852, 92);
            this.groupBoxUrlTrigger.TabIndex = 54;
            this.groupBoxUrlTrigger.TabStop = false;
            // 
            // cb_UrlTriggerEnabled
            // 
            this.cb_UrlTriggerEnabled.AutoSize = true;
            this.cb_UrlTriggerEnabled.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_UrlTriggerEnabled.Location = new System.Drawing.Point(5, 0);
            this.cb_UrlTriggerEnabled.Name = "cb_UrlTriggerEnabled";
            this.cb_UrlTriggerEnabled.Size = new System.Drawing.Size(96, 17);
            this.cb_UrlTriggerEnabled.TabIndex = 17;
            this.cb_UrlTriggerEnabled.Text = "Trigger URL(s)";
            this.cb_UrlTriggerEnabled.UseVisualStyleBackColor = true;
            this.cb_UrlTriggerEnabled.CheckedChanged += new System.EventHandler(this.cb_TriggerURLEnabled_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(711, 591);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Max age (days):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(647, 100);
            this.label4.Margin = new System.Windows.Forms.Padding(35, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Delay Between Actions MS:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Frm_LegacyActions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(888, 739);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bt_variables);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btTest);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_LegacyActions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Camera Actions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_LegacyActions_FormClosing);
            this.Load += new System.EventHandler(this.Frm_LegacyActions_Load);
            this.groupBoxMQTT.ResumeLayout(false);
            this.groupBoxMQTT.PerformLayout();
            this.groupBoxTelegram.ResumeLayout(false);
            this.groupBoxTelegram.PerformLayout();
            this.groupBoxPushover.ResumeLayout(false);
            this.groupBoxPushover.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxUrlCancel.ResumeLayout(false);
            this.groupBoxUrlCancel.PerformLayout();
            this.groupBoxUrlTrigger.ResumeLayout(false);
            this.groupBoxUrlTrigger.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.TextBox tbTriggerUrl;
        public System.Windows.Forms.CheckBox cb_telegram;
        public System.Windows.Forms.TextBox tb_cooldown;
        public System.Windows.Forms.CheckBox cb_copyAlertImages;
        public System.Windows.Forms.TextBox tb_network_folder;
        public System.Windows.Forms.CheckBox cb_RunProgram;
        public System.Windows.Forms.TextBox tb_RunExternalProgram;
        public System.Windows.Forms.TextBox tb_RunExternalProgramArgs;
        public System.Windows.Forms.TextBox tb_Sounds;
        public System.Windows.Forms.CheckBox cb_PlaySound;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tb_MQTT_Payload;
        public System.Windows.Forms.TextBox tb_MQTT_Topic;
        public System.Windows.Forms.CheckBox cb_MQTT_enabled;
        private System.Windows.Forms.LinkLabel linkLabelMqttSettings;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox tb_telegram_caption;
        public System.Windows.Forms.TextBox tb_network_folder_filename;
        public System.Windows.Forms.CheckBox cb_mergeannotations;
        public System.Windows.Forms.TextBox tb_MQTT_Payload_cancel;
        public System.Windows.Forms.TextBox tb_MQTT_Topic_Cancel;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox tbCancelUrl;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.CheckBox cb_queue_actions;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tb_jpeg_merge_quality;
        public System.Windows.Forms.CheckBox cb_MQTT_SendImage;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox tb_Pushover_Message;
        public System.Windows.Forms.TextBox tb_Pushover_Title;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.CheckBox cb_Pushover_Enabled;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox tb_Pushover_Device;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox tb_Pushover_sound;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox tb_Pushover_Priority;
        public System.Windows.Forms.GroupBox groupBoxPushover;
        public System.Windows.Forms.GroupBox groupBoxTelegram;
        public System.Windows.Forms.GroupBox groupBoxMQTT;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.TextBox cb_telegram_active_time;
        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.TextBox cb_pushover_active_time;
        private System.Windows.Forms.Label label22;
        public System.Windows.Forms.TextBox tb_DetectionFormat;
        public System.Windows.Forms.Label lbl_DetectionFormat;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.Label lbl_Confidence;
        public System.Windows.Forms.TextBox tb_ConfidenceFormat;
        private System.Windows.Forms.Button bt_variables;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox tb_sound_cooldown;
        public System.Windows.Forms.TextBox tb_ActionCancelSecs;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.CheckBox cb_ShowOnlyRelevant;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel LnkPushoverObjects;
        private System.Windows.Forms.LinkLabel lnkTelegramTriggeringObjects;
        private System.Windows.Forms.LinkLabel linkLabel1;
        public System.Windows.Forms.CheckBox cb_UrlCancelEnabled;
        public System.Windows.Forms.CheckBox cb_UrlTriggerEnabled;
        public System.Windows.Forms.GroupBox groupBoxUrlCancel;
        public System.Windows.Forms.GroupBox groupBoxUrlTrigger;
        public System.Windows.Forms.TextBox tb_ActionDelayMS;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox tb_NetworkFolderCleanupDays;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.CheckBox cb_ActivateBlueIrisWindow;
    }
}