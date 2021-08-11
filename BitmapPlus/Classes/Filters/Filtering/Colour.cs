using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Filtering
{
    public class Colour : Filter
    {

        #region members

        public enum Modes { Bicubic, Bilinear, Nearest }

        #endregion

        #region constructors

        public Colour(Sd.Color color, Rg.Interval red, Rg.Interval green, Rg.Interval blue, bool outside)
        {
            ImageType = ImageTypes.Rgb32bpp;

            Af.ColorFiltering newFilter = new Af.ColorFiltering();
            newFilter.FillColor = new Accord.Imaging.RGB(color);
            newFilter.Red = new Accord.IntRange((int)Remap(red.T0, 0, 255), (int)Remap(red.T1, 0, 255));
            newFilter.Green = new Accord.IntRange((int)Remap(green.T0, 0, 255), (int)Remap(green.T1, 0, 255));
            newFilter.Blue = new Accord.IntRange((int)Remap(blue.T0, 0, 255), (int)Remap(blue.T1, 0, 255));
            newFilter.FillOutsideRange = outside;

            this.filterObject = newFilter;
        }

        public Colour(Colour filter) : base(filter)
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
            return "Filter: Color";
        }

        #endregion

    }
}