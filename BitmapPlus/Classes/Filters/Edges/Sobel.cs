using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Edges
{
    public class Sobel : Filter
    {

        #region members



        #endregion

        #region constructors

        public Sobel(bool intensity)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.SobelEdgeDetector newFilter = new Af.SobelEdgeDetector();
            newFilter.ScaleIntensity = intensity;

            this.filterObject = newFilter;
        }

        public Sobel(Sobel filter) : base(filter)
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
            return "Filter: Sobel";
        }

        #endregion

    }
}