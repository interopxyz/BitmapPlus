using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Transform;

namespace BitmapPlus.Components.Transform
{
    public class GH_Bmp_Resize : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Resize class.
        /// </summary>
        public GH_Bmp_Resize()
          : base("Resize Image", "ResizeImg",
              "Resize an Image" + Properties.Resources.AccordCredit,
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
            pManager.AddIntegerParameter("Width", "W", "", GH_ParamAccess.item, 100);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Height", "H", "", GH_ParamAccess.item, 100);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)pManager[1];
            foreach (Fi.Resize.Modes value in Enum.GetValues(typeof(Fi.Resize.Modes)))
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

            int width = 100;
            DA.GetData(2, ref width);
            if (width < 5) width = 5;

            int height = 100;
            DA.GetData(3, ref height);
            if (height < 5) height = 5;

            image.Filters.Add(new Fi.Resize((Fi.Resize.Modes)mode,width,height));

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
                return Properties.Resources.Bmp_Xform_Resize_B;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("dea0820b-1301-46df-a185-f25eface511c"); }
        }
    }
}