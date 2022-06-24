using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;

using Fi = BitmapPlus.Filters.Convolutions;

namespace BitmapPlus.Components.Filter
{
    public class GH_ApplyMatrix : GH_Bmp_Filter
    {
        /// <summary>
        /// Initializes a new instance of the GH_ApplyMatrix class.
        /// </summary>
        public GH_ApplyMatrix()
          : base("Convolution Filter", "Convolution",
              "Apply Convolution Matrix filter to an Image" + Properties.Resources.AccordCredit,
                Constants.ShortName, "Filter")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quinary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Image / Bitmap", "I", "An Image or Bitmap", GH_ParamAccess.item);
            pManager.AddMatrixParameter("Matrix", "M", "The Matrix to apply", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Divisor", "D", "Optional Divisor Value", GH_ParamAccess.item);
            pManager[2].Optional = true;
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

            GH_Matrix ghMatrix = new GH_Matrix();
            if (!DA.GetData(1, ref ghMatrix)) return;

            int divisor = 9;
            bool hasDivisor = DA.GetData(2, ref divisor);

            if (hasDivisor)
            {
                image.Filters.Add(new Fi.Convolution(ghMatrix.Value,divisor));
            }
            else
            {
            image.Filters.Add(new Fi.Convolution(ghMatrix.Value));
            }

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
                return Properties.Resources.ConvolutionMatrix_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("4d77acfd-7f16-430d-ae68-50d750c32d00"); }
        }
    }
}