using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AITool
{
    public partial class InputForm : Form
    {
        public string text = ""; 

        public InputForm(string label, string title, bool show_textbox = true, string text_OK = "Ok", string text_Cancel = "Cancel", string defaulttext = "", List<String> cbitems = null)
        {
            //updated to support defaults and combobox items
            
            InitializeComponent();
            lbl_1.Text = label;
            this.Text = title;
            btn_1.Text = text_OK;
            btn_2.Text = text_Cancel;

            if (show_textbox)
            {
                if (cbitems == null || cbitems.Count() == 0)
                {
                    tb_1.Show();
                    tb_1.BringToFront();
                    tb_1.Text = defaulttext;
                    tb_1.Enabled = true;
                    cb_1.Enabled = false;
                }
                else
                {
                    cb_1.Show();
                    cb_1.BringToFront();
                    cb_1.Items.AddRange(cbitems.ToArray());
                    cb_1.Text = defaulttext;
                    tb_1.Enabled = false;
                    cb_1.Enabled = true;
                }
            }
            else
            {
                tb_1.Hide();
                cb_1.Hide();
            }
        }


        private void btn_2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tb_1.Text))
            {
                this.text = tb_1.Text;
            }
            else if (!string.IsNullOrWhiteSpace(cb_1.Text))
            {
                this.text = cb_1.Text;
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
                if (!string.IsNullOrWhiteSpace(tb_1.Text))
                {
                    this.text = tb_1.Text;
                }
                else if (!string.IsNullOrWhiteSpace(cb_1.Text))
                {
                    this.text = cb_1.Text;
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
