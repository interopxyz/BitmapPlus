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
    public class Gabor : Filter
    {

        #region members



        #endregion

        #region constructors

        public Gabor(double angle, int size, double gamma, double lambda, double psi, double sigma)
        {
            ImageType = ImageTypes.GrayscaleBT709;

            Af.GaborFilter newFilter = new Af.GaborFilter();
            newFilter.Theta = angle;
            newFilter.Size = size;
            newFilter.Gamma = gamma;
            newFilter.Lambda = lambda;
            newFilter.Psi = psi;
            newFilter.Sigma = sigma;

            this.filterObject = newFilter;
        }

        public Gabor(Gabor filter) : base(filter)
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
            return "Filter: Gabor";
        }

        #endregion

    }
}