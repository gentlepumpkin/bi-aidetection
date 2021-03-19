
namespace AITool
{
    partial class Frm_AnnoAdjust
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_BorderWidthPixels = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_FontName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_FontSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_RelevantAlpha = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_RelevantColor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_IrrelevantAlpha = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_IrrelevantColor = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_MaskedAlpha = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_MaskedColor = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Lbl_Example = new System.Windows.Forms.Label();
            this.cb_UseAssignedBackColor = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tb_FontBackColor = new System.Windows.Forms.TextBox();
            this.tb_FontForeColor = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Border Width:";
            // 
            // tb_BorderWidthPixels
            // 
            this.tb_BorderWidthPixels.Location = new System.Drawing.Point(83, 11);
            this.tb_BorderWidthPixels.Margin = new System.Windows.Forms.Padding(2);
            this.tb_BorderWidthPixels.Name = "tb_BorderWidthPixels";
            this.tb_BorderWidthPixels.Size = new System.Drawing.Size(34, 20);
            this.tb_BorderWidthPixels.TabIndex = 2;
            this.tb_BorderWidthPixels.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Font:";
            // 
            // tb_FontName
            // 
            this.tb_FontName.Location = new System.Drawing.Point(66, 19);
            this.tb_FontName.Margin = new System.Windows.Forms.Padding(2);
            this.tb_FontName.Name = "tb_FontName";
            this.tb_FontName.Size = new System.Drawing.Size(121, 20);
            this.tb_FontName.TabIndex = 2;
            this.tb_FontName.Text = "Segoe UI Semibold";
            this.tb_FontName.Enter += new System.EventHandler(this.tb_FontName_Enter);
            this.tb_FontName.Leave += new System.EventHandler(this.tb_FontName_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Size:";
            // 
            // tb_FontSize
            // 
            this.tb_FontSize.Location = new System.Drawing.Point(258, 18);
            this.tb_FontSize.Margin = new System.Windows.Forms.Padding(2);
            this.tb_FontSize.Name = "tb_FontSize";
            this.tb_FontSize.Size = new System.Drawing.Size(34, 20);
            this.tb_FontSize.TabIndex = 2;
            this.tb_FontSize.Text = "14";
            this.tb_FontSize.Enter += new System.EventHandler(this.tb_FontSize_Enter);
            this.tb_FontSize.Leave += new System.EventHandler(this.tb_FontSize_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "(Pixels)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(294, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "(Points)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tb_RelevantAlpha);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_RelevantColor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(11, 38);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(387, 44);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Relevant Object";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::AITool.Properties.Resources.color_wheel;
            this.button1.Location = new System.Drawing.Point(173, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 20);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(218, 19);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Alpha:";
            // 
            // tb_RelevantAlpha
            // 
            this.tb_RelevantAlpha.Location = new System.Drawing.Point(258, 17);
            this.tb_RelevantAlpha.Margin = new System.Windows.Forms.Padding(2);
            this.tb_RelevantAlpha.Name = "tb_RelevantAlpha";
            this.tb_RelevantAlpha.Size = new System.Drawing.Size(34, 20);
            this.tb_RelevantAlpha.TabIndex = 2;
            this.tb_RelevantAlpha.Text = "255";
            this.tb_RelevantAlpha.Enter += new System.EventHandler(this.tb_RelevantAlpha_Enter);
            this.tb_RelevantAlpha.Leave += new System.EventHandler(this.tb_RelevantAlpha_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 18);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Color:";
            // 
            // tb_RelevantColor
            // 
            this.tb_RelevantColor.Location = new System.Drawing.Point(47, 16);
            this.tb_RelevantColor.Margin = new System.Windows.Forms.Padding(2);
            this.tb_RelevantColor.Name = "tb_RelevantColor";
            this.tb_RelevantColor.Size = new System.Drawing.Size(121, 20);
            this.tb_RelevantColor.TabIndex = 2;
            this.tb_RelevantColor.Text = "DarkSlateGray";
            this.tb_RelevantColor.Enter += new System.EventHandler(this.tb_RelevantColor_Enter);
            this.tb_RelevantColor.Leave += new System.EventHandler(this.tb_RelevantColor_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(294, 19);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "(255 = solid)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tb_IrrelevantAlpha);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tb_IrrelevantColor);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(11, 86);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(387, 44);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Irrelevant Object";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(218, 19);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Alpha:";
            // 
            // tb_IrrelevantAlpha
            // 
            this.tb_IrrelevantAlpha.Location = new System.Drawing.Point(258, 17);
            this.tb_IrrelevantAlpha.Margin = new System.Windows.Forms.Padding(2);
            this.tb_IrrelevantAlpha.Name = "tb_IrrelevantAlpha";
            this.tb_IrrelevantAlpha.Size = new System.Drawing.Size(34, 20);
            this.tb_IrrelevantAlpha.TabIndex = 2;
            this.tb_IrrelevantAlpha.Text = "255";
            this.tb_IrrelevantAlpha.Leave += new System.EventHandler(this.tb_IrrelevantAlpha_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 18);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Color:";
            // 
            // tb_IrrelevantColor
            // 
            this.tb_IrrelevantColor.Location = new System.Drawing.Point(47, 16);
            this.tb_IrrelevantColor.Margin = new System.Windows.Forms.Padding(2);
            this.tb_IrrelevantColor.Name = "tb_IrrelevantColor";
            this.tb_IrrelevantColor.Size = new System.Drawing.Size(121, 20);
            this.tb_IrrelevantColor.TabIndex = 2;
            this.tb_IrrelevantColor.Text = "DarkSlateGray";
            this.tb_IrrelevantColor.Enter += new System.EventHandler(this.tb_IrrelevantColor_Enter);
            this.tb_IrrelevantColor.Leave += new System.EventHandler(this.tb_IrrelevantColor_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(294, 19);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "(255 = solid)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.tb_MaskedAlpha);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.tb_MaskedColor);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Location = new System.Drawing.Point(11, 134);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(387, 44);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Masked Object";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(218, 18);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Alpha:";
            // 
            // tb_MaskedAlpha
            // 
            this.tb_MaskedAlpha.Location = new System.Drawing.Point(258, 16);
            this.tb_MaskedAlpha.Margin = new System.Windows.Forms.Padding(2);
            this.tb_MaskedAlpha.Name = "tb_MaskedAlpha";
            this.tb_MaskedAlpha.Size = new System.Drawing.Size(34, 20);
            this.tb_MaskedAlpha.TabIndex = 2;
            this.tb_MaskedAlpha.Text = "255";
            this.tb_MaskedAlpha.TextChanged += new System.EventHandler(this.tb_MaskedAlpha_TextChanged);
            this.tb_MaskedAlpha.Leave += new System.EventHandler(this.tb_MaskedAlpha_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 18);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Color:";
            // 
            // tb_MaskedColor
            // 
            this.tb_MaskedColor.Location = new System.Drawing.Point(47, 16);
            this.tb_MaskedColor.Margin = new System.Windows.Forms.Padding(2);
            this.tb_MaskedColor.Name = "tb_MaskedColor";
            this.tb_MaskedColor.Size = new System.Drawing.Size(121, 20);
            this.tb_MaskedColor.TabIndex = 2;
            this.tb_MaskedColor.Text = "DarkSlateGray";
            this.tb_MaskedColor.Enter += new System.EventHandler(this.tb_MaskedColor_Enter);
            this.tb_MaskedColor.Leave += new System.EventHandler(this.tb_MaskedColor_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(294, 18);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "(255 = solid)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.Lbl_Example);
            this.groupBox4.Controls.Add(this.cb_UseAssignedBackColor);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.tb_FontBackColor);
            this.groupBox4.Controls.Add(this.tb_FontForeColor);
            this.groupBox4.Controls.Add(this.tb_FontName);
            this.groupBox4.Controls.Add(this.tb_FontSize);
            this.groupBox4.Location = new System.Drawing.Point(11, 201);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(389, 144);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Text Label";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // Lbl_Example
            // 
            this.Lbl_Example.AutoSize = true;
            this.Lbl_Example.Location = new System.Drawing.Point(7, 99);
            this.Lbl_Example.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lbl_Example.Name = "Lbl_Example";
            this.Lbl_Example.Size = new System.Drawing.Size(99, 13);
            this.Lbl_Example.TabIndex = 4;
            this.Lbl_Example.Text = "This is example text";
            this.Lbl_Example.Click += new System.EventHandler(this.Lbl_Example_Click);
            // 
            // cb_UseAssignedBackColor
            // 
            this.cb_UseAssignedBackColor.AutoSize = true;
            this.cb_UseAssignedBackColor.Location = new System.Drawing.Point(230, 69);
            this.cb_UseAssignedBackColor.Margin = new System.Windows.Forms.Padding(2);
            this.cb_UseAssignedBackColor.Name = "cb_UseAssignedBackColor";
            this.cb_UseAssignedBackColor.Size = new System.Drawing.Size(152, 17);
            this.cb_UseAssignedBackColor.TabIndex = 3;
            this.cb_UseAssignedBackColor.Text = "Use Assigned Object Color";
            this.cb_UseAssignedBackColor.UseVisualStyleBackColor = true;
            this.cb_UseAssignedBackColor.CheckedChanged += new System.EventHandler(this.cb_UseAssignedBackColor_CheckedChanged);
            this.cb_UseAssignedBackColor.Enter += new System.EventHandler(this.cb_UseAssignedBackColor_Enter);
            this.cb_UseAssignedBackColor.Leave += new System.EventHandler(this.cb_UseAssignedBackColor_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(2, 71);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Back Color:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 47);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Fore Color:";
            // 
            // tb_FontBackColor
            // 
            this.tb_FontBackColor.Location = new System.Drawing.Point(66, 67);
            this.tb_FontBackColor.Margin = new System.Windows.Forms.Padding(2);
            this.tb_FontBackColor.Name = "tb_FontBackColor";
            this.tb_FontBackColor.Size = new System.Drawing.Size(121, 20);
            this.tb_FontBackColor.TabIndex = 2;
            this.tb_FontBackColor.Text = "DarkSlateGray";
            this.tb_FontBackColor.Enter += new System.EventHandler(this.tb_FontBackColor_Enter);
            this.tb_FontBackColor.Leave += new System.EventHandler(this.tb_FontBackColor_Leave);
            // 
            // tb_FontForeColor
            // 
            this.tb_FontForeColor.Location = new System.Drawing.Point(66, 43);
            this.tb_FontForeColor.Margin = new System.Windows.Forms.Padding(2);
            this.tb_FontForeColor.Name = "tb_FontForeColor";
            this.tb_FontForeColor.Size = new System.Drawing.Size(121, 20);
            this.tb_FontForeColor.TabIndex = 2;
            this.tb_FontForeColor.Text = "DarkSlateGray";
            this.tb_FontForeColor.Enter += new System.EventHandler(this.tb_FontForeColor_Enter);
            this.tb_FontForeColor.Leave += new System.EventHandler(this.tb_FontForeColor_Leave);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(8, 183);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(190, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Click here for a list of valid color names";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(330, 352);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 30);
            this.btnCancel.TabIndex = 39;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.Location = new System.Drawing.Point(252, 352);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 30);
            this.btnSave.TabIndex = 38;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::AITool.Properties.Resources.color_wheel;
            this.button2.Location = new System.Drawing.Point(173, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(22, 20);
            this.button2.TabIndex = 5;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = global::AITool.Properties.Resources.color_wheel;
            this.button3.Location = new System.Drawing.Point(173, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(22, 20);
            this.button3.TabIndex = 5;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = global::AITool.Properties.Resources.color_wheel;
            this.button4.Location = new System.Drawing.Point(197, 67);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(22, 20);
            this.button4.TabIndex = 5;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = global::AITool.Properties.Resources.color_wheel;
            this.button5.Location = new System.Drawing.Point(197, 43);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(22, 20);
            this.button5.TabIndex = 5;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Frm_AnnoAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 392);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_BorderWidthPixels);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Frm_AnnoAdjust";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adjust Annotation Appearance ";
            this.Load += new System.EventHandler(this.Frm_AnnoAdjust_Load);
            this.Click += new System.EventHandler(this.Frm_AnnoAdjust_Click);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.LinkLabel linkLabel1;
        public System.Windows.Forms.TextBox tb_BorderWidthPixels;
        public System.Windows.Forms.TextBox tb_FontName;
        public System.Windows.Forms.TextBox tb_FontSize;
        public System.Windows.Forms.TextBox tb_RelevantColor;
        public System.Windows.Forms.TextBox tb_RelevantAlpha;
        public System.Windows.Forms.TextBox tb_IrrelevantAlpha;
        public System.Windows.Forms.TextBox tb_IrrelevantColor;
        public System.Windows.Forms.TextBox tb_MaskedAlpha;
        public System.Windows.Forms.TextBox tb_MaskedColor;
        public System.Windows.Forms.CheckBox cb_UseAssignedBackColor;
        public System.Windows.Forms.TextBox tb_FontBackColor;
        public System.Windows.Forms.TextBox tb_FontForeColor;
        public System.Windows.Forms.Label Lbl_Example;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
    }
}