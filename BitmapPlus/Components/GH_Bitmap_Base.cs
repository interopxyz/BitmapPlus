using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sd = System.Drawing;

namespace BitmapPlus.Components
{
    public abstract class GH_Bitmap_Base : GH_Component
    {
        public Img fileImage = new Img();
        /// <summary>
        /// Initializes a new instance of the GH_Bitmap_Base class.
        /// </summary>
        public GH_Bitmap_Base()
          : base("Base Componenent", "Base Componenent",
              "If you see this, something is wrong",
                Constants.ShortName, "Hidden")
        {
        }

        public GH_Bitmap_Base(string Name, string NickName, string Description, string Category, string Subcategory) : base(Name, NickName, Description, Category, Subcategory)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
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
                return null;
            }
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Save Image", SaveImage, true, false);
            Menu_AppendItem(menu, "Copy Image to Clipboard", CopyImage, true, false);

        }

        public void CopyImage(Object sender, EventArgs e)
        {
            Bitmap bmp = fileImage.GetFlatBitmap();

            Clipboard.Clear();
            Clipboard.SetDataObject(bmp);
        }


        public void SaveImage(Object sender, EventArgs e)
        {
            Bitmap bmp = fileImage.GetFlatBitmap();

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPEG Image|*.jpg|PNG Image|*.png|BMP Image|*.bmp|TIFF Image|*.tiff";
            saveFileDialog1.Title = "Save an Image";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        bmp.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case 2:
                        bmp.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case 3:
                        bmp.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 4:
                        bmp.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                }

                fs.Close();

                this.ExpireSolution(true);
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("3aac6ca5-a75c-41c1-8405-90bb35f086d8"); }
        }
    }
}