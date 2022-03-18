using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BitmapPlus.Components.Layers
{
    public class GH_Bmp_ModifyLayer : GH_Bitmap_Base
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_ModifyLayer class.
        /// </summary>
        public GH_Bmp_ModifyLayer()
          : base("Modify Layer", "ModLyr",
              "Modify a layer" + Properties.Resources.DynamicImageCredit,
                Constants.ShortName, "Layers")
        {
            Message = ((Modifier.ModifierModes)0).ToString();
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
            pManager.AddGenericParameter("Image", "I", "A Bitmap Plus Image or Bitmap", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Mode", "M", "The layer modifier mode ", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Value", "V", "The parameter value for the modifier. Not used in all Modes.", GH_ParamAccess.item, 0.0);
            pManager[2].Optional = true;
            pManager.AddColourParameter("Color", "C", "Color parameter for the modifier. Not used in all Modes.", GH_ParamAccess.item, Color.Transparent);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)pManager[1];
            foreach (Modifier.ModifierModes value in Enum.GetValues(typeof(Modifier.ModifierModes)))
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
            if (mode == 0)
            {
                DA.SetData(0, image);
                return;
            }
            Modifier modifier = new Modifier((Modifier.ModifierModes)mode);

            double value = 0.0;
            if (DA.GetData(2, ref value)) modifier.Value = value;

            Color color = Color.Black;
            if (DA.GetData(3, ref color)) modifier.Color = color;

            image.Layer.Modifiers.Add(modifier);

            fileImage = new Img(image);
            DA.SetData(0, image);
            Message = ((Modifier.ModifierModes)mode).ToString();
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
                return Properties.Resources.Bmp_Modify;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e4c5efee-5d7c-4c2d-9510-b22035f16fe4"); }
        }
    }
}