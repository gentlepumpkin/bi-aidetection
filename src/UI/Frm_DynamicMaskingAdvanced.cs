using System;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_DynamicMaskingAdvanced : Form
    {
        public Frm_DynamicMaskingAdvanced()
        {
            this.InitializeComponent();
        }

        private void numSmallObjMax_Leave(object sender, EventArgs e)
        {
            if (this.numSmallObjMax.Text == "")
            {
                this.numSmallObjMax.Text = this.numSmallObjMax.Value.ToString();
            }
        }

        private void numSmallObjPercent_Leave(object sender, EventArgs e)
        {
            if (this.numSmallObjPercent.Text == "")
            {
                this.numSmallObjPercent.Text = this.numSmallObjPercent.Value.ToString();
            }
        }

        private void numMidObjMax_Leave(object sender, EventArgs e)
        {
            if (this.numMidObjMax.Text == "")
            {
                this.numMidObjMax.Text = this.numMidObjMax.Value.ToString();
            }
        }

        private void numMidObjPercent_Leave(object sender, EventArgs e)
        {
            if (this.numMidObjPercent.Text == "")
            {
                this.numMidObjPercent.Text = this.numMidObjPercent.Value.ToString();
            }
        }

        private void Frm_DynamicMaskingAdvanced_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
        }

        private void numSmallObjMax_ValueChanged(object sender, EventArgs e)
        {
            this.numMidObjMin.Minimum = this.numSmallObjMax.Value;

            if (this.numMidObjMin.Value < this.numSmallObjMax.Value)
            {
                this.numMidObjMin.Value = this.numSmallObjMax.Value;
            }

            this.numMidObjMax.Minimum = this.numMidObjMin.Minimum + 1;
        }

        private void numMidObjMin_ValueChanged(object sender, EventArgs e)
        {
            if (this.numMidObjMin.Value == this.numMidObjMax.Value)
            {
                if (this.numMidObjMin.Value > this.numMidObjMin.Minimum)
                {
                    this.numMidObjMin.Value = this.numMidObjMin.Value - 1;
                }
            }
        }

        private void numMidObjMin_Leave(object sender, EventArgs e)
        {
            if (this.numMidObjMin.Text == "")
            {
                this.numMidObjMin.Text = this.numMidObjMin.Value.ToString();
            }
        }
    }
}
