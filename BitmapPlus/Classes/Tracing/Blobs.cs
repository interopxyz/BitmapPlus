using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Imaging;

using Ag = Accord.Math.Geometry;
using Rg = Rhino.Geometry;

namespace BitmapPlus
{
    public class Blobs
    {
        #region members

        protected Bitmap bitmap = new Bitmap(100, 100);

        public int MinWidth = -1;
        public int MaxWidth = -1;
        public int MinHeight = -1;
        public int MaxHeight = -1;

        public bool Coupled = false;
        public bool MaintainSize = false;
        public Color BackgroundColor = Color.Black;

        protected List<Blob> blobObjects = new List<Blob>();
        protected BlobCounter blobCounter = new BlobCounter();

        #endregion

        #region constructors

        public Blobs()
        {

        }

        public Blobs(Bitmap bitmap)
        {
            this.Bitmap = bitmap;
        }

        public Blobs(Blobs blobs)
        {
            this.MinWidth = blobs.MinWidth;
            this.MaxWidth = blobs.MinWidth;
            this.MinHeight = blobs.MinWidth;
            this.MaxHeight = blobs.MinWidth;

            this.Coupled = blobs.Coupled;
            this.MaintainSize = blobs.MaintainSize;
            this.BackgroundColor = blobs.BackgroundColor;

            foreach (Blob blob in blobs.blobObjects)
            {
                this.blobObjects.Add(blob);
            }

            this.Bitmap = bitmap;
        }

        #endregion

        #region properties

        public virtual Bitmap Bitmap
        {
            get { return (Bitmap)bitmap.Clone(); }
            set { bitmap = (Bitmap)value.ToAccordBitmap(Filter.ImageTypes.Rgb24bpp).Clone(); }
        }

        #endregion

        #region methods

        public void CalculateBlobs(Bitmap bitmap)
        {
            this.Bitmap = bitmap;

            this.CalculateBlobs();
        }

        public void CalculateBlobs()
        {

            blobCounter = new BlobCounter();
            blobCounter.FilterBlobs = true;

            if (MinWidth > 0) blobCounter.MinWidth = MinWidth;
            if (MaxWidth > 0) blobCounter.MaxWidth = MaxWidth;

            if (MinHeight > 0) blobCounter.MinHeight = MinHeight;
            if (MaxHeight > 0) blobCounter.MaxHeight = MaxHeight;

            blobCounter.CoupledSizeFiltering = Coupled;
            blobCounter.BackgroundThreshold = BackgroundColor;

            blobCounter.ProcessImage(bitmap);
            blobObjects = blobCounter.GetObjectsInformation().ToList();
            foreach (Blob blob in blobObjects)
            {
                blobCounter.ExtractBlobsImage(bitmap, blob, MaintainSize);
            }
        }

        public List<Bitmap> GetImages()
        {
            List<Bitmap> output = new List<Bitmap>();
            foreach (Blob blob in blobObjects)
            {
                output.Add(blob.GetBitmap());
            }
            return output;
        }

        public List<Rg.Rectangle3d> GetBoundaries()
        {
            List<Rg.Rectangle3d> output = new List<Rg.Rectangle3d>();
            foreach (Blob blob in blobObjects)
            {
                output.Add(blob.GetRhRect(bitmap.Height));
            }
            return output;
        }

        public List<Color> GetColors()
        {
            List<Color> output = new List<Color>();
            foreach (Blob blob in blobObjects)
            {
                output.Add(blob.ColorMean);
            }
            return output;
        }

        public List<Rg.Point3d> GetPoints()
        {
            List<Rg.Point3d> points = new List<Rg.Point3d>();
            foreach (Blob blob in blobObjects)
            {
                points.AddRange(blobCounter.GetBlobsEdgePoints(blob).ToRhinoPoints(bitmap.Height));
            }

            return points;
        }

        #endregion

    }
}
