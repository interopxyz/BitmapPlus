using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace BitmapPlus.Components
{
    public class GH_Bmp_GetPixel : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_GetPixel class.
        /// </summary>
        public GH_Bmp_GetPixel()
          : base("Get Pixel", "Pixel",
              "Gets a color from a bitmap at a column and row pixel location.",
              Constants.ShortName, "Analysis")
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
            pManager.AddGenericParameter("Image", "I", "A Bitmap Plus Image or Bitmap", GH_ParamAccess.item);
            pManager.AddIntegerParameter("X Location", "X", "The bitmap's column location to sample", GH_ParamAccess.item,0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Y Location", "Y", "The bitmap's row location to sample", GH_ParamAccess.item,0);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddColourParameter("Color", "C", "The color at the pixel location", GH_ParamAccess.item);
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

            image.Flatten();

            int x = 0;
            DA.GetData(1, ref x);
            if (x < 0) this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "X value cannot be less than 0.");
            if (x > image.Width) this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "X value cannot be greater than " + (image.Width - 1) + ".");

            int y = 0;
            DA.GetData(2, ref y);
            if (y < 0) this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Y value cannot be less than 0.");
            if (y > image.Height) this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Y value cannot be greater than " + (image.Height - 1) + ".");

            DA.SetData(0, image.GetPixel(x, y));

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
                return Properties.Resources.Bmp_GetPixel;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("07f8fce0-df10-493d-837c-697b4949465a"); }
        }
    }
}