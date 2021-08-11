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
    public class Stretch : Filter
    {

        #region members



        #endregion

        #region constructors

        public Stretch()
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.ContrastStretch newFilter = new Af.ContrastStretch();

            filterObject = newFilter;
        }

        public Stretch(Stretch filter) : base(filter)
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
            return "Filter: Stretch";
        }

        #endregion

    }
}