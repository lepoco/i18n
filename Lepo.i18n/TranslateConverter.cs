// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System;
using System.Globalization;
using System.Windows.Data;

namespace Lepo.i18n
{
    /// <summary>
    /// Translates text strings using the <c>Convert</c> method
    /// </summary>
    public sealed class TranslateConverter : IValueConverter
    {
        /// <summary>
        /// Translates text strings using the Converter in XAML
        /// </summary>
        /// <param name="value">ID of the translated text</param>
        /// <param name="targetType">The format that will be returned, usually a string</param>
        /// <returns>Translated text string based on the identifier, or only the identifier if the id does not exist</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Translator.String((string)parameter);
        }

        /// <summary>
        /// Throws <see cref="NotSupportedException"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ITranslator can only be used for one way conversion.");
        }
    }
}