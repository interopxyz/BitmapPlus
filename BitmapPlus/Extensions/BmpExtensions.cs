using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Sw = System.Windows.Interop;
using Si = System.Windows.Media.Imaging;
using System.Windows;

using Rg = Rhino.Geometry;

namespace BitmapPlus
{
    public static class BmpExtensions
    {

        public static Rg.Rectangle3d GetBoundary(this Sd.Bitmap input)
            {
            return new Rg.Rectangle3d(Rg.Plane.WorldXY, new Rg.Point3d(0, 0, 0), new Rg.Point3d(input.Width, input.Height, 0));
        }
        public static bool GetBitmapFromFile(this string FilePath, out Sd.Bitmap bitmap)
        {
            bitmap = null;
            if (Path.HasExtension(FilePath))
            {
                string extension = Path.GetExtension(FilePath);
                switch (extension)
                {
                    default:
                        return (false);
                    case ".bmp":
                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                    case ".jfif":
                    case ".gif":
                    case ".tif":
                    case ".tiff":
                        bitmap = (Sd.Bitmap)Sd.Bitmap.FromFile(FilePath);
                        return (bitmap != null);
                }

            }

            return (false);
        }


        public static Si.WriteableBitmap ToWriteableBitmap(this Sd.Bitmap input)
        {
            return new Si.WriteableBitmap(input.ToBitmapSource());
        }

        public static Si.BitmapSource ToBitmapSource(this Sd.Bitmap input)
        {
            Si.BitmapSource output = Sw.Imaging.CreateBitmapSourceFromHBitmap(input.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, Si.BitmapSizeOptions.FromEmptyOptions());

            return output;
        }

        public static Sd.Bitmap ToBitmap(this Si.BitmapSource input)
        {
            Sd.Bitmap output = new Sd.Bitmap(10, 10);
            using (MemoryStream outStream = new MemoryStream())
            {
                Si.PngBitmapEncoder enc = new Si.PngBitmapEncoder();

                enc.Frames.Add(Si.BitmapFrame.Create(input));
                enc.Save(outStream);
                output = new Sd.Bitmap(outStream);
            }

            return output;
        }


        public static List<int> GetReds(this Sd.Bitmap bitmap)
        {
            List<int> values = new List<int>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    values.Add(bitmap.GetPixel(i, j).R);
                }
            }

            return values;
        }
        public static List<int> GetGreens(this Sd.Bitmap bitmap)
        {
            List<int> values = new List<int>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    values.Add(bitmap.GetPixel(i, j).G);
                }
            }

            return values;
        }
        public static List<int> GetBlues(this Sd.Bitmap bitmap)
        {
            List<int> values = new List<int>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                }
            }

            return values;
        }
        public static List<int> GetAlphas(this Sd.Bitmap bitmap)
        {
            List<int> values = new List<int>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    values.Add(bitmap.GetPixel(i, j).A);
                }
            }

            return values;
        }
        public static List<int> GetHues(this Sd.Bitmap bitmap)
        {
            List<int> values = new List<int>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    values.Add((int)(255.0 * bitmap.GetPixel(i, j).GetHue() / 360.0));
                }
            }

            return values;
        }
        public static List<int> GetSaturations(this Sd.Bitmap bitmap)
        {
            List<int> values = new List<int>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    values.Add((int)(255.0 * bitmap.GetPixel(i, j).GetSaturation()));
                }
            }

            return values;
        }
        public static List<int> GetLuminances(this Sd.Bitmap bitmap)
        {
            List<int> values = new List<int>();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    values.Add((int)(255.0 * bitmap.GetPixel(i, j).GetBrightness()));
                }
            }

            return values;
        }
        public static Dictionary<string, List<int>> GetChannels(this Sd.Bitmap bitmap)
        {

            Dictionary<string, List<int>> channels = new Dictionary<string, List<int>>();
            channels.Add("Alpha", new List<int>());
            channels.Add("Red", new List<int>());
            channels.Add("Green", new List<int>());
            channels.Add("Blue", new List<int>());
            channels.Add("Hue", new List<int>());
            channels.Add("Saturation", new List<int>());
            channels.Add("Luminance", new List<int>());

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Sd.Color color = bitmap.GetPixel(i, j);
                    channels["Alpha"].Add(color.A);
                    channels["Red"].Add(color.R);
                    channels["Green"].Add(color.G);
                    channels["Blue"].Add(color.B);
                    channels["Hue"].Add((int)(255.0 * color.GetHue() / 360.0));
                    channels["Saturation"].Add((int)(255.0 * color.GetSaturation()));
                    channels["Luminance"].Add((int)(255.0 * color.GetBrightness()));
                }
            }

            return channels;
        }

    }
}
