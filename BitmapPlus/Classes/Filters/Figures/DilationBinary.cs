using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Figures
{
    public class DilationBinary : Filter
    {

        #region members



        #endregion

        #region constructors

        public DilationBinary()
        {
            this.ImageType = ImageTypes.GrayscaleBT709;

            Af.BinaryDilation3x3 newFilter = new Af.BinaryDilation3x3();

            this.filterObject = newFilter;
        }

        public DilationBinary(DilationBinary filter) : base(filter)
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
            return "Filter: DilationBinary";
        }

        #endregion

    }
}