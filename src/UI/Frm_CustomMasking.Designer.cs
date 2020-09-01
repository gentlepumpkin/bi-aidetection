namespace AITool
{
    partial class Frm_CustomMasking
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.numBrushSize = new System.Windows.Forms.NumericUpDown();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbMaskImage = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBrushSize)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMaskImage)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.numBrushSize);
            this.flowLayoutPanel1.Controls.Add(this.btnClear);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 565);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(998, 34);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Brush Size";
            // 
            // numBrushSize
            // 
            this.numBrushSize.Location = new System.Drawing.Point(66, 3);
            this.numBrushSize.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numBrushSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBrushSize.Name = "numBrushSize";
            this.numBrushSize.Size = new System.Drawing.Size(51, 20);
            this.numBrushSize.TabIndex = 4;
            this.numBrushSize.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numBrushSize.Leave += new System.EventHandler(this.numBrushSize_Leave);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(123, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(204, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(285, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbMaskImage);
            this.panel1.Location = new System.Drawing.Point(1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 550);
            this.panel1.TabIndex = 1;
            // 
            // pbMaskImage
            // 
            this.pbMaskImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbMaskImage.BackColor = System.Drawing.SystemColors.Control;
            this.pbMaskImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbMaskImage.Location = new System.Drawing.Point(0, 0);
            this.pbMaskImage.Margin = new System.Windows.Forms.Padding(2);
            this.pbMaskImage.Name = "pbMaskImage";
            this.pbMaskImage.Size = new System.Drawing.Size(998, 550);
            this.pbMaskImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMaskImage.TabIndex = 1;
            this.pbMaskImage.TabStop = false;
            this.pbMaskImage.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMaskImage_Paint);
            this.pbMaskImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMaskImage_MouseDown);
            this.pbMaskImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMaskImage_MouseMove);
            this.pbMaskImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMaskImage_MouseUp);
            // 
            // Frm_CustomMasking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 600);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Frm_CustomMasking";
            this.Text = "Custom Masking";
            this.Load += new System.EventHandler(this.Frm_CustomMasking_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBrushSize)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMaskImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numBrushSize;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbMaskImage;
    }
}