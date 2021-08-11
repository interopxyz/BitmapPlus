using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Difference
{
    public class Subtract : Filter
    {

        #region members



        #endregion

        #region constructors

        public Subtract(Sd.Bitmap bitmap)
        {
            bitmap = bitmap.ToAccordBitmap(ImageTypes.Rgb32bpp);

            this.ImageType = ImageTypes.Rgb32bpp;

            Af.Subtract newFilter = new Af.Subtract();
            newFilter.OverlayImage = bitmap;

            this.filterObject = newFilter;
        }

        public Subtract(Subtract filter) : base(filter)
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
            return "Filter: Subtract";
        }

        #endregion

    }
}