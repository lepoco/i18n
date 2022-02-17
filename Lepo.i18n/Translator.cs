// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Loads all specified languages into global memory.
        /// </summary>
        /// <param name="applicationAssembly">Main application <see cref="Assembly"/>. You can use <see cref="Assembly.GetExecutingAssembly"/></param>
        /// <param name="languagesCollection">A dictionary containing a key pair with the structure: <c>language_code</c> > <c>path_to_the_embedded_resource</c></param>
        /// <param name="reload">If the file was previously loaded, it will be reloaded.</param>
        public static bool LoadLanguages(Assembly applicationAssembly, IDictionary<string, string> languagesCollection, bool reload = false)
        {
            if (languagesCollection == null || !languagesCollection.Any())
                return false;

            foreach (KeyValuePair<string, string> singleLanguagePair in languagesCollection)
                LoadLanguage(applicationAssembly, singleLanguagePair.Key, singleLanguagePair.Value, reload);

            return true;
        }

        /// <summary>
        /// Asynchronously loads all specified languages into global memory.
        /// </summary>
        /// <param name="applicationAssembly">Main application <see cref="Assembly"/>. You can use <see cref="Assembly.GetExecutingAssembly"/></param>
        /// <param name="languagesCollection">A dictionary containing a key pair with the structure: <c>language_code</c> > <c>path_to_the_embedded_resource</c></param>
        /// <param name="reload">If the file was previously loaded, it will be reloaded.</param>
        public static async Task<bool> LoadLanguagesAsync(Assembly applicationAssembly, IDictionary<string, string> languagesCollection, bool reload = false)
        {
            return await Task.Run<bool>(() => LoadLanguages(applicationAssembly, languagesCollection, reload));
        }

        /// <summary>
        /// Loads the specified language into memory and allows you to use it globally.
        /// </summary>
        /// <param name="applicationAssembly">Main application <see cref="Assembly"/>. You can use <see cref="Assembly.GetExecutingAssembly"/></param>
        /// <param name="language">The language code to which you would like to assign the selected file. i.e: <i>pl_PL</i></param>
        /// <param name="embeddedResourcePath">Path to the YAML file, i.e: <i>MyApp.Assets.Strings.pl_PL.yaml</i></param>
        /// <param name="reload">If the file was previously loaded, it will be reloaded.</param>
        public static bool LoadLanguage(Assembly applicationAssembly, string language, string embeddedResourcePath, bool reload = false)
        {
            if (System.String.IsNullOrEmpty(language))
                throw new ArgumentNullException(nameof(language), "The name of the language must be specified.");

            if (System.String.IsNullOrEmpty(embeddedResourcePath))
                throw new ArgumentNullException(nameof(embeddedResourcePath), "The path to the location of the embedded resource in the application must be provided.");

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
        /// Asynchronously loads the specified language into memory and allows you to use it globally.
        /// </summary>
        /// <param name="applicationAssembly">Main application <see cref="Assembly"/>. You can use <see cref="Assembly.GetExecutingAssembly"/></param>
        /// <param name="language">The language code to which you would like to assign the selected file. i.e: <i>pl_PL</i></param>
        /// <param name="embeddedResourcePath">Path to the YAML file, i.e: <i>MyApp.Assets.Strings.pl_PL.yaml</i></param>
        /// <param name="reload">If the file was previously loaded, it will be reloaded.</param>
        public static async Task<bool> LoadLanguageAsync(Assembly applicationAssembly, string language, string embeddedResourcePath, bool reload = false)
        {
            return await Task.Run<bool>(() => LoadLanguage(applicationAssembly, language, embeddedResourcePath, reload));
        }

        /// <summary>
        /// Changes the currently used language, assuming that they have already been loaded using LoadLanguages.
        /// </summary>
        /// <param name="language">The language code to which you would like to assign the selected file. i.e: <i>pl_PL</i></param>
        /// <returns><see langword="false"/> if the language could not be defined from the dictionary. <para>ATTENTION, the <see cref="TranslationStorage.CurrentLanguage"/> will be updated whether or not the language has been changed.</para></returns>
        public static bool SetLanguage(string language)
        {
            // We update the language even if we do not change it, because the user may not know what is not working.
            TranslationStorage.CurrentLanguage = language;

            if (TranslationStorage.TranslationsDictionary == null)
                return false;

            if (!TranslationStorage.TranslationsDictionary.ContainsKey(language))
                return false;

            return true;
        }

        /// <summary>
        /// Asynchronously changes the currently used language, assuming that they have already been loaded using LoadLanguages.
        /// </summary>
        /// <param name="language">The language code to which you would like to assign the selected file. i.e: <i>pl_PL</i></param>
        /// <returns><see langword="false"/> if the language could not be defined from the dictionary. <para>ATTENTION, the <see cref="TranslationStorage.CurrentLanguage"/> will be updated whether or not the language has been changed.</para></returns>
        public static async Task<bool> SetLanguageAsync(string language)
        {
            return await Task.Run<bool>(() => SetLanguage(language));
        }

        /// <summary>
        /// Defines the currently used language of the application. By itself, it does not update rendered views.
        /// </summary>
        /// <param name="applicationAssembly">Main application <see cref="Assembly"/>. You can use <see cref="Assembly.GetExecutingAssembly"/></param>
        /// <param name="language">The language code to which you would like to assign the selected file. i.e: <i>pl_PL</i></param>
        /// <param name="embeddedResourcePath">Path to the YAML file, i.e: <i>MyApp.Assets.Strings.pl_PL.yaml</i></param>
        /// <param name="reload">If the file was previously loaded, it will be reloaded.</param>
        public static bool SetLanguage(Assembly applicationAssembly, string language, string embeddedResourcePath, bool reload = false)
        {
            LoadLanguage(applicationAssembly, language, embeddedResourcePath, reload);

            return SetLanguage(language);
        }

        /// <summary>
        /// Asynchronously defines the currently used language of the application. By itself, it does not update rendered views.
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