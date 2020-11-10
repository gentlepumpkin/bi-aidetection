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
            this.btn_add = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_AIServers)).BeginInit();
            this.SuspendLayout();
            // 
            // FOLV_AIServers
            // 
            this.FOLV_AIServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FOLV_AIServers.HideSelection = false;
            this.FOLV_AIServers.Location = new System.Drawing.Point(6, 5);
            this.FOLV_AIServers.Name = "FOLV_AIServers";
            this.FOLV_AIServers.ShowGroups = false;
            this.FOLV_AIServers.Size = new System.Drawing.Size(408, 97);
            this.FOLV_AIServers.TabIndex = 0;
            this.FOLV_AIServers.UseCompatibleStateImageBehavior = false;
            this.FOLV_AIServers.View = System.Windows.Forms.View.Details;
            this.FOLV_AIServers.VirtualMode = true;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(286, 127);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(61, 26);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(353, 127);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(61, 26);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(43, 127);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(92, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Type:";
            // 
            // Frm_AIServers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 157);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.FOLV_AIServers);
            this.Name = "Frm_AIServers";
            this.Text = "AI Server List";
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_AIServers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView FOLV_AIServers;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}