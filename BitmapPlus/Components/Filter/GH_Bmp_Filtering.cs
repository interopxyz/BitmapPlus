using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using Fi = BitmapPlus.Filters.Filtering;

namespace BitmapPlus.Components.Filter
{
    public class GH_Bmp_Filtering : GH_Bmp_Filter
    {
        public enum FilterModes { Channel, RGB, HSL, YCbCr };

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_Filtering class.
        /// </summary>
        public GH_Bmp_Filtering()
          : base("Filtering Filter", "Filtering",
              "Apply a Filtering filter to an Image" + Properties.Resources.AccordCredit,
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
            pManager.AddIntegerParameter("Mode", "M", "", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;

            pManager.AddIntervalParameter("Red", "R", "[0-1] Unitized adjustment value", GH_ParamAccess.item, new Interval(0, 1));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Green", "G", "[0-1] Unitized adjustment value", GH_ParamAccess.item, new Interval(0, 1));
            pManager[3].Optional = true;
            pManager.AddIntervalParameter("Blue", "B", "[0-1] Unitized adjustment value", GH_ParamAccess.item, new Interval(0, 1));
            pManager[4].Optional = true;

            pManager.AddBooleanParameter("Outside", "F", "Flip between inside and outside range", GH_ParamAccess.item, true);
            pManager[5].Optional = true;

            pManager.AddColourParameter("Color", "C", "Replacement Color", GH_ParamAccess.item, Color.Black);
            pManager[6].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[1];
            foreach (FilterModes value in Enum.GetValues(typeof(FilterModes)))
            {
                paramA.AddNamedValue(value.ToString(), (int)value);
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

            Interval valA = new Interval(0, 1);
            DA.GetData(2, ref valA);
            Interval valB = new Interval(0, 1);
            DA.GetData(3, ref valB);
            Interval valC = new Interval(0, 1);
            DA.GetData(4, ref valC);

            bool flip = true;
            DA.GetData(5, ref flip);

            Color color = Color.Black;
            DA.GetData(6, ref color);

            string filterName = ((FilterModes)mode).ToString();
            Message = filterName;

            switch ((FilterModes)mode)
            {
                case FilterModes.Channel:
                    SetParameter(2, "R", "Red", "[0-1] Unitized adjustment value");
                    SetParameter(3, "G", "Green", "[0-1] Unitized adjustment value");
                    SetParameter(4, "B", "Blue", "[0-1] Unitized adjustment value");
                    image.Filters.Add(new Fi.Channel(color,valA, valB, valC, flip));
                    break;
                case FilterModes.HSL:
                    SetParameter(2, "H", "Hue", "[0-1] Unitized adjustment value");
                    SetParameter(3, "S", "Saturation", "[0-1] Unitized adjustment value");
                    SetParameter(4, "L", "Luminance", "[0-1] Unitized adjustment value");
                    image.Filters.Add(new Fi.Colour(color, valA, valB, valC, flip));
                    break;
                case FilterModes.RGB:
                    SetParameter(2, "R", "Red", "[0-1] Unitized adjustment value");
                    SetParameter(3, "G", "Green", "[0-1] Unitized adjustment value");
                    SetParameter(4, "B", "Blue", "[0-1] Unitized adjustment value");
                    image.Filters.Add(new Fi.HSL(color, valA, valB, valC, flip));
                    break;
                case FilterModes.YCbCr:
                    SetParameter(2, "Y", "Y", "[0-1] Unitized adjustment value");
                    SetParameter(3, "Cb", "Cb", "[0-1] Unitized adjustment value");
                    SetParameter(4, "Cr", "Cr", "[0-1] Unitized adjustment value");
                    image.Filters.Add(new Fi.YCbCr(color, valA, valB, valC, flip));
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
                return Properties.Resources.Bmp_Filters;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("90661f72-37ca-4a1f-b34b-37aa2a413c1e"); }
        }
    }
}