using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Effects
{
    public class Posterize : Filter
    {

        #region members



        #endregion

        #region constructors

        public Posterize(double interval)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.SimplePosterization newFilter = new Af.SimplePosterization();
            newFilter.FillingType = Af.SimplePosterization.PosterizationFillingType.Average;
            newFilter.PosterizationInterval = (byte)Remap(interval, 1, 100);

            this.filterObject = newFilter;
        }

        public Posterize(Posterize filter) : base(filter)
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
            return "Filter: Posterize";
        }

        #endregion

    }
}