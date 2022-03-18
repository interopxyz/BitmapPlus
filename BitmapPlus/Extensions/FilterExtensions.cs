using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Ac = Accord;
using Ai = Accord.Imaging;
using Rg = Rhino.Geometry;

namespace BitmapPlus
{
    public static class FilterExtensions
    {

        public static Rg.Rectangle3d GetRhRect(this Ai.Blob input, int transposition = 0)
        {
            return input.Rectangle.ToRhinoRectangle(transposition);
        }
        public static Rg.Rectangle3d ToRhinoRectangle(this System.Drawing.Rectangle input, int transposition = 0)
        {
            return new Rg.Rectangle3d(Rg.Plane.WorldXY, new Rg.Interval(input.Left, input.Right), new Rg.Interval(transposition - input.Bottom, transposition - input.Top));
        }

        public static Sd.Bitmap GetBitmap(this Ai.Blob input)
        {
            return (Sd.Bitmap)input.Image.ToManagedImage().Clone();
        }

        public static Rg.Polyline ToPolyline(this List<Rg.Point3d> input, bool isClosed = false)
        {
            Rg.Polyline polyline = new Rg.Polyline(input);
            if (isClosed) polyline.Add(input[0]);

            return polyline;
        }

        public static Rg.Point3d ToRhPoint(this Ac.IntPoint input, int transposition = 0)
        {
            return new Rg.Point3d(input.X, transposition - input.Y, 0);
        }

        public static Rg.Point3d ToRhPoint(this Ac.Point input, int transposition = 0)
        {
            return new Rg.Point3d(input.X, transposition - input.Y, 0);
        }
        public static List<Rg.Point3d> ToRhinoPoints(this List<Ac.IntPoint> input, int transposition = 0)
        {
            List<Rg.Point3d> points = new List<Rg.Point3d>();

            foreach (Ac.IntPoint point in input)
            {
                points.Add(point.ToRhPoint(transposition));
            }
            return points;
        }

        public static Ac.IntPoint ToIntPt(this Rg.Vector3d input)
        {
            return new Ac.IntPoint((int)input.X, (int)input.Y);
        }

        public static Ac.IntPoint ToIntPt(this Rg.Point3d input)
        {
            return new Ac.IntPoint((int)input.X, (int)input.Y);
        }

        public static Ac.IntPoint ToIntPt(this Sd.Point input)
        {
            return new Ac.IntPoint(input.X, input.Y);
        }

        public static Ac.Range ToRange(this Rg.Interval input)
        {
            return new Ac.Range((float)input.T0, (float)input.T1);
        }

        public static Ac.Range ToRange(this Rg.Interval input, double min, double max)
        {
            return new Ac.Range((float)input.T0.Remap(min,max), (float)input.T1.Remap(min,max));
        }

        public static Ac.IntRange ToIntRange(this Rg.Interval input)
        {
            return new Ac.IntRange((int)input.T0, (int)input.T1);
        }

        public static Ac.IntRange ToIntRange(this Rg.Interval input, int min, int max)
        {
            return new Ac.IntRange(input.T0.RemapInt(min, max), input.T1.RemapInt(min, max));
        }

        private static int RemapInt(this double t, int min, int max)
        {
            return (int)(min + (max - min) * t);
        }
        private static double Remap(this double t, double min, double max)
        {
            return (min + (max - min) * t);
        }

        public static Sd.Rectangle ToDrawingRect(this Rg.Rectangle3d input)
        {
            return new Sd.Rectangle((int)input.Corner(0).X, (int)input.Corner(0).Y, (int)input.Width, (int)input.Height);
        }

        public static Sd.Rectangle ToDrawingRect(this Rg.Rectangle3d input, int height)
        {
            return new Sd.Rectangle((int)input.Corner(0).X, height-(int)input.Corner(0).Y- (int)input.Height, (int)input.Width, (int)input.Height);
        }

        public static Sd.Bitmap ToAccordBitmap(this Sd.Bitmap input, Filter.ImageTypes imageType)
        {
            switch (imageType)
            {
                default:
                    return (Sd.Bitmap)input.Clone();
                case Filter.ImageTypes.Binary8bpp:
                    var niblack = new Ai.Filters.NiblackThreshold();
                    Sd.Bitmap bitmap = niblack.Apply((Sd.Bitmap)input.Clone());
                    return bitmap;
                case Filter.ImageTypes.GrayScale16bpp:
                    return Ai.Image.Clone(input, Sd.Imaging.PixelFormat.Format16bppGrayScale);
                case Filter.ImageTypes.GrayscaleBT709:
                    if (input.PixelFormat != Sd.Imaging.PixelFormat.Format8bppIndexed)
                    {
                        return Ai.Filters.Grayscale.CommonAlgorithms.BT709.Apply((Sd.Bitmap)input.Clone());
                    }
                    else
                    {
                        return (Sd.Bitmap)input.Clone();
                    }
                case Filter.ImageTypes.GrayscaleRMY:
                    return Ai.Filters.Grayscale.CommonAlgorithms.RMY.Apply((Sd.Bitmap)input.Clone());
                case Filter.ImageTypes.GrayscaleY:
                    return Ai.Filters.Grayscale.CommonAlgorithms.Y.Apply((Sd.Bitmap)input.Clone());
                case Filter.ImageTypes.Rgb16bpp:
                    return Ai.Image.Clone(input, Sd.Imaging.PixelFormat.Format16bppRgb555);
                case Filter.ImageTypes.Rgb24bpp:
                    return Ai.Image.Clone(input, Sd.Imaging.PixelFormat.Format24bppRgb);
                case Filter.ImageTypes.Rgb32bpp:
                    return Ai.Image.Clone(input, Sd.Imaging.PixelFormat.Format32bppRgb);
                case Filter.ImageTypes.ARgb32bpp:
                    return Ai.Image.Clone(input, Sd.Imaging.PixelFormat.Format32bppArgb);
                case Filter.ImageTypes.Rgb48bpp:
                    return Ai.Image.Clone(input, Sd.Imaging.PixelFormat.Format48bppRgb);
                case Filter.ImageTypes.Rgb64bpp:
                    return Ai.Image.Clone(input, Sd.Imaging.PixelFormat.Format64bppArgb);
            }

        }

    }
}
