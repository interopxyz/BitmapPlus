using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Threshold
{
    public class Bradley : Filter
    {

        #region members



        #endregion

        #region constructors

        public Bradley(double brightness, int window)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.BradleyLocalThresholding newFilter = new Af.BradleyLocalThresholding();
            newFilter.PixelBrightnessDifferenceLimit = (float)brightness;
            newFilter.WindowSize = window;

            filterObject = newFilter;
        }

        public Bradley(Bradley filter) : base(filter)
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
            return "Filter: Bradley";
        }

        #endregion

    }
}