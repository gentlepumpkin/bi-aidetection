using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class InputForm : Form
    {
        public string text = ""; 

        public InputForm(string label, string title, bool show_textbox)
        {
            InitializeComponent();
            lbl_1.Text = label;
            this.Text = title;
            if (show_textbox)
            {
                tb_1.Show();
            }
            else
            {
                tb_1.Hide();
            }
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            text = tb_1.Text;
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
                text = tb_1.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void tb_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                text = tb_1.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btn_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                text = tb_1.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
