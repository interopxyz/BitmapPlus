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
    public class Additive : Filter
    {

        #region members



        #endregion

        #region constructors

        public Additive()
        {
            ImageType = ImageTypes.Rgb24bpp;

            Af.AdditiveNoise newFilter = new Af.AdditiveNoise();

            this.filterObject = newFilter;
        }

        public Additive(Additive filter) : base(filter)
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
            return "Filter: Additive";
        }

        #endregion

    }
}