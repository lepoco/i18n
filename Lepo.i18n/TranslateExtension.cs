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
        /// Overridden method, returns the source and path to bind to
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(System.IServiceProvider serviceProvider)
        {
            if (System.String.IsNullOrEmpty(String))
                return System.String.Empty;

            if (!System.String.IsNullOrEmpty(String) && !System.String.IsNullOrEmpty(Plural) && Number != null)
                return Translator.Plural(FixSpecialCharacters(String), FixSpecialCharacters(Plural), Number);

            return Translator.String(FixSpecialCharacters(String));
        }

        private string FixSpecialCharacters(string markupedText)
        {
            return markupedText
                .Trim()
                .Replace("&apos;", "\'")
                .Replace("&quot;", "\"")
                .Replace("&lt;", "<")
                .Replace("&gt;", ">")
                .Replace("&amp;", "&");
        }
    }
}