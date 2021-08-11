using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ai = Accord.Imaging;
using Ag = Accord.Math.Geometry;

using Rg = Rhino.Geometry;

namespace BitmapPlus
{
    public class Corners
    {

        #region members

        protected Bitmap bitmap = new Bitmap(100, 100);

        public int Threshold = -1;
        public double Value = -1;

        #endregion

        #region constructors

        public Corners()
        {

        }

        public Corners(Bitmap bitmap)
        {
            this.Bitmap = bitmap;
        }

        public Corners(Corners corners)
        {
            this.Bitmap = corners.bitmap;
            this.Threshold = corners.Threshold;
            this.Value = corners.Value;
        }

        #endregion

        #region properties

        public Bitmap Bitmap
        {
            get { return (Bitmap)bitmap.Clone(); }
            set { bitmap = (Bitmap)value.ToAccordBitmap(Filter.ImageTypes.Rgb24bpp).Clone(); }
        }

        #endregion

        #region properties

        public List<Rg.Point3d> GetFastCorners()
        {
            Ai.FastCornersDetector corners = new Ai.FastCornersDetector();
            if (Threshold >= 0) corners.Threshold = Threshold;

            return corners.ProcessImage(bitmap).ToRhinoPoints(bitmap.Height);
        }

        public List<Rg.Point3d> GetMorvacCorners()
        {
            Ai.MoravecCornersDetector corners = new Ai.MoravecCornersDetector();
            if (Threshold >= 0) corners.Threshold = Threshold;

            return corners.ProcessImage(bitmap).ToRhinoPoints(bitmap.Height);
        }

        public List<Rg.Point3d> GetHarrisCorners()
        {
            Ai.HarrisCornersDetector corners = new Ai.HarrisCornersDetector();
            if (Threshold >= 0) corners.Threshold = Threshold;
            if (Value >= 0) corners.Sigma = Value;

            return corners.ProcessImage(bitmap).ToRhinoPoints(bitmap.Height);
        }

        public List<Rg.Point3d> GetSusanCorners()
        {
            Ai.SusanCornersDetector corners = new Ai.SusanCornersDetector();
            if (Threshold >= 0) corners.DifferenceThreshold = Threshold;
            if (Value >= 0) corners.GeometricalThreshold = (int)Value;

            return corners.ProcessImage(bitmap).ToRhinoPoints(bitmap.Height);
        }

        #endregion

    }
}
