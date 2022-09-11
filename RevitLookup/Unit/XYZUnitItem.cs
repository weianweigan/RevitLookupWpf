using Autodesk.Revit.DB;

namespace RevitLookupWpf.Unit
{
    public class XYZUnitItem : UnitItem<XYZ>
    {
        public XYZUnitItem(XYZ value) : base(value)
        {
        }

        public override UnitType UnitType => UnitType.Normal;

        public override XYZ GetConveterValue()
        {
            return new XYZ(
                UnitConveter.ConvertValue(Value.X, UnitSystem),
                UnitConveter.ConvertValue(Value.Y, UnitSystem),
                UnitConveter.ConvertValue(Value.Z, UnitSystem)
                );
        }
    }
}
