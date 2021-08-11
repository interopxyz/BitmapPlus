using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Grayscale
{
    public class GrayscaleRMY : Filter
    {

        #region members



        #endregion

        #region constructors

        public GrayscaleRMY()
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.Grayscale newFilter = Af.Grayscale.CommonAlgorithms.RMY;

            filterObject = newFilter;
        }

        public GrayscaleRMY(GrayscaleRMY filter) : base(filter)
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
            return "Filter: GrayscaleRMY";
        }

        #endregion

    }
}