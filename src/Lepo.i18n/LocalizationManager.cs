// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Manages localization settings.
/// </summary>
/// <remarks>
/// This class implements the <see cref="ILocalizationManager"/> interface and provides a method to set the culture for localization.
/// </remarks>
public class LocalizationManager : ILocalizationManager
{
    /// <inheritdoc />
    public void SetCulture(CultureInfo culture)
    {
        StringLocalizerFactory.GetInstance()?.SetCulture(culture);
    }

    // TODO: Notifications / events for culture changes.
}
