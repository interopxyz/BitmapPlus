using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Grayscale;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Grayscale : GH_Bmp_Filter
    {
        private enum FilterModes { Y, RMY, BT709, Simple }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Grayscale class.
        /// </summary>
        public GH_Bmp_Grayscale()
          : base("Grayscale Filter", "Grayscale",
              "Apply a Grayscale filter to an Image" + Properties.Resources.AccordCredit,
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
            pManager.AddNumberParameter("Not Used", "-", "Parameter not used by this filter", GH_ParamAccess.item, 0.125);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Not Used", "-", "Parameter not used by this filter", GH_ParamAccess.item, 0.125);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Not Used", "-", "Parameter not used by this filter", GH_ParamAccess.item, 0.125);
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

            double numValA = 1.0;
            DA.GetData(2, ref numValA);

            double numValB = 1.0;
            DA.GetData(3, ref numValB);

            double numValC = 1.0;
            DA.GetData(4, ref numValC);

            string filterName = ((FilterModes)mode).ToString();
            Message = filterName;

            int[] indices = new int[] { 2, 3, 4 };

            switch ((FilterModes)mode)
            {
                case FilterModes.BT709:
                    SetParameter(2);
                    SetParameter(3);
                    SetParameter(4);
                    image.Filters.Add(new Fi.GrayscaleBT709());
                    break;
                case FilterModes.RMY:
                    SetParameter(2);
                    SetParameter(3);
                    SetParameter(4);
                    image.Filters.Add(new Fi.GrayscaleRMY());
                    break;
                case FilterModes.Y:
                    SetParameter(2);
                    SetParameter(3);
                    SetParameter(4);
                    image.Filters.Add(new Fi.GrayscaleY());
                    break;
                case FilterModes.Simple:
                    SetParameter(2, "R", "Red", "The Red coefficient");
                    SetParameter(3, "G", "Green", "The Green coefficient");
                    SetParameter(4, "B", "Blue", "The Blue coefficient");
                    image.Filters.Add(new Fi.Simple(numValA, numValB, numValC));
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
                return Properties.Resources.Bmp_Grayscale;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("69e7fda4-9484-4c65-8fb8-e2e068679759"); }
        }
    }
}