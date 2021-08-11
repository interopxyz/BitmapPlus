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
    public class Jitter : Filter
    {

        #region members



        #endregion

        #region constructors

        public Jitter(double radius)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.Jitter newFilter = new Af.Jitter();
            newFilter.Radius = (int)Remap(radius, 1, 10);

            this.filterObject = newFilter;
        }

        public Jitter(Jitter filter) : base(filter)
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
            return "Filter: Jitter";
        }

        #endregion

    }
}