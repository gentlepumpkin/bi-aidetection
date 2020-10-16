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
    public partial class Frm_CustomMasking:Form
    {
        public Camera Cam { get; set; }
        private Bitmap _transparentLayer, _cameraLayer, _inProgessLayer;
        private string _maskfilename {get;set;} = "";
        private const float DEFAULT_OPACITY = .5f;
        public int BrushSize { get; set; }
        private bool _drawing = false;

        //brush drawing
        private PointHistory _currentPoints = new PointHistory(); //Contains all points since the mouse down event fired. Draw all points at once in Paint method. Prevents tearing and performance issues
        private List<PointHistory> _allPointLists = new List<PointHistory>();  //History of all points. Reserved for future undo feature

        //rectangle drawing
        private Point _startRectanglePoint = Point.Empty;      // mouse-down position 
        private Point _currentRectanglePoint = Point.Empty;    // current mouse position 


        public Frm_CustomMasking()
        {
            InitializeComponent();
        }

        private void ShowImage()
        {
            try
            {
                //first check for saved image in cameras folder. If doesn't exist load the last camera image.
                if (pbMaskImage.Tag == null || pbMaskImage.Tag.ToString().ToLower() != this.Cam.last_image_file.ToLower())
                {
                    pbMaskImage.Tag = this.Cam.last_image_file;

                    if ((!string.IsNullOrWhiteSpace(this.Cam.last_image_file)) && (File.Exists(this.Cam.last_image_file)))
                    {
                        //lbl_imagefile.ForeColor = Color.Black;

                        _cameraLayer = new Bitmap(this.Cam.last_image_file);

                        //merge layer if masks exist
                        if (File.Exists(this._maskfilename))
                        {
                            using (Bitmap maskLayer = new Bitmap(this._maskfilename)) 
                            {
                                pbMaskImage.Image = MergeBitmaps(_cameraLayer, maskLayer);
                                _transparentLayer = new Bitmap(AdjustImageOpacity(maskLayer,2f)); // create new bitmap here to prevent file locks and increase to 100% opacity
                            }
                        }
                        else //if there are no masks
                        {
                            pbMaskImage.Image = new Bitmap(_cameraLayer);
                            _transparentLayer = new Bitmap(pbMaskImage.Image.Width, pbMaskImage.Image.Height, PixelFormat.Format32bppPArgb);
                        }
                    }
                    else
                    {
                        //lbl_imagefile.ForeColor = Color.Gray;
                        //this.Text += " (Missing)";
                    }
                }

                pbMaskImage.Refresh();

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
            if (point == null || point.IsEmpty || pbMaskImage.Image == null)
            {
                return point;
            }
            //default to current values
            int xScaled = point.X;
            int yScaled = point.Y;

            //get dimensions of picturebox and scaled image
            float imgWidth = pbMaskImage.Image.Width;
            float imgHeight = pbMaskImage.Image.Height;
            float picboxWidth = pbMaskImage.Width;
            float picboxHeight = pbMaskImage.Height;

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
            if (_startRectanglePoint != Point.Empty && _currentRectanglePoint != Point.Empty)
            {
                return new Rectangle(
                    Math.Min(_startRectanglePoint.X, _currentRectanglePoint.X),
                    Math.Min(_startRectanglePoint.Y, _currentRectanglePoint.Y),
                    Math.Abs(_startRectanglePoint.X - _currentRectanglePoint.X),
                    Math.Abs(_startRectanglePoint.Y - _currentRectanglePoint.Y));
            }
 
            else return new Rectangle();
        }

        private Rectangle getScaledRectangle()
        {
            if (_startRectanglePoint != Point.Empty && _currentRectanglePoint != Point.Empty)
            {
                Point scaledStart = AdjustZoomMousePosition(_startRectanglePoint);
                Point scaleCurrent = AdjustZoomMousePosition(_currentRectanglePoint);

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

            if (_drawing)
            {
                e.DrawRectangle(Pens.Yellow, getRectangle());
            }
            else if(_startRectanglePoint.IsEmpty == false)
            {
                using (Graphics g = Graphics.FromImage(_inProgessLayer))
                {
                    g.FillRectangle(fillBrush, getScaledRectangle());
                }
            }
        }

        private void drawBrush(Graphics e)
        {
            Color color = Color.FromArgb(255, 255, 255, 70);
            //first draw the image for the picturebox. Used as a readonly background layer
            using (Pen pen = new Pen(color, BrushSize))
            {
                pen.MiterLimit = pen.Width / 2;
                pen.LineJoin = LineJoin.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                if (_currentPoints.GetPoints().Count > 1)
                {
                    using (Graphics g = Graphics.FromImage(pbMaskImage.Image))
                    {
                        //first draw the mask on the picturebox. Used as a readonly background layer
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.DrawLines(pen, _currentPoints.GetPoints().ToArray());
                    }

                    using (Graphics g = Graphics.FromImage(_inProgessLayer))
                    {
                        //second draw the mask on a transparent layer. Used as a mask overlay on background defined above.
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.DrawLines(pen, _currentPoints.GetPoints().ToArray());
                    }
                }
            }
        }

        /********************* Start EVENT section*************************************/

        private void Frm_CustomMasking_Load(object sender, EventArgs e)
        {
            Global_GUI.RestoreWindowState(this);
            this._maskfilename = AITOOL.GetMaskFile(this.Cam.name);
            this.Text = "Custom Masking - " + this._maskfilename; 

            if (!File.Exists(this._maskfilename))
            {
               this.Text += " (Missing)";
            }

            BrushSize = Cam.mask_brush_size;
            numBrushSize.Value = Cam.mask_brush_size;
            ShowImage();
        }

        private void pbMaskImage_Paint(object sender, PaintEventArgs e)
        {
            if (pbMaskImage.Image != null)
            {
                if (_inProgessLayer == null)
                {
                    _inProgessLayer = new Bitmap(_transparentLayer.Width, _transparentLayer.Height, PixelFormat.Format32bppPArgb);
                }

                if (rbRectangle.Checked)
                {
                    drawRectangle(e.Graphics);
                }
                else if(rbBrush.Checked)
                {
                    drawBrush(e.Graphics);
                }
            }
        }

        private void pbMaskImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (rbBrush.Checked)
            {
                _currentPoints = new PointHistory(AdjustZoomMousePosition(e.Location), BrushSize);
            }
            else if (rbRectangle.Checked)
            {
                _currentRectanglePoint = _startRectanglePoint = e.Location;
                _drawing = true;
            }
        }

        private void pbMaskImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _currentPoints.AddPoint(AdjustZoomMousePosition(e.Location));
                _currentRectanglePoint = e.Location;
                pbMaskImage.Invalidate();
            }
        }

        private void pbMaskImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (rbBrush.Checked)
            {
                if (_inProgessLayer != null)
                {
                    _transparentLayer = MergeBitmaps(_transparentLayer, _inProgessLayer);
                    pbMaskImage.Image = MergeBitmaps(_cameraLayer, AdjustImageOpacity(_transparentLayer, DEFAULT_OPACITY));
                    _inProgessLayer = null;
                }

                if (_currentPoints.GetPoints().Count > 1)
                {
                    _allPointLists.Add(_currentPoints);
                    _currentPoints = new PointHistory();
                }
            }
            else if (rbRectangle.Checked && e.Button == MouseButtons.Left)
            {
                _drawing = false;

                if (_inProgessLayer != null)
                {
                    pbMaskImage.Refresh(); // call paint to draw inprogress rectangle to image
                    _transparentLayer = MergeBitmaps(_transparentLayer, _inProgessLayer);
                    pbMaskImage.Image = MergeBitmaps(_cameraLayer, AdjustImageOpacity(_transparentLayer, DEFAULT_OPACITY));

                    //reset rectangle positions and in progress layer
                    _startRectanglePoint = Point.Empty;      
                    _currentRectanglePoint = Point.Empty;    
                    _inProgessLayer = null;
                }
            }
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
            _allPointLists.Clear();
            
            pbMaskImage.Tag = null;

            //if mask exists, delete it
            if (File.Exists(this._maskfilename))
            {
                File.Delete(this._maskfilename);
            }
            
            ShowImage();
        }

        private void numBrushSize_ValueChanged(object sender, EventArgs e)
        {
            BrushSize = (int) numBrushSize.Value;
        }

        private void numBrushSize_KeyUp(object sender, KeyEventArgs e)
        {
            BrushSize = (int)numBrushSize.Value;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_transparentLayer != null)
                {
                    //save masks at 50% opacity 
                    AdjustImageOpacity(_transparentLayer, DEFAULT_OPACITY).Save(this._maskfilename);
                }

                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex) {
                Global.Log("Error: An error occured saving custom mask with file name: " + _maskfilename + ex.ToString());
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
                _points = new List<Point>();
                BrushSize = 40; 
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
                return _points;
            }

            public void ClearPoints()
            {
                _points.Clear();
            }
        }
    }
}
