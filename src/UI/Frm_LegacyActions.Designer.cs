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
            this.tbTriggerUrl = new System.Windows.Forms.TextBox();
            this.lblTriggerUrl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_telegram = new System.Windows.Forms.CheckBox();
            this.cb_TriggerCancels = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_cooldown = new System.Windows.Forms.TextBox();
            this.cb_copyAlertImages = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tb_network_folder = new System.Windows.Forms.TextBox();
            this.cb_UseOriginalFilename = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(1140, 394);
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
            this.btnSave.Location = new System.Drawing.Point(998, 394);
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
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cb_UseOriginalFilename);
            this.groupBox1.Controls.Add(this.tb_network_folder);
            this.groupBox1.Controls.Add(this.cb_copyAlertImages);
            this.groupBox1.Controls.Add(this.tbTriggerUrl);
            this.groupBox1.Controls.Add(this.lblTriggerUrl);
            this.groupBox1.Controls.Add(this.cb_telegram);
            this.groupBox1.Controls.Add(this.cb_TriggerCancels);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_cooldown);
            this.groupBox1.Location = new System.Drawing.Point(13, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1234, 370);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // tbTriggerUrl
            // 
            this.tbTriggerUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTriggerUrl.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTriggerUrl.Location = new System.Drawing.Point(30, 249);
            this.tbTriggerUrl.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tbTriggerUrl.Multiline = true;
            this.tbTriggerUrl.Name = "tbTriggerUrl";
            this.tbTriggerUrl.Size = new System.Drawing.Size(1186, 102);
            this.tbTriggerUrl.TabIndex = 22;
            this.toolTip1.SetToolTip(this.tbTriggerUrl, "A list of URLs each on their own line OR seperated with commas that will be trigg" +
        "ered on an alert");
            // 
            // lblTriggerUrl
            // 
            this.lblTriggerUrl.AutoSize = true;
            this.lblTriggerUrl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTriggerUrl.Location = new System.Drawing.Point(25, 213);
            this.lblTriggerUrl.Margin = new System.Windows.Forms.Padding(35, 0, 5, 0);
            this.lblTriggerUrl.MinimumSize = new System.Drawing.Size(158, 0);
            this.lblTriggerUrl.Name = "lblTriggerUrl";
            this.lblTriggerUrl.Size = new System.Drawing.Size(158, 30);
            this.lblTriggerUrl.TabIndex = 1;
            this.lblTriggerUrl.Text = "Trigger URL(s):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(297, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 30);
            this.label6.TabIndex = 2;
            this.label6.Text = "Minutes";
            // 
            // cb_telegram
            // 
            this.cb_telegram.AutoSize = true;
            this.cb_telegram.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_telegram.Location = new System.Drawing.Point(30, 77);
            this.cb_telegram.Margin = new System.Windows.Forms.Padding(10, 6, 5, 6);
            this.cb_telegram.Name = "cb_telegram";
            this.cb_telegram.Size = new System.Drawing.Size(319, 34);
            this.cb_telegram.TabIndex = 24;
            this.cb_telegram.Text = "Send alert images to Telegram";
            this.cb_telegram.UseVisualStyleBackColor = false;
            // 
            // cb_TriggerCancels
            // 
            this.cb_TriggerCancels.AutoSize = true;
            this.cb_TriggerCancels.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_TriggerCancels.Location = new System.Drawing.Point(30, 173);
            this.cb_TriggerCancels.Margin = new System.Windows.Forms.Padding(10, 6, 5, 6);
            this.cb_TriggerCancels.Name = "cb_TriggerCancels";
            this.cb_TriggerCancels.Size = new System.Drawing.Size(179, 34);
            this.cb_TriggerCancels.TabIndex = 25;
            this.cb_TriggerCancels.Text = "Trigger Cancels";
            this.toolTip1.SetToolTip(this.cb_TriggerCancels, resources.GetString("cb_TriggerCancels.ToolTip"));
            this.cb_TriggerCancels.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(30, 32);
            this.label5.Margin = new System.Windows.Forms.Padding(35, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 30);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cooldown Time";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_cooldown
            // 
            this.tb_cooldown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tb_cooldown.Location = new System.Drawing.Point(202, 30);
            this.tb_cooldown.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.tb_cooldown.Name = "tb_cooldown";
            this.tb_cooldown.Size = new System.Drawing.Size(79, 35);
            this.tb_cooldown.TabIndex = 21;
            // 
            // cb_copyAlertImages
            // 
            this.cb_copyAlertImages.AutoSize = true;
            this.cb_copyAlertImages.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cb_copyAlertImages.Location = new System.Drawing.Point(30, 125);
            this.cb_copyAlertImages.Margin = new System.Windows.Forms.Padding(40, 8, 7, 8);
            this.cb_copyAlertImages.Name = "cb_copyAlertImages";
            this.cb_copyAlertImages.Size = new System.Drawing.Size(295, 34);
            this.cb_copyAlertImages.TabIndex = 27;
            this.cb_copyAlertImages.Text = "Copy alert images to folder:";
            this.toolTip1.SetToolTip(this.cb_copyAlertImages, "When an object in an image is detected, copy the image to the\r\n folder specified");
            this.cb_copyAlertImages.UseVisualStyleBackColor = false;
            // 
            // tb_network_folder
            // 
            this.tb_network_folder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_network_folder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_network_folder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tb_network_folder.Location = new System.Drawing.Point(335, 125);
            this.tb_network_folder.Name = "tb_network_folder";
            this.tb_network_folder.Size = new System.Drawing.Size(624, 35);
            this.tb_network_folder.TabIndex = 28;
            // 
            // cb_UseOriginalFilename
            // 
            this.cb_UseOriginalFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_UseOriginalFilename.AutoSize = true;
            this.cb_UseOriginalFilename.Location = new System.Drawing.Point(975, 125);
            this.cb_UseOriginalFilename.Name = "cb_UseOriginalFilename";
            this.cb_UseOriginalFilename.Size = new System.Drawing.Size(241, 34);
            this.cb_UseOriginalFilename.TabIndex = 29;
            this.cb_UseOriginalFilename.Text = "Use Original Filename";
            this.toolTip1.SetToolTip(this.cb_UseOriginalFilename, "When this is unchecked the image will be copied as CAMNAME.JPG");
            this.cb_UseOriginalFilename.UseVisualStyleBackColor = true;
            // 
            // Frm_LegacyActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1262, 460);
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
        public System.Windows.Forms.CheckBox cb_TriggerCancels;
        public System.Windows.Forms.TextBox tb_cooldown;
        public System.Windows.Forms.CheckBox cb_copyAlertImages;
        public System.Windows.Forms.TextBox tb_network_folder;
        public System.Windows.Forms.CheckBox cb_UseOriginalFilename;
    }
}