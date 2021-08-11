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
    public class Polar : Filter
    {

        #region members



        #endregion

        #region constructors

        public Polar(bool toPolar, double depth, double angle, bool direction, bool vertical)
        {
            this.ImageType = ImageTypes.Rgb32bpp;

            if (toPolar)
            {
                Af.TransformToPolar newFilter = new Af.TransformToPolar();
                newFilter.CirlceDepth = depth;
                newFilter.OffsetAngle = angle;
                newFilter.MapFromTop = vertical;
                newFilter.MapBackwards = direction;
                filterObject = newFilter;
            }
            else
            {
                Af.TransformFromPolar newFilter = new Af.TransformFromPolar();
                newFilter.CirlceDepth = depth;
                newFilter.OffsetAngle = angle;
                newFilter.MapFromTop = vertical;
                newFilter.MapBackwards = direction;
                filterObject = newFilter;
            }
        }

        public Polar(Polar filter) : base(filter)
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
            return "Filter: Polar";
        }

        #endregion

    }
}