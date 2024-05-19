// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Yaml;

/// <summary>
/// Some weird YAML implementation. Don't ask me, it was supposed to be simple...
/// </summary>
public class YamlDecoder
{
    /// <summary>
    /// Creates new collection of mapped keys with translated values.
    /// </summary>
    /// <param name="content">String containing Yaml.</param>
    public static IDictionary<string, string> FromString(string content)
    {
        Dictionary<string, string> localizations = new();

        if (string.IsNullOrEmpty(content))
        {
            return localizations;
        }

        string[] splittedYamlLines = content.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );

        // TODO: Recognize tab stops as subsections

        if (splittedYamlLines.Length < 1)
        {
            return localizations;
        }

        foreach (string yamlLine in splittedYamlLines)
        {
            if (yamlLine.StartsWith("#") || String.IsNullOrEmpty(yamlLine))
            {
                continue;
            }

            string[] pair = yamlLine.Split(new[] { ':' }, 2);

            if (pair.Length < 2)
            {
                continue;
            }

            string mappedKey = pair[0].Trim();
            string translatedValue = pair[1].Trim();

            if (
                translatedValue.StartsWith("'") && translatedValue.EndsWith("'")
                || translatedValue.StartsWith("\"") && translatedValue.EndsWith("\"")
            )
            {
                translatedValue = translatedValue.Substring(1, translatedValue.Length - 2);
            }

            if (!localizations.ContainsKey(mappedKey))
            {
                localizations.Add(mappedKey, translatedValue);
            }
        }

        return localizations;
    }
}
