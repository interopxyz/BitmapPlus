using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BitmapPlus.Components
{
    public class GH_Bmp_ConstructBitmap : GH_Bitmap_Base
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_ConstructBitmap class.
        /// </summary>
        public GH_Bmp_ConstructBitmap()
          : base("Build Bitmap", "BuildBmp",
              "Build a bitmap from a width, height, and list of colors",
              Constants.ShortName, "Create")
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
            pManager.AddIntegerParameter("Width", "W", "The width of the new bitmap", GH_ParamAccess.item, 100);
            pManager[0].Optional = true;
            pManager.AddIntegerParameter("Height", "H", "The height of the new bitmap", GH_ParamAccess.item, 100);
            pManager[1].Optional = true;
            pManager.AddColourParameter("Colors", "C", "The list of colors corresponding to the pixels. If the number of colors is less than the total pixel count, the color pattern will be repeated.", GH_ParamAccess.list);
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
            int width = 100;
            DA.GetData(0, ref width);

            int height = 100;
            DA.GetData(1, ref height);

            List<Color> colors = new List<Color>();
            if (!DA.GetDataList(2, colors)) return;

            Img image = new Img(colors, width, height);

            fileImage = new Img(image);
            DA.SetData(0, image.Bmp);
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
                return Properties.Resources.Bmp_BuildBitmap;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("2592f436-0838-4561-85e6-676a2decffe5"); }
        }
    }
}