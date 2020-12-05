
namespace AITool
{
    partial class Frm_AIServers
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
            this.FOLV_AIServers = new BrightIdeasSoftware.FastObjectListView();
            this.bt_add = new System.Windows.Forms.Button();
            this.bt_delete = new System.Windows.Forms.Button();
            this.bt_up = new System.Windows.Forms.Button();
            this.bt_down = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_AIServers)).BeginInit();
            this.SuspendLayout();
            // 
            // FOLV_AIServers
            // 
            this.FOLV_AIServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FOLV_AIServers.HideSelection = false;
            this.FOLV_AIServers.Location = new System.Drawing.Point(12, 12);
            this.FOLV_AIServers.Name = "FOLV_AIServers";
            this.FOLV_AIServers.ShowGroups = false;
            this.FOLV_AIServers.Size = new System.Drawing.Size(566, 137);
            this.FOLV_AIServers.TabIndex = 0;
            this.FOLV_AIServers.UseCompatibleStateImageBehavior = false;
            this.FOLV_AIServers.View = System.Windows.Forms.View.Details;
            this.FOLV_AIServers.VirtualMode = true;
            // 
            // bt_add
            // 
            this.bt_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_add.Enabled = false;
            this.bt_add.Location = new System.Drawing.Point(584, 11);
            this.bt_add.Name = "bt_add";
            this.bt_add.Size = new System.Drawing.Size(70, 30);
            this.bt_add.TabIndex = 1;
            this.bt_add.Text = "Add";
            this.bt_add.UseVisualStyleBackColor = true;
            // 
            // bt_delete
            // 
            this.bt_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_delete.Enabled = false;
            this.bt_delete.Location = new System.Drawing.Point(584, 47);
            this.bt_delete.Name = "bt_delete";
            this.bt_delete.Size = new System.Drawing.Size(70, 30);
            this.bt_delete.TabIndex = 1;
            this.bt_delete.Text = "Delete";
            this.bt_delete.UseVisualStyleBackColor = true;
            // 
            // bt_up
            // 
            this.bt_up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_up.Enabled = false;
            this.bt_up.Location = new System.Drawing.Point(584, 83);
            this.bt_up.Name = "bt_up";
            this.bt_up.Size = new System.Drawing.Size(70, 30);
            this.bt_up.TabIndex = 1;
            this.bt_up.Text = "Up";
            this.bt_up.UseVisualStyleBackColor = true;
            // 
            // bt_down
            // 
            this.bt_down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_down.Enabled = false;
            this.bt_down.Location = new System.Drawing.Point(584, 119);
            this.bt_down.Name = "bt_down";
            this.bt_down.Size = new System.Drawing.Size(70, 30);
            this.bt_down.TabIndex = 1;
            this.bt_down.Text = "Down";
            this.bt_down.UseVisualStyleBackColor = true;
            // 
            // Frm_AIServers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 161);
            this.Controls.Add(this.bt_down);
            this.Controls.Add(this.bt_up);
            this.Controls.Add(this.bt_delete);
            this.Controls.Add(this.bt_add);
            this.Controls.Add(this.FOLV_AIServers);
            this.Name = "Frm_AIServers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AI Servers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_AddAIServers_FormClosing);
            this.Load += new System.EventHandler(this.Frm_AddAIServers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_AIServers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bt_add;
        private System.Windows.Forms.Button bt_delete;
        private System.Windows.Forms.Button bt_up;
        private System.Windows.Forms.Button bt_down;
        public BrightIdeasSoftware.FastObjectListView FOLV_AIServers;
    }
}