using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw = System.Windows;
using Sm = System.Windows.Media;
using Ws = System.Windows.Shapes;
using Pt = CsPotrace;
using Rg = Rhino.Geometry;
using System.Collections;

namespace BitmapPlus
{
    public static class TraceBitmap
    {
        public enum TurnModes { Black, White, Majority, Minority, Right, Left };

        #region Tracing

        public static List<Rg.Polyline> TraceToRhino(this Bitmap input, double threshold, double alpha, double tolerance, int size, bool optimize, TurnModes mode)
        {
            List<List<Pt.Curve>> crvs = new List<List<Pt.Curve>>();
            List<Rg.Polyline> polylines = new List<Rg.Polyline>();
            int height = input.Height;

            Pt.Potrace.Clear();

            Pt.Potrace.turnpolicy = (Pt.TurnPolicy)mode;

            Pt.Potrace.curveoptimizing = optimize;

            Pt.Potrace.opttolerance = tolerance;
            Pt.Potrace.Treshold = threshold;
            Pt.Potrace.alphamax = alpha * 1.3334;

            Pt.Potrace.turdsize = size;

            input = input.ToAccordBitmap(Filter.ImageTypes.Rgb24bpp);

            Pt.Potrace.Potrace_Trace(input, crvs);

            foreach (var crvList in crvs)
            {

                Rg.Polyline polyline = new Rg.Polyline();
                polyline.Add(crvList[0].A.ToRhPoint(height));
                foreach (Pt.Curve curve in crvList)
                {
                    Rg.Point3d a = curve.ControlPointA.ToRhPoint(height);
                    Rg.Point3d b = curve.ControlPointB.ToRhPoint(height);
                    Rg.Point3d c = curve.B.ToRhPoint(height);

                    if (a != polyline[polyline.Count - 1]) polyline.Add(a);
                    if (b != a) polyline.Add(b);
                    if (c != b) polyline.Add(c);
                }
                if (!polyline.IsClosed) polyline.Add(crvList[0].A.ToRhPoint(height));
                polylines.Add(polyline);
            }

            return polylines;
        }

        public static Sw.Point ToPoint(this Pt.dPoint input)
        {
            return new Sw.Point(input.x, input.y);
        }

        public static Rg.Point3d ToRhPoint(this Pt.dPoint input, double height)
        {
            return new Rg.Point3d(input.x, height - input.y, 0);
        }

        #endregion

    }
}
