using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

//Uses https://github.com/Auburns/FastNoise/wiki
namespace BitmapPlus
{
    public class Noise
    {

        #region members

        public enum OutputModes { None, Value, Perlin, Simplex, Cubic, White, Cellular };
        protected OutputModes OutputStatus = OutputModes.None;

        public enum InterpolationModes { Linear, Hermite, Quintic };
        public InterpolationModes InterpolationMode = InterpolationModes.Linear;

        public enum FractalModes { FBM, Billow, Rigid };
        public FractalModes FractalMode = FractalModes.FBM;

        public enum CellularModes { Euclidean, Manhattan, Natural };
        public CellularModes CellularMode = CellularModes.Euclidean;

        public enum CellularOutputs { Value = 0, Lookup = 1, Distance = 2, Distance2 = 3, Distance2Add = 4, Distance2Sub = 5, Distance2Mul = 6, Distance2Div = 7 }
        public CellularOutputs CellularOutput = CellularOutputs.Value;

        public int Seed = 1;

        public int Width = 100;
        public int Height = 100;
        public int Depth = 0;

        public double Frequency = 0.025;

        public int Octaves = 5;
        public double Lacunarity = 2.0;
        public double Gain = 0.5;

        public double PerturbanceAmplitude = 30;
        public double PerturbanceFrequency = 0.01;

        public double Jitter = 1.0;

        public bool IsFractal = false;
        public bool IsPerturbed = false;

        public int Index0 = 0;
        public int Index1 = 1;

        protected List<double> Values = new List<double>();

        protected FastNoiseBase fastNoise = new FastNoiseBase(1);

        #endregion

        #region constructors

        public Noise()
        {

        }

        public Noise(Noise noise)
        {
            Seed = noise.Seed;

            Width = noise.Width;
            Height = noise.Height;
            Depth = noise.Depth;

            Frequency = noise.Frequency;

            Octaves = noise.Octaves;
            Lacunarity = noise.Lacunarity;
            Gain = noise.Gain;

            PerturbanceAmplitude = noise.PerturbanceAmplitude;
            PerturbanceFrequency = noise.PerturbanceFrequency;

            OutputStatus = noise.OutputStatus;
            InterpolationMode = noise.InterpolationMode;
            FractalMode = noise.FractalMode;

            Jitter = noise.Jitter;

            IsFractal = noise.IsFractal;
            IsPerturbed = noise.IsPerturbed;

            Index0 = noise.Index0;
            Index1 = noise.Index1;
        }

        public Noise(int seed)
        {
            Seed = seed;
        }

        public Noise(int seed, int width, int height, int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;

            Seed = seed;
        }

        #endregion

        #region properties

        #region public 

        public Bitmap GetCurrent()
        {
            switch (OutputMode)
            {
                case OutputModes.Cellular:
                    return GetCellular();
                case OutputModes.Cubic:
                    return GetCubic();
                case OutputModes.Perlin:
                    return GetPerlin();
                case OutputModes.Simplex:
                    return GetSimplex();
                case OutputModes.Value:
                    return GetValue();
                default:
                    return GetWhiteNoise();
            }
        }

        public Bitmap GetCubic()
        {
            OutputStatus = OutputModes.Cubic;

            if (IsFractal)
            {
                return GetCubicFractal();
            }
            else
            {
                return GetCubicNormal();
            }
        }

        public Bitmap GetPerlin()
        {
            OutputStatus = OutputModes.Perlin;

            if (IsFractal)
            {
                return GetPerlinFractal();
            }
            else
            {
                return GetPerlinNormal();
            }
        }

        public Bitmap GetSimplex()
        {
            OutputStatus = OutputModes.Simplex;

            if (IsFractal)
            {
                return GetSimplexFractal();
            }
            else
            {
                return GetSimplexNormal();
            }
        }

        public Bitmap GetWhiteNoise()
        {
            OutputStatus = OutputModes.White;

            return GetWhiteNoiseNormal();
        }

        public Bitmap GetValue()
        {
            OutputStatus = OutputModes.Value;

            if (IsFractal)
            {
                return GetValueFractal();
            }
            else
            {
                return GetValueNormal();
            }
        }

        public virtual OutputModes OutputMode
        {
            get { return OutputStatus; }
        }

        #endregion

        #endregion

        #region methods

        #region normal

        private Bitmap GetCubicNormal()
        {
            Bitmap bitmap = GetNoiseBitmap();
            fastNoise.SetNoiseType(FastNoiseBase.NoiseType.Cubic);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = fastNoise.GetCubic(j, i, Depth);
                    Values.Add(Value);
                }
            }

            return GetBitmap(bitmap);
        }

        private Bitmap GetPerlinNormal()
        {
            Bitmap bitmap = GetNoiseBitmap();
            fastNoise.SetNoiseType(FastNoiseBase.NoiseType.Perlin);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = fastNoise.GetPerlin(j, i, Depth);
                    Values.Add(Value);
                }
            }

            return GetBitmap(bitmap);
        }

        private Bitmap GetSimplexNormal()
        {
            Bitmap bitmap = GetNoiseBitmap();
            fastNoise.SetNoiseType(FastNoiseBase.NoiseType.Simplex);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = fastNoise.GetSimplex(j, i, Depth);
                    Values.Add(Value);
                }
            }

            return GetBitmap(bitmap);
        }

        private Bitmap GetWhiteNoiseNormal()
        {
            Bitmap bitmap = GetNoiseBitmap();
            fastNoise.SetNoiseType(FastNoiseBase.NoiseType.WhiteNoise);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = fastNoise.GetWhiteNoise(j, i, Depth, Frequency);
                    Values.Add(Value);
                }
            }

            return GetBitmap(bitmap);
        }

        private Bitmap GetValueNormal()
        {
            Bitmap bitmap = GetNoiseBitmap();
            fastNoise.SetNoiseType(FastNoiseBase.NoiseType.Value);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = fastNoise.GetNoise(j, i, Depth);
                    Values.Add(Value);
                }
            }

            return GetBitmap(bitmap);
        }

        #endregion

        #region fractal

        private Bitmap GetCubicFractal()
        {
            Bitmap bitmap = GetNoiseBitmap();
            fastNoise.SetNoiseType(FastNoiseBase.NoiseType.CubicFractal);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = fastNoise.GetCubicFractal(j, i, Depth);
                    Values.Add(Value);
                }
            }

            return GetBitmap(bitmap);
        }

        private Bitmap GetPerlinFractal()
        {
            Bitmap bitmap = GetNoiseBitmap();
            fastNoise.SetNoiseType(FastNoiseBase.NoiseType.PerlinFractal);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = fastNoise.GetPerlinFractal(j, i, Depth);
                    Values.Add(Value);
                }
            }

            return GetBitmap(bitmap);
        }

        private Bitmap GetSimplexFractal()
        {
            Bitmap bitmap = GetNoiseBitmap();
            fastNoise.SetNoiseType(FastNoiseBase.NoiseType.SimplexFractal);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = fastNoise.GetSimplexFractal(j, i, Depth);
                    Values.Add(Value);
                }
            }

            return GetBitmap(bitmap);
        }

        private Bitmap GetValueFractal()
        {
            Bitmap bitmap = GetNoiseBitmap();
            fastNoise.SetNoiseType(FastNoiseBase.NoiseType.ValueFractal);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = fastNoise.GetNoise(j, i, Depth);
                    Values.Add(Value);
                }
            }

            return GetBitmap(bitmap);
        }

        #endregion

        #region cellular

        public Bitmap GetCellular()
        {
            OutputStatus = OutputModes.Cellular;

            Bitmap bitmap = GetCellularBitmap();
            fastNoise.SetNoiseType(FastNoiseBase.NoiseType.Cellular);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = fastNoise.GetCellular(j, i, Depth);
                    Values.Add(Value);
                }
            }

            return GetBitmap(bitmap);
        }

        #endregion

        #region bitmap

        public Bitmap GetBitmap(Bitmap bitmap)
        {
            double[] values = Values.ToArray();
            Array.Sort(values);

            double min = values[0];
            double factor = values[Values.Count - 1] - values[0];

            int k = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int IntValue = 0;
                    if (factor!=0) IntValue = (int)(255 * (Values[k] - min) / factor);

                    bitmap.SetPixel(j, i, Color.FromArgb(IntValue, IntValue, IntValue));
                    k += 1;
                }
            }

            return bitmap;
        }

        #endregion

        public void SetSize(int width, int height, int depth = 0)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }

        public void SetNoiseParameters(double frequency, InterpolationModes interpolation)
        {
            Frequency = frequency;
            InterpolationMode = interpolation;
        }

        public void SetFractal(FractalModes mode, int octaves, double lacunarity, double gain)
        {
            FractalMode = mode;

            Octaves = octaves;
            Lacunarity = lacunarity;
            Gain = gain;
        }

        private Bitmap GetNoiseBitmap()
        {
            fastNoise = new FastNoiseBase(Seed);

            fastNoise.SetFrequency(Frequency);
            fastNoise.SetInterp((FastNoiseBase.Interp)(int)InterpolationMode);

            fastNoise.SetFractalType((FastNoiseBase.FractalType)(int)FractalMode);
            fastNoise.SetFractalOctaves(Octaves);
            fastNoise.SetFractalLacunarity(Lacunarity);
            fastNoise.SetFractalGain(Gain);

            SetPerturbance(10, 0.01);

            return new Bitmap(Width, Height);
        }

        private Bitmap GetCellularBitmap()
        {
            fastNoise = new FastNoiseBase(Seed);

            fastNoise.SetFrequency(Frequency);
            fastNoise.SetInterp((FastNoiseBase.Interp)(int)InterpolationMode);

            fastNoise.SetCellularJitter((float)Jitter);

            fastNoise.SetCellularDistanceFunction((FastNoiseBase.CellularDistanceFunction)CellularMode);
            fastNoise.SetCellularReturnType((FastNoiseBase.CellularReturnType)CellularOutput);

            fastNoise.SetCellularDistance2Indicies(Index0, Index1);

            SetPerturbance(10, 0.01);

            return new Bitmap(Width, Height);
        }

        public void SetPerturbance(double amplitude, double frequency)
        {
            PerturbanceAmplitude = amplitude;
            PerturbanceFrequency = frequency;

            fastNoise.GradientPerturb(ref frequency, ref frequency, ref amplitude);
            fastNoise.SetGradientPerturbAmp(PerturbanceAmplitude);
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return OutputStatus.ToString() + " Noise";
        }

        #endregion

    }
}
