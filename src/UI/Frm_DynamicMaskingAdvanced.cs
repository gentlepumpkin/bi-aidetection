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
    public partial class Frm_DynamicMaskingAdvanced : Form
    {
        public Frm_DynamicMaskingAdvanced()
        {
            InitializeComponent();
        }

        private void numSmallObjMax_Leave(object sender, EventArgs e)
        {
            if (numSmallObjMax.Text == "")
            {
                numSmallObjMax.Text = numSmallObjMax.Value.ToString();
            }
        }

        private void numSmallObjPercent_Leave(object sender, EventArgs e)
        {
            if (numSmallObjPercent.Text == "")
            {
                numSmallObjPercent.Text = numSmallObjPercent.Value.ToString();
            }
        }

        private void numMidObjMax_Leave(object sender, EventArgs e)
        {
            if (numMidObjMax.Text == "")
            {
                numMidObjMax.Text = numMidObjMax.Value.ToString();
            }
        }

        private void numMidObjPercent_Leave(object sender, EventArgs e)
        {
            if (numMidObjPercent.Text == "")
            {
                numMidObjPercent.Text = numMidObjPercent.Value.ToString();
            }
        }

        private void Frm_DynamicMaskingAdvanced_Load(object sender, EventArgs e)
        {
            CenterToParent();
        }

        private void numSmallObjMax_ValueChanged(object sender, EventArgs e)
        {
            numMidObjMin.Minimum = numSmallObjMax.Value;
           
            if(numMidObjMin.Value < numSmallObjMax.Value)
            {
                numMidObjMin.Value = numSmallObjMax.Value;
            }

            numMidObjMax.Minimum = numMidObjMin.Minimum + 1;
        }

        private void numMidObjMin_ValueChanged(object sender, EventArgs e)
        {
            if (numMidObjMin.Value == numMidObjMax.Value)
            {
                if (numMidObjMin.Value > numMidObjMin.Minimum)
                {
                    numMidObjMin.Value = numMidObjMin.Value - 1;
                }
            }
        }

        private void numMidObjMin_Leave(object sender, EventArgs e)
        {
            if (numMidObjMin.Text == "")
            {
                numMidObjMin.Text = numMidObjMin.Value.ToString();
            }
        }
    }
}
    