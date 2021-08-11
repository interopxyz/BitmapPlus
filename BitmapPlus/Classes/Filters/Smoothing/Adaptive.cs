﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Rg = Rhino.Geometry;
using Af = Accord.Imaging.Filters;

namespace BitmapPlus.Filters.Smoothing
{
    public class Adaptive : Filter
    {

        #region members



        #endregion

        #region constructors

        public Adaptive()
        {
            this.ImageType = ImageTypes.Rgb24bpp;

            Af.AdaptiveSmoothing newFilter = new Af.AdaptiveSmoothing();

            filterObject = newFilter;
        }

        public Adaptive(Adaptive filter) : base(filter)
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
            return "Filter: Adaptive";
        }

        #endregion

    }
}