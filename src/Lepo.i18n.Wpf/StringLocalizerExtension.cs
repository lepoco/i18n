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
[ContentProperty(nameof(Text))]
[MarkupExtensionReturnType(typeof(string))]
public class StringLocalizerExtension : MarkupExtension
{
    /// <summary>
    /// Gets or sets the text to be localized.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringLocalizerExtension"/> class.
    /// </summary>
    public StringLocalizerExtension() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringLocalizerExtension"/> class with the specified text.
    /// </summary>
    /// <param name="text">The text to be localized.</param>
    public StringLocalizerExtension(string text)
    {
        Text = text;
    }

    /// <summary>
    /// Returns a localized string for the <see cref="Text"/> property.
    /// </summary>
    /// <param name="serviceProvider">An object that provides services for the markup extension.</param>
    /// <returns>The localized string, or the original text if no localization is found.</returns>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        if (Text is null)
        {
            return string.Empty;
        }

        //return LocalizationProvider.GetInstance()?[EscapeText(Text)] ?? EscapeText(Text);
        return string.Empty;
    }

    /// <summary>
    /// Escapes special characters in a string.
    /// </summary>
    /// <param name="text">The text to escape.</param>
    /// <returns>The escaped text.</returns>
    private static string EscapeText(string text)
    {
        return text.Replace("&amp;", "&")
            .Replace("&lt;", "<")
            .Replace("&gt;", ">")
            .Replace("&quot;", "\"")
            .Replace("&apos;", "'");
    }
}
