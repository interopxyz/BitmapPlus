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
    public class Morph : Filter
    {

        #region members



        #endregion

        #region constructors

        public Morph(Sd.Bitmap bitmap, double percent)
        {
            bitmap = bitmap.ToAccordBitmap(ImageTypes.Rgb24bpp);

            this.ImageType = ImageTypes.Rgb24bpp;

            Af.Morph newFilter = new Af.Morph();
            newFilter.OverlayImage = bitmap;
            newFilter.SourcePercent = percent;

            this.filterObject = newFilter;
        }

        public Morph(Morph filter) : base(filter)
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
            return "Filter: Morph";
        }

        #endregion

    }
}