using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf
{
    public class RevitVersion
    {
        public RevitVersion(string versionName, string versionNumber, string versionBuild, string subVersionNumber)
        {
            VersionName = versionName;
            VersionNumber = versionNumber;
            VersionBuild = versionBuild;
            SubVersionNumber = subVersionNumber;
        }

        public string VersionName { get; }

        public string VersionNumber { get; }

        public string VersionBuild { get; }

        public string SubVersionNumber { get; }

        public override string ToString()
        {
            return VersionName;
        }
    }
}
