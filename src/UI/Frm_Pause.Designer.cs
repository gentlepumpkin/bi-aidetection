
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cb_paused = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_cameras
            // 
            this.cmb_cameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cameras.FormattingEnabled = true;
            this.cmb_cameras.Location = new System.Drawing.Point(108, 18);
            this.cmb_cameras.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmb_cameras.Name = "cmb_cameras";
            this.cmb_cameras.Size = new System.Drawing.Size(230, 28);
            this.cmb_cameras.TabIndex = 0;
            this.cmb_cameras.SelectedIndexChanged += new System.EventHandler(this.cmb_cameras_SelectedIndexChanged);
            this.cmb_cameras.SelectionChangeCommitted += new System.EventHandler(this.cmb_cameras_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Camera";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pause for";
            // 
            // tb_minutes
            // 
            this.tb_minutes.Location = new System.Drawing.Point(108, 60);
            this.tb_minutes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_minutes.Name = "tb_minutes";
            this.tb_minutes.Size = new System.Drawing.Size(80, 26);
            this.tb_minutes.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(200, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
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
            this.groupBox1.Location = new System.Drawing.Point(28, 108);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(452, 112);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pause the following Services";
            // 
            // cb_URL
            // 
            this.cb_URL.AutoSize = true;
            this.cb_URL.Location = new System.Drawing.Point(207, 69);
            this.cb_URL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_URL.Name = "cb_URL";
            this.cb_URL.Size = new System.Drawing.Size(182, 24);
            this.cb_URL.TabIndex = 0;
            this.cb_URL.Text = "Trigger / Cancel URL";
            this.toolTip1.SetToolTip(this.cb_URL, "Prevent URL actions");
            this.cb_URL.UseVisualStyleBackColor = true;
            // 
            // cb_Pushover
            // 
            this.cb_Pushover.AutoSize = true;
            this.cb_Pushover.Location = new System.Drawing.Point(321, 34);
            this.cb_Pushover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_Pushover.Name = "cb_Pushover";
            this.cb_Pushover.Size = new System.Drawing.Size(101, 24);
            this.cb_Pushover.TabIndex = 0;
            this.cb_Pushover.Text = "Pushover";
            this.toolTip1.SetToolTip(this.cb_Pushover, "Prevent pushover actions");
            this.cb_Pushover.UseVisualStyleBackColor = true;
            // 
            // cb_MQTT
            // 
            this.cb_MQTT.AutoSize = true;
            this.cb_MQTT.Location = new System.Drawing.Point(20, 69);
            this.cb_MQTT.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_MQTT.Name = "cb_MQTT";
            this.cb_MQTT.Size = new System.Drawing.Size(78, 24);
            this.cb_MQTT.TabIndex = 0;
            this.cb_MQTT.Text = "MQTT";
            this.toolTip1.SetToolTip(this.cb_MQTT, "Prevent MQTT actions");
            this.cb_MQTT.UseVisualStyleBackColor = true;
            // 
            // cb_Telegram
            // 
            this.cb_Telegram.AutoSize = true;
            this.cb_Telegram.Location = new System.Drawing.Point(207, 34);
            this.cb_Telegram.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_Telegram.Name = "cb_Telegram";
            this.cb_Telegram.Size = new System.Drawing.Size(101, 24);
            this.cb_Telegram.TabIndex = 0;
            this.cb_Telegram.Text = "Telegram";
            this.toolTip1.SetToolTip(this.cb_Telegram, "Prevent Telegram actions");
            this.cb_Telegram.UseVisualStyleBackColor = true;
            // 
            // cb_FileMonitoring
            // 
            this.cb_FileMonitoring.AutoSize = true;
            this.cb_FileMonitoring.Location = new System.Drawing.Point(20, 34);
            this.cb_FileMonitoring.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_FileMonitoring.Name = "cb_FileMonitoring";
            this.cb_FileMonitoring.Size = new System.Drawing.Size(181, 24);
            this.cb_FileMonitoring.TabIndex = 0;
            this.cb_FileMonitoring.Text = "ALL (File Monitoring)";
            this.toolTip1.SetToolTip(this.cb_FileMonitoring, "This will prevent any new images from being processed by the AI server");
            this.cb_FileMonitoring.UseVisualStyleBackColor = true;
            // 
            // lbl_resumingtime
            // 
            this.lbl_resumingtime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_resumingtime.AutoSize = true;
            this.lbl_resumingtime.Location = new System.Drawing.Point(24, 240);
            this.lbl_resumingtime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_resumingtime.Name = "lbl_resumingtime";
            this.lbl_resumingtime.Size = new System.Drawing.Size(175, 20);
            this.lbl_resumingtime.TabIndex = 6;
            this.lbl_resumingtime.Text = "Resuming in xx minutes";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cb_paused
            // 
            this.cb_paused.Appearance = System.Windows.Forms.Appearance.Button;
            this.cb_paused.Location = new System.Drawing.Point(371, 228);
            this.cb_paused.Name = "cb_paused";
            this.cb_paused.Size = new System.Drawing.Size(109, 48);
            this.cb_paused.TabIndex = 9;
            this.cb_paused.Text = "Pause";
            this.cb_paused.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_paused.UseVisualStyleBackColor = true;
            this.cb_paused.CheckedChanged += new System.EventHandler(this.cb_paused_CheckedChanged);
            // 
            // Frm_Pause
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 288);
            this.Controls.Add(this.cb_paused);
            this.Controls.Add(this.lbl_resumingtime);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_minutes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_cameras);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cb_paused;
    }
}