using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Transform
{
    public class Shrink : Filter
    {

        #region members

        public enum Modes { Bicubic, Bilinear, Nearest }

        #endregion

        #region constructors

        public Shrink(Sd.Color color)
        {
            this.ImageType = ImageTypes.Rgb24bpp;

            Af.Shrink newFilter = new Af.Shrink();
            newFilter.ColorToRemove = color;
            filterObject = newFilter;
        }

        public Shrink(Shrink filter) : base(filter)
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
            return "Filter: Shrink";
        }

        #endregion

    }
}