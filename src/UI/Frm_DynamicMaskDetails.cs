using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using static AITool.AITOOL;

namespace AITool
{
    public partial class Frm_DynamicMaskDetails : Form
    {

        public Camera cam;
        private List<ObjectPosition> CurObjPosLst = new List<ObjectPosition>();
        private ObjectPosition contextMenuPosObj;
        private bool loading = false;

        public string GetBestImage()
        {
            //try to prioritize the images from actual detections first
            //goal is to always try to have an image to display
            // IS THIS OVERKILL OR CONFUSING TO USER???

            try
            {
                string lastfolder = "";

                if (this.contextMenuPosObj != null && !string.IsNullOrEmpty(this.contextMenuPosObj.ImagePath))
                {
                    lastfolder = Path.GetDirectoryName(this.contextMenuPosObj.ImagePath);
                    if (File.Exists(this.contextMenuPosObj.ImagePath))
                    {
                        Log($" (Found image from ACTUAL detected object: {this.contextMenuPosObj.ImagePath})");
                        return this.contextMenuPosObj.ImagePath;
                    }
                    else
                    {
                        Log("debug: Mask image file not found at location: " + this.contextMenuPosObj.ImagePath + ". Defaulting to last processed image");
                    }
                }
                else
                {
                    Log("debug: Mask image file path was blank or NULL. Defaulting to last processed image");

                }

                //See if we have an image stored that had ANY detections and use it
                if (this.cam != null)
                {
                    if (!string.IsNullOrEmpty(this.cam.last_image_file_with_detections))
                    {
                        lastfolder = Path.GetDirectoryName(this.cam.last_image_file_with_detections);
                        if (File.Exists(this.cam.last_image_file_with_detections))
                        {
                            Log($" (Found image from -last- detected object: {this.cam.last_image_file_with_detections})");
                            return this.cam.last_image_file_with_detections;
                        }
                        else
                        {
                            //extra debugging
                            Log(" >CAM.last_image_file_with_detections file no longer exists.");
                        }

                    }
                    else
                    {
                        //extra debugging
                        Log($" >No CAM.last_image_file_with_detections for '{this.cam.Name}'");
                    }


                    //Just take the last image processed by the camera even if no detections
                    if (!string.IsNullOrEmpty(this.cam.last_image_file))
                    {
                        lastfolder = Path.GetDirectoryName(this.cam.last_image_file);
                        if (File.Exists(this.cam.last_image_file))
                        {
                            Log($" (Found image from last processed image (no detections): {this.cam.last_image_file})");
                            return this.cam.last_image_file;
                        }
                        else
                        {
                            //extra debugging
                            Log(" >CAM.last_image_file file no longer exists.");
                        }
                    }
                    else
                    {
                        //extra debugging
                        Log($" >No CAM.last_image_file for '{this.cam.Name}'");
                    }

                    //try to use the LastCamImages version
                    string fldr = Path.Combine(Path.GetDirectoryName(AppSettings.Settings.SettingsFileName), "LastCamImages");
                    string file = Path.Combine(fldr, $"{cam.Name}-Last.jpg");

                    if (File.Exists(file))
                    {
                        Log($" (Found image from LastCamImages folder: ({file})");
                        return file;
                    }


                    if (string.IsNullOrEmpty(lastfolder))
                    {
                        lastfolder = this.cam.input_path;
                    }
                }
                else
                {
                    //extra debugging
                    Log(" >No CAM selected.");
                }

                //FAIL, scan the camera folder for the most recent image file
                if (!string.IsNullOrEmpty(lastfolder) && Directory.Exists(lastfolder))
                {
                    //I expect this may take a few seconds if folder is huge
                    DirectoryInfo dirinfo = new DirectoryInfo(lastfolder);
                    FileInfo myFile;
                    if (this.cam != null && !string.IsNullOrEmpty(this.cam.Prefix))
                    {
                        myFile = dirinfo.GetFiles($"{this.cam.Prefix.Trim()}*.jpg").OrderByDescending(f => f.LastWriteTime).First();
                        if (myFile != null)
                        {
                            Log($" (Found most recent image in camera folder for prefix '{this.cam.Prefix}' (no detections): {myFile.FullName})");
                            return myFile.FullName;
                        }
                        else
                        {
                            //extra debugging
                            Log($" >No files found starting with '{this.cam.Prefix}' in {lastfolder}'");
                        }

                    }
                    else
                    {
                        myFile = dirinfo.GetFiles("*.jpg").OrderByDescending(f => f.LastWriteTime).First();
                        if (myFile != null)
                        {
                            Log($" (Found most recent image in camera folder (no detections): {myFile.FullName})");
                            return myFile.FullName;
                        }
                        else
                        {
                            //extra debugging
                            Log($" >No files found in {lastfolder}'");
                        }
                    }
                }
                else
                {
                    //extra debugging
                    Log($">Lastfolder not found or doesnt exist - '{lastfolder}'");
                }


            }
            catch (Exception ex)
            {

                Log("Error: " + ex.Msg());
            }

            return "";

        }


        public Frm_DynamicMaskDetails()
        {
            this.InitializeComponent();
        }

        private void Frm_DynamicMaskDetails_Load(object sender, EventArgs e)
        {
            this.loading = true;

            Global_GUI.ConfigureFOLV(this.FOLV_MaskHistory, typeof(ObjectPosition), null, null, "createDate", SortOrder.Descending);
            Global_GUI.ConfigureFOLV(this.FOLV_Masks, typeof(ObjectPosition), null, null, "createDate", SortOrder.Descending);

            Global_GUI.RestoreWindowState(this);

            this.comboBox_filter_camera.Items.Clear();
            this.comboBox_filter_camera.Items.Add("All Cameras");

            int i = 1;
            int curidx = 1;
            foreach (Camera curcam in AppSettings.Settings.CameraList)
            {
                this.comboBox_filter_camera.Items.Add($"   {curcam.Name}");
                if (this.cam.Name.Trim().ToLower() == curcam.Name.Trim().ToLower())
                {
                    curidx = i;
                    Log($"Cam '{curcam.Name}' is at index '{curidx}'");
                }
                i++;
            }

            this.comboBox_filter_camera.SelectedIndex = curidx;


            this.Refresh();

            this.loading = false;


        }

        private void Refresh()
        {

            //in case of disabled cameras:
            if (!string.Equals(this.comboBox_filter_camera.Text, "All Cameras", StringComparison.OrdinalIgnoreCase) && this.comboBox_filter_camera.Text.ToLower().Trim() != this.cam.Name.ToLower().Trim())
            {
                this.cam = AITOOL.GetCamera(this.comboBox_filter_camera.Text);
            }

            List<ObjectPosition> hist = new List<ObjectPosition>();
            List<ObjectPosition> masked = new List<ObjectPosition>();

            if (this.comboBox_filter_camera.Text == "All Cameras")
            {
                foreach (Camera curcam in AppSettings.Settings.CameraList)
                {
                    hist.AddRange(curcam.maskManager.LastPositionsHistory);
                    masked.AddRange(curcam.maskManager.MaskedPositions);
                }
            }
            else
            {
                hist = this.cam.maskManager.LastPositionsHistory;
                masked = this.cam.maskManager.MaskedPositions;
            }

            Global_GUI.UpdateFOLV(this.FOLV_MaskHistory, hist, FullRefresh: true);
            Global_GUI.UpdateFOLV(this.FOLV_Masks, masked, FullRefresh: true);
            this.CurObjPosLst.Clear();
            this.ShowMaskImage();
            this.ShowImageMask(null);
        }


        private void ShowMaskImage()
        {
            try
            {
                string imagePath = this.GetBestImage();

                if (!string.IsNullOrEmpty(imagePath) && (this.pictureBox1.Tag == null || !string.Equals(this.pictureBox1.Tag.ToString(), imagePath, StringComparison.OrdinalIgnoreCase)))
                {
                    using (var img = new Bitmap(imagePath))
                    {
                        this.pictureBox1.BackgroundImage = new Bitmap(img); //load actual image as background, so that an overlay can be added as the image
                    }

                    this.pictureBox1.Tag = imagePath;
                    if (this.contextMenuPosObj != null && this.contextMenuPosObj.ImagePath != null)
                    {
                        if (!string.Equals(this.contextMenuPosObj.ImagePath, imagePath, StringComparison.OrdinalIgnoreCase))
                        {
                            this.lbl_lastfile.ForeColor = Color.DarkMagenta;
                            this.lbl_lastfile.Text = "Original Mask Image not found, using: " + imagePath;
                        }
                        else
                        {
                            this.lbl_lastfile.ForeColor = SystemColors.ControlText;
                            this.lbl_lastfile.Text = "Mask image: " + imagePath;
                        }

                    }
                    else
                    {
                        this.lbl_lastfile.ForeColor = Color.DarkMagenta;
                        this.lbl_lastfile.Text = "Original Mask Image not found, using: " + imagePath;
                    }

                }
                else if (string.IsNullOrEmpty(imagePath))
                {
                    this.pictureBox1.BackgroundImage = null;
                    this.lbl_lastfile.Text = "No image to display";
                    this.pictureBox1.Tag = "";

                }

                this.pictureBox1.Refresh();

            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Msg());
            }
        }

        private void ShowLastImage()
        {
            try
            {
                if (this.pictureBox1.Tag == null || !string.Equals(this.pictureBox1.Tag.ToString(), this.cam.last_image_file, StringComparison.OrdinalIgnoreCase))
                {
                    if ((!string.IsNullOrWhiteSpace(this.cam.last_image_file)))
                    {
                        if (File.Exists(this.cam.last_image_file))
                        {
                            using (var img = new Bitmap(this.cam.last_image_file))
                            {
                                this.pictureBox1.BackgroundImage = new Bitmap(img); //load actual image as background, so that an overlay can be added as the image
                            }

                            this.lbl_lastfile.Text = "Last image: " + this.cam.last_image_file;
                            this.pictureBox1.Tag = this.cam.last_image_file;
                        }
                        else
                        {
                            this.lbl_lastfile.Text = "Last image doesn't exist: " + this.cam.last_image_file;
                            this.pictureBox1.BackgroundImage = null;
                        }
                    }
                    else
                    {
                        this.lbl_lastfile.Text = "No image to show for this camera yet (Must have processed an image within the current session)";
                        this.pictureBox1.BackgroundImage = null;
                    }
                }

                this.pictureBox1.Refresh();

            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Msg());
            }
        }

        private void ShowImageMask(PaintEventArgs e = null)
        {
            try
            {
                if (this.CurObjPosLst.Count > 0 && e != null && this.pictureBox1 != null && this.pictureBox1.BackgroundImage != null)
                {
                    //1. get the padding between the image and the picturebox border

                    //get dimensions of the image and the picturebox
                    double imgWidth = this.pictureBox1.BackgroundImage.Width;
                    double imgHeight = this.pictureBox1.BackgroundImage.Height;
                    double boxWidth = this.pictureBox1.Width;
                    double boxHeight = this.pictureBox1.Height;

                    //these variables store the padding between image border and picturebox border
                    double absX = 0;
                    double absY = 0;

                    //because the sizemode of the picturebox is set to 'zoom', the image is scaled down
                    double scale = 1;

                    //Comparing the aspect ratio of both the control and the image itself.
                    if (imgWidth / imgHeight > boxWidth / boxHeight) //if the image is p.e. 16:9 and the picturebox is 4:3
                    {
                        scale = boxWidth / imgWidth; //get scale factor
                        absY = (boxHeight - scale * imgHeight) / 2; //padding on top and below the image
                    }
                    else //if the image is p.e. 4:3 and the picturebox is widescreen 16:9
                    {
                        scale = boxHeight / imgHeight; //get scale factor
                        absX = (boxWidth - scale * imgWidth) / 2; //padding left and right of the image
                    }

                    bool showkey = this.CurObjPosLst.Count > 1;

                    foreach (ObjectPosition op in this.CurObjPosLst)
                    {
                        if (op != null)
                        {
                            //Log("Painting object");
                            //2. inputted position values are for the original image size. As the image is probably smaller in the picturebox, the positions must be adapted. 

                            double xmin = (scale * op.Xmin + this.cam.XOffset) + absX;
                            double xmax = (scale * op.Xmax) + absX;
                            double ymin = (scale * op.Ymin + this.cam.YOffset) + absY;
                            double ymax = (scale * op.Ymax) + absY;

                            Color color;
                            if (op.IsStatic)
                            {
                                color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectRelevantColorAlpha, AppSettings.Settings.RectRelevantColor);
                            }
                            else
                            {
                                color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectIrrelevantColorAlpha, AppSettings.Settings.RectIrrelevantColor);
                            }

                            //3. paint rectangle
                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(xmin.ToInt(), ymin.ToInt(), (xmax - xmin).ToInt(), (ymax - ymin).ToInt());
                            using (Pen pen = new Pen(color, AppSettings.Settings.RectBorderWidth))
                            {
                                e.Graphics.DrawRectangle(pen, rect); //draw rectangle
                            }

                            //object name text below rectangle
                            rect = new System.Drawing.Rectangle((xmin - 1).ToInt(), ymax.ToInt(), boxWidth.ToInt(), boxHeight.ToInt()); //sets bounding box for drawn text

                            Brush brush = new SolidBrush(color); //sets background rectangle color
                            //if (AppSettings.Settings.RectDetectionTextBackColor != System.Drawing.Color.Gainsboro)
                            //    brush = new SolidBrush(AppSettings.Settings.RectDetectionTextBackColor);

                            Brush forecolor = Brushes.Black;
                            //if (AppSettings.Settings.RectDetectionTextForeColor != System.Drawing.Color.Gainsboro)
                            //    forecolor = new SolidBrush(AppSettings.Settings.RectDetectionTextForeColor);

                            string display = $"{op.Label}";

                            if (showkey)
                                display += $" ({op.Key})";

                            System.Drawing.SizeF size = e.Graphics.MeasureString(display, new Font(AppSettings.Settings.RectDetectionTextFont, AppSettings.Settings.RectDetectionTextSize)); //finds size of text to draw the background rectangle
                            e.Graphics.FillRectangle(brush, (xmin - 1).ToInt(), ymax.ToInt(), size.Width, size.Height); //draw grey background rectangle for detection text
                            e.Graphics.DrawString(display, new Font(AppSettings.Settings.RectDetectionTextFont, AppSettings.Settings.RectDetectionTextSize), forecolor, rect); //draw detection text
                        }
                        else
                        {
                            //Log("op is empty");
                        }
                    }
                }
                else
                {
                    //this.pictureBox1 = null;
                    //this.pictureBox1.BackgroundImage = null;
                }
            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Msg());
            }
        }

        private void FOLV_MaskHistory_SelectionChanged(object sender, EventArgs e)
        {
            this.CurObjPosLst.Clear();

            if (this.FOLV_MaskHistory.SelectedObjects != null && this.FOLV_MaskHistory.SelectedObjects.Count > 0)
            {
                this.contextMenuPosObj = (ObjectPosition)this.FOLV_MaskHistory.SelectedObjects[0];
                this.cam = AITOOL.GetCamera(this.contextMenuPosObj.CameraName);

                this.ShowMaskImage();

                foreach (object obj in this.FOLV_MaskHistory.SelectedObjects)
                {
                    this.CurObjPosLst.Add((ObjectPosition)obj);
                }
            }
            this.pictureBox1.Refresh();
        }

        private void FOLV_Masks_SelectionChanged(object sender, EventArgs e)
        {
            this.CurObjPosLst.Clear();

            if (this.FOLV_Masks.SelectedObjects != null && this.FOLV_Masks.SelectedObjects.Count > 0)
            {
                this.contextMenuPosObj = (ObjectPosition)this.FOLV_Masks.SelectedObjects[0];
                this.cam = AITOOL.GetCamera(this.contextMenuPosObj.CameraName);

                this.ShowMaskImage();

                foreach (object obj in this.FOLV_Masks.SelectedObjects)
                {
                    this.CurObjPosLst.Add((ObjectPosition)obj);
                }
            }

            this.pictureBox1.Refresh();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            this.ShowImageMask(e);
        }

        private void lblClearMasks_Click(object sender, EventArgs e)
        {
            this.cam.maskManager.MaskedPositions.Clear();
            this.Refresh();
            AppSettings.SaveAsync();
        }

        private void lblClearHistory_Click(object sender, EventArgs e)
        {
            this.cam.maskManager.LastPositionsHistory.Clear();
            this.Refresh();
            AppSettings.SaveAsync();
        }

        private void FOLV_MaskHistory_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
        {
            if (e.Model != null)
            {
                this.contextMenuPosObj = (ObjectPosition)e.Model;
                this.cam = AITOOL.GetCamera(this.contextMenuPosObj.CameraName);
            }
        }

        private void createStaticMaskToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (ObjectPosition op in this.CurObjPosLst)
            {
                op.IsStatic = true;
                op.Counter = 0;
                this.cam.maskManager.CreateDynamicMask(op, true);
            }
            this.Refresh();
            AppSettings.SaveAsync();
        }

        private void FOLV_Masks_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
        {
            if (e.Model != null)
            {
                this.contextMenuPosObj = (ObjectPosition)e.Model;
            }
        }

        private void removeMaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ObjectPosition op in this.CurObjPosLst)
            {
                this.cam.maskManager.RemoveActiveMask(op);
            }
            this.Refresh();
            AppSettings.SaveAsync();
        }

        private void Frm_DynamicMaskDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void FOLV_MaskHistory_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            this.FormatRow(sender, e);
        }

        private void FormatRow(object Sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            try
            {
                ObjectPosition OP = (ObjectPosition)e.Model;

                // If SPI IsNot Nothing Then
                if (OP.IsStatic && e.Item.ForeColor != Color.Blue)
                    e.Item.ForeColor = Color.Blue;
                else if (!OP.IsStatic && e.Item.ForeColor != Color.Black)
                    e.Item.ForeColor = Color.Black;
            }



            catch (Exception)
            {
            }
            // Log("Error: " & ex.Msg())
            finally
            {
            }
        }

        private void FOLV_Masks_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            this.FormatRow(sender, e);
        }

        private void createStaticMaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (ObjectPosition op in this.CurObjPosLst)
            {
                op.IsStatic = true;
                op.Counter = 0;
            }
            this.Refresh();
            AppSettings.SaveAsync();
        }

        private void FOLV_MaskHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_filter_camera_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void comboBox_filter_camera_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //I think this event only triggers when user picks something NOT when items are initially added to combobox
            if (!(this.comboBox_filter_camera.Text == "All Cameras"))
            {
                this.BtnDynamicMaskingSettings.Enabled = true;
                this.cam = AITOOL.GetCamera(this.comboBox_filter_camera.Text);
            }
            else
            {
                this.BtnDynamicMaskingSettings.Enabled = false;
            }

            this.Refresh();
        }

        private void BtnDynamicMaskingSettings_Click(object sender, EventArgs e)
        {
            using (Frm_DynamicMasking frm = new Frm_DynamicMasking())
            {
                frm.cam = this.cam;
                frm.Text = "Dynamic Masking Settings - " + this.cam.Name;

                //Camera cam = AITOOL.GetCamera(list2.SelectedItems[0].Text);

                //Merge ClassObject's code
                frm.num_history_mins.Value = this.cam.maskManager.HistorySaveMins;//load minutes to retain history objects that have yet to become masks
                frm.num_mask_create.Value = this.cam.maskManager.HistoryThresholdCount; // load mask create counter
                frm.num_mask_remove.Value = this.cam.maskManager.MaskRemoveMins; //load mask remove counter
                frm.numMaskThreshold.Value = this.cam.maskManager.MaskRemoveThreshold;
                frm.num_max_unused.Value = this.cam.maskManager.MaxMaskUnusedDays;

                //frm.num_percent_var.Value = (decimal)cam.maskManager.thresholdPercent * 100;
                frm.num_percent_var.Value = (decimal)this.cam.maskManager.PercentMatch;

                frm.cb_enabled.Checked = this.cam.maskManager.MaskingEnabled;

                //frm.tb_objects.Text = this.cam.maskManager.Objects;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ////get masking values from textboxes
                    this.cam.maskManager.HistorySaveMins = frm.num_history_mins.Text.ToInt();
                    this.cam.maskManager.HistoryThresholdCount = frm.num_mask_create.Text.ToInt();
                    this.cam.maskManager.MaskRemoveMins = frm.num_mask_remove.Text.ToInt();
                    this.cam.maskManager.MaskRemoveThreshold = frm.numMaskThreshold.Text.ToInt();
                    this.cam.maskManager.PercentMatch = frm.num_percent_var.Text.ToDouble();
                    this.cam.maskManager.MaxMaskUnusedDays = frm.num_max_unused.Text.ToInt();
                    this.cam.maskManager.MaskingEnabled = frm.cb_enabled.Checked;

                    AppSettings.SaveAsync();

                }
            }
        }

        private void staticMaskMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void createDynamicMaskTemporaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ObjectPosition op in this.CurObjPosLst)
            {
                this.cam.maskManager.CreateDynamicMask(op, false, true);
            }
            this.Refresh();
            AppSettings.SaveAsync();
        }

        private void removeHistoryMaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ObjectPosition op in this.CurObjPosLst)
            {
                this.cam.maskManager.RemoveHistoryMask(op);

            }
            this.Refresh();
            AppSettings.SaveAsync();
        }
    }
}
