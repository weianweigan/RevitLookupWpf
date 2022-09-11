namespace RevitLookupWpf.Unit
{
    public class NormalUnitItem : UnitItem<double>
    {
        public NormalUnitItem(double value) : base(value)
        {
        }

        public override UnitType UnitType => UnitType.Normal;

        public override double GetConveterValue()
        {
            return UnitConveter.ConvertValue(Value,UnitType,UnitSystem);
        }
        
    }
}
