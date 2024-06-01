// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.DependencyInjection;

/// <summary>
/// Provides a dependency injection-based implementation of the <see cref="LocalizationBuilder"/> class.
/// </summary>
/// <remarks>
/// This class uses the .NET Core dependency injection framework to register localization services.
/// It allows the registration of resources for specific cultures using the <see cref="FromResource{TResource}"/> method.
/// </remarks>
public class DependencyInjectionLocalizationBuilder(IServiceCollection services)
    : LocalizationBuilder
{
    /// <inheritdoc />
    public override void FromResource<TResource>(CultureInfo culture)
    {
        base.FromResource<TResource>(culture);

        _ = services.AddSingleton<
            IStringLocalizer<TResource>,
            ProviderBasedStringLocalizer<TResource>
        >();
    }
}
