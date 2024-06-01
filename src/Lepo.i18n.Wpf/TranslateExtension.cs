// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Wpf;

/// <summary>
/// Provides a markup extension that localizes strings in XAML.
/// </summary>
/// <remarks>
/// This class extends <see cref="MarkupExtension"/> and overrides the <see cref="ProvideValue"/> method to return localized strings.
/// </remarks>
[ContentProperty(nameof(String))]
[MarkupExtensionReturnType(typeof(string))]
[Obsolete(
    $"{nameof(TranslateExtension)} is obsolete, use {nameof(StringLocalizerExtension)} instead."
)]
public class TranslateExtension : MarkupExtension
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateExtension"/> class.
    /// </summary>
    public TranslateExtension() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateExtension"/> class with the specified text.
    /// </summary>
    /// <param name="text">The text to be localized.</param>

    public TranslateExtension(string? text)
    {
        String = EscapeText(text);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateExtension"/> class with the specified text, plural text, and number.
    /// </summary>
    /// <param name="text">The text to be localized.</param>
    /// <param name="plural">The plural text to be localized.</param>
    /// <param name="number">The number that determines whether to use the singular or plural form of the text.</param>

    public TranslateExtension(string? text, string? plural, object number)
    {
        String = EscapeText(text);
        Plural = EscapeText(plural);
        Number = number;
    }

    /// <summary>
    /// Word or key to be translated.
    /// </summary>
    public string String { get; set; }

    /// <summary>
    /// If the given <see cref="Number"/> is less than one the <see cref="String"/> will be used, if greater than one - the <see cref="Plural"/> version will be used.
    /// </summary>
    public string Plural { get; set; }

    /// <summary>
    /// The number that is used to determine whether we are using <see cref="Plural"/> or <see cref="String"/>. Can also be used for <see cref="Translator.Prepare"/>.
    /// </summary>
    public object Number { get; set; }

    /// <summary>
    /// Provider key.
    /// </summary>
    public string ProviderKey { get; set; } = string.Empty;

    /// <summary>
    /// Returns a localized string for the <see cref="String"/> property.
    /// </summary>
    /// <param name="serviceProvider">An object that provides services for the markup extension.</param>
    /// <returns>The localized string, or the original text if no localization is found.</returns>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        if (String is null)
        {
            return string.Empty;
        }

        LocalizationSet? localizationSet = LocalizationProviderFactory
            .GetInstance(ProviderKey)
            ?.GetLocalizationSet(
                LocalizationProviderFactory.GetInstance(ProviderKey)?.GetCulture()
                    ?? CultureInfo.CurrentUICulture,
                default
            );

        if (localizationSet is null)
        {
            return String;
        }

        string localizedString;

        if (IsSelectedNumberPlural())
        {
            localizedString =
                localizationSet.Strings.FirstOrDefault(s => s.Key == Plural).Value
                ?? Plural
                ?? string.Empty;
        }
        else
        {
            localizedString =
                localizationSet.Strings.FirstOrDefault(s => s.Key == String).Value
                ?? String
                ?? string.Empty;
        }

        return Prepare(localizedString, Number);
    }

    /// <summary>
    /// Determines if the selected number is plural.
    /// </summary>
    /// <returns>True if the number is greater than 1, false otherwise.</returns>
    private bool IsSelectedNumberPlural()
    {
        return (Number.ToString() ?? "0").All(char.IsDigit)
            && int.Parse(Number.ToString() ?? "0") != 1;
        ;
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
