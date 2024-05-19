// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides functionality to manage the current culture for localization.
/// </summary>
public class LocalizationCultureManager : ILocalizationCultureManager
{
    /// <inheritdoc />
    public CultureInfo GetCulture()
    {
        return LocalizationProvider.GetInstance()?.GetCulture() ?? CultureInfo.CurrentCulture;
    }

    /// <inheritdoc />
    public void SetCulture(CultureInfo culture)
    {
        LocalizationProvider.GetInstance()?.SetCulture(culture);
    }
}
