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
    public class SIS : Filter
    {

        #region members



        #endregion

        #region constructors

        public SIS()
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.SISThreshold newFilter = new Af.SISThreshold();

            filterObject = newFilter;
        }

        public SIS(SIS filter) : base(filter)
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
            return "Filter: SIS";
        }

        #endregion

    }
}