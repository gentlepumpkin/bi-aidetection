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
    public partial class Frm_LegacyActions:Form
    {
        public Frm_LegacyActions()
        {
            InitializeComponent();
        }

        private void Frm_LegacyActions_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);
        }

        private void Frm_LegacyActions_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void linkLabelMqttSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Frm_MQTTSettings frm = new Frm_MQTTSettings())
            {

                frm.tb_ServerPort.Text = AppSettings.Settings.mqtt_serverandport;
                frm.cb_UseTLS.Checked = AppSettings.Settings.mqtt_UseTLS;
                frm.tb_Password.Text = AppSettings.Settings.mqtt_password;
                frm.tb_Username.Text = AppSettings.Settings.mqtt_username;

                frm.tb_Topic.Text = tb_MQTT_Topic.Text.Trim();
                frm.tb_Payload.Text = tb_MQTT_Payload.Text.Trim();

                if (frm.ShowDialog() == DialogResult.OK)
                {

                    AppSettings.Settings.mqtt_UseTLS = frm.cb_UseTLS.Checked;
                    AppSettings.Settings.mqtt_username = frm.tb_Username.Text.Trim();
                    AppSettings.Settings.mqtt_serverandport = frm.tb_ServerPort.Text.Trim();
                    AppSettings.Settings.mqtt_password = frm.tb_Password.Text.Trim();

                    this.tb_MQTT_Payload.Text = frm.tb_Payload.Text.Trim();
                    this.tb_MQTT_Topic.Text = frm.tb_Topic.Text.Trim();

                    AppSettings.Save();

                }
            }
        }
    }
}
