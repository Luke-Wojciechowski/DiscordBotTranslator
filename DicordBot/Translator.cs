using System.Net.Http;
using System.Threading.Tasks;

namespace DicordBot
{
    class Translator
    {
        public async Task<string> Trans(string s)
        {
            s = s.Replace(" ", "%20");
            var client = new HttpClient();
            var data = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=ru&tl=en&dt=t&q=\"{s}\"";
            HttpResponseMessage res = await client.GetAsync(data);
            var result = await res.Content.ReadAsStringAsync();
            result =  result.Split(",")[0];
            result = result.Remove(result.Length - 1).Remove(0, 4);
            return result;
        }
    }
}
