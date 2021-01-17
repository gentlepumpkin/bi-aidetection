using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using static AITool.AITOOL;

namespace AITool
{
    public partial class Frm_CustomMasking : Form
    {
        public Camera Cam { get; set; }
        private Bitmap _transparentLayer, _cameraLayer, _inProgessLayer;
        private string _maskfilename { get; set; } = "";
        private const float DEFAULT_OPACITY = .5f;
        public int BrushSize { get; set; }
        private bool _drawing = false;

        private ImageResItem _imageRes = null;
        //brush drawing
        private PointHistory _currentPoints = new PointHistory(); //Contains all points since the mouse down event fired. Draw all points at once in Paint method. Prevents tearing and performance issues
        private List<PointHistory> _allPointLists = new List<PointHistory>();  //History of all points. Reserved for future undo feature

        //rectangle drawing
        private Point _startRectanglePoint = Point.Empty;      // mouse-down position 
        private Point _currentRectanglePoint = Point.Empty;    // current mouse position 


        public Frm_CustomMasking()
        {
            this.InitializeComponent();
        }

        /********************* Start EVENT section*************************************/

        private void Frm_CustomMasking_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);

            UpdateRes();

            UpdateMaskFile();

        }

        private void UpdateMaskFile()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            try
            {


                this._maskfilename = this.Cam.GetMaskFile(false, null, this._imageRes);
                this.Text = "Custom Masking - " + this._maskfilename;

                if (!File.Exists(this._maskfilename))
                    this.Text += " (Missing)";

                this.BrushSize = this.Cam.mask_brush_size;
                this.numBrushSize.Value = this.Cam.mask_brush_size;
                this.ShowImage();

            }
            catch (Exception ex)
            {
                AITOOL.Log($"Error: {Global.ExMsg(ex)}");
            }

        }

        private void UpdateRes()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            cbResolution.Items.Clear();
            foreach (ImageResItem res in this.Cam.ImageResolutions)
            {
                cbResolution.Items.Add(res);
            }

            if (cbResolution.Items.Count > 0)
            {
                cbResolution.SelectedIndex = 0;
                if (cbResolution.SelectedValue != null)
                    this._imageRes = (ImageResItem)cbResolution.SelectedValue;

            }

        }

        private async void linkLabelScan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            linkLabelScan.Text = "Scanning 60 secs...";
            linkLabelScan.Enabled = false;
            linkLabelScan.Update();
            await this.Cam.ScanImagesAsync();
            linkLabelScan.Text = "Scan";
            linkLabelScan.Update();
            linkLabelScan.Enabled = true;

            this.UpdateRes();
            this.UpdateMaskFile();


        }

        private void cbResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void ShowImage()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            try
            {
                string lastimage = "";

                if (this._imageRes == null && this.Cam.ImageResolutions.Count > 0)
                    this._imageRes = this.Cam.ImageResolutions[0];

                if (this._imageRes != null)
                    lastimage = this._imageRes.LastFileName;

                //first check for saved image in cameras folder. If doesn't exist load the last camera image.
                if (this.pbMaskImage.Tag == null || !string.Equals(this.pbMaskImage.Tag.ToString(), lastimage, StringComparison.OrdinalIgnoreCase))
                {
                    this.pbMaskImage.Tag = lastimage;

                    if ((!string.IsNullOrWhiteSpace(lastimage)) && (File.Exists(lastimage)))
                    {
                        this._cameraLayer = new Bitmap(lastimage);

                        //merge layer if masks exist
                        if (File.Exists(this._maskfilename))
                        {
                            using (Bitmap maskLayer = new Bitmap(this._maskfilename))
                            {
                                this.pbMaskImage.Image = this.MergeBitmaps(this._cameraLayer, maskLayer);
                                this._transparentLayer = new Bitmap(this.AdjustImageOpacity(maskLayer, 2f)); // create new bitmap here to prevent file locks and increase to 100% opacity
                            }
                        }
                        else //if there are no masks
                        {
                            this.pbMaskImage.Image = new Bitmap(this._cameraLayer);
                            this._transparentLayer = new Bitmap(this.pbMaskImage.Image.Width, this.pbMaskImage.Image.Height, PixelFormat.Format32bppPArgb);
                        }
                    }
                    else
                    {
                        //lbl_imagefile.ForeColor = Color.Gray;
                        //this.Text += " (Missing)";
                    }
                }

                this.pbMaskImage.Refresh();

            }
            catch (Exception ex)
            {
                Log("Error: " + Global.ExMsg(ex));
            }
        }

        private Bitmap MergeBitmaps(Bitmap cameraImage, Bitmap layer)
        {
            Bitmap newImage = new Bitmap(cameraImage.Width, cameraImage.Height, PixelFormat.Format32bppPArgb);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(cameraImage, Point.Empty);
                g.DrawImage(layer, Point.Empty);
            }
            return newImage;
        }

        private Point AdjustZoomMousePosition(Point point)
        {
            //return point;
            if (point == null || point.IsEmpty || this.pbMaskImage.Image == null)
            {
                return point;
            }
            //default to current values
            int xScaled = point.X;
            int yScaled = point.Y;

            //get dimensions of picturebox and scaled image
            float imgWidth = this.pbMaskImage.Image.Width;
            float imgHeight = this.pbMaskImage.Image.Height;
            float picboxWidth = this.pbMaskImage.Width;
            float picboxHeight = this.pbMaskImage.Height;

            float picAspect = picboxWidth / (float)picboxHeight;
            float imgAspect = imgWidth / (float)imgHeight;

            if (picAspect > imgAspect)
            {
                // pictureBox is wider/shorter than the image.
                yScaled = (int)(imgHeight * point.Y / (float)picboxHeight);

                // image fills the height of the PictureBox.
                // Get width.
                float scaledWidth = imgWidth * picboxHeight / imgHeight;
                float dx = (picboxWidth - scaledWidth) / 2;
                xScaled = (int)((point.X - dx) * imgHeight / (float)picboxHeight);
            }
            else
            {
                // pictureBox is taller/thinner than the image.
                xScaled = (int)(imgWidth * point.X / (float)picboxWidth);

                // image fills the height of the PictureBox.
                // Get height.
                float scaledHeight = imgHeight * picboxWidth / imgWidth;
                float dy = (picboxHeight - scaledHeight) / 2;
                yScaled = (int)((point.Y - dy) * imgWidth / picboxWidth);
            }

            return new Point(xScaled, yScaled);
        }

        private Bitmap AdjustImageOpacity(Image image, float alphaLevel)
        {
            // Initialize the color matrix.
            // Note the value {level} in row 4, column 4. this is for  the alpha channel
            float[][] matrixItems ={
               new float[] {1, 0, 0, 0, 0},
               new float[] {0, 1, 0, 0, 0},
               new float[] {0, 0, 1, 0, 0},
               new float[] {0, 0, 0, alphaLevel, 0},
               new float[] {0, 0, 0, 0, 1}};
            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);

            // Create an ImageAttributes object and set its color matrix.
            ImageAttributes imageAtt = new ImageAttributes();
            imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            // Now draw the semitransparent bitmap image.
            int iWidth = image.Width;
            int iHeight = image.Height;

            Bitmap newBmp = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(newBmp))
            {
                g.DrawImage(
                    image,
                    new Rectangle(0, 0, iWidth, iHeight),  // destination rectangle
                    0.0f,                          // source rectangle x
                    0.0f,                          // source rectangle y
                    iWidth,                        // source rectangle width
                    iHeight,                       // source rectangle height
                    GraphicsUnit.Pixel,
                    imageAtt);
            }
            return newBmp;
        }

        private Rectangle getRectangle()
        {
            if (this._startRectanglePoint != Point.Empty && this._currentRectanglePoint != Point.Empty)
            {
                return new Rectangle(
                    Math.Min(this._startRectanglePoint.X, this._currentRectanglePoint.X),
                    Math.Min(this._startRectanglePoint.Y, this._currentRectanglePoint.Y),
                    Math.Abs(this._startRectanglePoint.X - this._currentRectanglePoint.X),
                    Math.Abs(this._startRectanglePoint.Y - this._currentRectanglePoint.Y));
            }

            else return new Rectangle();
        }

        private Rectangle getScaledRectangle()
        {
            if (this._startRectanglePoint != Point.Empty && this._currentRectanglePoint != Point.Empty)
            {
                Point scaledStart = this.AdjustZoomMousePosition(this._startRectanglePoint);
                Point scaleCurrent = this.AdjustZoomMousePosition(this._currentRectanglePoint);

                return new Rectangle(
                    Math.Min(scaledStart.X, scaleCurrent.X),
                    Math.Min(scaledStart.Y, scaleCurrent.Y),
                    Math.Abs(scaledStart.X - scaleCurrent.X),
                    Math.Abs(scaledStart.Y - scaleCurrent.Y));
            }

            else return new Rectangle();
        }

        private void drawRectangle(Graphics e)
        {
            Color color = Color.FromArgb(255, 255, 255, 70);
            SolidBrush fillBrush = new SolidBrush(color);

            if (this._drawing)
            {
                e.DrawRectangle(Pens.Yellow, this.getRectangle());
            }
            else if (this._startRectanglePoint.IsEmpty == false)
            {
                using (Graphics g = Graphics.FromImage(this._inProgessLayer))
                {
                    g.FillRectangle(fillBrush, this.getScaledRectangle());
                }
            }
        }

        private void drawBrush(Graphics e)
        {
            Color color = Color.FromArgb(255, 255, 255, 70);
            //first draw the image for the picturebox. Used as a readonly background layer
            using (Pen pen = new Pen(color, this.BrushSize))
            {
                pen.MiterLimit = pen.Width / 2;
                pen.LineJoin = LineJoin.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                if (this._currentPoints.GetPoints().Count > 1)
                {
                    using (Graphics g = Graphics.FromImage(this.pbMaskImage.Image))
                    {
                        //first draw the mask on the picturebox. Used as a readonly background layer
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.DrawLines(pen, this._currentPoints.GetPoints().ToArray());
                    }

                    using (Graphics g = Graphics.FromImage(this._inProgessLayer))
                    {
                        //second draw the mask on a transparent layer. Used as a mask overlay on background defined above.
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.DrawLines(pen, this._currentPoints.GetPoints().ToArray());
                    }
                }
            }
        }


        private void pbMaskImage_Paint(object sender, PaintEventArgs e)
        {
            if (this.pbMaskImage.Image != null)
            {
                if (this._inProgessLayer == null)
                {
                    this._inProgessLayer = new Bitmap(this._transparentLayer.Width, this._transparentLayer.Height, PixelFormat.Format32bppPArgb);
                }

                if (this.rbRectangle.Checked)
                {
                    this.drawRectangle(e.Graphics);
                }
                else if (this.rbBrush.Checked)
                {
                    this.drawBrush(e.Graphics);
                }
            }
        }

        private void pbMaskImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.rbBrush.Checked)
            {
                this._currentPoints = new PointHistory(this.AdjustZoomMousePosition(e.Location), this.BrushSize);
            }
            else if (this.rbRectangle.Checked)
            {
                this._currentRectanglePoint = this._startRectanglePoint = e.Location;
                this._drawing = true;
            }
        }

        private void pbMaskImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._currentPoints.AddPoint(this.AdjustZoomMousePosition(e.Location));
                this._currentRectanglePoint = e.Location;
                this.pbMaskImage.Invalidate();
            }
        }

        private void pbMaskImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.rbBrush.Checked)
            {
                if (this._inProgessLayer != null)
                {
                    this._transparentLayer = this.MergeBitmaps(this._transparentLayer, this._inProgessLayer);
                    this.pbMaskImage.Image = this.MergeBitmaps(this._cameraLayer, this.AdjustImageOpacity(this._transparentLayer, DEFAULT_OPACITY));
                    this._inProgessLayer = null;
                }

                if (this._currentPoints.GetPoints().Count > 1)
                {
                    this._allPointLists.Add(this._currentPoints);
                    this._currentPoints = new PointHistory();
                }
            }
            else if (this.rbRectangle.Checked && e.Button == MouseButtons.Left)
            {
                this._drawing = false;

                if (this._inProgessLayer != null)
                {
                    this.pbMaskImage.Refresh(); // call paint to draw inprogress rectangle to image
                    this._transparentLayer = this.MergeBitmaps(this._transparentLayer, this._inProgessLayer);
                    this.pbMaskImage.Image = this.MergeBitmaps(this._cameraLayer, this.AdjustImageOpacity(this._transparentLayer, DEFAULT_OPACITY));

                    //reset rectangle positions and in progress layer
                    this._startRectanglePoint = Point.Empty;
                    this._currentRectanglePoint = Point.Empty;
                    this._inProgessLayer = null;
                }
            }
        }

        private void numBrushSize_Leave(object sender, EventArgs e)
        {
            if (this.numBrushSize.Text == "")
            {
                this.numBrushSize.Text = this.numBrushSize.Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            this._allPointLists.Clear();

            this.pbMaskImage.Tag = null;

            //if mask exists, delete it
            if (File.Exists(this._maskfilename))
            {
                File.Delete(this._maskfilename);
            }

            UpdateMaskFile();
            this.ShowImage();
        }

        private void numBrushSize_ValueChanged(object sender, EventArgs e)
        {
            this.BrushSize = (int)this.numBrushSize.Value;
        }

        private void numBrushSize_KeyUp(object sender, KeyEventArgs e)
        {
            this.BrushSize = (int)this.numBrushSize.Value;
        }

        private void pbMaskImage_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Frm_CustomMasking_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global_GUI.SaveWindowState(this);
        }

        private void cbResolution_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateMaskFile();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            try
            {
                if (this._transparentLayer != null)
                {
                    //save masks at 50% opacity 
                    Bitmap img = this.AdjustImageOpacity(this._transparentLayer, DEFAULT_OPACITY);
                    img.Save(this._maskfilename);
                    //save mask file with resolution attached
                    //string resfile = Path.Combine(Path.GetDirectoryName(this._maskfilename), $"{Path.GetFileNameWithoutExtension(this._maskfilename)}_{this._transparentLayer.Width}x{this._transparentLayer.Height}.bmp");
                    Log($"Mask file saved.  Resolution={img.Width}x{img.Height}. Transparency Resolution={this._transparentLayer.Width}x{this._transparentLayer.Height} : " + this._maskfilename);
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Log("Error: An error occurred saving custom mask with file name: " + this._maskfilename + ex.ToString());
            }
            finally
            {
                this.Close();
            }
        }


        public class PointHistory
        {
            private List<Point> _points = new List<Point>();
            public int BrushSize { get; set; }

            public PointHistory()
            {
                this._points = new List<Point>();
                this.BrushSize = 40;
            }

            public PointHistory(Point point, int brushSize)
            {
                this._points.Add(point);
                this.BrushSize = brushSize;
            }

            public void AddPoint(Point point)
            {
                this._points.Add(point);
            }

            public List<Point> GetPoints()
            {
                return this._points;
            }

            public void ClearPoints()
            {
                this._points.Clear();
            }
        }
    }
}
