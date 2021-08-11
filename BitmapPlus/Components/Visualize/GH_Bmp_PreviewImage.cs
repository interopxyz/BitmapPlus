using System;
using System.IO;
using System.Collections.Generic;
using Sd = System.Drawing;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System.Windows.Forms;

namespace BitmapPlus.Components
{
    public class GH_Bmp_PreviewImage : GH_Component
    {
        public Sd.Image img = null;
        string message = "Nothing here";

        /// <summary>
        /// Initializes a new instance of the PreviewBitmap class.
        /// </summary>
        public GH_Bmp_PreviewImage()
          : base("Prev Image", "PrevImg",
                "Previews a bitmap",
                Constants.ShortName, "Visualize")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        public override void CreateAttributes()
        {
            img = Properties.Resources.BitmapPlus_Logo_200;
            m_attributes = new Attributes_Custom(this);
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Smart Bitmap", "S", "Smart Bitmap or Bitmap image to preview (right-click to save)", GH_ParamAccess.item);
            pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            //pManager.AddGenericParameter("Image", "I", "An Image object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Goo goo = null;
            Img image = null;
            if (!DA.GetData(0, ref goo))
            {
                img = Properties.Resources.BitmapPlus_Logo_200;
                return;
            }
            if (!goo.TryGetImage(ref image))
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Must either be an Image or System.Drawing.Bitmap object.");
                return;
            }

            image.Flatten();
            img = image.Bmp;
            message = "(" + img.Width + "x" + img.Height + ") " + img.PixelFormat.ToString();
            UpdateMessage();
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Save Image", SaveImage, true, false);

        }

        public void SaveImage(Object sender, EventArgs e)
        {

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
                        img.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case 2:
                        img.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case 3:
                        img.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 4:
                        img.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                }

                fs.Close();

                this.ExpireSolution(true);
            }
        }

        private void UpdateMessage()
        {
            Message = message;
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
                return Properties.Resources.Bmp_Preview;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("87be230e-03e8-44b4-9d5c-f77338700b6e"); }
        }
    }

    public class Attributes_Custom : GH_ComponentAttributes
    {
        public Attributes_Custom(GH_Component owner) : base(owner) { }

        private Sd.Rectangle ButtonBounds { get; set; }
        protected override void Layout()
        {
            base.Layout();
            GH_Bmp_PreviewImage comp = Owner as GH_Bmp_PreviewImage;

            int width = comp.img.Width;
            if (width < 50) width = 50;
            int height = comp.img.Height;
            if (height < 50) height = 50;
            Sd.Rectangle rec0 = GH_Convert.ToRectangle(Bounds);

            int cWidth = rec0.Width;
            int cHeight = rec0.Height;

            rec0.Width = width;
            rec0.Height += height;

            Sd.Rectangle rec1 = rec0;
            rec1.Y = rec1.Bottom - height;
            rec1.Height = height;
            rec1.Width = width;

            Bounds = rec0;
            ButtonBounds = rec1;

        }

        protected override void Render(GH_Canvas canvas, Sd.Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);
            GH_Bmp_PreviewImage comp = Owner as GH_Bmp_PreviewImage;

            if (channel == GH_CanvasChannel.Objects)
            {
                GH_Capsule capsule = GH_Capsule.CreateCapsule(ButtonBounds, GH_Palette.Normal, 0, 0);
                capsule.Render(graphics, Selected, Owner.Locked, true);
                capsule.AddOutputGrip(this.OutputGrip.Y);
                capsule.Dispose();
                capsule = null;

                Sd.StringFormat format = new Sd.StringFormat();
                format.Alignment = Sd.StringAlignment.Center;
                format.LineAlignment = Sd.StringAlignment.Center;

                Sd.RectangleF textRectangle = ButtonBounds;

                graphics.DrawImage(comp.img, Bounds.X + 2, m_innerBounds.Y - (ButtonBounds.Height - Bounds.Height), comp.img.Width - 4, comp.img.Height - 4);

                format.Dispose();
            }
        }
    }
}