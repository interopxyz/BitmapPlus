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
    public class Kuwahara : Filter
    {

        #region members



        #endregion

        #region constructors

        public Kuwahara(double size)
        {
            ImageType = ImageTypes.GrayscaleBT709;
            if (size < 0) size = 0;
            Af.Kuwahara newFilter = new Af.Kuwahara();
            newFilter.Size = makeOdd(size);

            this.filterObject = newFilter;
        }

        public int makeOdd(double value)
        {
            int val = (int)(5 + ((value % 1.0) * 95));
            return val + ((val + 1) % 2);
        }

        public Kuwahara(Kuwahara filter) : base(filter)
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
            return "Filter: Kuwahara";
        }

        #endregion

    }
}