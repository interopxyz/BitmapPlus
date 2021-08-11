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
    public class Homogeneity : Filter
    {

        #region members



        #endregion

        #region constructors

        public Homogeneity()
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.HomogenityEdgeDetector newFilter = new Af.HomogenityEdgeDetector();

            this.filterObject = newFilter;
        }

        public Homogeneity(Homogeneity filter) : base(filter)
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
            return "Filter: Homogeneity";
        }

        #endregion

    }
}