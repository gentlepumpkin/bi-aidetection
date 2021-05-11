
namespace AITool
{
    partial class Frm_RelevantObjects
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_RelevantObjects));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FOLV_RelevantObjects = new BrightIdeasSoftware.FastObjectListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cb_ObjectIgnoreDynamicMask = new System.Windows.Forms.CheckBox();
            this.tb_Name = new System.Windows.Forms.TextBox();
            this.cb_ObjectIgnoreImageMask = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_ObjectTriggers = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Time = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_ConfidenceUpper = new System.Windows.Forms.TextBox();
            this.tb_ConfidenceLower = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_enabled = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_maxpercent = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_MinPercent = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxCameras = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDown = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.btn_adddefaults = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_RelevantObjects)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.NavajoWhite;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 52);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FOLV_RelevantObjects);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1028, 840);
            this.splitContainer1.SplitterDistance = 588;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // FOLV_RelevantObjects
            // 
            this.FOLV_RelevantObjects.CheckBoxes = true;
            this.FOLV_RelevantObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FOLV_RelevantObjects.HideSelection = false;
            this.FOLV_RelevantObjects.Location = new System.Drawing.Point(0, 0);
            this.FOLV_RelevantObjects.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FOLV_RelevantObjects.Name = "FOLV_RelevantObjects";
            this.FOLV_RelevantObjects.ShowGroups = false;
            this.FOLV_RelevantObjects.ShowImagesOnSubItems = true;
            this.FOLV_RelevantObjects.Size = new System.Drawing.Size(1024, 584);
            this.FOLV_RelevantObjects.TabIndex = 0;
            this.FOLV_RelevantObjects.UseCompatibleStateImageBehavior = false;
            this.FOLV_RelevantObjects.View = System.Windows.Forms.View.Details;
            this.FOLV_RelevantObjects.VirtualMode = true;
            this.FOLV_RelevantObjects.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.FOLV_RelevantObjects_FormatCell);
            this.FOLV_RelevantObjects.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.FOLV_RelevantObjects_FormatRow);
            this.FOLV_RelevantObjects.SelectionChanged += new System.EventHandler(this.FOLV_RelevantObjects_SelectionChanged);
            this.FOLV_RelevantObjects.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.FOLV_RelevantObjects_ItemChecked);
            this.FOLV_RelevantObjects.SelectedIndexChanged += new System.EventHandler(this.FOLV_RelevantObjects_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cb_enabled);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Location = new System.Drawing.Point(14, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(966, 346);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            this.groupBox1.Leave += new System.EventHandler(this.groupBox1_Leave);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cb_ObjectIgnoreDynamicMask);
            this.groupBox4.Controls.Add(this.tb_Name);
            this.groupBox4.Controls.Add(this.cb_ObjectIgnoreImageMask);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.cb_ObjectTriggers);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.tb_Time);
            this.groupBox4.Location = new System.Drawing.Point(9, 35);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(306, 235);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            // 
            // cb_ObjectIgnoreDynamicMask
            // 
            this.cb_ObjectIgnoreDynamicMask.AutoSize = true;
            this.cb_ObjectIgnoreDynamicMask.Location = new System.Drawing.Point(8, 185);
            this.cb_ObjectIgnoreDynamicMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_ObjectIgnoreDynamicMask.Name = "cb_ObjectIgnoreDynamicMask";
            this.cb_ObjectIgnoreDynamicMask.Size = new System.Drawing.Size(246, 24);
            this.cb_ObjectIgnoreDynamicMask.TabIndex = 0;
            this.cb_ObjectIgnoreDynamicMask.Text = "Object Ignores Dynamic Mask";
            this.toolTip1.SetToolTip(this.cb_ObjectIgnoreDynamicMask, resources.GetString("cb_ObjectIgnoreDynamicMask.ToolTip"));
            this.cb_ObjectIgnoreDynamicMask.UseVisualStyleBackColor = true;
            this.cb_ObjectIgnoreDynamicMask.CheckedChanged += new System.EventHandler(this.cb_IgnoreMask_CheckedChanged);
            // 
            // tb_Name
            // 
            this.tb_Name.Location = new System.Drawing.Point(69, 34);
            this.tb_Name.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_Name.Name = "tb_Name";
            this.tb_Name.Size = new System.Drawing.Size(222, 26);
            this.tb_Name.TabIndex = 1;
            this.tb_Name.TextChanged += new System.EventHandler(this.tb_Name_TextChanged);
            this.tb_Name.Leave += new System.EventHandler(this.tb_Name_Leave);
            // 
            // cb_ObjectIgnoreImageMask
            // 
            this.cb_ObjectIgnoreImageMask.AutoSize = true;
            this.cb_ObjectIgnoreImageMask.Location = new System.Drawing.Point(8, 149);
            this.cb_ObjectIgnoreImageMask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_ObjectIgnoreImageMask.Name = "cb_ObjectIgnoreImageMask";
            this.cb_ObjectIgnoreImageMask.Size = new System.Drawing.Size(230, 24);
            this.cb_ObjectIgnoreImageMask.TabIndex = 0;
            this.cb_ObjectIgnoreImageMask.Text = "Object Ignores Image Mask";
            this.toolTip1.SetToolTip(this.cb_ObjectIgnoreImageMask, resources.GetString("cb_ObjectIgnoreImageMask.ToolTip"));
            this.cb_ObjectIgnoreImageMask.UseVisualStyleBackColor = true;
            this.cb_ObjectIgnoreImageMask.CheckedChanged += new System.EventHandler(this.cb_IgnoreMask_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // cb_ObjectTriggers
            // 
            this.cb_ObjectTriggers.AutoSize = true;
            this.cb_ObjectTriggers.Location = new System.Drawing.Point(8, 114);
            this.cb_ObjectTriggers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_ObjectTriggers.Name = "cb_ObjectTriggers";
            this.cb_ObjectTriggers.Size = new System.Drawing.Size(142, 24);
            this.cb_ObjectTriggers.TabIndex = 0;
            this.cb_ObjectTriggers.Text = "Object Triggers";
            this.toolTip1.SetToolTip(this.cb_ObjectTriggers, "If you uncheck this, the detection of this object will prevent a trigger NO MATTE" +
        "R what other objects are detected.  (For example, the object name could be a per" +
        "sons name used used for face detection)");
            this.cb_ObjectTriggers.UseVisualStyleBackColor = true;
            this.cb_ObjectTriggers.CheckedChanged += new System.EventHandler(this.cb_ObjectTriggers_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Time:";
            // 
            // tb_Time
            // 
            this.tb_Time.Location = new System.Drawing.Point(69, 74);
            this.tb_Time.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_Time.Name = "tb_Time";
            this.tb_Time.Size = new System.Drawing.Size(222, 26);
            this.tb_Time.TabIndex = 1;
            this.tb_Time.TextChanged += new System.EventHandler(this.tb_Time_TextChanged);
            this.tb_Time.Leave += new System.EventHandler(this.tb_Time_Leave);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.tb_ConfidenceUpper);
            this.groupBox3.Controls.Add(this.tb_ConfidenceLower);
            this.groupBox3.Location = new System.Drawing.Point(324, 35);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(315, 108);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Object Confidence limits";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 6.75F);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(14, 66);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(312, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "(Range limited by CAM\\Default objects)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(135, 31);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "Upper:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 31);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Lower:";
            // 
            // tb_ConfidenceUpper
            // 
            this.tb_ConfidenceUpper.Location = new System.Drawing.Point(198, 25);
            this.tb_ConfidenceUpper.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_ConfidenceUpper.Name = "tb_ConfidenceUpper";
            this.tb_ConfidenceUpper.Size = new System.Drawing.Size(48, 26);
            this.tb_ConfidenceUpper.TabIndex = 1;
            this.tb_ConfidenceUpper.Text = "100";
            this.toolTip1.SetToolTip(this.tb_ConfidenceUpper, "MQTT, PUSHOVER, TELEGRAM, DYNAMIC MASK objects cannot be set lower or higher than" +
        " \\DEFAULT objects");
            this.tb_ConfidenceUpper.TextChanged += new System.EventHandler(this.tb_ConfidenceUpper_TextChanged);
            this.tb_ConfidenceUpper.Leave += new System.EventHandler(this.tb_ConfidenceUpper_Leave);
            // 
            // tb_ConfidenceLower
            // 
            this.tb_ConfidenceLower.Location = new System.Drawing.Point(76, 25);
            this.tb_ConfidenceLower.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_ConfidenceLower.Name = "tb_ConfidenceLower";
            this.tb_ConfidenceLower.Size = new System.Drawing.Size(48, 26);
            this.tb_ConfidenceLower.TabIndex = 0;
            this.tb_ConfidenceLower.Text = "100";
            this.toolTip1.SetToolTip(this.tb_ConfidenceLower, "MQTT, PUSHOVER, TELEGRAM, DYNAMIC MASK objects cannot be set lower or higher than" +
        " \\DEFAULT objects");
            this.tb_ConfidenceLower.TextChanged += new System.EventHandler(this.tb_ConfidenceLower_TextChanged);
            this.tb_ConfidenceLower.Leave += new System.EventHandler(this.tb_ConfidenceLower_Leave);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Consolas", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(4, 275);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(634, 58);
            this.label3.TabIndex = 4;
            this.label3.Text = "Example Time Reanges - \"00:01:00-02:59:59, 06:00:00-11:59:59\".  Semicolon Hour li" +
    "st: \"22;23;0;1;2;3;4;5\".  or Dusk-Dawn, Dawn-Dusk, Sunrise-Sunset, Sunset-Sunris" +
    "e";
            // 
            // cb_enabled
            // 
            this.cb_enabled.AutoSize = true;
            this.cb_enabled.Enabled = false;
            this.cb_enabled.Location = new System.Drawing.Point(9, 0);
            this.cb_enabled.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_enabled.Name = "cb_enabled";
            this.cb_enabled.Size = new System.Drawing.Size(94, 24);
            this.cb_enabled.TabIndex = 2;
            this.cb_enabled.Text = "Enabled";
            this.cb_enabled.UseVisualStyleBackColor = true;
            this.cb_enabled.CheckedChanged += new System.EventHandler(this.cb_enabled_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.tb_maxpercent);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.tb_MinPercent);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Location = new System.Drawing.Point(324, 163);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(315, 108);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Limit Size Percentage";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(14, 31);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(280, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Percentage of prediction size vs image";
            // 
            // tb_maxpercent
            // 
            this.tb_maxpercent.Location = new System.Drawing.Point(198, 63);
            this.tb_maxpercent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_maxpercent.Name = "tb_maxpercent";
            this.tb_maxpercent.Size = new System.Drawing.Size(48, 26);
            this.tb_maxpercent.TabIndex = 3;
            this.tb_maxpercent.Text = "100";
            this.tb_maxpercent.TextChanged += new System.EventHandler(this.tb_maxpercent_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(132, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Max %:";
            // 
            // tb_MinPercent
            // 
            this.tb_MinPercent.Location = new System.Drawing.Point(76, 63);
            this.tb_MinPercent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_MinPercent.Name = "tb_MinPercent";
            this.tb_MinPercent.Size = new System.Drawing.Size(48, 26);
            this.tb_MinPercent.TabIndex = 2;
            this.tb_MinPercent.Text = "100";
            this.tb_MinPercent.TextChanged += new System.EventHandler(this.tb_MinPercent_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 68);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Min %:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxCameras,
            this.toolStripSeparator1,
            this.toolStripButtonAdd,
            this.toolStripButtonDelete,
            this.toolStripButtonUp,
            this.toolStripButtonDown});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1028, 38);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripComboBoxCameras
            // 
            this.toolStripComboBoxCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxCameras.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBoxCameras.Name = "toolStripComboBoxCameras";
            this.toolStripComboBoxCameras.Size = new System.Drawing.Size(336, 38);
            this.toolStripComboBoxCameras.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxCameras_SelectedIndexChanged);
            this.toolStripComboBoxCameras.Click += new System.EventHandler(this.toolStripComboBoxCameras_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = global::AITool.Properties.Resources.image_x_generic;
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(74, 33);
            this.toolStripButtonAdd.Text = "Add";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Enabled = false;
            this.toolStripButtonDelete.Image = global::AITool.Properties.Resources.edit_delete_5;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(90, 33);
            this.toolStripButtonDelete.Text = "Delete";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonUp
            // 
            this.toolStripButtonUp.Enabled = false;
            this.toolStripButtonUp.Image = global::AITool.Properties.Resources.arrow_up_double_3;
            this.toolStripButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUp.Name = "toolStripButtonUp";
            this.toolStripButtonUp.Size = new System.Drawing.Size(63, 33);
            this.toolStripButtonUp.Text = "Up";
            this.toolStripButtonUp.Click += new System.EventHandler(this.toolStripButtonUp_Click);
            // 
            // toolStripButtonDown
            // 
            this.toolStripButtonDown.Enabled = false;
            this.toolStripButtonDown.Image = global::AITool.Properties.Resources.arrow_down_double_3;
            this.toolStripButtonDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDown.Name = "toolStripButtonDown";
            this.toolStripButtonDown.Size = new System.Drawing.Size(87, 33);
            this.toolStripButtonDown.Text = "Down";
            this.toolStripButtonDown.Click += new System.EventHandler(this.toolStripButtonDown_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(922, 903);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 46);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(14, 902);
            this.btnReset.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(105, 46);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btn_adddefaults
            // 
            this.btn_adddefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_adddefaults.Location = new System.Drawing.Point(130, 902);
            this.btn_adddefaults.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_adddefaults.Name = "btn_adddefaults";
            this.btn_adddefaults.Size = new System.Drawing.Size(120, 46);
            this.btn_adddefaults.TabIndex = 10;
            this.btn_adddefaults.Text = "Add Defaults";
            this.btn_adddefaults.UseVisualStyleBackColor = true;
            this.btn_adddefaults.Click += new System.EventHandler(this.btn_adddefaults_Click);
            // 
            // Frm_RelevantObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 960);
            this.Controls.Add(this.btn_adddefaults);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Frm_RelevantObjects";
            this.Tag = "SAVE";
            this.Text = "Relevant Objects";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_RelevantObjects_FormClosing);
            this.Load += new System.EventHandler(this.Frm_RelevantObjects_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_RelevantObjects)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private BrightIdeasSoftware.FastObjectListView FOLV_RelevantObjects;
        private System.Windows.Forms.TextBox tb_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_enabled;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox tb_Time;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox tb_ConfidenceUpper;
        public System.Windows.Forms.TextBox tb_ConfidenceLower;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonUp;
        private System.Windows.Forms.ToolStripButton toolStripButtonDown;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox cb_ObjectIgnoreImageMask;
        private System.Windows.Forms.CheckBox cb_ObjectTriggers;
        private System.Windows.Forms.Button btn_adddefaults;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxCameras;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox cb_ObjectIgnoreDynamicMask;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_maxpercent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_MinPercent;
        private System.Windows.Forms.Label label7;
    }
}