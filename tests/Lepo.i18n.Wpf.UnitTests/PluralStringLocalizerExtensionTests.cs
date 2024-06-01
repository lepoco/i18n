// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Wpf.UnitTests;

[CollectionDefinition("ExtensionsTests", DisableParallelization = true)]
public sealed class PluralStringLocalizerExtensionTests
{
    [Fact]
    public void ProvideValue_ShouldReturnProperLocalizationPerCount()
    {
        string provider = nameof(ProvideValue_ShouldReturnProperLocalizationPerCount);
        LocalizationProvider localizationProvider =
            new(
                new CultureInfo("en-US"),
                [
                    new LocalizationSet(
                        default,
                        new CultureInfo("en-US"),
                        new Dictionary<string, string>
                        {
                            { "users.single", "There is only one user" },
                            { "users.plural", "There are {0} users" }
                        }!
                    )
                ]
            );

        LocalizationProviderFactory.SetInstance(localizationProvider, provider);
        LocalizationProviderFactory.GetInstance(provider)!.SetCulture(new CultureInfo("en-US"));

        _ = new PluralStringLocalizerExtension(1, "users.single", "users.plural")
        {
            ProviderKey = provider
        }
            .ProvideValue(null!)
            .Should()
            .Be("There is only one user");

        LocalizationProviderFactory.SetInstance(localizationProvider);
        _ = new PluralStringLocalizerExtension(4, "users.single", "users.plural")
        {
            ProviderKey = provider
        }
            .ProvideValue(null!)
            .Should()
            .Be("There are 4 users");
    }
}
