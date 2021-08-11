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
    public class Closing : Filter
    {

        #region members



        #endregion

        #region constructors

        public Closing()
        {
            this.ImageType = ImageTypes.Rgb24bpp;

            Af.Closing newFilter = new Af.Closing();

            this.filterObject = newFilter;
        }

        public Closing(Closing filter) : base(filter)
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
            return "Filter: Closing";
        }

        #endregion

    }
}