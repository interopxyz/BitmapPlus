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
    public class MoveTowards : Filter
    {

        #region members



        #endregion

        #region constructors

        public MoveTowards(Sd.Bitmap bitmap, double size)
        {
            bitmap = bitmap.ToAccordBitmap(ImageTypes.Rgb32bpp);

            this.ImageType = ImageTypes.Rgb32bpp;

            Af.MoveTowards newFilter = new Af.MoveTowards();
            newFilter.OverlayImage = bitmap;
            newFilter.StepSize = (int)Remap(size, 0, 255); ;

            this.filterObject = newFilter;
        }

        public MoveTowards(MoveTowards filter) : base(filter)
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
            return "Filter: MoveTowards";
        }

        #endregion

    }
}