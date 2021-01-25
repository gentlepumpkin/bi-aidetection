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

        ClsURLItem CurURL = null;

        public Frm_AIServers()
        {
            InitializeComponent();
        }

        private void Frm_AddAIServers_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);
            Global_GUI.ConfigureFOLV(FOLV_AIServers, typeof(ClsURLItem), null, this.imageList1);
            Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList);
        }

        private void Frm_AddAIServers_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }
               

        private void FOLV_AIServers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FOLV_AIServers_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.FOLV_AIServers.SelectedObjects != null && this.FOLV_AIServers.SelectedObjects.Count > 0)
                {
                    this.CurURL = (ClsURLItem)this.FOLV_AIServers.SelectedObjects[0];
                }
                else
                {
                    this.CurURL = null;
                }

            }
            catch (Exception ex)
            {
                AITOOL.Log($"Error: {Global.ExMsg(ex)}");

            }

            UpdateButtons();

        }

        private void UpdateButtons()
        {
            if (this.CurURL != null)
            {
                
                toolStripButtonDelete.Enabled = true;
                toolStripButtonDown.Enabled = true;
                toolStripButtonEdit.Enabled = true;
                toolStripButtonUp.Enabled = true;
            }
            else
            {
                toolStripButtonDelete.Enabled = false;
                toolStripButtonDown.Enabled = false;
                toolStripButtonEdit.Enabled = false;
                toolStripButtonUp.Enabled = false;
            }

        }

        private void deepstackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClsURLItem url = new ClsURLItem("", AppSettings.Settings.AIURLList.Count + 1, URLTypeEnum.DeepStack);
            //if (!AppSettings.Settings.AIURLList.Contains(url))
            //{
                this.CurURL = url;
                AppSettings.Settings.AIURLList.Add(url);
                Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList, UseSelected: true, SelectObject: this.CurURL, FullRefresh: true);
            //}
            //else
            //{
            //    MessageBox.Show("Already exists");
            //}
            UpdateButtons();

        }

        private void addAmazonReToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClsURLItem url = new ClsURLItem("", AppSettings.Settings.AIURLList.Count + 1, URLTypeEnum.AWSRekognition_Objects);
            if (!AppSettings.Settings.AIURLList.Contains(url))
            {
                this.CurURL = url;
                AppSettings.Settings.AIURLList.Add(url);
                Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList, UseSelected: true, SelectObject: this.CurURL, FullRefresh: true);
                AITOOL.UpdateAIURLs();

            }
            else
            {
                MessageBox.Show("Already exists");
            }
            UpdateButtons();

        }

        private void addDoodsServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClsURLItem url = new ClsURLItem("", AppSettings.Settings.AIURLList.Count + 1, URLTypeEnum.DOODS);
            if (!AppSettings.Settings.AIURLList.Contains(url))
            {
                this.CurURL = url;
                AppSettings.Settings.AIURLList.Add(url);
                Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList, UseSelected: true, SelectObject: this.CurURL, FullRefresh: true);
            }
            else
            {
                MessageBox.Show("Already exists");
            }
            UpdateButtons();

        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (AppSettings.Settings.AIURLList.Contains(this.CurURL))
            {
                AppSettings.Settings.AIURLList.Remove(this.CurURL);
                for (int i = 0; i < AppSettings.Settings.AIURLList.Count; i++)
                {
                    AppSettings.Settings.AIURLList[i].Order = i + 1;
                }
                Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList, UseSelected: true, FullRefresh: true);
            }
            UpdateButtons();

        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            Edit();


        }

        private void Edit()
        {
            using (Frm_AIServerDeepstackEdit frm = new Frm_AIServerDeepstackEdit())
            {
                frm.CurURL = this.CurURL;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.CurURL = frm.CurURL;  //this should update the list item by ref - prob not needed but makes me feel warm and fuzzy
                    Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList, UseSelected: true, SelectObject: this.CurURL, FullRefresh: true);
                }
            }
            UpdateButtons();
        }

        private void toolStripButtonUp_Click(object sender, EventArgs e)
        {
            if (this.CurURL == null)
                return;

            int idx = AppSettings.Settings.AIURLList.IndexOf(this.CurURL);

            if (idx > -1)
            {
                AppSettings.Settings.AIURLList.Move(idx, idx - 1);

                for (int i = 0; i < AppSettings.Settings.AIURLList.Count; i++)
                {
                    AppSettings.Settings.AIURLList[i].Order = i + 1;
                }

                Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList, UseSelected: true, SelectObject: this.CurURL, FullRefresh: true);

            }
        }

        private void toolStripButtonDown_Click(object sender, EventArgs e)
        {
            if (this.CurURL == null)
                return;

            int idx = AppSettings.Settings.AIURLList.IndexOf(this.CurURL);

            if (idx > -1)
            {
                AppSettings.Settings.AIURLList.Move(idx, idx + 1);

                for (int i = 0; i < AppSettings.Settings.AIURLList.Count; i++)
                {
                    AppSettings.Settings.AIURLList[i].Order = i + 1;
                }

                Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList, UseSelected: true, SelectObject: this.CurURL, FullRefresh: true);

            }
        }

        private void FOLV_AIServers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Edit();
        }

        private void FOLV_AIServers_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            FormatURLRow(sender, e);
        }

        private void FormatURLRow(object Sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            try
            {
                ClsURLItem url = (ClsURLItem)e.Model;

                // If SPI IsNot Nothing Then
                if (url.Enabled.ReadFullFence() && url.CurErrCount.ReadFullFence() == 0 && !url.UseAsRefinementServer)
                    e.Item.ForeColor = Color.Green;
                
                else if (url.Enabled.ReadFullFence() && url.CurErrCount.ReadFullFence() == 0 && url.UseAsRefinementServer)
                    e.Item.ForeColor = Color.DarkOrange;

                else if (url.Enabled.ReadFullFence() && url.CurErrCount.ReadFullFence() > 0)
                {
                    e.Item.ForeColor = Color.Black;
                    e.Item.BackColor = Color.Red;
                }
                else if (!url.Enabled.ReadFullFence())
                    e.Item.ForeColor = Color.Gray;
                else
                    e.Item.ForeColor = Color.Black;
            }


            catch (Exception)
            {
            }
            // Log("Error: " & ExMsg(ex))
            finally
            {
            }
        }

        private void toolStripSplitButtonAdd_ButtonClick(object sender, EventArgs e)
        {
            toolStripSplitButtonAdd.ShowDropDown();
        }

        private void sightHoundAIServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClsURLItem url = new ClsURLItem("", AppSettings.Settings.AIURLList.Count + 1, URLTypeEnum.SightHound_Vehicle);
            if (!AppSettings.Settings.AIURLList.Contains(url))
            {
                this.CurURL = url;
                AppSettings.Settings.AIURLList.Add(url);
                Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList, UseSelected: true, SelectObject: this.CurURL, FullRefresh: true);
                AITOOL.UpdateAIURLs();
            }
            else
            {
                MessageBox.Show("Already exists");
            }
            UpdateButtons();
        }

        private void sightHoundPersonAIServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClsURLItem url = new ClsURLItem("", AppSettings.Settings.AIURLList.Count + 1, URLTypeEnum.SightHound_Person);
            if (!AppSettings.Settings.AIURLList.Contains(url))
            {
                this.CurURL = url;
                AppSettings.Settings.AIURLList.Add(url);
                Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList, UseSelected: true, SelectObject: this.CurURL, FullRefresh: true);
                AITOOL.UpdateAIURLs();
            }
            else
            {
                MessageBox.Show("Already exists");
            }
            UpdateButtons();
        }

        private void addAmazonFaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClsURLItem url = new ClsURLItem("", AppSettings.Settings.AIURLList.Count + 1, URLTypeEnum.AWSRekognition_Faces);
            if (!AppSettings.Settings.AIURLList.Contains(url))
            {
                this.CurURL = url;
                AppSettings.Settings.AIURLList.Add(url);
                Global_GUI.UpdateFOLV(FOLV_AIServers, AppSettings.Settings.AIURLList, UseSelected: true, SelectObject: this.CurURL, FullRefresh: true);
                AITOOL.UpdateAIURLs();

            }
            else
            {
                MessageBox.Show("Already exists");
            }
            UpdateButtons();
        }
    }
}
