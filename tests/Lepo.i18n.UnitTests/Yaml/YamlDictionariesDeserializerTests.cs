// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Collections.Generic;
using Lepo.i18n.Yaml;

namespace Lepo.i18n.UnitTests.Yaml;

public sealed class YamlDictionariesDeserializerTests
{
    [Fact]
    public void FromString_ShouldProperlyDecodeString()
    {
        const string input = """
            # Comment
            main.languages: Languages
            main.hello: "Hello world"

            namespace.test:
                main.languages: Languages in namespace.test #yet another comment 
                main.hello: 'Hello world in namespace.test'

            # Some comment
            other.namespace:
                main.languages: Languages in other.namespace #yet another comment 
                main.hello: 'Hello world in other.namespace'
            """;

        IDictionary<string, IDictionary<string, string>> result =
            YamlDictionariesDeserializer.FromString(input);

        _ = result.Should().ContainKey("default").WhoseValue.ContainsKey("main.languages");
        _ = result.Should().ContainKey("namespace.test").WhoseValue.ContainsKey("main.languages");
        _ = result.Should().ContainKey("other.namespace").WhoseValue.ContainsKey("main.languages");
    }
}
