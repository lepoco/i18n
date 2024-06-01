// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.DependencyInjection;

/// <summary>
/// Provides a provider-based implementation of the <see cref="IStringLocalizerFactory"/> interface.
/// </summary>
/// <remarks>
/// This class uses an <see cref="ILocalizationProvider"/> to retrieve localization sets,
/// and an <see cref="ILocalizationCultureManager"/> to manage the current culture.
/// </remarks>
public class ProviderBasedStringLocalizerFactory(
    ILocalizationProvider localizations,
    ILocalizationCultureManager cultureManager
) : IStringLocalizerFactory
{
    /// <inheritdoc />
    public IStringLocalizer Create(Type resourceSource)
    {
        string? baseName = resourceSource.FullName?.ToLower().Trim();

        return Create(baseName ?? default, default);
    }

    /// <inheritdoc />
    public IStringLocalizer Create(string? baseName, string? location)
    {
        LocalizationSet? resourceLocalizationSet = localizations.GetLocalizationSet(
            cultureManager.GetCulture(),
            baseName
        );

        if (resourceLocalizationSet is null)
        {
            resourceLocalizationSet = localizations.GetLocalizationSet(
                cultureManager.GetCulture(),
                default
            );
        }

        if (resourceLocalizationSet is null)
        {
            throw new InvalidOperationException(
                "No localization set found for the given resource source."
            );
        }

        return new LocalizationSetStringLocalizer(resourceLocalizationSet);
    }
}
