using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Dither
{
    public class Carry : Filter
    {

        #region members



        #endregion

        #region constructors

        public Carry(double threshold)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.ThresholdWithCarry newFilter = new Af.ThresholdWithCarry();
            newFilter.ThresholdValue = (byte)Remap(threshold, 0, 255);

            this.filterObject = newFilter;
        }

        public Carry(Carry filter) : base(filter)
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
            return "Filter: Carry";
        }

        #endregion

    }
}