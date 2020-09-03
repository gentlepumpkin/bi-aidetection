using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AITool
{
    public partial class Frm_CustomMasking : Form
    {
        public Camera cam {get; set;}
        private Point scaledLastPoint = Point.Empty;
        private Point lastPoint = Point.Empty;
        private bool isMouseDown = new Boolean();
        private Bitmap transparentLayer, cameraLayer;
        private const string baseDirectory = "./cameras/";
        private const string FILE_TYPE = ".bmp";
        private int brushSize;

        public Frm_CustomMasking()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void ShowImage()
        {
            try
            {
                //first check for saved image in cameras folder. If doesn't exist load the last camera image.
                if (pbMaskImage.Tag == null || pbMaskImage.Tag.ToString().ToLower() != this.cam.last_image_file.ToLower())
                {
                    if ((!string.IsNullOrWhiteSpace(this.cam.last_image_file)) && (File.Exists(this.cam.last_image_file)))
                    {
                        cameraLayer = new Bitmap(this.cam.last_image_file);

                        //merge layer if masks exist
                        if (File.Exists(baseDirectory + cam.name + FILE_TYPE))
                        {
                            using (Bitmap maskLayer = new Bitmap(baseDirectory + cam.name + FILE_TYPE)) 
                            {
                                cameraLayer = MergeBitmaps(cameraLayer, maskLayer);
                                pbMaskImage.Image = cameraLayer;
                                transparentLayer = new Bitmap(maskLayer); // create new bitmap here to prevent file locks
                            }
                        }
                        else //if there are no masks
                        {
                            pbMaskImage.Image = cameraLayer;
                            transparentLayer = new Bitmap(pbMaskImage.Image.Width, pbMaskImage.Image.Height);
                            transparentLayer.MakeTransparent(Color.Transparent);
                        }
                        
                    }
                }

                pbMaskImage.Refresh();

            }
            catch (Exception ex)
            {
                Global.Log("Error: " + Global.ExMsg(ex));
            }
        }

        private Bitmap MergeBitmaps(Bitmap cameraImage, Bitmap layer)
        {
            Bitmap newImage = new Bitmap(cameraImage.Width, cameraImage.Height);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(cameraImage, Point.Empty);
                g.DrawImage(layer, Point.Empty);
            }
            return newImage;
        }

        private Point AdjustZoomMousePosition(Point point)
        {
            if (point == null || point.IsEmpty || pbMaskImage.Image == null)
            {
                return point;
            }

            float boxWidth = pbMaskImage.Image.Width;
            float boxHeight = pbMaskImage.Image.Height;
            float imgWidth = pbMaskImage.Width;
            float imgHeight = pbMaskImage.Height;


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

            int xScaled = (int)(scale * point.X) + absX;
            int yScaled = (int)(scale * point.Y) + absY;

            return new Point(xScaled, yScaled);
        }

        private void Frm_CustomMasking_Load(object sender, EventArgs e)
        {
            Int32.TryParse(numBrushSize.Text, out brushSize);
            ShowImage();
            isMouseDown = false;
        }

        private void pbMaskImage_Paint(object sender, PaintEventArgs e)
        {
            

            e.Graphics.DrawRectangle(Pens.Yellow, lastPoint.X, lastPoint.Y, brushSize, brushSize);

            if (isMouseDown == true)
            {
                if (scaledLastPoint != Point.Empty && pbMaskImage.Image != null)
                {
                    //first draw the image for the picturebox. Used as a readonly background layer
                    using (Graphics g = Graphics.FromImage(pbMaskImage.Image))
                    {
                        Color color = Color.FromArgb(255, Color.Black);
                        SolidBrush semiTransBrush = new SolidBrush(color);

                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.FillRectangle(semiTransBrush, scaledLastPoint.X, scaledLastPoint.Y, brushSize, brushSize);
                    }

                    //second draw the mask on a transparent layer. Used as a mask overlay on background defined above.  
                    using (Graphics g = Graphics.FromImage(transparentLayer))
                    {
                        Color color = Color.FromArgb(255, 0, 0, 0);
                        SolidBrush semiTransBrush = new SolidBrush(color);

                        //g.CompositingQuality = CompositingQuality.GammaCorrected;
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.FillRectangle(semiTransBrush, scaledLastPoint.X, scaledLastPoint.Y, brushSize, brushSize);
                    }
                }
            }
        }

        private void pbMaskImage_MouseDown(object sender, MouseEventArgs e)
        {
            scaledLastPoint = AdjustZoomMousePosition(e.Location); //assign the scaledLastPoint to the current mouse position
            isMouseDown = true;     //set to true because our mouse button is pressed down
        }

        private void pbMaskImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (scaledLastPoint != null && pbMaskImage.Image != null)//if our last point is not null, which in this case we have assigned above
            {
                pbMaskImage.Invalidate();  //refreshes the picturebox
                scaledLastPoint = AdjustZoomMousePosition(e.Location);    //set the scaledLastPoint to the current mouse position    
                lastPoint = e.Location;
            }
        }

        private void pbMaskImage_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            scaledLastPoint = Point.Empty;
        }

        private void numBrushSize_Leave(object sender, EventArgs e)
        {
            if (numBrushSize.Text == "")
            {
                numBrushSize.Text = numBrushSize.Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //if mask exists, delete it
            if (File.Exists(baseDirectory + cam.name + FILE_TYPE))
            {
                File.Delete(baseDirectory + cam.name + FILE_TYPE);
            }
            
            ShowImage();
        }

        private void numBrushSize_ValueChanged(object sender, EventArgs e)
        {
            brushSize = (int) numBrushSize.Value;
        }

        private void numBrushSize_KeyUp(object sender, KeyEventArgs e)
        {
            brushSize = (int)numBrushSize.Value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (transparentLayer != null)
            {
                string path = baseDirectory + cam.name + FILE_TYPE;
                transparentLayer.Save(path);
            }
        }
    }
}
