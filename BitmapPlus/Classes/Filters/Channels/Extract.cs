using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Channels
{
    public class Extract : Filter
    {

        #region members

        public enum Modes { Alpha = 3, Red = 2, Green = 1, Blue = 0, Cb = 5, Cr = 6, Y = 4 };

        #endregion

        #region constructors

        public Extract(Modes mode)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            if ((int)mode > 3)
            {
                Af.YCbCrExtractChannel newFilter = new Af.YCbCrExtractChannel();
                newFilter.Channel = (short)(mode - 4);
                this.filterObject = newFilter;
            }
            else
            {
                Af.ExtractChannel newFilter = new Af.ExtractChannel();
                newFilter.Channel = (short)mode;
                this.filterObject = newFilter;
            }

        }

        public Extract(Extract filter) : base(filter)
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
            return "Filter: Extract";
        }

        #endregion

    }
}