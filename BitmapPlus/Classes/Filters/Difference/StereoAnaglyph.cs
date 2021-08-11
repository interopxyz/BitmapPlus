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
    public class StereoAnaglyph : Filter
    {

        #region members

        public enum Modes { Color, Gray, HalfColor, Optimized, True };

        #endregion

        #region constructors

        public StereoAnaglyph(Sd.Bitmap bitmap, Modes mode)
        {
            bitmap = bitmap.ToAccordBitmap(ImageTypes.Rgb32bpp);

            this.ImageType = ImageTypes.Rgb32bpp;

            Af.StereoAnaglyph newFilter = new Af.StereoAnaglyph();
            newFilter.OverlayImage = bitmap;
            newFilter.AnaglyphAlgorithm = (Af.StereoAnaglyph.Algorithm)mode;

            this.filterObject = newFilter;
        }

        public StereoAnaglyph(StereoAnaglyph filter) : base(filter)
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
            return "Filter: StereoAnaglyph";
        }

        #endregion

    }
}