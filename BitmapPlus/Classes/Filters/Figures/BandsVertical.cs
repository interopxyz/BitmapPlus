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
    public class BandsVertical : Filter
    {

        #region members



        #endregion

        #region constructors

        public BandsVertical(int gap, bool borders)
        {
            this.ImageType = ImageTypes.GrayscaleBT709;

            Af.VerticalRunLengthSmoothing newFilter = new Af.VerticalRunLengthSmoothing();
            newFilter.MaxGapSize = gap;
            newFilter.ProcessGapsWithImageBorders = borders;

            this.filterObject = newFilter;
        }

        public BandsVertical(BandsVertical filter) : base(filter)
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
            return "Filter: BandsVertical";
        }

        #endregion

    }
}