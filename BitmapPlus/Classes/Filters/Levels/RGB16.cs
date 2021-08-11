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
    public class RGB16 : Filter
    {

        #region members



        #endregion

        #region constructors

        public RGB16(Rg.Interval redIn, Rg.Interval redOut, Rg.Interval greenIn, Rg.Interval greenOut, Rg.Interval blueIn, Rg.Interval blueOut)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.LevelsLinear16bpp newFilter = new Af.LevelsLinear16bpp();
            newFilter.InRed = redIn.ToIntRange();
            newFilter.OutRed = redOut.ToIntRange();
            newFilter.InGreen = greenIn.ToIntRange();
            newFilter.OutGreen = greenOut.ToIntRange();
            newFilter.InBlue = blueIn.ToIntRange();
            newFilter.OutBlue = blueOut.ToIntRange();

            filterObject = newFilter;
        }

        public RGB16(RGB16 filter) : base(filter)
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
            return "Filter: RGB16";
        }

        #endregion

    }
}