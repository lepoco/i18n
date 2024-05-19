// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides functionality to retrieve localization sets for specific cultures.
/// </summary>
public class LocalizationProvider(CultureInfo culture, IEnumerable<LocalizationSet> sets)
    : ILocalizationProvider
{
    private static LocalizationProvider? instance;

    /// <summary>
    /// Retrieves the localization set for the specified culture.
    /// </summary>
    /// <param name="culture">The culture to get the localization set for.</param>
    /// <returns>The localization set for the specified culture, or null if no localization set is found.</returns>
    public LocalizationSet? Get(CultureInfo culture)
    {
        return sets.FirstOrDefault(s => s.Culture.Equals(culture));
    }

    /// <summary>
    /// Retrieves the localization set with the specified name for the specified culture.
    /// </summary>
    /// <param name="name">The name of the localization set to get.</param>
    /// <param name="culture">The culture to get the localization set for.</param>
    /// <returns>The localization set with the specified name for the specified culture, or null if no localization set is found.</returns>
    public LocalizationSet? Get(string name, CultureInfo culture)
    {
        return sets.FirstOrDefault(s => s.Culture.Equals(culture) && s.Name == name);
    }

    /// <summary>
    /// Gets the current culture.
    /// </summary>
    /// <returns>The current culture.</returns>
    public CultureInfo GetCulture()
    {
        return culture;
    }

    /// <summary>
    /// Sets the current culture.
    /// </summary>
    /// <param name="cultureInfo">The culture to set.</param>
    public void SetCulture(CultureInfo cultureInfo)
    {
        culture = cultureInfo;
    }

    /// <summary>
    /// Gets the current instance of the <see cref="LocalizationProvider"/>.
    /// </summary>
    /// <returns>The current instance of the <see cref="LocalizationProvider"/>, or null if no instance has been set.</returns>
    public static LocalizationProvider? GetInstance()
    {
        return instance;
    }

    /// <summary>
    /// Sets the current instance of the <see cref="LocalizationProvider"/>.
    /// </summary>
    /// <param name="provider">The <see cref="LocalizationProvider"/> to set as the current instance.</param>
    public static void SetInstance(LocalizationProvider provider)
    {
        instance = provider;
    }
}
