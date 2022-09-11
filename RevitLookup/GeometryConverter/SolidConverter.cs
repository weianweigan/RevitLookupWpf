using Autodesk.Revit.DB;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;

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
                face.AddToMeshBuilder(meshBuilder);
            }

            return meshBuilder;
        }        
    }
}
