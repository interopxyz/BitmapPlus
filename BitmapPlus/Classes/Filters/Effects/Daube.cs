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
    public class Daube : Filter
    {

        #region members



        #endregion

        #region constructors

        public Daube(double size)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.OilPainting newFilter = new Af.OilPainting();
            newFilter.BrushSize = (int)Remap(size, 1, 21);

            this.filterObject = newFilter;
        }

        public Daube(Daube filter) : base(filter)
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
            return "Filter: Daube";
        }

        #endregion

    }
}