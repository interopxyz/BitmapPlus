using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Figures;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Figure : GH_Bitmap_Base
    {
        private enum FilterModes { Closing, Dilation, DilationBinary, Erosion, ErosionBinary, HatBottom, HatTop, Opening, Skeletonization, SkeletonizationZhangSuen }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Figure class.
        /// </summary>
        public GH_Bmp_Figure()
          : base("Figure Filter", "Figure",
              "Apply a Figure filter to an Image" + Properties.Resources.AccordCredit,
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

            string filterName = ((FilterModes)mode).ToString();
            Message = filterName;

            switch ((FilterModes)mode)
            {
                case FilterModes.Closing:
                    image.Filters.Add(new Fi.Closing());
                    break;
                case FilterModes.Dilation:
                    image.Filters.Add(new Fi.Dilation());
                    break;
                case FilterModes.DilationBinary:
                    image.Filters.Add(new Fi.DilationBinary());
                    break;
                case FilterModes.Erosion:
                    image.Filters.Add(new Fi.Erosion());
                    break;
                case FilterModes.ErosionBinary:
                    image.Filters.Add(new Fi.ErosionBinary());
                    break;
                case FilterModes.HatBottom:
                    image.Filters.Add(new Fi.HatBottom());
                    break;
                case FilterModes.HatTop:
                    image.Filters.Add(new Fi.HatTop());
                    break;
                case FilterModes.Opening:
                    image.Filters.Add(new Fi.Opening());
                    break;
                case FilterModes.Skeletonization:
                    image.Filters.Add(new Fi.Skeletonization());
                    break;
                case FilterModes.SkeletonizationZhangSuen:
                    image.Filters.Add(new Fi.SkeletonizationZhangSuen());
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
                return Properties.Resources.Bmp_Figures;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("b3295cd2-69a5-4108-aebd-dbf402a79729"); }
        }
    }
}