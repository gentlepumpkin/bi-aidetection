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

namespace AITool
{
    public partial class Frm_DynamicMaskDetails:Form
    {

        public Camera cam;
        public List<ObjectPosition> CurObjPosLst = new List<ObjectPosition>();

        public Frm_DynamicMaskDetails()
        {
            InitializeComponent();
        }

        private void Frm_DynamicMaskDetails_Load(object sender, EventArgs e)
        {
            Global_GUI.ConfigureFOLV(ref FOLV_MaskHistory, typeof(ObjectPosition), null, null);
            Global_GUI.ConfigureFOLV(ref FOLV_Masks, typeof(ObjectPosition), null, null);

            Refresh();

        }

        private void Refresh()
        {
            Global_GUI.UpdateFOLV(ref FOLV_MaskHistory, cam.maskManager.last_positions_history, true);
            Global_GUI.UpdateFOLV(ref FOLV_Masks, cam.maskManager.masked_positions, true);
            this.CurObjPosLst.Clear();
            ShowImageMask(null);

        }

        private void ShowImage()
        {
            try
            {

                if (pictureBox1.Tag == null || pictureBox1.Tag.ToString().ToLower() != this.cam.last_image_file.ToLower())
                {
                    if ((!string.IsNullOrWhiteSpace(this.cam.last_image_file)) && (File.Exists(this.cam.last_image_file)))
                    {
                        using (var img = new Bitmap(this.cam.last_image_file))
                        {
                            pictureBox1.BackgroundImage = new Bitmap(img); //load actual image as background, so that an overlay can be added as the image
                        }

                        pictureBox1.Tag = this.cam.last_image_file;
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


                if (CurObjPosLst.Count > 0 && e != null)
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
                            if (op.isVisible)
                            {
                                color = Color.Red;
                            }
                            else
                            {
                                color = Color.Gray;
                            }

                            //set alpha/transparency so you can see under the label
                            Color newColor = Color.FromArgb(100, color);  //The alpha component specifies how the shape and background colors are mixed; alpha values near 0 place more weight on the background colors, and alpha values near 255 place more weight on the shape color.

                            //3. paint rectangle
                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
                            using (Pen pen = new Pen(newColor, 2))
                            {
                                e.Graphics.DrawRectangle(pen, rect); //draw rectangle
                            }

                            //object name text below rectangle
                            rect = new System.Drawing.Rectangle(xmin - 1, ymax, (int)boxWidth, (int)boxHeight); //sets bounding box for drawn text


                            Brush brush = new SolidBrush(newColor); //sets background rectangle color

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

        private void FOLV_MaskHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FOLV_Masks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FOLV_MaskHistory_SelectionChanged(object sender, EventArgs e)
        {
            this.CurObjPosLst.Clear();
            ShowImage();
            if (FOLV_MaskHistory.SelectedObjects != null && FOLV_MaskHistory.SelectedObjects.Count > 0)
            {
                foreach (object obj in FOLV_MaskHistory.SelectedObjects)
                {
                    CurObjPosLst.Add((ObjectPosition)obj);
                }
            }
            pictureBox1.Refresh();
        }

        private void FOLV_Masks_SelectionChanged(object sender, EventArgs e)
        {
            this.CurObjPosLst.Clear();
            ShowImage();
            if (FOLV_Masks.SelectedObjects != null && FOLV_Masks.SelectedObjects.Count > 0)
            {
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
    }
}
