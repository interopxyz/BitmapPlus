using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Filtering
{
    public class HSL : Filter
    {

        #region members



        #endregion

        #region constructors

        public HSL(Sd.Color color, Rg.Interval hue, Rg.Interval saturation, Rg.Interval luminance, bool outside)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.HSLFiltering newFilter = new Af.HSLFiltering();
            newFilter.FillColor = Accord.Imaging.HSL.FromRGB(new Accord.Imaging.RGB(color));
            newFilter.Hue = new Accord.IntRange((int)Remap(hue.T0, 0, 359), (int)Remap(hue.T1, 0, 359));
            newFilter.Saturation = new Accord.Range((float)saturation.T0, (float)saturation.T1);
            newFilter.Luminance = new Accord.Range((float)luminance.T0, (float)luminance.T1);
            newFilter.FillOutsideRange = outside;

            this.filterObject = newFilter;
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