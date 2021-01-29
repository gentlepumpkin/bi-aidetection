
namespace AITool
{
    partial class Frm_Variables
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
            this.FOLV_Vars = new BrightIdeasSoftware.FastObjectListView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Vars)).BeginInit();
            this.SuspendLayout();
            // 
            // FOLV_Vars
            // 
            this.FOLV_Vars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FOLV_Vars.HideSelection = false;
            this.FOLV_Vars.Location = new System.Drawing.Point(2, 25);
            this.FOLV_Vars.Name = "FOLV_Vars";
            this.FOLV_Vars.ShowGroups = false;
            this.FOLV_Vars.Size = new System.Drawing.Size(382, 450);
            this.FOLV_Vars.TabIndex = 0;
            this.FOLV_Vars.UseCompatibleStateImageBehavior = false;
            this.FOLV_Vars.View = System.Windows.Forms.View.Details;
            this.FOLV_Vars.VirtualMode = true;
            this.FOLV_Vars.SelectedIndexChanged += new System.EventHandler(this.FOLV_Vars_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CTRL-Click copies variables to clipboard";
            // 
            // Frm_Variables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 476);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FOLV_Vars);
            this.Name = "Frm_Variables";
            this.Tag = "SAVE";
            this.Text = "Variables";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_Variables_FormClosed);
            this.Load += new System.EventHandler(this.Frm_Variables_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FOLV_Vars)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public BrightIdeasSoftware.FastObjectListView FOLV_Vars;
        private System.Windows.Forms.Label label1;
    }
}