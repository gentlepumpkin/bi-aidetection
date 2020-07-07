namespace WindowsFormsApp2
{
    partial class InputForm
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
            this.lbl_1 = new System.Windows.Forms.Label();
            this.tb_1 = new System.Windows.Forms.TextBox();
            this.btn_2 = new System.Windows.Forms.Button();
            this.btn_1 = new System.Windows.Forms.Button();
            this.cb_1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_1
            // 
            this.lbl_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_1.Location = new System.Drawing.Point(28, 17);
            this.lbl_1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(419, 20);
            this.lbl_1.TabIndex = 0;
            this.lbl_1.Text = "label1";
            // 
            // tb_1
            // 
            this.tb_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_1.Location = new System.Drawing.Point(28, 51);
            this.tb_1.Margin = new System.Windows.Forms.Padding(30, 5, 30, 5);
            this.tb_1.Name = "tb_1";
            this.tb_1.Size = new System.Drawing.Size(409, 26);
            this.tb_1.TabIndex = 1;
            this.tb_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_1_KeyDown);
            // 
            // btn_2
            // 
            this.btn_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_2.Location = new System.Drawing.Point(325, 104);
            this.btn_2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_2.Name = "btn_2";
            this.btn_2.Size = new System.Drawing.Size(112, 32);
            this.btn_2.TabIndex = 4;
            this.btn_2.Text = "Cancel";
            this.btn_2.UseVisualStyleBackColor = true;
            this.btn_2.Click += new System.EventHandler(this.btn_2_Click);
            // 
            // btn_1
            // 
            this.btn_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_1.Location = new System.Drawing.Point(191, 104);
            this.btn_1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_1.Name = "btn_1";
            this.btn_1.Size = new System.Drawing.Size(112, 32);
            this.btn_1.TabIndex = 3;
            this.btn_1.Text = "Ok";
            this.btn_1.UseVisualStyleBackColor = true;
            this.btn_1.Click += new System.EventHandler(this.btn_1_Click);
            this.btn_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_1_KeyDown);
            // 
            // cb_1
            // 
            this.cb_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_1.FormattingEnabled = true;
            this.cb_1.Location = new System.Drawing.Point(28, 50);
            this.cb_1.Name = "cb_1";
            this.cb_1.Size = new System.Drawing.Size(409, 28);
            this.cb_1.TabIndex = 5;
            this.cb_1.SelectedIndexChanged += new System.EventHandler(this.cb_1_SelectedIndexChanged);
            this.cb_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_1_KeyDown);
            // 
            // InputForm
            // 
            this.AcceptButton = this.btn_1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_2;
            this.ClientSize = new System.Drawing.Size(476, 183);
            this.ControlBox = false;
            this.Controls.Add(this.cb_1);
            this.Controls.Add(this.btn_2);
            this.Controls.Add(this.btn_1);
            this.Controls.Add(this.lbl_1);
            this.Controls.Add(this.tb_1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "InputForm";
            this.Load += new System.EventHandler(this.InputForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputForm_KeyDown_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_1;
        private System.Windows.Forms.TextBox tb_1;
        private System.Windows.Forms.Button btn_2;
        private System.Windows.Forms.Button btn_1;
        private System.Windows.Forms.ComboBox cb_1;
    }
}