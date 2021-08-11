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
    public class Iterative : Filter
    {

        #region members



        #endregion

        #region constructors

        public Iterative(double minimum, double threshold)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.IterativeThreshold newFilter = new Af.IterativeThreshold();
            newFilter.MinimumError = (int)Remap(minimum, 0, 10);
            newFilter.ThresholdValue = (int)Remap(threshold, 0, 255);

            filterObject = newFilter;

        }

        public Iterative(Iterative filter) : base(filter)
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
            return "Filter: Iterative";
        }

        #endregion

    }
}