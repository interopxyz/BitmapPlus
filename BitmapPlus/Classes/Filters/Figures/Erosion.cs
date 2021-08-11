using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Figures
{
    public class Erosion : Filter
    {

        #region members



        #endregion

        #region constructors

        public Erosion()
        {
            this.ImageType = ImageTypes.Rgb24bpp;

            Af.Erosion newFilter = new Af.Erosion();

            this.filterObject = newFilter;
        }

        public Erosion(Erosion filter) : base(filter)
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
            return "Filter: Erosion";
        }

        #endregion

    }
}