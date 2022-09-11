using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.Unit
{
    public enum UnitSystem
    {
        /// <summary>
        /// Meter,Kilogram,Second
        /// </summary>
        MKS,

        /// <summary>
        /// Centimeter,Gram,Second
        /// </summary>
        CGS,

        /// <summary>
        /// Millimeter,Gram,Second
        /// </summary>
        MMGS,

        /// <summary>
        /// Inch,Pound,Second
        /// </summary>
        IPS,

        //TODO:
        Custom,
    }
}
