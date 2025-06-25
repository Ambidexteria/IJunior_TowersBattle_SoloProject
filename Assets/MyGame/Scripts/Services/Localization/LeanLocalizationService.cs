using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Services.Localization
{
    public class LeanLocalizationService : ILocalizationService
    {
        private const string DefaultCode = "en";

        private Dictionary<string, string> _localizationCodes;

        public LeanLocalizationService()
        {
            _localizationCodes = new Dictionary<string, string>
            {
                { "ru", "Russian" },
                { "en", "English" },
                { "tr", "Turkish" }
            };
        }

        public void SetLanguage(string languageCode)
        {
            if(_localizationCodes.ContainsKey(languageCode))
            {
                LeanLocalization.SetCurrentLanguageAll(_localizationCodes[languageCode]);
            }
            else
            {
                Debug.LogWarning($"{nameof(LeanLocalizationService)}: language code \"{languageCode}\" not found. Default language set");
                LeanLocalization.SetCurrentLanguageAll(_localizationCodes[DefaultCode]);
            }
        }
    }
}
