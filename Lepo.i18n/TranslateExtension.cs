// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Windows.Markup;

namespace Lepo.i18n
{
    /// <summary>
    /// XAML markup extension for <see cref="Translator"/>.
    /// </summary>
    public class TranslateExtension : MarkupExtension
    {
        /// <summary>
        /// Word to be translated.
        /// </summary>
        public string String { get; set; }

        /// <summary>
        /// Overridden method, returns the source and path to bind to
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(System.IServiceProvider serviceProvider)
        {
            if (System.String.IsNullOrEmpty(String))
                return System.String.Empty;

            return Translator.String(FixSpecialCharacters(String));
        }

        private string FixSpecialCharacters(string markupedText)
        {
            return markupedText
                .Trim()
                .Replace("&apos;", "\'")
                .Replace("	&quot;", "\"")
                .Replace("&lt;", "<")
                .Replace("&gt;", ">")
                .Replace("&amp;", "&");
        }
    }
}