// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Collections.Generic;

namespace Lepo.i18n.UnitTests;

public sealed class StringLocalizerBuilderExtensionsTests
{
    private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

    [Fact]
    public void FromResource_ShouldAddLocalizations_WhenResourceSetIsNotNull()
    {
        ServiceCollection services = new();

        _ = services.AddStringLocalizer(b =>
        {
            _ = b.FromResource(assembly, "Lepo.i18n.UnitTests.Resources.Test", new("pl-PL"));
            _ = b.FromResource(assembly, "Lepo.i18n.UnitTests.Resources.Test", new("en-US"));
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IStringLocalizer localizer = serviceProvider.GetRequiredService<IStringLocalizer>();
        ILocalizationManager manager = serviceProvider.GetRequiredService<ILocalizationManager>();

        manager.SetCulture(new("pl-PL"));

        _ = localizer["Test"].Value.Should().Be("Test po polsku");

        manager.SetCulture(new("en-US"));

        _ = localizer["Test"].Value.Should().Be("Test in english");
    }

    [Fact]
    public void FromResource_ShouldReturnWithoutTranslation_WhenResourceIsEmpty()
    {
        ServiceCollection services = new();

        _ = services.AddStringLocalizer(b =>
        {
            _ = b.FromResource(assembly, "Lepo.i18n.UnitTests.Resources.Test", new("cs-CZ"));
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IStringLocalizer localizer = serviceProvider.GetRequiredService<IStringLocalizer>();
        ILocalizationManager manager = serviceProvider.GetRequiredService<ILocalizationManager>();

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

        _ = action
            .Should()
            .Throw<StringLocalizerBuilderException>()
            .WithMessage("Failed to register translation resources.");
    }

    [Fact]
    public void AddLocalization_ShouldAddLocalizations_ForTheProvidedCulture()
    {
        ServiceCollection services = new();

        _ = services.AddStringLocalizer(b =>
        {
            _ = b.AddLocalization(
                new("en-US"),
                new Dictionary<string, string> { { "Test", "Manual translation" } }
            );
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IStringLocalizer localizer = serviceProvider.GetRequiredService<IStringLocalizer>();
        ILocalizationManager manager = serviceProvider.GetRequiredService<ILocalizationManager>();

        manager.SetCulture(new("en-US"));

        _ = localizer["Test"].Value.Should().Be("Manual translation");
    }
}
