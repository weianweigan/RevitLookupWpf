using Autodesk.Revit.DB;
using HelixToolkit.Wpf;

namespace RevitLookupWpf.GeometryConverter
{
    public static class LinesConveter
    {
        public static LinesVisual3D ToGeometry3D(this PolyLine polyLine)
        {
            var lines = new LinesVisual3D();

            for (int i = 0; i < polyLine.NumberOfCoordinates; i++)
            {
                lines.Points.Add(polyLine.GetCoordinate(i).ToPoint3D());
            }

            return lines;
        }
    }
}
