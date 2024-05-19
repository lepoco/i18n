// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides a factory for managing a singleton instance of <see cref="StringLocalizer"/>.
/// </summary>
public static class StringLocalizerFactory
{
    private static StringLocalizer? instance;

    /// <summary>
    /// Sets the singleton instance of <see cref="StringLocalizer"/>.
    /// </summary>
    /// <param name="localizer">The <see cref="StringLocalizer"/> instance to be set.</param>
    internal static void SetInstance(StringLocalizer localizer)
    {
        instance = localizer;
    }

    /// <summary>
    /// Gets the singleton instance of <see cref="StringLocalizer"/>.
    /// </summary>
    /// <returns>The singleton instance of <see cref="StringLocalizer"/>, or null if no instance has been set.</returns>
    public static StringLocalizer? GetInstance()
    {
        return instance;
    }
}
