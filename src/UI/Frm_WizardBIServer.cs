using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static AITool.AITOOL;

namespace AITool
{
    public partial class Frm_WizardBIServer : Form
    {
        public bool Valid = false;

        public Frm_WizardBIServer()
        {
            InitializeComponent();
        }

        private void Frm_WizardBIServer_Load(object sender, EventArgs e)
        {
            this.txt_BlueIrisServer.Text = BlueIrisInfo.ServerName;
            this.lblURL.Text = BlueIrisInfo.URL;
        }

        private async void bt_validate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_BlueIrisServer.Text))
            {
                lblStatus.Text = "Error: Select a server!";
                return;
            }

            BlueIris bi = new BlueIris();

            lblStatus.Text = "Validating...";

            BlueIrisResult bir = await bi.RefreshBIInfoAsync(this.txt_BlueIrisServer.Text);

            if (bir == BlueIrisResult.Valid)
            {
                lblStatus.ForeColor = Color.DodgerBlue;
                lblStatus.Text = $"Validated.  Result={bir}.  {bi.ClipPaths.Count} clip paths, {bi.Users.Count} users, {bi.Cameras} cameras.";
                BlueIrisInfo = bi;
                this.Valid = true;
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = $"Error: Result={bir}.  See log for more details.";
                this.Valid = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
