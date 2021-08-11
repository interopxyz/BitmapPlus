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
    public class YCbCr : Filter
    {

        #region members



        #endregion

        #region constructors

        public YCbCr(Sd.Color color, Rg.Interval y,Rg.Interval cb, Rg.Interval cr, bool outside)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.YCbCrFiltering newFilter = new Af.YCbCrFiltering();
            newFilter.FillColor = Accord.Imaging.YCbCr.FromRGB(new Accord.Imaging.RGB(color));
            newFilter.Y = new Accord.Range((float)y.T0, (float)y.T1);
            newFilter.Cb = new Accord.Range((float)Remap(cb.T0, -0.5, 0.5), (float)Remap(cb.T1, -0.5, 0.5));
            newFilter.Cr = new Accord.Range((float)Remap(cr.T0, -0.5, 0.5), (float)Remap(cr.T1, -0.5, 0.5));
            newFilter.FillOutsideRange = outside;

            this.filterObject = newFilter;
        }

        public YCbCr(YCbCr filter) : base(filter)
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
            return "Filter: YCbCr";
        }

        #endregion

    }
}