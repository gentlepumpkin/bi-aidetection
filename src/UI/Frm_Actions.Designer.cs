namespace AITool
{
    partial class Frm_Actions
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
            this.FOLV_Actions = new BrightIdeasSoftware.FastObjectListView();
            this.btAdd = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Actions)).BeginInit();
            this.SuspendLayout();
            // 
            // FOLV_Actions
            // 
            this.FOLV_Actions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FOLV_Actions.HideSelection = false;
            this.FOLV_Actions.Location = new System.Drawing.Point(15, 14);
            this.FOLV_Actions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FOLV_Actions.Name = "FOLV_Actions";
            this.FOLV_Actions.ShowGroups = false;
            this.FOLV_Actions.Size = new System.Drawing.Size(801, 222);
            this.FOLV_Actions.TabIndex = 0;
            this.FOLV_Actions.UseCompatibleStateImageBehavior = false;
            this.FOLV_Actions.View = System.Windows.Forms.View.Details;
            this.FOLV_Actions.VirtualMode = true;
            this.FOLV_Actions.SelectionChanged += new System.EventHandler(this.FOLV_Actions_SelectionChanged);
            this.FOLV_Actions.SelectedIndexChanged += new System.EventHandler(this.FOLV_Actions_SelectedIndexChanged);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Location = new System.Drawing.Point(823, 16);
            this.btAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(98, 50);
            this.btAdd.TabIndex = 1;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelete.Location = new System.Drawing.Point(823, 74);
            this.btDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(98, 50);
            this.btDelete.TabIndex = 2;
            this.btDelete.Text = "Delete";
            this.btDelete.UseVisualStyleBackColor = true;
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(823, 131);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(98, 50);
            this.btUp.TabIndex = 3;
            this.btUp.Text = "Up";
            this.btUp.UseVisualStyleBackColor = true;
            // 
            // btDown
            // 
            this.btDown.Location = new System.Drawing.Point(823, 187);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(98, 50);
            this.btDown.TabIndex = 4;
            this.btDown.Text = "Down";
            this.btDown.UseVisualStyleBackColor = true;
            // 
            // Frm_Actions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 249);
            this.Controls.Add(this.btDown);
            this.Controls.Add(this.btUp);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.FOLV_Actions);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frm_Actions";
            this.Text = "Camera Trigger Actions";
            this.Load += new System.EventHandler(this.Frm_Actions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Actions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView FOLV_Actions;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btDown;
    }
}