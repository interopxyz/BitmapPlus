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
    public class BlobsUnique : Filter
    {

        #region members



        #endregion

        #region constructors

        public BlobsUnique(Rg.Interval height, Rg.Interval width, bool coupled, bool blobs)
        {
            this.ImageType = ImageTypes.GrayscaleBT709;

            Af.ConnectedComponentsLabeling newFilter = new Af.ConnectedComponentsLabeling();
            newFilter.MinWidth = (int)width.T0;
            newFilter.MaxWidth = (int)width.T1;
            newFilter.MinHeight = (int)height.T0;
            newFilter.MaxHeight = (int)height.T1;
            newFilter.CoupledSizeFiltering = coupled;
            newFilter.FilterBlobs = blobs;

            this.filterObject = newFilter;
        }

        public BlobsUnique(BlobsUnique filter) : base(filter)
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
            return "Filter: BlobsUnique";
        }

        #endregion

    }
}