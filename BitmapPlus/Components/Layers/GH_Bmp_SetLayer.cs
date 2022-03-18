using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace BitmapPlus.Components.Layers
{
    public class GH_Bmp_SetLayer : GH_Bitmap_Base
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_CreateLayer class.
        /// </summary>
        public GH_Bmp_SetLayer()
          : base("Set Layer", "SetLyr",
              "Applies a layer to an image"+Properties.Resources.DynamicImageCredit,
                Constants.ShortName, "Layers")
        {
            Message = ((Layer.BlendModes)0).ToString();
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
            pManager.AddIntegerParameter("Blend Mode", "B", "The transparency blend mode.", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Opacity", "O", "An opacity value [0-1]", GH_ParamAccess.item, 1.0);
            pManager[2].Optional = true;
            pManager.AddGenericParameter("Mask", "M", "An optional Image or Bitmap for an opacity mask", GH_ParamAccess.item);
            pManager[3].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[1];
            foreach (Layer.BlendModes value in Enum.GetValues(typeof(Layer.BlendModes)))
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

            Layer layer = new Layer();

            IGH_Goo gooM = null;
            Img mask = null;
            if (DA.GetData(3, ref gooM))
            {
                if (!gooM.TryGetImage(ref mask))
                {
                    this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Must either be an Image or System.Drawing.Bitmap object.");
                    return;
                }
                layer.Mask = mask.Bmp;
            }

            int blendMode = 0;
            DA.GetData(1, ref blendMode);

            double opacity = 1.0;
            DA.GetData(2, ref opacity);

            layer.BlendMode = (Layer.BlendModes)blendMode;
            layer.Opacity = 100.0 * opacity;

            image.Layer = layer;
            Message = ((Layer.BlendModes)blendMode).ToString();

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
                return Properties.Resources.Bmp_AddLayer;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("aef6de61-634b-4ffb-b648-34567f9be92a"); }
        }
    }
}