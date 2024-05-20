// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Lepo.i18n.Yaml;

namespace Lepo.i18n.UnitTests.Yaml;

public sealed class FromYamlTests
{
    [Fact]
    public void FromYaml_ShouldProperlyAddLocalizations()
    {
        LocalizationBuilder builder = new();
        builder.SetCulture(new CultureInfo("pl-PL"));

        _ = builder.FromYaml(
            "Lepo.i18n.UnitTests.Resources.Translations-pl-PL.yaml",
            new CultureInfo("pl-PL")
        );

        ILocalizationProvider localizationProvider = builder.Build();

        LocalizationSet? localizationSet = localizationProvider.GetLocalizationSet(
            new CultureInfo("pl-PL"),
            default
        );

        _ = localizationSet!
            .Strings.First(x => x.Key == "main.hello")
            .Value.Should()
            .Be("Witaj świecie");
    }
}
