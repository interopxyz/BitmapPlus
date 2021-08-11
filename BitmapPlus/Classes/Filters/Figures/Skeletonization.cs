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
    public class Skeletonization : Filter
    {

        #region members



        #endregion

        #region constructors

        public Skeletonization()
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.SimpleSkeletonization newFilter = new Af.SimpleSkeletonization();

            this.filterObject = newFilter;
        }

        public Skeletonization(Skeletonization filter) : base(filter)
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
            return "Filter: Skeletonization";
        }

        #endregion

    }
}