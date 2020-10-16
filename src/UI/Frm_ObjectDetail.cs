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

        private void folv_ObjectDetail_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            FormatRow(sender, e);
        }

        private async void FormatRow(object Sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            try
            {
                ClsPrediction OP = (ClsPrediction)e.Model;

                // If SPI IsNot Nothing Then
                if (OP.Result == ResultType.Relevant)
                    e.Item.ForeColor = AppSettings.Settings.RectRelevantColor;
                else if (OP.Result == ResultType.DynamicMasked || OP.Result == ResultType.ImageMasked || OP.Result == ResultType.StaticMasked)
                    e.Item.ForeColor = AppSettings.Settings.RectMaskedColor;
                else if (OP.Result == ResultType.Error)
                {
                    e.Item.ForeColor = Color.Black;
                    e.Item.BackColor = Color.Red;
                }
                else
                    e.Item.ForeColor = AppSettings.Settings.RectIrrelevantColor;
            }



            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        private void createStaticMasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet.");

            //if (folv_ObjectDetail.SelectedObjects != null && folv_ObjectDetail.SelectedObjects.Count > 0)
            //{
            //    foreach (ClsPrediction CP in folv_ObjectDetail.SelectedObjects)
            //    {
            //        Object imageObject = new Object();
            //        imageObject.
            //        ObjectPosition OP = new ObjectPosition() 
            //    }
            //}
        }
    }
}
