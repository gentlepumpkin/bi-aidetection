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
    public partial class Frm_Pause : Form
    {
        public Camera CurrentCam = null;

        public Frm_Pause()
        {
            InitializeComponent();
        }

        private void Frm_Pause_Load(object sender, EventArgs e)
        {
            this.cmb_cameras.Items.Add("All Cameras"); //add all cameras stats entry
            int i = 0;
            int oldidxstats = 0;
            foreach (Camera cam in AppSettings.Settings.CameraList)
            {
                this.cmb_cameras.Items.Add($"   {cam.Name}");
                if (this.CurrentCam.Name.EqualsIgnoreCase(cam.Name))
                    oldidxstats = i + 1;
                i++;

            }

            if (this.cmb_cameras.Items.Count > 0)
                this.cmb_cameras.SelectedIndex = oldidxstats;

            this.ShowCamera();

            Global_GUI.RestoreWindowState(this);
            timer1.Start();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateButton();
        }

        private void UpdateButton()
        {
            if (!this.CurrentCam.IsNull() && this.CurrentCam.Paused)
            {
                this.lbl_resumingtime.Text = $"Resuming in {(this.CurrentCam.ResumeTime - DateTime.Now).TotalMinutes.Round()} minutes...";
            }
            else
            {
                this.lbl_resumingtime.Text = "Not paused.";
            }

        }


        private void cmb_cameras_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowCamera();

        }

        public void ShowCamera()
        {
            if (cmb_cameras.Text.Trim().EqualsIgnoreCase("all cameras"))
                this.CurrentCam = AITOOL.GetCamera("default", true);
            else
                this.CurrentCam = AITOOL.GetCamera(cmb_cameras.Text.Trim());

            cb_Paused.Checked = this.CurrentCam.Paused;
            tb_minutes.Text = this.CurrentCam.PauseMinutes.ToString();
            cb_FileMonitoring.Checked = this.CurrentCam.PauseFileMon;
            cb_MQTT.Checked = this.CurrentCam.PauseMQTT;
            cb_Pushover.Checked = this.CurrentCam.PausePushover;
            cb_Telegram.Checked = this.CurrentCam.PauseTelegram;
            cb_URL.Checked = this.CurrentCam.PauseURL;

            UpdateButton();

        }

        private void SaveCamera()
        {

            if (this.cmb_cameras.Text.Trim().EqualsIgnoreCase("All Cameras"))
            {
                foreach (var cam in AppSettings.Settings.CameraList)
                {
                    cam.PauseMinutes = tb_minutes.Text.ToDouble();
                    cam.PauseFileMon = cb_FileMonitoring.Checked;
                    cam.PauseMQTT = cb_MQTT.Checked;
                    cam.PausePushover = cb_Pushover.Checked;
                    cam.PauseTelegram = cb_Telegram.Checked;
                    cam.PauseURL = cb_URL.Checked;
                    if (cam.Paused && !cb_Paused.Checked)
                    {
                       cam.Resume();
                    }
                    else if (!cam.Paused && cb_Paused.Checked)
                    {
                        cam.Pause();
                    }
                }
            }
            else
            {
                this.CurrentCam.PauseMinutes = tb_minutes.Text.ToDouble();
                this.CurrentCam.PauseFileMon = cb_FileMonitoring.Checked;
                this.CurrentCam.PauseMQTT = cb_MQTT.Checked;
                this.CurrentCam.PausePushover = cb_Pushover.Checked;
                this.CurrentCam.PauseTelegram = cb_Telegram.Checked;
                this.CurrentCam.PauseURL = cb_URL.Checked;
                if (this.CurrentCam.Paused && !cb_Paused.Checked)
                {
                    this.CurrentCam.Resume();
                }
                else if (!this.CurrentCam.Paused && cb_Paused.Checked)
                {
                    this.CurrentCam.Pause();
                }
            }

            UpdateButton();

        }

        private void bt_pause_Click(object sender, EventArgs e)
        {


        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            SaveCamera();
        }

        private void Frm_Pause_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);

        }
    }
}
