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
    public partial class Frm_ObjectDetail:Form
    {
        public List<ClsPrediction> PredictionObjectDetail = null;
        public Frm_ObjectDetail()
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

        private void Frm_ObjectDetail_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);

            this.Show();

            try
            {
                Global_GUI.ConfigureFOLV(folv_ObjectDetail, typeof(ClsPrediction), null, null);

                Global_GUI.UpdateFOLV_add(folv_ObjectDetail, PredictionObjectDetail);

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void Frm_ObjectDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        
    }
}
