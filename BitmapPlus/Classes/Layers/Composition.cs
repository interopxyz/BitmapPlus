using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Di = SoundInTheory.DynamicImage;

namespace BitmapPlus
{
    public class Composition
    {

        #region members

        protected List<Img> images = new List<Img>();
        public Color Background = Color.Transparent;

        #endregion

        #region constructors

        public Composition()
        {
        }
            public Composition(List<Img> images)
        {
            List<Img> imgs = new List<Img>();
            foreach (Img image in images)
            {
                imgs.Add(new Img(image));
            }
            this.images = imgs;
        }
        public Composition(Img image)
        {
            List<Img> imgs = new List<Img>();
                imgs.Add(new Img(image));

            this.images = imgs;
        }

        public Composition(List<Img> images, Color backgroundColor)
        {
            List<Img> imgs = new List<Img>();
            foreach (Img image in images)
            {
                imgs.Add(new Img(image));
            }
            this.images = imgs;
            this.Background = backgroundColor;
        }

        public Composition(Composition composition)
        {
            List<Img> images = new List<Img>();
            foreach(Img image in composition.images)
            {
                images.Add(new Img(image));
            }
            this.images = images;
            this.Background = composition.Background;
        }

        #endregion

        #region properties



        #endregion

        #region methods

        public void AddImage(Img image)
        {
            images.Add(new Img(image));
        }

        public Img GetImage()
        {
            Di.Composition composition = new Di.Composition();
            composition.Fill.Type = Di.FillType.Solid;
            composition.Fill.BackgroundColor = this.Background.ToLayerColor();
            composition.ImageFormat = Di.DynamicImageFormat.Png;

            foreach (Img image in images)
            {
                Layer layer = image.Layer;
                image.ApplyFilters();
                Di.Layers.ImageLayer imgLayer = new Di.Layers.ImageLayer();
                imgLayer.BlendMode = (Di.BlendMode)layer.BlendMode;
                imgLayer.Source = image.Bmp.ToImageImageSource();

                if (layer.IsMasked)
                {
                    Di.Filters.ClippingMaskFilter mask = new Di.Filters.ClippingMaskFilter();
                    mask.MaskImage = layer.Mask.ToImageImageSource();
                    mask.Enabled = true;
                    imgLayer.Filters.Add(mask);
                }
                if (layer.Opacity < 100.0)
                {
                    Di.Filters.OpacityAdjustmentFilter opacity = new Di.Filters.OpacityAdjustmentFilter();
                    opacity.Opacity = (byte)layer.Opacity;
                    opacity.Enabled = true;
                    imgLayer.Filters.Add(opacity);
                }

                foreach (Modifier modifier in layer.Modifiers)
                {
                    imgLayer.Filters.Add(modifier.GetFilter());
                }

                imgLayer.Filters.Add(GetRotationFilter(layer.Angle));

                int w = image.Width;
                if (layer.Width > 0) w = layer.Width;
                int h = image.Height;
                if (layer.Height > 0) h = layer.Height;
                imgLayer.Filters.Add(GetScaleFilter(w, h, layer.FittingMode));

                imgLayer.X = layer.X;
                imgLayer.Y = layer.Y;

                composition.Layers.Add(imgLayer);
            }
            if (composition.Layers.Count() < 1) return null;
            Bitmap bitmap = composition.GenerateImage().Image.ToBitmap();
            Img img = new Img(bitmap);

            return img;
        }

        private Di.Filters.Filter GetRotationFilter(int angle)
        {
            Di.Filters.RotationFilter rotation = new Di.Filters.RotationFilter
            {

                Angle = angle
            };
            return rotation;
        }

        private Di.Filters.Filter GetScaleFilter(int width, int height, Layer.FittingModes mode)
        {
            Di.Filters.ResizeFilter resize = new Di.Filters.ResizeFilter
            {
                Mode = (Di.Filters.ResizeMode)mode,

                Width = Di.Unit.Pixel(width),
                Height = Di.Unit.Pixel(height)
            };

            return resize;
        }

        private Di.Filters.Filter GetTranslationFilter(Layer layer)
        {
            Di.Filters.ResizeFilter resize = new Di.Filters.ResizeFilter();

            return resize;
        }

        #endregion

    }
}
