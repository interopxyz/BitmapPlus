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
    public class Sepia : Filter
    {

        #region members



        #endregion

        #region constructors

        public Sepia()
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.Sepia newFilter = new Af.Sepia(); 

            filterObject = newFilter;
        }

        public Sepia(Sepia filter) : base(filter)
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
            return "Filter: Sepia";
        }

        #endregion

    }
}