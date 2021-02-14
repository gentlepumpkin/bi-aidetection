
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
            this.cb_TimeoutError = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBoxLinked = new System.Windows.Forms.GroupBox();
            this.cb_LinkedServers = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBoxRefine = new System.Windows.Forms.GroupBox();
            this.cb_RefinementServer = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_RefinementObjects = new System.Windows.Forms.TextBox();
            this.tb_Upper = new System.Windows.Forms.TextBox();
            this.tb_LinkedRefineTimeout = new System.Windows.Forms.TextBox();
            this.tb_Lower = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_timeout = new System.Windows.Forms.TextBox();
            this.tb_ImagesPerMonth = new System.Windows.Forms.TextBox();
            this.btn_ImageAdjustEdit = new System.Windows.Forms.Button();
            this.cb_ImageAdjustProfile = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelTimeout = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.linkHelpURL = new System.Windows.Forms.LinkLabel();
            this.btTest = new System.Windows.Forms.Button();
            this.bt_clear = new System.Windows.Forms.Button();
            this.cb_OnlyLinked = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.checkedComboBoxLinked = new CheckComboBoxTest.CheckedComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBoxLinked.SuspendLayout();
            this.groupBoxRefine.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "URL:";
            // 
            // lbl_type
            // 
            this.lbl_type.AutoSize = true;
            this.lbl_type.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbl_type.Location = new System.Drawing.Point(162, 25);
            this.lbl_type.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_type.Name = "lbl_type";
            this.lbl_type.Size = new System.Drawing.Size(13, 20);
            this.lbl_type.TabIndex = 0;
            this.lbl_type.Text = ".";
            // 
            // tb_URL
            // 
            this.tb_URL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_URL.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_URL.Location = new System.Drawing.Point(166, 54);
            this.tb_URL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_URL.Name = "tb_URL";
            this.tb_URL.Size = new System.Drawing.Size(690, 27);
            this.tb_URL.TabIndex = 2;
            this.tb_URL.TextChanged += new System.EventHandler(this.tb_URL_TextChanged);
            // 
            // bt_Save
            // 
            this.bt_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bt_Save.Location = new System.Drawing.Point(770, 578);
            this.bt_Save.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(105, 46);
            this.bt_Save.TabIndex = 3;
            this.bt_Save.Text = "Save";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 140);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Active Time Range:";
            // 
            // tb_ActiveTimeRange
            // 
            this.tb_ActiveTimeRange.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ActiveTimeRange.Location = new System.Drawing.Point(166, 134);
            this.tb_ActiveTimeRange.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_ActiveTimeRange.Name = "tb_ActiveTimeRange";
            this.tb_ActiveTimeRange.Size = new System.Drawing.Size(226, 27);
            this.tb_ActiveTimeRange.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tb_ActiveTimeRange, "Active time range in form of 00:00:00-23:59:59");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 100);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Apply to Cameras:";
            // 
            // tb_ApplyToCams
            // 
            this.tb_ApplyToCams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_ApplyToCams.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ApplyToCams.Location = new System.Drawing.Point(166, 94);
            this.tb_ApplyToCams.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_ApplyToCams.Name = "tb_ApplyToCams";
            this.tb_ApplyToCams.Size = new System.Drawing.Size(688, 27);
            this.tb_ApplyToCams.TabIndex = 5;
            this.toolTip1.SetToolTip(this.tb_ApplyToCams, "A comma separated list of cameras that this AI server will work with.\r\n\r\nLeave em" +
        "pty for ALL.");
            // 
            // chk_Enabled
            // 
            this.chk_Enabled.AutoSize = true;
            this.chk_Enabled.ForeColor = System.Drawing.Color.DodgerBlue;
            this.chk_Enabled.Location = new System.Drawing.Point(16, 0);
            this.chk_Enabled.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chk_Enabled.Name = "chk_Enabled";
            this.chk_Enabled.Size = new System.Drawing.Size(94, 24);
            this.chk_Enabled.TabIndex = 6;
            this.chk_Enabled.Text = "Enabled";
            this.chk_Enabled.UseVisualStyleBackColor = true;
            this.chk_Enabled.CheckedChanged += new System.EventHandler(this.chk_Enabled_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cb_OnlyLinked);
            this.groupBox1.Controls.Add(this.cb_TimeoutError);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.groupBoxLinked);
            this.groupBox1.Controls.Add(this.groupBoxRefine);
            this.groupBox1.Controls.Add(this.tb_Upper);
            this.groupBox1.Controls.Add(this.tb_LinkedRefineTimeout);
            this.groupBox1.Controls.Add(this.tb_Lower);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_timeout);
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
            this.groupBox1.Controls.Add(this.labelTimeout);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(8, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(867, 559);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // cb_TimeoutError
            // 
            this.cb_TimeoutError.AutoSize = true;
            this.cb_TimeoutError.ForeColor = System.Drawing.Color.Firebrick;
            this.cb_TimeoutError.Location = new System.Drawing.Point(781, 510);
            this.cb_TimeoutError.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_TimeoutError.Name = "cb_TimeoutError";
            this.cb_TimeoutError.Size = new System.Drawing.Size(70, 24);
            this.cb_TimeoutError.TabIndex = 19;
            this.cb_TimeoutError.Text = "Error";
            this.toolTip1.SetToolTip(this.cb_TimeoutError, "An error will show in the log if a timeout happens");
            this.cb_TimeoutError.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(727, 511);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 20);
            this.label12.TabIndex = 18;
            this.label12.Text = "ms";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Firebrick;
            this.label10.Location = new System.Drawing.Point(12, 511);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(616, 20);
            this.label10.TabIndex = 18;
            this.label10.Text = "Maximum time to wait for a LINKED or REFINEMENT server URL to become available:";
            // 
            // groupBoxLinked
            // 
            this.groupBoxLinked.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLinked.Controls.Add(this.cb_LinkedServers);
            this.groupBoxLinked.Controls.Add(this.label13);
            this.groupBoxLinked.Controls.Add(this.checkedComboBoxLinked);
            this.groupBoxLinked.Location = new System.Drawing.Point(12, 404);
            this.groupBoxLinked.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxLinked.Name = "groupBoxLinked";
            this.groupBoxLinked.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxLinked.Size = new System.Drawing.Size(842, 97);
            this.groupBoxLinked.TabIndex = 17;
            this.groupBoxLinked.TabStop = false;
            // 
            // cb_LinkedServers
            // 
            this.cb_LinkedServers.AutoSize = true;
            this.cb_LinkedServers.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_LinkedServers.Location = new System.Drawing.Point(12, 0);
            this.cb_LinkedServers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_LinkedServers.Name = "cb_LinkedServers";
            this.cb_LinkedServers.Size = new System.Drawing.Size(325, 24);
            this.cb_LinkedServers.TabIndex = 12;
            this.cb_LinkedServers.Text = "Link / Combine Results with other servers";
            this.cb_LinkedServers.UseVisualStyleBackColor = true;
            this.cb_LinkedServers.CheckedChanged += new System.EventHandler(this.cb_LinkedServers_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 31);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(825, 20);
            this.label13.TabIndex = 13;
            this.label13.Text = "Wait for results from all linked servers - Useful to combine output from normal D" +
    "eepstack and custom trained models";
            // 
            // groupBoxRefine
            // 
            this.groupBoxRefine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRefine.Controls.Add(this.cb_RefinementServer);
            this.groupBoxRefine.Controls.Add(this.label11);
            this.groupBoxRefine.Controls.Add(this.tb_RefinementObjects);
            this.groupBoxRefine.Location = new System.Drawing.Point(12, 263);
            this.groupBoxRefine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxRefine.Name = "groupBoxRefine";
            this.groupBoxRefine.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxRefine.Size = new System.Drawing.Size(842, 97);
            this.groupBoxRefine.TabIndex = 16;
            this.groupBoxRefine.TabStop = false;
            // 
            // cb_RefinementServer
            // 
            this.cb_RefinementServer.AutoSize = true;
            this.cb_RefinementServer.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_RefinementServer.Location = new System.Drawing.Point(12, 0);
            this.cb_RefinementServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_RefinementServer.Name = "cb_RefinementServer";
            this.cb_RefinementServer.Size = new System.Drawing.Size(222, 24);
            this.cb_RefinementServer.TabIndex = 12;
            this.cb_RefinementServer.Text = "Use as Refinement Server";
            this.cb_RefinementServer.UseVisualStyleBackColor = true;
            this.cb_RefinementServer.CheckedChanged += new System.EventHandler(this.cb_RefinementServer_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 31);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(517, 20);
            this.label11.TabIndex = 13;
            this.label11.Text = "Use this server ONLY if another server detects the following objects first:";
            // 
            // tb_RefinementObjects
            // 
            this.tb_RefinementObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_RefinementObjects.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_RefinementObjects.Location = new System.Drawing.Point(12, 55);
            this.tb_RefinementObjects.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_RefinementObjects.Name = "tb_RefinementObjects";
            this.tb_RefinementObjects.Size = new System.Drawing.Size(818, 27);
            this.tb_RefinementObjects.TabIndex = 14;
            // 
            // tb_Upper
            // 
            this.tb_Upper.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Upper.Location = new System.Drawing.Point(321, 215);
            this.tb_Upper.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_Upper.Name = "tb_Upper";
            this.tb_Upper.Size = new System.Drawing.Size(72, 27);
            this.tb_Upper.TabIndex = 11;
            this.tb_Upper.Text = "100";
            // 
            // tb_LinkedRefineTimeout
            // 
            this.tb_LinkedRefineTimeout.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_LinkedRefineTimeout.Location = new System.Drawing.Point(645, 506);
            this.tb_LinkedRefineTimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_LinkedRefineTimeout.Name = "tb_LinkedRefineTimeout";
            this.tb_LinkedRefineTimeout.Size = new System.Drawing.Size(72, 27);
            this.tb_LinkedRefineTimeout.TabIndex = 11;
            this.tb_LinkedRefineTimeout.Tag = "";
            this.tb_LinkedRefineTimeout.Text = "5000";
            // 
            // tb_Lower
            // 
            this.tb_Lower.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Lower.Location = new System.Drawing.Point(166, 215);
            this.tb_Lower.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_Lower.Name = "tb_Lower";
            this.tb_Lower.Size = new System.Drawing.Size(72, 27);
            this.tb_Lower.TabIndex = 11;
            this.tb_Lower.Tag = "";
            this.tb_Lower.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 220);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Confidence limits:";
            // 
            // tb_timeout
            // 
            this.tb_timeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_timeout.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_timeout.Location = new System.Drawing.Point(788, 172);
            this.tb_timeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_timeout.Name = "tb_timeout";
            this.tb_timeout.Size = new System.Drawing.Size(67, 27);
            this.tb_timeout.TabIndex = 9;
            this.tb_timeout.Text = "0";
            this.toolTip1.SetToolTip(this.tb_timeout, "If you set this to any value other than 0 it will override the default timeout");
            // 
            // tb_ImagesPerMonth
            // 
            this.tb_ImagesPerMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_ImagesPerMonth.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ImagesPerMonth.Location = new System.Drawing.Point(788, 134);
            this.tb_ImagesPerMonth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_ImagesPerMonth.Name = "tb_ImagesPerMonth";
            this.tb_ImagesPerMonth.Size = new System.Drawing.Size(67, 27);
            this.tb_ImagesPerMonth.TabIndex = 9;
            this.tb_ImagesPerMonth.Text = "0";
            this.toolTip1.SetToolTip(this.tb_ImagesPerMonth, "Max images per month - 0 for unlimited.    Amazon has 5000 free images a month");
            // 
            // btn_ImageAdjustEdit
            // 
            this.btn_ImageAdjustEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ImageAdjustEdit.Location = new System.Drawing.Point(404, 174);
            this.btn_ImageAdjustEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ImageAdjustEdit.Name = "btn_ImageAdjustEdit";
            this.btn_ImageAdjustEdit.Size = new System.Drawing.Size(46, 32);
            this.btn_ImageAdjustEdit.TabIndex = 8;
            this.btn_ImageAdjustEdit.Text = "···";
            this.toolTip1.SetToolTip(this.btn_ImageAdjustEdit, "Edit the image adjust profile");
            this.btn_ImageAdjustEdit.UseVisualStyleBackColor = true;
            this.btn_ImageAdjustEdit.Click += new System.EventHandler(this.btn_ImageAdjustEdit_Click);
            // 
            // cb_ImageAdjustProfile
            // 
            this.cb_ImageAdjustProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ImageAdjustProfile.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_ImageAdjustProfile.FormattingEnabled = true;
            this.cb_ImageAdjustProfile.Location = new System.Drawing.Point(166, 174);
            this.cb_ImageAdjustProfile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_ImageAdjustProfile.Name = "cb_ImageAdjustProfile";
            this.cb_ImageAdjustProfile.Size = new System.Drawing.Size(226, 28);
            this.cb_ImageAdjustProfile.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(404, 220);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(450, 20);
            this.label9.TabIndex = 1;
            this.label9.Text = "Upper    (These will override the CAMERA setting if configured)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(242, 220);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "Lower";
            // 
            // labelTimeout
            // 
            this.labelTimeout.Location = new System.Drawing.Point(480, 180);
            this.labelTimeout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTimeout.Name = "labelTimeout";
            this.labelTimeout.Size = new System.Drawing.Size(298, 20);
            this.labelTimeout.TabIndex = 1;
            this.labelTimeout.Text = "Timeout:";
            this.labelTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(600, 140);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(176, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "Max Images Per Month:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 180);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "Image Adjust Profile:";
            // 
            // linkHelpURL
            // 
            this.linkHelpURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkHelpURL.AutoSize = true;
            this.linkHelpURL.Location = new System.Drawing.Point(3, 550);
            this.linkHelpURL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkHelpURL.Name = "linkHelpURL";
            this.linkHelpURL.Size = new System.Drawing.Size(13, 20);
            this.linkHelpURL.TabIndex = 8;
            this.linkHelpURL.TabStop = true;
            this.linkHelpURL.Text = ".";
            this.linkHelpURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHelpURL_LinkClicked);
            // 
            // btTest
            // 
            this.btTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btTest.Location = new System.Drawing.Point(656, 578);
            this.btTest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(105, 46);
            this.btTest.TabIndex = 9;
            this.btTest.Text = "Test";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // bt_clear
            // 
            this.bt_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_clear.Location = new System.Drawing.Point(542, 578);
            this.bt_clear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new System.Drawing.Size(105, 46);
            this.bt_clear.TabIndex = 9;
            this.bt_clear.Text = "Clear Stats";
            this.bt_clear.UseVisualStyleBackColor = true;
            this.bt_clear.Click += new System.EventHandler(this.bt_clear_Click);
            // 
            // cb_OnlyLinked
            // 
            this.cb_OnlyLinked.AutoSize = true;
            this.cb_OnlyLinked.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cb_OnlyLinked.Location = new System.Drawing.Point(24, 370);
            this.cb_OnlyLinked.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_OnlyLinked.Name = "cb_OnlyLinked";
            this.cb_OnlyLinked.Size = new System.Drawing.Size(224, 24);
            this.cb_OnlyLinked.TabIndex = 12;
            this.cb_OnlyLinked.Text = "Use ONLY as linked server";
            this.cb_OnlyLinked.UseVisualStyleBackColor = true;
            this.cb_OnlyLinked.CheckedChanged += new System.EventHandler(this.cb_OnlyLinked_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label14.Location = new System.Drawing.Point(246, 371);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(550, 20);
            this.label14.TabIndex = 20;
            this.label14.Text = "(This should be checked if ANOTHER server uses this one as a linked server)";
            // 
            // checkedComboBoxLinked
            // 
            this.checkedComboBoxLinked.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedComboBoxLinked.CheckOnClick = true;
            this.checkedComboBoxLinked.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.checkedComboBoxLinked.DropDownHeight = 1;
            this.checkedComboBoxLinked.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.checkedComboBoxLinked.FormattingEnabled = true;
            this.checkedComboBoxLinked.IntegralHeight = false;
            this.checkedComboBoxLinked.Location = new System.Drawing.Point(9, 55);
            this.checkedComboBoxLinked.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkedComboBoxLinked.Name = "checkedComboBoxLinked";
            this.checkedComboBoxLinked.Size = new System.Drawing.Size(822, 28);
            this.checkedComboBoxLinked.TabIndex = 15;
            this.checkedComboBoxLinked.Text = "Click dropdown to select";
            this.checkedComboBoxLinked.ValueSeparator = ", ";
            // 
            // Frm_AIServerDeepstackEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 636);
            this.Controls.Add(this.bt_clear);
            this.Controls.Add(this.btTest);
            this.Controls.Add(this.linkHelpURL);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_Save);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Frm_AIServerDeepstackEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit AI Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_AIServerDeepstackEdit_FormClosing);
            this.Load += new System.EventHandler(this.Frm_AIServerDeepstackEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxLinked.ResumeLayout(false);
            this.groupBoxLinked.PerformLayout();
            this.groupBoxRefine.ResumeLayout(false);
            this.groupBoxRefine.PerformLayout();
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
        private System.Windows.Forms.TextBox tb_RefinementObjects;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cb_RefinementServer;
        private System.Windows.Forms.TextBox tb_timeout;
        private System.Windows.Forms.Label labelTimeout;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox cb_LinkedServers;
        private CheckComboBoxTest.CheckedComboBox checkedComboBoxLinked;
        private System.Windows.Forms.GroupBox groupBoxLinked;
        private System.Windows.Forms.GroupBox groupBoxRefine;
        private System.Windows.Forms.CheckBox cb_TimeoutError;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_LinkedRefineTimeout;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cb_OnlyLinked;
    }
}