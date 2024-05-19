// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Builds a dictionary of localized strings for different cultures.
/// </summary>
public class StringLocalizerBuilder
{
    private readonly IDictionary<CultureInfo, IEnumerable<LocalizedString>> localizations =
        new Dictionary<CultureInfo, IEnumerable<LocalizedString>>();

    /// <summary>
    /// Gets the dictionary of localized strings for different cultures.
    /// </summary>
    /// <returns>The dictionary of localized strings.</returns>
    public IDictionary<CultureInfo, IEnumerable<LocalizedString>> GetLocalizations()
    {
        return localizations;
    }

    /// <summary>
    /// Adds a new set of localized strings for a specific culture to the dictionary.
    /// </summary>
    /// <param name="culture">The culture for which the localized strings are provided.</param>
    /// <param name="localizedStrings">The localized strings for the specified culture.</param>
    /// <exception cref="InvalidOperationException">Thrown when localized strings for the specified culture already exist in the dictionary.</exception>
    public void AddLocalization(CultureInfo culture, IEnumerable<LocalizedString> localizedStrings)
    {
        if (localizations.ContainsKey(culture))
        {
            // NOTE: Consider adding merging of multiple collections for one culture
            throw new InvalidOperationException(
                $"Localization for culture {culture} already exists."
            );
        }

        localizations.Add(culture, localizedStrings);
    }
}
