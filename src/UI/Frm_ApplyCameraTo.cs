using System;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_ApplyCameraTo : Form
    {
        public Frm_ApplyCameraTo()
        {
            this.InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
