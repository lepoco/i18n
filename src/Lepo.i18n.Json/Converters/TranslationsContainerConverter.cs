// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Lepo.i18n.Json.Models;

namespace Lepo.i18n.Json.Converters;

/// <summary>
/// JSON converter for the ITranslationsContainer interface.
/// </summary>
internal class TranslationsContainerConverter : JsonConverter<ITranslationsContainer>
{
    public override ITranslationsContainer? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        JsonElement jsonObject = JsonDocument.ParseValue(ref reader).RootElement;

        string version = "1.0";

        foreach (JsonProperty property in jsonObject.EnumerateObject())
        {
            if (string.Equals(property.Name, "Version", StringComparison.OrdinalIgnoreCase))
            {
                version = property.Value.GetString() ?? version;
                break;
            }
        }

        return new TranslationsContainer(new Version(version).ToString());
    }

    public override void Write(
        Utf8JsonWriter writer,
        ITranslationsContainer? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new TranslationsContainer(value?.Version ?? "1.0"),
            options
        );
    }
}
