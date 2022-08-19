using Autodesk.Revit.DB;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryConverter
{
    public static class PointConverter
    {
        public static void AddToVisual(this XYZ point,Visual3D visual3D)
        {
            PointGeometryBuilder ptBuilder = new PointGeometryBuilder(visual3D);
            ptBuilder.CreatePositions(new List<Point3D>() { point.ToPoint3D() });
        }

        public static Point3D ToPoint3D(this XYZ point)
        {
            return new Point3D(point.X, point.Y, point.Z);
        }

        public static Size3D ToSize3D(this XYZ point)
        {
            return new Size3D(point.X, point.Y, point.Z);
        }
    }
}
