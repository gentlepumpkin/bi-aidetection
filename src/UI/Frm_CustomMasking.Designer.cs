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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbMaskImage = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.numBrushSize = new System.Windows.Forms.NumericUpDown();
            this.rbBrush = new System.Windows.Forms.RadioButton();
            this.rbRectangle = new System.Windows.Forms.RadioButton();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMaskImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBrushSize)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pbMaskImage);
            this.panel1.Location = new System.Drawing.Point(2, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(861, 418);
            this.panel1.TabIndex = 1;
            // 
            // pbMaskImage
            // 
            this.pbMaskImage.BackColor = System.Drawing.SystemColors.Control;
            this.pbMaskImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbMaskImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pbMaskImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMaskImage.Location = new System.Drawing.Point(0, 0);
            this.pbMaskImage.Margin = new System.Windows.Forms.Padding(4);
            this.pbMaskImage.Name = "pbMaskImage";
            this.pbMaskImage.Size = new System.Drawing.Size(861, 418);
            this.pbMaskImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMaskImage.TabIndex = 1;
            this.pbMaskImage.TabStop = false;
            this.pbMaskImage.Click += new System.EventHandler(this.pbMaskImage_Click);
            this.pbMaskImage.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMaskImage_Paint);
            this.pbMaskImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMaskImage_MouseDown);
            this.pbMaskImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMaskImage_MouseMove);
            this.pbMaskImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMaskImage_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(2, 429);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(421, 58);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tools";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.numBrushSize);
            this.flowLayoutPanel1.Controls.Add(this.rbBrush);
            this.flowLayoutPanel1.Controls.Add(this.rbRectangle);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(6, 15, 6, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(409, 33);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(22, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Brush Size";
            // 
            // numBrushSize
            // 
            this.numBrushSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numBrushSize.Location = new System.Drawing.Point(91, 6);
            this.numBrushSize.Margin = new System.Windows.Forms.Padding(6);
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
            this.numBrushSize.Size = new System.Drawing.Size(48, 20);
            this.numBrushSize.TabIndex = 4;
            this.numBrushSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numBrushSize.ValueChanged += new System.EventHandler(this.numBrushSize_ValueChanged);
            this.numBrushSize.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numBrushSize_KeyUp);
            this.numBrushSize.Leave += new System.EventHandler(this.numBrushSize_Leave);
            // 
            // rbBrush
            // 
            this.rbBrush.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbBrush.AutoSize = true;
            this.rbBrush.Checked = true;
            this.rbBrush.Location = new System.Drawing.Point(200, 7);
            this.rbBrush.Margin = new System.Windows.Forms.Padding(55, 6, 6, 6);
            this.rbBrush.Name = "rbBrush";
            this.rbBrush.Size = new System.Drawing.Size(52, 17);
            this.rbBrush.TabIndex = 5;
            this.rbBrush.TabStop = true;
            this.rbBrush.Text = "Brush";
            this.rbBrush.UseVisualStyleBackColor = true;
            // 
            // rbRectangle
            // 
            this.rbRectangle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbRectangle.AutoSize = true;
            this.rbRectangle.Location = new System.Drawing.Point(264, 7);
            this.rbRectangle.Margin = new System.Windows.Forms.Padding(6, 6, 55, 6);
            this.rbRectangle.Name = "rbRectangle";
            this.rbRectangle.Size = new System.Drawing.Size(74, 17);
            this.rbRectangle.TabIndex = 6;
            this.rbRectangle.Text = "Rectangle";
            this.rbRectangle.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(620, 447);
            this.btnClear.Margin = new System.Windows.Forms.Padding(6, 15, 6, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 30);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(702, 447);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(784, 447);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 46, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // Frm_CustomMasking
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(864, 493);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Frm_CustomMasking";
            this.Text = "Custom Masking";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_CustomMasking_FormClosing);
            this.Load += new System.EventHandler(this.Frm_CustomMasking_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMaskImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBrushSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbMaskImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numBrushSize;
        private System.Windows.Forms.RadioButton rbBrush;
        private System.Windows.Forms.RadioButton rbRectangle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}