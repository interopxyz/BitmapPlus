using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Transform;

namespace BitmapPlus.Components.Transform
{
    public class GH_Bmp_Polar : GH_Bitmap_Base
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Polar class.
        /// </summary>
        public GH_Bmp_Polar()
          : base("Polar Image", "PolarImg",
              "Apply a Polar Transformation to an Image" + Properties.Resources.AccordCredit,
                Constants.ShortName, "Transform")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "I", "A Bitmap Plus Image or Bitmap", GH_ParamAccess.item);
            pManager.AddNumberParameter("Angle", "A", "The rotation angle in degrees [0-360]", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Pinch", "P", "Unitized value where 0 pinches the distortion to the corners and 1 to smoothed circular distortion [0-1]", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("To", "T", "True maps from orthogonal to polar and false from polar to orthogonal", GH_ParamAccess.item, true);
            pManager[2].Optional = true;
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

            double angle = 0;
            DA.GetData(1, ref angle);

            double depth = 0;
            DA.GetData(2, ref depth);

            bool toPolar = true;
            DA.GetData(3, ref toPolar);

            image.Filters.Add(new Fi.Polar(toPolar,depth,angle,false,false));

            fileImage = new Img(image);
            DA.SetData(0, image);
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
                return Properties.Resources.Bmp_Xform_Polar;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("88b311c5-eb7a-404b-baee-4412ffdfcfd5"); }
        }
    }
}