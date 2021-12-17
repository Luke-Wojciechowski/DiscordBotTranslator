using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DicordBot
{
    internal class Translator
    {
        private record GoogleResponseContainer
        {
            public string TranslatedText;
            public string OriginalText;
            public ELanguage TranslationLanguage;
            public ELanguage Original;
        }

        private static GoogleResponseContainer ParseGoogleTranslatorResponse(string googleTranslatorResponse, ELanguage translationLanguage)
        {
            //Removes extra data
            googleTranslatorResponse = Regex
                .Replace(googleTranslatorResponse, "\\[|\\]|null|\\d*", "")
                .Replace(".", "")
                .Trim();

            //Removes extra quotation marks("), commas(,) and slashes(\) 
            googleTranslatorResponse = Regex
                .Replace(googleTranslatorResponse, ",+|,$", ",")
                .Replace("\"", "")
                .Replace("\\", " ")
                .Trim();

            //Takes first 3 elements: translated text, original text, original text language token
            //Example: word, слово, ru
            var parts = googleTranslatorResponse.Split(",")[..3];

            return new GoogleResponseContainer
            {
                TranslatedText = parts[0],
                OriginalText = parts[1],
                TranslationLanguage = translationLanguage,
                Original = Language.GetLanguageByToken(parts[2])
            };
        }

        public static async Task<Dictionary<ELanguage, string>> Translate(string textToToTranslate)
        {
            textToToTranslate = textToToTranslate.Replace(" ", "%20");

            var googleResponseContainers = new Dictionary<ELanguage, string>();
            
            foreach (var languageToken in Language.LanguageTokens)
            {
                var client = new HttpClient();
                var data = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl={languageToken}&dt=t&q=\"{textToToTranslate}\"";
                var res = await client.GetAsync(data);
                var result = await res.Content.ReadAsStringAsync();

                var parsedGoogleResponse = ParseGoogleTranslatorResponse(result, Language.GetLanguageByToken(languageToken));
                googleResponseContainers.Add(parsedGoogleResponse.TranslationLanguage, parsedGoogleResponse.TranslatedText);
            }
            
            
            return googleResponseContainers;
        }
    }
}
