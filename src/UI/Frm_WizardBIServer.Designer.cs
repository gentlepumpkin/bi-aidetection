
namespace AITool
{
    partial class Frm_WizardBIServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_WizardBIServer));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_BlueIrisServer = new System.Windows.Forms.TextBox();
            this.bt_validate = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblURL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(582, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select BlueIris Server Machine";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(558, 117);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("label2.Text");
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txt_BlueIrisServer
            // 
            this.txt_BlueIrisServer.Location = new System.Drawing.Point(15, 167);
            this.txt_BlueIrisServer.Name = "txt_BlueIrisServer";
            this.txt_BlueIrisServer.Size = new System.Drawing.Size(189, 20);
            this.txt_BlueIrisServer.TabIndex = 2;
            // 
            // bt_validate
            // 
            this.bt_validate.Location = new System.Drawing.Point(215, 163);
            this.bt_validate.Name = "bt_validate";
            this.bt_validate.Size = new System.Drawing.Size(60, 26);
            this.bt_validate.TabIndex = 3;
            this.bt_validate.Text = "Validate";
            this.bt_validate.UseVisualStyleBackColor = true;
            this.bt_validate.Click += new System.EventHandler(this.bt_validate_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(281, 170);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(53, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Unknown";
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(12, 204);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(10, 13);
            this.lblURL.TabIndex = 5;
            this.lblURL.Text = ".";
            // 
            // Frm_WizardBIServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(582, 277);
            this.ControlBox = false;
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.bt_validate);
            this.Controls.Add(this.txt_BlueIrisServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_WizardBIServer";
            this.Text = "Frm_WizardBI";
            this.Load += new System.EventHandler(this.Frm_WizardBIServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txt_BlueIrisServer;
        private System.Windows.Forms.Button bt_validate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblURL;
    }
}