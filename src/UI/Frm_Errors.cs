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
    public partial class Frm_Errors:Form
    {
        public List<ClsDetailItm> errors = null;
        public Frm_Errors()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(AppSettings.Settings.LogFileName))
            {
                System.Diagnostics.Process.Start(AppSettings.Settings.LogFileName);
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
                Global_GUI.ConfigureFOLV(folv_errors, typeof(ClsDetailItm), null, null, "Time", SortOrder.Descending);

                Global_GUI.UpdateFOLV_add(folv_errors, errors);

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
