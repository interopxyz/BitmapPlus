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
    public class Brightness : Filter
    {

        #region members



        #endregion

        #region constructors

        public Brightness(double adjust)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.BrightnessCorrection newFilter = new Af.BrightnessCorrection();
            newFilter.AdjustValue = (int)Remap(adjust, -255, 255);

            filterObject = newFilter;
        }

        public Brightness(Brightness filter) : base(filter)
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
            return "Filter: Brightness";
        }

        #endregion

    }
}