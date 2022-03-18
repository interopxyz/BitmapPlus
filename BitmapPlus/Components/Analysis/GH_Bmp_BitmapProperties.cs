using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace BitmapPlus.Components.Analysis
{
    public class GH_Bmp_BitmapProperties : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_BitmapProperties class.
        /// </summary>
        public GH_Bmp_BitmapProperties()
          : base("Bitmap Properties", "BmpProp",
              "Get a bitmap's properties",
              Constants.ShortName, "Analysis")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "I", "A Bitmap Plus Image or Bitmap", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddRectangleParameter("Boundary", "B", "The rectangular boundary of the bitmap in pixels", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Width", "W", "The bitmap pixel width", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Height", "H", "The bitmap pixel height", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Horizontal Resolution", "X", "The resolution (dpi / ppi) in the X direction", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Vertical Resolution", "Y", "The resolution (dpi / ppi) in the Y direction", GH_ParamAccess.item);
            pManager.AddTextParameter("Format", "F", "The bitmap's pixel format", GH_ParamAccess.item);
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
            DA.SetData(0, image.Bmp.GetBoundary());
            DA.SetData(1, image.Width);
            DA.SetData(2, image.Height);
            DA.SetData(3, image.Bmp.HorizontalResolution);
            DA.SetData(4, image.Bmp.VerticalResolution);
            DA.SetData(5, image.Bmp.PixelFormat.ToString());

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
                return Properties.Resources.Bmp_Properties;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("df8eb4cb-e2e6-43f0-a2fe-d1c2f8042066"); }
        }
    }
}