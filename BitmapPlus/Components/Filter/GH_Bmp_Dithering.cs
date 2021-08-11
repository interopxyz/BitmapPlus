using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Dither;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Dithering : GH_Bmp_Filter
    {
        private enum FilterModes { Bayer, Ordered, Burkes, Carry, FloydSteinberg, JarvisJudiceNinke, Sierra, Stucki }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Dithering class.
        /// </summary>
        public GH_Bmp_Dithering()
          : base("Dithering Filter", "Dithering",
              "Apply a Dithering filter to an Image" + Properties.Resources.AccordCredit,
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

            double numVal = 0;
            DA.GetData(2, ref numVal);

            string filterName = ((FilterModes)mode).ToString();
            Message = filterName;

            switch ((FilterModes)mode)
            {
                case FilterModes.Bayer:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Bayer());
                    break;
                case FilterModes.Ordered:
                    SetParameter(2);
                    image.Filters.Add(new Fi.Ordered());
                    break;
                case FilterModes.Burkes:
                    SetParameter(2, "V", "Threshold", filterName + " Threshold Value [0-1] Unitized adjustment value");
                    image.Filters.Add(new Fi.Burkes(numVal));
                    break;
                case FilterModes.Carry:
                    SetParameter(2, "V", "Threshold", filterName + " Threshold Value [0-1] Unitized adjustment value");
                    image.Filters.Add(new Fi.Carry(numVal));
                    break;
                case FilterModes.FloydSteinberg:
                    SetParameter(2, "V", "Threshold", filterName + " Threshold Value [0-1] Unitized adjustment value");
                    image.Filters.Add(new Fi.FloydSteinberg(numVal));
                    break;
                case FilterModes.JarvisJudiceNinke:
                    SetParameter(2, "V", "Threshold", filterName + " Threshold Value [0-1] Unitized adjustment value");
                    image.Filters.Add(new Fi.JarvisJudiceNinke(numVal));
                    break;
                case FilterModes.Sierra:
                    SetParameter(2, "V", "Threshold", filterName + " Threshold Value [0-1] Unitized adjustment value");
                    image.Filters.Add(new Fi.Sierra(numVal));
                    break;
                case FilterModes.Stucki:
                    SetParameter(2, "V", "Threshold", filterName + " Threshold Value [0-1] Unitized adjustment value");
                    image.Filters.Add(new Fi.Stucki(numVal));
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
                return Properties.Resources.Bmp_Dither;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("c3e7e214-cd63-4f5a-b7c2-fb9f651a86fd"); }
        }
    }
}