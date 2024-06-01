// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Collections.Generic;
using Lepo.i18n.UnitTests.Resources;

namespace Lepo.i18n.UnitTests;

#pragma warning disable CS0618 // Type or member is obsolete
public sealed class TranslatorTests
{
    [Fact]
    public void LoadLanguages_ShouldProperlyRegisterYaml()
    {
        _ = Translator.LoadLanguages(
            Assembly.GetExecutingAssembly(),
            new Dictionary<string, string>
            {
                { "pl_PL", "Lepo.i18n.UnitTests.Resources.Translations-pl-PL.yaml" },
                { "en_US", "Lepo.i18n.UnitTests.Resources.Translations-en-US.yaml" }
            }
        );

        ILocalizationProvider localizationProvider = LocalizationProviderFactory.GetInstance()!;

        _ = localizationProvider
            .GetLocalizationSet("pl-PL")!["main.hello"]
            .Should()
            .Be("Witaj świecie");
    }

    [Fact]
    public void LoadLanguages_ShouldProperlyRegisterResource()
    {
        _ = Translator.LoadLanguages(
            Assembly.GetExecutingAssembly(),
            new Dictionary<string, string>
            {
                { "pl_PL", typeof(Test).FullName },
                { "en_US", typeof(Test).FullName }
            }
        );

        ILocalizationProvider localizationProvider = LocalizationProviderFactory.GetInstance()!;

        _ = localizationProvider.GetLocalizationSet("pl-PL")!["Test"].Should().Be("Test po polsku");
    }

    [Fact]
    public void LoadLanguage_ShouldProperlyRegisterResource()
    {
        _ = Translator.LoadLanguage(
            Assembly.GetExecutingAssembly(),
            "pl_PL",
            typeof(Test).FullName!
        );

        ILocalizationProvider localizationProvider = LocalizationProviderFactory.GetInstance()!;
        localizationProvider.SetCulture(new CultureInfo("en-US"));

        _ = localizationProvider.GetLocalizationSet("pl-PL")!["Test"].Should().Be("Test po polsku");
    }

    [Fact]
    public void SetLanguage_ShouldChangeCulture()
    {
        _ = Translator.LoadLanguage(
            Assembly.GetExecutingAssembly(),
            "pl_PL",
            typeof(Test).FullName!
        );

        ILocalizationProvider localizationProvider = LocalizationProviderFactory.GetInstance()!;
        localizationProvider.SetCulture(new CultureInfo("en-US"));

        _ = localizationProvider.GetCulture().Should().Be(new CultureInfo("en-US"));

        _ = Translator.SetLanguage("pl_PL");

        _ = localizationProvider.GetCulture().Should().Be(new CultureInfo("pl-PL"));
    }
}
#pragma warning restore CS0618 // Type or member is obsolete
