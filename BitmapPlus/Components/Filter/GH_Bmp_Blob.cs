using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Figures;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Blob : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Blob class.
        /// </summary>
        public GH_Bmp_Blob()
          : base("Blob Filter", "Blob",
              "Apply a Blob filter to an Image" + Properties.Resources.AccordCredit,
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
            pManager.AddIntervalParameter("Width", "W", "The horizontal threshold domain for the filtered blobs", GH_ParamAccess.item, new Interval(1, 100));
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Height", "H", "The vertical threshold domain for the filtered blobs", GH_ParamAccess.item, new Interval(1, 100));
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Unique", "U", "If true, each discrete blob is color individually", GH_ParamAccess.item, false);
            pManager[3].Optional = true;
            pManager.AddBooleanParameter("Blobs", "B", "", GH_ParamAccess.item, false);
            pManager[4].Optional = true;
            pManager.AddBooleanParameter("Coupled", "C", "", GH_ParamAccess.item, false);
            pManager[5].Optional = true;
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

            Interval numValA = new Interval(1, 100);
            DA.GetData(1, ref numValA);

            Interval numValB = new Interval(1, 100);
            DA.GetData(2, ref numValB);

            bool unique = false;
            DA.GetData(3, ref unique);

            bool blobs = false;
            DA.GetData(4, ref blobs);

            bool coupled = false;
            DA.GetData(5, ref coupled);

            if (unique)
            {
                image.Filters.Add(new Fi.BlobsUnique(numValA, numValB, blobs, coupled));
            }
            else
            {
                image.Filters.Add(new Fi.BlobsFilter(numValA, numValB, coupled));
            }

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
                return Properties.Resources.Bmp_Blob;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ea97240b-8285-4845-a394-7c9e361b776c"); }
        }
    }
}