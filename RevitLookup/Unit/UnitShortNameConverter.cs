namespace RevitLookupWpf.Unit
{
    public static class UnitShortNameConverter
    {
        public struct UnitKeys
        {
            public UnitSystem UnitSystem;

            public UnitType UnitType;

            public UnitKeys(UnitSystem unitSystem, UnitType unitType)
            {
                this.UnitSystem = unitSystem;
                this.UnitType = unitType;
            }
        }

        public static Dictionary<UnitKeys, string> UnitShortNameDictionary = new Dictionary<UnitKeys, string>()
        {
            {new UnitKeys(UnitSystem.MKS,UnitType.Normal),"m" },
            {new UnitKeys(UnitSystem.MKS,UnitType.Area),"m²" },
            {new UnitKeys(UnitSystem.MKS,UnitType.Volume),"m³" },

            {new UnitKeys(UnitSystem.CGS,UnitType.Normal),"cm" },
            {new UnitKeys(UnitSystem.CGS,UnitType.Area),"cm²" },
            {new UnitKeys(UnitSystem.CGS,UnitType.Volume),"cm³" },

            {new UnitKeys(UnitSystem.MMGS,UnitType.Normal),"mm" },
            {new UnitKeys(UnitSystem.MMGS,UnitType.Area),"mm²" },
            {new UnitKeys(UnitSystem.MMGS,UnitType.Volume),"mm³" },

            {new UnitKeys(UnitSystem.IPS,UnitType.Normal),"in" },
            {new UnitKeys(UnitSystem.IPS,UnitType.Area),"sq.in" },
            {new UnitKeys(UnitSystem.IPS,UnitType.Volume),"cu.in" },
        };

        public static string GetShortName(UnitType unitType, UnitSystem unitSystem)
        {
            if (UnitShortNameDictionary.TryGetValue(
                new UnitKeys(unitSystem,unitType),
                out var value))
            {
                return value;
            }

            return string.Empty;
        }
    }
}
