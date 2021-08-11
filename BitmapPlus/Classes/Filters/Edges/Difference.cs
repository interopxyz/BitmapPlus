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
    public class Difference : Filter
    {

        #region members



        #endregion

        #region constructors

        public Difference()
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.DifferenceEdgeDetector newFilter = new Af.DifferenceEdgeDetector();

            this.filterObject = newFilter;
        }

        public Difference(Difference filter) : base(filter)
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
            return "Filter: Difference";
        }

        #endregion

    }
}