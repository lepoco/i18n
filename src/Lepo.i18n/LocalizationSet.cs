// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Represents a set of localized strings for a specific culture.
/// </summary>
/// <param name="Name">The name of the localization set. This could be the name of the resource file or another identifier.</param>
/// <param name="Culture">The culture that the localized strings are for.</param>
/// <param name="Strings">The localized strings in this set.</param>
public record LocalizationSet(
    string? Name,
    CultureInfo Culture,
    IEnumerable<KeyValuePair<string, string?>> Strings
)
{
    public string? this[string key]
    {
        get
        {
            foreach (KeyValuePair<string, string?> localizationString in Strings)
            {
                if (localizationString.Key == key)
                {
                    return localizationString.Value;
                }
            }

            return key;
        }
    }
}
