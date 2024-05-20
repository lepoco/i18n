// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Represents errors that occur during the execution of the localization builder.
/// </summary>
public class LocalizationBuilderException : Exception
{
    public LocalizationBuilderException(string message, Exception innerException)
        : base(message, innerException) { }

    public LocalizationBuilderException(string message)
        : base(message) { }
};
