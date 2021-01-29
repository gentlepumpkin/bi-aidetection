using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static AITool.AITOOL;

namespace AITool
{
    public partial class Frm_LegacyActions : Form
    {
        public Camera cam;

        public Frm_LegacyActions()
        {
            this.InitializeComponent();
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

                frm.cam = this.cam;

                frm.tb_ServerPort.Text = AppSettings.Settings.mqtt_serverandport;
                frm.cb_UseTLS.Checked = AppSettings.Settings.mqtt_UseTLS;
                frm.tb_Password.Text = AppSettings.Settings.mqtt_password;
                frm.tb_Username.Text = AppSettings.Settings.mqtt_username;

                frm.tb_Topic.Text = this.tb_MQTT_Topic.Text.Trim();
                frm.tb_Payload.Text = this.tb_MQTT_Payload.Text.Trim();

                if (frm.ShowDialog() == DialogResult.OK)
                {

                    AppSettings.Settings.mqtt_UseTLS = frm.cb_UseTLS.Checked;
                    AppSettings.Settings.mqtt_username = frm.tb_Username.Text.Trim();
                    AppSettings.Settings.mqtt_serverandport = frm.tb_ServerPort.Text.Trim();
                    AppSettings.Settings.mqtt_password = frm.tb_Password.Text.Trim();

                    this.tb_MQTT_Payload.Text = frm.tb_Payload.Text.Trim();
                    this.tb_MQTT_Topic.Text = frm.tb_Topic.Text.Trim();


                    AppSettings.SaveAsync();

                }
            }
        }

        private async void btTest_Click(object sender, EventArgs e)
        {
            this.btnCancel.Enabled = false;
            this.btnSave.Enabled = false;
            this.btTest.Enabled = false;
            try
            {
                using (Global_GUI.CursorWait cw = new Global_GUI.CursorWait())
                {
                    Log("----------------------- TESTING TRIGGERS ----------------------------");

                    if (!string.IsNullOrEmpty(this.cam.last_image_file_with_detections) && File.Exists(this.cam.last_image_file_with_detections))
                    {
                        //test by copying the file as a new file into the watched folder'
                        string folder = Path.GetDirectoryName(this.cam.last_image_file_with_detections);
                        string filename = Path.GetFileNameWithoutExtension(this.cam.last_image_file_with_detections);
                        string ext = Path.GetExtension(this.cam.last_image_file_with_detections);
                        string testfile = Path.Combine(folder, $"{filename}_AITOOLTEST_{DateTime.Now.TimeOfDay.TotalSeconds}{ext}");
                        File.Copy(this.cam.last_image_file_with_detections, testfile, true);
                        string str = "Created test image file based on last detected object for the camera: " + testfile;
                        Log(str);
                        MessageBox.Show(str, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //do a generic test of the trigger
                        //bool result = await AITOOL.Trigger(cam, null, true);
                        bool result = await AITOOL.TriggerActionQueue.AddTriggerActionAsync(TriggerType.All, this.cam, null, null, true, true, null, "");

                        if (result)
                        {
                            MessageBox.Show($"Succeeded! See log for details.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Failed. See log for details.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                    Log("---------------------- DONE TESTING TRIGGERS -------------------------");
                }
            }
            catch { }
            finally
            {
                this.btnCancel.Enabled = true;
                this.btnSave.Enabled = true;
                this.btTest.Enabled = true;

            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(AITOOL.ReplaceParams(this.cam, null, null, this.label3.Text));
        }

        private void cb_mergeannotations_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_mergeannotations.Checked)
                this.tb_jpeg_merge_quality.Enabled = true;
            else
                this.tb_jpeg_merge_quality.Enabled = false;

        }

        private void cb_Pushover_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            Global_GUI.GroupboxEnableDisable(groupBoxPushover, (CheckBox)sender);
        }

        private void cb_telegram_CheckedChanged(object sender, EventArgs e)
        {
            Global_GUI.GroupboxEnableDisable(groupBoxTelegram, (CheckBox)sender);
        }

        private void cb_MQTT_enabled_CheckedChanged(object sender, EventArgs e)
        {
            Global_GUI.GroupboxEnableDisable(groupBoxMQTT, (CheckBox)sender);
        }

        private void cb_PlaySound_CheckedChanged(object sender, EventArgs e)
        {
            tb_Sounds.Enabled = cb_PlaySound.Checked;
        }

        private void cb_RunProgram_CheckedChanged(object sender, EventArgs e)
        {
            tb_RunExternalProgram.Enabled = cb_RunProgram.Checked;
            tb_RunExternalProgramArgs.Enabled = cb_RunProgram.Checked;
        }

        private void cb_copyAlertImages_CheckedChanged(object sender, EventArgs e)
        {
            tb_network_folder.Enabled = cb_copyAlertImages.Checked;
            tb_network_folder_filename.Enabled = cb_copyAlertImages.Checked;
        }

        private void tb_DetectionFormat_TextChanged(object sender, EventArgs e)
        {
            Updateformat();
        }

        private void tb_ConfidenceFormat_TextChanged(object sender, EventArgs e)
        {
            Updateformat();
        }

        private void Updateformat()
        {
            try
            {
                lbl_Confidence.Text = string.Format(tb_ConfidenceFormat.Text.Trim(), 99.123);
                if (!string.IsNullOrEmpty(tb_ConfidenceFormat.Text.Trim()))
                    AppSettings.Settings.DisplayPercentageFormat = tb_ConfidenceFormat.Text.Trim();

                lbl_Confidence.ForeColor = Color.DarkGreen;
                lbl_DetectionFormat.Text = AITOOL.ReplaceParams(cam, null, null, tb_DetectionFormat.Text);
                lbl_DetectionFormat.Text = lbl_DetectionFormat.Text.Replace("[]", "").Replace("()", "").Replace("   ", " ").Replace("  ", " ");
            }
            catch (Exception)
            {
                lbl_Confidence.ForeColor = Color.Red;
            }
        }

        private void bt_variables_Click(object sender, EventArgs e)
        {
            try
            {
                string vars = "[Camera];[Prefix];[CamInputFolder];[InputFolder];[ImagePath];[ImagePathEscaped];[ImageFilename];[ImageFilenameNoExt];[Username];[Password];[BlueIrisServerIP];[BlueIrisURL];[SummaryNonEscaped];[Summary];[Detection];[Label];[Detail];[Result];[Position];[Confidence];[Detections];[Confidences];%DATE%;%TIME%;%DATETIME%;%TEMP%;%APPDATA%;%USERPROFILE%;%USERNAME%";
                List<string> varlist = Global.Split(vars, ";");
                List<ClsProp> props = new List<ClsProp>();
                foreach (var varitm in varlist)
                {
                    string value = AITOOL.ReplaceParams(cam, null, null, varitm);
                    props.Add(new ClsProp(varitm, value));
                }
                using (Frm_Variables frm = new Frm_Variables())
                {
                    frm.props = props;
                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void lbl_DetectionFormat_Click(object sender, EventArgs e)
        {

        }
    }
}
