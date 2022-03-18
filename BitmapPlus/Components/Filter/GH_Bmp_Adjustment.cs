using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Drawing;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Adjustments;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Adjustment : GH_Bmp_Filter
    {
        private enum FilterModes { Invert, GrayWorld, Stretch, Histogram, WhitePatch, RGBChromacity, Sepia, Brightness, Contrast, Gamma, Hue, Saturation }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Adjustment class.
        /// </summary>
        public GH_Bmp_Adjustment()
          : base("Adjustment Filter", "Adjustment",
              "Apply a Adjustment filter to an Image" + Properties.Resources.AccordCredit,
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
            pManager.AddIntegerParameter("Mode", "M", "Select filter mode", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Not Used", "-", "Parameter not used by this filter", GH_ParamAccess.item, 0.5);
            pManager[2].Optional = true;

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

            double numVal = 0.5;
            DA.GetData(2, ref numVal);

            string filterName = ((FilterModes)mode).ToString();
            Message = filterName;

            switch ((FilterModes)mode)
            {
                case FilterModes.GrayWorld:
                    SetParameter(2);
                    image.Filters.Add( new Fi.GrayWorld());
                    break;
                case FilterModes.Histogram:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Histogram());
                    break;
                case FilterModes.Invert:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Invert());
                    break;
                case FilterModes.Stretch:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Stretch());
                    break;
                case FilterModes.WhitePatch:
                    SetParameter(2);
                    image.Filters.Add(new Fi.WhitePatch());
                    break;
                case FilterModes.Sepia:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Sepia());
                    break;
                case FilterModes.RGBChromacity:
                    SetParameter(2);
                    image.Filters.Add(new Fi.RGChromacity());
                    break;
                case FilterModes.Brightness:
                    SetParameter(2, "V", "Adjust", filterName + " Adjust Value Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.Brightness(numVal));
                    break;
                case FilterModes.Contrast:
                    SetParameter(2, "V", "Factor", filterName + " Factor Value Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.Contrast(numVal));
                    break;
                case FilterModes.Gamma:
                    SetParameter(2, "V", "Gamma", filterName + " Gamma Value Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.Gamma(numVal));
                    break;
                case FilterModes.Hue:
                    SetParameter(2, "V", "Hue", filterName + " Hue Value Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.Hue(numVal));
                    break;
                case FilterModes.Saturation:
                    SetParameter(2, "V", "Adjust", filterName + " Adjust Value Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.Saturation(numVal));
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
                return Properties.Resources.Bmp_Adjust;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("528b67b6-8a46-4822-bbcc-af808f8b5652"); }
        }
    }
}