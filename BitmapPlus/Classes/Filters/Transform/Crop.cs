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
    public class Crop : Filter
    {

        #region members



        #endregion

        #region constructors

        public Crop(Sd.Color color, Sd.Rectangle rectangle, bool originalSize)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            if (originalSize)
            {
                Af.CanvasCrop newFilter = new Af.CanvasCrop(rectangle);
                newFilter.FillColorRGB = color;

                filterObject = newFilter;
            }
            else
            {
                Af.Crop newFilter = new Af.Crop(rectangle);
                filterObject = newFilter;
            }

        }

        public Crop(Sd.Color color, Rg.Rectangle3d rectangle, bool originalSize)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            if (originalSize)
            {
                Af.CanvasCrop newFilter = new Af.CanvasCrop(rectangle.ToDrawingRect());
                newFilter.FillColorRGB = color;

                filterObject = newFilter;
            }
            else
            {
                Af.Crop newFilter = new Af.Crop(rectangle.ToDrawingRect());
                filterObject = newFilter;
            }

        }

        public Crop(Crop filter) : base(filter)
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
            return "Filter: Crop";
        }

        #endregion

    }
}