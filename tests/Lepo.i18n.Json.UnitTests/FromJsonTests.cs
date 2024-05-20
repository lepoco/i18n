// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Globalization;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Lepo.i18n.Json.UnitTests;

public sealed class FromJsonTests
{
    [Fact]
    public void FromJson_ShouldProperlyAddLocalizations()
    {
        LocalizationBuilder builder = new();

        _ = builder.FromJson("Resources.Translations-pl-PL.json", new CultureInfo("pl-PL"));
        _ = builder.FromJson("Resources.Translations-en-US.json", new CultureInfo("en-US"));

        ILocalizationProvider localizationProvider = builder.Build();

        LocalizationSet? localizationSet = localizationProvider.GetLocalizationSet("pl-PL");

        _ = localizationSet!
            .Strings.First(x => x.Key == "Test")
            .Value.Should()
            .Be("Test po polsku");
    }
}
