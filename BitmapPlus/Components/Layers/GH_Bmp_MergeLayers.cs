using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BitmapPlus.Components.Layers
{
    public class GH_Bmp_MergeLayers : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Bmp_MergeLayers class.
        /// </summary>
        public GH_Bmp_MergeLayers()
          : base("Merge Images", "MergeImages",
              "Merge Images into a single image." + Environment.NewLine + "Input can be a list of Bitmaps, Images, or Images with Layers assigned" + Properties.Resources.DynamicImageCredit,
                Constants.ShortName, "Layers")
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
            pManager.AddGenericParameter("Images", "I", "An Images or Bitmaps", GH_ParamAccess.list);
            pManager.AddColourParameter("Background Color", "C", "The background color of the image", GH_ParamAccess.item, Color.Transparent);
            pManager[1].Optional = true;
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
            List<IGH_Goo> goos = new List<IGH_Goo>();
            if (!DA.GetDataList(0, goos)) return;

            List<Img> images = new List<Img>();
            List<int> indices = new List<int>();

            int i = 0;
            foreach (IGH_Goo goo in goos)
            {
                Img image = null;
                if (!goo.TryGetImage(ref image))
                {
                    indices.Add(i);
                }
                else
                {
                    images.Add(image);
                }
                i++;
            }
            if(indices.Count>0)
            {
                foreach(int index in indices)
                {
                    this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Item "+index+" must either be an Image or System.Drawing.Bitmap object.");
                }
            }
            Color color = Color.Transparent;
            DA.GetData(1, ref color);
            Composition composition = new Composition(images,color);

            Img img = composition.GetImage();

            DA.SetData(0, img);
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
                return Properties.Resources.Bmp_MergeLayers;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("533ee95a-a6c1-42bb-b86a-e193638e5bdc"); }
        }
    }
}