using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Edges
{
    public class Canny : Filter
    {

        #region members



        #endregion

        #region constructors

        public Canny(double sigma, int size, int lowThreshold, int highThreshold)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.CannyEdgeDetector newFilter = new Af.CannyEdgeDetector();
            newFilter.GaussianSigma = sigma;
            newFilter.GaussianSize = size;
            newFilter.LowThreshold = (byte)lowThreshold;
            newFilter.HighThreshold = (byte)highThreshold;

            this.filterObject = newFilter;
        }

        public Canny(Canny filter) : base(filter)
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
            return "Filter: Canny";
        }

        #endregion

    }
}