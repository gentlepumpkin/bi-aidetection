
namespace AITool
{
    partial class Frm_ImageAdjust
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
            this.tb_ImageFile = new System.Windows.Forms.TextBox();
            this.tbar_Jpegquality = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_jpegquality = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbar_ResizePercent = new System.Windows.Forms.TrackBar();
            this.tb_Width = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.bt_save = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bt_Delete = new System.Windows.Forms.Button();
            this.tb_SizePercent = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_originalHeight = new System.Windows.Forms.Label();
            this.lbl_originalwidth = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbar_contrast = new System.Windows.Forms.TrackBar();
            this.tbar_brightness = new System.Windows.Forms.TrackBar();
            this.tb_contrast = new System.Windows.Forms.TextBox();
            this.tb_brightness = new System.Windows.Forms.TextBox();
            this.tb_Height = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_Jpegquality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_ResizePercent)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_contrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_brightness)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Test Image:";
            // 
            // tb_ImageFile
            // 
            this.tb_ImageFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_ImageFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_ImageFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tb_ImageFile.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ImageFile.Location = new System.Drawing.Point(133, 15);
            this.tb_ImageFile.Name = "tb_ImageFile";
            this.tb_ImageFile.Size = new System.Drawing.Size(871, 20);
            this.tb_ImageFile.TabIndex = 2;
            // 
            // tbar_Jpegquality
            // 
            this.tbar_Jpegquality.Location = new System.Drawing.Point(133, 41);
            this.tbar_Jpegquality.Maximum = 100;
            this.tbar_Jpegquality.Minimum = 5;
            this.tbar_Jpegquality.Name = "tbar_Jpegquality";
            this.tbar_Jpegquality.Size = new System.Drawing.Size(168, 45);
            this.tbar_Jpegquality.TabIndex = 3;
            this.tbar_Jpegquality.TickFrequency = 5;
            this.tbar_Jpegquality.Value = 100;
            this.tbar_Jpegquality.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "JPEG Quality Percent:";
            // 
            // tb_jpegquality
            // 
            this.tb_jpegquality.Location = new System.Drawing.Point(307, 44);
            this.tb_jpegquality.Name = "tb_jpegquality";
            this.tb_jpegquality.Size = new System.Drawing.Size(51, 22);
            this.tb_jpegquality.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1020, 324);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(364, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Resize Width:";
            // 
            // tbar_ResizePercent
            // 
            this.tbar_ResizePercent.Location = new System.Drawing.Point(133, 84);
            this.tbar_ResizePercent.Maximum = 100;
            this.tbar_ResizePercent.Minimum = 5;
            this.tbar_ResizePercent.Name = "tbar_ResizePercent";
            this.tbar_ResizePercent.Size = new System.Drawing.Size(168, 45);
            this.tbar_ResizePercent.TabIndex = 3;
            this.tbar_ResizePercent.TickFrequency = 5;
            this.tbar_ResizePercent.Value = 100;
            this.tbar_ResizePercent.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // tb_Width
            // 
            this.tb_Width.Location = new System.Drawing.Point(443, 84);
            this.tb_Width.Name = "tb_Width";
            this.tb_Width.Size = new System.Drawing.Size(51, 22);
            this.tb_Width.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(1042, 77);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(51, 22);
            this.textBox1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(500, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Resize Height:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(349, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "File Size:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(404, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = ".";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Profile:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(57, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(150, 21);
            this.comboBox1.TabIndex = 9;
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(220, 10);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(54, 24);
            this.bt_save.TabIndex = 10;
            this.bt_save.Text = "Save";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // bt_Delete
            // 
            this.bt_Delete.Location = new System.Drawing.Point(280, 10);
            this.bt_Delete.Name = "bt_Delete";
            this.bt_Delete.Size = new System.Drawing.Size(54, 24);
            this.bt_Delete.TabIndex = 10;
            this.bt_Delete.Text = "Delete";
            this.bt_Delete.UseVisualStyleBackColor = true;
            this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
            // 
            // tb_SizePercent
            // 
            this.tb_SizePercent.Location = new System.Drawing.Point(307, 84);
            this.tb_SizePercent.Name = "tb_SizePercent";
            this.tb_SizePercent.Size = new System.Drawing.Size(51, 22);
            this.tb_SizePercent.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(45, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Resize Percent:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(714, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Brightness:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lbl_originalHeight);
            this.groupBox1.Controls.Add(this.lbl_originalwidth);
            this.groupBox1.Controls.Add(this.tb_ImageFile);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbar_Jpegquality);
            this.groupBox1.Controls.Add(this.tbar_contrast);
            this.groupBox1.Controls.Add(this.tbar_brightness);
            this.groupBox1.Controls.Add(this.tbar_ResizePercent);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_jpegquality);
            this.groupBox1.Controls.Add(this.tb_contrast);
            this.groupBox1.Controls.Add(this.tb_brightness);
            this.groupBox1.Controls.Add(this.tb_SizePercent);
            this.groupBox1.Controls.Add(this.tb_Height);
            this.groupBox1.Controls.Add(this.tb_Width);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 366);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1020, 138);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // lbl_originalHeight
            // 
            this.lbl_originalHeight.AutoSize = true;
            this.lbl_originalHeight.Location = new System.Drawing.Point(579, 68);
            this.lbl_originalHeight.Name = "lbl_originalHeight";
            this.lbl_originalHeight.Size = new System.Drawing.Size(13, 13);
            this.lbl_originalHeight.TabIndex = 13;
            this.lbl_originalHeight.Text = "0";
            // 
            // lbl_originalwidth
            // 
            this.lbl_originalwidth.AutoSize = true;
            this.lbl_originalwidth.Location = new System.Drawing.Point(440, 68);
            this.lbl_originalwidth.Name = "lbl_originalwidth";
            this.lbl_originalwidth.Size = new System.Drawing.Size(13, 13);
            this.lbl_originalwidth.TabIndex = 13;
            this.lbl_originalwidth.Text = "0";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(724, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Contrast:";
            // 
            // tbar_contrast
            // 
            this.tbar_contrast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbar_contrast.Location = new System.Drawing.Point(779, 84);
            this.tbar_contrast.Maximum = 100;
            this.tbar_contrast.Minimum = 1;
            this.tbar_contrast.Name = "tbar_contrast";
            this.tbar_contrast.Size = new System.Drawing.Size(168, 45);
            this.tbar_contrast.TabIndex = 3;
            this.tbar_contrast.TickFrequency = 5;
            this.tbar_contrast.Value = 100;
            this.tbar_contrast.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.tbar_contrast.ValueChanged += new System.EventHandler(this.tbar_contrast_ValueChanged);
            // 
            // tbar_brightness
            // 
            this.tbar_brightness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbar_brightness.Location = new System.Drawing.Point(779, 38);
            this.tbar_brightness.Maximum = 100;
            this.tbar_brightness.Minimum = 1;
            this.tbar_brightness.Name = "tbar_brightness";
            this.tbar_brightness.Size = new System.Drawing.Size(168, 45);
            this.tbar_brightness.TabIndex = 3;
            this.tbar_brightness.TickFrequency = 5;
            this.tbar_brightness.Value = 100;
            this.tbar_brightness.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.tbar_brightness.ValueChanged += new System.EventHandler(this.tbar_brightness_ValueChanged);
            // 
            // tb_contrast
            // 
            this.tb_contrast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_contrast.Location = new System.Drawing.Point(953, 84);
            this.tb_contrast.Name = "tb_contrast";
            this.tb_contrast.Size = new System.Drawing.Size(51, 22);
            this.tb_contrast.TabIndex = 5;
            // 
            // tb_brightness
            // 
            this.tb_brightness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_brightness.Location = new System.Drawing.Point(953, 41);
            this.tb_brightness.Name = "tb_brightness";
            this.tb_brightness.Size = new System.Drawing.Size(51, 22);
            this.tb_brightness.TabIndex = 5;
            // 
            // tb_Height
            // 
            this.tb_Height.Location = new System.Drawing.Point(582, 84);
            this.tb_Height.Name = "tb_Height";
            this.tb_Height.Size = new System.Drawing.Size(51, 22);
            this.tb_Height.TabIndex = 5;
            // 
            // Frm_ImageAdjust
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1044, 514);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_Delete);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "Frm_ImageAdjust";
            this.Text = "Image Adjust";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_ImageAdjust_FormClosing);
            this.Load += new System.EventHandler(this.Frm_ImageAdjust_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbar_Jpegquality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_ResizePercent)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_contrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_brightness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_ImageFile;
        private System.Windows.Forms.TrackBar tbar_Jpegquality;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_jpegquality;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbar_ResizePercent;
        private System.Windows.Forms.TextBox tb_Width;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button bt_Delete;
        private System.Windows.Forms.TextBox tb_SizePercent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_originalHeight;
        private System.Windows.Forms.Label lbl_originalwidth;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar tbar_contrast;
        private System.Windows.Forms.TrackBar tbar_brightness;
        private System.Windows.Forms.TextBox tb_contrast;
        private System.Windows.Forms.TextBox tb_brightness;
        private System.Windows.Forms.TextBox tb_Height;
    }
}