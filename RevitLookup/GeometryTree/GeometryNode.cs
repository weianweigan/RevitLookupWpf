using Autodesk.Revit.DB;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace RevitLookupWpf.GeometryTree
{
    public abstract class GeometryNode
    {
        public string Name { get; set; }

        public ObservableCollection<GeometryNode> Children { get; set; } = new ObservableCollection<GeometryNode>();

        /// <summary>
        /// Make sure you called <see cref="LoadModel3D"/>
        /// </summary>
        public Visual3D Visual3D { get; protected set; }

        public static GeometryNode CreateByGeometryObject(GeometryObject geometryObject)
        {
            var node = default(GeometryNode);
            switch (geometryObject)
            {
                case Solid:
                    node = new SolidGeometryNode(geometryObject as Solid);
                    break;
                case Curve:
                    node = new CurveGeometryNode(geometryObject as Curve);
                    break;
                case Edge:
                    node = new EdgeGemetryNode(geometryObject as Edge);
                    break;
                case Face:
                    node = new FaceGeometryNode(geometryObject as Face);
                    break;
                case GeometryElement:
                    node = new GeometryElementGeometryNode(geometryObject as GeometryElement);
                    break;
                case GeometryInstance:
                    node = new GeometryInstanceGeometryNode(geometryObject as GeometryInstance);
                    break;
                case Mesh:
                    node = new MeshGeometryNode(geometryObject as Mesh);
                    break;
                case Point:
                    node = new PointGeometryNode(geometryObject as Point);
                    break;
                case PolyLine:
                    node = new PolyLineGeometryNode(geometryObject as PolyLine);
                    break;
                case Profile:
                    node = new ProfileGeometryNode(geometryObject as Profile);
                    break;
                default:
                    break;
            }
            return node;
        }

        public static GeometryNode CreateXYZ(XYZ value)
        {
            return new XYZGeometryNode(value);
        }

        internal static GeometryNode CreateBoundingBoxXYZ(BoundingBoxXYZ rvtObject)
        {
            return new BoundingBoxXYZGeometryNode(rvtObject);
        }

        public abstract Visual3D LoadModel3D();
    }

    public abstract class GeometryNode<TRvtGeometryObject>:GeometryNode
        where TRvtGeometryObject : class
    {
        public GeometryNode(TRvtGeometryObject rvtGeometryObject)
        {
            RvtGeometryObject = rvtGeometryObject;
            Name = typeof(TRvtGeometryObject).Name;
        }

        public TRvtGeometryObject RvtGeometryObject { get; set; }
    }
}
