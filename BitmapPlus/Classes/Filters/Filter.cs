using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Af = Accord.Imaging.Filters;

namespace BitmapPlus
{
    public class Filter
    {

        #region members

        public enum ImageTypes { Binary8bpp,GrayscaleBT709, GrayscaleRMY, GrayscaleY, GrayScale16bpp, Rgb16bpp, Rgb24bpp, Rgb32bpp, ARgb32bpp, Rgb48bpp, Rgb64bpp, None };
        public ImageTypes ImageType = ImageTypes.None;

        protected Af.IFilter filterObject = null;

        #endregion

        #region constructors

        public Filter()
        {

        }

        public Filter(Filter filter)
        {
            this.ImageType = filter.ImageType;
            this.filterObject = filter.filterObject;
        }

        #endregion

        #region properties

        public virtual Af.IFilter FilterObject
        {
            get { return filterObject; }
        }

        public bool HasFilter
        {
            get { return (this.filterObject!=null); }
        }

        #endregion

        #region methods

        protected double Remap(double t, double min, double max)
        {
            return (min + (max - min) * t);
        }

        public Filter GetDefault()
        {
            Filter filter = new Filter();
            filter.ImageType = ImageTypes.ARgb32bpp;
            filter.filterObject = null;

            return filter;
        }

        #endregion

    }
}
