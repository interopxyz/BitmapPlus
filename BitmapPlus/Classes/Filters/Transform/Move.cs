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
    public class Move : Filter
    {

        #region members



        #endregion

        #region constructors

        public Move(Sd.Color color, Rg.Vector3d translation)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.CanvasMove newFilter = new Af.CanvasMove(translation.ToIntPt());
            newFilter.FillColorRGB = color;
            filterObject = newFilter;
        }

        public Move(Sd.Color color, Rg.Point3d translation)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.CanvasMove newFilter = new Af.CanvasMove(translation.ToIntPt());
            newFilter.FillColorRGB = color;
            filterObject = newFilter;
        }

        public Move(Sd.Color color, Sd.Point translation)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            Af.CanvasMove newFilter = new Af.CanvasMove(translation.ToIntPt());
            newFilter.FillColorRGB = color;
            filterObject = newFilter;
        }

        public Move(Move filter) : base(filter)
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
            return "Filter: Move";
        }

        #endregion

    }
}