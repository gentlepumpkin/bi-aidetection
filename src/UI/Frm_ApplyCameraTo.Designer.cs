namespace AITool
{
    partial class Frm_ApplyCameraTo
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
            this.btnApply = new System.Windows.Forms.Button();
            this.checkedListBoxCameras = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_apply_objects = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_apply_confidence_limits = new System.Windows.Forms.CheckBox();
            this.cb_apply_actions = new System.Windows.Forms.CheckBox();
            this.cb_apply_mask_settings = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(172, 326);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(70, 30);
            this.btnApply.TabIndex = 26;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // checkedListBoxCameras
            // 
            this.checkedListBoxCameras.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxCameras.CheckOnClick = true;
            this.checkedListBoxCameras.FormattingEnabled = true;
            this.checkedListBoxCameras.Location = new System.Drawing.Point(3, 40);
            this.checkedListBoxCameras.Name = "checkedListBoxCameras";
            this.checkedListBoxCameras.Size = new System.Drawing.Size(239, 199);
            this.checkedListBoxCameras.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(0, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 28);
            this.label1.TabIndex = 28;
            this.label1.Text = "Select the cameras you want to apply the current cameras settings to.";
            // 
            // cb_apply_objects
            // 
            this.cb_apply_objects.AutoSize = true;
            this.cb_apply_objects.Checked = true;
            this.cb_apply_objects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_apply_objects.Location = new System.Drawing.Point(8, 19);
            this.cb_apply_objects.Name = "cb_apply_objects";
            this.cb_apply_objects.Size = new System.Drawing.Size(108, 17);
            this.cb_apply_objects.TabIndex = 29;
            this.cb_apply_objects.Text = "Relevant Objects";
            this.cb_apply_objects.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cb_apply_mask_settings);
            this.groupBox1.Controls.Add(this.cb_apply_actions);
            this.groupBox1.Controls.Add(this.cb_apply_confidence_limits);
            this.groupBox1.Controls.Add(this.cb_apply_objects);
            this.groupBox1.Location = new System.Drawing.Point(4, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 72);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings to apply";
            // 
            // cb_apply_confidence_limits
            // 
            this.cb_apply_confidence_limits.AutoSize = true;
            this.cb_apply_confidence_limits.Checked = true;
            this.cb_apply_confidence_limits.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_apply_confidence_limits.Location = new System.Drawing.Point(123, 19);
            this.cb_apply_confidence_limits.Name = "cb_apply_confidence_limits";
            this.cb_apply_confidence_limits.Size = new System.Drawing.Size(109, 17);
            this.cb_apply_confidence_limits.TabIndex = 29;
            this.cb_apply_confidence_limits.Text = "Confidence Limits";
            this.cb_apply_confidence_limits.UseVisualStyleBackColor = true;
            // 
            // cb_apply_actions
            // 
            this.cb_apply_actions.AutoSize = true;
            this.cb_apply_actions.Checked = true;
            this.cb_apply_actions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_apply_actions.Location = new System.Drawing.Point(8, 42);
            this.cb_apply_actions.Name = "cb_apply_actions";
            this.cb_apply_actions.Size = new System.Drawing.Size(61, 17);
            this.cb_apply_actions.TabIndex = 29;
            this.cb_apply_actions.Text = "Actions";
            this.cb_apply_actions.UseVisualStyleBackColor = true;
            // 
            // cb_apply_mask_settings
            // 
            this.cb_apply_mask_settings.AutoSize = true;
            this.cb_apply_mask_settings.Checked = true;
            this.cb_apply_mask_settings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_apply_mask_settings.Location = new System.Drawing.Point(123, 42);
            this.cb_apply_mask_settings.Name = "cb_apply_mask_settings";
            this.cb_apply_mask_settings.Size = new System.Drawing.Size(93, 17);
            this.cb_apply_mask_settings.TabIndex = 29;
            this.cb_apply_mask_settings.Text = "Mask Settings";
            this.cb_apply_mask_settings.UseVisualStyleBackColor = true;
            // 
            // Frm_ApplyCameraTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 368);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBoxCameras);
            this.Controls.Add(this.btnApply);
            this.Name = "Frm_ApplyCameraTo";
            this.Text = "Apply to...";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        public System.Windows.Forms.CheckedListBox checkedListBoxCameras;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox cb_apply_objects;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox cb_apply_mask_settings;
        public System.Windows.Forms.CheckBox cb_apply_actions;
        public System.Windows.Forms.CheckBox cb_apply_confidence_limits;
    }
}