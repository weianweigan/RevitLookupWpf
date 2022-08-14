using Autodesk.Revit.DB;
using HelixToolkit.Wpf.SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.GeometryConverter
{
    public static class PointConverter
    {
        public static PointGeometryModel3D ToGeometry3D(this XYZ point)
        {
            var pt = new PointGeometryModel3D()
            {

            };
            return pt;
        }

        public static SharpDX.Vector3 ToVector3(this XYZ point)
        {
            return new SharpDX.Vector3((float)point.X, (float)point.Y, (float)point.Z);
        }
    }

    public static class LineConveter
    {
        public static LineGeometry3D ToGeometry3D(this Line line)
        {
            var lineBuilder = new LineBuilder();
            lineBuilder.AddLine(line.GetEndPoint(0).ToVector3(), line.GetEndPoint(1).ToVector3());
            return lineBuilder.ToLineGeometry3D();
        }
    }
}
