namespace AITool
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_2 = new System.Windows.Forms.Button();
            this.btn_1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_1
            // 
            this.lbl_1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_1.AutoSize = true;
            this.lbl_1.Location = new System.Drawing.Point(170, 26);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(35, 13);
            this.lbl_1.TabIndex = 0;
            this.lbl_1.Text = "label1";
            // 
            // tb_1
            // 
            this.tb_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_1.Location = new System.Drawing.Point(20, 87);
            this.tb_1.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.tb_1.Name = "tb_1";
            this.tb_1.Size = new System.Drawing.Size(335, 20);
            this.tb_1.TabIndex = 1;
            this.tb_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_1_KeyDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(375, 218);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btn_2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 133);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(369, 82);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btn_2
            // 
            this.btn_2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_2.Location = new System.Drawing.Point(239, 29);
            this.btn_2.Name = "btn_2";
            this.btn_2.Size = new System.Drawing.Size(75, 23);
            this.btn_2.TabIndex = 4;
            this.btn_2.Text = "Cancel";
            this.btn_2.UseVisualStyleBackColor = true;
            this.btn_2.Click += new System.EventHandler(this.btn_2_Click);
            // 
            // btn_1
            // 
            this.btn_1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_1.Location = new System.Drawing.Point(54, 29);
            this.btn_1.Name = "btn_1";
            this.btn_1.Size = new System.Drawing.Size(75, 23);
            this.btn_1.TabIndex = 3;
            this.btn_1.Text = "Ok";
            this.btn_1.UseVisualStyleBackColor = true;
            this.btn_1.Click += new System.EventHandler(this.btn_1_Click);
            this.btn_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_1_KeyDown);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 218);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "InputForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputForm_KeyDown_1);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_1;
        private System.Windows.Forms.TextBox tb_1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_2;
        private System.Windows.Forms.Button btn_1;
    }
}