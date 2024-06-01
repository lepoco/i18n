// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.DependencyInjection.UnitTests;

public sealed class ProviderBasedStringLocalizerFactoryTests
{
    [Fact]
    public void Create_ShouldReturnCorrectIStringLocalizer()
    {
        ServiceCollection services = [];

        _ = services.AddStringLocalizer(b =>
        {
            b.AddLocalization(
                new LocalizationSet(
                    "Test",
                    new CultureInfo("en-US"),
                    new Dictionary<string, string?> { { "Test", "Test in english" } }!
                )
            );
            b.AddLocalization(
                new LocalizationSet(
                    "Test",
                    new CultureInfo("pl-PL"),
                    new Dictionary<string, string?> { { "Test", "Test po polsku" } }!
                )
            );
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        ILocalizationCultureManager manager =
            serviceProvider.GetRequiredService<ILocalizationCultureManager>();

        _ = manager.SetCulture("pl-PL");

        IStringLocalizerFactory factory =
            serviceProvider.GetRequiredService<IStringLocalizerFactory>();
        IStringLocalizer localizer = factory.Create("Test", default!);

        _ = localizer.Should().NotBeNull();
        _ = localizer["Test"].Value.Should().Be("Test po polsku");
    }
}
