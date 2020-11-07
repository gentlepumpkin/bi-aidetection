namespace AITool
{
    partial class Frm_CameraAdd
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
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Cameras = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(267, 502);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(105, 46);
            this.btnApply.TabIndex = 26;
            this.btnApply.Text = "Add";
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
            this.checkedListBoxCameras.Location = new System.Drawing.Point(4, 48);
            this.checkedListBoxCameras.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkedListBoxCameras.Name = "checkedListBoxCameras";
            this.checkedListBoxCameras.Size = new System.Drawing.Size(366, 303);
            this.checkedListBoxCameras.TabIndex = 27;
            this.checkedListBoxCameras.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxCameras_ItemCheck);
            this.checkedListBoxCameras.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxCameras_SelectedIndexChanged);
            this.checkedListBoxCameras.Leave += new System.EventHandler(this.checkedListBoxCameras_Leave);
            this.checkedListBoxCameras.Validated += new System.EventHandler(this.checkedListBoxCameras_Validated);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(0, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(372, 29);
            this.label1.TabIndex = 28;
            this.label1.Text = "Select the Blue Iris cameras you want to add";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 358);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 20);
            this.label2.TabIndex = 29;
            this.label2.Text = "Or type new camera names seperated by commas:";
            // 
            // tb_Cameras
            // 
            this.tb_Cameras.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Cameras.Location = new System.Drawing.Point(2, 383);
            this.tb_Cameras.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_Cameras.Multiline = true;
            this.tb_Cameras.Name = "tb_Cameras";
            this.tb_Cameras.Size = new System.Drawing.Size(368, 101);
            this.tb_Cameras.TabIndex = 30;
            this.tb_Cameras.Leave += new System.EventHandler(this.tb_Cameras_Leave);
            // 
            // Frm_CameraAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 566);
            this.Controls.Add(this.tb_Cameras);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBoxCameras);
            this.Controls.Add(this.btnApply);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Frm_CameraAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Cameras";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Frm_CameraAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        public System.Windows.Forms.CheckedListBox checkedListBoxCameras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tb_Cameras;
    }
}