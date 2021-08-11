using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sd = System.Drawing;

using Di = SoundInTheory.DynamicImage;
using Ds = SoundInTheory.DynamicImage.Sources;

namespace BitmapPlus
{
    public static class LayerExtensions
    {
        public static Di.Sources.ImageImageSource ToImageImageSource(this Sd.Bitmap input)
        {
            Ds.ImageImageSource output = new Ds.ImageImageSource();
            output.Image = input.ToWriteableBitmap();

            return output;
        }

        public static Di.Color ToLayerColor(this Sd.Color input)
        {
            return Di.Color.FromArgb(input.A, input.R, input.G, input.B);
        }

        public static Di.Fill ToLayerFill(this Sd.Color input)
        {
            Di.Fill fill = new Di.Fill();
            fill.Type = Di.FillType.Solid;
            fill.BackgroundColor = input.ToLayerColor();
            return fill;
        }
    }
}
