using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace BitmapPlus.Components.Create
{
    public class GH_Bmp_CreateFractal : GH_Bitmap_Base
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_CreateFractal class.
        /// </summary>
        public GH_Bmp_CreateFractal()
          : base("Procedural Fractal Pass", "Fractal",
              "Generate a procedural fractal pass on Cellular or Noise",
              Constants.ShortName, "Create")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Noise", "N", "A Bitmap Plus Noise object", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager.AddIntegerParameter("Mode", "M", "The noise fractal mode", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Octaves", "O", "The amount of noise layers used to create the fractal", GH_ParamAccess.item, 5);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Lacunity", "L", "The frequency multiplier between each octave", GH_ParamAccess.item, 2);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Gain", "G", "The relative strength of noise from each layer when compared to the last", GH_ParamAccess.item, 0.5);
            pManager[4].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[1];
            paramA.AddNamedValue("FBM", 0);
            paramA.AddNamedValue("Billow", 1);
            paramA.AddNamedValue("Rigid", 2);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "I", "An Image object", GH_ParamAccess.item);
            //pManager.AddGenericParameter("Noise", "N", "A Noise object for input into the Noise Fractal component", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Noise noise = new Noise();
            if (!DA.GetData(0, ref noise))return;
            noise = new Noise(noise);

            int mode = 0;
            DA.GetData(1, ref mode);

            int octaves = 5;
            DA.GetData(2, ref octaves);

            double lacunity = 2.0;
            DA.GetData(3, ref lacunity);

            double gain = 0.5;
            DA.GetData(4, ref gain);

            noise.IsFractal = true;
            noise.FractalMode = (Noise.FractalModes)mode;
            noise.Octaves = octaves;
            noise.Lacunarity = lacunity;
            noise.Gain = gain;

            fileImage = new Img(noise.GetCurrent());
            DA.SetData(0, new Img(noise.GetCurrent()));
            //DA.SetData(1, new Noise(noise));
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Properties.Resources.Bmp_Noise_Fractal;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("cef63bb4-8eca-42dc-81ce-dbb42494e39c"); }
        }
    }
}