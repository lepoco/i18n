// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Lepo.i18n.IO;
using Lepo.i18n.Json.Converters;
using Lepo.i18n.Json.Models;
using Lepo.i18n.Json.Models.v1;

namespace Lepo.i18n.Json;

/// <summary>
/// Provides extension methods for the <see cref="LocalizationBuilder"/> class.
/// </summary>
public static class LocalizationBuilderExtensions
{
    private static readonly JsonSerializerOptions DefaultJsonSerializerOptions =
        new()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            Converters = { new TranslationsContainerConverter() }
        };

    /// <summary>
    /// Loads localization data from a JSON file in the calling assembly.
    /// </summary>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to add the localization data to.</param>
    /// <param name="path">The path to the JSON file.</param>
    /// <param name="culture">The culture of the localization data.</param>
    /// <returns>The updated <see cref="LocalizationBuilder"/>.</returns>
    public static LocalizationBuilder FromJson(
        this LocalizationBuilder builder,
        string path,
        CultureInfo culture
    )
    {
        return builder.FromJson(Assembly.GetCallingAssembly(), path, culture);
    }

    /// <summary>
    /// Loads localization data from a JSON file in the specified assembly.
    /// </summary>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to add the localization data to.</param>
    /// <param name="assembly">The assembly that contains the JSON file.</param>
    /// <param name="path">The path to the JSON file.</param>
    /// <param name="culture">The culture of the localization data.</param>
    /// <returns>The updated <see cref="LocalizationBuilder"/>.</returns>
    public static LocalizationBuilder FromJson(
        this LocalizationBuilder builder,
        Assembly assembly,
        string path,
        CultureInfo culture
    )
    {
        if (!path.EndsWith(".json"))
        {
            throw new ArgumentException(
                $"Parameter {nameof(path)} in {nameof(FromJson)} must be path to the JSON file."
            );
        }

        string? contents = EmbeddedResourceReader.ReadToEnd(path, assembly);

        if (contents is null)
        {
            throw new LocalizationBuilderException(
                $"Resource {path} not found in assembly {assembly.FullName}."
            );
        }

        Version schemaVersion =
            new(
                JsonSerializer
                    .Deserialize<ITranslationsContainer>(contents, DefaultJsonSerializerOptions)
                    ?.Version ?? "1.0.0"
            );

        if (!schemaVersion.Major.Equals(1))
        {
            throw new LocalizationBuilderException(
                $"Localization file with schema version \"{schemaVersion.ToString() ?? "unknown"}\" is not supported."
            );
        }

        TranslationFile? translationFile = JsonSerializer.Deserialize<TranslationFile>(
            contents,
            DefaultJsonSerializerOptions
        );

        if (translationFile is null)
        {
            throw new LocalizationBuilderException("Unable to extract data from json file.");
        }

        Dictionary<string, string> localizedStrings = new();

        foreach (TranslationEntity localizedString in translationFile.Strings)
        {
            if (localizedStrings.ContainsKey(localizedString.Name))
            {
                throw new LocalizationBuilderException(
                    $"The {path} file contains duplicate \"{localizedString.Name}\" keys."
                );
            }

            localizedStrings.Add(localizedString.Name, localizedString.Value);
        }

        builder.AddLocalization(
            new LocalizationSet(
                Path.GetFileNameWithoutExtension(path).Trim().ToLowerInvariant(),
                culture,
                localizedStrings!
            )
        );

        return builder;
    }
}
