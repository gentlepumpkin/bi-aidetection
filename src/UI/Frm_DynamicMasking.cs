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

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            using (Frm_DynamicMaskingAdvanced frm = new Frm_DynamicMaskingAdvanced())
            {
                frm.cbEnableScaling.Checked = cam.maskManager.scaleConfig.isScaledObject;
                frm.numSmallObjMax.Value = cam.maskManager.scaleConfig.smallObjectMaxPercent;
                frm.numSmallObjPercent.Value = cam.maskManager.scaleConfig.smallObjectScalePercent;
                frm.numMidObjMin.Value = cam.maskManager.scaleConfig.mediumObjectMinPercent;
                frm.numMidObjMax.Value = cam.maskManager.scaleConfig.mediumObjectMaxPercent;
                frm.numMidObjPercent.Value = cam.maskManager.scaleConfig.mediumObjectScalePercent;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Int32.TryParse(frm.numSmallObjMax.Text, out int smallObjMax);
                    Int32.TryParse(frm.numSmallObjPercent.Text, out int smallObjPercent);
                    Int32.TryParse(frm.numMidObjMin.Text, out int midObjMin);
                    Int32.TryParse(frm.numMidObjMax.Text, out int midObjMax);
                    Int32.TryParse(frm.numMidObjPercent.Text, out int midObjPercent);

                    cam.maskManager.scaleConfig.isScaledObject = frm.cbEnableScaling.Checked;

                    cam.maskManager.scaleConfig.smallObjectMaxPercent = smallObjMax;
                    cam.maskManager.scaleConfig.smallObjectScalePercent = smallObjPercent;
                    cam.maskManager.scaleConfig.mediumObjectMinPercent = midObjMin;
                    cam.maskManager.scaleConfig.mediumObjectMaxPercent = midObjMax;
                    cam.maskManager.scaleConfig.mediumObjectScalePercent = midObjPercent;

                    AppSettings.Save();
                }
            }

        }
    }
}
