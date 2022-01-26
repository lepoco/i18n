// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Collections.Generic;

namespace Lepo.i18n
{
    /// <summary>
    /// Singleton containing a globally available translation table.
    /// Both for XAML and C# back.
    /// </summary>
    internal static class TranslationStorage
    {
        public static IDictionary<string, IDictionary<uint, string>> TranslationsDictionary =
            new Dictionary<string, IDictionary<uint, string>>();

        public static string CurrentLanguage = "en_US";
    }
}