
namespace AITool
{
    partial class Frm_Pause
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
            this.cmb_cameras = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_minutes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_URL = new System.Windows.Forms.CheckBox();
            this.cb_Pushover = new System.Windows.Forms.CheckBox();
            this.cb_MQTT = new System.Windows.Forms.CheckBox();
            this.cb_Telegram = new System.Windows.Forms.CheckBox();
            this.cb_FileMonitoring = new System.Windows.Forms.CheckBox();
            this.lbl_resumingtime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cb_Paused = new System.Windows.Forms.CheckBox();
            this.bt_save = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_cameras
            // 
            this.cmb_cameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cameras.FormattingEnabled = true;
            this.cmb_cameras.Location = new System.Drawing.Point(72, 12);
            this.cmb_cameras.Name = "cmb_cameras";
            this.cmb_cameras.Size = new System.Drawing.Size(155, 21);
            this.cmb_cameras.TabIndex = 0;
            this.cmb_cameras.SelectionChangeCommitted += new System.EventHandler(this.cmb_cameras_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Camera";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pause for";
            // 
            // tb_minutes
            // 
            this.tb_minutes.Location = new System.Drawing.Point(72, 39);
            this.tb_minutes.Name = "tb_minutes";
            this.tb_minutes.Size = new System.Drawing.Size(55, 20);
            this.tb_minutes.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Minutes";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cb_URL);
            this.groupBox1.Controls.Add(this.cb_Pushover);
            this.groupBox1.Controls.Add(this.cb_MQTT);
            this.groupBox1.Controls.Add(this.cb_Telegram);
            this.groupBox1.Controls.Add(this.cb_FileMonitoring);
            this.groupBox1.Location = new System.Drawing.Point(19, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 73);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pause the following Services";
            // 
            // cb_URL
            // 
            this.cb_URL.AutoSize = true;
            this.cb_URL.Location = new System.Drawing.Point(138, 45);
            this.cb_URL.Name = "cb_URL";
            this.cb_URL.Size = new System.Drawing.Size(128, 17);
            this.cb_URL.TabIndex = 0;
            this.cb_URL.Text = "Trigger / Cancel URL";
            this.toolTip1.SetToolTip(this.cb_URL, "Prevent URL actions");
            this.cb_URL.UseVisualStyleBackColor = true;
            // 
            // cb_Pushover
            // 
            this.cb_Pushover.AutoSize = true;
            this.cb_Pushover.Location = new System.Drawing.Point(214, 22);
            this.cb_Pushover.Name = "cb_Pushover";
            this.cb_Pushover.Size = new System.Drawing.Size(71, 17);
            this.cb_Pushover.TabIndex = 0;
            this.cb_Pushover.Text = "Pushover";
            this.toolTip1.SetToolTip(this.cb_Pushover, "Prevent pushover actions");
            this.cb_Pushover.UseVisualStyleBackColor = true;
            // 
            // cb_MQTT
            // 
            this.cb_MQTT.AutoSize = true;
            this.cb_MQTT.Location = new System.Drawing.Point(13, 45);
            this.cb_MQTT.Name = "cb_MQTT";
            this.cb_MQTT.Size = new System.Drawing.Size(57, 17);
            this.cb_MQTT.TabIndex = 0;
            this.cb_MQTT.Text = "MQTT";
            this.toolTip1.SetToolTip(this.cb_MQTT, "Prevent MQTT actions");
            this.cb_MQTT.UseVisualStyleBackColor = true;
            // 
            // cb_Telegram
            // 
            this.cb_Telegram.AutoSize = true;
            this.cb_Telegram.Location = new System.Drawing.Point(138, 22);
            this.cb_Telegram.Name = "cb_Telegram";
            this.cb_Telegram.Size = new System.Drawing.Size(70, 17);
            this.cb_Telegram.TabIndex = 0;
            this.cb_Telegram.Text = "Telegram";
            this.toolTip1.SetToolTip(this.cb_Telegram, "Prevent Telegram actions");
            this.cb_Telegram.UseVisualStyleBackColor = true;
            // 
            // cb_FileMonitoring
            // 
            this.cb_FileMonitoring.AutoSize = true;
            this.cb_FileMonitoring.Location = new System.Drawing.Point(13, 22);
            this.cb_FileMonitoring.Name = "cb_FileMonitoring";
            this.cb_FileMonitoring.Size = new System.Drawing.Size(122, 17);
            this.cb_FileMonitoring.TabIndex = 0;
            this.cb_FileMonitoring.Text = "ALL (File Monitoring)";
            this.toolTip1.SetToolTip(this.cb_FileMonitoring, "This will prevent any new images from being processed by the AI server");
            this.cb_FileMonitoring.UseVisualStyleBackColor = true;
            // 
            // lbl_resumingtime
            // 
            this.lbl_resumingtime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_resumingtime.AutoSize = true;
            this.lbl_resumingtime.Location = new System.Drawing.Point(16, 156);
            this.lbl_resumingtime.Name = "lbl_resumingtime";
            this.lbl_resumingtime.Size = new System.Drawing.Size(117, 13);
            this.lbl_resumingtime.TabIndex = 6;
            this.lbl_resumingtime.Text = "Resuming in xx minutes";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cb_Paused
            // 
            this.cb_Paused.AutoSize = true;
            this.cb_Paused.Location = new System.Drawing.Point(241, 15);
            this.cb_Paused.Name = "cb_Paused";
            this.cb_Paused.Size = new System.Drawing.Size(62, 17);
            this.cb_Paused.TabIndex = 7;
            this.cb_Paused.Text = "Paused";
            this.toolTip1.SetToolTip(this.cb_Paused, "Pause the current camera - Note that you must ALSO check at least one of the boxe" +
        "s below");
            this.cb_Paused.UseVisualStyleBackColor = true;
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(246, 147);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(74, 31);
            this.bt_save.TabIndex = 8;
            this.bt_save.Text = "Save";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // Frm_Pause
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 187);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.cb_Paused);
            this.Controls.Add(this.lbl_resumingtime);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_minutes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_cameras);
            this.Name = "Frm_Pause";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pause";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Pause_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Pause_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_cameras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_minutes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_URL;
        private System.Windows.Forms.CheckBox cb_Pushover;
        private System.Windows.Forms.CheckBox cb_MQTT;
        private System.Windows.Forms.CheckBox cb_Telegram;
        private System.Windows.Forms.CheckBox cb_FileMonitoring;
        private System.Windows.Forms.Label lbl_resumingtime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cb_Paused;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}