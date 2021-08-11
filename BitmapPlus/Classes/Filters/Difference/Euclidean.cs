using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Difference
{
    public class Euclidean : Filter
    {

        #region members



        #endregion

        #region constructors

        public Euclidean(Sd.Bitmap bitmap, double threshold)
        {
            bitmap = bitmap.ToAccordBitmap(ImageTypes.Rgb32bpp);

            this.ImageType = ImageTypes.Rgb32bpp;

            Af.ThresholdedEuclideanDifference newFilter = new Af.ThresholdedEuclideanDifference();
            newFilter.OverlayImage = bitmap;
            newFilter.Threshold = (int)Remap(threshold, 0, 255); ;

            this.filterObject = newFilter;
        }

        public Euclidean(Euclidean filter) : base(filter)
        {
            this.ImageType = filter.ImageType;
            this.filterObject = filter.filterObject;
        }

        #endregion

        #region properties



        #endregion

        #region methods



        #endregion

        #region override

        public override string ToString()
        {
            return "Filter: Euclidean";
        }

        #endregion

    }
}