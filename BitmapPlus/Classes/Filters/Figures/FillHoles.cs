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
    public class FillHoles : Filter
    {

        #region members



        #endregion

        #region constructors

        public FillHoles(int width, int height, bool filtered)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.FillHoles newFilter = new Af.FillHoles();
            newFilter.MaxHoleWidth = width;
            newFilter.MaxHoleHeight = height;
            newFilter.CoupledSizeFiltering = filtered;

            this.filterObject = newFilter;
        }

        public FillHoles(FillHoles filter) : base(filter)
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
            return "Filter: FillHoles";
        }

        #endregion

    }
}