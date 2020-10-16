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
            ((System.ComponentModel.ISupportInitialize)(this.folv_ObjectDetail)).BeginInit();
            this.contextMenuStripMask.SuspendLayout();
            this.SuspendLayout();
            // 
            // folv_ObjectDetail
            // 
            this.folv_ObjectDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.folv_ObjectDetail.ContextMenuStrip = this.contextMenuStripMask;
            this.folv_ObjectDetail.HideSelection = false;
            this.folv_ObjectDetail.Location = new System.Drawing.Point(12, 12);
            this.folv_ObjectDetail.Name = "folv_ObjectDetail";
            this.folv_ObjectDetail.ShowGroups = false;
            this.folv_ObjectDetail.Size = new System.Drawing.Size(962, 387);
            this.folv_ObjectDetail.TabIndex = 5;
            this.folv_ObjectDetail.UseCompatibleStateImageBehavior = false;
            this.folv_ObjectDetail.View = System.Windows.Forms.View.Details;
            this.folv_ObjectDetail.VirtualMode = true;
            this.folv_ObjectDetail.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.folv_ObjectDetail_FormatRow);
            // 
            // contextMenuStripMask
            // 
            this.contextMenuStripMask.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripMask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createStaticMasksToolStripMenuItem});
            this.contextMenuStripMask.Name = "contextMenuStripMask";
            this.contextMenuStripMask.Size = new System.Drawing.Size(247, 69);
            // 
            // createStaticMasksToolStripMenuItem
            // 
            this.createStaticMasksToolStripMenuItem.Name = "createStaticMasksToolStripMenuItem";
            this.createStaticMasksToolStripMenuItem.Size = new System.Drawing.Size(246, 32);
            this.createStaticMasksToolStripMenuItem.Text = "Create Static Mask(s)";
            this.createStaticMasksToolStripMenuItem.Click += new System.EventHandler(this.createStaticMasksToolStripMenuItem_Click);
            // 
            // Frm_ObjectDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(986, 411);
            this.Controls.Add(this.folv_ObjectDetail);
            this.Name = "Frm_ObjectDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prediction Object Detail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_ObjectDetail_FormClosing);
            this.Load += new System.EventHandler(this.Frm_ObjectDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.folv_ObjectDetail)).EndInit();
            this.contextMenuStripMask.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public BrightIdeasSoftware.FastObjectListView folv_ObjectDetail;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMask;
        private System.Windows.Forms.ToolStripMenuItem createStaticMasksToolStripMenuItem;
    }
}