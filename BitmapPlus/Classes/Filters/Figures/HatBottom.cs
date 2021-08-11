﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Figures
{
    public class HatBottom : Filter
    {

        #region members



        #endregion

        #region constructors

        public HatBottom()
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.BottomHat newFilter = new Af.BottomHat();

            this.filterObject = newFilter;
        }

        public HatBottom(HatBottom filter) : base(filter)
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
            return "Filter: HatBottom";
        }

        #endregion

    }
}