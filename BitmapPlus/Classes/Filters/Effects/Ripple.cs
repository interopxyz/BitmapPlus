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
    public class Ripple : Filter
    {

        #region members



        #endregion

        #region constructors

        public Ripple(int horizontalAmplitude, int horizontalCount, int verticalAmplitude, int verticalCount)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.WaterWave newFilter = new Af.WaterWave();
            newFilter.HorizontalWavesAmplitude = horizontalAmplitude;
            newFilter.HorizontalWavesCount = horizontalCount;
            newFilter.VerticalWavesAmplitude = verticalAmplitude;
            newFilter.VerticalWavesCount = verticalCount;

            this.filterObject = newFilter;
        }

        public Ripple(Ripple filter) : base(filter)
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
            return "Filter: Ripples";
        }

        #endregion

    }
}