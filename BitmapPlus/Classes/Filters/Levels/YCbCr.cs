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
    public class YCbCr : Filter
    {

        #region members



        #endregion

        #region constructors

        public YCbCr(Rg.Interval yIn, Rg.Interval yOut, Rg.Interval cbIn, Rg.Interval cbOut, Rg.Interval crIn, Rg.Interval crOut)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.YCbCrLinear newFilter = new Af.YCbCrLinear();
            newFilter.InY = yIn.ToIntRange(0, 255);
            newFilter.OutY = yOut.ToIntRange(0, 255);
            newFilter.InCb = cbIn.ToIntRange(0, 255);
            newFilter.OutCb = cbOut.ToIntRange(0, 255);
            newFilter.InCr = crIn.ToIntRange(0, 255);
            newFilter.OutCr = crOut.ToIntRange(0, 255);

            filterObject = newFilter;

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