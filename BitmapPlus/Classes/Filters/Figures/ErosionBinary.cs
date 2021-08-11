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
    public class ErosionBinary : Filter
    {

        #region members



        #endregion

        #region constructors

        public ErosionBinary()
        {
            this.ImageType = ImageTypes.GrayscaleBT709;

            Af.BinaryErosion3x3 newFilter = new Af.BinaryErosion3x3();

            this.filterObject = newFilter;
        }

        public ErosionBinary(ErosionBinary filter) : base(filter)
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
            return "Filter: ErosionBinary";
        }

        #endregion

    }
}