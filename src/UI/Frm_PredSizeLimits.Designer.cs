
namespace AITool
{
    partial class Frm_PredSizeLimits
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_maxpercent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_MinPercent = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_maxheight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_maxwidth = new System.Windows.Forms.TextBox();
            this.tb_minheight = new System.Windows.Forms.TextBox();
            this.tb_minwidth = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_ConfidenceUpper = new System.Windows.Forms.TextBox();
            this.tb_ConfidenceLower = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tb_duplicatepercent = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Min %:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_maxpercent);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_MinPercent);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 70);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Limit Size Percentage";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(9, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Percentage of prediction size vs image";
            // 
            // tb_maxpercent
            // 
            this.tb_maxpercent.Location = new System.Drawing.Point(199, 41);
            this.tb_maxpercent.Name = "tb_maxpercent";
            this.tb_maxpercent.Size = new System.Drawing.Size(33, 20);
            this.tb_maxpercent.TabIndex = 3;
            this.tb_maxpercent.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Max %:";
            // 
            // tb_MinPercent
            // 
            this.tb_MinPercent.Location = new System.Drawing.Point(86, 41);
            this.tb_MinPercent.Name = "tb_MinPercent";
            this.tb_MinPercent.Size = new System.Drawing.Size(33, 20);
            this.tb_MinPercent.TabIndex = 2;
            this.tb_MinPercent.Text = "100";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tb_maxheight);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tb_maxwidth);
            this.groupBox2.Controls.Add(this.tb_minheight);
            this.groupBox2.Controls.Add(this.tb_minwidth);
            this.groupBox2.Location = new System.Drawing.Point(12, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 77);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Limit Size in Pixels (0 disables)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(134, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Max Height:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Min Height:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Max Width:";
            // 
            // tb_maxheight
            // 
            this.tb_maxheight.Location = new System.Drawing.Point(201, 47);
            this.tb_maxheight.Name = "tb_maxheight";
            this.tb_maxheight.Size = new System.Drawing.Size(33, 20);
            this.tb_maxheight.TabIndex = 7;
            this.tb_maxheight.Text = "100";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Min Width:";
            // 
            // tb_maxwidth
            // 
            this.tb_maxwidth.Location = new System.Drawing.Point(201, 23);
            this.tb_maxwidth.Name = "tb_maxwidth";
            this.tb_maxwidth.Size = new System.Drawing.Size(33, 20);
            this.tb_maxwidth.TabIndex = 5;
            this.tb_maxwidth.Text = "100";
            // 
            // tb_minheight
            // 
            this.tb_minheight.Location = new System.Drawing.Point(86, 47);
            this.tb_minheight.Name = "tb_minheight";
            this.tb_minheight.Size = new System.Drawing.Size(33, 20);
            this.tb_minheight.TabIndex = 6;
            this.tb_minheight.Text = "100";
            // 
            // tb_minwidth
            // 
            this.tb_minwidth.Location = new System.Drawing.Point(86, 23);
            this.tb_minwidth.Name = "tb_minwidth";
            this.tb_minwidth.Size = new System.Drawing.Size(33, 20);
            this.tb_minwidth.TabIndex = 4;
            this.tb_minwidth.Text = "100";
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Location = new System.Drawing.Point(208, 292);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(70, 30);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(12, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(238, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "(See History tab > Prediction Details, for size info)";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.tb_ConfidenceUpper);
            this.groupBox3.Controls.Add(this.tb_ConfidenceLower);
            this.groupBox3.Location = new System.Drawing.Point(14, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(263, 43);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Object Confidence limits";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(157, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Upper:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Lower:";
            // 
            // tb_ConfidenceUpper
            // 
            this.tb_ConfidenceUpper.Location = new System.Drawing.Point(199, 16);
            this.tb_ConfidenceUpper.Name = "tb_ConfidenceUpper";
            this.tb_ConfidenceUpper.Size = new System.Drawing.Size(33, 20);
            this.tb_ConfidenceUpper.TabIndex = 1;
            this.tb_ConfidenceUpper.Text = "100";
            // 
            // tb_ConfidenceLower
            // 
            this.tb_ConfidenceLower.Location = new System.Drawing.Point(86, 16);
            this.tb_ConfidenceLower.Name = "tb_ConfidenceLower";
            this.tb_ConfidenceLower.Size = new System.Drawing.Size(33, 20);
            this.tb_ConfidenceLower.TabIndex = 0;
            this.tb_ConfidenceLower.Text = "100";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.tb_duplicatepercent);
            this.groupBox4.Location = new System.Drawing.Point(12, 234);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(265, 49);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Duplicate object detection";
            // 
            // tb_duplicatepercent
            // 
            this.tb_duplicatepercent.Location = new System.Drawing.Point(86, 19);
            this.tb_duplicatepercent.Name = "tb_duplicatepercent";
            this.tb_duplicatepercent.Size = new System.Drawing.Size(33, 20);
            this.tb_duplicatepercent.TabIndex = 8;
            this.tb_duplicatepercent.Text = "100";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Match Size %:";
            // 
            // Frm_PredSizeLimits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 334);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_PredSizeLimits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prediction Tolerances";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_PredSizeLimits_FormClosed);
            this.Load += new System.EventHandler(this.Frm_PredSizeLimits_Load);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnSave;
        public System.Windows.Forms.TextBox tb_maxpercent;
        public System.Windows.Forms.TextBox tb_MinPercent;
        public System.Windows.Forms.TextBox tb_maxheight;
        public System.Windows.Forms.TextBox tb_maxwidth;
        public System.Windows.Forms.TextBox tb_minheight;
        public System.Windows.Forms.TextBox tb_minwidth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox tb_ConfidenceUpper;
        public System.Windows.Forms.TextBox tb_ConfidenceLower;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox tb_duplicatepercent;
    }
}