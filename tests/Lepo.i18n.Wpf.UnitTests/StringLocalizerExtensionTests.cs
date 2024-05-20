// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Wpf.UnitTests;

public sealed class StringLocalizerExtensionTests
{
    [Fact]
    public void ProvideValue_ShouldReturnLocalizedText()
    {
        LocalizationProvider localizationProvider =
            new(
                new CultureInfo("en-US"),
                [
                    new LocalizationSet(
                        "Test",
                        new CultureInfo("en-US"),
                        new Dictionary<string, string> { { "Test", "Test value" } }!
                    )
                ]
            );

        LocalizationProviderFactory.SetInstance(localizationProvider);

        StringLocalizerExtension extension = new("Test");

        object? result = extension.ProvideValue(null!);

        _ = result.Should().Be("Test value");
    }
}
