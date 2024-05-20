// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Diagnostics.CodeAnalysis;

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
        ;

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
        throw new Exception("This method is obsolete and should not be used.");
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
        throw new Exception("This method is obsolete and should not be used.");
    }

    [Obsolete("This method is obsolete and should not be used.")]
    public static void LoadLanguages() { }

    [Obsolete("This method is obsolete and should not be used.")]
    public static void SetLanguage(string culture) { }
}
