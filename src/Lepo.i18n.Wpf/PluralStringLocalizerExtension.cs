// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Wpf;

/// <summary>
/// Provides a markup extension that localizes strings in XAML, supporting both singular and plural forms.
/// </summary>
/// <remarks>
/// This class extends <see cref="MarkupExtension"/> and overrides the <see cref="ProvideValue"/> method to return localized strings.
/// It uses the <see cref="Count"/> property to determine whether to use the singular or plural form of the text.
/// </remarks>

[ContentProperty(nameof(Count))]
[MarkupExtensionReturnType(typeof(int))]
public class PluralStringLocalizerExtension : MarkupExtension
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringLocalizerExtension"/> class.
    /// </summary>
    public PluralStringLocalizerExtension() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PluralStringLocalizerExtension"/> class with the specified count, text, and plural text.
    /// </summary>
    /// <param name="count">The count that determines whether to use the singular or plural form of the text.</param>
    /// <param name="text">The text to be localized.</param>
    /// <param name="pluralText">The plural text to be localized.</param>
    public PluralStringLocalizerExtension(int count, string text, string pluralText)
    {
        Count = count;
        Text = EscapeText(text);
        PluralText = EscapeText(pluralText);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PluralStringLocalizerExtension"/> class with the specified count, text, plural text, and namespace.
    /// </summary>
    /// <param name="count">The count that determines whether to use the singular or plural form of the text.</param>
    /// <param name="text">The text to be localized.</param>
    /// <param name="pluralText">The plural text to be localized.</param>
    /// <param name="namespaceName">The namespace of the text to be localized.</param>
    public PluralStringLocalizerExtension(
        int count,
        string text,
        string pluralText,
        string namespaceName
    )
    {
        Count = count;
        Text = EscapeText(text);
        PluralText = EscapeText(pluralText);
        Namespace = namespaceName;
    }

    /// <summary>
    /// Gets or sets the count that determines whether to use the singular or plural form of the text.
    /// </summary>
    public int? Count { get; set; }

    /// <summary>
    /// Gets or sets the text to be localized.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the plural text to be localized.
    /// </summary>
    public string? PluralText { get; set; }

    /// <summary>
    /// Gets or sets the namespace of the text to be localized.
    /// </summary>
    public string? Namespace { get; set; }

    /// <summary>
    /// Provider key.
    /// </summary>
    public string ProviderKey { get; set; } = string.Empty;

    /// <summary>
    /// Returns a localized string for the <see cref="Text"/> property.
    /// </summary>
    /// <param name="serviceProvider">An object that provides services for the markup extension.</param>
    /// <returns>The localized string, or the original text if no localization is found.</returns>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        if (Text is null && PluralText is null)
        {
            return string.Empty;
        }

        LocalizationSet? localizationSet = LocalizationProviderFactory
            .GetInstance(ProviderKey)
            ?.GetLocalizationSet(
                LocalizationProviderFactory.GetInstance(ProviderKey)?.GetCulture()
                    ?? CultureInfo.CurrentUICulture,
                Namespace?.ToLowerInvariant() ?? default
            );

        if (localizationSet is null)
        {
            return Text;
        }

        string localizedString;

        if (IsSelectedNumberPlural())
        {
            localizedString =
                localizationSet.Strings.FirstOrDefault(s => s.Key == PluralText).Value
                ?? PluralText
                ?? string.Empty;
        }
        else
        {
            localizedString =
                localizationSet.Strings.FirstOrDefault(s => s.Key == Text).Value
                ?? Text
                ?? string.Empty;
        }

        return Prepare(localizedString, Count);
    }

    /// <summary>
    /// Determines if the selected number is plural.
    /// </summary>
    /// <returns>True if the number is greater than 1, false otherwise.</returns>
    private bool IsSelectedNumberPlural()
    {
        return Count > 1;
    }

    /// <summary>
    /// Escapes special characters in a string.
    /// </summary>
    /// <param name="text">The text to escape.</param>
    /// <returns>The escaped text.</returns>
    private static string EscapeText(string? text)
    {
        if (text is null)
        {
            return string.Empty;
        }

        return text.Replace("&amp;", "&")
            .Replace("&lt;", "<")
            .Replace("&gt;", ">")
            .Replace("&quot;", "\"")
            .Replace("&apos;", "'")
            .Trim();
    }

    /// <summary>
    /// Prepares the localized string by replacing placeholders with the provided parameters.
    /// </summary>
    /// <param name="text">The text to prepare.</param>
    /// <param name="parameters">The parameters to replace the placeholders with.</param>
    /// <returns>The prepared text.</returns>
    private static string Prepare(string? text, params object[] parameters)
    {
        if (text is null)
        {
            return string.Empty;
        }

        return string.Format(text, parameters);
    }
}
