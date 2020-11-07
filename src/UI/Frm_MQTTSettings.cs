using MQTTnet.Client.Publishing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static AITool.AITOOL;

namespace AITool
{
    public partial class Frm_MQTTSettings : Form
    {

        public Camera cam;

        public Frm_MQTTSettings()
        {
            this.InitializeComponent();
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
            this.btTest.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;

            try
            {
                AppSettings.Settings.mqtt_serverandport = this.tb_ServerPort.Text.Trim();
                AppSettings.Settings.mqtt_password = this.tb_Password.Text.Trim();
                AppSettings.Settings.mqtt_username = this.tb_Username.Text.Trim();
                AppSettings.Settings.mqtt_UseTLS = this.cb_UseTLS.Checked;

                using (Global_GUI.CursorWait cw = new Global_GUI.CursorWait())
                {
                    Log("------ TESTING MQTT --------");


                    string topic = AITOOL.ReplaceParams(this.cam, null, null, this.tb_Topic.Text.Trim());
                    string payload = AITOOL.ReplaceParams(this.cam, null, null, this.tb_Payload.Text.Trim());

                    List<string> topics = Global.Split(topic, ";|");
                    List<string> payloads = Global.Split(payload, ";|");

                    MQTTClient mq = new MQTTClient();
                    MqttClientPublishResult pr = null;
                    ClsImageQueueItem CurImg = null;

                    if (this.cam.Action_mqtt_send_image)
                    {
                        if (!string.IsNullOrEmpty(this.cam.last_image_file_with_detections) && File.Exists(this.cam.last_image_file_with_detections))
                        {
                            CurImg = new ClsImageQueueItem(this.cam.last_image_file_with_detections, 0);
                        }
                        else if (!string.IsNullOrEmpty(this.cam.last_image_file) && File.Exists(this.cam.last_image_file))
                        {
                            CurImg = new ClsImageQueueItem(this.cam.last_image_file, 0);
                        }

                    }

                    for (int i = 0; i < topics.Count; i++)
                    {
                        pr = await mq.PublishAsync(topics[i], payloads[i], this.cam.Action_mqtt_retain_message, CurImg);

                    }

                    Log("------ DONE TESTING MQTT --------");

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
                this.btTest.Enabled = true;
                this.btnSave.Enabled = true;
                this.btnCancel.Enabled = true;
            }

        }

        private void Frm_MQTTSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
