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
    public class Pixellate : Filter
    {

        #region members



        #endregion

        #region constructors

        public Pixellate(double width, double height)
        {
            ImageType = ImageTypes.Rgb24bpp;

            Af.Pixellate newFilter = new Af.Pixellate();
            newFilter.PixelWidth = (int)Remap(width, 2, 32);
            newFilter.PixelHeight = (int)Remap(height, 2, 32);

            this.filterObject = newFilter;
        }

        public Pixellate(Pixellate filter) : base(filter)
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
            return "Filter: Pixellate";
        }

        #endregion

    }
}