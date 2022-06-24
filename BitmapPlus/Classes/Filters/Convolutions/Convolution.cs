using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Convolutions
{
    public class Convolution : Filter
    {

        #region members



        #endregion

        #region constructors

        public Convolution(Rhino.Geometry.Matrix matrix)
        {
            ImageType = ImageTypes.Rgb24bpp;

            Af.Convolution newFilter = new Af.Convolution(matrix.ToKernel());
            newFilter.ProcessAlpha = true;
            this.filterObject = newFilter;
        }

        public Convolution(Rhino.Geometry.Matrix matrix, int divisor)
        {
            ImageType = ImageTypes.Rgb24bpp;

            Af.Convolution newFilter = new Af.Convolution(matrix.ToKernel(),divisor);
            newFilter.ProcessAlpha = true;
            this.filterObject = newFilter;
        }

        public Convolution(Convolution filter) : base(filter)
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
            return "Filter: Convolution";
        }

        #endregion

    }
}