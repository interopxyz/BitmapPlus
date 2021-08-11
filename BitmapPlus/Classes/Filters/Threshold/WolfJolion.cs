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
    public class WolfJolion : Filter
    {

        #region members



        #endregion

        #region constructors

        public WolfJolion(double r,double k, int radius)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.WolfJolionThreshold newFilter = new Af.WolfJolionThreshold();
            newFilter.K = k;
            newFilter.R = Remap(r, 0, 255);
            newFilter.Radius = radius;

            filterObject = newFilter;

        }

        public WolfJolion(WolfJolion filter) : base(filter)
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
            return "Filter: WolfJolion";
        }

        #endregion

    }
}