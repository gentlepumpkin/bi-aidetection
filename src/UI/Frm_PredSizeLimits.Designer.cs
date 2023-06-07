
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
            label1 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label3 = new System.Windows.Forms.Label();
            tb_maxpercent = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            tb_MinPercent = new System.Windows.Forms.TextBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label7 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            tb_maxheight = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            tb_maxwidth = new System.Windows.Forms.TextBox();
            tb_minheight = new System.Windows.Forms.TextBox();
            tb_minwidth = new System.Windows.Forms.TextBox();
            BtnSave = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            tb_ConfidenceUpper = new System.Windows.Forms.TextBox();
            tb_ConfidenceLower = new System.Windows.Forms.TextBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            label11 = new System.Windows.Forms.Label();
            tb_duplicatepercent = new System.Windows.Forms.TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(42, 44);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(42, 13);
            label1.TabIndex = 0;
            label1.Text = "Min %:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(tb_maxpercent);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(tb_MinPercent);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(14, 59);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(263, 70);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Limit Size Percentage";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = System.Drawing.Color.DimGray;
            label3.Location = new System.Drawing.Point(9, 20);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(203, 13);
            label3.TabIndex = 2;
            label3.Text = "Percentage of prediction size vs image";
            // 
            // tb_maxpercent
            // 
            tb_maxpercent.Location = new System.Drawing.Point(199, 41);
            tb_maxpercent.Name = "tb_maxpercent";
            tb_maxpercent.Size = new System.Drawing.Size(33, 22);
            tb_maxpercent.TabIndex = 3;
            tb_maxpercent.Text = "100";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(157, 44);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(43, 13);
            label2.TabIndex = 0;
            label2.Text = "Max %:";
            // 
            // tb_MinPercent
            // 
            tb_MinPercent.Location = new System.Drawing.Point(86, 41);
            tb_MinPercent.Name = "tb_MinPercent";
            tb_MinPercent.Size = new System.Drawing.Size(33, 22);
            tb_MinPercent.TabIndex = 2;
            tb_MinPercent.Text = "100";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(tb_maxheight);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(tb_maxwidth);
            groupBox2.Controls.Add(tb_minheight);
            groupBox2.Controls.Add(tb_minwidth);
            groupBox2.Location = new System.Drawing.Point(12, 151);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(264, 77);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Limit Size in Pixels (0 disables)";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(134, 50);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(69, 13);
            label7.TabIndex = 0;
            label7.Text = "Max Height:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(19, 50);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(68, 13);
            label5.TabIndex = 0;
            label5.Text = "Min Height:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(137, 26);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(66, 13);
            label6.TabIndex = 0;
            label6.Text = "Max Width:";
            // 
            // tb_maxheight
            // 
            tb_maxheight.Location = new System.Drawing.Point(201, 47);
            tb_maxheight.Name = "tb_maxheight";
            tb_maxheight.Size = new System.Drawing.Size(33, 22);
            tb_maxheight.TabIndex = 7;
            tb_maxheight.Text = "100";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(22, 26);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(65, 13);
            label4.TabIndex = 0;
            label4.Text = "Min Width:";
            // 
            // tb_maxwidth
            // 
            tb_maxwidth.Location = new System.Drawing.Point(201, 23);
            tb_maxwidth.Name = "tb_maxwidth";
            tb_maxwidth.Size = new System.Drawing.Size(33, 22);
            tb_maxwidth.TabIndex = 5;
            tb_maxwidth.Text = "100";
            // 
            // tb_minheight
            // 
            tb_minheight.Location = new System.Drawing.Point(86, 47);
            tb_minheight.Name = "tb_minheight";
            tb_minheight.Size = new System.Drawing.Size(33, 22);
            tb_minheight.TabIndex = 6;
            tb_minheight.Text = "100";
            // 
            // tb_minwidth
            // 
            tb_minwidth.Location = new System.Drawing.Point(86, 23);
            tb_minwidth.Name = "tb_minwidth";
            tb_minwidth.Size = new System.Drawing.Size(33, 22);
            tb_minwidth.TabIndex = 4;
            tb_minwidth.Text = "100";
            // 
            // BtnSave
            // 
            BtnSave.Location = new System.Drawing.Point(211, 292);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new System.Drawing.Size(70, 30);
            BtnSave.TabIndex = 9;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = System.Drawing.Color.DimGray;
            label8.Location = new System.Drawing.Point(12, 132);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(261, 13);
            label8.TabIndex = 2;
            label8.Text = "(See History tab > Prediction Details, for size info)";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(tb_ConfidenceUpper);
            groupBox3.Controls.Add(tb_ConfidenceLower);
            groupBox3.Location = new System.Drawing.Point(14, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(263, 43);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "Object Confidence limits";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(157, 19);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(42, 13);
            label10.TabIndex = 0;
            label10.Text = "Upper:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(41, 19);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(41, 13);
            label9.TabIndex = 0;
            label9.Text = "Lower:";
            // 
            // tb_ConfidenceUpper
            // 
            tb_ConfidenceUpper.Location = new System.Drawing.Point(199, 16);
            tb_ConfidenceUpper.Name = "tb_ConfidenceUpper";
            tb_ConfidenceUpper.Size = new System.Drawing.Size(33, 22);
            tb_ConfidenceUpper.TabIndex = 1;
            tb_ConfidenceUpper.Text = "100";
            // 
            // tb_ConfidenceLower
            // 
            tb_ConfidenceLower.Location = new System.Drawing.Point(86, 16);
            tb_ConfidenceLower.Name = "tb_ConfidenceLower";
            tb_ConfidenceLower.Size = new System.Drawing.Size(33, 22);
            tb_ConfidenceLower.TabIndex = 0;
            tb_ConfidenceLower.Text = "100";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(tb_duplicatepercent);
            groupBox4.Location = new System.Drawing.Point(12, 234);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(265, 49);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            groupBox4.Text = "Duplicate object detection";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(6, 22);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(77, 13);
            label11.TabIndex = 2;
            label11.Text = "Match Size %:";
            // 
            // tb_duplicatepercent
            // 
            tb_duplicatepercent.Location = new System.Drawing.Point(86, 19);
            tb_duplicatepercent.Name = "tb_duplicatepercent";
            tb_duplicatepercent.Size = new System.Drawing.Size(33, 22);
            tb_duplicatepercent.TabIndex = 8;
            tb_duplicatepercent.Text = "100";
            // 
            // Frm_PredSizeLimits
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            AutoScroll = true;
            ClientSize = new System.Drawing.Size(288, 329);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(label8);
            Controls.Add(BtnSave);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "Frm_PredSizeLimits";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Prediction Tolerances";
            FormClosed += Frm_PredSizeLimits_FormClosed;
            Load += Frm_PredSizeLimits_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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