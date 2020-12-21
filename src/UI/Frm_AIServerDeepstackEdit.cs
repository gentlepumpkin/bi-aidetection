using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_AIServerDeepstackEdit : Form
    {
        public ClsURLItem CurURL;

        public Frm_AIServerDeepstackEdit()
        {
            InitializeComponent();
        }

        private void Frm_AIServerDeepstackEdit_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);

            this.tb_URL.Text = this.CurURL.url;
            this.lbl_type.Text = this.CurURL.Type.ToString();

            if (this.CurURL.Type == URLTypeEnum.AWSRekognition)
                this.tb_URL.Enabled = false;
            else
                this.tb_URL.Enabled = true;

            this.tb_ActiveTimeRange.Text = this.CurURL.ActiveTimeRange;
            this.chk_Enabled.Checked = this.CurURL.Enabled.ReadFullFence();
            this.tb_ApplyToCams.Text = this.CurURL.Cameras;
            this.tb_ImagesPerMonth.Text = this.CurURL.MaxImagesPerMonth.ToString();
            this.cb_ImageAdjustProfile.Text = this.CurURL.ImageAdjustProfile;
            this.linkHelpURL.Text = this.CurURL.HelpURL;

            foreach (ClsImageAdjust ia in AppSettings.Settings.ImageAdjustProfiles)
            {
                this.cb_ImageAdjustProfile.Items.Add(ia.Name);
            }

            this.cb_ImageAdjustProfile.SelectedIndex = this.cb_ImageAdjustProfile.Items.IndexOf(this.CurURL.DefaultURL);

        }

        private void Frm_AIServerDeepstackEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        
        private void tb_URL_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void ValidateForm()
        {
            this.CurURL.url = tb_URL.Text.Trim();
            if (!this.CurURL.UpdateIsValid())
            {
                tb_URL.ForeColor = Color.White;
                tb_URL.BackColor = Color.Red;
                bt_Save.Enabled = false;
                btTest.Enabled = false;

            }
            else
            {
                if (this.CurURL.UrlFixed)
                    tb_URL.Text = this.CurURL.url;  //not sure if this is the right thing to do here...  May interfere with peoples typing

                tb_URL.ResetBackColor();
                tb_URL.ResetForeColor();
                bt_Save.Enabled = true;
                btTest.Enabled = true;
            }
        }

        private void bt_Save_Click(object sender, EventArgs e)
        {
            this.Update();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Update()
        {
            this.CurURL.url = this.tb_URL.Text.Trim();
            this.CurURL.ActiveTimeRange = this.tb_ActiveTimeRange.Text.Trim();
            this.CurURL.Enabled.WriteFullFence(this.chk_Enabled.Checked);
            this.CurURL.Cameras = this.tb_ApplyToCams.Text.Trim();
            this.CurURL.ImageAdjustProfile = this.cb_ImageAdjustProfile.Text;
            this.CurURL.MaxImagesPerMonth = Convert.ToInt32(this.tb_ImagesPerMonth.Text.Trim());
        }
        private void linkHelpURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkHelpURL.Text);
        }

        private void btn_ImageAdjustEdit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet.");
        }

        private async void btTest_Click(object sender, EventArgs e)
        {
            string pth = Global.GetSetting("TestImage", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImage.jpg"));

            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = Path.GetDirectoryName(pth),
                FileName = Path.GetFileName(pth),
                Title = "Select test image",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,
                ShowReadOnly = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pth = ofd.FileName;
                Global.SaveSetting("TestImage", pth);

                if (File.Exists(pth))
                {
                    //must create a temp unique file every time because database key is the filename
                    string tpth = Path.Combine(Path.GetTempPath(), $"TEST_CAM.{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}");
                    File.Copy(pth, tpth, true);

                    btTest.Enabled = false;
                    bt_Save.Enabled = false;
                    btTest.Text = "Working...";
                    this.Update();
                    ClsImageQueueItem CurImg = new ClsImageQueueItem(tpth, 0);
                    Camera cam = new Camera("TEST_CAM");
                    bool ret = await AITOOL.DetectObjects(CurImg, this.CurURL, cam);
                    btTest.Enabled = true;
                    bt_Save.Enabled = true;
                    btTest.Text = "Test";
                    if (ret)
                    {
                        this.CurURL.ErrCount.WriteUnfenced(0);
                        this.CurURL.CurErrCount.WriteUnfenced(0);
                        MessageBox.Show($"Success! {this.CurURL.LastResultMessage}", "Success");
                    }
                    else
                        MessageBox.Show($"Error! {this.CurURL.LastResultMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    MessageBox.Show($"Test file does not exist:\r\n{pth}");
                }

            }

        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            this.CurURL.ErrCount.WriteUnfenced(0);
            this.CurURL.CurErrCount.WriteUnfenced(0);
            this.CurURL.AITimeCalcs = new MovingCalcs(250, "Images", true);
            this.CurURL.LastResultMessage = "";
            this.CurURL.LastTimeMS = 0;
            this.CurURL.LastUsedTime = DateTime.MinValue;

        }
    }
}
