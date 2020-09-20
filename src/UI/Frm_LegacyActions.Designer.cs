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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_mergeannotations = new System.Windows.Forms.CheckBox();
            this.tb_network_folder_filename = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_telegram_caption = new System.Windows.Forms.TextBox();
            this.linkLabelMqttSettings = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_MQTT_Payload_cancel = new System.Windows.Forms.TextBox();
            this.tb_MQTT_Payload = new System.Windows.Forms.TextBox();
            this.tb_MQTT_Topic_Cancel = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_MQTT_Topic = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_MQTT_enabled = new System.Windows.Forms.CheckBox();
            this.tb_Sounds = new System.Windows.Forms.TextBox();
            this.cb_PlaySound = new System.Windows.Forms.CheckBox();
            this.tb_RunExternalProgramArgs = new System.Windows.Forms.TextBox();
            this.tb_RunExternalProgram = new System.Windows.Forms.TextBox();
            this.cb_RunProgram = new System.Windows.Forms.CheckBox();
            this.tb_network_folder = new System.Windows.Forms.TextBox();
            this.cb_copyAlertImages = new System.Windows.Forms.CheckBox();
            this.tbCancelUrl = new System.Windows.Forms.TextBox();
            this.tbTriggerUrl = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblTriggerUrl = new System.Windows.Forms.Label();
            this.cb_telegram = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_cooldown = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.btTest = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(1135, 729);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 52);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.Location = new System.Drawing.Point(994, 729);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 52);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.cb_mergeannotations);
            this.groupBox1.Controls.Add(this.tb_network_folder_filename);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tb_telegram_caption);
            this.groupBox1.Controls.Add(this.linkLabelMqttSettings);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_MQTT_Payload_cancel);
            this.groupBox1.Controls.Add(this.tb_MQTT_Payload);
            this.groupBox1.Controls.Add(this.tb_MQTT_Topic_Cancel);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tb_MQTT_Topic);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cb_MQTT_enabled);
            this.groupBox1.Controls.Add(this.tb_Sounds);
            this.groupBox1.Controls.Add(this.cb_PlaySound);
            this.groupBox1.Controls.Add(this.tb_RunExternalProgramArgs);
            this.groupBox1.Controls.Add(this.tb_RunExternalProgram);
            this.groupBox1.Controls.Add(this.cb_RunProgram);
            this.groupBox1.Controls.Add(this.tb_network_folder);
            this.groupBox1.Controls.Add(this.cb_copyAlertImages);
            this.groupBox1.Controls.Add(this.tbCancelUrl);
            this.groupBox1.Controls.Add(this.tbTriggerUrl);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblTriggerUrl);
            this.groupBox1.Controls.Add(this.cb_telegram);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_cooldown);
            this.groupBox1.Location = new System.Drawing.Point(13, 146);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1229, 571);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // cb_mergeannotations
            // 
            this.cb_mergeannotations.AutoSize = true;
            this.cb_mergeannotations.Location = new System.Drawing.Point(436, 31);
            this.cb_mergeannotations.Name = "cb_mergeannotations";
            this.cb_mergeannotations.Size = new System.Drawing.Size(340, 34);
            this.cb_mergeannotations.TabIndex = 46;
            this.cb_mergeannotations.Text = "Merge Annotations Into Images ";
            this.toolTip1.SetToolTip(this.cb_mergeannotations, "Merge detected object text and rectangles into actual image.");
            this.cb_mergeannotations.UseVisualStyleBackColor = true;
            // 
            // tb_network_folder_filename
            // 
            this.tb_network_folder_filename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_network_folder_filename.Location = new System.Drawing.Point(882, 126);
            this.tb_network_folder_filename.Name = "tb_network_folder_filename";
            this.tb_network_folder_filename.Size = new System.Drawing.Size(329, 35);
            this.tb_network_folder_filename.TabIndex = 45;
            this.toolTip1.SetToolTip(this.tb_network_folder_filename, "The filename to be created in the network folder NOT including file extension.  F" +
        "or example, [camera] would be saved as MYCAMERA.JPG");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(775, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 30);
            this.label8.TabIndex = 44;
            this.label8.Text = "Filename:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(340, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 30);
            this.label7.TabIndex = 43;
            this.label7.Text = "Caption:";
            // 
            // tb_telegram_caption
            // 
            this.tb_telegram_caption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_telegram_caption.Location = new System.Drawing.Point(436, 78);
            this.tb_telegram_caption.Name = "tb_telegram_caption";
            this.tb_telegram_caption.Size = new System.Drawing.Size(775, 35);
            this.tb_telegram_caption.TabIndex = 42;
            // 
            // linkLabelMqttSettings
            // 
            this.linkLabelMqttSettings.AutoSize = true;
            this.linkLabelMqttSettings.Location = new System.Drawing.Point(129, 260);
            this.linkLabelMqttSettings.Name = "linkLabelMqttSettings";
            this.linkLabelMqttSettings.Size = new System.Drawing.Size(87, 30);
            this.linkLabelMqttSettings.TabIndex = 41;
            this.linkLabelMqttSettings.TabStop = true;
            this.linkLabelMqttSettings.Text = "Settings";
            this.toolTip1.SetToolTip(this.linkLabelMqttSettings, "Global MQTT Settings");
            this.linkLabelMqttSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMqttSettings_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(791, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 30);
            this.label4.TabIndex = 40;
            this.label4.Text = "Params:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.DarkRed;
            this.label10.Location = new System.Drawing.Point(786, 300);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 30);
            this.label10.TabIndex = 39;
            this.label10.Text = "Payload:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(786, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 30);
            this.label2.TabIndex = 39;
            this.label2.Text = "Payload:";
            // 
            // tb_MQTT_Payload_cancel
            // 
            this.tb_MQTT_Payload_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_MQTT_Payload_cancel.Location = new System.Drawing.Point(882, 299);
            this.tb_MQTT_Payload_cancel.Name = "tb_MQTT_Payload_cancel";
            this.tb_MQTT_Payload_cancel.Size = new System.Drawing.Size(329, 35);
            this.tb_MQTT_Payload_cancel.TabIndex = 38;
            // 
            // tb_MQTT_Payload
            // 
            this.tb_MQTT_Payload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_MQTT_Payload.Location = new System.Drawing.Point(882, 258);
            this.tb_MQTT_Payload.Name = "tb_MQTT_Payload";
            this.tb_MQTT_Payload.Size = new System.Drawing.Size(329, 35);
            this.tb_MQTT_Payload.TabIndex = 38;
            // 
            // tb_MQTT_Topic_Cancel
            // 
            this.tb_MQTT_Topic_Cancel.Location = new System.Drawing.Point(436, 299);
            this.tb_MQTT_Topic_Cancel.Name = "tb_MQTT_Topic_Cancel";
            this.tb_MQTT_Topic_Cancel.Size = new System.Drawing.Size(328, 35);
            this.tb_MQTT_Topic_Cancel.TabIndex = 37;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DarkRed;
            this.label9.Location = new System.Drawing.Point(296, 300);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 30);
            this.label9.TabIndex = 36;
            this.label9.Text = "Cancel Topic:";
            // 
            // tb_MQTT_Topic
            // 
            this.tb_MQTT_Topic.Location = new System.Drawing.Point(436, 258);
            this.tb_MQTT_Topic.Name = "tb_MQTT_Topic";
            this.tb_MQTT_Topic.Size = new System.Drawing.Size(328, 35);
            this.tb_MQTT_Topic.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 30);
            this.label1.TabIndex = 36;
            this.label1.Text = "Trigger Topic:";
            // 
            // cb_MQTT_enabled
            // 
            this.cb_MQTT_enabled.AutoSize = true;
            this.cb_MQTT_enabled.Location = new System.Drawing.Point(13, 258);
            this.cb_MQTT_enabled.Name = "cb_MQTT_enabled";
            this.cb_MQTT_enabled.Size = new System.Drawing.Size(100, 34);
            this.cb_MQTT_enabled.TabIndex = 35;
            this.cb_MQTT_enabled.Text = "MQTT:";
            this.toolTip1.SetToolTip(this.cb_MQTT_enabled, "For now, see JSON config file for server, port, username, password settings");
            this.cb_MQTT_enabled.UseVisualStyleBackColor = true;
            // 
            // tb_Sounds
            // 
            this.tb_Sounds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Sounds.Location = new System.Drawing.Point(436, 214);
            this.tb_Sounds.Name = "tb_Sounds";
            this.tb_Sounds.Size = new System.Drawing.Size(775, 35);
            this.tb_Sounds.TabIndex = 34;
            this.toolTip1.SetToolTip(this.tb_Sounds, resources.GetString("tb_Sounds.ToolTip"));
            // 
            // cb_PlaySound
            // 
            this.cb_PlaySound.AutoSize = true;
            this.cb_PlaySound.Location = new System.Drawing.Point(13, 216);
            this.cb_PlaySound.Name = "cb_PlaySound";
            this.cb_PlaySound.Size = new System.Drawing.Size(147, 34);
            this.cb_PlaySound.TabIndex = 33;
            this.cb_PlaySound.Text = "Play Sound:";
            this.cb_PlaySound.UseVisualStyleBackColor = true;
            // 
            // tb_RunExternalProgramArgs
            // 
            this.tb_RunExternalProgramArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_RunExternalProgramArgs.Location = new System.Drawing.Point(882, 171);
            this.tb_RunExternalProgramArgs.Name = "tb_RunExternalProgramArgs";
            this.tb_RunExternalProgramArgs.Size = new System.Drawing.Size(329, 35);
            this.tb_RunExternalProgramArgs.TabIndex = 32;
            this.toolTip1.SetToolTip(this.tb_RunExternalProgramArgs, "Command line arguments to run the external app or script");
            // 
            // tb_RunExternalProgram
            // 
            this.tb_RunExternalProgram.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_RunExternalProgram.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tb_RunExternalProgram.Location = new System.Drawing.Point(436, 171);
            this.tb_RunExternalProgram.Name = "tb_RunExternalProgram";
            this.tb_RunExternalProgram.Size = new System.Drawing.Size(328, 35);
            this.tb_RunExternalProgram.TabIndex = 31;
            this.toolTip1.SetToolTip(this.tb_RunExternalProgram, "Path to EXE, BAT, etc");
            // 
            // cb_RunProgram
            // 
            this.cb_RunProgram.AutoSize = true;
            this.cb_RunProgram.Location = new System.Drawing.Point(13, 171);
            this.cb_RunProgram.Name = "cb_RunProgram";
            this.cb_RunProgram.Size = new System.Drawing.Size(246, 34);
            this.cb_RunProgram.TabIndex = 30;
            this.cb_RunProgram.Text = "Run external program:";
            this.cb_RunProgram.UseVisualStyleBackColor = true;
            // 
            // tb_network_folder
            // 
            this.tb_network_folder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_network_folder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tb_network_folder.Location = new System.Drawing.Point(436, 126);
            this.tb_network_folder.Name = "tb_network_folder";
            this.tb_network_folder.Size = new System.Drawing.Size(328, 35);
            this.tb_network_folder.TabIndex = 28;
            // 
            // cb_copyAlertImages
            // 
            this.cb_copyAlertImages.AutoSize = true;
            this.cb_copyAlertImages.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_copyAlertImages.Location = new System.Drawing.Point(13, 126);
            this.cb_copyAlertImages.Margin = new System.Windows.Forms.Padding(40, 8, 7, 8);
            this.cb_copyAlertImages.Name = "cb_copyAlertImages";
            this.cb_copyAlertImages.Size = new System.Drawing.Size(295, 34);
            this.cb_copyAlertImages.TabIndex = 27;
            this.cb_copyAlertImages.Text = "Copy alert images to folder:";
            this.toolTip1.SetToolTip(this.cb_copyAlertImages, "When an object in an image is detected, copy the image to the\r\n folder specified");
            this.cb_copyAlertImages.UseVisualStyleBackColor = false;
            // 
            // tbCancelUrl
            // 
            this.tbCancelUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCancelUrl.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCancelUrl.Location = new System.Drawing.Point(13, 478);
            this.tbCancelUrl.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tbCancelUrl.Multiline = true;
            this.tbCancelUrl.Name = "tbCancelUrl";
            this.tbCancelUrl.Size = new System.Drawing.Size(1198, 79);
            this.tbCancelUrl.TabIndex = 22;
            this.toolTip1.SetToolTip(this.tbCancelUrl, "URLs that cancel the alert - For BI, use ");
            // 
            // tbTriggerUrl
            // 
            this.tbTriggerUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTriggerUrl.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTriggerUrl.Location = new System.Drawing.Point(13, 364);
            this.tbTriggerUrl.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tbTriggerUrl.Multiline = true;
            this.tbTriggerUrl.Name = "tbTriggerUrl";
            this.tbTriggerUrl.Size = new System.Drawing.Size(1198, 79);
            this.tbTriggerUrl.TabIndex = 22;
            this.tbTriggerUrl.Text = "test\r\ntest2\r\ntest3";
            this.toolTip1.SetToolTip(this.tbTriggerUrl, "A list of URLs each on their own line OR seperated with commas that will be trigg" +
        "ered on an alert");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label11.ForeColor = System.Drawing.Color.DarkRed;
            this.label11.Location = new System.Drawing.Point(8, 449);
            this.label11.Margin = new System.Windows.Forms.Padding(35, 0, 5, 0);
            this.label11.MinimumSize = new System.Drawing.Size(158, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(158, 30);
            this.label11.TabIndex = 1;
            this.label11.Text = "Cancel URL(s):";
            // 
            // lblTriggerUrl
            // 
            this.lblTriggerUrl.AutoSize = true;
            this.lblTriggerUrl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTriggerUrl.Location = new System.Drawing.Point(8, 331);
            this.lblTriggerUrl.Margin = new System.Windows.Forms.Padding(35, 0, 5, 0);
            this.lblTriggerUrl.MinimumSize = new System.Drawing.Size(158, 0);
            this.lblTriggerUrl.Name = "lblTriggerUrl";
            this.lblTriggerUrl.Size = new System.Drawing.Size(158, 30);
            this.lblTriggerUrl.TabIndex = 1;
            this.lblTriggerUrl.Text = "Trigger URL(s):";
            // 
            // cb_telegram
            // 
            this.cb_telegram.AutoSize = true;
            this.cb_telegram.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_telegram.Location = new System.Drawing.Point(13, 78);
            this.cb_telegram.Margin = new System.Windows.Forms.Padding(10, 6, 5, 6);
            this.cb_telegram.Name = "cb_telegram";
            this.cb_telegram.Size = new System.Drawing.Size(319, 34);
            this.cb_telegram.TabIndex = 24;
            this.cb_telegram.Text = "Send alert images to Telegram";
            this.cb_telegram.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(13, 33);
            this.label5.Margin = new System.Windows.Forms.Padding(35, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 30);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cooldown Time";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(280, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 30);
            this.label6.TabIndex = 2;
            this.label6.Text = "Minutes";
            // 
            // tb_cooldown
            // 
            this.tb_cooldown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tb_cooldown.Location = new System.Drawing.Point(185, 31);
            this.tb_cooldown.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tb_cooldown.Name = "tb_cooldown";
            this.tb_cooldown.Size = new System.Drawing.Size(79, 35);
            this.tb_cooldown.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Consolas", 8.142858F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1221, 127);
            this.label3.TabIndex = 5;
            this.label3.Text = resources.GetString("label3.Text");
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btTest
            // 
            this.btTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btTest.Location = new System.Drawing.Point(853, 729);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(109, 52);
            this.btTest.TabIndex = 6;
            this.btTest.Text = "Test";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // Frm_LegacyActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1257, 795);
            this.Controls.Add(this.btTest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_LegacyActions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Actions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_LegacyActions_FormClosing);
            this.Load += new System.EventHandler(this.Frm_LegacyActions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTriggerUrl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox tb_MQTT_Payload;
        public System.Windows.Forms.TextBox tb_MQTT_Topic;
        public System.Windows.Forms.CheckBox cb_MQTT_enabled;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabelMqttSettings;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox tb_telegram_caption;
        public System.Windows.Forms.TextBox tb_network_folder_filename;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.CheckBox cb_mergeannotations;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox tb_MQTT_Payload_cancel;
        public System.Windows.Forms.TextBox tb_MQTT_Topic_Cancel;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox tbCancelUrl;
        private System.Windows.Forms.Label label11;
    }
}