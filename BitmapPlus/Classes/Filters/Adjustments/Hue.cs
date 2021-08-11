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
    public class Hue : Filter
    {

        #region members



        #endregion

        #region constructors

        public Hue(double hue)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.HueModifier newFilter = new Af.HueModifier();
            newFilter.Hue = (int)Remap(hue, 0, 359);

            filterObject = newFilter;
        }

        public Hue(Hue filter) : base(filter)
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
            return "Filter: Hue";
        }

        #endregion

    }
}