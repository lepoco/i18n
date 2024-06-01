// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Lepo.i18n.IO;

namespace Lepo.i18n;

/// <summary>
/// Provides extension methods for the <see cref="LocalizationBuilder"/> class.
/// </summary>
public static class LocalizationBuilderExtensions
{
    /// <summary>
    /// Sets the culture for the <see cref="LocalizationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to set the culture for.</param>
    /// <param name="cultureName">The culture to set.</param>
    /// <returns>The <see cref="LocalizationBuilder"/> with the set culture.</returns>
    public static LocalizationBuilder SetCulture(
        this LocalizationBuilder builder,
        string cultureName
    )
    {
        builder.SetCulture(new CultureInfo(cultureName));

        return builder;
    }

    /// <summary>
    /// Adds a new set of localized strings for a specific culture to the <see cref="LocalizationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to add the localized strings to.</param>
    /// <param name="name">The base name of the resource.</param>
    /// <param name="culture">The culture for which the localized strings are provided.</param>
    /// <param name="localizations">A dictionary where the key is the original string and the value is the localized string.</param>
    /// <returns>The <see cref="LocalizationBuilder"/> with the added localized strings.</returns>
    public static LocalizationBuilder AddLocalization(
        this LocalizationBuilder builder,
        string name,
        CultureInfo culture,
        IEnumerable<KeyValuePair<string, string?>> localizations
    )
    {
        builder.AddLocalization(
            new LocalizationSet(name.Trim().ToLowerInvariant(), culture, localizations)
        );

        return builder;
    }

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
        IEnumerable<KeyValuePair<string, string?>> localizations
    )
    {
        builder.AddLocalization(new LocalizationSet(default, culture, localizations));

        return builder;
    }

    /// <summary>
    /// Adds localized strings from a resource in the calling assembly to the <see cref="LocalizationBuilder"/>.
    /// </summary>
    /// <typeparam name="TResource">The type of the resource.</typeparam>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to add the localized strings to.</param>
    /// <param name="culture">The culture for which the localized strings are provided.</param>
    /// <returns>The <see cref="LocalizationBuilder"/> with the added localized strings.</returns>
    public static LocalizationBuilder FromResource<TResource>(
        this LocalizationBuilder builder,
        string culture
    )
    {
        return builder.FromResource<TResource>(new CultureInfo(culture));
    }

    /// <summary>
    /// Adds localized strings from a resource in the calling assembly to the <see cref="LocalizationBuilder"/>.
    /// </summary>
    /// <typeparam name="TResource">The type of the resource.</typeparam>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to add the localized strings to.</param>
    /// <param name="culture">The culture for which the localized strings are provided.</param>
    /// <returns>The <see cref="LocalizationBuilder"/> with the added localized strings.</returns>
    public static LocalizationBuilder FromResource<TResource>(
        this LocalizationBuilder builder,
        CultureInfo culture
    )
    {
        Type resourceType = typeof(TResource);
        string? resourceName = resourceType.FullName;

        if (resourceName is null)
        {
            return builder;
        }

        return builder.FromResource(resourceType.Assembly, resourceName, culture);
    }

    /// <summary>
    /// Adds localized strings from a resource with the specified base name in the specified assembly to the <see cref="LocalizationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to add the localized strings to.</param>
    /// <param name="baseName">The base name of the resource.</param>
    /// <param name="culture">The culture for which the localized strings are provided.</param>
    /// <returns>The <see cref="LocalizationBuilder"/> with the added localized strings.</returns>
    /// <exception cref="LocalizationBuilderException">Thrown when the resource cannot be found.</exception>
    public static LocalizationBuilder FromResource(
        this LocalizationBuilder builder,
        string baseName,
        CultureInfo culture
    )
    {
        return builder.FromResource(Assembly.GetCallingAssembly(), baseName, culture);
    }

    /// <summary>
    /// Adds localized strings from a resource with the specified base name in the specified assembly to the <see cref="LocalizationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to add the localized strings to.</param>
    /// <param name="assembly">The assembly that contains the resource.</param>
    /// <param name="baseName">The base name of the resource.</param>
    /// <param name="culture">The culture for which the localized strings are provided.</param>
    /// <returns>The <see cref="LocalizationBuilder"/> with the added localized strings.</returns>
    /// <exception cref="LocalizationBuilderException">Thrown when the resource cannot be found.</exception>
    public static LocalizationBuilder FromResource(
        this LocalizationBuilder builder,
        Assembly assembly,
        string baseName,
        CultureInfo culture
    )
    {
        LocalizationSet? localizationSet = LocalizationSetResourceParser.Parse(
            assembly,
            baseName,
            culture
        );

        if (localizationSet is not null)
        {
            builder.AddLocalization(localizationSet);
        }

        return builder;
    }
}
