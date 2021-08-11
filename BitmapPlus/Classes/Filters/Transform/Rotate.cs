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
    public class Rotate : Filter
    {

        #region members

        public enum Modes { Bicubic, Bilinear, Nearest }

        #endregion

        #region constructors

        public Rotate(Sd.Color color, Modes mode, double angle, bool keepSize)
        {
            this.ImageType = ImageTypes.Rgb24bpp;

            switch (mode)
            {
                case Modes.Bicubic:
                    Af.RotateBicubic newFilterA = new Af.RotateBicubic(angle);
                    newFilterA.FillColor = color;
                    newFilterA.KeepSize = keepSize;
                    filterObject = newFilterA;
                    break;
                case Modes.Bilinear:
                    Af.RotateBilinear newFilterB = new Af.RotateBilinear(angle);
                    newFilterB.FillColor = color;
                    newFilterB.KeepSize = keepSize;
                    filterObject = newFilterB;
                    break;
                case Modes.Nearest:
                    Af.RotateNearestNeighbor newFilterC = new Af.RotateNearestNeighbor(angle);
                    newFilterC.FillColor = color;
                    newFilterC.KeepSize = keepSize;
                    filterObject = newFilterC;
                    break;
            }
        }

        public Rotate(Rotate filter) : base(filter)
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
            return "Filter: Rotate";
        }

        #endregion

    }
}