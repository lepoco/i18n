// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides extension methods for the <see cref="LocalizationBuilder"/> class.
/// </summary>
public static class LocalizationBuilderExtensions
{
    /// <summary>
    /// Adds a new set of localized strings for a specific culture to the <see cref="LocalizationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to add the localized strings to.</param>
    /// <param name="culture">The culture for which the localized strings are provided.</param>
    /// <param name="localizations">A dictionary where the key is the original string and the value is the localized string.</param>
    /// <returns>The <see cref="LocalizationBuilder"/> with the added localized strings.</returns>
    public static LocalizationBuilder AddLocalization(
        this LocalizationBuilder builder,
        CultureInfo culture,
        IDictionary<string, string?> localizations
    )
    {
        builder.AddLocalization(new LocalizationSet(default, culture, localizations));

        return builder;
    }

    public static LocalizationBuilder FromResource<TResource>(
        this LocalizationBuilder builder,
        CultureInfo culture
    )
    {
        return builder.FromResource<TResource>(culture, Assembly.GetCallingAssembly());
    }

    public static LocalizationBuilder FromResource<TResource>(
        this LocalizationBuilder builder,
        CultureInfo culture,
        Assembly assembly
    )
    {
        string? resourceName = typeof(TResource).FullName;

        if (resourceName is null)
        {
            return builder;
        }

        return builder.FromResource(assembly, resourceName, culture);
    }

    public static LocalizationBuilder FromResource(
        this LocalizationBuilder builder,
        Assembly assembly,
        string baseName,
        CultureInfo culture
    )
    {
        try
        {
            ResourceManager resourceManager = new ResourceManager(baseName, assembly);
            ResourceSet? resourceSet = resourceManager.GetResourceSet(culture, true, true);

            if (resourceSet is null)
            {
                return builder;
            }

            Dictionary<string, string?> localizations = resourceSet
                .Cast<DictionaryEntry>()
                .Where(x => x.Key is string)
                .ToDictionary(x => (string)x.Key!, x => (string?)x.Value);

            builder.AddLocalization(new LocalizationSet(baseName, culture, localizations));

            return builder;
        }
        catch (MissingManifestResourceException ex)
        {
            throw new LocalizationBuilderException("Failed to register translation resources.", ex);
        }
    }
}
