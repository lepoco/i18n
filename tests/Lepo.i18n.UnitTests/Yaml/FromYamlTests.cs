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
        _ = builder.FromYaml(
            "Lepo.i18n.UnitTests.Resources.Translations.pl_PL.yaml",
            new CultureInfo("pl-PL")
        );

        LocalizationProvider localizationProvider =
            new(new CultureInfo("pl-PL"), builder.GetLocalizations());

        LocalizationSet? localizationSet = localizationProvider.Get(new CultureInfo("pl-PL"));

        _ = localizationSet!
            .Strings.First(x => x.Key == "main.hello")
            .Value.Should()
            .Be("Witaj świecie");
    }
}
