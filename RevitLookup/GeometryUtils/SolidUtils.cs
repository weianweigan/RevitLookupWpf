using Autodesk.Revit.DB;
using HelixToolkit.Wpf.SharpDX;

namespace RevitLookupWpf.GeometryUtils
{
    public static class SolidUtils
    {
        public static Geometry3D ToGeometry3D(this Solid solid)
        {
            var meshBuilder = new MeshBuilder();

            //TODO: convert revit solid to mesh


            return meshBuilder.ToMesh();
        }
    }
}
