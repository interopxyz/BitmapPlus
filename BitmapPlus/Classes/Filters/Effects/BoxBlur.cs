using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Effects
{
    public class BoxBlur : Filter
    {

        #region members



        #endregion

        #region constructors

        public BoxBlur(int horizontal, int vertical)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.FastBoxBlur newFilter = new Af.FastBoxBlur();
            newFilter.HorizontalKernelSize = (byte)horizontal;
            newFilter.VerticalKernelSize = (byte)vertical;

            this.filterObject = newFilter;
        }

        public BoxBlur(BoxBlur filter) : base(filter)
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
            return "Filter: BoxBlur";
        }

        #endregion

    }
}