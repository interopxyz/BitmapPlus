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
    public class Burkes : Filter
    {

        #region members



        #endregion

        #region constructors

        public Burkes(double threshold)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.BurkesDithering newFilter = new Af.BurkesDithering();
            newFilter.ThresholdValue = (byte)Remap(threshold,0,255);

            this.filterObject = newFilter;
        }

        public Burkes(Burkes filter) : base(filter)
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
            return "Filter: Burkes";
        }

        #endregion

    }
}