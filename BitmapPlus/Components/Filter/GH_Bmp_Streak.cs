using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Figures;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Streak: GH_Component
    {
        private enum FilterModes { HorizontalBands, VerticalBands, FillHoles }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Figure class.
        /// </summary>
        public GH_Bmp_Streak()
          : base("Streak Filter", "Streak",
              "Apply a Streak filter to an Image" + Properties.Resources.AccordCredit,
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
            pManager.AddBooleanParameter("Borders", "B", "Process gaps", GH_ParamAccess.item, false);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Threshold", "T", "The pixel threshold", GH_ParamAccess.item, 1);
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

            bool valueA = false;
            DA.GetData(2, ref valueA);

            int valueB = 1;
            DA.GetData(3, ref valueB);

            string filterName = ((FilterModes)mode).ToString();
            Message = filterName;

            switch ((FilterModes)mode)
            {
                case FilterModes.HorizontalBands:
                    image.Filters.Add(new Fi.BandsHorizontal(valueB, valueA));
                    break;
                case FilterModes.VerticalBands:
                    image.Filters.Add(new Fi.BandsVertical(valueB, valueA));
                    break;
                case FilterModes.FillHoles:
                    image.Filters.Add(new Fi.FillHoles(valueB, valueB, valueA));
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
                return Properties.Resources.Bmp_Streak;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("654d8496-d7d5-4e9b-a161-23d7088a867f"); }
        }
    }
}