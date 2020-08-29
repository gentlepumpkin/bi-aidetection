namespace AITool
{
    partial class Frm_DynamicMasking
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutAdvancedMasking = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.num_history_mins = new System.Windows.Forms.NumericUpDown();
            this.num_mask_create = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.num_mask_remove = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.num_percent_var = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tableLayoutAdvancedMasking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_history_mins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mask_create)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mask_remove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_percent_var)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(466, 142);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(57, 24);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(544, 142);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(57, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutAdvancedMasking
            // 
            this.tableLayoutAdvancedMasking.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutAdvancedMasking.ColumnCount = 3;
            this.tableLayoutAdvancedMasking.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.77294F));
            this.tableLayoutAdvancedMasking.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.309487F));
            this.tableLayoutAdvancedMasking.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.91757F));
            this.tableLayoutAdvancedMasking.Controls.Add(this.label16, 0, 2);
            this.tableLayoutAdvancedMasking.Controls.Add(this.label17, 0, 0);
            this.tableLayoutAdvancedMasking.Controls.Add(this.label18, 0, 1);
            this.tableLayoutAdvancedMasking.Controls.Add(this.num_history_mins, 1, 0);
            this.tableLayoutAdvancedMasking.Controls.Add(this.num_mask_create, 1, 1);
            this.tableLayoutAdvancedMasking.Controls.Add(this.label20, 2, 0);
            this.tableLayoutAdvancedMasking.Controls.Add(this.num_mask_remove, 1, 2);
            this.tableLayoutAdvancedMasking.Controls.Add(this.label21, 2, 2);
            this.tableLayoutAdvancedMasking.Controls.Add(this.label22, 0, 3);
            this.tableLayoutAdvancedMasking.Controls.Add(this.num_percent_var, 1, 3);
            this.tableLayoutAdvancedMasking.Controls.Add(this.label23, 2, 3);
            this.tableLayoutAdvancedMasking.Controls.Add(this.label19, 2, 1);
            this.tableLayoutAdvancedMasking.Location = new System.Drawing.Point(5, 6);
            this.tableLayoutAdvancedMasking.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutAdvancedMasking.Name = "tableLayoutAdvancedMasking";
            this.tableLayoutAdvancedMasking.RowCount = 4;
            this.tableLayoutAdvancedMasking.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutAdvancedMasking.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutAdvancedMasking.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutAdvancedMasking.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutAdvancedMasking.Size = new System.Drawing.Size(597, 129);
            this.tableLayoutAdvancedMasking.TabIndex = 20;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label16.Location = new System.Drawing.Point(3, 72);
            this.label16.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(111, 15);
            this.label16.TabIndex = 3;
            this.label16.Text = "Remove mask after ";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label17.Location = new System.Drawing.Point(3, 8);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(122, 15);
            this.label17.TabIndex = 2;
            this.label17.Text = "Clear object history in";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label18.Location = new System.Drawing.Point(3, 40);
            this.label18.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(99, 15);
            this.label18.TabIndex = 1;
            this.label18.Text = "Create mask after";
            // 
            // num_history_mins
            // 
            this.num_history_mins.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_history_mins.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.num_history_mins.Location = new System.Drawing.Point(131, 4);
            this.num_history_mins.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.num_history_mins.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.num_history_mins.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_history_mins.Name = "num_history_mins";
            this.num_history_mins.Size = new System.Drawing.Size(39, 23);
            this.num_history_mins.TabIndex = 6;
            this.num_history_mins.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // num_mask_create
            // 
            this.num_mask_create.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_mask_create.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.num_mask_create.Location = new System.Drawing.Point(131, 36);
            this.num_mask_create.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.num_mask_create.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.num_mask_create.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_mask_create.Name = "num_mask_create";
            this.num_mask_create.Size = new System.Drawing.Size(39, 23);
            this.num_mask_create.TabIndex = 7;
            this.num_mask_create.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label20.Location = new System.Drawing.Point(174, 8);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(317, 15);
            this.label20.TabIndex = 9;
            this.label20.Text = "minute(s).   Clears list of objects detected in same location ";
            // 
            // num_mask_remove
            // 
            this.num_mask_remove.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_mask_remove.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.num_mask_remove.Location = new System.Drawing.Point(131, 68);
            this.num_mask_remove.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.num_mask_remove.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_mask_remove.Name = "num_mask_remove";
            this.num_mask_remove.Size = new System.Drawing.Size(39, 23);
            this.num_mask_remove.TabIndex = 10;
            this.num_mask_remove.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label21.Location = new System.Drawing.Point(174, 72);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(101, 15);
            this.label21.TabIndex = 11;
            this.label21.Text = "time(s) not visible";
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label22.Location = new System.Drawing.Point(3, 105);
            this.label22.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(102, 15);
            this.label22.TabIndex = 12;
            this.label22.Text = "Object variance %";
            // 
            // num_percent_var
            // 
            this.num_percent_var.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_percent_var.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.num_percent_var.Location = new System.Drawing.Point(131, 101);
            this.num_percent_var.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.num_percent_var.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.num_percent_var.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.num_percent_var.Name = "num_percent_var";
            this.num_percent_var.Size = new System.Drawing.Size(39, 23);
            this.num_percent_var.TabIndex = 13;
            this.num_percent_var.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label23
            // 
            this.label23.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label23.Location = new System.Drawing.Point(174, 105);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(319, 15);
            this.label23.TabIndex = 14;
            this.label23.Text = "percent.  Adjusts for variations in object\'s detected location";
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label19.Location = new System.Drawing.Point(174, 40);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(365, 15);
            this.label19.TabIndex = 8;
            this.label19.Text = "detection(s).  Number of history detections needed to create a mask";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Frm_DynamicMasking
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(607, 173);
            this.Controls.Add(this.tableLayoutAdvancedMasking);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Frm_DynamicMasking";
            this.Text = "Dynamic Masking";
            this.Load += new System.EventHandler(this.Frm_DynamicMasking_Load);
            this.tableLayoutAdvancedMasking.ResumeLayout(false);
            this.tableLayoutAdvancedMasking.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_history_mins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mask_create)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mask_remove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_percent_var)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutAdvancedMasking;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.NumericUpDown num_history_mins;
        public System.Windows.Forms.NumericUpDown num_mask_create;
        public System.Windows.Forms.NumericUpDown num_mask_remove;
        public System.Windows.Forms.NumericUpDown num_percent_var;
    }
}