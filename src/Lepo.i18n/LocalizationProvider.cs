// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides functionality to retrieve localization sets for specific cultures.
/// </summary>
public class LocalizationProvider(
    CultureInfo currentCulture,
    IEnumerable<LocalizationSet> localizationSets
) : ILocalizationProvider
{
    /// <inheritdoc />
    public LocalizationSet? GetLocalizationSet(CultureInfo culture, string? name)
    {
        if (name is null)
        {
            return localizationSets.FirstOrDefault(s => s.Culture.Equals(culture));
        }

        return localizationSets.FirstOrDefault(s => s.Culture.Equals(culture) && s.Name == name);
    }

    /// <inheritdoc />
    public CultureInfo GetCulture()
    {
        return currentCulture;
    }

    /// <inheritdoc />
    public void SetCulture(CultureInfo cultureInfo)
    {
        currentCulture = cultureInfo;
    }
}
