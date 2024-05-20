// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides a factory for managing the current instance of the <see cref="LocalizationProvider"/>.
/// </summary>
public static class LocalizationProviderFactory
{
    private static ILocalizationProvider? instance;

    /// <summary>
    /// Gets the current instance of the <see cref="ILocalizationProvider"/>.
    /// </summary>
    /// <returns>The current instance of the <see cref="ILocalizationProvider"/>, or null if no instance has been set.</returns>
    public static ILocalizationProvider? GetInstance()
    {
        return instance;
    }

    /// <summary>
    /// Sets the current instance of the <see cref="ILocalizationProvider"/>.
    /// </summary>
    /// <param name="provider">The <see cref="ILocalizationProvider"/> to set as the current instance.</param>
    public static void SetInstance(ILocalizationProvider provider)
    {
        instance = provider;
    }
}
