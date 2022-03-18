using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Fi = BitmapPlus.Filters.Effects;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Effects : GH_Bmp_Filter
    {
        private enum FilterModes { Additive, SaltPepper, Daube, Jitter, Kuwahara, Posterize, Blur, GaussianBlur, Pixellate }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Effects class.
        /// </summary>
        public GH_Bmp_Effects()
          : base("Effects Filter", "Effects",
              "Apply an Effects filter to an Image" + Properties.Resources.AccordCredit,
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
            pManager.AddNumberParameter("Not Used", "-", "Parameter not used by this filter", GH_ParamAccess.item, 1.0);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Not Used", "-", "Parameter not used by this filter", GH_ParamAccess.item, 1.0);
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

            double numValA = 0;
            DA.GetData(2, ref numValA);

            double numValB = 0;
            DA.GetData(3, ref numValB);

            string filterName = ((FilterModes)mode).ToString();
            Message = filterName;

            switch ((FilterModes)mode)
            {
                case FilterModes.Additive:
                    SetParameter(2);
                    SetParameter(3);
                    image.Filters.Add(new Fi.Additive());
                    break;
                case FilterModes.Daube:
                    SetParameter(2, "S", "Size", filterName + " Size Value. Unitized adjustment value (0-1)");
                    SetParameter(3);
                    image.Filters.Add(new Fi.Daube(numValA));
                    break;
                case FilterModes.SaltPepper:
                    SetParameter(2, "N", "Noise", filterName + " Noise Value. Unitized adjustment value (0-1)");
                    SetParameter(3);
                    image.Filters.Add(new Fi.SaltPepper(numValA));
                    break;
                case FilterModes.Jitter:
                    SetParameter(2, "R", "Radius", filterName + " Radius Value. Unitized adjustment value (0-1)");
                    SetParameter(3);
                    image.Filters.Add(new Fi.Jitter(numValA));
                    break;
                case FilterModes.Kuwahara:
                    SetParameter(2, "S", "Size", filterName + " Size Value. Unitized adjustment value (0-1)");
                    SetParameter(3);
                    image.Filters.Add(new Fi.Kuwahara(numValA));
                    break;
                case FilterModes.Posterize:
                    SetParameter(2, "I", "Interval", filterName + " Size Value. Unitized adjustment value (0-1)");
                    SetParameter(3);
                    image.Filters.Add(new Fi.Posterize(numValA));
                    break;
                case FilterModes.GaussianBlur:
                    SetParameter(2, "X", "Sigma", filterName + " Sigma Value. Unitized adjustment value (0-1)");
                    SetParameter(3, "S", "Size", filterName + " Size Value. Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.GaussianBlur(numValA, numValB));
                    break;
                case FilterModes.Pixellate:
                    SetParameter(2, "W", "Width", filterName + " Width Value. Unitized adjustment value (0-1)");
                    SetParameter(3, "H", "Height", filterName + " Height Value. Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.Pixellate(numValA, numValB));
                    break;
                case FilterModes.Blur:
                    SetParameter(2, "D", "Divisor", filterName + " Divisor Value. Unitized adjustment value (0-1)");
                    SetParameter(3, "T", "Threshold", filterName + " Threshold Value. Unitized adjustment value (0-1)");
                    image.Filters.Add(new Fi.Blur(numValA, numValB));
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
                return Properties.Resources.Bmp_Effects;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("bc6b40ab-a0d9-4222-8d46-3eca2be06cfd"); }
        }
    }
}