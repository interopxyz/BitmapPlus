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
    public class FlatField : Filter
    {

        #region members



        #endregion

        #region constructors

        public FlatField(Sd.Bitmap bitmap)
        {
            bitmap = bitmap.ToAccordBitmap(ImageTypes.Rgb24bpp);

            this.ImageType = ImageTypes.Rgb24bpp;

            Af.FlatFieldCorrection newFilter = new Af.FlatFieldCorrection();
            newFilter.BackgoundImage = bitmap;

            this.filterObject = newFilter;
        }

        public FlatField(FlatField filter) : base(filter)
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
            return "Filter: FlatField";
        }

        #endregion

    }
}