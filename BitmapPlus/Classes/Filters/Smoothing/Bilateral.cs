using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Smoothing
{
    public class Bilateral : Filter
    {

        #region members



        #endregion

        #region constructors

        public Bilateral(int colorFactor, int colorPower, int spatialFactor, int spatialPower)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.BilateralSmoothing newFilter = new Af.BilateralSmoothing();
            newFilter.ColorFactor = colorFactor;
            newFilter.ColorPower = colorPower;
            newFilter.SpatialFactor = spatialFactor;
            newFilter.SpatialPower = spatialPower;

            filterObject = newFilter;
        }

        public Bilateral(Bilateral filter) : base(filter)
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
            return "Filter: Bilateral";
        }

        #endregion

    }
}