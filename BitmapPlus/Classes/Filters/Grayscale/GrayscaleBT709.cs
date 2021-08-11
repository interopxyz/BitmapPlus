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
    public class GrayscaleBT709 : Filter
    {

        #region members



        #endregion

        #region constructors

        public GrayscaleBT709()
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.Grayscale newFilter = Af.Grayscale.CommonAlgorithms.BT709;

            filterObject = newFilter;
        }

        public GrayscaleBT709(GrayscaleBT709 filter) : base(filter)
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
            return "Filter: GrayscaleBT709";
        }

        #endregion

    }
}