using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Edges;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_CannyEdges : GH_Bitmap_Base
    {

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Edges class.
        /// </summary>
        public GH_Bmp_CannyEdges()
          : base("Canny Edges Filter", "Canny Edges",
              "Apply an Canny Edges filter to an Image" + Properties.Resources.AccordCredit,
                Constants.ShortName, "Filter")
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
            pManager.AddNumberParameter("Sigma", "X", "Gaussian sigma", GH_ParamAccess.item, 1.0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Size", "S", "Gaussian size", GH_ParamAccess.item, 1);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Low Threshold", "L", "Low threshold value used for hysteresis", GH_ParamAccess.item, 1);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("High Threshold", "H", "High threshold value used for hysteresis", GH_ParamAccess.item, 1);
            pManager[4].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "I", "An Bitmap Plus Image", GH_ParamAccess.item);
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

            double numValA = 1.0;
            DA.GetData(1, ref numValA);

            int numValB = 1;
            DA.GetData(2, ref numValB);

            int numValC = 1;
            DA.GetData(3, ref numValC);

            int numValD = 1;
            DA.GetData(4, ref numValD);

            image.Filters.Add(new Fi.Canny(numValA, numValB, numValC, numValD));

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
                return Properties.Resources.Bmp_EdgesCanny;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0765884a-ecbd-47f4-897e-8cbd9136f8d0"); }
        }
    }
}