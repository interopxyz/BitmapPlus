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
    public class Resize : Filter
    {

        #region members

        public enum Modes { Bicubic, Bilinear, Nearest }

        #endregion

        #region constructors

        public Resize(Modes mode, int width, int height)
        {
            this.ImageType = ImageTypes.Rgb24bpp;

            switch (mode)
            {
                case Modes.Bicubic:
                    Af.ResizeBicubic newFilterA = new Af.ResizeBicubic(width, height);
                    filterObject = newFilterA;
                    break;
                case Modes.Bilinear:
                    Af.ResizeBilinear newFilterB = new Af.ResizeBilinear(width, height);
                    filterObject = newFilterB;
                    break;
                case Modes.Nearest:
                    Af.ResizeNearestNeighbor newFilterC = new Af.ResizeNearestNeighbor(width, height);
                    filterObject = newFilterC;
                    break;
            }
        }

        public Resize(Resize filter) : base(filter)
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
            return "Filter: Resize";
        }

        #endregion

    }
}