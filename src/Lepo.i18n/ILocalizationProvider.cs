// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides functionality to retrieve localization sets for specific cultures.
/// </summary>
public interface ILocalizationProvider
{
    /// <summary>
    /// Retrieves the localization set for the specified culture.
    /// </summary>
    /// <param name="culture">The culture to get the localization set for.</param>
    /// <returns>The localization set for the specified culture, or null if no localization set is found.</returns>

    LocalizationSet? Get(CultureInfo culture);

    /// <summary>
    /// Retrieves the localization set with the specified name for the specified culture.
    /// </summary>
    /// <param name="name">The name of the localization set to get.</param>
    /// <param name="culture">The culture to get the localization set for.</param>
    /// <returns>The localization set with the specified name for the specified culture, or null if no localization set is found.</returns>

    LocalizationSet? Get(string name, CultureInfo culture);
}
