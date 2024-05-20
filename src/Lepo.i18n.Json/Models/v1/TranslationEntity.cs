// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Json.Models.v1;

/// <summary>
/// Represents a translation entity with a name and a value.
/// </summary>
/// <param name="Name">The name of the translation entity.</param>
/// <param name="Value">The value of the translation entity.</param>
[method: JsonConstructor]
internal readonly record struct TranslationEntity(string Name, string Value);
