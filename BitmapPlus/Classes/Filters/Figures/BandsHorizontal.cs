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
    public class BandsHorizontal : Filter
    {

        #region members



        #endregion

        #region constructors

        public BandsHorizontal(int gap, bool borders)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.HorizontalRunLengthSmoothing newFilter = new Af.HorizontalRunLengthSmoothing();
            newFilter.MaxGapSize = gap;
            newFilter.ProcessGapsWithImageBorders = borders;

            this.filterObject = newFilter;
        }

        public BandsHorizontal(BandsHorizontal filter) : base(filter)
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
            return "Filter: BandsHorizontal";
        }

        #endregion

    }
}