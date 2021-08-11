using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using Fi = BitmapPlus.Filters.Transform;

namespace BitmapPlus.Components.Transform
{
    public class GH_Bmp_Crop : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Crop class.
        /// </summary>
        public GH_Bmp_Crop()
          : base("Crop Image", "CropImg",
              "Crop an Image" + Properties.Resources.AccordCredit,
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
            pManager.AddGenericParameter("Image", "I", "An Image or Bitmap", GH_ParamAccess.item);
            pManager.AddRectangleParameter("Region", "R", "", GH_ParamAccess.item, new Rectangle3d(Plane.WorldXY, 100, 100));
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Keep Original", "K", "", GH_ParamAccess.item, true);
            pManager[2].Optional = true;
            pManager.AddColourParameter("Color", "C", "", GH_ParamAccess.item, Color.Transparent);
            pManager[3].Optional = true;
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

            Rectangle3d region = new Rectangle3d();
            DA.GetData(1, ref region);

            bool original = false;
            DA.GetData(2, ref original);

            Color color = Color.Transparent;
            DA.GetData(3, ref color);

            image.Filters.Add(new Fi.Crop(color, region.ToDrawingRect(image.Height), original));

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
                return Properties.Resources.Bmp_Xform_Crop;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("1d73aba1-e222-44c2-b686-ca064742ccd8"); }
        }
    }
}