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
    public class Variance : Filter
    {

        #region members



        #endregion

        #region constructors

        public Variance(bool fast, int radius)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            if (fast)
            {
                Af.FastVariance newFilter = new Af.FastVariance();
                newFilter.Radius = radius;
                this.filterObject = newFilter;
            }
            else
            {
                Af.Variance newFilter = new Af.Variance();
                newFilter.Radius = radius;
                this.filterObject = newFilter;
            }
        }

        public Variance(Variance filter) : base(filter)
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
            return "Filter: Variance";
        }

        #endregion

    }
}