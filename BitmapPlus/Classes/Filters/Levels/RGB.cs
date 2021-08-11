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
    public class RGB : Filter
    {

        #region members



        #endregion

        #region constructors

        public RGB(Rg.Interval redIn, Rg.Interval redOut, Rg.Interval greenIn, Rg.Interval greenOut, Rg.Interval blueIn, Rg.Interval blueOut)
        {
            this.ImageType = ImageTypes.Rgb32bpp;
            
            Af.LevelsLinear newFilter = new Af.LevelsLinear();
            newFilter.InRed = redIn.ToIntRange(0,255);
            newFilter.OutRed = redOut.ToIntRange(0, 255);
            newFilter.InGreen = greenIn.ToIntRange(0, 255);
            newFilter.OutGreen = greenOut.ToIntRange(0, 255);
            newFilter.InBlue = blueIn.ToIntRange(0, 255);
            newFilter.OutBlue = blueOut.ToIntRange(0, 255);

            filterObject = newFilter;

        }

        public RGB(RGB filter) : base(filter)
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
            return "Filter: RGB";
        }

        #endregion

    }
}