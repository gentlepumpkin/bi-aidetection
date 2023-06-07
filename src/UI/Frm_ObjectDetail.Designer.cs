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
            components = new System.ComponentModel.Container();
            folv_ObjectDetail = new BrightIdeasSoftware.FastObjectListView();
            contextMenuStripMask = new System.Windows.Forms.ContextMenuStrip(components);
            createStaticMasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)folv_ObjectDetail).BeginInit();
            contextMenuStripMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // folv_ObjectDetail
            // 
            folv_ObjectDetail.ContextMenuStrip = contextMenuStripMask;
            folv_ObjectDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            folv_ObjectDetail.Location = new System.Drawing.Point(0, 0);
            folv_ObjectDetail.Name = "folv_ObjectDetail";
            folv_ObjectDetail.ShowGroups = false;
            folv_ObjectDetail.Size = new System.Drawing.Size(316, 232);
            folv_ObjectDetail.TabIndex = 5;
            folv_ObjectDetail.UseCompatibleStateImageBehavior = false;
            folv_ObjectDetail.View = System.Windows.Forms.View.Details;
            folv_ObjectDetail.VirtualMode = true;
            folv_ObjectDetail.FormatRow += folv_ObjectDetail_FormatRow;
            folv_ObjectDetail.SelectionChanged += folv_ObjectDetail_SelectionChanged;
            folv_ObjectDetail.SelectedIndexChanged += folv_ObjectDetail_SelectedIndexChanged;
            // 
            // contextMenuStripMask
            // 
            contextMenuStripMask.ImageScalingSize = new System.Drawing.Size(24, 24);
            contextMenuStripMask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { createStaticMasksToolStripMenuItem });
            contextMenuStripMask.Name = "contextMenuStripMask";
            contextMenuStripMask.Size = new System.Drawing.Size(185, 26);
            // 
            // createStaticMasksToolStripMenuItem
            // 
            createStaticMasksToolStripMenuItem.Name = "createStaticMasksToolStripMenuItem";
            createStaticMasksToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            createStaticMasksToolStripMenuItem.Text = "Create Static Mask(s)";
            createStaticMasksToolStripMenuItem.Click += createStaticMasksToolStripMenuItem_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.AutoScroll = true;
            splitContainer1.Panel2.Controls.Add(pictureBox1);
            splitContainer1.Size = new System.Drawing.Size(962, 512);
            splitContainer1.SplitterDistance = 320;
            splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.Location = new System.Drawing.Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(folv_ObjectDetail);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(pictureBox2);
            splitContainer2.Size = new System.Drawing.Size(320, 512);
            splitContainer2.SplitterDistance = 236;
            splitContainer2.TabIndex = 6;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox2.Location = new System.Drawing.Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(316, 268);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(634, 508);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // Frm_ObjectDetail
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            ClientSize = new System.Drawing.Size(962, 512);
            Controls.Add(splitContainer1);
            Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "Frm_ObjectDetail";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Tag = "SAVE";
            Text = "Prediction Object Detail";
            FormClosing += Frm_ObjectDetail_FormClosing;
            Load += Frm_ObjectDetail_Load;
            ((System.ComponentModel.ISupportInitialize)folv_ObjectDetail).EndInit();
            contextMenuStripMask.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
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