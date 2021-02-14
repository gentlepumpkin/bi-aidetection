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
    public partial class Frm_PredSizeLimits : Form
    {
        public Frm_PredSizeLimits()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Frm_PredSizeLimits_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);
        }

        private void Frm_PredSizeLimits_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }
    }
}
