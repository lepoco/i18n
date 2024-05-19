// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Yaml;

/// <summary>
/// Provides extension methods for the <see cref="LocalizationBuilder"/> class.
/// </summary>
public static class StringLocalizerBuilderExtensions
{
    public static LocalizationBuilder FromYaml(
        this LocalizationBuilder builder,
        string path,
        CultureInfo culture
    )
    {
        //var yaml = new YamlStream();
        //using (var reader = new StreamReader(path))
        //{
        //    yaml.Load(reader);
        //}

        //var root = (YamlMappingNode)yaml.Documents[0].RootNode;
        //var localizations = root.Children.ToDictionary(
        //    x => x.Key.ToString(),
        //    x => x.Value.ToString()
        //);

        //return builder.AddLocalization(culture, localizations);
        return builder;
    }
}
