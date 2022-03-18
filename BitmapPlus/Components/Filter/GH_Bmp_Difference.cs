using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using Fi = BitmapPlus.Filters.Difference;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Difference : GH_Bmp_Filter
    {
        private enum FilterModes { Add=0, Subtract=1, Multiply=2, Divide=3, Merge=4, FlatField=5, Intersect=6, Euclidean=7, Morph=8, MoveTowards=9, Simple=10 }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Difference class.
        /// </summary>
        public GH_Bmp_Difference()
          : base("Difference Filter", "Difference",
              "Apply a Difference filter to an Image" + Properties.Resources.AccordCredit,
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
            pManager.AddNumberParameter("Not Used", "-", "Parameter not used by this filter", GH_ParamAccess.item, 0.5);
            pManager[2].Optional = true;
            pManager.AddGenericParameter("Bitmap", "B", "The bitmap to compare. Should be the same size as the Image", GH_ParamAccess.item);

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

            Img imageOverlay = null;
            IGH_Goo gooA = null;
            if (!DA.GetData(3, ref gooA)) return;
            if (!gooA.TryGetImage(ref imageOverlay))
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Must either be an Image or System.Drawing.Bitmap object.");
                return;
            }
            imageOverlay.Flatten();
            Bitmap overlay = imageOverlay.Bmp;

            if(image.Width!= imageOverlay.Width)
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Both images must be the same width and height.");
                return;
            }

            if (image.Height != imageOverlay.Height)
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Both images must be the same width and height.");
                return;
            }

            string filterName = ((FilterModes)mode).ToString();
            Message = filterName;

            switch ((FilterModes)mode)
            {
                case FilterModes.Add:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Add(overlay));
                    break;
                case FilterModes.Subtract:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Subtract(overlay));
                    break;
                case FilterModes.Multiply:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Multiply(overlay));
                    break;
                case FilterModes.Divide:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Divide(overlay));
                    break;
                case FilterModes.FlatField:
                    SetParameter(2);
                    image.Filters.Add(new Fi.FlatField(overlay));
                    break;
                case FilterModes.Intersect:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Intersect(overlay));
                    break;
                case FilterModes.Merge:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Merge(overlay));
                    break;
                case FilterModes.Euclidean:
                    SetParameter(2, "V", "Threshold", filterName + " Threshold Value Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.Euclidean(overlay, numVal));
                    break;
                case FilterModes.Morph:
                    SetParameter(2, "V", "Percent", filterName + " Percent Value Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.Morph(overlay, numVal));
                    break;
                case FilterModes.MoveTowards:
                    SetParameter(2, "V", "Size", filterName + " Size Value Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.MoveTowards(overlay, numVal));
                    break;
                case FilterModes.Simple:
                    SetParameter(2, "V", "Threshold ", filterName + " Threshold Value Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.Simple(overlay, numVal));
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
                return Properties.Resources.Bmp_Difference;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("757512d9-d752-4bc0-ae3f-ce6d3ff973c4"); }
        }
    }
}