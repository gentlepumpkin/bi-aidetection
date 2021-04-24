
namespace AITool
{
    partial class Frm_UpdateCheck
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
            this.bt_check = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_CurrentVersion = new System.Windows.Forms.Label();
            this.bt_InstallBeta = new System.Windows.Forms.Button();
            this.linkLabelRelease = new System.Windows.Forms.LinkLabel();
            this.linkLabelBeta = new System.Windows.Forms.LinkLabel();
            this.bt_installRelease = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_Donate = new System.Windows.Forms.Button();
            this.linkLabelGithub = new System.Windows.Forms.LinkLabel();
            this.linkLabelIPCam = new System.Windows.Forms.LinkLabel();
            this.linkLabelReportIssue = new System.Windows.Forms.LinkLabel();
            this.lbl_message = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_check
            // 
            this.bt_check.Location = new System.Drawing.Point(9, 94);
            this.bt_check.Margin = new System.Windows.Forms.Padding(2);
            this.bt_check.Name = "bt_check";
            this.bt_check.Size = new System.Drawing.Size(85, 24);
            this.bt_check.TabIndex = 0;
            this.bt_check.Text = "Check";
            this.bt_check.UseVisualStyleBackColor = true;
            this.bt_check.Click += new System.EventHandler(this.bt_check_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current Official Release:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Current Beta Release:";
            // 
            // lbl_CurrentVersion
            // 
            this.lbl_CurrentVersion.AutoSize = true;
            this.lbl_CurrentVersion.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CurrentVersion.Location = new System.Drawing.Point(137, 14);
            this.lbl_CurrentVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_CurrentVersion.Name = "lbl_CurrentVersion";
            this.lbl_CurrentVersion.Size = new System.Drawing.Size(91, 13);
            this.lbl_CurrentVersion.TabIndex = 2;
            this.lbl_CurrentVersion.Text = "1.1.1 (2/2/22)";
            // 
            // bt_InstallBeta
            // 
            this.bt_InstallBeta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_InstallBeta.Enabled = false;
            this.bt_InstallBeta.Location = new System.Drawing.Point(271, 33);
            this.bt_InstallBeta.Margin = new System.Windows.Forms.Padding(2);
            this.bt_InstallBeta.Name = "bt_InstallBeta";
            this.bt_InstallBeta.Size = new System.Drawing.Size(69, 23);
            this.bt_InstallBeta.TabIndex = 0;
            this.bt_InstallBeta.Text = "Install";
            this.toolTip1.SetToolTip(this.bt_InstallBeta, "Download latest beta installer");
            this.bt_InstallBeta.UseVisualStyleBackColor = true;
            this.bt_InstallBeta.Click += new System.EventHandler(this.bt_InstallBeta_Click);
            // 
            // linkLabelRelease
            // 
            this.linkLabelRelease.AutoSize = true;
            this.linkLabelRelease.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelRelease.Location = new System.Drawing.Point(137, 66);
            this.linkLabelRelease.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabelRelease.Name = "linkLabelRelease";
            this.linkLabelRelease.Size = new System.Drawing.Size(13, 13);
            this.linkLabelRelease.TabIndex = 3;
            this.linkLabelRelease.TabStop = true;
            this.linkLabelRelease.Text = ".";
            this.toolTip1.SetToolTip(this.linkLabelRelease, "Click to go to release page");
            this.linkLabelRelease.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRelease_LinkClicked);
            // 
            // linkLabelBeta
            // 
            this.linkLabelBeta.AutoSize = true;
            this.linkLabelBeta.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelBeta.Location = new System.Drawing.Point(137, 39);
            this.linkLabelBeta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabelBeta.Name = "linkLabelBeta";
            this.linkLabelBeta.Size = new System.Drawing.Size(13, 13);
            this.linkLabelBeta.TabIndex = 3;
            this.linkLabelBeta.TabStop = true;
            this.linkLabelBeta.Text = ".";
            this.toolTip1.SetToolTip(this.linkLabelBeta, "Click to go to beta installer page");
            this.linkLabelBeta.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelBeta_LinkClicked);
            // 
            // bt_installRelease
            // 
            this.bt_installRelease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_installRelease.Enabled = false;
            this.bt_installRelease.Location = new System.Drawing.Point(271, 61);
            this.bt_installRelease.Margin = new System.Windows.Forms.Padding(2);
            this.bt_installRelease.Name = "bt_installRelease";
            this.bt_installRelease.Size = new System.Drawing.Size(69, 22);
            this.bt_installRelease.TabIndex = 4;
            this.bt_installRelease.Text = "Install";
            this.toolTip1.SetToolTip(this.bt_installRelease, "Download latest official release version");
            this.bt_installRelease.UseVisualStyleBackColor = true;
            this.bt_installRelease.Click += new System.EventHandler(this.bt_installRelease_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_Donate);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabelGithub);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabelIPCam);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabelReportIssue);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_message);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.bt_installRelease);
            this.splitContainer1.Panel1.Controls.Add(this.bt_check);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabelBeta);
            this.splitContainer1.Panel1.Controls.Add(this.bt_InstallBeta);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabelRelease);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_CurrentVersion);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Size = new System.Drawing.Size(752, 275);
            this.splitContainer1.SplitterDistance = 348;
            this.splitContainer1.TabIndex = 5;
            // 
            // btn_Donate
            // 
            this.btn_Donate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Donate.Image = global::AITool.Properties.Resources.donate_button;
            this.btn_Donate.Location = new System.Drawing.Point(9, 208);
            this.btn_Donate.Name = "btn_Donate";
            this.btn_Donate.Size = new System.Drawing.Size(79, 29);
            this.btn_Donate.TabIndex = 9;
            this.btn_Donate.UseVisualStyleBackColor = true;
            this.btn_Donate.Click += new System.EventHandler(this.btn_Donate_Click);
            // 
            // linkLabelGithub
            // 
            this.linkLabelGithub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelGithub.AutoSize = true;
            this.linkLabelGithub.Location = new System.Drawing.Point(226, 251);
            this.linkLabelGithub.Name = "linkLabelGithub";
            this.linkLabelGithub.Size = new System.Drawing.Size(95, 13);
            this.linkLabelGithub.TabIndex = 8;
            this.linkLabelGithub.TabStop = true;
            this.linkLabelGithub.Text = "Github Discussion ";
            // 
            // linkLabelIPCam
            // 
            this.linkLabelIPCam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelIPCam.AutoSize = true;
            this.linkLabelIPCam.Location = new System.Drawing.Point(107, 251);
            this.linkLabelIPCam.Name = "linkLabelIPCam";
            this.linkLabelIPCam.Size = new System.Drawing.Size(91, 13);
            this.linkLabelIPCam.TabIndex = 7;
            this.linkLabelIPCam.TabStop = true;
            this.linkLabelIPCam.Text = "IPCamTalk Forum";
            this.linkLabelIPCam.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelIPCam_LinkClicked);
            // 
            // linkLabelReportIssue
            // 
            this.linkLabelReportIssue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelReportIssue.AutoSize = true;
            this.linkLabelReportIssue.Location = new System.Drawing.Point(12, 251);
            this.linkLabelReportIssue.Name = "linkLabelReportIssue";
            this.linkLabelReportIssue.Size = new System.Drawing.Size(67, 13);
            this.linkLabelReportIssue.TabIndex = 6;
            this.linkLabelReportIssue.TabStop = true;
            this.linkLabelReportIssue.Text = "Report Issue";
            this.linkLabelReportIssue.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelReportIssue_LinkClicked);
            // 
            // lbl_message
            // 
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.ForeColor = System.Drawing.Color.Green;
            this.lbl_message.Location = new System.Drawing.Point(137, 100);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(203, 52);
            this.lbl_message.TabIndex = 5;
            this.lbl_message.Text = "A newer version is available. ";
            this.lbl_message.Visible = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(3, 29);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(390, 239);
            this.webBrowser1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(395, 26);
            this.label4.TabIndex = 0;
            this.label4.Text = "Release Notes";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Frm_UpdateCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 275);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Frm_UpdateCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "SAVE";
            this.Text = "Check for updates";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_UpdateCheck_FormClosing);
            this.Load += new System.EventHandler(this.Frm_UpdateCheck_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_check;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_CurrentVersion;
        private System.Windows.Forms.Button bt_InstallBeta;
        private System.Windows.Forms.LinkLabel linkLabelRelease;
        private System.Windows.Forms.LinkLabel linkLabelBeta;
        private System.Windows.Forms.Button bt_installRelease;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.LinkLabel linkLabelReportIssue;
        private System.Windows.Forms.LinkLabel linkLabelGithub;
        private System.Windows.Forms.LinkLabel linkLabelIPCam;
        private System.Windows.Forms.Button btn_Donate;
    }
}