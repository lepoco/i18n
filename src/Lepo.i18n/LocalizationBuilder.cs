// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Lepo.i18n.IO;

namespace Lepo.i18n;

/// <summary>
/// Provides functionality to build a collection of localized strings for different cultures.
/// </summary>
public class LocalizationBuilder
{
    private readonly HashSet<LocalizationSet> localizations = [];

    private CultureInfo? selectedCulture;

    /// <summary>
    /// Builds an <see cref="ILocalizationProvider"/> using the current culture and localizations.
    /// </summary>
    /// <returns>An <see cref="ILocalizationProvider"/> with the current culture and localizations.</returns>
    public virtual ILocalizationProvider Build()
    {
        return new LocalizationProvider(
            selectedCulture ?? CultureInfo.CurrentCulture,
            localizations
        );
    }

    /// <summary>
    /// Sets the culture for the <see cref="LocalizationBuilder"/>.
    /// </summary>
    /// <param name="culture">The culture to set.</param>
    public virtual void SetCulture(CultureInfo culture)
    {
        selectedCulture = culture;
    }

    /// <summary>
    /// Adds a localization set to the collection.
    /// </summary>
    /// <param name="localization">The localization set to add.</param>
    /// <exception cref="InvalidOperationException">Thrown when a localization set for the same culture already exists in the collection.</exception>
    public virtual void AddLocalization(LocalizationSet localization)
    {
        if (
            localizations.Any(x =>
                x.Name == localization.Name && x.Culture.Equals(localization.Culture)
            )
        )
        {
            // NOTE: Consider adding merging of multiple collections for one culture
            throw new InvalidOperationException(
                $"Localization \"{localization.Name}\" for culture {localization.Culture} already exists."
            );
        }

        _ = localizations.Add(localization);
    }

    /// <summary>
    /// Adds localized strings from a resource in the calling assembly to the <see cref="LocalizationBuilder"/>.
    /// </summary>
    /// <typeparam name="TResource">The type of the resource.</typeparam>
    /// <param name="builder">The <see cref="LocalizationBuilder"/> to add the localized strings to.</param>
    /// <param name="culture">The culture for which the localized strings are provided.</param>
    /// <returns>The <see cref="LocalizationBuilder"/> with the added localized strings.</returns>
    public virtual void FromResource<TResource>(CultureInfo culture)
    {
        Type resourceType = typeof(TResource);
        string? resourceName = resourceType.FullName;

        if (resourceName is null)
        {
            return;
        }

        FromResource(resourceType.Assembly, resourceName, culture);
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
    public virtual void FromResource(Assembly assembly, string baseName, CultureInfo culture)
    {
        LocalizationSet? localizationSet = LocalizationSetResourceParser.Parse(
            assembly,
            baseName,
            culture
        );

        if (localizationSet is not null)
        {
            AddLocalization(localizationSet);
        }
    }
}
