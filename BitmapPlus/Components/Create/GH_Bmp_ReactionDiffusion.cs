using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace BitmapPlus.Components
{
    public class GH_Bmp_ReactionDiffusion : GH_Bitmap_Base
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_ReactionDiffusion class.
        /// </summary>
        public GH_Bmp_ReactionDiffusion()
          : base("Reaction Diffusion", "ReactDiffuse",
              "Applies Grey Scott reaction diffusion to an image",
                Constants.ShortName, "Create")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "I", "An Image or Bitmap", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Steps", "S", "The number of iterations", GH_ParamAccess.item, 10);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Diffusion Rate 1", "D1", "The U diffusion rate", GH_ParamAccess.item, 1.0);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Diffusion Rate 2", "D2", "The V diffusion rate", GH_ParamAccess.item, 0.5);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Conversion Factor", "F", "The conversion factor", GH_ParamAccess.item, 0.035);
            pManager[4].Optional = true;
            pManager.AddNumberParameter("Exchange", "K", "The feed and drain exchange", GH_ParamAccess.item, 0.058);
            pManager[5].Optional = true;
            pManager.AddNumberParameter("Adjacent Kernal", "Ka", "Adjacent Kernal Factor", GH_ParamAccess.item, 0.2);
            pManager[6].Optional = true;
            pManager.AddNumberParameter("Corner Kernal", "Kc", "corner Kernal Factor", GH_ParamAccess.item, 0.05);
            pManager[7].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "I", "An Image object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Goo goo = null;
            Img image = null;
            if (!DA.GetData(0, ref goo)) return;
            if (!goo.TryGetImage(ref image))
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Must either be an Image or System.Drawing.Bitmap object.");
                return;
            }

            int steps = 10;
            DA.GetData(1, ref steps);
            double d1 = 1.0;
            DA.GetData(2, ref d1);
            double d2 = 0.5;
            DA.GetData(3, ref d2);
            double f = 0.035;
            DA.GetData(4, ref f);
            double k = 0.058;
            DA.GetData(5, ref k);
            double ka = 0.2;
            DA.GetData(6, ref ka);
            double kc = 0.05;
            DA.GetData(7, ref kc);

            DA.SetData(0, image.GetFlatBitmap().ReactionDiffusion(d1, d2, f, k, steps, ka, kc));
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
                return Properties.Resources.Bmp_ReactionDiffusion;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("8ab546fa-34fe-41ed-94c1-ebd16a793159"); }
        }
    }
}