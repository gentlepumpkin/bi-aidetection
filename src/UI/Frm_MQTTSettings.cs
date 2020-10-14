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

        public Camera cam;

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
            btTest.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            try
            {
                AppSettings.Settings.mqtt_serverandport = tb_ServerPort.Text.Trim();
                AppSettings.Settings.mqtt_password = tb_Password.Text.Trim();
                AppSettings.Settings.mqtt_username = tb_Username.Text.Trim();
                AppSettings.Settings.mqtt_UseTLS = cb_UseTLS.Checked;

                using (Global_GUI.CursorWait cw = new Global_GUI.CursorWait())
                {
                    Global.Log("------ TESTING MQTT --------");


                    string topic = AITOOL.ReplaceParams(this.cam, null, null, tb_Topic.Text.Trim());
                    string payload = AITOOL.ReplaceParams(this.cam, null, null, tb_Payload.Text.Trim());

                    List<string> topics = Global.Split(topic, ";|");
                    List<string> payloads = Global.Split(payload, ";|");

                    MQTTClient mq = new MQTTClient();
                    MqttClientPublishResult pr = null;

                    for (int i = 0; i < topics.Count; i++)
                    {
                        pr = await mq.PublishAsync(topics[i], payloads[i], cam.Action_mqtt_retain_message);

                    }

                    Global.Log("------ DONE TESTING MQTT --------");

                    if (pr != null && (pr.ReasonCode == MqttClientPublishReasonCode.Success))
                    {
                        MessageBox.Show("Success! See Log for details.");
                    }
                    else if (pr != null)
                    {
                        MessageBox.Show($"Failed. See log for details. Reason={pr.ReasonCode}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Failed. See log for details.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }


            }
            catch
            {

            }
            finally
            {
                btTest.Enabled = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }

        }

        private void Frm_MQTTSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
