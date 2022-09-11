namespace RevitLookupWpf.Unit
{
    public class AreaUnitItem : UnitItem<double>
    {
        public AreaUnitItem(double value) : base(value)
        {
        }

        public override UnitType UnitType => UnitType.Area;

        public override double GetConveterValue()
        {
            return UnitConveter.ConvertValue(Value, UnitType, UnitSystem);
        }
    }
}
