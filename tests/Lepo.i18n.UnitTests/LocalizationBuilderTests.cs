// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Lepo.i18n.UnitTests.Resources;

namespace Lepo.i18n.UnitTests;

public sealed class LocalizationBuilderTests
{
    [Fact]
    public void FromResource_ShouldThrowException_WhenResourceSetIsMissing()
    {
        LocalizationBuilder builder = new();

        Func<LocalizationBuilder> action = () =>
            builder.FromResource(
                Assembly.GetExecutingAssembly(),
                "Lepo.i18n.UnitTests.Resources.Invalid",
                new CultureInfo("en-US")
            );

        _ = action.Should().Throw<LocalizationBuilderException>();
    }

    [Fact]
    public void FromResource_ShouldLoadResourceFromNamespace()
    {
        LocalizationBuilder builder = new();

        _ = builder.FromResource<Test>(Assembly.GetExecutingAssembly(), new CultureInfo("en-US"));

        ILocalizationProvider provider = builder.Build();

        provider.SetCulture(new CultureInfo("en-US"));
        LocalizationSet? localizationSet = provider.GetLocalizationSet("en-US");

        _ = localizationSet!["Test"].Should().Be("Test in english");
    }

    [Fact]
    public void FromResource_ShouldLoadResource()
    {
        LocalizationBuilder builder = new();

        _ = builder.FromResource(
            Assembly.GetExecutingAssembly(),
            "Lepo.i18n.UnitTests.Resources.Test",
            new CultureInfo("en-US")
        );

        ILocalizationProvider provider = builder.Build();

        provider.SetCulture(new CultureInfo("en-US"));
        LocalizationSet? localizationSet = provider.GetLocalizationSet("en-US");

        _ = localizationSet!["Test"].Should().Be("Test in english");
    }
}
