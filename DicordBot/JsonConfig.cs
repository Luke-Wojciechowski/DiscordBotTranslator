using Newtonsoft.Json;

namespace DicordBot
{
    public struct JsonConfig
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
    }
}
