using Autodesk.Revit.DB;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryConverter
{
    public static class CurveConverter
    {
        public const double DISTANCE = 0.01d;

        public static LinesVisual3D ToCurvesVisual3D(this CurveArray curveArr)
        {
            var curves = curveArr.OfType<Curve>();

            return ToCurvesVisual3D(curves);
        }

        public static LinesVisual3D ToCurvesVisual3D(this EdgeArray edgeArr)
        {
            var curves = edgeArr
                .OfType<Edge>()
                .Select(p => p.AsCurve());

            return ToCurvesVisual3D(curves);
        }

        private static LinesVisual3D ToCurvesVisual3D(IEnumerable<Curve> curves)
        {
            var lines = new LinesVisual3D();
            foreach (Curve curve in curves)
            {
                if (curve is Line)
                {
                    lines.Points.Add(curve.GetEndPoint(0).ToPoint3D());
                    lines.Points.Add(curve.GetEndPoint(1).ToPoint3D());
                }
                else
                {
                    var count = Convert.ToInt32(curve.ApproximateLength / DISTANCE);
                    var point3ds = new List<Point3D>(count);

                    double step = 1d / count;
                    for (int i = 0; i < count; i++)
                    {
                        double parameter = step * i;
                        var pt = curve.Evaluate(parameter, true);
                        point3ds.Add(pt.ToPoint3D());
                    }

                    lines.Points.Add(point3ds[0]);
                    for (int i = 1; i < point3ds.Count - 1; i++)
                    {
                        lines.Points.Add(point3ds[i]);
                        lines.Points.Add(point3ds[i]);
                    }
                    lines.Points.Add(point3ds[point3ds.Count - 1]);
                }
            }

            return lines;
        }

        public static Visual3D ToCurveVisual3D(this Curve curve)
        {
            var lines = new LinesVisual3D();
            if (curve is Line)
            {
                lines.Points.Add(curve.GetEndPoint(0).ToPoint3D());
                lines.Points.Add(curve.GetEndPoint(1).ToPoint3D());
            }
            else
            {
                var count = Convert.ToInt32(curve.ApproximateLength / DISTANCE);
                var point3ds = new List<Point3D>(count);

                double step = 1d / count;
                for (int i = 0; i < count; i++)
                {
                    double parameter = step * i;
                    var pt = curve.Evaluate(parameter, true);
                    point3ds.Add(pt.ToPoint3D());
                }

                lines.Points.Add(point3ds[0]);
                for (int i = 1; i < point3ds.Count-1; i++)
                {
                    lines.Points.Add(point3ds[i]);
                    lines.Points.Add(point3ds[i]);
                }
                lines.Points.Add(point3ds[point3ds.Count-1]);
            }
           
            return lines;
        }
    }
}
