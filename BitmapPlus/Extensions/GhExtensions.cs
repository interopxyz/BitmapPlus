using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapPlus
{
    public static class GhExtensions
    {


        public static bool TryGetImage(this IGH_Goo goo, ref Img image)
        {

            string filePath = string.Empty;
            goo.CastTo<string>(out filePath);
            Bitmap bitmap = null;
            Img img = null;

            if (goo.CastTo<Img>(out img))
            {
                image = new Img(img);
                return true;
            }
            else if(goo.CastTo<Bitmap>(out bitmap))
            {
                image = new Img(bitmap);
                return true;
            }
            else if (File.Exists(filePath))
            {
                if(filePath.GetBitmapFromFile(out bitmap))
                {
                    image = new Img(bitmap);
                    return true;
                }
                return false;
            }
            return false;
        }

        public static string StripExtension(this string name)
        {
            string[] parts = name.Split('.');
            if (parts.Count() > 1)
            {
                string extension = parts[parts.Count() - 1];
                extension = extension.ToLower();
                bool hasExtension = false;
                switch (extension)
                {
                    case "png":
                    case "jpg":
                    case "jpeg":
                    case "tif":
                    case "tiff":
                    case "bmp":
                        hasExtension = true;
                        break;
                }

                if (hasExtension) parts = parts.Take(parts.Length - 1).ToArray();

                return String.Join(".", parts);
            }
            return name;
        }

    }
}
