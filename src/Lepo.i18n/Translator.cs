// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Lepo.i18n.Yaml;

namespace Lepo.i18n;

/// <summary>
/// Provides functionality to translate strings. This class is obsolete and <see cref="LocalizationProvider"/> should be used instead.
/// </summary>
#if NET5_0_OR_GREATER
[ExcludeFromCodeCoverage(Justification = "This class is obsolete and should not be used.")]
#endif
[Obsolete($"{nameof(Translator)} is obsolete, use {nameof(LocalizationProvider)} instead.")]
public static class Translator
{
    /// <summary>
    /// Translates a string to the current culture.
    /// </summary>
    /// <param name="value">The string to translate.</param>
    /// <returns>The translated string if a translation exists, otherwise the original string.</returns>
    [Obsolete("This method is obsolete and should not be used.")]
    public static string String(string value)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (value is null)
        {
            return string.Empty;
        }

        LocalizationSet? localizationSet = LocalizationProviderFactory
            .GetInstance()
            ?.GetLocalizationSet(
                LocalizationProviderFactory.GetInstance()?.GetCulture()
                    ?? CultureInfo.CurrentUICulture,
                default
            );

        if (localizationSet is null)
        {
            return value;
        }

        return localizationSet.Strings.FirstOrDefault(s => s.Key == value).Value ?? value;
    }

    /// <summary>
    /// Prepares a string for translation. This method is obsolete and should not be used.
    /// </summary>
    /// <param name="value">The text or key to prepare.</param>
    /// <param name="parameters">The parameters to use in the prepared string.</param>
    /// <returns>Throws an exception indicating that this method is obsolete.</returns>
    [Obsolete("This method is obsolete and should not be used.")]
    public static string Prepare(string value, params object[] parameters)
    {
        return FillPlaceholders(String(value), parameters);
    }

    /// <summary>
    /// Translates a string to the correct plural form for the current culture. This method is obsolete and should not be used.
    /// </summary>
    /// <param name="single">The singular form of the string.</param>
    /// <param name="plural">The plural form of the string.</param>
    /// <param name="number">The number to determine the correct form.</param>
    /// <returns>Throws an exception indicating that this method is obsolete.</returns>
    [Obsolete("This method is obsolete and should not be used.")]
    public static string Plural(string single, string plural, object number)
    {
        bool isPlural =
            (number.ToString() ?? "0").All(char.IsDigit)
            && int.Parse(number.ToString() ?? "0") != 1;

        return Prepare(isPlural ? plural : single, number);
    }

    /// <summary>
    /// Loads the specified language into memory and allows you to use it globally.
    /// </summary>
    [Obsolete("This method is obsolete and should not be used.")]
    public static bool LoadLanguage(
        Assembly applicationAssembly,
        string language,
        string embeddedResourcePath,
        bool reload = false
    )
    {
        return LoadLanguages(
            applicationAssembly,
            new Dictionary<string, string> { { language, embeddedResourcePath } },
            reload
        );
    }

    /// <summary>
    /// Asynchronously loads the specified language into memory and allows you to use it globally.
    /// </summary>
    [Obsolete("This method is obsolete and should not be used.")]
    public static Task<bool> LoadLanguageAsync(
        Assembly applicationAssembly,
        string language,
        string embeddedResourcePath,
        bool reload = false
    )
    {
        return Task.FromResult(
            LoadLanguage(applicationAssembly, language, embeddedResourcePath, reload)
        );
    }

    /// <summary>
    /// Loads all specified languages into global memory.
    /// </summary>
    [Obsolete("This method is obsolete and should not be used.")]
    public static bool LoadLanguages(
        Assembly applicationAssembly,
        IDictionary<string, string> languagesCollection,
        bool reload = false
    )
    {
        LocalizationBuilder builder = new();

        foreach (KeyValuePair<string, string> languageResource in languagesCollection)
        {
            if (
                (
                    languageResource.Value.EndsWith(".yml")
                    || languageResource.Value.EndsWith(".yaml")
                )
            )
            {
                _ = builder.FromYaml(
                    applicationAssembly,
                    languageResource.Value,
                    new CultureInfo(languageResource.Key.Replace("_", "-"))
                );
            }
            else
            {
                builder.FromResource(
                    applicationAssembly,
                    languageResource.Value,
                    new CultureInfo(languageResource.Key.Replace("_", "-"))
                );
            }
        }

        LocalizationProviderFactory.SetInstance(builder.Build());

        return true;
    }

    /// <summary>
    /// Asynchronously loads all specified languages into global memory.
    /// </summary>
    [Obsolete("This method is obsolete and should not be used.")]
    public static Task<bool> LoadLanguagesAsync(
        Assembly applicationAssembly,
        IDictionary<string, string> languagesCollection,
        bool reload = false
    )
    {
        return Task.FromResult(LoadLanguages(applicationAssembly, languagesCollection, reload));
    }

    /// <summary>
    /// Changes the currently used language.
    /// </summary>
    /// <param name="language">The language code to which you would like to assign the selected file. i.e: <i>pl_PL</i></param>
    [Obsolete("This method is obsolete and should not be used.")]
    public static bool SetLanguage(string language)
    {
        LocalizationProviderFactory
            .GetInstance()
            ?.SetCulture(new CultureInfo(language.Replace("_", "-")));

        return true;
    }

    /// <summary>
    /// Asynchronously changes the currently used language.
    /// </summary>
    /// <param name="language">The language code to which you would like to assign the selected file. i.e: <i>pl_PL</i></param>
    [Obsolete("This method is obsolete and should not be used.")]
    public static Task<bool> SetLanguageAsync(string language)
    {
        return Task.FromResult(SetLanguage(language));
    }

    /// <summary>
    /// Defines the currently used language of the application.
    /// </summary>
    [Obsolete("This method is obsolete and should not be used.")]
    public static bool SetLanguage(
        Assembly applicationAssembly,
        string language,
        string embeddedResourcePath,
        bool reload = false
    )
    {
        CultureInfo culture = new(language);

        CultureInfo.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        LocalizationProviderFactory.GetInstance()?.SetCulture(culture);

        return true;
    }

    /// <summary>
    /// Asynchronously defines the currently used language of the application.
    /// </summary>
    [Obsolete("This method is obsolete and should not be used.")]
    public static Task<bool> SetLanguageAsync(
        Assembly applicationAssembly,
        string language,
        string embeddedResourcePath,
        bool reload = false
    )
    {
        return Task.FromResult(
            SetLanguage(applicationAssembly, language, embeddedResourcePath, reload)
        );
    }

    /// <summary>
    /// Clears dictionary with languages records.
    /// </summary>
    [Obsolete("This method is obsolete and should not be used.")]
    public static bool Flush()
    {
        return true;
    }

    /// <summary>
    /// Asynchronously clears dictionary with languages records.
    /// </summary>
    [Obsolete("This method is obsolete and should not be used.")]
    public static Task<bool> FlushAsync()
    {
        return Task.FromResult(Flush());
    }

    /// <summary>
    /// Fills placeholders in a string with the provided values.
    /// </summary>
    /// <param name="value">The string with placeholders.</param>
    /// <param name="placeholders">The values to fill the placeholders with.</param>
    /// <returns>The string with filled placeholders.</returns>
    private static string FillPlaceholders(string value, object[] placeholders)
    {
        for (int i = 0; i < placeholders.Length; i++)
        {
            value = value.Replace($"{{{i}}}", placeholders[i].ToString());
        }

        return value;
    }
}
