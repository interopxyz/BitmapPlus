using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Levels;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Levels : GH_Bmp_Filter
    {
        private enum FilterModes { RGB, YCbCr, HSL }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Levels class.
        /// </summary>
        public GH_Bmp_Levels()
          : base("Levels Filter", "Levels",
              "Apply a Levels filter to an Image" + Properties.Resources.AccordCredit,
                Constants.ShortName, "Filter")
        {
            Message = ((FilterModes)0).ToString();
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "I", "An Image or Bitmap", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Mode", "M", "Select filter type", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Red In", "Ri", "Domain [0,1]", GH_ParamAccess.item, new Interval(0, 1));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Red Out", "Ro", "Domain [0,1]", GH_ParamAccess.item, new Interval(0, 1));
            pManager[3].Optional = true;
            pManager.AddIntervalParameter("Green In", "Gi", "Domain [0,1]", GH_ParamAccess.item, new Interval(0, 1));
            pManager[4].Optional = true;
            pManager.AddIntervalParameter("Green Out", "Go", "Domain [0,1]", GH_ParamAccess.item, new Interval(0, 1));
            pManager[5].Optional = true;
            pManager.AddIntervalParameter("Blue In", "Bi", "Domain [0,1]", GH_ParamAccess.item, new Interval(0, 1));
            pManager[6].Optional = true;
            pManager.AddIntervalParameter("Blue Out", "Bo", "Domain [0,1]", GH_ParamAccess.item, new Interval(0, 1));
            pManager[7].Optional = true;

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

            Interval numValA = new Interval(0, 1);
            DA.GetData(2, ref numValA);

            Interval numValB = new Interval(0, 1);
            DA.GetData(3, ref numValB);

            Interval numValC = new Interval(0, 1);
            DA.GetData(4, ref numValC);

            Interval numValD = new Interval(0, 1);
            DA.GetData(5, ref numValD);

            Interval numValE = new Interval(0, 1);
            DA.GetData(6, ref numValE);

            Interval numValF = new Interval(0, 1);
            DA.GetData(7, ref numValF);

            string filterName = ((FilterModes)mode).ToString();
            Message = filterName;

            switch ((FilterModes)mode)
            {
                case FilterModes.HSL:
                    SetParameter(2, "Si", "Saturation In", "Domain [0,1]");
                    SetParameter(3, "So", "Saturation Out", "Domain [0,1]");
                    SetParameter(4, "Li", "Luminance In", "Domain [0,1]");
                    SetParameter(5, "Lo", "Luminance Out", "Domain [0,1]");
                    SetParameter(6, "-", "Not Used", "Parameter not used by this filter");
                    SetParameter(7, "-", "Not Used", "Parameter not used by this filter");
                    image.Filters.Add(new Fi.HSL(numValA, numValB, numValC, numValD));
                    break;
                case FilterModes.RGB:
                    SetParameter(2, "Ri", "Red In", "Domain [0,1]");
                    SetParameter(3, "Ro", "Red Out", "Domain [0,1]");
                    SetParameter(4, "Gi", "Green In", "Domain [0,1]");
                    SetParameter(5, "Go", "Green Out", "Domain [0,1]");
                    SetParameter(6, "Bi", "Blue In", "Domain [0,1]");
                    SetParameter(7, "Bo", "Blue Out", "Domain [0,1]");
                    image.Filters.Add(new Fi.RGB(numValA, numValB, numValC, numValD, numValE, numValF));
                    break;
                case FilterModes.YCbCr:
                    SetParameter(2, "Ri", "Red In", "Domain [0,1]");
                    SetParameter(3, "Ro", "Red Out", "Domain [0,1]");
                    SetParameter(4, "Yi", "Y In", "Domain [0,1]");
                    SetParameter(5, "Yo", "Y Out", "Domain [0,1]");
                    SetParameter(6, "Bi", "Blue In", "Domain [0,1]");
                    SetParameter(7, "Bo", "Blue Out", "Domain [0,1]");
                    image.Filters.Add(new Fi.YCbCr(numValC, numValD, numValE, numValF, numValA, numValB));
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
                return Properties.Resources.Bmp_Levels;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("6ee5e9d3-21c2-48f6-90db-5c7aa449cb13"); }
        }
    }
}