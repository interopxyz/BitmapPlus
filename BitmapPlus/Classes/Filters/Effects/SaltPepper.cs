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
    public class SaltPepper : Filter
    {

        #region members



        #endregion

        #region constructors

        public SaltPepper(double noise)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.SaltAndPepperNoise newFilter = new Af.SaltAndPepperNoise();
            newFilter.NoiseAmount = Remap(noise, 0, 100);

            this.filterObject = newFilter;
        }

        public SaltPepper(SaltPepper filter) : base(filter)
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
            return "Filter: SaltPepper";
        }

        #endregion

    }
}