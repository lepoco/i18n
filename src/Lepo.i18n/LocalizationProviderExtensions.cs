// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides extension methods for the <see cref="ILocalizationProvider"/> interface.
/// </summary>
public static class LocalizationProviderExtensions
{
    /// <summary>
    /// Retrieves a set of localized strings for a specific culture from the <see cref="ILocalizationProvider"/>.
    /// </summary>
    /// <param name="provider">The <see cref="ILocalizationProvider"/> to retrieve the localized strings from.</param>
    /// <param name="cultureName">The culture for which the localized strings are provided.</param>
    /// <returns>The set of localized strings, or null if no such set exists.</returns>
    public static LocalizationSet? GetLocalizationSet(
        this ILocalizationProvider provider,
        string cultureName
    )
    {
        return provider.GetLocalizationSet(new CultureInfo(cultureName), default);
    }

    /// <summary>
    /// Retrieves a set of localized strings for a specific culture and name from the <see cref="ILocalizationProvider"/>.
    /// </summary>
    /// <param name="provider">The <see cref="ILocalizationProvider"/> to retrieve the localized strings from.</param>
    /// <param name="cultureName">The culture for which the localized strings are provided.</param>
    /// <param name="name">The base name of the resource.</param>
    /// <returns>The set of localized strings, or null if no such set exists.</returns>
    public static LocalizationSet? GetLocalizationSet(
        this ILocalizationProvider provider,
        string cultureName,
        string name
    )
    {
        return provider.GetLocalizationSet(new CultureInfo(cultureName), name);
    }
}
