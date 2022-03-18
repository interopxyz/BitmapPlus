using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace BitmapPlus.Components
{
    public class GH_Bmp_OpenBitmap : GH_Bitmap_Base
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_OpenBitmap class.
        /// </summary>
        public GH_Bmp_OpenBitmap()
          : base("Open Bitmap", "OpenBmp",
                "Open an Bitmap file and return a Bitmap object",
                Constants.ShortName, "File")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("File Path", "P", "The path to the bitmap file", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Load", "L", "Load the bitmap if true", GH_ParamAccess.item, false);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "The loaded Bitmap object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string P = null;
            if (!DA.GetData(0, ref P)) return;

            bool load = false;
            if (!DA.GetData(1, ref load)) return;

            if (!File.Exists(P))
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "The file provided path does not exist. Please verify this is a valid file path.");
                return;
            }

            if (load)
            {
                Bitmap bitmap = null;
                if (!P.GetBitmapFromFile(out bitmap))
                {
                    if (!Path.HasExtension(P))
                    {
                        this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "This is not a valid file path. This file does not have a valid bitmap extension"); this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "This is not a valid file path. This file does not have a valid bitmap extension");
                        return;
                    }
                    else
                    {
                        this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "This is not a valid bitmap file type. The extension " + Path.GetExtension(P) + " is not a supported bitmap format");
                        return;
                    }
                }


                fileImage = new Img(bitmap);
                DA.SetData(0, bitmap);
            }
            else
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "To load a file the Load input must be set to true");
            }

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
                return Properties.Resources.Bmp_LoadBitmap;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0df87ce6-1b5c-4b33-a1c5-3eec247e132b"); }
        }
    }
}