using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace RevitLookupWpf.Helpers
{
    public static class RevitInfoManager
    {
        private static List<RevitInfo> deserializeObject;

        public static RevitVersion Version { get; internal set; }

        public static void Init()
        {
            if (deserializeObject != null)
                return;

            LoadFormResource();
            //LoadFromDirectory();
        }

        private static void LoadFormResource()
        {
            using (var s = Application.GetResourceStream(new Uri("pack://application:,,,/RevitLookupWpf;component/Resources/RevitAPI2022.json")).Stream)
            {
                using (StreamReader r = new StreamReader(s))
                {
                    string json = r.ReadToEnd();
                    deserializeObject = JsonConvert.DeserializeObject<List<RevitInfo>>(json);
                }
            }
        }

        private static void LoadFromDirectory()
        {
            var directory = Path.GetDirectoryName(typeof(RvtAddinBase).Assembly.Location);

            string filepath = @"D:\weigan\repos\RevitTools\RevitLookupWpf\RevitLookup\RevitAPI2022.json";// @$"{directory}\RevitAPI2022.json";

            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException($"{filepath} Not Found");
            }

            using (StreamReader r = new StreamReader(filepath))
            {
                string json = r.ReadToEnd();
                deserializeObject = JsonConvert.DeserializeObject<List<RevitInfo>>(json);
            }
        }

        public static RevitInfo Find(string apiName)
        {
            Init();
            return deserializeObject
                .FirstOrDefault(x => x.APIName == apiName);
        }

        public static string FindLink(string apiName)
        {
            Init();
            var revitInfo = Find(apiName);
            if (revitInfo != null)
            {
                return null;
            }
            else
            {
                return revitInfo.GetUrl(Version.ToString());
            }
        }
    }
}
