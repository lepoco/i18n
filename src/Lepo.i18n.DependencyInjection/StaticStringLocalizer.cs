// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.DependencyInjection;

/// <summary>
/// Provides functionality to localize strings.
/// </summary>
public class StaticStringLocalizer(
    ILocalizationProvider localizations,
    ILocalizationCultureManager cultureManager
) : IStringLocalizer
{
    /// <summary>
    /// Gets the localized string for the specified name.
    /// </summary>
    /// <param name="name">The name of the localized string.</param>
    /// <returns>The localized string.</returns>
    public LocalizedString this[string name] => this[name, Array.Empty<object>()];

    /// <summary>
    /// Gets the localized string for the specified name and format arguments.
    /// </summary>
    /// <param name="name">The name of the localized string.</param>
    /// <param name="arguments">The format arguments.</param>
    /// <returns>The localized string.</returns>
    public LocalizedString this[string name, params object[] arguments] =>
        LocalizeString(name, arguments);

    /// <summary>
    /// Gets all the localized strings for the current culture.
    /// </summary>
    /// <param name="_">A boolean parameter (not used).</param>
    /// <returns>The localized strings.</returns>
    public IEnumerable<LocalizedString> GetAllStrings(bool _)
    {
        return localizations
                .GetLocalizationSet(cultureManager.GetCulture(), default)
                ?.Strings.Select(x => new LocalizedString(x.Key, x.Value ?? x.Key)) ?? [];
    }

    private LocalizedString LocalizeString(string name, object[] placeholders)
    {
        return new LocalizedString(
            name,
            FillPlaceholders(
                GetAllStrings(true).FirstOrDefault(x => x.Name == name) ?? name,
                placeholders
            )
        );
    }

    private static string FillPlaceholders(string value, object[] placeholders)
    {
        for (int i = 0; i < placeholders.Length; i++)
        {
            value = value.Replace($"{{{i}}}", placeholders[i].ToString());
        }

        return value;
    }
}
