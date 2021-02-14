namespace AITool
{
    partial class Frm_ObjectDetail
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
            this.folv_ObjectDetail = new BrightIdeasSoftware.FastObjectListView();
            this.contextMenuStripMask = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createStaticMasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.folv_ObjectDetail)).BeginInit();
            this.contextMenuStripMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // folv_ObjectDetail
            // 
            this.folv_ObjectDetail.ContextMenuStrip = this.contextMenuStripMask;
            this.folv_ObjectDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folv_ObjectDetail.HideSelection = false;
            this.folv_ObjectDetail.Location = new System.Drawing.Point(0, 0);
            this.folv_ObjectDetail.Name = "folv_ObjectDetail";
            this.folv_ObjectDetail.ShowGroups = false;
            this.folv_ObjectDetail.Size = new System.Drawing.Size(316, 232);
            this.folv_ObjectDetail.TabIndex = 5;
            this.folv_ObjectDetail.UseCompatibleStateImageBehavior = false;
            this.folv_ObjectDetail.View = System.Windows.Forms.View.Details;
            this.folv_ObjectDetail.VirtualMode = true;
            this.folv_ObjectDetail.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.folv_ObjectDetail_FormatRow);
            this.folv_ObjectDetail.SelectionChanged += new System.EventHandler(this.folv_ObjectDetail_SelectionChanged);
            // 
            // contextMenuStripMask
            // 
            this.contextMenuStripMask.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripMask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createStaticMasksToolStripMenuItem});
            this.contextMenuStripMask.Name = "contextMenuStripMask";
            this.contextMenuStripMask.Size = new System.Drawing.Size(185, 26);
            // 
            // createStaticMasksToolStripMenuItem
            // 
            this.createStaticMasksToolStripMenuItem.Name = "createStaticMasksToolStripMenuItem";
            this.createStaticMasksToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.createStaticMasksToolStripMenuItem.Text = "Create Static Mask(s)";
            this.createStaticMasksToolStripMenuItem.Click += new System.EventHandler(this.createStaticMasksToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(962, 512);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.folv_ObjectDetail);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pictureBox2);
            this.splitContainer2.Size = new System.Drawing.Size(320, 512);
            this.splitContainer2.SplitterDistance = 236;
            this.splitContainer2.TabIndex = 6;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(316, 268);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(634, 508);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // Frm_ObjectDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(962, 512);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Frm_ObjectDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "SAVE";
            this.Text = "Prediction Object Detail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_ObjectDetail_FormClosing);
            this.Load += new System.EventHandler(this.Frm_ObjectDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.folv_ObjectDetail)).EndInit();
            this.contextMenuStripMask.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public BrightIdeasSoftware.FastObjectListView folv_ObjectDetail;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMask;
        private System.Windows.Forms.ToolStripMenuItem createStaticMasksToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}