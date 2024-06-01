// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.IO;

/// <summary>
/// Provides functionality to parse resources into a <see cref="LocalizationSet"/>.
/// </summary>
public static class LocalizationSetResourceParser
{
    /// <summary>
    /// Parses resources from the specified assembly into a <see cref="LocalizationSet"/>.
    /// </summary>
    /// <param name="assembly">The assembly that contains the resources.</param>
    /// <param name="baseName">The base name of the resource.</param>
    /// <param name="culture">The culture for which the resources are provided.</param>
    /// <returns>A <see cref="LocalizationSet"/> that contains the parsed resources, or null if the resources cannot be found.</returns>
    /// <exception cref="LocalizationBuilderException">Thrown when the resources cannot be found in the assembly.</exception>
    public static LocalizationSet? Parse(Assembly assembly, string baseName, CultureInfo culture)
    {
        CultureInfo cultureToRestore = Thread.CurrentThread.CurrentCulture;

        // NOTE: Fix net framework satellite assembly loading
        try
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            ResourceManager resourceManager = new(baseName, assembly);

            ResourceSet? resourceSet = resourceManager.GetResourceSet(culture, true, true);

            if (resourceSet is null)
            {
                return null;
            }

            Dictionary<string, string?> localizations = resourceSet
                .Cast<DictionaryEntry>()
                .Where(x => x.Key is string)
                .ToDictionary(x => (string)x.Key!, x => (string?)x.Value);

            return new LocalizationSet(baseName.ToLowerInvariant(), culture, localizations);
        }
        catch (MissingManifestResourceException ex)
        {
            throw new LocalizationBuilderException(
                $"Failed to register translation resources for \"{culture}\".",
                ex
            );
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = cultureToRestore;
            Thread.CurrentThread.CurrentUICulture = cultureToRestore;
        }
    }
}
