using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using Fi = BitmapPlus.Filters.Transform;

namespace BitmapPlus.Components.Transform
{
    public class GH_Bmp_Rotate : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Rotate class.
        /// </summary>
        public GH_Bmp_Rotate()
          : base("Rotate Image", "RotateImg",
              "Rotate an Image" + Properties.Resources.AccordCredit,
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
            pManager.AddIntegerParameter("Mode", "M", "", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Angle", "A", "", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Keep Original", "K", "", GH_ParamAccess.item, true);
            pManager[3].Optional = true;
            pManager.AddColourParameter("Color", "C", "", GH_ParamAccess.item, Color.Transparent);
            pManager[4].Optional = true;

            Param_Integer param = (Param_Integer)pManager[1];
            foreach (Fi.Rotate.Modes value in Enum.GetValues(typeof(Fi.Rotate.Modes)))
            {
                param.AddNamedValue(value.ToString(), (int)value);
            }
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

            int mode = 0;
            DA.GetData(1, ref mode);

            double angle = 0;
            DA.GetData(2, ref angle);

            bool original = false;
            DA.GetData(3, ref original);

            Color color = Color.Black;
            DA.GetData(4, ref color);

            image.Filters.Add(new Fi.Rotate(color, (Fi.Rotate.Modes)mode,angle, original));

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
                return Properties.Resources.Bmp_Xform_Rotate;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("05a0b55a-fd99-47d8-a88a-e6ac28f8861b"); }
        }
    }
}