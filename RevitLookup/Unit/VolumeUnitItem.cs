namespace RevitLookupWpf.Unit
{
    public class VolumeUnitItem : UnitItem<double>
    {
        public VolumeUnitItem(double value) : base(value)
        {
        }

        public override UnitType UnitType => UnitType.Volume;

        public override double GetConveterValue()
        {
            return UnitConveter.ConvertValue(Value, UnitType, UnitSystem);
        }
    }
}
