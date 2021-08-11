using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Dither
{
    public class Bayer : Filter
    {

        #region members



        #endregion

        #region constructors

        public Bayer()
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.BayerDithering newFilter = new Af.BayerDithering();

            this.filterObject = newFilter;
        }

        public Bayer(Bayer filter) : base(filter)
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
            return "Filter: Bayer";
        }

        #endregion

    }
}