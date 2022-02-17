// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
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
        /// Translates <see cref="string"/> based on given word or key.
        /// </summary>
        /// <param name="textOrKey">Text to be translated.</param>
        /// <returns></returns>
        public static string String(string textOrKey)
        {
            if (System.String.IsNullOrEmpty(textOrKey))
            {
                System.Diagnostics.Debug.WriteLine($"WARNING | Null string provided in Translator.String(string), called from {(new System.Diagnostics.StackTrace()).GetFrame(1)}", "Lepo.i18n");

                return "i18n.error.nullInput";
            }


            return Translator.FirstOrDefault(textOrKey);
        }

        /// <summary>
        /// Translates a string and then replaces the <c>%s</c>, <c>%i</c>, <c>%f</c> or <c>%d</c>  keys.
        /// </summary>
        /// <param name="textOrKey">Text to be translated.</param>
        /// <param name="parameters">Strings, integers, double or floating number to be replaced.</param>
        public static string Prepare(string textOrKey, params object[] parameters)
        {
            if (System.String.IsNullOrEmpty(textOrKey))
            {
                System.Diagnostics.Debug.WriteLine($"WARNING | Null string provided in Translator.Prepare(string, params object[]), called from {(new System.Diagnostics.StackTrace()).GetFrame(1)}", "Lepo.i18n");

                return "i18n.error.nullInput";
            }

            var translatedString = Translator.FirstOrDefault(textOrKey);

            if (parameters == null || parameters.Length < 1)
                return translatedString;

            var parameterIndex = 0;
            var indexOffset = 0;
            var pattern = /* language=regex */ "[%]+[s|i|f|d]";

            // TODO: It's experimental

            foreach (Match match in Regex.Matches(translatedString, pattern))
            {
                if (parameters.Length < parameterIndex + 1)
                    break;

                // We make sure that we are working on a text string
                var embeddedString = (parameters[parameterIndex]?.ToString() ?? "").Trim();

                // Index + Offset + 1 char for replacement operator (s|i|f|d)
                if (match.Index + indexOffset > translatedString.Length)
                {
                    System.Diagnostics.Debug.WriteLine($"WARNING | The element replacement in Translator.Prepare(string, params object[]) operation failed. Input string: {translatedString}, parameter: {embeddedString}, called from {(new System.Diagnostics.StackTrace()).GetFrame(1)}", "Lepo.i18n");

                    break;
                }


                switch (translatedString.Substring(match.Index + indexOffset + 1, 1))
                {
                    case "d":
                        Double.TryParse(embeddedString, out double resultDouble);
                        embeddedString = resultDouble.ToString("F2");

                        break;
                    case "f":
                        Single.TryParse(embeddedString, out float resultFloat);
                        embeddedString = resultFloat.ToString("F2");

                        break;
                    case "i":
                        Int32.TryParse(embeddedString, out int resultValue);
                        embeddedString = resultValue.ToString();

                        break;
                }

                // Here, the actual manipulation of the output string is done.
                translatedString = translatedString.Remove(match.Index + indexOffset, 2).Insert(match.Index + indexOffset, embeddedString);

                // As we edit a string on the fly, we need to take into account that it changes.
                indexOffset += CalcAddedOffset(embeddedString);

                // Update parameter index from method attributes
                parameterIndex++;
            }

            //return Translator.FirstOrDefault(textOrKey).Replace("%s", replace);
            return translatedString;
        }

        /// <summary>
        /// Used when you want to use the appropriate form of a string based on whether a number is singular or plural.
        /// </summary>
        /// <param name="single">The text to be used if the number is singular.</param>
        /// <param name="plural">The text to be used if the number is plural.</param>
        /// <param name="number">The number to compare against to use either the singular or plural form.</param>
        public static string Plural(string single, string plural, object number)
        {
            if (System.String.IsNullOrEmpty(single) && System.String.IsNullOrEmpty(plural))
            {
                System.Diagnostics.Debug.WriteLine($"WARNING | Null string provided in Translator.Plural(string, string, object), called from {(new System.Diagnostics.StackTrace()).GetFrame(1)}", "Lepo.i18n");

                return "i18n.error.nullInput";
            }

            var isPlural = (number.ToString() ?? "0").All(char.IsDigit) && Int32.Parse(number.ToString() ?? "0") > 1;

            return isPlural ? Prepare(plural, number) : Prepare(single, number);
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

        /// <summary>
        /// Adds an offset to the two-letter translation key.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static int CalcAddedOffset(string input)
        {
            if (input.Length == 0)
                return -2;

            if (input.Length == 1)
                return -1;

            if (input.Length == 2)
                return 0;

            return input.Length - 2;
        }
    }
}