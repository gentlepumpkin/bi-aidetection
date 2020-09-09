using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AITool
{
    public partial class Frm_DynamicMaskDetails:Form
    {

        public Camera cam;
        public List<ObjectPosition> CurObjPosLst = new List<ObjectPosition>();
        public ObjectPosition contextMenuPosObj;

        public string GetBestImage()
        {
            //try to prioritize the images from actual detections first
            //goal is to always try to have an image to display
            // IS THIS OVERKILL OR CONFUSING TO USER???

            try
            {
                string lastfolder = "";

                if (contextMenuPosObj != null && !string.IsNullOrEmpty(contextMenuPosObj.imagePath))
                {
                    lastfolder = Path.GetDirectoryName(contextMenuPosObj.imagePath);
                    if (File.Exists(contextMenuPosObj.imagePath))
                    {
                        Global.Log($" (Found image from ACTUAL detected object: {contextMenuPosObj.imagePath})");
                        return contextMenuPosObj.imagePath;
                    }
                    else
                    {
                        Global.Log("WARNING: Mask image file not found at location: " + contextMenuPosObj.imagePath + ". Defaulting to last processed image");
                    }
                }
                else
                {
                    Global.Log("WARNING: Mask image file path was blank or NULL. Defaulting to last processed image");

                }

                //See if we have an image stored that had ANY detections and use it
                if (cam != null && !string.IsNullOrEmpty(cam.last_image_file_with_detections))
                {
                    lastfolder = Path.GetDirectoryName(cam.last_image_file_with_detections);
                    if (File.Exists(cam.last_image_file_with_detections))
                    {
                        Global.Log($" (Found image from -last- detected object: {cam.last_image_file_with_detections})");
                        return cam.last_image_file_with_detections;
                    }
                }

                //Just take the last image processed by the camera even if no detections
                if (cam != null && !string.IsNullOrEmpty(cam.last_image_file) )
                {
                    lastfolder = Path.GetDirectoryName(cam.last_image_file);
                    if (File.Exists(cam.last_image_file))
                    {
                        Global.Log($" (Found image from last processed image (no detections): {cam.last_image_file})");
                        return cam.last_image_file;
                    }
                }

                //FAIL, scan the camera folder for the most recent image file
                if (!string.IsNullOrEmpty(lastfolder) && Directory.Exists(lastfolder))
                {
                    //I expect this may take a few seconds if folder is huge
                    DirectoryInfo dirinfo = new DirectoryInfo(lastfolder);
                    FileInfo myFile = dirinfo.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                    Global.Log($" (Found most recent image in camera folder (no detections): {myFile.FullName})");
                    return myFile.FullName;
                }

            }
            catch (Exception ex)
            {

                Global.Log("Error: " + Global.ExMsg(ex));
            }
            
            return "";

        }

       

        public Frm_DynamicMaskDetails()
        {
            InitializeComponent();
        }

        private void Frm_DynamicMaskDetails_Load(object sender, EventArgs e)
        {
            Global_GUI.ConfigureFOLV(ref FOLV_MaskHistory, typeof(ObjectPosition), null, null);
            Global_GUI.ConfigureFOLV(ref FOLV_Masks, typeof(ObjectPosition), null, null);

            Global_GUI.RestoreWindowState(this);

            Refresh();
        }

        private void Refresh()
        {
            Global_GUI.UpdateFOLV(ref FOLV_MaskHistory, cam.maskManager.last_positions_history, true);
            Global_GUI.UpdateFOLV(ref FOLV_Masks, cam.maskManager.masked_positions, true);
            this.CurObjPosLst.Clear();
            ShowMaskImage();
            ShowImageMask(null);
        }


        private void ShowMaskImage()
        {
            try
            {
                string imagePath = GetBestImage();

                if (pictureBox1.Tag == null || pictureBox1.Tag.ToString().ToLower() != imagePath.ToLower())
                {
                    using (var img = new Bitmap(imagePath))
                    {
                        pictureBox1.BackgroundImage = new Bitmap(img); //load actual image as background, so that an overlay can be added as the image
                    }

                    lbl_lastfile.Text = "Mask image: " + imagePath;
                    pictureBox1.Tag = imagePath;
                }
                pictureBox1.Refresh();

            }
            catch (Exception ex)
            {
                Global.Log("Error: " + Global.ExMsg(ex));
            }
        }

        private void ShowLastImage()
        {
            try
            {
                if (pictureBox1.Tag == null || pictureBox1.Tag.ToString().ToLower() != this.cam.last_image_file.ToLower())
                {
                    if ((!string.IsNullOrWhiteSpace(this.cam.last_image_file)))
                    {
                        if (File.Exists(this.cam.last_image_file))
                        {
                            using (var img = new Bitmap(this.cam.last_image_file))
                            {
                                pictureBox1.BackgroundImage = new Bitmap(img); //load actual image as background, so that an overlay can be added as the image
                            }

                            lbl_lastfile.Text = "Last image: " + this.cam.last_image_file;
                            pictureBox1.Tag = this.cam.last_image_file;
                        }
                        else
                        {
                            lbl_lastfile.Text = "Last image doesn't exist: " + this.cam.last_image_file;
                            pictureBox1.BackgroundImage = null;
                        }
                    }
                    else
                    {
                        lbl_lastfile.Text = "No image to show for this camera yet (Must have processed an image within the current session)";
                        pictureBox1.BackgroundImage = null;
                    }
                }

                pictureBox1.Refresh();

            }
            catch (Exception ex)
            {
                Global.Log("Error: " + Global.ExMsg(ex));
            }
        }

        private void ShowImageMask(PaintEventArgs e = null)
        {
            try
            {
                if (CurObjPosLst.Count > 0 && e != null && pictureBox1 != null && pictureBox1.BackgroundImage != null)
                {
                    //1. get the padding between the image and the picturebox border

                    //get dimensions of the image and the picturebox
                    float imgWidth = pictureBox1.BackgroundImage.Width;
                    float imgHeight = pictureBox1.BackgroundImage.Height;
                    float boxWidth = pictureBox1.Width;
                    float boxHeight = pictureBox1.Height;

                    //these variables store the padding between image border and picturebox border
                    int absX = 0;
                    int absY = 0;

                    //because the sizemode of the picturebox is set to 'zoom', the image is scaled down
                    float scale = 1;

                    //Comparing the aspect ratio of both the control and the image itself.
                    if (imgWidth / imgHeight > boxWidth / boxHeight) //if the image is p.e. 16:9 and the picturebox is 4:3
                    {
                        scale = boxWidth / imgWidth; //get scale factor
                        absY = (int)(boxHeight - scale * imgHeight) / 2; //padding on top and below the image
                    }
                    else //if the image is p.e. 4:3 and the picturebox is widescreen 16:9
                    {
                        scale = boxHeight / imgHeight; //get scale factor
                        absX = (int)(boxWidth - scale * imgWidth) / 2; //padding left and right of the image
                    }

                    foreach (ObjectPosition op in CurObjPosLst)
                    {
                        if (op != null)
                        {
                            //Global.Log("Painting object");
                            //2. inputted position values are for the original image size. As the image is probably smaller in the picturebox, the positions must be adapted. 
                            int xmin = (int)(scale * op.xmin) + absX;
                            int xmax = (int)(scale * op.xmax) + absX;
                            int ymin = (int)(scale * op.ymin) + absY;
                            int ymax = (int)(scale * op.ymax) + absY;

                            Color color;
                            if (op.isStatic)
                            {
                                color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectRelevantColorAlpha, AppSettings.Settings.RectRelevantColor);
                            }
                            else
                            {
                                color = System.Drawing.Color.FromArgb(AppSettings.Settings.RectIrrelevantColorAlpha, AppSettings.Settings.RectIrrelevantColor);
                            }

                            //3. paint rectangle
                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
                            using (Pen pen = new Pen(color, 2))
                            {
                                e.Graphics.DrawRectangle(pen, rect); //draw rectangle
                            }

                            //object name text below rectangle
                            rect = new System.Drawing.Rectangle(xmin - 1, ymax, (int)boxWidth, (int)boxHeight); //sets bounding box for drawn text

                            Brush brush = new SolidBrush(color); //sets background rectangle color

                            System.Drawing.SizeF size = e.Graphics.MeasureString(op.label, new Font("Segoe UI Semibold", 10)); //finds size of text to draw the background rectangle
                            e.Graphics.FillRectangle(brush, xmin - 1, ymax, size.Width, size.Height); //draw grey background rectangle for detection text
                            e.Graphics.DrawString(op.label, new Font("Segoe UI Semibold", 10), Brushes.Black, rect); //draw detection text
                        }
                        else
                        {
                            //Global.Log("op is empty");
                        }
                    }
                }
                else
                {
                    //debug a bit
                    if (CurObjPosLst.Count == 0)
                    {
                        //Global.Log("Empty object list");
                    }
                    else if (e == null)
                    {
                        //Global.Log("Not painting");
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Log("Error: " + Global.ExMsg(ex));
            }
        }
       
        private void FOLV_MaskHistory_SelectionChanged(object sender, EventArgs e)
        {
            this.CurObjPosLst.Clear();

            if (FOLV_MaskHistory.SelectedObjects != null && FOLV_MaskHistory.SelectedObjects.Count > 0)
            {
                contextMenuPosObj = (ObjectPosition)FOLV_MaskHistory.SelectedObject;
                ShowMaskImage();

                foreach (object obj in FOLV_MaskHistory.SelectedObjects)
                {
                    CurObjPosLst.Add((ObjectPosition)obj);
                    contextMenuPosObj = (ObjectPosition)obj;
                }
            }
            pictureBox1.Refresh();
        }

        private void FOLV_Masks_SelectionChanged(object sender, EventArgs e)
        {
            this.CurObjPosLst.Clear();
            
            if (FOLV_Masks.SelectedObjects != null && FOLV_Masks.SelectedObjects.Count > 0)
            {
                contextMenuPosObj = (ObjectPosition)FOLV_Masks.SelectedObject;
                ShowMaskImage();

                foreach (object obj in FOLV_Masks.SelectedObjects)
                {
                    CurObjPosLst.Add((ObjectPosition)obj);
                }
            }
            pictureBox1.Refresh();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            ShowImageMask(e);
        }

        private void lblClearMasks_Click(object sender, EventArgs e)
        {
            cam.maskManager.masked_positions.Clear();
            Refresh();
            AppSettings.Save();
        }

        private void lblClearHistory_Click(object sender, EventArgs e)
        {
            cam.maskManager.last_positions_history.Clear();
            Refresh();
            AppSettings.Save();
        }

        private void FOLV_MaskHistory_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
        {
            if (e.Model != null)
            {
                contextMenuPosObj = (ObjectPosition)e.Model;
            }
        }

        private void createStaticMaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuPosObj != null)
            {
                contextMenuPosObj.isStatic = true;
                contextMenuPosObj.counter = 0;
                cam.maskManager.masked_positions.Add(contextMenuPosObj);
                cam.maskManager.last_positions_history.Remove(contextMenuPosObj);
                contextMenuPosObj = null;
                Refresh();
                AppSettings.Save();
            }
        }

        private void FOLV_Masks_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
        {
            if (e.Model != null)
            {
                contextMenuPosObj = (ObjectPosition)e.Model;
            }
        }

        private void removeMaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuPosObj != null)
            {
                cam.maskManager.masked_positions.Remove(contextMenuPosObj);
                contextMenuPosObj = null;
                Refresh();
                AppSettings.Save();
            }
        }

        private void Frm_DynamicMaskDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void FOLV_MaskHistory_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            FormatRow(sender, e);
        }

        private async void FormatRow(object Sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            try
            {
                ObjectPosition OP = (ObjectPosition)e.Model;

                // If SPI IsNot Nothing Then
                if (OP.isStatic && e.Item.ForeColor != Color.Blue)
                    e.Item.ForeColor = Color.Blue;
                else if (!OP.isStatic && e.Item.ForeColor != Color.Black)
                    e.Item.ForeColor = Color.Black;
            }

            

            catch (Exception ex)
            {
            }
            // Log("Error: " & ExMsg(ex))
            finally
            {
            }
        }

        private void FOLV_Masks_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            FormatRow(sender, e);
        }

        private void createStaticMaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (contextMenuPosObj != null)
            {
                contextMenuPosObj.isStatic = true;
                contextMenuPosObj.counter = 0;
                contextMenuPosObj = null;
                Refresh();
                AppSettings.Save();
            }
        }

        private void FOLV_MaskHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
