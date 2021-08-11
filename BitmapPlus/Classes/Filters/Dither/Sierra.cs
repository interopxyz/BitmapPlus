using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Dither
{
    public class Sierra : Filter
    {

        #region members



        #endregion

        #region constructors

        public Sierra(double threshold)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.SierraDithering newFilter = new Af.SierraDithering();
            newFilter.ThresholdValue = (byte)Remap(threshold, 0, 255);

            this.filterObject = newFilter;
        }

        public Sierra(Sierra filter) : base(filter)
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
            return "Filter: Sierra";
        }

        #endregion

    }
}