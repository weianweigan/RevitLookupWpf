using Autodesk.Revit.DB;

namespace RevitLookupWpf.Unit
{
    public abstract class UnitItem
    {
        public abstract UnitType UnitType { get;}

        public static UnitSystem UnitSystem { get; set; }

        public string ShortName => UnitShortNameConverter.GetShortName(UnitType, UnitSystem);
    
        public static UnitItem CreateByValue(double value,UnitType unitType)
        {
            var unitItem = default(UnitItem);
            switch (unitType)
            {
                case UnitType.Normal:
                    unitItem = new NormalUnitItem(value);
                    break;
                case UnitType.Area:
                    unitItem = new AreaUnitItem(value);
                    break;
                case UnitType.Volume:
                    unitItem = new VolumeUnitItem(value);
                    break;
                default:
                    break;
            }
            return unitItem;
        }

        public static UnitItem CreateByXYZ(XYZ value)
        {
            return new XYZUnitItem(value);
        }
    }

    public abstract class UnitItem<TValue>:UnitItem
    {
        public UnitItem(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; set; }

        public abstract TValue GetConveterValue();

        public override string ToString()
        {
            return $"{GetConveterValue()} {ShortName}";
        }
    }
}
