using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Swap2Channels : GH_Component
    {

        public enum LimitedChannels { Alpha, Red, Green, Blue }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_SwapChannels class.
        /// </summary>
        public GH_Bmp_Swap2Channels()
          : base("Swap 2 Channels", "Swap2",
              "Swap one bitmap channel for another one." + Properties.Resources.AccordCredit,
                Constants.ShortName, "Edit")
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
            pManager.AddIntegerParameter("New", "N", "The new channel to replace the source channel", GH_ParamAccess.item, 6);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Original", "O", "The channel to be replaced", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[1];
            foreach (Img.Channels value in Enum.GetValues(typeof(Img.Channels)))
            {
                paramA.AddNamedValue(value.ToString(), (int)value);
            }

            Param_Integer paramB = (Param_Integer)pManager[2];
            foreach (LimitedChannels value in Enum.GetValues(typeof(LimitedChannels)))
            {
                paramB.AddNamedValue(value.ToString(), (int)value);
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

            int source = 0;
            DA.GetData(1, ref source);

            int target = 0;
            DA.GetData(2, ref target);

            image.Swap2Channels((Img.Channels)source, (Img.Channels)target);

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
                return Properties.Resources.Bmp_Swap2;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("04b36750-0747-4a4f-bff2-d0568f9f5f80"); }
        }
    }
}