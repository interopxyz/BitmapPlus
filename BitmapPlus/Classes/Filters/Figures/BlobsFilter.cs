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
    public class BlobsFilter : Filter
    {

        #region members



        #endregion

        #region constructors

        public BlobsFilter(Rg.Interval height, Rg.Interval width, bool coupled)
        {
            ImageType = ImageTypes.Rgb24bpp;

            Af.BlobsFiltering newFilter = new Af.BlobsFiltering();
            newFilter.MinWidth = (int)width.T0;
            newFilter.MaxWidth = (int)width.T1;
            newFilter.MinHeight = (int)height.T0;
            newFilter.MaxHeight = (int)height.T1;
            newFilter.CoupledSizeFiltering = coupled;

            this.filterObject = newFilter;
        }

        public BlobsFilter(BlobsFilter filter) : base(filter)
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
            return "Filter: BlobsFilter";
        }

        #endregion

    }
}