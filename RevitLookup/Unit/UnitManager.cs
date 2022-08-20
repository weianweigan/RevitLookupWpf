using Autodesk.Revit.DB;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.Unit
{
    public static class UnitConveter
    {
        public static double ConvertValue(double value,UnitType unitType,UnitSystem unitSystem)
        {
            switch (unitType)
            {
                case UnitType.Normal:
                    value = ConvertValue(value,unitSystem);
                    break;
                case UnitType.Area:
                    value = ConvertArea(value,unitSystem);
                    break;
                case UnitType.Volume:
                    value = ConvertVolume(value, unitSystem);
                    break;
                default:
                    break;
            }
            return value;
        }

        public static double ConvertValue(double value,UnitSystem unitSystem)
        {
            switch (unitSystem)
            {
                case UnitSystem.MKS:
                    value = UnitUtils.ConvertFromInternalUnits(value,
#if R19 || R20 
                        DisplayUnitType.DUT_METERS                                           
#else
                        UnitTypeId.Meters
#endif
                        );
                    break;
                case UnitSystem.CGS:
                    value = UnitUtils.ConvertFromInternalUnits(value,
#if R19 || R20
    DisplayUnitType.DUT_CENTIMETERS
#else
                        UnitTypeId.Centimeters
#endif
    );
                    break;
                case UnitSystem.MMGS:
                    value = UnitUtils.ConvertFromInternalUnits(value,
#if R19 || R20
    DisplayUnitType.DUT_CUBIC_MILLIMETERS
#else
                        UnitTypeId.Millimeters
#endif
    );
                    break;
                case UnitSystem.IPS:
                    //Default Unit 
                    break;
                case UnitSystem.Custom:
                    throw new NotImplementedException();
                default:
                    break;
            }
            return value;
        }

        public static double ConvertArea(double value, UnitSystem unitSystem)
        {
            switch (unitSystem)
            {
                case UnitSystem.MKS:
                    value = UnitUtils.ConvertFromInternalUnits(value,
#if R19 || R20 
                        DisplayUnitType.DUT_SQUARE_METERS                                           
#else
                        UnitTypeId.SquareMeters
#endif
                        );
                    break;
                case UnitSystem.CGS:
                    value = UnitUtils.ConvertFromInternalUnits(value,
#if R19 || R20
                        DisplayUnitType.DUT_SQUARE_CENTIMETERS
#else
                        UnitTypeId.SquareCentimeters
#endif
    );
                    break;
                case UnitSystem.MMGS:
                    value = UnitUtils.ConvertFromInternalUnits(value,
#if R19 || R20
                        DisplayUnitType.DUT_SQUARE_MILLIMETERS
#else
                        UnitTypeId.SquareCentimeters
#endif
    );
                    break;
                case UnitSystem.IPS:
                    //Default Unit 
                    break;
                case UnitSystem.Custom:
                    throw new NotImplementedException();
                default:
                    break;
            }
            return value;
        }

        public static double ConvertVolume(double value, UnitSystem unitSystem)
        {
            switch (unitSystem)
            {
                case UnitSystem.MKS:
                    value = UnitUtils.ConvertFromInternalUnits(value,
#if R19 || R20 
                        DisplayUnitType.DUT_CUBIC_METERS                                           
#else
                        UnitTypeId.CubicMeters
#endif
                        );
                    break;
                case UnitSystem.CGS:
                    value = UnitUtils.ConvertFromInternalUnits(value,
#if R19 || R20
                        DisplayUnitType.DUT_CUBIC_CENTIMETERS
#else
                        UnitTypeId.CubicCentimeters
#endif
    );
                    break;
                case UnitSystem.MMGS:
                    value = UnitUtils.ConvertFromInternalUnits(value,
#if R19 || R20
                        DisplayUnitType.DUT_CUBIC_MILLIMETERS
#else
                        UnitTypeId.CubicCentimeters
#endif
    );
                    break;
                case UnitSystem.IPS:
                    //Default Unit 
                    break;
                case UnitSystem.Custom:
                    throw new NotImplementedException();
                default:
                    break;
            }
            return value;
        }

        public static double ConvertFromInternalUnits(double value)
        {
#if R19 || R20
            return UnitUtils.ConvertFromInternalUnits(value, DisplayUnitType.DUT_MILLIMETERS);
#else
            return UnitUtils.ConvertFromInternalUnits(value, UnitTypeId.Millimeters);
#endif
        }

        public static double ConvertToMM(double value)
        {
#if R19 || R20
            return UnitUtils.ConvertFromInternalUnits(value, DisplayUnitType.DUT_MILLIMETERS);
#else
            return UnitUtils.ConvertFromInternalUnits(value, UnitTypeId.Millimeters);
#endif
        }

        public static XYZ ConvertToMM(this XYZ vector)
        {
            return new XYZ(ConvertToMM(vector.X), ConvertToMM(vector.Y), ConvertToMM(vector.Z));
        }
    }
}
