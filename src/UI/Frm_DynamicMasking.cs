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
        public Camera cam;

        public Frm_DynamicMasking()
        {
            InitializeComponent();
        }

        private void Frm_DynamicMasking_Load(object sender, EventArgs e)
        {
            //CenterToParent();
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
        private void numMaskThreshold_Leave(object sender, EventArgs e)
        {
            if (numMaskThreshold.Text == "")
            {
                numMaskThreshold.Text = numMaskThreshold.Value.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            using (Frm_DynamicMaskingAdvanced frm = new Frm_DynamicMaskingAdvanced())
            {
                frm.cbEnableScaling.Checked = cam.maskManager.ScaleConfig.IsScaledObject;
                frm.numSmallObjMax.Value = cam.maskManager.ScaleConfig.SmallObjectMaxPercent;
                frm.numSmallObjPercent.Value = cam.maskManager.ScaleConfig.SmallObjectMatchPercent;
                frm.numMidObjMin.Value = cam.maskManager.ScaleConfig.MediumObjectMinPercent;
                frm.numMidObjMax.Value = cam.maskManager.ScaleConfig.MediumObjectMaxPercent;
                frm.numMidObjPercent.Value = cam.maskManager.ScaleConfig.MediumObjectMatchPercent;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Int32.TryParse(frm.numSmallObjMax.Text, out int smallObjMax);
                    Int32.TryParse(frm.numSmallObjPercent.Text, out int smallObjPercent);
                    Int32.TryParse(frm.numMidObjMin.Text, out int midObjMin);
                    Int32.TryParse(frm.numMidObjMax.Text, out int midObjMax);
                    Int32.TryParse(frm.numMidObjPercent.Text, out int midObjPercent);

                    cam.maskManager.ScaleConfig.IsScaledObject = frm.cbEnableScaling.Checked;

                    cam.maskManager.ScaleConfig.SmallObjectMaxPercent = smallObjMax;
                    cam.maskManager.ScaleConfig.SmallObjectMatchPercent = smallObjPercent;
                    cam.maskManager.ScaleConfig.MediumObjectMinPercent = midObjMin;
                    cam.maskManager.ScaleConfig.MediumObjectMaxPercent = midObjMax;
                    cam.maskManager.ScaleConfig.MediumObjectMatchPercent = midObjPercent;

                    AppSettings.Save();
                }
            }

        }
    }
}
