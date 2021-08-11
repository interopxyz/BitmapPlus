using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Adjustments
{
    public class Saturation : Filter
    {

        #region members



        #endregion

        #region constructors

        public Saturation(double adjust)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.SaturationCorrection newFilter = new Af.SaturationCorrection();
            newFilter.AdjustValue = (float)Remap(adjust, -1.0, 1.0);

            filterObject = newFilter;
        }

        public Saturation(Saturation filter) : base(filter)
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
            return "Filter: Saturation";
        }

        #endregion

    }
}