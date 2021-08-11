using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Smoothing
{
    public class Median : Filter
    {

        #region members



        #endregion

        #region constructors

        public Median()
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.Median newFilter = new Af.Median();

            filterObject = newFilter;
        }

        public Median(Median filter) : base(filter)
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
            return "Filter: Median";
        }

        #endregion

    }
}