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
    public class GaussianBlur : Filter
    {

        #region members



        #endregion

        #region constructors

        public GaussianBlur(double sigma, double size)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.GaussianBlur newFilter = new Af.GaussianBlur();
            newFilter.Sigma = Remap(sigma, 0.5, 5.0);
            newFilter.Size = (int)Remap(size, 3, 21);

            this.filterObject = newFilter;
        }

        public GaussianBlur(GaussianBlur filter) : base(filter)
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
            return "Filter: GaussianBlur";
        }

        #endregion

    }
}