namespace AITool
{
    partial class Frm_DynamicMaskDetails
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FOLV_MaskHistory = new BrightIdeasSoftware.FastObjectListView();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FOLV_Masks = new BrightIdeasSoftware.FastObjectListView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblClearHistory = new System.Windows.Forms.Label();
            this.lblClearMasks = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_MaskHistory)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Masks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(426, 453);
            this.splitContainer1.SplitterDistance = 226;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblClearHistory);
            this.groupBox1.Controls.Add(this.FOLV_MaskHistory);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(426, 226);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "History";
            // 
            // FOLV_MaskHistory
            // 
            this.FOLV_MaskHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FOLV_MaskHistory.EmptyListMsg = "None";
            this.FOLV_MaskHistory.HideSelection = false;
            this.FOLV_MaskHistory.Location = new System.Drawing.Point(2, 15);
            this.FOLV_MaskHistory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FOLV_MaskHistory.Name = "FOLV_MaskHistory";
            this.FOLV_MaskHistory.ShowGroups = false;
            this.FOLV_MaskHistory.Size = new System.Drawing.Size(422, 209);
            this.FOLV_MaskHistory.TabIndex = 0;
            this.FOLV_MaskHistory.UseCompatibleStateImageBehavior = false;
            this.FOLV_MaskHistory.View = System.Windows.Forms.View.Details;
            this.FOLV_MaskHistory.VirtualMode = true;
            this.FOLV_MaskHistory.SelectionChanged += new System.EventHandler(this.FOLV_MaskHistory_SelectionChanged);
            this.FOLV_MaskHistory.SelectedIndexChanged += new System.EventHandler(this.FOLV_MaskHistory_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(378, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Refresh";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblClearMasks);
            this.groupBox2.Controls.Add(this.FOLV_Masks);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(426, 225);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Active Masks";
            // 
            // FOLV_Masks
            // 
            this.FOLV_Masks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FOLV_Masks.EmptyListMsg = "Empty";
            this.FOLV_Masks.HideSelection = false;
            this.FOLV_Masks.Location = new System.Drawing.Point(2, 15);
            this.FOLV_Masks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FOLV_Masks.Name = "FOLV_Masks";
            this.FOLV_Masks.ShowGroups = false;
            this.FOLV_Masks.Size = new System.Drawing.Size(422, 208);
            this.FOLV_Masks.TabIndex = 0;
            this.FOLV_Masks.UseCompatibleStateImageBehavior = false;
            this.FOLV_Masks.View = System.Windows.Forms.View.Details;
            this.FOLV_Masks.VirtualMode = true;
            this.FOLV_Masks.SelectionChanged += new System.EventHandler(this.FOLV_Masks_SelectionChanged);
            this.FOLV_Masks.SelectedIndexChanged += new System.EventHandler(this.FOLV_Masks_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer2.Size = new System.Drawing.Size(830, 453);
            this.splitContainer2.SplitterDistance = 426;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(402, 453);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // lblClearHistory
            // 
            this.lblClearHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblClearHistory.AutoSize = true;
            this.lblClearHistory.BackColor = System.Drawing.SystemColors.Control;
            this.lblClearHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClearHistory.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblClearHistory.Location = new System.Drawing.Point(45, 0);
            this.lblClearHistory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClearHistory.Name = "lblClearHistory";
            this.lblClearHistory.Size = new System.Drawing.Size(66, 13);
            this.lblClearHistory.TabIndex = 3;
            this.lblClearHistory.Text = "Clear History";
            this.lblClearHistory.Click += new System.EventHandler(this.lblClearHistory_Click);
            // 
            // lblClearMasks
            // 
            this.lblClearMasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblClearMasks.AutoSize = true;
            this.lblClearMasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClearMasks.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblClearMasks.Location = new System.Drawing.Point(77, 0);
            this.lblClearMasks.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClearMasks.Name = "lblClearMasks";
            this.lblClearMasks.Size = new System.Drawing.Size(65, 13);
            this.lblClearMasks.TabIndex = 4;
            this.lblClearMasks.Text = "Clear Masks";
            this.lblClearMasks.Click += new System.EventHandler(this.lblClearMasks_Click);
            // 
            // Frm_DynamicMaskDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(830, 453);
            this.Controls.Add(this.splitContainer2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Frm_DynamicMaskDetails";
            this.Text = "Dynamic Mask Detail";
            this.Load += new System.EventHandler(this.Frm_DynamicMaskDetails_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_MaskHistory)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Masks)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private BrightIdeasSoftware.FastObjectListView FOLV_MaskHistory;
        private BrightIdeasSoftware.FastObjectListView FOLV_Masks;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblClearHistory;
        private System.Windows.Forms.Label lblClearMasks;
    }
}