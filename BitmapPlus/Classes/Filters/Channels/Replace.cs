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
    public class Replace : Filter
    {

        #region members

        public enum Modes { Alpha = 3, Red = 2, Green = 1, Blue = 0, Cb = 5, Cr = 6, Y = 4 };

        #endregion

        #region constructors

        public Replace(Modes mode, Sd.Bitmap bitmap)
        {
            this.ImageType = ImageTypes.ARgb32bpp;

            if ((int)mode > 3)
            {
                Af.YCbCrReplaceChannel newFilter = new Af.YCbCrReplaceChannel((short)(mode - 4), bitmap);
                this.filterObject = newFilter;
            }
            else
            {
                Af.ReplaceChannel newFilter = new Af.ReplaceChannel((short)mode, bitmap);
                this.filterObject = newFilter;
            }

        }

        public Replace(Replace filter) : base(filter)
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
            return "Filter: Replace";
        }

        #endregion

    }
}