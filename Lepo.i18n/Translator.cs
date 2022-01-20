// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Collections.Generic;

namespace Lepo.i18n
{
    // TODO: Refactor
    public class Translator
    {
        private string _path = "";

        private string _language = "";

        private string _default = "en_US";

        private Dictionary<string, Dictionary<uint, string>> _translations = new();

        public string Language => _language;

        public string DefaultLanguage => _default;

        public static Translator Instance { get; set; } = null;

        /// <summary>
        /// Creates new instance of <see cref="Translator"/> and sets it's <see cref="Instance"/>.
        /// </summary>
        public Translator()
        {
            Instance = this;
        }

        /// <summary>
        /// Defines default language.
        /// </summary>
        /// <param name="defaultLanguage">ISO language code <para><c>eg. en_US</c>.</para></param>
        public void SetDefault(string defaultLanguage)
        {
            _default = defaultLanguage;
        }

        /// <summary>
        /// Defines current language.
        /// </summary>
        /// <param name="language">ISO language code <para><c>eg. en_US</c>.</para></param>
        public void SetLanguage(string language)
        {
            _language = language;
        }

        /// <summary>
        /// Defines path in which the strings are located.
        /// </summary>
        /// <param name="pathToStrings"></param>
        public void SetStringsPath(string pathToStrings)
        {
            if (!pathToStrings.EndsWith("/"))
                pathToStrings += "/";

            _path = pathToStrings;
        }

        /// <summary>
        /// Load language from string containing YAML.
        /// </summary>
        /// <param name="language"></param>
        public void LoadLanguage(string language)
        {
            _language = language;

            if (!_translations.ContainsKey(language))
            {
                _translations.Add(language, Yaml.FromPath(_path + language + ".yaml"));
            }
        }

        /// <summary>
        /// Load language from path to file containing YAML.
        /// </summary>
        /// <param name="language"></param>
        /// <param name="yamlFileContent"></param>
        public void LoadFromFile(string language, string yamlFileContent)
        {
            _language = language;

            if (!_translations.ContainsKey(language))
            {
                _translations.Add(language, Yaml.FromString(yamlFileContent));
            }
        }

        /// <summary>
        /// Translates <see cref="string"/>.
        /// </summary>
        /// <param name="text">Text to be translated.</param>
        /// <param name="replace">Replaces <c>%s</c> with provided value.</param>
        /// <returns></returns>
        public static string String(string? text, string? replace = null)
        {
            if (string.IsNullOrEmpty(text))
                return "i18n.error.null";

            // Needed for XAML preview
            if (Instance == null)
                return text;

            if (System.String.IsNullOrEmpty(replace))
                return Instance.FirstOrDefault(text);

            return Instance.FirstOrDefault(text).Replace("%s", replace);
        }

        internal string FirstOrDefault(string key)
        {
            key = key.Trim();

            uint mappedKey = Yaml.Map(key);

            if (!_translations.ContainsKey(_language))
            {
#if DEBUG
                return "i18n.(" + key + ")";
#else
                return key;
#endif
            }


            if (!_translations[_language].ContainsKey(mappedKey))
            {
#if DEBUG
                return "i18n.(" + key + ")";
#else
                return key;
#endif
            }

            return _translations[_language][mappedKey];
        }
    }
}
