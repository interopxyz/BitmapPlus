using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Af = Accord.Imaging.Filters;

using Grasshopper.Kernel.Types;
using GH_IO.Serialization;

namespace BitmapPlus
{
    public class Img : IGH_Goo
    {
        #region members
        public enum Channels { Alpha =0 , Red=1, Green=2, Blue=3, Hue=4, Saturation=5, Luminance=6 };

        protected Bitmap bitmap = new Bitmap(100, 100, PixelFormat.Format32bppArgb);
        public Layer Layer = new Layer();
        public List<Filter> Filters = new List<Filter>();
        public int FilterIterations = 0;

        #endregion

        #region constructors

        public Img()
        {

        }

        public Img(Img img)
        {
            if (img.bitmap != null) this.bitmap = (Bitmap)img.bitmap.Clone();
            this.Layer = new Layer(img.Layer);
            this.FilterIterations = img.FilterIterations;
            List<Filter> temp = new List<Filter>();
            foreach(Filter filter in img.Filters)
            {
                temp.Add(filter);
            }
            this.Filters = temp;
        }

        public Img(Bitmap bitmap, Layer layer, int filterIterations, List<Filter> filters)
        {
            if (bitmap != null) this.bitmap = (Bitmap)bitmap.Clone();
            if (layer != null) this.Layer = new Layer(layer);
            this.FilterIterations = filterIterations;
            List<Filter> temp = new List<Filter>();
            foreach (Filter filter in filters)
            {
                temp.Add(filter);
            }
            this.Filters = temp;
        }

        public Img(Bitmap bitmap)
        {
            this.bitmap = (Bitmap)bitmap.Clone();
        }

        public Img(List<Color> colors, int width)
        {
            int height = (int)Math.Ceiling((double)(colors.Count / width));
            BuildBitmap(colors, width, height);
        }

        public Img(List<Color> colors, int width, int height)
        {
            BuildBitmap(colors, width, height);
        }

        #endregion

        #region properties

        public virtual Bitmap Bmp
        {
            get { return (Bitmap)this.bitmap.Clone(); }
            set { this.bitmap = (Bitmap)value.Clone(); }
        }

        public virtual int Width
        {
            get { return this.bitmap.Width; }
        }

        public virtual int Height
        {
            get { return this.bitmap.Height; }
        }

        public virtual int Resolution
        {
            get { return (int)this.bitmap.HorizontalResolution; }
        }

        public virtual List<Color> Colors
        {
            get { return GetColors(); }
        }

        #endregion

        #region methods

        public Bitmap GetFlatBitmap()
        {
            Img img = new Img(this);
            img.Flatten();

            return img.Bmp;
        }

        public void Flatten()
        {
            ApplyFilters();
            Composition composition = new Composition(new Img(this));

            this.Bmp = composition.GetImage().Bmp;
            this.Layer = new Layer();
        }

        #region colors
        private List<Color> GetColors()
        {
            List<Color> colors = new List<Color>() ;

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    colors.Add(bitmap.GetPixel(j, i));
                }
            }

            return colors;

        }

        public Color Evaluate(double u, double v)
        {
            return GetPixel((int)(u * this.Width), (int)(v * this.Height));
        }

        public Color GetPixel(int x, int y)
        {
            if (x < 0) x = 0;
            if (x > (this.Width - 1)) x = this.Width - 1;

            if (y < 0) y = 0;
            if (y > (this.Height - 1)) y = this.Height - 1;

            return(this.bitmap.GetPixel(x, y));
        }

        #endregion

        #region construct

        private void BuildBitmap(List<Color> colors, int width, int height)
        {

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            int k = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    k = (i * width + j) % colors.Count;
                    bmp.SetPixel(j, i, colors[k]);
                }
            }
            this.bitmap = bmp;
        }

        #endregion

        #region filters

        public void ApplyFilters()
        {
            if (this.Filters.Count > 0)
            {
                Filter.ImageTypes imageType = Filter.ImageTypes.ARgb32bpp;

                Af.FiltersSequence sequence = new Af.FiltersSequence();
                foreach (Filter filter in Filters)
                {
                    sequence.Add(filter.FilterObject);
                    if (filter.ImageType < imageType) imageType = filter.ImageType;
                }

                if (FilterIterations > 0)
                {
                    this.Bmp = new Af.FilterIterator(sequence, FilterIterations).Apply(this.Bmp.ToAccordBitmap(imageType));
                }
                else
                {
                    this.Bmp = sequence.Apply(this.Bmp.ToAccordBitmap(imageType));
                }

                this.Filters = new List<Filter>();
                this.FilterIterations = 0;
            }
        }

        #endregion

        #region channels

        public void Swap2Channels(Channels source, Channels target)
        {
            this.ApplyFilters();

            Dictionary<string, List<int>> channels = this.bitmap.GetChannels();

            List<int> swap = channels[source.ToString()];
            channels[target.ToString()] = swap;

            Bitmap output = new Bitmap(this.bitmap.Width, this.bitmap.Height);

            int k = 0;
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    output.SetPixel(i, j, Color.FromArgb(channels["Alpha"][k], channels["Red"][k], channels["Green"][k], channels["Blue"][k]));
                    k += 1;
                }
            }

            this.Bmp = output;
        }


        public void SwapChannels(Channels alpha, Channels red, Channels green, Channels blue)
        {
            this.ApplyFilters();
            Dictionary<string, List<int>> channels = this.bitmap.GetChannels();

            List<int> alphas = channels[alpha.ToString()];
            List<int> reds = channels[red.ToString()];
            List<int> greens = channels[green.ToString()];
            List<int> blues = channels[blue.ToString()];

            Bitmap output = new Bitmap(bitmap.Width, bitmap.Height);

            int k = 0;
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    output.SetPixel(i, j, Color.FromArgb(alphas[k], reds[k], greens[k], blues[k]));
                    k += 1;
                }
            }

            this.Bmp = output;
        }


        #endregion

        #endregion

        #region overrides

        public override string ToString()
        {
            return "Image(w:"+this.Width+" h:"+this.Height+" r:"+this.Resolution+")";
        }

        #endregion

        #region IGH_Goo

        public bool IsValid { get { return this.IsValidWhyNot == string.Empty; } }

        public string IsValidWhyNot
        {
            get
            {
                if (bitmap == null)
                {
                    return "Bitmap not set";
                }
                else if (bitmap.Height == 0 || bitmap.Width == 0)
                {
                    return "Bitmap height or width is 0";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string TypeName { get { return "BmpPlus"; } }

        public string TypeDescription { get { return "Grasshopper type for Bitmap+ image"; } }

        public IGH_Goo Duplicate()
        {
            return new Img(this);
        }

        public IGH_GooProxy EmitProxy()
        {
            return null;
        }

        public bool CastFrom(object source)
        {
            // Note: GH_Param<T>.Cast_Object(object data), which is used when adding data to a floating parameter, or
            // when collecting data from sources, tries to use
            //   target = InstantiateT();
            //   target.CastFrom(data) ...
            // before trying to use
            //   data.CastTo<T>(out target)

            if (source is Bitmap)
            {
                var img = new Img((Bitmap)source);
                this.Layer = img.Layer;
                this.Filters = img.Filters;
                this.FilterIterations = img.FilterIterations;

                return true;
            }
            else if (source is IGH_Goo)
            {
                if (((IGH_Goo)source).CastTo(out Bitmap bitmap))
                {
                    var img = new Img((Bitmap)source);
                    this.Layer = img.Layer;
                    this.Filters = img.Filters;
                    this.FilterIterations = img.FilterIterations;

                    return true;
                }
            }

            return false;
        }

        public bool CastTo<T>(out T target)
        {
            bool success = false;

            if (typeof(T).IsAssignableFrom(typeof(Bitmap)))
            {
                target = (T)((object)this.GetFlatBitmap());
                return true;
            }
            else if (typeof(T).IsAssignableFrom(typeof(Image)))
            {
                target = (T)((object)this.GetFlatBitmap());
                return true;
            }

            try
            {
                object o = bitmap;
                target = (T)o;
                success = true;
            }
            catch //(Exception e)
            {
                target = default(T);
            }
            return success;
        }

        public object ScriptVariable()
        {
            return this.bitmap;
        }

        public bool Write(GH_IWriter writer)
        {
            throw new NotImplementedException();
        }

        public bool Read(GH_IReader reader)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
