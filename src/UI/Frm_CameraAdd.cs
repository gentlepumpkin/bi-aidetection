using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_CameraAdd : Form
    {
        public Frm_CameraAdd()
        {
            this.InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            UpdateCamList(true);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void checkedListBoxCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCamList(false);
        }

        private void Frm_CameraAdd_Load(object sender, EventArgs e)
        {
            UpdateCamList(false);
        }

        private void UpdateCamList(bool keeptextcontents)
        {
            List<string> cams = new List<string>();

            if (keeptextcontents)
                cams = Global.Split(tb_Cameras.Text, "\r\n|;,");

            foreach (object itm in this.checkedListBoxCameras.CheckedItems)
            {
                if (!cams.Contains(itm.ToString(), StringComparer.OrdinalIgnoreCase))
                    cams.Add(itm.ToString());

            }

            tb_Cameras.Text = string.Join(",", cams);

        }

        private void checkedListBoxCameras_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateCamList(false);
        }

        private void checkedListBoxCameras_Leave(object sender, EventArgs e)
        {
            UpdateCamList(false);

        }

        private void checkedListBoxCameras_Validated(object sender, EventArgs e)
        {
            UpdateCamList(false);
        }

        private void tb_Cameras_Leave(object sender, EventArgs e)
        {
            UpdateCamList(true);
        }
    }
}
