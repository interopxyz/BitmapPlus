using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Sharpening;
using Fs = BitmapPlus.Filters.Smoothing;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Sharpen : GH_Bitmap_Base
    {
        private enum FilterModes { Gaussian, HighBoost, Mean, Simple }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Sharpen class.
        /// </summary>
        public GH_Bmp_Sharpen()
          : base("Sharpen Filter", "Sharpen",
              "Apply a Sharpen filter to an Image" + Properties.Resources.AccordCredit,
                Constants.ShortName, "Filter")
        {
            Message = ((FilterModes)0).ToString();
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
            pManager.AddIntegerParameter("Mode", "M", "Select filter type", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Divisor", "D", "Division factor", GH_ParamAccess.item, 1);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Threshold", "T", "Threshold weighted sum", GH_ParamAccess.item, 1);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)pManager[1];
            foreach (FilterModes value in Enum.GetValues(typeof(FilterModes)))
            {
                param.AddNamedValue(value.ToString(), (int)value);
            }
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

            int mode = 0;
            DA.GetData(1, ref mode);

            int numValA = 1;
            DA.GetData(2, ref numValA);

            int numValB = 1;
            DA.GetData(3, ref numValB);

            string filterName = ((FilterModes)mode).ToString();
            Message = filterName;

            switch ((FilterModes)mode)
            {
                case FilterModes.Gaussian:
                    image.Filters.Add(new Fi.Gaussian(numValA, numValB));
                    break;
                case FilterModes.HighBoost:
                    image.Filters.Add(new Fi.HighBoost(numValA, numValB));
                    break;
                case FilterModes.Mean:
                    image.Filters.Add(new Fs.Mean(numValA, numValB));
                    break;
                case FilterModes.Simple:
                    image.Filters.Add(new Fi.Simple(numValA, numValB));
                    break;
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
                return Properties.Resources.Bmp_Sharpen;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("c5114410-37cd-4c9b-929c-7831a776a596"); }
        }
    }
}