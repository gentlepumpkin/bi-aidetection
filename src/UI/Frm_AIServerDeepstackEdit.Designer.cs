
namespace AITool
{
    partial class Frm_AIServerDeepstackEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_type = new System.Windows.Forms.Label();
            this.tb_URL = new System.Windows.Forms.TextBox();
            this.bt_Save = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_ActiveTimeRange = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_ApplyToCams = new System.Windows.Forms.TextBox();
            this.chk_Enabled = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_ImagesPerMonth = new System.Windows.Forms.TextBox();
            this.btn_ImageAdjustEdit = new System.Windows.Forms.Button();
            this.cb_ImageAdjustProfile = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.linkHelpURL = new System.Windows.Forms.LinkLabel();
            this.btTest = new System.Windows.Forms.Button();
            this.bt_clear = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Lower = new System.Windows.Forms.TextBox();
            this.tb_Upper = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "URL:";
            // 
            // lbl_type
            // 
            this.lbl_type.AutoSize = true;
            this.lbl_type.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbl_type.Location = new System.Drawing.Point(108, 16);
            this.lbl_type.Name = "lbl_type";
            this.lbl_type.Size = new System.Drawing.Size(10, 13);
            this.lbl_type.TabIndex = 0;
            this.lbl_type.Text = ".";
            // 
            // tb_URL
            // 
            this.tb_URL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_URL.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_URL.Location = new System.Drawing.Point(111, 35);
            this.tb_URL.Name = "tb_URL";
            this.tb_URL.Size = new System.Drawing.Size(464, 20);
            this.tb_URL.TabIndex = 2;
            this.tb_URL.TextChanged += new System.EventHandler(this.tb_URL_TextChanged);
            // 
            // bt_Save
            // 
            this.bt_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bt_Save.Location = new System.Drawing.Point(516, 193);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(70, 30);
            this.bt_Save.TabIndex = 3;
            this.bt_Save.Text = "Save";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Active Time Range:";
            // 
            // tb_ActiveTimeRange
            // 
            this.tb_ActiveTimeRange.Location = new System.Drawing.Point(111, 87);
            this.tb_ActiveTimeRange.Name = "tb_ActiveTimeRange";
            this.tb_ActiveTimeRange.Size = new System.Drawing.Size(152, 20);
            this.tb_ActiveTimeRange.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tb_ActiveTimeRange, "Active time range in form of 00:00:00-23:59:59");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Apply to Cameras:";
            // 
            // tb_ApplyToCams
            // 
            this.tb_ApplyToCams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_ApplyToCams.Location = new System.Drawing.Point(111, 61);
            this.tb_ApplyToCams.Name = "tb_ApplyToCams";
            this.tb_ApplyToCams.Size = new System.Drawing.Size(463, 20);
            this.tb_ApplyToCams.TabIndex = 5;
            this.toolTip1.SetToolTip(this.tb_ApplyToCams, "A comma separated list of cameras that this AI server will work with.\r\n\r\nLeave em" +
        "pty for ALL.");
            // 
            // chk_Enabled
            // 
            this.chk_Enabled.AutoSize = true;
            this.chk_Enabled.Location = new System.Drawing.Point(11, 0);
            this.chk_Enabled.Name = "chk_Enabled";
            this.chk_Enabled.Size = new System.Drawing.Size(65, 17);
            this.chk_Enabled.TabIndex = 6;
            this.chk_Enabled.Text = "Enabled";
            this.chk_Enabled.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tb_Upper);
            this.groupBox1.Controls.Add(this.tb_Lower);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_ImagesPerMonth);
            this.groupBox1.Controls.Add(this.btn_ImageAdjustEdit);
            this.groupBox1.Controls.Add(this.cb_ImageAdjustProfile);
            this.groupBox1.Controls.Add(this.chk_Enabled);
            this.groupBox1.Controls.Add(this.tb_ApplyToCams);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_ActiveTimeRange);
            this.groupBox1.Controls.Add(this.lbl_type);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_URL);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(581, 168);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // tb_ImagesPerMonth
            // 
            this.tb_ImagesPerMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_ImagesPerMonth.Location = new System.Drawing.Point(525, 87);
            this.tb_ImagesPerMonth.Name = "tb_ImagesPerMonth";
            this.tb_ImagesPerMonth.Size = new System.Drawing.Size(49, 20);
            this.tb_ImagesPerMonth.TabIndex = 9;
            this.tb_ImagesPerMonth.Text = "0";
            this.toolTip1.SetToolTip(this.tb_ImagesPerMonth, "Max images per month - 0 for unlimited.    Amazon has 5000 free images a month");
            // 
            // btn_ImageAdjustEdit
            // 
            this.btn_ImageAdjustEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ImageAdjustEdit.Location = new System.Drawing.Point(269, 113);
            this.btn_ImageAdjustEdit.Name = "btn_ImageAdjustEdit";
            this.btn_ImageAdjustEdit.Size = new System.Drawing.Size(31, 21);
            this.btn_ImageAdjustEdit.TabIndex = 8;
            this.btn_ImageAdjustEdit.Text = "···";
            this.toolTip1.SetToolTip(this.btn_ImageAdjustEdit, "Edit the image adjust profile");
            this.btn_ImageAdjustEdit.UseVisualStyleBackColor = true;
            this.btn_ImageAdjustEdit.Click += new System.EventHandler(this.btn_ImageAdjustEdit_Click);
            // 
            // cb_ImageAdjustProfile
            // 
            this.cb_ImageAdjustProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ImageAdjustProfile.FormattingEnabled = true;
            this.cb_ImageAdjustProfile.Location = new System.Drawing.Point(111, 113);
            this.cb_ImageAdjustProfile.Name = "cb_ImageAdjustProfile";
            this.cb_ImageAdjustProfile.Size = new System.Drawing.Size(152, 21);
            this.cb_ImageAdjustProfile.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(400, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Max Images Per Month:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Image Adjust Profile:";
            // 
            // linkHelpURL
            // 
            this.linkHelpURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkHelpURL.AutoSize = true;
            this.linkHelpURL.Location = new System.Drawing.Point(2, 175);
            this.linkHelpURL.Name = "linkHelpURL";
            this.linkHelpURL.Size = new System.Drawing.Size(10, 13);
            this.linkHelpURL.TabIndex = 8;
            this.linkHelpURL.TabStop = true;
            this.linkHelpURL.Text = ".";
            this.linkHelpURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHelpURL_LinkClicked);
            // 
            // btTest
            // 
            this.btTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btTest.Location = new System.Drawing.Point(440, 193);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(70, 30);
            this.btTest.TabIndex = 9;
            this.btTest.Text = "Test";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // bt_clear
            // 
            this.bt_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_clear.Location = new System.Drawing.Point(364, 193);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new System.Drawing.Size(70, 30);
            this.bt_clear.TabIndex = 9;
            this.bt_clear.Text = "Clear Stats";
            this.bt_clear.UseVisualStyleBackColor = true;
            this.bt_clear.Click += new System.EventHandler(this.bt_clear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Confidence limits:";
            // 
            // tb_Lower
            // 
            this.tb_Lower.Location = new System.Drawing.Point(111, 140);
            this.tb_Lower.Name = "tb_Lower";
            this.tb_Lower.Size = new System.Drawing.Size(49, 20);
            this.tb_Lower.TabIndex = 11;
            this.tb_Lower.Tag = "";
            this.tb_Lower.Text = "0";
            // 
            // tb_Upper
            // 
            this.tb_Upper.Location = new System.Drawing.Point(214, 140);
            this.tb_Upper.Name = "tb_Upper";
            this.tb_Upper.Size = new System.Drawing.Size(49, 20);
            this.tb_Upper.TabIndex = 11;
            this.tb_Upper.Text = "100";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(161, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Lower";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(269, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(303, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Upper    (These will override the CAMERA setting if configured)";
            // 
            // Frm_AIServerDeepstackEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 231);
            this.Controls.Add(this.bt_clear);
            this.Controls.Add(this.btTest);
            this.Controls.Add(this.linkHelpURL);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_Save);
            this.Name = "Frm_AIServerDeepstackEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Deepstack AI Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_AIServerDeepstackEdit_FormClosing);
            this.Load += new System.EventHandler(this.Frm_AIServerDeepstackEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_type;
        private System.Windows.Forms.Button bt_Save;
        public System.Windows.Forms.TextBox tb_URL;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox tb_ActiveTimeRange;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox tb_ApplyToCams;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chk_Enabled;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_ImageAdjustEdit;
        public System.Windows.Forms.ComboBox cb_ImageAdjustProfile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_ImagesPerMonth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkHelpURL;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.Button bt_clear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_Upper;
        private System.Windows.Forms.TextBox tb_Lower;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}