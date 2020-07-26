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
            this.label1 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPost = new System.Windows.Forms.TextBox();
            this.btUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Actions)).BeginInit();
            this.SuspendLayout();
            // 
            // FOLV_Actions
            // 
            this.FOLV_Actions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FOLV_Actions.HideSelection = false;
            this.FOLV_Actions.Location = new System.Drawing.Point(12, 12);
            this.FOLV_Actions.Name = "FOLV_Actions";
            this.FOLV_Actions.ShowGroups = false;
            this.FOLV_Actions.Size = new System.Drawing.Size(743, 157);
            this.FOLV_Actions.TabIndex = 0;
            this.FOLV_Actions.UseCompatibleStateImageBehavior = false;
            this.FOLV_Actions.View = System.Windows.Forms.View.Details;
            this.FOLV_Actions.VirtualMode = true;
            this.FOLV_Actions.SelectionChanged += new System.EventHandler(this.FOLV_Actions_SelectionChanged);
            this.FOLV_Actions.SelectedIndexChanged += new System.EventHandler(this.FOLV_Actions_SelectedIndexChanged);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAdd.Location = new System.Drawing.Point(273, 186);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(80, 42);
            this.btAdd.TabIndex = 1;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btDelete.Location = new System.Drawing.Point(78, 186);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(80, 42);
            this.btDelete.TabIndex = 2;
            this.btDelete.Text = "Delete";
            this.btDelete.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(543, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Type:";
            // 
            // cbType
            // 
            this.cbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(596, 194);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(159, 28);
            this.cbType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "ID:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Key:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 310);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "URL:";
            // 
            // tbID
            // 
            this.tbID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbID.Location = new System.Drawing.Point(76, 243);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(679, 26);
            this.tbID.TabIndex = 6;
            // 
            // tbKey
            // 
            this.tbKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKey.Location = new System.Drawing.Point(76, 275);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(679, 26);
            this.tbKey.TabIndex = 7;
            // 
            // tbURL
            // 
            this.tbURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbURL.Location = new System.Drawing.Point(76, 307);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(679, 26);
            this.tbURL.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 342);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Post:";
            // 
            // tbPost
            // 
            this.tbPost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPost.Location = new System.Drawing.Point(76, 339);
            this.tbPost.Name = "tbPost";
            this.tbPost.Size = new System.Drawing.Size(679, 26);
            this.tbPost.TabIndex = 9;
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btUpdate.Location = new System.Drawing.Point(176, 186);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(80, 42);
            this.btUpdate.TabIndex = 10;
            this.btUpdate.Text = "Save";
            this.btUpdate.UseVisualStyleBackColor = true;
            // 
            // Frm_Actions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 389);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.tbPost);
            this.Controls.Add(this.tbURL);
            this.Controls.Add(this.tbKey);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.FOLV_Actions);
            this.Name = "Frm_Actions";
            this.Text = "Camera Trigger Actions";
            this.Load += new System.EventHandler(this.Frm_Actions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Actions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView FOLV_Actions;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPost;
        private System.Windows.Forms.Button btUpdate;
    }
}