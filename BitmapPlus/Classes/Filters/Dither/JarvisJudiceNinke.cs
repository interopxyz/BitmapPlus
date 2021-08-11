using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Dither
{
    public class JarvisJudiceNinke : Filter
    {

        #region members



        #endregion

        #region constructors

        public JarvisJudiceNinke(double threshold)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.JarvisJudiceNinkeDithering newFilter = new Af.JarvisJudiceNinkeDithering();
            newFilter.ThresholdValue = (byte)Remap(threshold, 0, 255);

            this.filterObject = newFilter;
        }

        public JarvisJudiceNinke(JarvisJudiceNinke filter) : base(filter)
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
            return "Filter: JarvisJudiceNinke";
        }

        #endregion

    }
}