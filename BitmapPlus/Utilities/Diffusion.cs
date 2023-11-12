using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapPlus
{
    public static class Diffusion
    {
        public static Bitmap ReactionDiffusion(this Bitmap input, double d1, double d2, double f, double k, int iterations, double ka = 0.2, double kc = 0.05)
        {
            int w = input.Width;
            int h = input.Height;
            double[,] A0 = new double[w, h];
            double[,] B0 = new double[w, h];
            double[,] A1 = new double[w, h];
            double[,] B1 = new double[w, h];


            //Read Red Values from bitmap and convert to factors
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    double v = input.GetPixel(x, y).R / 255.0;
                    A0[x, y] = v / 1.0;
                    B0[x, y] = 1.0 - v;
                    A1[x, y] = 0.0;
                    B1[x, y] = 0.0;
                }
            }

            for (int i = 0; i < iterations; i++)
            {
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {

                        int x0 = (x + w - 1) % w;
                        int x1 = (x + 1) % w;

                        int y0 = (y + h - 1) % h;
                        int y1 = (y + 1) % h;

                        A1[x, y] = A0[x, y] + (A0[x0, y0]+ A0[x0, y1]+ A0[x1, y0]+ A0[x1, y1]) * d1 * kc+ (A0[x, y0] + A0[x, y1] + A0[x0, y] + A0[x1, y]) * d1 * ka;

                        A1[x, y] += (-1.0) * A0[x, y] * d1;
                        A1[x, y] -= A0[x, y] * B0[x, y] * B0[x, y];
                        A1[x, y] += f * (1.0 - A0[x, y]);

                        B1[x, y] = B0[x, y] + (B0[x0, y0]+ B0[x0, y1]+ B0[x1, y0] + B0[x1, y1]) * d2 * kc + (B0[x, y0]+ B0[x, y1]+ B0[x0, y] + B0[x1, y]) * d2 * ka;

                        B1[x, y] += (-1.0) * B0[x, y] * d2;
                        B1[x, y] += A0[x, y] * B0[x, y] * B0[x, y];
                        B1[x, y] -= (f + k) * B0[x, y];
                    }
                }

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        A0[x, y] = A1[x, y];
                        B0[x, y] = B1[x, y];
                    }
                }
            }

            System.Drawing.Bitmap output = new System.Drawing.Bitmap(w, h);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int v = (int)(A0[x, y] * 255.0);
                    if (v < 0) v = 0;
                    if (v > 255) v = 255;
                    output.SetPixel(x, y, Color.FromArgb(v, v, v));
                }
            }

            return output;
        }

    }
}
