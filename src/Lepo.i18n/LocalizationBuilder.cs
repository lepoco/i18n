// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides functionality to build a collection of localized strings for different cultures.
/// </summary>
public class LocalizationBuilder
{
    private readonly HashSet<LocalizationSet> localizations = new HashSet<LocalizationSet>();

    /// <summary>
    /// Gets the collection of localized strings for different cultures.
    /// </summary>
    /// <returns>The collection of localized strings.</returns>
    public IEnumerable<LocalizationSet> GetLocalizations()
    {
        return localizations;
    }

    /// <summary>
    /// Adds a localization set to the collection.
    /// </summary>
    /// <param name="localization">The localization set to add.</param>
    /// <exception cref="InvalidOperationException">Thrown when a localization set for the same culture already exists in the collection.</exception>
    public void AddLocalization(LocalizationSet localization)
    {
        if (localizations.Any(x => x.Culture == localization.Culture))
        {
            // NOTE: Consider adding merging of multiple collections for one culture
            throw new InvalidOperationException(
                $"Localization for culture {localization.Culture} already exists."
            );
        }

        _ = localizations.Add(localization);
    }
}
