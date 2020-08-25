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
    public partial class Frm_DynamicMasking:Form
    {
        public Frm_DynamicMasking()
        {
            InitializeComponent();
        }

        private void Frm_DynamicMasking_Load(object sender, EventArgs e)
        {

        }


        private void num_history_mins_Leave(object sender, EventArgs e)
        {
            if (num_history_mins.Text == "")
            {
                num_history_mins.Text = num_history_mins.Value.ToString();
            }
        }

        private void num_mask_create_Leave(object sender, EventArgs e)
        {
            if (num_mask_create.Text == "")
            {
                num_mask_create.Text = num_mask_create.Value.ToString();
            }
        }

        private void num_mask_remove_Leave(object sender, EventArgs e)
        {
            if (num_mask_remove.Text == "")
            {
                num_mask_remove.Text = num_mask_remove.Value.ToString();
            }
        }

        private void num_percent_var_Leave(object sender, EventArgs e)
        {
            if (num_percent_var.Text == "")
            {
                num_percent_var.Text = num_percent_var.Value.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
