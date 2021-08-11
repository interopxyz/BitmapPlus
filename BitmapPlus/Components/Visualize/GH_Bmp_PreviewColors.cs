using System;
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
    public class GH_Bmp_PreviewColors : GH_Component
    {
        public List<Sd.Color> backgrounds = new List<Sd.Color>();
        public int Width = 49;
        public int Height = 27;

        /// <summary>
        /// Initializes a new instance of the GH_Ai_PreviewColor class.
        /// </summary>
        public GH_Bmp_PreviewColors()
          : base("Prev Colors", "PrevClr", 
                "Previews a list of Colors as swatches", 
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
            backgrounds = new List<Sd.Color>();
            Width = 49;
            Height = 27;
            m_attributes = new ColorsAttributes_Custom(this);
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddColourParameter("Colors", " ", "The colors to preview", GH_ParamAccess.list);
            pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            //pManager.AddColourParameter("Colors", " ", "The colors", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Sd.Color> colors = new List<Sd.Color>();
            DA.GetDataList(0, colors);

            //DA.SetDataList(0, colors);

            backgrounds = colors;
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            //Width Number Picker
            var labelA = new Label { Text = "Width" };
            labelA.Margin = new Padding(0, 5, 0, 0);

            var numCtrlA = new NumericUpDown();
            numCtrlA.Width = 50;
            numCtrlA.Minimum = 49;
            numCtrlA.Maximum = 5000;
            numCtrlA.Value = (Decimal)Width;
            numCtrlA.ValueChanged -= (o, e) => { SetWidthOffset((int)numCtrlA.Value); };
            numCtrlA.ValueChanged += (o, e) => { SetWidthOffset((int)numCtrlA.Value); };

            FlowLayoutPanel menuItemA = new FlowLayoutPanel();
            menuItemA.FlowDirection = FlowDirection.LeftToRight;
            menuItemA.Height = 30;
            menuItemA.Controls.Add(numCtrlA);
            menuItemA.Controls.Add(labelA);

            Menu_AppendCustomItem(menu, menuItemA);

            //Width Number Picker
            var labelB = new Label { Text = "Height" };
            labelB.Margin = new Padding(0, 5, 0, 0);

            var numCtrlB = new NumericUpDown();
            numCtrlB.Width = 50;
            numCtrlB.Minimum = 27; 
            numCtrlB.Maximum = 500;
            numCtrlB.Value = (Decimal)Height;
            numCtrlB.ValueChanged -= (o, e) => { SetHeightOffset((int)numCtrlB.Value); };
            numCtrlB.ValueChanged += (o, e) => { SetHeightOffset((int)numCtrlB.Value); };

            FlowLayoutPanel menuItemB = new FlowLayoutPanel();
            menuItemB.FlowDirection = FlowDirection.LeftToRight;
            menuItemB.Height = 30;
            menuItemB.Controls.Add(numCtrlB);
            menuItemB.Controls.Add(labelB);

            Menu_AppendCustomItem(menu, menuItemB);
        }

        public void SetWidthOffset(int value)
        {
            Width = value;
            this.ExpireSolution(true);
        }

        public void SetHeightOffset(int value)
        {
            Height = value;
            this.ExpireSolution(true);
        }

        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            writer.SetInt32("Width", Width);
            writer.SetInt32("Height", Height);

            return base.Write(writer);
        }
        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            Width = reader.GetInt32("Width");
            Height = reader.GetInt32("Height");

            return base.Read(reader);
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
                return Properties.Resources.Bmp_ColorPreview;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("835f062a-07cf-405f-bbf5-52c719acc9c5"); }
        }
    }


    public class ColorsAttributes_Custom : GH_ComponentAttributes
    {
        public ColorsAttributes_Custom(GH_Component owner) : base(owner) { }

        private Sd.Rectangle ButtonBounds { get; set; }
        protected override void Layout()
        {
            base.Layout();
            GH_Bmp_PreviewColors comp = Owner as GH_Bmp_PreviewColors;

            Sd.Rectangle rec0 = GH_Convert.ToRectangle(Bounds);

            int width = comp.Width;
            int height = comp.Height;
            if (comp.backgrounds.Count > 0) height = (int)(comp.Height-6) * comp.backgrounds.Count+6;

            rec0.Width = width;
            rec0.Height = height;

            Sd.Rectangle rec1 = rec0;
            rec1.X = rec1.Left + 3;
            rec1.Y = rec1.Bottom - height + 3;
            rec1.Height = height - 6;
            rec1.Width = width - 6;

            Bounds = rec0;
            ButtonBounds = rec1;

        }

        protected override void Render(GH_Canvas canvas, Sd.Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);
            GH_Bmp_PreviewColors comp = Owner as GH_Bmp_PreviewColors;

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

                int count = comp.backgrounds.Count;
                if (count > 0)
                {
                    int height = (int)(comp.Height - 6);

                    for (int i = 0; i < count; i++)
                    {
                        Sd.RectangleF fillRectangle = new Sd.RectangleF(ButtonBounds.Left, ButtonBounds.Top + i * (comp.Height - 6), ButtonBounds.Width, (comp.Height - 6));
                        Sd.Rectangle borderRectangle = new Sd.Rectangle(ButtonBounds.Left, ButtonBounds.Top + i * (comp.Height - 6), ButtonBounds.Width, (comp.Height - 6));

                        graphics.FillRectangle(new Sd.SolidBrush(comp.backgrounds[i]), fillRectangle);
                        graphics.DrawRectangle(new Sd.Pen(Sd.Color.Black, 1), borderRectangle);

                    }
                }
                else
                {
                    graphics.FillRectangle(new Sd.SolidBrush(Sd.Color.White), ButtonBounds);
                }

                format.Dispose();
            }
        }
    }
}