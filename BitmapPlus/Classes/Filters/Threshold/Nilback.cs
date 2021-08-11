using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Threshold
{
    public class Nilback : Filter
    {

        #region members



        #endregion

        #region constructors

        public Nilback(double k, double c, int radius)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.NiblackThreshold newFilter = new Af.NiblackThreshold();
            newFilter.K = k;
            newFilter.C = Remap(c, 0, 255);
            newFilter.Radius = radius;

            filterObject = newFilter;

        }

        public Nilback(Nilback filter) : base(filter)
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
            return "Filter: Nilback";
        }

        #endregion

    }
}