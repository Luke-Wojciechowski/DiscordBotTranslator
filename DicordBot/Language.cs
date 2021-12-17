using System;
using System.Collections.Generic;
using System.Linq;

namespace DicordBot
{
    public static class Language
    {
        private static Dictionary<ELanguage, string> _languageToToken = new()
        {
            {ELanguage.English, "en"},
            {ELanguage.Polish, "pl"},
            {ELanguage.Russian, "ru"}
        };

        public static string[] LanguageTokens => _languageToToken.Select(pair => pair.Value).ToArray();
        
        public static ELanguage GetLanguageByToken(string languageToken)
        {
            if (!_languageToToken.ContainsValue(languageToken))
            {
                Console.Error.WriteLine($"There is no such language token defined as \"{languageToken}\"");

                return ELanguage.English;
            }

            return _languageToToken.First(pair => pair.Value == languageToken).Key;
        }
    }

    public enum ELanguage
    {
        English,
        Polish,
        Russian
    }
}