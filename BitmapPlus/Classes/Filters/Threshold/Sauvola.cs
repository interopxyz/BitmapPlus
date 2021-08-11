using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Threshold
{
    public class Sauvola : Filter
    {

        #region members



        #endregion

        #region constructors

        public Sauvola(double r, double k, int radius)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.SauvolaThreshold newFilter = new Af.SauvolaThreshold();
            newFilter.K = k;
            newFilter.R = Remap(r, 0, 255);
            newFilter.Radius = radius;

            filterObject = newFilter;
        }

        public Sauvola(Sauvola filter) : base(filter)
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
            return "Filter: Sauvola";
        }

        #endregion

    }
}