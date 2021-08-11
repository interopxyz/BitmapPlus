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
    public class Blur : Filter
    {

        #region members



        #endregion

        #region constructors

        public Blur(double divisor, double threshold)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.Blur newFilter = new Af.Blur();
            newFilter.Divisor = (int)Remap(Math.Abs(divisor), 1, 100);
            newFilter.Threshold = (int)Remap(threshold, 0, 100);

            this.filterObject = newFilter;
        }

        public Blur(Blur filter) : base(filter)
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
            return "Filter: Blur";
        }

        #endregion

    }
}