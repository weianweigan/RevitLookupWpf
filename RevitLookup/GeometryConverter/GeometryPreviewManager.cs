using Autodesk.Revit.DB;
using RevitLookupWpf.PropertySys;
using RevitLookupWpf.PropertySys.BaseProperty;
using RevitLookupWpf.PropertySys.BaseProperty.ReferenceType;
using RevitLookupWpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.GeometryConverter
{
    public static class GeometryPreviewManager
    {
        public static PreviewWindow PreviewWindow { get; private set; }

        internal static void Preview(PropertyBase selectedProperty)
        {
            if (PreviewWindow == null)
            {
                PreviewWindow = new PreviewWindow();
                PreviewWindow.Closed += PreviewWindow_Closed;
            }

            if (selectedProperty is DefaultObjectProperty defaultObject)
            {
                //Solid Line Curve
                if (defaultObject.Value is Solid solid)
                {
                    PreviewWindow.AddSolid(solid);
                }
            }else if (selectedProperty is XYZProperty)
            {

            }

            PreviewWindow.Show();
            PreviewWindow.ThreeDView.ZoomExtents();
        }

        internal static void Preview(Solid solid)
        {
            if (PreviewWindow == null)
            {
                PreviewWindow = new PreviewWindow();
                PreviewWindow.Closed += PreviewWindow_Closed;
            }

            PreviewWindow.AddSolid(solid);
            PreviewWindow.Show();
            PreviewWindow.ThreeDView.ZoomExtents();
        }

        private static void PreviewWindow_Closed(object sender, EventArgs e)
        {
            PreviewWindow.Closed -= PreviewWindow_Closed;
            PreviewWindow = null;
        }
    }
}
