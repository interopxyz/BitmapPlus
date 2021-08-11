using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Levels
{
    public class HSL : Filter
    {

        #region members



        #endregion

        #region constructors

        public HSL(Rg.Interval luminanceIn, Rg.Interval luminanceOut, Rg.Interval saturationIn, Rg.Interval saturationOut)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.HSLLinear newFilter = new Af.HSLLinear();
            newFilter.InLuminance = luminanceIn.ToRange();
            newFilter.OutLuminance = luminanceOut.ToRange();
            newFilter.InSaturation = saturationIn.ToRange();
            newFilter.OutSaturation = saturationOut.ToRange();

            filterObject = newFilter;
        }

        public HSL(HSL filter) : base(filter)
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
            return "Filter: HSL";
        }

        #endregion

    }
}