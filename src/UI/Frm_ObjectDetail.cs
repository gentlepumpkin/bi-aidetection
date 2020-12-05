﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static AITool.AITOOL;

namespace AITool
{
    public partial class Frm_ObjectDetail : Form
    {
        public List<ClsPrediction> PredictionObjectDetail = null;
        public Frm_ObjectDetail()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void Frm_ObjectDetail_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);

            this.Show();

            try
            {
                Global_GUI.ConfigureFOLV(this.folv_ObjectDetail, typeof(ClsPrediction), null, null);

                Global_GUI.UpdateFOLV(this.folv_ObjectDetail, this.PredictionObjectDetail);

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void Frm_ObjectDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void folv_ObjectDetail_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            this.FormatRow(sender, e);
        }

        private async void FormatRow(object Sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            try
            {
                ClsPrediction OP = (ClsPrediction)e.Model;

                // If SPI IsNot Nothing Then
                if (OP.Result == ResultType.Relevant)
                    e.Item.ForeColor = AppSettings.Settings.RectRelevantColor;
                else if (OP.Result == ResultType.DynamicMasked || OP.Result == ResultType.ImageMasked || OP.Result == ResultType.StaticMasked)
                    e.Item.ForeColor = AppSettings.Settings.RectMaskedColor;
                else if (OP.Result == ResultType.Error)
                {
                    e.Item.ForeColor = Color.Black;
                    e.Item.BackColor = Color.Red;
                }
                else
                    e.Item.ForeColor = AppSettings.Settings.RectIrrelevantColor;
            }



            catch (Exception)
            {
            }
            finally
            {
            }
        }

        private void createStaticMasksToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int cnt = 0;
            if (this.folv_ObjectDetail.SelectedObjects != null && this.folv_ObjectDetail.SelectedObjects.Count > 0)
            {
                foreach (ClsPrediction CP in this.folv_ObjectDetail.SelectedObjects)
                {
                    if (string.IsNullOrEmpty(CP.Camera))
                        Log("Error: Can only add newer history prediction items that include cameraname, imagewidth, imageheight.");
                    else
                    {
                        ObjectPosition OP = new ObjectPosition(CP.XMin, CP.XMax, CP.YMin, CP.YMax, CP.Label, CP.ImageHeight, CP.ImageWidth, CP.Camera, CP.Filename);
                        Camera cam = GetCamera(CP.Camera);
                        cam.maskManager.CreateDynamicMask(OP, true);
                        cnt++;
                    }

                }
            }
            Log($"Added/updated {cnt} masks.");
        }
    }
}
