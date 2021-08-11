using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Edges
{
    public class Robinson : Filter
    {

        #region members



        #endregion

        #region constructors

        public Robinson()
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.RobinsonEdgeDetector newFilter = new Af.RobinsonEdgeDetector();

            this.filterObject = newFilter;
        }

        public Robinson(Robinson filter) : base(filter)
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
            return "Filter: Robinson";
        }

        #endregion

    }
}