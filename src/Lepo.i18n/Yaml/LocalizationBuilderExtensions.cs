// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Lepo.i18n.IO;

namespace Lepo.i18n.Yaml;

/// <summary>
/// Provides extension methods for the <see cref="LocalizationBuilder"/> class.
/// </summary>
public static class LocalizationBuilderExtensions
{
    /// <summary>
    /// Adds localized strings from a YAML file in the calling assembly to the <see cref="LocalizationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to add the localized strings to.</param>
    /// <param name="path">The path to the YAML file in the assembly.</param>
    /// <param name="culture">The culture for which the localized strings are provided.</param>
    /// <returns>The <see cref="LocalizationBuilder"/> with the added localized strings.</returns>
    public static LocalizationBuilder FromYaml(
        this LocalizationBuilder builder,
        string path,
        CultureInfo culture
    )
    {
        return builder.FromYaml(Assembly.GetCallingAssembly(), path, culture);
    }

    /// <summary>
    /// Adds localized strings from a YAML file in the specified assembly to the <see cref="LocalizationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to add the localized strings to.</param>
    /// <param name="assembly">The assembly that contains the YAML file.</param>
    /// <param name="path">The path to the YAML file in the assembly.</param>
    /// <param name="culture">The culture for which the localized strings are provided.</param>
    /// <returns>The <see cref="LocalizationBuilder"/> with the added localized strings.</returns>
    /// <exception cref="ArgumentException">Thrown when the path does not point to a YAML file.</exception>
    /// <exception cref="Exception">Thrown when the YAML file cannot be found in the assembly.</exception>
    public static LocalizationBuilder FromYaml(
        this LocalizationBuilder builder,
        Assembly assembly,
        string path,
        CultureInfo culture
    )
    {
        if (!(path.EndsWith(".yml") || path.EndsWith(".yaml")))
        {
            throw new ArgumentException(
                $"Parameter {nameof(path)} in {nameof(FromYaml)} must be path to the YAML file."
            );
        }

        string? contents = EmbeddedResourceReader.ReadToEnd(path, assembly);

        if (contents is null)
        {
            throw new LocalizationBuilderException(
                $"Resource {path} not found in assembly {assembly.FullName}."
            );
        }

        string baseNamespace = Path.GetFileNameWithoutExtension(path).Trim().ToLowerInvariant();
        IDictionary<string, IDictionary<string, string>> deserializedYaml =
            YamlDictionariesDeserializer.FromString(contents);

        foreach (
            KeyValuePair<string, IDictionary<string, string>> localizedStrings in deserializedYaml
        )
        {
            string? name =
                (localizedStrings.Key is null || localizedStrings.Key == "default")
                    ? baseNamespace
                    : localizedStrings.Key;

            builder.AddLocalization(new LocalizationSet(name, culture, localizedStrings.Value!));
        }

        return builder;
    }
}
