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
    public class GrayWorld : Filter
    {

        #region members



        #endregion

        #region constructors

        public GrayWorld()
        {
            this.ImageType = ImageTypes.Rgb24bpp;

            Af.GrayWorld newFilter = new Af.GrayWorld();

            filterObject = newFilter;
        }

        public GrayWorld(GrayWorld filter) : base(filter)
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
            return "Filter: GrayWorld";
        }

        #endregion

    }
}