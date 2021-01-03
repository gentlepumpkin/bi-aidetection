using System;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_DynamicMasking : Form
    {
        public Camera cam;

        public Frm_DynamicMasking()
        {
            this.InitializeComponent();
        }

        private void Frm_DynamicMasking_Load(object sender, EventArgs e)
        {
            //CenterToParent();
        }


        private void num_history_mins_Leave(object sender, EventArgs e)
        {
            if (this.num_history_mins.Text == "")
            {
                this.num_history_mins.Text = this.num_history_mins.Value.ToString();
            }
        }

        private void num_mask_create_Leave(object sender, EventArgs e)
        {
            if (this.num_mask_create.Text == "")
            {
                this.num_mask_create.Text = this.num_mask_create.Value.ToString();
            }
        }

        private void num_mask_remove_Leave(object sender, EventArgs e)
        {
            if (this.num_mask_remove.Text == "")
            {
                this.num_mask_remove.Text = this.num_mask_remove.Value.ToString();
            }
        }

        private void num_percent_var_Leave(object sender, EventArgs e)
        {
            if (this.num_percent_var.Text == "")
            {
                this.num_percent_var.Text = this.num_percent_var.Value.ToString();
            }
        }
        private void numMaskThreshold_Leave(object sender, EventArgs e)
        {
            if (this.numMaskThreshold.Text == "")
            {
                this.numMaskThreshold.Text = this.numMaskThreshold.Value.ToString();
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
                frm.cbEnableScaling.Checked = this.cam.maskManager.ScaleConfig.IsScaledObject;
                frm.numSmallObjMax.Value = this.cam.maskManager.ScaleConfig.SmallObjectMaxPercent;
                frm.numSmallObjPercent.Value = this.cam.maskManager.ScaleConfig.SmallObjectMatchPercent;
                frm.numMidObjMin.Value = this.cam.maskManager.ScaleConfig.MediumObjectMinPercent;
                frm.numMidObjMax.Value = this.cam.maskManager.ScaleConfig.MediumObjectMaxPercent;
                frm.numMidObjPercent.Value = this.cam.maskManager.ScaleConfig.MediumObjectMatchPercent;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Int32.TryParse(frm.numSmallObjMax.Text, out int smallObjMax);
                    Int32.TryParse(frm.numSmallObjPercent.Text, out int smallObjPercent);
                    Int32.TryParse(frm.numMidObjMin.Text, out int midObjMin);
                    Int32.TryParse(frm.numMidObjMax.Text, out int midObjMax);
                    Int32.TryParse(frm.numMidObjPercent.Text, out int midObjPercent);

                    this.cam.maskManager.ScaleConfig.IsScaledObject = frm.cbEnableScaling.Checked;

                    this.cam.maskManager.ScaleConfig.SmallObjectMaxPercent = smallObjMax;
                    this.cam.maskManager.ScaleConfig.SmallObjectMatchPercent = smallObjPercent;
                    this.cam.maskManager.ScaleConfig.MediumObjectMinPercent = midObjMin;
                    this.cam.maskManager.ScaleConfig.MediumObjectMaxPercent = midObjMax;
                    this.cam.maskManager.ScaleConfig.MediumObjectMatchPercent = midObjPercent;

                    AppSettings.SaveAsync();
                }
            }

        }

        private void num_mask_remove_ValueChanged(object sender, EventArgs e)
        {

        }

        private void num_max_unused_Leave(object sender, EventArgs e)
        {
            if (this.num_max_unused.Text == "")
            {
                this.num_max_unused.Text = this.num_max_unused.Value.ToString();
            }
        }
    }
}
