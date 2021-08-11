using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Transform
{
    public class Mirror : Filter
    {

        #region members



        #endregion

        #region constructors

        public Mirror(bool aboutX, bool aboutY)
        {
            this.ImageType = ImageTypes.Rgb24bpp;

            Af.Mirror newFilter = new Af.Mirror(aboutX, aboutY);

            filterObject = newFilter;
        }

        public Mirror(Mirror filter) : base(filter)
        {
        }

        #endregion

        #region properties



        #endregion

        #region methods



        #endregion

        #region override

        public override string ToString()
        {
            return "Filter: Mirror";
        }

        #endregion

    }
}