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
    public class Simple : Filter
    {

        #region members



        #endregion

        #region constructors

        public Simple(int threshold)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.Threshold newFilter = new Af.Threshold();
            newFilter.ThresholdValue = threshold;

            filterObject = newFilter;
        }

        public Simple(Simple filter) : base(filter)
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
            return "Filter: Simple";
        }

        #endregion

    }
}