using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using Fi = BitmapPlus.Filters.Transform;

namespace BitmapPlus.Components.Transform
{
    public class GH_Bmp_Move : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Move class.
        /// </summary>
        public GH_Bmp_Move()
          : base("Move Image", "MoveImg",
              "Move an Image" + Properties.Resources.AccordCredit,
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
            pManager.AddColourParameter("Color", "C", "The background color", GH_ParamAccess.item, Color.Transparent);
            pManager[1].Optional = true;
            pManager.AddVectorParameter("Vector", "V", "The translation vector", GH_ParamAccess.item, new Vector3d(1, 0, 0));
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

            Color color = Color.Transparent;
            DA.GetData(1, ref color);

            Vector3d vector = new Vector3d(1,0,0);
            DA.GetData(2, ref vector);

            image.Filters.Add(new Fi.Move(color,vector));

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
                return Properties.Resources.Bmp_Xform_Move;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("04280cb9-f776-447e-ba1d-4d0c2a10a047"); }
        }
    }
}