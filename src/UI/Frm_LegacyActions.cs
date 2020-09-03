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
    public partial class Frm_LegacyActions:Form
    {
        public Frm_LegacyActions()
        {
            InitializeComponent();
        }

        private void Frm_LegacyActions_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);
        }

        private void Frm_LegacyActions_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        
    }
}
