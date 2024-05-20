// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides extension methods for the <see cref="ILocalizationCultureManager"/> class.
/// </summary>
public static class LocalizationCultureManagerExtensions
{
    /// <summary>
    /// Sets the current culture.
    /// </summary>
    /// <param name="manager">The <see cref="ILocalizationCultureManager"/> to set the culture for.</param>
    /// <param name="cultureName">The culture to set.</param>
    public static ILocalizationCultureManager SetCulture(
        this ILocalizationCultureManager manager,
        string cultureName
    )
    {
        manager.SetCulture(new CultureInfo(cultureName));

        return manager;
    }
}
