using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Di = SoundInTheory.DynamicImage;
using Df = SoundInTheory.DynamicImage.Filters;


namespace BitmapPlus
{
    public class Modifier
    {

        #region members

        public enum ModifierModes { None = 0, Invert = 1, Solarize = 2, Grayscale = 3, Sepia = 4, Vignette = 5, Emboss = 6, Brightness = 7, Contrast = 8, Gaussian = 9, Border = 10, ColorTint = 11, ColorKey = 12 }
        public ModifierModes ModifierMode = ModifierModes.Invert;

        public double Value = 0;
        public Color Color = Color.Transparent;

        #endregion

        #region constructors

        public Modifier()
        {

        }

        public Modifier(ModifierModes modifierMode)
        {
            this.ModifierMode = modifierMode;
        }

        public Modifier(Modifier modifier)
        {
            this.ModifierMode = modifier.ModifierMode;
            this.Value = modifier.Value;
            this.Color = modifier.Color;
        }

        #endregion

        #region properties

        public Df.Filter GetFilter()
        {
            switch (this.ModifierMode)
            {
                default:
                    return GetInversionFilter();
                case ModifierModes.Border:
                    return GetBorderFilter();
                case ModifierModes.Brightness:
                    return GetBrightnessFilter();
                case ModifierModes.Contrast:
                    return GetContrastFilter();
                case ModifierModes.Emboss:
                    return GetEmbossFilter();
                case ModifierModes.Gaussian:
                    return GetGaussianFilter();
                case ModifierModes.Grayscale:
                    return GetGrayscaleFilter();
                case ModifierModes.Sepia:
                    return GetSepiaFilter();
                case ModifierModes.Solarize:
                    return GetSolarizeFilter();
                case ModifierModes.ColorTint:
                    return GetColorTintFilter();
                case ModifierModes.ColorKey:
                    return GetColorKeyFilter();
                case ModifierModes.Vignette:
                    return GetVignetteFilter();
            }
        }

        #endregion

        #region methods

        private Df.Filter GetBorderFilter()
        {
            Df.BorderFilter filter = new Df.BorderFilter();

            filter.Fill = Color.ToLayerFill();
            filter.Width = (int)Value;

            filter.Enabled = true;
            return filter;
        }

        private Df.Filter GetBrightnessFilter()
        {
            Df.BrightnessAdjustmentFilter filter = new Df.BrightnessAdjustmentFilter();
            filter.Level = (int)Value;

            filter.Enabled = true;
            return filter;
        }

        private Df.Filter GetVignetteFilter()
        {
            Df.VignetteFilter filter = new Df.VignetteFilter();

            filter.Enabled = true;
            return filter;
        }

        private Df.Filter GetSolarizeFilter()
        {
            Df.SolarizeFilter filter = new Df.SolarizeFilter();

            filter.Enabled = true;
            return filter;
        }

        private Df.Filter GetSepiaFilter()
        {
            Df.SepiaFilter filter = new Df.SepiaFilter();

            filter.Enabled = true;
            return filter;
        }

        private Df.Filter GetInversionFilter()
        {
            Df.InversionFilter filter = new Df.InversionFilter();

            filter.Enabled = true;
            return filter;
        }

        private Df.Filter GetGrayscaleFilter()
        {
            Df.GrayscaleFilter filter = new Df.GrayscaleFilter();

            filter.Enabled = true;
            return filter;
        }

        private Df.Filter GetGaussianFilter()
        {
            Df.GaussianBlurFilter filter = new Df.GaussianBlurFilter();
            filter.Radius = (float)(Value % 20);

            filter.Enabled = true;
            return filter;
        }

        private Df.Filter GetEmbossFilter()
        {
            Df.EmbossFilter filter = new Df.EmbossFilter();
            filter.Amount = (float)Value;

            filter.Enabled = true;
            return filter;
        }

        private Df.Filter GetContrastFilter()
        {
            Df.ContrastAdjustmentFilter filter = new Df.ContrastAdjustmentFilter();
            filter.Level = (int)Value;

            filter.Enabled = true;
            return filter;
        }

        private Df.Filter GetColorTintFilter()
        {
            Df.ColorTintFilter filter = new Df.ColorTintFilter();
            filter.Amount = (int)Value;
            filter.Color = Color.ToLayerColor();

            filter.Enabled = true;
            return filter;
        }

        private Df.Filter GetColorKeyFilter()
        {
            Df.ColorKeyFilter filter = new Df.ColorKeyFilter();
            filter.ColorTolerance = (byte)Value;
            filter.Color = Color.ToLayerColor();

            filter.Enabled = true;
            return filter;
        }

        #endregion

    }
}
