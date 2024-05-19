// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Globalization;

namespace Lepo.i18n.Yaml;

/// <summary>
/// Provides extension methods for the <see cref="LocalizationBuilder"/> class.
/// </summary>
public static class StringLocalizerBuilderExtensions
{
    public static LocalizationBuilder FromJson(
        this LocalizationBuilder builder,
        string path,
        CultureInfo culture
    )
    {
        return builder;
    }
}
