using Autodesk.Revit.DB;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryConverter
{
    public static class PointConverter
    {
        //public static PointGeometryModel3D ToGeometry3D(this XYZ point)
        //{
        //    var pt = new PointGeometryModel3D()
        //    {

        //    };
        //    return pt;
        //}

        public static Point3D ToVector3(this XYZ point)
        {
            return new Point3D((float)point.X, (float)point.Y, (float)point.Z);
        }
    }

    public static class LineConveter
    {
        //public static LineGeometry3D ToGeometry3D(this Line line)
        //{
        //    var lineBuilder = new LineBuilder();
        //    lineBuilder.AddLine(line.GetEndPoint(0).ToVector3(), line.GetEndPoint(1).ToVector3());
        //    return lineBuilder.ToLineGeometry3D();
        //}
    }
}
