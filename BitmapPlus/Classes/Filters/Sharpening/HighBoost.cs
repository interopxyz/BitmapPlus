﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Sharpening
{
    public class HighBoost : Filter
    {

        #region members



        #endregion

        #region constructors

        public HighBoost(int divisor, int threshold)
        {
            if (divisor < 1) divisor = 1;

            this.ImageType = ImageTypes.Rgb32bpp;

            Af.HighBoost newFilter = new Af.HighBoost();
            newFilter.Divisor = divisor;
            newFilter.Threshold = threshold;

            filterObject = newFilter;
        }

        public HighBoost(HighBoost filter) : base(filter)
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
            return "Filter: HighBoost";
        }

        #endregion

    }
}