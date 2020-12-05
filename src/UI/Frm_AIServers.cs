using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_AIServers : Form
    {
        public Frm_AIServers()
        {
            InitializeComponent();
        }

        private void Frm_AddAIServers_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);
            Global_GUI.ConfigureFOLV(FOLV_AIServers, typeof(ClsURLItem), null, null);
            Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList);
        }

        private void Frm_AddAIServers_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void ll_deepstack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://ipcamtalk.com/threads/tool-tutorial-free-ai-person-detection-for-blue-iris.37330/");
        }

        private void ll_doods_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/snowzach/doods");
        }

        private void ll_amazon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://docs.aws.amazon.com/rekognition/latest/dg/setting-up.html");
        }
    }
}
