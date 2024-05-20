// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Json.Models;

/// <summary>
/// Defines a contract for a translations container with a schema version.
/// </summary>
internal interface ITranslationsContainer
{
    /// <summary>
    /// Gets the version of the translation container.
    /// </summary>
    string Version { get; }
}
