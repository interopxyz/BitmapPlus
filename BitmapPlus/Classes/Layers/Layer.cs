using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapPlus
{

    public class Layer
    {

        #region members

        public enum BlendModes { Normal = 0, Dissolve = 1, Multiply = 2, Screen = 3, Overlay = 4, Darken = 5, Lighten = 6, ColorDodge = 7, ColorBurn = 8, LinearDodge = 9, LinearBurn = 10, LighterColor = 11, DarkerColor = 12, HardLight = 13, SoftLight = 14, LinearLight = 16, PinLight = 17, Difference = 19, Exclusion = 20, Color = 23, Luminosity = 24 }
        public BlendModes BlendMode = BlendModes.Normal;

        public enum FittingModes { UseWidth = 0, UseHeight = 1, Fill = 2, Uniform = 3, UniformFill = 4 }
        public FittingModes FittingMode = FittingModes.Fill;

        private Bitmap mask = null;
        public double Opacity = 100.0;

        public int Angle = 0;

        public int X = 0;
        public int Y = 0;

        protected int width = 0;
        protected int height = 0;

        public List<Modifier> Modifiers = new List<Modifier>();

        #endregion

        #region constructors

        public Layer()
        {

        }

        public Layer(Bitmap mask)
        {
            this.mask = (Bitmap)mask.Clone();
        }

        public Layer(Layer layer)
        {
            this.BlendMode = layer.BlendMode;
            this.Mask = layer.Mask;
            this.Opacity = layer.Opacity;

            this.Angle = layer.Angle;

            this.X = layer.X;
            this.Y = layer.Y;

            this.width = layer.width;
            this.height = layer.height;

            this.FittingMode = layer.FittingMode;

            foreach (Modifier modifier in layer.Modifiers)
            {
                this.Modifiers.Add(new Modifier(modifier));
            }
        }

        #endregion

        #region properties

        public virtual Bitmap Mask
        {
            get {
                if (this.IsMasked)
                {
                    return (Bitmap)mask.Clone();
                }
                else
                {
                    return null;
                };
            }
            set {
                mask = checkValid(value); }
            }
        
        private Bitmap checkValid(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                return (Bitmap)bitmap.Clone();
            }
            else
            {
                return null;
            }
        }

        public virtual bool IsMasked
        {
            get { return mask != null; }
        }

        public virtual int Width
        {
            get { return width; }
            set
            {
                width = value;
            }
        }

        public virtual int Height
        {
            get { return height; }
            set
            {
                height = value;
            }
        }

        #endregion

        #region methods

        public override string ToString()
        {
            string text = "";
            if (IsMasked) text += "Masked ";
            text += "Layer";
            if (BlendMode != BlendModes.Normal) text += ": " + BlendMode.ToString();
            if (Modifiers.Count > 0) text += " [" + Modifiers.Count + " Modifiers]";

            return text;
        }


        #endregion

    }
}
