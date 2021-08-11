using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Figures
{
    public class SkeletonizationZhangSuen : Filter
    {

        #region members



        #endregion

        #region constructors

        public SkeletonizationZhangSuen()
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.ZhangSuenSkeletonization newFilter = new Af.ZhangSuenSkeletonization();

            this.filterObject = newFilter;
        }

        public SkeletonizationZhangSuen(SkeletonizationZhangSuen filter) : base(filter)
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
            return "Filter: SkeletonizationZhangSuen";
        }

        #endregion

    }
}