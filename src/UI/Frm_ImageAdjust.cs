using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AITool.AITOOL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.Metadata;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Image = System.Drawing.Image;

namespace AITool
{
    public partial class Frm_ImageAdjust : Form
    {

        public Camera cam = null;

        private ClsImageAdjust CurIA = null;

        public Frm_ImageAdjust()
        {
            InitializeComponent();
        }

        private void Frm_ImageAdjust_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);
            UpdateCombo();

            this.tb_ImageFile.Text = GetBestImage();

        }

        private async Task ApplyAsync()
        {

            try
            {
                if (!string.IsNullOrEmpty(this.tb_ImageFile.Text) && File.Exists(this.tb_ImageFile.Text))
                {
                    //SixLabors.ImageSharp.Configuration config = new Configuration();

                    SixLabors.ImageSharp.Image image = await SixLabors.ImageSharp.Image.LoadAsync(this.tb_ImageFile.Text);

                        int width = Convert.ToInt32(tb_Width.Text);
                    int height = Convert.ToInt32(tb_Height.Text);
                    int sizepercent = Convert.ToInt32(tb_SizePercent.Text);

                    if (width != -1 && height != -1 && image.Width != width || image.Height != height)  //hard coded size
                    {
                        Log($"Resizing image from {image.Width},{image.Height} to {width},{height}...");
                        image.Mutate(img => img.Resize(width, height));
                    }
                    else if (sizepercent > 0 && sizepercent < 100)
                    {
                        double fractionalPercentage = (sizepercent / 100.0);
                        int outputWidth = (int)(image.Width * fractionalPercentage);
                        int outputHeight = (int)(image.Height * fractionalPercentage);

                        Log($"Resizing image to {sizepercent} from {image.Width},{image.Height} to {outputWidth},{outputHeight}...");
                        image.Mutate(img => img.Resize(outputWidth, outputHeight));
                    }

                    int quality = Convert.ToInt32(this.tb_jpegquality.Text);

                    MemoryStream stream = new System.IO.MemoryStream();

                    if (quality > 0 && quality < 100)
                    {
                        //string tfile = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "_AITOOL\tmpimage.jpg");

                        //Save the image using the specified jpeg compression
                        Log($"Compressing jpeg to {quality}% quality...");
                        SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder();
                        encoder.Quality = quality;
                        await image.SaveAsJpegAsync(stream, encoder);

                        image = await SixLabors.ImageSharp.Image.LoadAsync(stream);

                     }

                    int bright = Convert.ToInt32(tb_brightness.Text);

                    if (bright > 1)
                    {

                    }

                    // render onto an Image
                    //image.SaveAsBmp(stream);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(stream);

                    // dispose the old image before displaying the new one
                    //pictureBox1.Image?.Dispose();
                    this.pictureBox1.BackgroundImage = img;
                }
                else
                {
                    MessageBox.Show("File does not exist.");
                }

            }
            catch (Exception ex)
            {

                Log("Error: " + Global.ExMsg(ex));
            }

        }

        private void UpdateCombo()
        {
            string last = "Default";

            if (AppSettings.Settings.ImageAdjustProfiles.Count == 0)
            {
                AppSettings.Settings.ImageAdjustProfiles.Add(new ClsImageAdjust("Default"));
            }

            if (!string.IsNullOrEmpty(this.comboBox1.Text.Trim()))
                last = this.comboBox1.Text.Trim();

            if (!AITOOL.HasImageAdjustProfile(this.comboBox1.Text.Trim()) && !string.IsNullOrEmpty(this.comboBox1.Text.Trim()))
            {
                AppSettings.Settings.ImageAdjustProfiles.Add(new ClsImageAdjust(this.comboBox1.Text.Trim()));
            }

            this.comboBox1.Items.Clear();

            int i = 0;
            int oldidx = 0;
            foreach (ClsImageAdjust ia in AppSettings.Settings.ImageAdjustProfiles)
            {
                this.comboBox1.Items.Add(ia.Name);
                if (string.Equals(last.Trim(), ia.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    oldidx = i + 1;
                }
                i++;
            }

            if (this.comboBox1.Items.Count > 0)
                this.comboBox1.SelectedIndex = oldidx;

            this.CurIA = AITOOL.GetImageAdjustProfileByName(this.comboBox1.Text.Trim());

        }

        public string GetBestImage()
        {
            //try to prioritize the images from actual detections first
            //goal is to always try to have an image to display
            // IS THIS OVERKILL OR CONFUSING TO USER???

            try
            {
                string lastfolder = "";

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
                        Log($" >No CAM.last_image_file_with_detections for '{this.cam.name}'");
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
                        Log($" >No CAM.last_image_file for '{this.cam.name}'");
                    }

                    //try to use the LastCamImages version
                    string fldr = Path.Combine(Path.GetDirectoryName(AppSettings.Settings.SettingsFileName), "LastCamImages");
                    string file = Path.Combine(fldr, $"{cam.name}-Last.jpg");

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
                    if (this.cam != null && !string.IsNullOrEmpty(this.cam.prefix))
                    {
                        myFile = dirinfo.GetFiles($"{this.cam.prefix.Trim()}*.jpg").OrderByDescending(f => f.LastWriteTime).First();
                        if (myFile != null)
                        {
                            Log($" (Found most recent image in camera folder for prefix '{this.cam.prefix}' (no detections): {myFile.FullName})");
                            return myFile.FullName;
                        }
                        else
                        {
                            //extra debugging
                            Log($" >No files found starting with '{this.cam.prefix}' in {lastfolder}'");
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

                Log("Error: " + Global.ExMsg(ex));
            }

            return "";

        }
        private void Frm_ImageAdjust_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            UpdateCombo();
        }

        private void bt_Delete_Click(object sender, EventArgs e)
        {
            if (AITOOL.HasImageAdjustProfile(this.comboBox1.Text.Trim()))
            {
                AppSettings.Settings.ImageAdjustProfiles.Remove(AITOOL.GetImageAdjustProfileByName(this.comboBox1.Text.Trim()));
                this.comboBox1.Items.Clear();
                this.comboBox1.Text = "";
                UpdateCombo();
            }
        }

        private void tbar_brightness_ValueChanged(object sender, EventArgs e)
        {
            tb_brightness.Text = tbar_brightness.Value.ToString() ;
        }

        private void tbar_contrast_ValueChanged(object sender, EventArgs e)
        {
            tb_contrast.Text = tbar_contrast.Value.ToString();
        }
    }
}
