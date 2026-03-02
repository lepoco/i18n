// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Wpf.UnitTests;

[CollectionDefinition("ExtensionsTests", DisableParallelization = true)]
public sealed class StringLocalizerExtensionTests
{
    [Fact]
    public void ProvideValue_ShouldReturnLocalizedText()
    {
        string provider = nameof(ProvideValue_ShouldReturnLocalizedText);
        LocalizationProvider localizationProvider = new(
            new CultureInfo("en-US"),
            [
                new LocalizationSet(
                    default,
                    new CultureInfo("en-US"),
                    new Dictionary<string, string> { { "Test", "Test value" } }!
                ),
            ]
        );

        LocalizationProviderFactory.SetInstance(localizationProvider, provider);
        LocalizationProviderFactory.GetInstance(provider)!.SetCulture(new CultureInfo("en-US"));

        _ = new StringLocalizerExtension("Test") { ProviderKey = provider }
            .ProvideValue(null!)
            .Should()
            .Be("Test value");
    }
}
