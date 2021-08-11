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
    public class ErosionWatershed : Filter
    {

        #region members

        public enum Modes { Chessboard, Euclidean, Manhattan, SquaredEuclidean }

        #endregion

        #region constructors

        public ErosionWatershed(Modes mode, double tolerance)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.BinaryWatershed newFilter = new Af.BinaryWatershed();
            newFilter.Distance = (Af.DistanceTransformMethod)mode;
            newFilter.Tolerance = (float)tolerance;

            this.filterObject = newFilter;
        }

        public ErosionWatershed(ErosionWatershed filter) : base(filter)
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
            return "Filter: ErosionWatershed";
        }

        #endregion

    }
}