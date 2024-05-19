// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Manages localization settings.
/// </summary>
public interface ILocalizationManager
{
    /// <summary>
    /// Sets the culture to be used for localization.
    /// </summary>
    /// <param name="culture">The culture to be used for localization.</param>
    /// <remarks>
    /// This method sets the culture for the <see cref="StringLocalizerFactory"/> instance.
    /// </remarks>
    void SetCulture(CultureInfo culture);
}
