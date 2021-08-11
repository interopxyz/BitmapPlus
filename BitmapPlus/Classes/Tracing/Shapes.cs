using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Accord.Imaging;
using Ag = Accord.Math.Geometry;

using Rg = Rhino.Geometry;

namespace BitmapPlus
{
    public class Shapes : Blobs
    {

        #region Members

        protected List<Rg.Curve> circles = new List<Rg.Curve>();
        protected List<Rg.Curve> triangles = new List<Rg.Curve>();
        protected List<Rg.Curve> quadrilaterals = new List<Rg.Curve>();
        protected List<Rg.Curve> polylines = new List<Rg.Curve>();

        public double Angle = -1;
        public double Length = -1;
        public double Distortion = -1;

        #endregion

        #region Constructors

        public Shapes() : base()
        {

        }

        public Shapes(Bitmap bitmap) : base(bitmap)
        {
        }

        public Shapes(Shapes shapes) : base(shapes)
        {
            this.Angle = shapes.Angle;
            this.Length = shapes.Length;
        }

        #endregion

        #region Properties

        public virtual List<Rg.Curve> GetCircles { get { return circles; } }
        public virtual List<Rg.Curve> GetTriangles { get { return triangles; } }
        public virtual List<Rg.Curve> GetQuadrilaterals { get { return quadrilaterals; } }
        public virtual List<Rg.Curve> GetPolylines { get { return polylines; } }

        #endregion

        #region Methods

        public void CalculateShapes()
        {
            circles.Clear();
            triangles.Clear();
            quadrilaterals.Clear();
            polylines.Clear();

            CalculateBlobs();

            Ag.SimpleShapeChecker shapeChecker = new Ag.SimpleShapeChecker();

            if (Angle >= 0) shapeChecker.AngleError = (float)Angle;
            if (Length >= 0) shapeChecker.LengthError = (float)Length;
            if (Distortion >= 0) shapeChecker.MinAcceptableDistortion = (float)Distortion;

            foreach (Blob blob in blobObjects)
            {
                List<Accord.IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blob);

                Accord.Point center;
                float radius;

                if (shapeChecker.IsCircle(edgePoints, out center, out radius))
                {
                    circles.Add(new Rg.Circle(Rg.Plane.WorldXY, center.ToRhPoint(bitmap.Height), radius).ToNurbsCurve());
                }
                else
                {
                    List<Accord.IntPoint> corners;
                    if (edgePoints.Count > 2)
                    {
                        if (shapeChecker.IsConvexPolygon(edgePoints, out corners))
                        {
                            if (shapeChecker.IsTriangle(edgePoints))
                            {
                                triangles.Add(corners.ToRhinoPoints(bitmap.Height).ToPolyline(true).ToNurbsCurve());
                            }
                            else if (shapeChecker.IsQuadrilateral(edgePoints))
                            {
                                quadrilaterals.Add(corners.ToRhinoPoints(bitmap.Height).ToPolyline(true).ToNurbsCurve());
                            }
                            else
                            {
                                polylines.Add(corners.ToRhinoPoints(bitmap.Height).ToPolyline(true).ToNurbsCurve());
                            }
                        }
                    }

                }
            }
        }


        #endregion


    }
}
