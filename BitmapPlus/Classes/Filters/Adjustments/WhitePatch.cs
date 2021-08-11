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
    public class WhitePatch : Filter
    {

        #region members
        


        #endregion

        #region constructors

        public WhitePatch()
        {
            ImageType = ImageTypes.Rgb24bpp;

            Af.WhitePatch newFilter = new Af.WhitePatch();

            this.filterObject = newFilter;
        }

        public WhitePatch(WhitePatch filter) : base(filter)
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
            return "Filter: WhitePatch";
        }

        #endregion

    }
}