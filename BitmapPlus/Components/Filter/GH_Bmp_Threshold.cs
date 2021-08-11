using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Threshold;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Threshold : GH_Bmp_Filter
    {
        private enum FilterModes { Otsu, SIS, Bradley, Iterative, Nilback, Sauvola, WolfJolion }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Threshold class.
        /// </summary>
        public GH_Bmp_Threshold()
          : base("Threshold Filter", "Threshold",
              "Apply a Threshold filter to an Image" + Properties.Resources.AccordCredit,
                Constants.ShortName, "Filter")
        {
            Message = ((FilterModes)0).ToString();
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
            pManager.AddIntegerParameter("Mode", "M", "Select filter type", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Not Used", "-", "Parameter not used by this filter", GH_ParamAccess.item, 0.5);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Not Used", "-", "Parameter not used by this filter", GH_ParamAccess.item, 0.5);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Not Used", "-", "Parameter not used by this filter", GH_ParamAccess.item, 1);
            pManager[4].Optional = true;

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

            double numValA = 0.5;
            DA.GetData(2, ref numValA);

            double numValB = 0.5;
            DA.GetData(3, ref numValB);

            int numValC = 1;
            DA.GetData(4, ref numValC);

            string filterName = ((FilterModes)mode).ToString();
            Message = filterName;

            switch ((FilterModes)mode)
            {
                case FilterModes.Otsu:
                    SetParameter(2);
                    SetParameter(3);
                    SetParameter(4);
                    image.Filters.Add(new Fi.Otsu());
                    break;
                case FilterModes.SIS:
                    image.Filters.Add(new Fi.SIS());
                    break;
                case FilterModes.Bradley:
                    SetParameter(2, "B", "Brightness", "Brightness difference limit");
                    SetParameter(3, "S", "Size", "Window size");
                    SetParameter(4);
                    image.Filters.Add(new Fi.Bradley(numValA, (int)numValB));
                    break;
                case FilterModes.Iterative:
                    SetParameter(2, "M", "Minimum", "Minimum error value");
                    SetParameter(3, "T", "Threshold", "Threshold value");
                    SetParameter(4);
                    image.Filters.Add(new Fi.Iterative(numValA, numValB));
                    break;
                case FilterModes.Nilback:
                    SetParameter(2, "C", "C", "Mean offset C");
                    SetParameter(3, "K", "K", "Parameter K");
                    SetParameter(4, "R", "Radius", "Filter convolution radius");
                    image.Filters.Add(new Fi.Nilback(numValA, numValB, numValC));
                    break;
                case FilterModes.Sauvola:
                    SetParameter(2, "R", "R", "Dynamic range");
                    SetParameter(3, "K", "K", "Parameter K");
                    SetParameter(4, "R", "Radius", "Filter convolution radius");
                    image.Filters.Add(new Fi.Sauvola(numValA, numValB, numValC));
                    break;
                case FilterModes.WolfJolion:
                    SetParameter(2, "R", "R", "Dynamic range");
                    SetParameter(3, "K", "K", "Parameter K");
                    SetParameter(4, "R", "Radius", "Filter convolution radius");
                    image.Filters.Add(new Fi.WolfJolion(numValA, numValB, numValC));
                    break;
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
                return Properties.Resources.Bmp_Threshold;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("055a7f41-ba3f-4e1a-ae38-02c9b9169d36"); }
        }
    }
}