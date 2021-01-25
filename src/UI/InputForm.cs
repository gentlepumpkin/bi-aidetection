using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AITool
{
    public partial class InputForm : Form
    {
        public string text = "";

        public InputForm(string label, string title, bool show_textbox = true, string text_OK = "Ok", string text_Cancel = "Cancel", string defaulttext = "", List<String> cbitems = null)
        {
            //updated to support defaults and combobox items

            this.InitializeComponent();
            this.lbl_1.Text = label;
            this.Text = title;
            this.btn_1.Text = text_OK;
            this.btn_2.Text = text_Cancel;

            if (show_textbox)
            {
                if (cbitems == null || cbitems.Count == 0)
                {
                    this.tb_1.Show();
                    this.tb_1.BringToFront();
                    this.tb_1.Text = defaulttext;
                    this.tb_1.Enabled = true;
                    this.cb_1.Enabled = false;
                }
                else
                {
                    this.cb_1.Show();
                    this.cb_1.BringToFront();
                    this.cb_1.Items.AddRange(cbitems.ToArray());
                    this.cb_1.Text = defaulttext;
                    this.tb_1.Enabled = false;
                    this.cb_1.Enabled = true;
                }
            }
            else
            {
                this.tb_1.Hide();
                this.cb_1.Hide();
            }
        }


        private void btn_2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.tb_1.Text))
            {
                this.text = this.tb_1.Text;
            }
            else if (!string.IsNullOrWhiteSpace(this.cb_1.Text))
            {
                this.text = this.cb_1.Text;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }



        private void InputForm_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(this.tb_1.Text))
                {
                    this.text = this.tb_1.Text;
                }
                else if (!string.IsNullOrWhiteSpace(this.cb_1.Text))
                {
                    this.text = this.cb_1.Text;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void tb_1_KeyDown(object sender, KeyEventArgs e)
        {
            //    if (e.KeyCode == Keys.Escape)
            //    {
            //        this.DialogResult = DialogResult.Cancel;
            //        this.Close();
            //    }
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        if (tb_1.Visible)
            //        {
            //            text = tb_1.Text;
            //        }
            //        else if (cb_1.Visible)
            //        {
            //            text = cb_1.Text;
            //        }
            //        this.DialogResult = DialogResult.OK;
            //        this.Close();
            //    }
        }

        private void btn_1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    this.DialogResult = DialogResult.Cancel;
            //    this.Close();
            //}
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (tb_1.Visible)
            //    {
            //        text = tb_1.Text;
            //    }
            //    else if (cb_1.Visible)
            //    {
            //        text = cb_1.Text;
            //    }
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();
            //}
        }

        private void InputForm_Load(object sender, EventArgs e)
        {

        }

        private void cb_1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cb_1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    this.DialogResult = DialogResult.Cancel;
            //    this.Close();
            //}
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (tb_1.Visible)
            //    {
            //        text = tb_1.Text;
            //    }
            //    else if (cb_1.Visible)
            //    {
            //        text = cb_1.Text;
            //    }
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();
            //}
        }
    }
}
