// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Windows.Data;

namespace Lepo.i18n.Wpf;

/// <summary>
/// Translates text strings using the <c>Convert</c> method.
/// </summary>
[Obsolete($"{nameof(TranslateConverter)} is obsolete.")]
#pragma warning disable CS0618 // Type or member is obsolete
public sealed class TranslateConverter : IValueConverter
{
    /// <summary>
    /// Translates text strings using the Converter in XAML
    /// </summary>
    /// <param name="value">Binded value to be replaced via <see cref="Translator.Prepare"/></param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">Word or key of the translated string.</param>
    /// <param name="culture">Culture information.</param>
    /// <returns>Translated text string based on the identifier, or only the identifier if the id does not exist</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter is not string stringParameter)
        {
            return string.Empty;
        }

        if (value is null)
        {
            return Translator.String(stringParameter);
        }

        return Translator.Prepare(stringParameter, value);
    }

    /// <summary>
    /// Throws <see cref="NotSupportedException"/>.
    /// </summary>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        throw new NotSupportedException(
            "TranslateConverter can only be used for one way conversion."
        );
    }
}
#pragma warning restore CS0618 // Type or member is obsolete
