using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_Errors : Form
    {
        public List<ClsLogItm> errors = null;
        public Frm_Errors()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(AITOOL.LogMan.GetCurrentLogFileName()))
            {
                System.Diagnostics.Process.Start(AITOOL.LogMan.GetCurrentLogFileName());
                //this.lbl_errors.Text = "";
            }
            else
            {
                MessageBox.Show("log missing");
            }

        }

        private void Frm_Errors_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);

            this.Show();

            try
            {
                Global_GUI.ConfigureFOLV(this.folv_errors, typeof(ClsLogItm), null, null, "Time", SortOrder.Descending);

                Global_GUI.UpdateFOLV(this.folv_errors, this.errors);

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void Frm_Errors_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
