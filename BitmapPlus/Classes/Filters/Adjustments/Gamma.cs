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
    public class Gamma : Filter
    {

        #region members



        #endregion

        #region constructors

        public Gamma(double gamma)
        {
            this.ImageType = ImageTypes.Rgb24bpp;

            Af.GammaCorrection newFilter = new Af.GammaCorrection();
            newFilter.Gamma = Remap(gamma, 0.1, 5.0);

            filterObject = newFilter;
        }

        public Gamma(Gamma filter) : base(filter)
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
            return "Filter: Gamma";
        }

        #endregion

    }
}