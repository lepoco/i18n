// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Lepo.i18n
{
    /// <summary>
    /// Implements application translation using Yaml files.
    /// </summary>
    public static class Translator
    {
        /// <summary>
        /// Returns the currently used language.
        /// </summary>
        public static string Current => TranslationStorage.CurrentLanguage ?? "";

        /// <summary>
        /// Defines the currently used language of the application. By itself, it does not update rendered views.
        /// </summary>
        /// <param name="applicationAssembly">Main application <see cref="Assembly"/>. You can use <see cref="Assembly.GetExecutingAssembly"/></param>
        /// <param name="language">The language code to which you would like to assign the selected file. i.e: <i>pl_PL</i></param>
        /// <param name="embeddedResourcePath">Path to the YAML file, i.e: <i>MyApp.Assets.Strings.pl_PL.yaml</i></param>
        /// <param name="reload">If the file was previously loaded, it will be reloaded.</param>
        public static bool SetLanguage(Assembly applicationAssembly, string language, string embeddedResourcePath, bool reload = false)
        {
            language = language.Trim();

            // We update the language even if we do not change it, because the user may not know what is not working.
            TranslationStorage.CurrentLanguage = language;

            var languageDictionary = AssemblyLoader.TryLoad(applicationAssembly, embeddedResourcePath);

            if (languageDictionary == null)
                return false;

            TranslationStorage.TranslationsDictionary ??= new Dictionary<string, IDictionary<uint, string>>();

            if (!TranslationStorage.TranslationsDictionary.ContainsKey(language))
            {
                TranslationStorage.TranslationsDictionary.Add(language, languageDictionary);

                return true;
            }

            if (TranslationStorage.TranslationsDictionary.ContainsKey(language) && reload)
                TranslationStorage.TranslationsDictionary[language] = languageDictionary;

            return true;
        }

        /// <summary>
        /// Defines the currently used language of the application. By itself, it does not update rendered views.
        /// </summary>
        /// <param name="applicationAssembly">Main application <see cref="Assembly"/>. You can use <see cref="Assembly.GetExecutingAssembly"/></param>
        /// <param name="language">The language code to which you would like to assign the selected file. i.e: <i>pl_PL</i></param>
        /// <param name="embeddedResourcePath">Path to the YAML file, i.e: <i>MyApp.Assets.Strings.pl_PL.yaml</i></param>
        /// <param name="reload">If the file was previously loaded, it will be reloaded.</param>
        public static async Task<bool> SetLanguageAsync(Assembly applicationAssembly, string language, string embeddedResourcePath, bool reload = false)
        {
            return await Task.Run<bool>(() => SetLanguage(applicationAssembly, language, embeddedResourcePath, reload));
        }

        /// <summary>
        /// Translates <see cref="string"/>.
        /// </summary>
        /// <param name="text">Text to be translated.</param>
        /// <param name="replace">Replaces <c>%s</c> with provided value.</param>
        /// <returns></returns>
        public static string String(string text, string replace = null)
        {
            if (string.IsNullOrEmpty(text))
                return "i18n.error.nullInput";

            if (System.String.IsNullOrEmpty(replace))
                return Translator.FirstOrDefault(text);

            return Translator.FirstOrDefault(text).Replace("%s", replace);
        }

        internal static string FirstOrDefault(string key)
        {
            if (TranslationStorage.TranslationsDictionary == null)
                return "i18n.error.dictionaryNull";

            if (TranslationStorage.CurrentLanguage == null)
                return "i18n.error.languageNull";

            key = key.Trim();

            uint mappedKey = Yaml.Map(key);

            if (!TranslationStorage.TranslationsDictionary.ContainsKey(TranslationStorage.CurrentLanguage))
            {
#if DEBUG
                return "i18n.(" + key + ")";
#else
                return key;
#endif
            }


            if (!TranslationStorage.TranslationsDictionary[TranslationStorage.CurrentLanguage].ContainsKey(mappedKey))
            {
#if DEBUG
                return "i18n.(" + key + ")";
#else
                return key;
#endif
            }

            return TranslationStorage.TranslationsDictionary[TranslationStorage.CurrentLanguage][mappedKey];
        }
    }
}