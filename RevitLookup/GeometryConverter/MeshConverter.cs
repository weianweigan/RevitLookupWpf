using Autodesk.Revit.DB;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryConverter
{
    public static class MeshConverter
    {
        public static MeshGeometry3D ToGeometry3D(this Mesh mesh)
        {
            return mesh.ToMeshBuilder().ToMesh();
        }

        public static MeshBuilder ToMeshBuilder(this Mesh mesh)
        {
            return mesh.AddToMeshBuilder(new MeshBuilder());
        }

        public static MeshBuilder AddToMeshBuilder(
            this Mesh mesh,
            MeshBuilder meshBuilder)
        {
            var triangleCorners = new Point3D[3];

            for (int i = 0; i < mesh.NumTriangles; i++)
            {
                MeshTriangle triangle = mesh.get_Triangle(i);
                triangleCorners[0] = triangle.get_Vertex(0).ToPoint3D();
                triangleCorners[1] = triangle.get_Vertex(1).ToPoint3D();
                triangleCorners[2] = triangle.get_Vertex(2).ToPoint3D();

                meshBuilder.AddPolygon(triangleCorners);
            }

            return meshBuilder;
        }
    }
}
