using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool
{
    public static class RectangleExtensions
    {

        public static double Area(this Rectangle rect)
        {
            return rect.Width * rect.Height;
        }
        public static double Perimeter(this Rectangle rect)
        {
            return 2 * (rect.Width + rect.Height);
        }
        public static double PercentOfSize(this Rectangle rect, Rectangle compareRect)
        {

            double Diff = compareRect.Area() - rect.Area();
            double DiffInArea = Math.Abs(Diff);   //we dont care if change is positive or negative
            double PercentDifferent = 100 - (DiffInArea / rect.Area() * 100);

            return PercentDifferent;

        }

        public static double IntersectPercent(this Rectangle rect, Rectangle compareRect)
        {

            Rectangle objIntersect = Rectangle.Intersect(rect, compareRect);

            double percentage = ((objIntersect.Width * objIntersect.Height * 2) * 100) /
                                ((compareRect.Width * compareRect.Height) + (compareRect.Width * rect.Height));

            return percentage;
        }
        //I think there is another way of doing this extension so we dont have to use new Rectangle().FromVertices...
        public static Rectangle FromVertices(this Rectangle rect, List<Point> vertices)
        {
            var minX = vertices.Min(p => p.X);
            var minY = vertices.Min(p => p.Y);
            var maxX = vertices.Max(p => p.X);
            var maxY = vertices.Max(p => p.Y);
            return new Rectangle(new Point(minX, minY), new Size(maxX - minX, maxY - minY));

        }

        public static int MidX(this Rectangle rect)
        {
            return rect.Left + rect.Width / 2;
        }
        public static int MidY(this Rectangle rect)
        {
            return rect.Top + rect.Height / 2;
        }
        public static Point Center(this Rectangle rect)
        {
            return new Point(rect.MidX(), rect.MidY());
        }

        public static float MidX(this RectangleF rect)
        {
            return rect.Left + rect.Width / 2;
        }
        public static float MidY(this RectangleF rect)
        {
            return rect.Top + rect.Height / 2;
        }
        public static PointF Center(this RectangleF rect)
        {
            return new PointF(rect.MidX(), rect.MidY());
        }
    }
}
