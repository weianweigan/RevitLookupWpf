using Newtonsoft.Json;

namespace RevitLookupWpf
{
    public class RevitInfo
    {
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Keywords")]
        public string Keywords { get; set; }
        [JsonProperty("APIName")]
        public string APIName { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("NameSpace")]
        public string NameSpace { get; set; }
        [JsonProperty("Guid")]
        public string Guid { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }

        public string GetUrl(string revitVersion)
        {
            if (string.IsNullOrEmpty(revitVersion)) revitVersion = "2022";

            string linkQuery = $"https://www.revitapidocs.com/{revitVersion}/{Guid}.htm";

            return linkQuery;
        }
    }
}
