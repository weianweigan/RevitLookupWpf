using Autodesk.Revit.DB;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;

namespace RevitLookupWpf.GeometryConverter
{
    public static class SolidConverter
    {
        public static MeshGeometry3D ToGeometry3D(this Solid solid)
        {
            return solid.ToMeshBuilder().ToMesh();
        }

        public static MeshBuilder ToMeshBuilder(this Solid solid)
        {
            return solid.AddToMeshBuilder(new MeshBuilder());
        }

        public static MeshBuilder AddToMeshBuilder(this Solid solid,MeshBuilder meshBuilder)
        {
            foreach (Face face in solid.Faces)
            {
                Mesh mesh = face.Triangulate();
                var triangleCorners = new Vector3[3];

                for (int i = 0; i < mesh.NumTriangles; i++)
                {
                    MeshTriangle triangle = mesh.get_Triangle(i);
                    triangleCorners[0] = triangle.get_Vertex(0).ToVector3();
                    triangleCorners[1] = triangle.get_Vertex(0).ToVector3();
                    triangleCorners[2] = triangle.get_Vertex(0).ToVector3();

                    meshBuilder.AddPolygon(triangleCorners);
                }
            }

            return meshBuilder;
        }        
    }
}
