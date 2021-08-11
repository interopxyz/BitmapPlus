using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Grayscale
{
    public class Simple : Filter
    {

        #region members



        #endregion

        #region constructors

        public Simple(double red, double green, double blue)
        {
            ImageType = ImageTypes.Rgb32bpp;
          
            Af.Grayscale newFilter = new Af.Grayscale(red, green, blue);

            filterObject = newFilter;
        }

        public Simple(Simple filter) : base(filter)
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
            return "Filter: Simple";
        }

        #endregion

    }
}