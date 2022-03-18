using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace BitmapPlus.Components
{
    public class GH_Bmp_EvaluateBitmap : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_EvaluateBitmap class.
        /// </summary>
        public GH_Bmp_EvaluateBitmap()
          : base("Evaluate Bitmap", "EvalBmp",
              "Evaluates a bitmap by parameterized values in the x and y directions and returns the color.",
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
            pManager.AddNumberParameter("U Parameter", "U", "A unitized parameter in the X direction of the bitmap (0-1)", GH_ParamAccess.item,0.5);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("V Parameter", "V", "A unitized parameter in the Y direction of the bitmap (0-1)", GH_ParamAccess.item, 0.5);
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

            double u = 0.5;
            DA.GetData(1, ref u);
            if (u < 0) this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "U parameter cannot be less than 0. Must be between (0-1)");
            if (u > 1) this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "U parameter cannot be greater than 1. Must be between (0-1)");
            

            double v = 0.5;
            DA.GetData(2, ref v);
            if (v < 0) this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "V parameter cannot be less than 0. Must be between (0-1)");
            if (v > 1) this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "V parameter cannot be greater than 1. Must be between (0-1)");

            DA.SetData(0, image.Evaluate(u, v));

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
                return Properties.Resources.Bmp_Evaluate;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("1fecf1fd-0379-401d-82e2-7be23b15287c"); }
        }
    }
}