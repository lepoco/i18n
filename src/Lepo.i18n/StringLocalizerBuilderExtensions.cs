// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides extension methods for the <see cref="StringLocalizerBuilder"/> class.
/// </summary>
public static class StringLocalizerBuilderExtensions
{
    /// <summary>
    /// Adds a new set of localized strings for a specific culture to the <see cref="StringLocalizerBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringLocalizerBuilder"/> to add the localized strings to.</param>
    /// <param name="culture">The culture for which the localized strings are provided.</param>
    /// <param name="localizations">A dictionary where the key is the original string and the value is the localized string.</param>
    /// <returns>The <see cref="StringLocalizerBuilder"/> with the added localized strings.</returns>
    public static StringLocalizerBuilder AddLocalization(
        this StringLocalizerBuilder builder,
        CultureInfo culture,
        IDictionary<string, string> localizations
    )
    {
        builder.AddLocalization(
            culture,
            localizations.Select(x => new LocalizedString(x.Key, x.Value))
        );

        return builder;
    }

    public static StringLocalizerBuilder FromResource(
        this StringLocalizerBuilder builder,
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

            Dictionary<string, string> localizations = resourceSet
                .Cast<DictionaryEntry>()
                .Where(x => x.Key is string && x.Value is string)
                .ToDictionary(x => (string)x.Key!, x => (string)x.Value!);

            return builder.AddLocalization(culture, localizations);
        }
        catch (MissingManifestResourceException ex)
        {
            throw new StringLocalizerBuilderException(
                "Failed to register translation resources.",
                ex
            );
        }
    }
}
