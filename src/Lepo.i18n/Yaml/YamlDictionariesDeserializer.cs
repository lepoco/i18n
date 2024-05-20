// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Yaml;

/// <summary>
/// Some weird YAML implementation. Don't ask me, it was supposed to be simple...
/// </summary>
public static class YamlDictionariesDeserializer
{
    private const string DefaultNamespace = "default";

    public static IDictionary<string, IDictionary<string, string>> FromString(string input)
    {
        Dictionary<string, IDictionary<string, string>> result = new();
        string[] lines = input.Split(
            new Char[] { '\n', '\r' },
            StringSplitOptions.RemoveEmptyEntries
        );
        string currentNamespace = DefaultNamespace;
        int currentIndentation = 0;

        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();

            // Ignore comments
            if (trimmedLine.StartsWith("#"))
            {
                continue;
            }

            // Check for namespace
            if (trimmedLine.EndsWith(":"))
            {
                currentNamespace = trimmedLine.TrimEnd(':');
                currentIndentation = line.IndexOf(trimmedLine);
                continue;
            }

            // Check for nested namespace
            if (trimmedLine.StartsWith("- "))
            {
                currentNamespace += "." + trimmedLine.TrimStart('-').Trim();
                continue;
            }

            // Check for end of namespace
            if (line.IndexOf(trimmedLine) < currentIndentation)
            {
                currentNamespace = DefaultNamespace;
                currentIndentation = 0;
            }

            // Split key and value
            string[] parts = trimmedLine.Split(new[] { ':' }, 2).Select(p => p.Trim()).ToArray();

            if (parts.Length != 2)
            {
                continue;
            }

            // Remove start and end quotes from value, but keep quotes within the string
            string value = RemoveStartEndQuotes(parts[1]);

            // Add to dictionary
            AddToDictionary(result, currentNamespace, parts[0], value);
        }

        return result;
    }

    private static string RemoveStartEndQuotes(string value)
    {
        if (
            (value.StartsWith("'") && value.EndsWith("'"))
            || (value.StartsWith("\"") && value.EndsWith("\""))
        )
        {
            value = value.Substring(1, value.Length - 2);
        }

        return value;
    }

    private static void AddToDictionary(
        IDictionary<string, IDictionary<string, string>> dict,
        string namespaceKey,
        string key,
        string value
    )
    {
        if (!dict.ContainsKey(namespaceKey))
        {
            dict[namespaceKey] = new Dictionary<string, string>();
        }

        dict[namespaceKey][key] = value;

        // If it's not a nested namespace, also add to default
        if (!namespaceKey.Contains("."))
        {
            if (!dict.ContainsKey(DefaultNamespace))
            {
                dict[DefaultNamespace] = new Dictionary<string, string>();
            }

            dict[DefaultNamespace][key] = value;
        }
    }
}
