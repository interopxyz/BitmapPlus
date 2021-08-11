using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace BitmapPlus.Components.Layers
{
    public class GH_Bmp_TransformLayer : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_TransformLayer class.
        /// </summary>
        public GH_Bmp_TransformLayer()
          : base("Transform Layers", "XfrmLyr",
              "Transform a layer" + Properties.Resources.DynamicImageCredit,
                Constants.ShortName, "Layers")
        {
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
            pManager.AddVectorParameter("Translation Vector", "V", "The optional vector's [x,y] values will be used to move the layer", GH_ParamAccess.item, new Vector3d());
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Angle", "A", "The rotation angle of the Image in the layer", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Width", "W", "A new pixel width of the image", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Height", "H", "A new pixel height of the image", GH_ParamAccess.item, 0);
            pManager[4].Optional = true;
            pManager.AddIntegerParameter("Mode", "M", "Fitting modes for resize", GH_ParamAccess.item, 0);
            pManager[5].Optional = true;

            Param_Integer param = (Param_Integer)pManager[5];
            foreach (Layer.FittingModes value in Enum.GetValues(typeof(Layer.FittingModes)))
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

            Vector3d vector = new Vector3d();
            if (DA.GetData(1, ref vector))
            {
                image.Layer.X = (int)vector.X;
                image.Layer.Y = (int)vector.Y;
            };

            int angle = 0;
            if (DA.GetData(2, ref angle)) image.Layer.Angle = angle;

            int width = 0;
            if (DA.GetData(3, ref width)) image.Layer.Width = width;

            int height = 0;
            if (DA.GetData(4, ref height)) image.Layer.Height = height;

            int mode = 0;
            if (DA.GetData(5, ref mode)) image.Layer.FittingMode = (Layer.FittingModes)mode;

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
                return Properties.Resources.Bmp_XformLayer;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("065bf209-0a28-46fd-ae71-178264eeb6f4"); }
        }
    }
}