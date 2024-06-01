// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.DependencyInjection;

/// <summary>
/// Provides a provider-based implementation of the <see cref="IStringLocalizer"/> interface.
/// </summary>
/// <remarks>
/// This class uses an <see cref="ILocalizationProvider"/> to retrieve localization sets,
/// and an <see cref="ILocalizationCultureManager"/> to manage the current culture.
/// </remarks>
public class ProviderBasedStringLocalizer(
    ILocalizationProvider localizations,
    ILocalizationCultureManager cultureManager
) : IStringLocalizer
{
    /// <inheritdoc />
    public LocalizedString this[string name] => this[name, []];

    /// <inheritdoc />
    public LocalizedString this[string name, params object[] arguments] =>
        LocalizeString(name, arguments);

    /// <inheritdoc />
    public IEnumerable<LocalizedString> GetAllStrings(bool _)
    {
        return localizations
                .GetLocalizationSet(cultureManager.GetCulture(), default)
                ?.Strings.Select(x => new LocalizedString(x.Key, x.Value ?? x.Key)) ?? [];
    }

    /// <summary>
    /// Fills placeholders in a string with the provided values.
    /// </summary>
    /// <param name="name">The string with placeholders.</param>
    /// <param name="placeholders">The values to fill the placeholders with.</param>
    /// <returns>The string with filled placeholders.</returns>
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

    /// <summary>
    /// Fills placeholders in a string with the provided values.
    /// </summary>
    /// <param name="value">The string with placeholders.</param>
    /// <param name="placeholders">The values to fill the placeholders with.</param>
    /// <returns>The string with filled placeholders.</returns>
    private static string FillPlaceholders(string value, object[] placeholders)
    {
        for (int i = 0; i < placeholders.Length; i++)
        {
            value = value.Replace($"{{{i}}}", placeholders[i].ToString());
        }

        return value;
    }
}
