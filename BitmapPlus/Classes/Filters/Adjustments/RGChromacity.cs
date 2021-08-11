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
    public class RGChromacity : Filter
    {

        #region members



        #endregion

        #region constructors

        public RGChromacity()
        {
            ImageType = ImageTypes.Rgb24bpp;

            Af.RGChromacity newFilter = new Af.RGChromacity();

            filterObject = newFilter;
        }

        public RGChromacity(RGChromacity filter) : base(filter)
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
            return "Filter: RGChromacity";
        }

        #endregion

    }
}