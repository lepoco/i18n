// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Json.Models.v1;

/// <summary>
/// Represents a translation file with a version and a collection of translation entities.
/// </summary>
/// <param name="Version">The version of the translation file.</param>
/// <param name="Strings">The collection of translation entities in the file.</param>
[method: JsonConstructor]
internal sealed record TranslationFile(string Version, IEnumerable<TranslationEntity> Strings)
    : TranslationsContainer(Version);
