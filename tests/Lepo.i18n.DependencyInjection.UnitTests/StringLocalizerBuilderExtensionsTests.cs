// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Lepo.i18n.Yaml;

namespace Lepo.i18n.DependencyInjection.UnitTests;

public sealed class StringLocalizerBuilderExtensionsTests
{
    private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

    [Fact]
    public void FromResource_ShouldAddLocalizations_WhenResourceSetIsNotNull()
    {
        ServiceCollection services = new();

        _ = services.AddStringLocalizer(b =>
        {
            _ = b.FromYaml(
                "Lepo.i18n.DependencyInjection.UnitTests.Resources.Translations-pl-PL.yaml",
                new CultureInfo("pl-PL")
            );
            _ = b.FromYaml(
                "Lepo.i18n.DependencyInjection.UnitTests.Resources.Translations-en-US.yaml",
                new CultureInfo("en-US")
            );
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IStringLocalizer localizer = serviceProvider.GetRequiredService<IStringLocalizer>();
        ILocalizationCultureManager manager =
            serviceProvider.GetRequiredService<ILocalizationCultureManager>();

        _ = localizer["Test"].Value.Should().Be("Test po polsku");

        _ = manager.SetCulture("en-US");

        _ = localizer["Test"].Value.Should().Be("Test in english");
    }

    [Fact]
    public void FromResource_ShouldReturnWithoutTranslation_WhenResourceIsEmpty()
    {
        ServiceCollection services = new();

        _ = services.AddStringLocalizer(b =>
        {
            _ = b.FromResource(
                assembly,
                "Lepo.i18n.DependencyInjection.UnitTests.Resources.Test",
                new("cs-CZ")
            );
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IStringLocalizer localizer = serviceProvider.GetRequiredService<IStringLocalizer>();
        ILocalizationCultureManager manager =
            serviceProvider.GetRequiredService<ILocalizationCultureManager>();

        manager.SetCulture(new("cz-CZ"));

        _ = localizer["Test"].Value.Should().Be("Test");
    }

    [Fact]
    public void FromResource_ShouldThrowException_WhenResourceSetIsMissing()
    {
        ServiceCollection services = new();

        Func<IServiceCollection> action = () =>
            services.AddStringLocalizer(b =>
            {
                _ = b.FromResource(assembly, "Lepo.i18n.UnitTests.Resources.Invalid", new("en-US"));
            });

        _ = action.Should().Throw<LocalizationBuilderException>();
    }

    [Fact]
    public void AddLocalization_ShouldAddLocalizations_ForTheProvidedCulture()
    {
        ServiceCollection services = new();

        _ = services.AddStringLocalizer(b =>
        {
            _ = b.AddLocalization(
                new("en-US"),
                new Dictionary<string, string?> { { "Test", "Manual translation" } }
            );
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IStringLocalizer localizer = serviceProvider.GetRequiredService<IStringLocalizer>();
        ILocalizationCultureManager manager =
            serviceProvider.GetRequiredService<ILocalizationCultureManager>();

        manager.SetCulture(new("en-US"));

        _ = localizer["Test"].Value.Should().Be("Manual translation");
    }
}
