
namespace AITool
{
    partial class Frm_Faces
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.FOLV_Faces = new BrightIdeasSoftware.FastObjectListView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxFaceServers = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBoxMainFace = new System.Windows.Forms.PictureBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.FOLV_FaceFiles = new BrightIdeasSoftware.FastObjectListView();
            this.pictureBoxCurrentFace = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Faces)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMainFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_FaceFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurrentFace)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1041, 449);
            this.splitContainer1.SplitterDistance = 287;
            this.splitContainer1.TabIndex = 0;
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
            this.splitContainer2.Panel1.Controls.Add(this.FOLV_Faces);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pictureBoxMainFace);
            this.splitContainer2.Size = new System.Drawing.Size(287, 449);
            this.splitContainer2.SplitterDistance = 248;
            this.splitContainer2.TabIndex = 0;
            // 
            // FOLV_Faces
            // 
            this.FOLV_Faces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FOLV_Faces.HideSelection = false;
            this.FOLV_Faces.Location = new System.Drawing.Point(0, 0);
            this.FOLV_Faces.Name = "FOLV_Faces";
            this.FOLV_Faces.ShowGroups = false;
            this.FOLV_Faces.Size = new System.Drawing.Size(283, 244);
            this.FOLV_Faces.TabIndex = 0;
            this.FOLV_Faces.UseCompatibleStateImageBehavior = false;
            this.FOLV_Faces.View = System.Windows.Forms.View.Details;
            this.FOLV_Faces.VirtualMode = true;
            this.FOLV_Faces.SelectionChanged += new System.EventHandler(this.FOLV_Faces_SelectionChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripComboBoxFaceServers});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1041, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(126, 22);
            this.toolStripLabel1.Text = "Deepstack Face Server:";
            // 
            // toolStripComboBoxFaceServers
            // 
            this.toolStripComboBoxFaceServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxFaceServers.DropDownWidth = 300;
            this.toolStripComboBoxFaceServers.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBoxFaceServers.Name = "toolStripComboBoxFaceServers";
            this.toolStripComboBoxFaceServers.Size = new System.Drawing.Size(200, 25);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 480);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1041, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Status";
            // 
            // pictureBoxMainFace
            // 
            this.pictureBoxMainFace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxMainFace.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMainFace.Name = "pictureBoxMainFace";
            this.pictureBoxMainFace.Size = new System.Drawing.Size(283, 193);
            this.pictureBoxMainFace.TabIndex = 0;
            this.pictureBoxMainFace.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.FOLV_FaceFiles);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.pictureBoxCurrentFace);
            this.splitContainer3.Size = new System.Drawing.Size(750, 449);
            this.splitContainer3.SplitterDistance = 173;
            this.splitContainer3.TabIndex = 0;
            // 
            // FOLV_FaceFiles
            // 
            this.FOLV_FaceFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FOLV_FaceFiles.HideSelection = false;
            this.FOLV_FaceFiles.Location = new System.Drawing.Point(0, 0);
            this.FOLV_FaceFiles.Name = "FOLV_FaceFiles";
            this.FOLV_FaceFiles.ShowGroups = false;
            this.FOLV_FaceFiles.Size = new System.Drawing.Size(169, 445);
            this.FOLV_FaceFiles.TabIndex = 0;
            this.FOLV_FaceFiles.UseCompatibleStateImageBehavior = false;
            this.FOLV_FaceFiles.View = System.Windows.Forms.View.Details;
            this.FOLV_FaceFiles.VirtualMode = true;
            // 
            // pictureBoxCurrentFace
            // 
            this.pictureBoxCurrentFace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxCurrentFace.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxCurrentFace.Name = "pictureBoxCurrentFace";
            this.pictureBoxCurrentFace.Size = new System.Drawing.Size(569, 445);
            this.pictureBoxCurrentFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCurrentFace.TabIndex = 0;
            this.pictureBoxCurrentFace.TabStop = false;
            // 
            // Frm_Faces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 502);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Frm_Faces";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "SAVE";
            this.Text = "Deepstack Faces";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Faces_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Faces_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Faces)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMainFace)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_FaceFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurrentFace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private BrightIdeasSoftware.FastObjectListView FOLV_Faces;
        private System.Windows.Forms.PictureBox pictureBoxMainFace;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxFaceServers;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private BrightIdeasSoftware.FastObjectListView FOLV_FaceFiles;
        private System.Windows.Forms.PictureBox pictureBoxCurrentFace;
    }
}