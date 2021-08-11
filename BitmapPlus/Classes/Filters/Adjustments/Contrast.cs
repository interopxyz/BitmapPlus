using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Adjustments
{
    public class Contrast : Filter
    {

        #region members



        #endregion

        #region constructors

        public Contrast(double factor)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.ContrastCorrection newFilter = new Af.ContrastCorrection();
            newFilter.Factor = (int)Remap(factor, -127, 127); ;

            filterObject = newFilter;
        }

        public Contrast(Contrast filter) : base(filter)
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
            return "Filter: Contrast";
        }

        #endregion

    }
}