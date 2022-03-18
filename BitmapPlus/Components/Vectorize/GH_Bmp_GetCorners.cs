using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BitmapPlus.Components.Vectorize
{
    public class GH_Bmp_GetCorners : GH_Component
    {
        public enum CornerModes { Fast, Susan, Morvec, Harris }

        /// <summary>
        /// Initializes a new instance of the GH_Bmp_GetCorners class.
        /// </summary>
        public GH_Bmp_GetCorners()
          : base("Bitmap Corners", "BmpCorners",
              "Get Corners from Bitmap Figures" + Properties.Resources.AccordCredit,
                Constants.ShortName, "Vectorize")
        {
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
            pManager.AddGenericParameter("Image", "I", "A Bitmap Plus Image or Bitmap", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Mode", "M", "Corner detection mode", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Threshold", "T", "Harris threshold [0-1]", GH_ParamAccess.item, 0.5);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Value", "V", "Sigma value", GH_ParamAccess.item, 100.00);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)pManager[1];
            foreach (CornerModes value in Enum.GetValues(typeof(CornerModes)))
            {
                param.AddNamedValue(value.ToString(), (int)value);
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "Corner Points", GH_ParamAccess.list);
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

            image.Flatten();
            Bitmap bitmap = image.Bmp;

            Corners corners = new Corners(bitmap);

            double threshold = 1.0;
            DA.GetData(2, ref threshold);
            corners.Threshold = (int)(threshold * 255.0);

            double valueModifier = 1.0;
            if (DA.GetData(3, ref valueModifier)) corners.Value = valueModifier;

            int mode = 0;
            DA.GetData(1, ref mode);

            List<Point3d> points = new List<Point3d>();

            switch ((CornerModes)mode)
            {
                default:
                    points = corners.GetSusanCorners();
                    break;
                case CornerModes.Fast:
                    points = corners.GetFastCorners();
                    break;
                case CornerModes.Harris:
                    points = corners.GetHarrisCorners();
                    break;
                case CornerModes.Morvec:
                    points = corners.GetMorvacCorners();
                    break;
            }

            DA.SetDataList(0, points);


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
                return Properties.Resources.Bmp_TraceCorners;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("c6cd18c4-154e-4515-bf97-f5a2850f8061"); }
        }
    }
}