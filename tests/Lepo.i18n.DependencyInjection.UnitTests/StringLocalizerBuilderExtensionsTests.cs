// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Lepo.i18n.Yaml;

namespace Lepo.i18n.DependencyInjection.UnitTests;

public sealed class StringLocalizerBuilderExtensionsTests
{
    [Fact]
    public void AddStringLocalizer_ShouldAddLocalizations_ToServiceCollection()
    {
        ServiceCollection services = new();

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
        IStringLocalizer localizer = serviceProvider.GetRequiredService<IStringLocalizer>();
        ILocalizationCultureManager manager =
            serviceProvider.GetRequiredService<ILocalizationCultureManager>();

        _ = manager.SetCulture("pl-PL");
        
        _ = localizer["Test"].Value.Should().Be("Test po polsku");

        _ = manager.SetCulture("en-US");

        _ = localizer["Test"].Value.Should().Be("Test in english");
    }

    [Fact]
    public void AddStringLocalizer_ShouldRegisterEmptySet()
    {
        ServiceCollection services = new();

        _ = services.AddStringLocalizer(b =>
        {
            b.AddLocalization(
                new LocalizationSet(
                    "Test",
                    new CultureInfo("cz-CZ"),
                    new Dictionary<string, string?>()
                )
            );
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IStringLocalizer localizer = serviceProvider.GetRequiredService<IStringLocalizer>();
        ILocalizationCultureManager manager =
            serviceProvider.GetRequiredService<ILocalizationCultureManager>();

        manager.SetCulture(new("cz-CZ"));

        _ = localizer["Test"].Value.Should().Be("Test");
    }
}
