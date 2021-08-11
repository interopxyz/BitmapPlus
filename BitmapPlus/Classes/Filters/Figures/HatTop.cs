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
    public class HatTop : Filter
    {

        #region members



        #endregion

        #region constructors

        public HatTop()
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.TopHat newFilter = new Af.TopHat();

            this.filterObject = newFilter;
        }

        public HatTop(HatTop filter) : base(filter)
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
            return "Filter: HatTop";
        }

        #endregion

    }
}