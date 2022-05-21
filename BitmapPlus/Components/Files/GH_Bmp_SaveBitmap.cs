using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace BitmapPlus.Components
{
    public class GH_Bmp_SaveBitmap : GH_Bitmap_Base
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_SaveImage class.
        /// </summary>
        public GH_Bmp_SaveBitmap()
          : base("Save Bitmap", "SaveBmp",
                "Save an Image or Bitmap to File",
                Constants.ShortName, "File")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Image / Bitmap", "I", "An Image or Bitmap", GH_ParamAccess.item);
            pManager.AddTextParameter("Folder Path", "F", "The folder path to save the file", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddTextParameter("File Name", "N", "The file name for the bitmap", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Extension", "E", "File type extension", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
            pManager.AddBooleanParameter("Save", "S", "If true, save image file", GH_ParamAccess.item, false);
            pManager[4].Optional = true;

            Param_Integer param = (Param_Integer)pManager[3];
            param.AddNamedValue("png", 0);
            param.AddNamedValue("jpeg", 1);
            param.AddNamedValue("bmp", 2);
            param.AddNamedValue("tiff", 3);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Filepath", "P", "The full path to the new file", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool save = false;
            DA.GetData(4, ref save);
            if (save) {
            IGH_Goo goo = null;
            Img image = null;
            if (!DA.GetData(0, ref goo)) return;
            if (!goo.TryGetImage(ref image))
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Must either be an Image or System.Drawing.Bitmap object.");
                return;
            }

            image.Flatten();

            string path = "C:\\Users\\Public\\Documents\\";
            bool hasPath = DA.GetData(1, ref path);

            string name = DateTime.UtcNow.ToString("yyyy-dd-M_HH-mm-ss");
            bool hasName = DA.GetData(2, ref name);

            int extension = 0;
            DA.GetData(3, ref extension);
            if (extension < 0) extension = 0;
            if (extension > 3) extension = 3;

            Bitmap bitmap = image.Bmp;
            fileImage = new Img(image);

            if (!hasPath)
            {
                if (this.OnPingDocument().FilePath != null)
                {
                    path = Path.GetDirectoryName(this.OnPingDocument().FilePath) + "\\";
                }
            }

            name = name.StripExtension();

            if (!Directory.Exists(path))
            {
                    Directory.CreateDirectory(path);
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "The provided folder path does not exist. A new directory has been created.");
            }

            string ext = ".png";
            System.Drawing.Imaging.ImageFormat encoding = System.Drawing.Imaging.ImageFormat.Png;
            switch (extension)
            {
                case 1:
                    ext = ".jpeg";
                    encoding = System.Drawing.Imaging.ImageFormat.Jpeg;
                    break;
                case 2:
                    ext = ".bmp";
                    encoding = System.Drawing.Imaging.ImageFormat.Bmp;
                    break;
                case 3:
                    ext = ".tiff";
                    encoding = System.Drawing.Imaging.ImageFormat.Tiff;
                    break;
            }

            string filepath = path + name + ext;

            if (save)
            {
                bitmap.Save(filepath, encoding);
                bitmap.Dispose();

                DA.SetData(0, filepath);
            }
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
                return Properties.Resources.Bmp_SaveBitmap;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("9e8f66d2-7c57-49ea-8fbc-63fb50aa50e4"); }
        }
    }
}