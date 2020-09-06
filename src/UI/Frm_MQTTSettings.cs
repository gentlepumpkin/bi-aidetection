using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MQTTnet.Client.Publishing;

namespace AITool
{
    public partial class Frm_MQTTSettings:Form
    {
        public Frm_MQTTSettings()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btTest_ClickAsync(object sender, EventArgs e)
        {
            AppSettings.Settings.mqtt_serverandport = tb_ServerPort.Text.Trim();
            AppSettings.Settings.mqtt_password = tb_Password.Text.Trim();
            AppSettings.Settings.mqtt_username = tb_Username.Text.Trim();
            AppSettings.Settings.mqtt_UseTLS = cb_UseTLS.Checked;

            MQTTClient mq = new MQTTClient();

            MqttClientPublishResult pr = await mq.PublishAsync(tb_Topic.Text.Trim(),tb_Payload.Text.Trim());

            if (pr != null && (pr.ReasonCode == MqttClientPublishReasonCode.Success))
            {
                MessageBox.Show("Success.");
            }
            else if (pr != null)
            {
                MessageBox.Show($"Failed. See log. Reason={pr.ReasonCode}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"Failed. See log.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
