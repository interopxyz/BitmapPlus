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
    public class Intersect : Filter
    {

        #region members



        #endregion

        #region constructors

        public Intersect(Sd.Bitmap bitmap)
        {
            bitmap = bitmap.ToAccordBitmap(ImageTypes.Rgb32bpp);

            this.ImageType = ImageTypes.Rgb32bpp;

            Af.Intersect newFilter = new Af.Intersect();
            newFilter.OverlayImage = bitmap;

            this.filterObject = newFilter;
        }

        public Intersect(Intersect filter) : base(filter)
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
            return "Filter: Intersect";
        }

        #endregion

    }
}